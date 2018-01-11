using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Text;
using MOBOT.BHL.BHLPDFGenerator.BHLWS;

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
            BHLWS.BHLWS service = null;

            try
            {
                this.LogMessage("Deleting old PDFs...");
                service = new BHLWS.BHLWS();

                // Get data for PDFs that need to be deleted
                this.LogMessage("Getting PDFs to be deleted.");
                PDF[] pdfs = service.PDFSelectForDeletion();

                foreach (PDF pdf in pdfs)
                {
                    // Delete the file
                    if (File.Exists(pdf.FileLocation)) File.Delete(pdf.FileLocation);

                    // Mark the PDF record as deleted
                    service.PDFUpdateFileDeletion(pdf.PdfID);

                    this.deletedPdfs.Add(pdf.PdfID.ToString());
                }
            }
            catch (Exception ex)
            {
                log.Error("Exception deleting old PDFs.", ex);
                errorMessages.Add("Exception deleting old PDFs:  " + ex.Message);
            }
            finally
            {
                if (service != null) service.Dispose();
            }
        }

        private void ProcessNewPdfs()
        {
            BHLWS.BHLWS service = null;

            try
            {
                this.LogMessage("Processing new PDFs...");
                service = new BHLWS.BHLWS();

                // Get data for the first PDF that needs to be generated
                this.LogMessage("Getting PDF to be generated.");
                PDF[] pdfs = service.PDFSelectForFileCreation();
                PageSummaryView[] pdfPages = null;
                List<String> pageUrls = new List<string>();

                while (pdfs.Length > 0)
                {
                    foreach (PDF pdf in pdfs)
                    {
                        try
                        {
                            // Get the pages for this pdf and process them
                            this.LogMessage("Getting pages for PDF: " + pdf.PdfID);
                            pdfPages = service.PDFPageSummaryViewSelectByPdfID(pdf.PdfID);
                            pageUrls.Clear();

                            foreach (PageSummaryView pdfPage in pdfPages)
                            {
                                // Build the URLs to the page and OCR text and add them to the list
                                String urlString = String.Empty;
                                String ocrTextLocation = String.Format(configParms.OcrTextLocation,
                                    pdfPage.OCRFolderShare, pdfPage.FileRootFolder, pdfPage.BarCode,
                                    pdfPage.FileNamePrefix);

                                String extUrl = String.Empty;
                                if (pdfPage.AltExternalURL.EndsWith(".jp2"))
                                {
                                    extUrl = pdfPage.AltExternalURL.Substring(0, pdfPage.AltExternalURL.Length - 3) + "jpg";
                                }
                                else if (pdfPage.AltExternalURL.IndexOf("/download/" + pdfPage.BarCode + "/page/n", StringComparison.OrdinalIgnoreCase) >= 0)
                                {
                                    extUrl = pdfPage.AltExternalURL + "_w1000"; // scale the image down a bit for inclusion in the PDF
                                }
                                else
                                {
                                    extUrl = pdfPage.AltExternalURL;
                                }
                                urlString = extUrl;

                                pageUrls.Add(pdfPage.PageID.ToString() + "|" + urlString + "|" + ocrTextLocation);
                            }

                            if (pageUrls.Count > 0)
                            {
                                this.LogMessage("Generating file for PDF " + pdf.PdfID);

                                // Generate the PDF
                                PDFDocument pdfDoc = new PDFDocument(pdf, pageUrls,
                                    configParms.PdfFilePath, configParms.PdfUrl);
                                pdfDoc.GenerateFile(configParms.RetryImageWait);

                                this.LogMessage(string.Format("Generated file for PDF {0} with {1} image errors.", 
                                    pdf.PdfID.ToString(), pdf.NumberImagesMissing.ToString()));
                                foreach(string error in pdfDoc.ImageErrors)
                                {
                                    this.LogMessage(string.Format("Image error for PDF {0}\r\n{1}",
                                        pdf.PdfID.ToString(), error));
                                }

                                // Send email to the PDF requestor
                                String emailBody = this.GetRequestorEmailBody(pdf.PdfID, pdfDoc.FileUrl,
                                    pdf.ArticleTitle, pdf.ArticleCreators, pdf.ArticleTags);
                                this.SendEmail("BHL PDF Generation request #" + pdf.PdfID.ToString() + " - Complete",
                                    emailBody, "noreply@biodiversitylibrary.org", pdf.EmailAddress,
                                    pdf.ShareWithEmailAddresses);

                                // Update PDF record feedback from the generation process.
                                // This also marks the PDF as generated.
                                service.PDFUpdateGenerationInfo(pdf.PdfID, pdfDoc.FileLocation,
                                    pdfDoc.FileUrl, pdfDoc.NumberImagesMissing, pdfDoc.NumberOcrMissing);
                            }

                            this.generatedPdfs.Add(pdf.PdfID.ToString());
                        }
                        catch (Exception ex)
                        {
                            service.PDFUpdatePdfStatusError(pdf.PdfID);
                            log.Error("Exception processing pdf: " + pdf.PdfID, ex);
                            errorMessages.Add("Exception processing pdf" + pdf.PdfID + ":  " + ex.Message);
                            // don't bomb.  try next PDF
                        }
                    }

                    // Get data for the next PDF that needs to be generated
                    this.LogMessage("Getting PDF to be generated.");
                    pdfs = service.PDFSelectForFileCreation();
                    pdfPages = null;
                }

                this.LogMessage("New PDF processing complete.");
            }
            catch (Exception ex)
            {
                log.Error("Exception processing new PDFs.", ex);
                errorMessages.Add("Exception processing new PDFs:  " + ex.Message);
            }
            finally
            {
                if (service != null) service.Dispose();
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
                if (deletedPdfs.Count > 0 || errorMessages.Count > 0)
                {
                    String subject = String.Empty;
                    String thisComputer = Environment.MachineName;
                    if (this.errorMessages.Count == 0)
                    {
                        subject = "BHLPDFGenerator: PDF generation on " + thisComputer + " completed successfully.";
                    }
                    else
                    {
                        subject = "BHLPDFGenerator: PDF generation on " + thisComputer + " completed with errors.";
                    }

                    this.LogMessage("Sending Email....");
                    String message = this.GetCompletionEmailBody();
                    this.LogMessage(message);
                    this.SendEmail(subject, message, configParms.EmailFromAddress, configParms.EmailToAddress, "");
                }
                else
                {
                    this.LogMessage("No PDFs generated.  Email not sent.");
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
        private String GetCompletionEmailBody()
        {
            StringBuilder sb = new StringBuilder();
            const string endOfLine = "\r\n";

            string thisComputer = Environment.MachineName;

            sb.Append("BHLPDFGenerator: PDF Generation on " + thisComputer + " complete." + endOfLine);
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
        private String GetRequestorEmailBody(int pdfId, String fileLocation, string articleTitle, 
            string articleCreators, string articleTags)
        {
            StringBuilder sb = new StringBuilder();
            const string endOfLine = "\r\n";

            sb.Append("Your PDF generation request has been completed.");
            sb.Append(endOfLine);
            sb.Append(endOfLine);
            if (!string.IsNullOrWhiteSpace(articleTitle + articleCreators + articleTags))
            {
                sb.Append("The following metadata was supplied with this request:" + endOfLine + endOfLine);
                if (articleTitle.Trim() != string.Empty) sb.Append("Article/Chapter Title - " + articleTitle.Trim() + endOfLine);
                if (articleCreators.Trim() != string.Empty) sb.Append("Author(s) - " + articleCreators.Trim() + endOfLine);
                if (articleTags.Trim() != string.Empty) sb.Append("Subjects(s) - " + articleTags.Trim() + endOfLine);
                sb.Append(endOfLine);
            }
            sb.Append("The PDF can be downloaded from the following location: " + fileLocation);
            sb.Append(endOfLine);
            sb.Append(endOfLine);
            sb.Append("You have 30 days to complete the download.");
            sb.Append(endOfLine);
            sb.Append(endOfLine);
            sb.Append("Having Problems Viewing Your PDF? Depending on your browser, you may experience trouble using the built-in PDF viewer, which may not correctly display the images contained in this PDF. If you experience viewing problems in your browser, open the PDF in an alternative viewer.");
            sb.Append(endOfLine);
            sb.Append(endOfLine);
            sb.Append("If you have questions or need to report an error, please contact us via our Feedback page: https://www.biodiversitylibrary.org/feedback.aspx");
            sb.Append(endOfLine);
            sb.Append(endOfLine);
            sb.Append("**Please Support BHL**");
            sb.Append(endOfLine);
            sb.Append("BHL depends on the financial support of its patrons. Help us continue to provide free services like custom PDFs with a tax-deductible donation: https://s.si.edu/donate-bhl02");
            sb.Append(endOfLine);
            sb.Append(endOfLine);
            sb.Append("Join our mailing list to receive the latest BHL news and content highlights: https://s.si.edu/bhl-mailinglist02");
            sb.Append(endOfLine);

            return sb.ToString();
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
                MailMessage mailMessage = new MailMessage();
                MailAddress mailAddress = new MailAddress(fromAddress);
                mailMessage.From = mailAddress;
                mailMessage.To.Add(toAddress);
                if (ccAddresses != String.Empty) mailMessage.CC.Add(ccAddresses);
                mailMessage.Subject = subject;
                mailMessage.Body = message;

                SmtpClient smtpClient = new SmtpClient(configParms.SMTPHost);
                smtpClient.Send(mailMessage);
            }
            catch (Exception ex)
            {
                log.Error("Email Exception: ", ex);
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
