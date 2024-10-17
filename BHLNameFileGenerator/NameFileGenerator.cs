using BHL.WebServiceREST.v1;
using BHL.WebServiceREST.v1.Client;
using MOBOT.FileAccess;
using MOBOT.IA.Utilities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Mail;
using System.Text;

namespace MOBOT.BHL.BHLNameFileGenerator
{
    public class NameFileGenerator
    {
        // Create a logger for use in this class
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        // NOTE that using System.Reflection.MethodBase.GetCurrentMethod().DeclaringType
        // is equivalent to typeof(LoggingExample) but is more portable
        // i.e. you can copy the code directly into another class without
        // needing to edit the code.

        private ConfigParms configParms = new ConfigParms();
        private bool getItemsPerformed = false;
        private List<string> filesCreated = new List<string>();
        private List<string> filesUploaded = new List<string>();
        private List<string> errorMessages = new List<string>();

        /// <summary>
        /// Read and validate configuration parameters, and initiate the appropriate
        /// processor.
        /// </summary>
        public void Process()
        {
            this.LogMessage("BHLNameFileGenerator Processing Start");

            // Load app settings from the configuration file
            configParms.LoadAppConfig();

            // Read additional app settings from the command line
            // Note: Command line arguments override configuration file settings
            if (!this.ReadCommandLineArguments()) return;

            // validate config values
            if (!this.ValidateConfiguration()) return;

            if (configParms.GetItems)
            {
                // Refresh the list of items for which we need to generate name files
                this.GetItems();
            }

            if (configParms.CreateFiles)
            {
                // Create name files
                this.CreateFiles();
            }

            if (configParms.UploadFiles)
            {
                // Upload name files to Internet Archive
                this.UploadFiles();
            }

            // Report the results of item/page processing
            this.ProcessResults();

            this.LogMessage("BHLNameFileGenerator Processing Complete");
        }
        
        /// <summary>
        /// Call a procedure to update the list of items that need name files
        /// </summary>
        private void GetItems()
        {
            try
            {
                this.LogMessage("Getting items that need name files");
                new ItemNameFileLogClient(configParms.BHLWSEndpoint).ItemNameFileLogRefresh(configParms.LastGetItemsDate);
                configParms.UpdateAppSetting(ConfigParms.LastGetItemsDateKey, DateTime.Now);
                getItemsPerformed = true;
                this.LogMessage("Done getting items");
            }
            catch (Exception ex)
            {
                this.LogMessage("Error getting items: " + ex.Message, true);
            }
        }

        /// <summary>
        /// Get a list of items for which to create name files, retreive the xml for the name file,
        /// and write the xml to a file.
        /// </summary>
        private void CreateFiles()
        {
            try
            {
                this.LogMessage("Creating files");
                ICollection<ItemNameFileLog> items = new ItemNameFileLogClient(configParms.BHLWSEndpoint).GetItemNameFileLogForCreate();

                if (items != null)
                {
                    foreach (ItemNameFileLog item in items)
                    {
                        // Create the file
                        string xml = new ItemsClient(configParms.BHLWSEndpoint).GetItemNamesXml((int)item.ItemID);
                        string fileName = String.Format(configParms.NameFileFormat, item.BarCode);
                        string fileLocation = String.Format(configParms.NameFilePathFormat,
                            item.OcrFolderShare, item.FileRootFolder, fileName);
                        FileAccessProvider fileAccessProvider = new FileAccessProvider();
                        fileAccessProvider.SaveFile(new ASCIIEncoding().GetBytes(xml), fileLocation);

                        // Update log information
                        new ItemNameFileLogClient(configParms.BHLWSEndpoint).UpdateItemNameFileLogCreateDate((int)item.LogID);
                        this.filesCreated.Add(item.ItemID.ToString());
                        this.LogMessage("Name file written for item " + item.ItemID.ToString());
                    }
                }
                this.LogMessage("Done creating files");
            }
            catch (Exception ex)
            {
                this.LogMessage("Error creating files: " + ex.Message, true);
            }
        }

        /// <summary>
        /// Get a list of items for which to upload name files, and upload those files.  Don't
        /// quit on upload failure unless we have 10 consecutive failures.
        /// </summary>
        private void UploadFiles()
        {
            S3 s3 = null;

            try
            {
                this.LogMessage("Uploading files");
                ICollection<ItemNameFileLog> items = new ItemNameFileLogClient(configParms.BHLWSEndpoint).GetItemNameFileLogForUpload();

                int consecutiveErrors = 0;
                s3 = new S3(ConfigurationManager.AppSettings["IAS3AccessKey"], ConfigurationManager.AppSettings["IAS3SecretKey"]);

                if (items != null)
                {
                    foreach (ItemNameFileLog item in items)
                    {
                        try
                        {
                            // Upload the file
                            string fileName = String.Format(configParms.NameFileFormat, item.BarCode);
                            string fileLocation = String.Format(configParms.NameFilePathFormat,
                                item.OcrFolderShare, item.FileRootFolder, fileName);
                            string putResult = s3.PutObject(fileLocation, item.BarCode,
                                String.Format(configParms.NameFileFormat, item.BarCode),
                                "application/xml", null, true, false);

                            // Update log information
                            if (putResult == "Success")
                            {
                                new ItemNameFileLogClient(configParms.BHLWSEndpoint).UpdateItemNameFileLogUploadDate((int)item.LogID);
                                this.filesUploaded.Add(item.ItemID.ToString());
                                this.LogMessage("Name file uploaded for item " + item.ItemID.ToString());
                            }
                            else if (putResult.ToLower().Contains("403"))   // "Forbidden" error; see details below
                            {
                                new ItemNameFileLogClient(configParms.BHLWSEndpoint).UpdateItemNameFileLogUploadDate((int)item.LogID);
                                this.LogMessage("Name file skipped (forbidden) for item " + item.ItemID.ToString());
                            }
                            else
                            {
                                this.LogMessage("Error uploading file for item " + item.ItemID + ": " + putResult, true);
                            }

                            // Clear consecutive error count on "Success" or "403" message.  "403" is
                            // an indicator that we do not have permission to write the file to IA.
                            // We're not authorized to write to all IA buckets, so this error is 
                            // expected.
                            if (putResult == "Success" || putResult.ToLower().Contains("403"))
                            {
                                consecutiveErrors = 0;
                            }
                        }
                        catch (Exception ex)
                        {
                            consecutiveErrors++;
                            LogMessage("Error uploading file for item " + item.ItemID + ": " + ex.Message, true);

                            // If we've had 10 consecutive upload failures, then it's time to give up
                            if (consecutiveErrors >= 10)
                            {
                                throw new Exception("Ten consecutive upload errors have occurred");
                            }
                        }
                    }
                }
                this.LogMessage("Done uploading files");
            }
            catch (Exception ex)
            {
                LogMessage("Error uploading files: " + ex.Message, true);
            }
            finally
            {
                if (s3 != null) s3 = null;
            }
        }

        /// <summary>
        /// Reads the arguments supplied on the command line and stores them 
        /// in an instance of the ConfigParms class.
        /// </summary>
        /// <returns>True if the arguments were in a valid format, false otherwise</returns>
        private bool ReadCommandLineArguments()
        {
            return true;
        }

        /// <summary>
        /// Verify that the config file arguments are valid
        /// </summary>
        /// <returns>True if arguments valid, false otherwise</returns>
        private bool ValidateConfiguration()
        {
            return true;
        }

        /// <summary>
        /// Examine the results of the item/page processing and take the appropriate 
        /// actions (log, send email, do nothing).
        /// </summary>
        private void ProcessResults()
        {
            try
            {
                // send email with process results to Exchange group
                if (getItemsPerformed || filesCreated.Count > 0 || filesUploaded.Count > 0 || errorMessages.Count > 0)
                {
                    this.LogMessage("Sending Email....");
                    string message = this.GetEmailBody();
                    this.LogMessage(message);
                    //this.SendEmail(message);
                    this.SendServiceLog("BHLNameFileGenerator", message);
                }
                else
                {
                    this.LogMessage("No items processed.  Email not sent.");
                }
            }
            catch (Exception ex)
            {
                log.Error("Exception sending email.", ex);
                return;
            }
        }

        /// <summary>
        /// Constructs the body of an email message to be sent
        /// </summary>
        /// <returns>Body of email message to be sent</returns>
        private string GetEmailBody()
        {
            StringBuilder sb = new StringBuilder();
            const string endOfLine = "\r\n";

            string thisComputer = Environment.MachineName;
            string itemType = string.Empty;

            if (this.getItemsPerformed)
            {
                sb.Append(endOfLine + "Refreshed list of items for which to build name files" + endOfLine);
            }
            if (this.filesCreated.Count > 0)
            {
                sb.Append(endOfLine + "Created " + this.filesCreated.Count.ToString() + " Files" + endOfLine);
            }
            if (this.filesUploaded.Count > 0)
            {
                sb.Append(endOfLine + "Uploaded " + this.filesUploaded.Count.ToString() + " Files" + endOfLine);
            }
            if (this.errorMessages.Count > 0)
            {
                sb.Append(endOfLine + this.errorMessages.Count.ToString() + " Errors Occurred" + endOfLine + "See the log file for details");
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
        private void SendEmail(string message)
        {
            try
            {
                EmailClient restClient = null;

                MailRequestModel mailRequest = new MailRequestModel();
                mailRequest.Subject = string.Format("BHLNameFileGenerator: Name File Processing on {0} completed {1}.", 
                    Environment.MachineName, 
                    (errorMessages.Count == 0) ? "successfully" : "with errors"); ;
                mailRequest.Body = message;
                mailRequest.From = configParms.EmailFromAddress;

                List<string> recipients = new List<string>();
                foreach (string recipient in configParms.EmailToAddress.Split(',')) recipients.Add(recipient);
                mailRequest.To = recipients;

                restClient = new EmailClient(configParms.BHLWSEndpoint);
                restClient.SendEmail(mailRequest);
            }
            catch (Exception ex)
            {
                log.Error("Email Exception: ", ex);
            }
        }

        /// <summary>
        /// Send the specified email message 
        /// </summary>
        /// <param name="message">Body of the message to be sent</param>
        private void SendServiceLog(string serviceName, string message)
        {
            try
            {
                ServiceLogModel serviceLog = new ServiceLogModel();
                serviceLog.Servicename = serviceName;
                serviceLog.Logdate = DateTime.Now;
                serviceLog.Severityname = (errorMessages.Count == 0 ? "Information" : "Error");
                serviceLog.Message = string.Format("Processing on {0} completed.\n\r{1}", Environment.MachineName, message);

                ServiceLogsClient restClient = new ServiceLogsClient(configParms.BHLWSEndpoint);
                restClient.InsertServiceLog(serviceLog);
            }
            catch (Exception ex)
            {
                log.Error("Service Log Exception: ", ex);
            }
        }

        private void LogMessage(string message)
        {
            this.LogMessage(message, false);
        }

        private void LogMessage(string message, bool isError)
        {
            // logger automatically adds date/time
            if (log.IsInfoEnabled) log.Info(message);
            Console.Write(message + "\r\n");

            // If this is an error message, add it to the in-memory list of error messages
            if (isError) errorMessages.Add(message);
        }
    }
}
