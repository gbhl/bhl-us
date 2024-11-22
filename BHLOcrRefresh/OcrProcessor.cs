using BHL.WebServiceREST.v1;
using BHL.WebServiceREST.v1.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;

namespace MOBOT.BHL.BHLOcrRefresh
{
    class OcrProcessor
    {
        // Create a logger for use in this class
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        // NOTE that using System.Reflection.MethodBase.GetCurrentMethod().DeclaringType
        // is equivalent to typeof(LoggingExample) but is more portable
        // i.e. you can copy the code directly into another class without
        // needing to edit the code.

        private ConfigParms configParms = new();
        private List<string> jobsComplete = new();
        private List<string> errorMessages = new();

        public object ItemsClientclient { get; private set; }

        public void Process()
        {
            // Load app settings from the configuration file
            configParms.LoadAppConfig();

            // Read additional app settings from the command line
            // Note: Command line arguments override configuration file settings
            if (!ReadCommandLineArguments()) return;

            // validate config values
            if (!ValidateConfiguration()) return;

            string itemID = string.Empty;
            try
            {
                itemID = GetNextJobId();
                while (!string.IsNullOrWhiteSpace(itemID))
                {
                    try
                    {
                        string status = GetOcrForItem(itemID);
                        new ItemsClient(configParms.BHLWSEndpoint).DeleteItemNames(Convert.ToInt32(itemID)); // Clear names and reset last name lookup date
                        new PageTextLogsClient(configParms.BHLWSEndpoint).InsertPageTextLog(new PageTextLogModel
                        {
                            Itemid = Convert.ToInt32(itemID),
                            Textsource = "OCR",
                            Userid = 1
                        }); // Log the new source of the page text
                        LogMessage(status);
                        MarkJobComplete(itemID, status);
                        jobsComplete.Add(itemID.ToString());
                    }
                    catch (Exception ex)
                    {
                        LogMessage("Error processing Ocr job", ex);
                        if (!string.IsNullOrWhiteSpace(itemID)) MarkJobError(itemID, ex.Message + "\n\r" + ex.StackTrace);
                    }
                    itemID = GetNextJobId();
                }
            }
            catch(Exception ex)
            {
                LogMessage("Error processing Ocr job", ex);
                if (!string.IsNullOrWhiteSpace(itemID)) MarkJobError(itemID, ex.Message + "\n\r" + ex.StackTrace);
            }

            // Report the results of pdf generation
            ProcessResults();
        }

        #region Job File Methods

        private string GetNextJobId()
        {
            string jobId = string.Empty;

            // Get just the first job
            string[] jobs = Directory.GetFiles(configParms.OcrJobNewPath);
            if (jobs.Length > 0)
            {
                // Randomly pick the next job to process.
                // This should help avoid collisions when multiple instances of this process are running.
                int jobIndex = new Random().Next(jobs.Length);

                jobId = new FileInfo(jobs[jobIndex]).Name;
                string newPath = jobs[jobIndex];
                string processingPath = string.Format("{0}{1}", configParms.OcrJobProcessingPath, jobId);

                if (!File.Exists(processingPath))
                {
                    // Write a job file with the current time to the Processing folder
                    File.WriteAllText(processingPath, DateTime.Now.ToString());
                }
                else
                {
                    // Already processing this item, so remove the job
                    jobId = string.Empty;
                }

                // Remove the "new" job file
                if (File.Exists(newPath)) File.Delete(newPath);
            }

            return jobId;
        }

        /// <summary>
        /// Write a job file in the Complete folder and remove the job file from the Processing folder
        /// </summary>
        /// <param name="jobId"></param>
        /// <param name="numFiles"></param>
        private void MarkJobComplete(string jobId, string status)
        {
            File.WriteAllText(string.Format("{0}{1}", configParms.OcrJobCompletePath, jobId), status);
            string processingFile = string.Format("{0}{1}", configParms.OcrJobProcessingPath, jobId);
            if (File.Exists(processingFile)) File.Delete(processingFile);
        }

        /// <summary>
        /// Write a job file in the Error folder and remove the job file from the Processing folder
        /// </summary>
        /// <param name="jobid"></param>
        /// <param name="error"></param>
        private void MarkJobError(string jobId, string error)
        {
            File.WriteAllText(string.Format("{0}{1}", configParms.OcrJobErrorPath, jobId), error);
            string processingFile = string.Format("{0}{1}", configParms.OcrJobProcessingPath, jobId);
            if (File.Exists(processingFile)) File.Delete(processingFile);
        }

        #endregion Job File Methods

        private string GetOcrForItem(string itemID)
        {
            string status = string.Empty;
            string tempFolder = string.Empty;

            try
            {
                string barcode = GetBarcodeForItem(itemID);

                // Create a temp directory for processing this item
                tempFolder = string.Format("{0}{1}", configParms.OcrJobTempPath, barcode);
                if (!Directory.Exists(tempFolder)) Directory.CreateDirectory(tempFolder);

                // Get the DJVU from IA
                Stream djvu = GetDJVU(itemID);

                // Convert the DJVU into TXT files (one per page)
                ConvertDjvuToText(djvu, tempFolder, barcode, itemID);

                // Move the OCR files to their final destination
                status = MoveOcrToProduction(itemID, barcode, tempFolder);

                // Normalize the file names in the database
                NormalizeFileNames(itemID);
            }
            finally
            {
                // Remove the temp folder
                if (!string.IsNullOrWhiteSpace(tempFolder)) Directory.Delete(tempFolder, true);
            }

            return status;
        }

        /// <summary>
        /// Get the barcode for the specified Item
        /// </summary>
        /// <param name="itemID"></param>
        /// <returns></returns>
        private string GetBarcodeForItem(string itemID)
        {
            string barcode = string.Empty;

            try
            {
                Book book = new BooksClient(configParms.BHLWSEndpoint).GetBookByItemID(Convert.ToInt32(itemID));
                if (book != null)
                {
                    barcode = book.BarCode;
                }
                else
                {
                    Segment segment = new SegmentsClient(configParms.BHLWSEndpoint).GetSegmentByItemID(Convert.ToInt32(itemID));
                    if (segment != null) barcode = segment.BarCode;
                }
            }
            catch (Exception e)
            {
                LogMessage("Error getting barcode for item " + itemID, e);
                if (!string.IsNullOrWhiteSpace(itemID)) MarkJobError(itemID, e.Message + "\n\r" + e.StackTrace);
            }


            return barcode;
        }

        /// <summary>
        /// Get the contents of the DJVU file for the specified barcode from Internet Archive
        /// </summary>
        /// <param name="barcode"></param>
        /// <returns></returns>
        private Stream GetDJVU(string itemID)
        {
            StreamReader reader = null;
            Stream djvu = Stream.Null;

            try
            {
                Item item = new ItemsClient(configParms.BHLWSEndpoint).GetItemFilenames(Convert.ToInt32(itemID));
                string djvuPath = new ConfigurationClient(configParms.BHLWSEndpoint).GetDjvuFilePath(item.BarCode, item.DjvuFilename);

                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(djvuPath);
                req.Method = "GET";
                req.Timeout = 15000;
                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                djvu = resp.GetResponseStream();
            }
            catch (Exception e)
            {
                LogMessage("Error getting DJVU for item " + itemID, e);
                if (!string.IsNullOrWhiteSpace(itemID)) MarkJobError(itemID, e.Message + "\n\r" + e.StackTrace);
            }
            finally
            {
                reader?.Dispose();
            }

            return djvu;
        }

        /// <summary>
        /// Convert the specified DJVU stream to individual TXT files (one per page)
        /// </summary>
        /// <param name="djvu"></param>
        /// <param name="tempFolder"></param>
        /// <param name="barcode"></param>
        private void ConvertDjvuToText(Stream djvu, string tempFolder, string barcode, string itemID)
        {
            try
            {
                StringBuilder pageText = new StringBuilder();
                XmlReaderSettings settings = new() { Async = true, DtdProcessing = DtdProcessing.Parse };
                int counter = 1;
                using (XmlReader reader = XmlReader.Create(djvu, settings))
                {
                    bool wordStarted = false;
                    while (reader.Read())
                    {
                        if (reader.NodeType == XmlNodeType.Element && reader.Name == "OBJECT") pageText.Clear();
                        if (reader.NodeType == XmlNodeType.Element && reader.Name == "WORD") wordStarted = true;
                        if (reader.NodeType == XmlNodeType.Text && wordStarted) pageText.Append(reader.Value + " ");
                        if (reader.NodeType == XmlNodeType.EndElement)
                        {
                            if (reader.Name == "WORD") wordStarted = false;
                            if (reader.Name == "LINE") pageText.AppendLine();
                            if (reader.Name == "PARAGRAPH") pageText.AppendLine();
                            if (reader.Name == "OBJECT")
                            {
                                File.WriteAllText(string.Format("{0}\\{1}_{2}.txt", tempFolder, barcode, Convert.ToString(counter).PadLeft(4, '0')), pageText.ToString());
                                counter++;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                LogMessage("Error converting DJVU to TXT for " + barcode, e);
                if (!string.IsNullOrWhiteSpace(itemID)) MarkJobError(itemID, e.Message + "\n\r" + e.StackTrace);
            }
        }

        private void NormalizeFileNames(string itemID)
        {
            try
            {
                new ItemsClient(configParms.BHLWSEndpoint).NormalizeFileNames(Convert.ToInt32(itemID));
            }
            catch (Exception e)
            {
                LogMessage("Error normalizing filenames for item " + itemID, e);
                if (!string.IsNullOrWhiteSpace(itemID)) MarkJobError(itemID, e.Message + "\n\r" + e.StackTrace);
            }
        }

        /// <summary>
        /// Move the OCR files for the specified item from the specified temp folder
        /// to their production location.
        /// </summary>
        /// <param name="itemID"></param>
        /// <param name="tempFolder"></param>
        /// <returns></returns>
        private string MoveOcrToProduction(string itemID, string barcode, string tempFolder)
        {
            string status;

            string path = GetFilePath(itemID, barcode);
            if (!string.IsNullOrWhiteSpace(path))
            {
                string[] files = Directory.GetFiles(tempFolder);
                status = string.Format("Added {0} files to {1}", files.Length, path);
                foreach (string file in files)
                {
                    string fileName = Path.GetFileName(file);
                    File.Copy(file, path + "\\" + fileName, true);
                }
            }
            else
            {
                throw new Exception("File Path for Item " + itemID + " not found.");
            }

            return status;
        }

        /// <summary>
        /// Get the production OCR path for the specified item
        /// </summary>
        /// <param name="barcode"></param>
        /// <returns></returns>
        private string GetFilePath(string itemID, string barcode)
        {
            string path;

            Item item = new ItemsClient(configParms.BHLWSEndpoint).GetItem(Convert.ToInt32(itemID));
            Vault vault = new VaultsClient(configParms.BHLWSEndpoint).GetVault((int)item.VaultID);
            path = string.Format("{0}\\{1}\\{2}", vault.OcrFolderShare, item.FileRootFolder, barcode);

            return path;
        }

        #region Get and validate parameters

        /// <summary>
        /// Reads the arguments supplied on the command line and stores them 
        /// in an instance of the ConfigParms class.
        /// </summary>
        /// <returns>True if the arguments were in a valid format, false otherwise</returns>
        private static bool ReadCommandLineArguments()
        {
            bool returnValue = true;

            string[] args = Environment.GetCommandLineArgs();
            for (int x = 0; x < args.Length; x++)
            {
                // Read any command line arguments here



            }

            return returnValue;
        }

        /// <summary>
        /// Verify that the config file and command line arguments are valid
        /// </summary>
        /// <returns>True if arguments valid, false otherwise</returns>
        private static bool ValidateConfiguration()
        {
            return true;
        }

        #endregion Get and validate parameters

        #region Process results and log

        /// <summary>
        /// Examine the results of the item/page processing and take the appropriate 
        /// actions (log, send email, do nothing).
        /// </summary>
        private void ProcessResults()
        {
            try
            {
                string message;
                string serviceName = "BHLOcrRefresh";
                // Only send email if an error occurred.
                if (errorMessages.Count > 0)
                {
                    LogMessage("Sending Email....");
                    message = GetCompletionEmailBody();
                    LogMessage(message);
                    SendServiceLog(serviceName, message);
                    SendEmail(serviceName, message);
                }
                else if (jobsComplete.Count > 0)
                {
                    message = string.Format("{0} Ocr jobs completed successfully", jobsComplete.Count.ToString());
                    SendServiceLog(serviceName, message);
                }
            }
            catch (Exception ex)
            {
                LogMessage("Exception processing results.", ex);
            }
        }

        /// <summary>
        /// Constructs the body of an email message to be sent
        /// </summary>
        /// <returns>Body of email message to be sent</returns>
        private string GetCompletionEmailBody()
        {
            StringBuilder sb = new();
            const string endOfLine = "\r\n";

            string thisComputer = Environment.MachineName;

            if (errorMessages.Count > 0)
            {
                sb.Append(endOfLine + errorMessages.Count.ToString() + " Errors Occurred" + endOfLine + "See the log file for details" + endOfLine);
                foreach (string message in errorMessages)
                {
                    sb.Append(message + endOfLine);
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// Send the specified email message 
        /// </summary>
        /// <param name="message">Body of the message to be sent</param>
        private void SendEmail(string serviceName, string message)
        {
            if (errorMessages.Count > 0 && configParms.EmailOnError)
            {
                MailRequestModel mailRequest = new()
                {
                    Subject = string.Format("{0}: Process on {1} completed with errors.", serviceName, Environment.MachineName),
                    Body = message,
                    From = configParms.EmailFromAddress
                };

                List<string> recipients = new();
                foreach (string recipient in configParms.EmailToAddress.Split(',')) recipients.Add(recipient);
                mailRequest.To = recipients;

                EmailClient restClient = new(configParms.BHLWSEndpoint);
                restClient.SendEmail(mailRequest);
            }
        }

        /// <summary>
        /// Send the specified message to the log table in the database
        /// </summary>
        /// <param name="serviceName">Name of the service being logged</param>
        /// <param name="message">Body of the message to be sent</param>
        private void SendServiceLog(string serviceName, string message)
        {
            try
            {
                ServiceLogModel serviceLog = new ServiceLogModel();
                serviceLog.Servicename = serviceName;
                serviceLog.Logdate = DateTime.Now;
                serviceLog.Severityname = (errorMessages.Count > 0 ? "Error" : "Information");
                serviceLog.Message = string.Format("Processing on {0} completed.\n\r{1}", Environment.MachineName, message);

                ServiceLogsClient restClient = new ServiceLogsClient(configParms.BHLWSEndpoint);
                restClient.InsertServiceLog(serviceLog);
            }
            catch (Exception ex)
            {
                log.Error("Service Log Exception: ", ex);
            }
        }

        private static void LogMessage(string message)
        {
            if (log.IsInfoEnabled) log.Info(message);
            Console.Write(message + "\r\n");
        }

        private void LogMessage(string message, Exception ex)
        {
            errorMessages.Add(ex.Message);
            log.Error(message, ex);
            Console.Write(message + " : " + ex.Message + "\r\n");
        }

        #endregion Process results and log
    }
}
