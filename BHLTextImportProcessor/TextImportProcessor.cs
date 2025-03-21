﻿using BHL.TextImportUtility;
using BHL.WebServiceREST.v1;
using BHL.WebServiceREST.v1.Client;
using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using MOBOT.FileAccess;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;

namespace BHL.TextImportProcessor
{
    public class TextImportProcessor
    {
        // Create a logger for use in this class
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        // NOTE that using System.Reflection.MethodBase.GetCurrentMethod().DeclaringType
        // is equivalent to typeof(LoggingExample) but is more portable
        // i.e. you can copy the code directly into another class without
        // needing to edit the code.

        private ConfigParms configParms = new ConfigParms();
        private List<string> processedBatches = new List<string>();
        private List<string> processedFiles = new List<string>();
        private List<string> processedPages = new List<string>();
        private List<string> errorMessages = new List<string>();

        private const string _fileFormatBHL = "bhlcsv";

        /// <summary>
        /// Read and validate configuration parameters, read the queued batches, and process them.
        /// </summary>
        public void Process()
        {
            // Load app settings from the configuration file
            configParms.LoadAppConfig();

            // Read additional app settings from the command line
            // Note: Command line arguments override configuration file settings
            if (!this.ReadCommandLineArguments()) return;

            // Validate config values
            if (!this.ValidateConfiguration()) return;

            // Add the folder to hold temporary copies of the Text Import files
            if (!Directory.Exists(configParms.TextImportLocalFilePath)) Directory.CreateDirectory(configParms.TextImportLocalFilePath);

            // Process the text imports
            this.ProcessTextImports();

            // Report the results of item/page processing
            this.ProcessResults();

            this.LogMessage("BHLTextImportProcessor Processing Complete");
        }

        private void ProcessTextImports()
        {
            BHLProvider service = new BHLProvider();

            try
            {
                this.LogMessage("Processing new Text Import batches...");

                // Get data for the first batch that needs to be processed
                this.LogMessage("Getting Batch to be processed.");
                List<TextImportBatch> batches = service.TextImportBatchSelectForFileCreation();
                List<TextImportBatchFile> batchFiles = null;

                while (batches.Count > 0)
                {
                    foreach (TextImportBatch batch in batches)
                    {
                        try
                        {
                            // Get the files for this batch and process them
                            this.LogMessage("Getting Files for Batch: " + batch.TextImportBatchID);
                            batchFiles = service.TextImportBatchFileSelectForProcessing(batch.TextImportBatchID);

                            foreach (TextImportBatchFile batchFile in batchFiles)
                            {
                                string textImportFileLocalPath = string.Empty;

                                try
                                {
                                    this.LogMessage("Generating text files for Text Import File " + batchFile.TextImportBatchFileID);

                                    // Get the Page information (including file paths) for the BHL Item
                                    List<MOBOT.BHL.DataObjects.PageSummaryView> pages = service.PageSummarySelectAllByItemID((int)batchFile.ItemID);
                                    if (pages.Count == 0) pages = service.PageSummarySegmentSelectAllByItemID((int)batchFile.ItemID);

                                    // Get the text import file
                                    string textImportFilePath = string.Format("{0}{1}", configParms.TextImportFilePath, batchFile.Filename);
                                    textImportFileLocalPath = string.Format("{0}{1}", configParms.TextImportLocalFilePath, batchFile.Filename);

                                    WebClient wc = new WebClient();
                                    wc.Encoding = Encoding.UTF8;
                                    string importFileContents = string.Empty;
                                    int retryLimit = 1;
                                    while (retryLimit <= 3)
                                    {
                                        try
                                        {
                                            importFileContents = wc.DownloadString(textImportFilePath);
                                            break;
                                        }
                                        catch (WebException wex)
                                        {
                                            if (((HttpWebResponse)wex.Response).StatusCode == HttpStatusCode.NotFound)
                                            {
                                                // 404 Not Found - retry three times before giving up
                                                if (retryLimit == 3) throw;
                                                Thread.Sleep(TimeSpan.FromSeconds(30)); // Wait 30 seconds before trying again
                                                retryLimit++;
                                            }
                                            else
                                            {
                                                // Some non-404 error, don't try again
                                                throw;
                                            }
                                        }
                                    }
                                    File.AppendAllText(textImportFileLocalPath, importFileContents, Encoding.UTF8);

                                    TextImportTool importTool = new TextImportTool(textImportFileLocalPath);

                                    // Validate the file
                                    int filePageCount = importTool.PageCount();
                                    if (filePageCount == 0) throw new Exception(string.Format("No pages found in {0}", batchFile.Filename));

                                    string fileFormat = importTool.GetFileFormat();
                                    if (fileFormat == _fileFormatBHL)
                                    {
                                        // Add sequence numbers to the file, if necessary.  Throws error if Page ID not part of in Item.
                                        importTool.AddSequenceNumbers();
                                        importTool = new TextImportTool(textImportFileLocalPath);   // Reload file after the update
                                    }
                                    else if (filePageCount != pages.Count)
                                    {
                                        throw new Exception(string.Format("Number of pages {0} in item {1} does not match the number of pages {2} in the file {3}",
                                            pages.Count, batchFile.ItemID, filePageCount, batchFile.Filename));
                                    }

                                    // Parse the transcriptions from the file
                                    foreach (MOBOT.BHL.DataObjects.PageSummaryView page in pages)
                                    {
                                        if (importTool.TextAvailable(page.SequenceOrder.ToString()))
                                        {
                                            // Get the new text for the page from the text import file
                                            string pageText = importTool.GetText(page.SequenceOrder.ToString());

                                            // Write new text file to the correct item path.
                                            // Only write files to final destination if not in debug mode.
                                            string filePath = (configParms.DebugMode ?
                                                string.Format("{0}\\{1}.txt", configParms.DebugPath, page.FileNamePrefix) :
                                                page.OcrTextLocation);
                                            this.PageTextWriteFile(filePath, pageText);

                                            // Update the page so that it will be indexed for search, and write log entry 
                                            // for the page to track updates to the page text.
                                            service.PageUpdateAndLogTextChange(page.PageID, "Text Import", batchFile.TextImportBatchFileID, batchFile.LastModifiedUserID);

                                            // Count the pages processed
                                            this.processedPages.Add(page.PageID.ToString());
                                        }
                                    }

                                    // Update the file status
                                    service.TextImportBatchFileUpdateStatus(batchFile.TextImportBatchFileID, configParms.TextImportBatchFileStatusImported,
                                        batchFile.LastModifiedUserID);
                                }
                                catch (Exception ex)
                                {
                                    // Update the file status and error message
                                    service.TextImportBatchFileUpdate(batchFile.TextImportBatchFileID, configParms.TextImportBatchFileStatusError,
                                        batchFile.ItemID, batchFile.Filename, batchFile.FileFormat, ex.Message, batchFile.LastModifiedUserID);
                                    log.Error("Exception processing Text Import file: " + batchFile.Filename, ex);
                                    errorMessages.Add("Exception processing Text Import file " + batchFile.Filename + ":  " + ex.Message);
                                }
                                finally
                                {
                                    // Delete the local copy of the text import file
                                    if (!string.IsNullOrWhiteSpace(textImportFileLocalPath)) File.Delete(textImportFileLocalPath);

                                    this.processedFiles.Add(batchFile.TextImportBatchFileID.ToString());
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            log.Error("Exception processing Text Import batch: " + batch.TextImportBatchID, ex);
                            errorMessages.Add("Exception processing Text Import batch" + batch.TextImportBatchID + ":  " + ex.Message);
                            // don't bomb.  try next batch
                        }
                        finally
                        {
                            this.processedBatches.Add(batch.TextImportBatchID.ToString());

                            // Upon completion, mark the batch as "Imported".  If errors, those will be noted at the File level.
                            service.TextImportBatchUpdateStatus(batch.TextImportBatchID, configParms.TextImportBatchStatusImported, batch.LastModifiedUserID);
                        }
                    }

                    // Get data for the next PDF that needs to be generated
                    this.LogMessage("Getting Batch to be processed.");
                    batches = service.TextImportBatchSelectForFileCreation();
                    batchFiles = null;
                }

                this.LogMessage("New Text Import batch processing complete.");
            }
            catch (Exception ex)
            {
                log.Error("Exception processing new Text Import batch.", ex);
                errorMessages.Add("Exception processing new Text Import batch:  " + ex.Message);
            }
        }

        /// <summary>
        /// Write the supplied text to the specified file
        /// </summary>
        /// <param name="textFilePath"></param>
        /// <param name="text"></param>
        private void PageTextWriteFile(string textFilePath, string text)
        {
            IFileAccessProvider fileAccessProvider = new FileAccessProvider();
            fileAccessProvider.SaveFile(Encoding.UTF8.GetBytes(text), textFilePath);
        }

        /// <summary>
        /// Reads the arguments supplied on the command line and stores them 
        /// in an instance of the ConfigParms class.
        /// </summary>
        /// <returns>True if the arguments were in a valid format, false otherwise</returns>
        private bool ReadCommandLineArguments()
        {
            bool returnValue = true;
            return returnValue;
        }

        /// <summary>
        /// Verify that the config file and command line arguments are valid
        /// </summary>
        /// <returns>True if arguments valid, false otherwise</returns>
        private bool ValidateConfiguration()
        {
            return true;
        }

        /// <summary>
        /// Examine the results of the batch processing and take the appropriate 
        /// actions (log, send email, do nothing).
        /// </summary>
        private void ProcessResults()
        {
            try
            {
                string message;
                string serviceName = "BHLTextImportProcessor";
                if (processedBatches.Count > 0)
                {
                    this.LogMessage("Sending Email....");
                    message = this.GetEmailBody();
                    this.LogMessage(message);
                    this.SendServiceLog(serviceName, message);
                    this.SendEmail(serviceName, message);
                }
                else
                {
                    message = "Nothing to do";
                    this.LogMessage(message);
                }
            }
            catch (Exception ex)
            {
                log.Error("Exception processing results.", ex);
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
            string thisComputer = Environment.MachineName;

            if (this.processedBatches.Count > 0)
            {
                sb.Append(
                    string.Format(
                        "\r\nProcessed {0} Pages from {1} Files in {2} Batches\r\n",
                        this.processedPages.Count.ToString(), 
                        this.processedFiles.Count.ToString(),
                        this.processedBatches.Count.ToString())
                    );
            }

            if (this.errorMessages.Count > 0)
            {
                sb.Append(
                    string.Format("\r\n{0} Errors Occurred\r\nSee the log file for details\r\n",
                        this.errorMessages.Count.ToString())
                    );
                foreach (string message in errorMessages)
                {
                    sb.Append(string.Format("{0}\r\n", message));
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// Send the specified email message 
        /// </summary>
        /// <param name="serviceName">Name of service</param>
        /// <param name="message">Body of the message to be sent</param>
        private void SendEmail(string serviceName, string message)
        {
            try
            {
                if (errorMessages.Count > 0 && configParms.EmailOnError)
                {
                    MailRequestModel mailRequest = new MailRequestModel();
                    mailRequest.Subject = String.Format(
                        "{0}: Processing on {1} completed {2}.",
                        serviceName,
                        Environment.MachineName,
                        (errorMessages.Count == 0) ? "successfully" : "with errors");
                    mailRequest.Body = message;
                    mailRequest.From = configParms.EmailFromAddress;

                    List<string> recipients = new List<string>();
                    foreach (string recipient in configParms.EmailToAddress.Split(',')) recipients.Add(recipient);
                    mailRequest.To = recipients;

                    EmailClient restClient = new EmailClient(configParms.BHLWSEndpoint);
                    restClient.SendEmail(mailRequest);
                }
            }
            catch (Exception ex)
            {
                log.Error("Email Exception: ", ex);
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

        private void LogMessage(string message)
        {
            // logger automatically adds date/time
            if (log.IsInfoEnabled) log.Info(message);
            Console.Write(message + "\r\n");
        }
    }
}
