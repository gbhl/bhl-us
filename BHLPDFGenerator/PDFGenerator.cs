using BHL.WebServiceREST.v1;
using BHL.WebServiceREST.v1.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MOBOT.BHL.BHLPDFGenerator
{
    public class PDFGenerator
    {
        // Create a logger for use in this class
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        // NOTE that using System.Reflection.MethodBase.GetCurrentMethod().DeclaringType
        // is equivalent to typeof(LoggingExample) but is more portable
        // i.e. you can copy the code directly into another class without
        // needing to edit the code.

        private ConfigParms configParms = new ConfigParms();
        private List<string> deletedPdfs = new List<string>();
        private List<string> generatedPdfs = new List<string>();
        private List<string> errorMessages = new List<string>();

        private int _pdfStatusPending = 10;
        private int _pdfStatusProcessing = 20;
        private int _pdfStatusGenerated = 30;
        private int _pdfStatusError = 40;
        private int _pdfStatusRejected = 50;

        public void Generate()
        {
            // Load app settings from the configuration file
            configParms.LoadAppConfig();

            // Read additional app settings from the command line
            // Note: Command line arguments override configuration file settings
            if (!this.ReadCommandLineArguments()) return;

            // validate config values
            if (!this.ValidateConfiguration()) return;

            // clean up old pdfs
            this.RemoveOldPdfs();

            // generate the pdfs
            this.ProcessNewPdfs();
            
            // Report the results of pdf generation
            this.ProcessResults();

            this.LogMessage("BHLPDFGenerator Processing Complete");
        }

        #region Process the PDFs

        private void RemoveOldPdfs()
        {
            try
            {
                this.LogMessage("Deleting old PDFs...");
                PdfClient restClient = new PdfClient(configParms.BHLWSEndpoint);

                // Get data for PDFs that need to be deleted
                this.LogMessage("Getting PDFs to be deleted.");
                ICollection<PDF> pdfs = restClient.GetPdfsForDeletion();

                foreach (PDF pdf in pdfs)
                {
                    // Delete the file
                    if (File.Exists(pdf.FileLocation)) File.Delete(pdf.FileLocation);

                    // Mark the PDF record as deleted
                    restClient.UpdatePdfDeletionDate((int)pdf.PdfID, new PdfModel());

                    this.deletedPdfs.Add(pdf.PdfID.ToString());
                }
            }
            catch (Exception ex)
            {
                log.Error("Exception deleting old PDFs.", ex);
                errorMessages.Add("Exception deleting old PDFs:  " + ex.Message);
            }
        }

        private void ProcessNewPdfs()
        {
            try
            {
                this.LogMessage("Processing new PDFs...");
                PdfClient pdfRestClient = new PdfClient(configParms.BHLWSEndpoint);

                // Get data for the first PDF that needs to be generated
                this.LogMessage("Getting PDF to be generated.");
                ICollection<PDF> pdfs = pdfRestClient.GetPdfsForCreation();
                ICollection<PageSummaryView> pdfPages = null;
                List<string> pageUrls = new List<string>();

                while (pdfs.Count > 0)
                {
                    foreach (PDF pdf in pdfs)
                    {
                        try
                        {
                            // Get the pages for this pdf and process them
                            this.LogMessage("Getting pages for PDF: " + pdf.PdfID);
                            pdfPages = new PageSummaryViewClient(configParms.BHLWSEndpoint).GetPageSummaryViewByPdf((int)pdf.PdfID);
                            pageUrls.Clear();

                            foreach (PageSummaryView pdfPage in pdfPages)
                            {
                                // Build the URLs to the page and OCR/DJVU text and add them to the list
                                String urlString = String.Empty;
                                String ocrTextLocation = String.Format(configParms.OcrTextLocation, pdfPage.OcrFolderShare, pdfPage.FileRootFolder);

                                String extUrl = String.Empty;
                                if (pdfPage.ExternalURL.EndsWith(".jp2"))
                                {
                                    extUrl = pdfPage.ExternalURL.Substring(0, pdfPage.ExternalURL.Length - 3) + "jpg";
                                }
                                else if (pdfPage.ExternalURL.IndexOf("/download/" + pdfPage.BarCode + "/page/n", StringComparison.OrdinalIgnoreCase) >= 0)
                                {
                                    extUrl = pdfPage.ExternalURL;
                                }
                                else
                                {
                                    extUrl = pdfPage.ExternalURL;
                                }
                                urlString = extUrl;

                                pageUrls.Add(pdfPage.PageID.ToString() + "|" + urlString + "|" + ocrTextLocation + "|" + pdfPage.SequenceOrder.ToString());
                            }

                            if (pageUrls.Count > 0)
                            {
                                this.LogMessage("Generating file for PDF " + pdf.PdfID);

                                // Generate the PDF
                                PDFDocument pdfDoc = new PDFDocument(pdf, pageUrls, configParms.PdfFilePath, configParms.PdfUrl, configParms.BHLWSEndpoint);
                                pdfDoc.GenerateFile(configParms.RetryImageWait);

                                this.LogMessage(string.Format("Generated file for PDF {0} with {1} image errors.",
                                    pdf.PdfID.ToString(), pdf.NumberImagesMissing.ToString()));
                                foreach (string error in pdfDoc.ImageErrors)
                                {
                                    this.LogMessage(string.Format("Image error for PDF {0}\r\n{1}", pdf.PdfID.ToString(), error));
                                }

                                // Send email to the PDF requestor
                                String emailBody = this.GetRequestorEmailBody((int)pdf.PdfID, pdfDoc.FileUrl,
                                    pdf.ArticleTitle, pdf.ArticleCreators, pdf.ArticleTags);
                                this.SendEmail("BHL PDF Generation request #" + pdf.PdfID.ToString() + " - Complete",
                                    emailBody, "noreply@biodiversitylibrary.org", pdf.EmailAddress,
                                    pdf.ShareWithEmailAddresses);

                                // Update PDF record feedback from the generation process.
                                // This also marks the PDF as generated.
                                pdfRestClient.UpdatePdfGenerationInfo((int)pdf.PdfID, new PdfModel {
                                    FileLocation = pdfDoc.FileLocation,
                                    FileUrl = pdfDoc.FileUrl, 
                                    NumberImagesMissing = pdfDoc.NumberImagesMissing, 
                                    NumberOcrMissing = pdfDoc.NumberOcrMissing 
                                });
                            }

                            this.generatedPdfs.Add(pdf.PdfID.ToString());
                        }
                        catch (Exception ex)
                        {
                            pdfRestClient.UpdatePdfStatus((int)pdf.PdfID, new PdfModel
                            {
                                Pdfstatusid = _pdfStatusError
                            });
                            log.Error("Exception processing pdf: " + pdf.PdfID, ex);
                            errorMessages.Add("Exception processing pdf" + pdf.PdfID + ":  " + ex.Message);
                            // don't bomb.  try next PDF
                        }
                    }

                    // Get data for the next PDF that needs to be generated
                    this.LogMessage("Getting PDF to be generated.");
                    pdfs = pdfRestClient.GetPdfsForCreation();
                    pdfPages = null;
                }

                this.LogMessage("New PDF processing complete.");
            }
            catch (Exception ex)
            {
                log.Error("Exception processing new PDFs.", ex);
                errorMessages.Add("Exception processing new PDFs:  " + ex.Message);
            }
        }

        #endregion Process the PDFs

        #region Get and validate parameters

        /// <summary>
        /// Reads the arguments supplied on the command line and stores them 
        /// in an instance of the ConfigParms class.
        /// </summary>
        /// <returns>True if the arguments were in a valid format, false otherwise</returns>
        private bool ReadCommandLineArguments()
        {
            bool returnValue = true;

            string[] args = System.Environment.GetCommandLineArgs();
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
        private bool ValidateConfiguration()
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
                // Send email if PDFS were deleted, or if an error occurred.
                // Don't send an email each time a PDF is generated.
                string message;
                string serviceName = "BHLPDFGenerator";
                if (deletedPdfs.Count > 0 || errorMessages.Count > 0)
                {
                    string subject = string.Format("{0}: Processing on {1} completed {2}",
                        serviceName,
                        Environment.MachineName,
                        (this.errorMessages.Count == 0 ? "successfully" : "with errors"));

                    this.LogMessage("Sending Email....");
                    message = this.GetCompletionEmailBody();
                    this.LogMessage(message);
                    this.SendServiceLog(serviceName, message);
                    if (this.errorMessages.Count > 0 && configParms.EmailOnError)
                    {
                        this.SendEmail(subject, message, configParms.EmailFromAddress, configParms.EmailToAddress, "");
                    }
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
        private String GetCompletionEmailBody()
        {
            StringBuilder sb = new StringBuilder();
            const string endOfLine = "\r\n";

            string thisComputer = Environment.MachineName;

            if (this.deletedPdfs.Count > 0)
            {
                sb.Append(endOfLine + "Deleted " + this.deletedPdfs.Count.ToString() + " PDFs" + endOfLine);
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
        /// Constructs the body of an email message to be sent
        /// </summary>
        /// <returns>Body of email message to be sent</returns>
        private String GetRequestorEmailBody(int pdfId, String fileLocation, string articleTitle, string articleCreators, string articleTags)
        {
            const string endOfLine = "\r\n";
            string emailText = File.ReadAllText(@"ResponseEmail.txt");
            emailText = emailText.Replace("[FileLocation]", fileLocation);

            if (!string.IsNullOrWhiteSpace(articleTitle + articleCreators + articleTags))
            {
                emailText = emailText.Replace("[ArticleHeader]", "The following metadata was supplied with this request:" + endOfLine + endOfLine);

                if (articleTitle.Trim() != string.Empty)
                    emailText = emailText.Replace("[ArticleTitle]", "Article/Chapter Title - " + articleTitle.Trim() + endOfLine);
                else
                    emailText = emailText.Replace("[ArticleTitle]", "");

                if (articleCreators.Trim() != string.Empty) 
                    emailText = emailText.Replace("[ArticleAuthor]", "Author(s) - " + articleCreators.Trim() + endOfLine);
                else
                    emailText = emailText.Replace("[ArticleAuthor]", "");

                if (articleTags.Trim() != string.Empty) 
                    emailText = emailText.Replace("[ArticleSubject]", "Subjects(s) - " + articleTags.Trim() + endOfLine);
                else
                    emailText = emailText.Replace("[ArticleSubject]", "");

                emailText = emailText.Replace("[ArticleFooter]", endOfLine);
            }
            else
            {
                emailText = emailText.Replace("[ArticleHeader]", "");
                emailText = emailText.Replace("[ArticleTitle]", "");
                emailText = emailText.Replace("[ArticleAuthor]", "");
                emailText = emailText.Replace("[ArticleSubject]", "");
                emailText = emailText.Replace("[ArticleFooter]", "");
            }
            return emailText;
        }

        /// <summary>
        /// Send the specified email message 
        /// </summary>
        /// <param name="message">Body of the message to be sent</param>
        private void SendEmail(String subject, String message, String fromAddress, 
            String toAddress, String ccAddresses)
        {
            try
            {
                MailRequestModel mailRequest = new MailRequestModel();
                mailRequest.Subject = subject;
                mailRequest.Body = message;
                mailRequest.From = fromAddress;

                List<string> recipients = new List<string>();
                foreach (string recipient in toAddress.Split(',')) recipients.Add(recipient);
                mailRequest.To = recipients;

                if (ccAddresses != String.Empty)
                {
                    List<string> ccs = new List<string>();
                    foreach (string cc in ccAddresses.Split(',')) ccs.Add(cc);
                    mailRequest.Cc = ccs;
                }

                EmailClient restClient = new EmailClient(configParms.BHLWSEndpoint);
                restClient.SendEmail(mailRequest);
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

        #endregion Process results and log
    }
}
