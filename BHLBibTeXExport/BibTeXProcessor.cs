using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net.Mail;
using System.Text;
using MOBOT.BHL.BHLBibTeXExport.BHLWS;
using MOBOT.BHL.Web.Utilities;
using ICSharpCode.SharpZipLib;

namespace MOBOT.BHL.BHLBibTeXExport
{
    public class BibTeXProcessor
    {
        // Create a logger for use in this class
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        // NOTE that using System.Reflection.MethodBase.GetCurrentMethod().DeclaringType
        // is equivalent to typeof(LoggingExample) but is more portable
        // i.e. you can copy the code directly into another class without
        // needing to edit the code.

        private ConfigParms configParms = new ConfigParms();
        private List<string> processedTitleCitations = new List<string>();
        private List<string> processedItemCitations = new List<string>();
        private List<string> processedSegmentCitations = new List<string>();
        private List<string> errorMessages = new List<string>();

        /// <summary>
        /// Read and validate configuration parameters, and initiate the appropriate
        /// processor.
        /// </summary>
        public void Process()
        {
            // Load app settings from the configuration file
            configParms.LoadAppConfig();

            // Read additional app settings from the command line
            // Note: Command line arguments override configuration file settings
            if (!this.ReadCommandLineArguments()) return;

            // validate config values
            if (!this.ValidateConfiguration()) return;

            // Generate the BibTeX
            this.ProcessBibTeX();

            // Report the results of item/page processing
            this.ProcessResults();

            this.LogMessage("BHLBibTeXExport Processing Complete");
        }

        private void ProcessBibTeX()
        {
            BHLWS.BHLWS service = null;

            try
            {
                service = new BHLWS.BHLWS();
                service.Timeout = 1800000; // wait thirty minutes for this call to return
                TitleBibTeX[] citations = null;

                this.LogMessage("Processing items...");
                this.LogMessage("Getting BibTeX data for all items.");
                citations = service.TitleBibTeXSelectAllItemCitations();
                this.GenerateCitations(citations, configParms.BibTexItemTempFile, 
                    configParms.BibTexItemFile, configParms.BibTexItemZipFile, processedItemCitations);
                this.LogMessage("Item processing complete.");

                this.LogMessage("Processing titles...");
                this.LogMessage("Getting BibTeX data for all titles.");
                citations = service.TitleBibTeXSelectAllTitleCitations();
                this.GenerateCitations(citations, configParms.BibTexTitleTempFile,
                    configParms.BibTexTitleFile, configParms.BibTexTitleZipFile, processedTitleCitations);
                this.LogMessage("Title processing complete.");
                
                this.LogMessage("Processing segments...");
                this.LogMessage("Getting BibTeX data for all segments.");
                citations = service.SegmentSelectAllBibTeXCitations();
                this.GenerateCitations(citations, configParms.BibTexSegmentTempFile,
                    configParms.BibTexSegmentFile, configParms.BibTexSegmentZipFile, processedSegmentCitations);
                this.LogMessage("Segment processing complete.");
            }
            catch (Exception ex)
            {
                log.Error("Exception processing citations.", ex);
                errorMessages.Add("Exception processing citation:  " + ex.Message);
            }
            finally
            {
                if (service != null) service.Dispose();
            }
        }

        private void GenerateCitations(TitleBibTeX[] citations, String bibtexTempFile,
            String bibtexFile, String bibtexZipFile, List<String> processedCitations)
        {
            // Clean up an existing temp file
            if (File.Exists(bibtexTempFile)) File.Delete(bibtexTempFile);

            if (citations.Length > 0)
            {
                double numErrors = 0;
                foreach (TitleBibTeX citation in citations)
                {
                    try
                    {
                        String citationText = this.GenerateCitation(citation);
                        File.AppendAllText(bibtexTempFile, citationText, Encoding.UTF8);
                        processedCitations.Add(citation.CitationKey);

                        this.LogMessage("Processing complete for citation: " + citation.CitationKey);
                    }
                    catch (Exception ex)
                    {
                        log.Error("Exception processing citation: " + citation.CitationKey, ex);
                        errorMessages.Add("Exception processing citation " + citation.CitationKey + ":  " + ex.Message);
                        numErrors++;
                        // don't bomb.  try next citation
                    }
                }

                if ((numErrors / Convert.ToDouble(citations.Length) * 100) > 1.0)
                {
                    log.Error("BibTeX processing failed. " + numErrors.ToString() + " out of " + citations.Length + " item citations produced errors.");
                    errorMessages.Add("BibTeX processing failed. " + numErrors.ToString() + " out of " + citations.Length + " item citations produced errors.");
                }
                else
                {
                    // Move the newly created file to "production"
                    File.Delete(bibtexFile);
                    File.Move(bibtexTempFile, bibtexFile);

                    // Create a compressed version of the file
                    this.CompressFile(bibtexFile, bibtexZipFile);
                }
            }

            this.LogMessage("Citation processing complete.");
        }

        private String GenerateCitation(TitleBibTeX citation)
        {
            String citationText = String.Empty;

            String citationType = String.Empty;
            switch (citation.Type)
            {
                case "Article":
                    citationType = BibTeXRefType.ARTICLE;
                    break;
                case "Chapter":
                case "Treatment":
                    citationType = BibTeXRefType.INBOOK;
                    break;
                default:
                    citationType = BibTeXRefType.BOOK;
                    break;
            }
            String citationKey = citation.CitationKey;

            String title = citation.Title;
            String journal = citation.Journal;
            String volume = citation.Volume;
            String series = citation.Series;
            String number = citation.Issue;
            String copyrightStatus = citation.CopyrightStatus;
            String url = citation.Url;
            String note = citation.Note;
            String publisher = citation.Publisher;
            String year = citation.Year;
            String pages = String.Empty;
            if (citationType == BibTeXRefType.ARTICLE || citationType == BibTeXRefType.INBOOK)
                pages = citation.PageRange;
            else
                pages = citation.Pages.ToString();
            String typeOfWork = String.Empty;
            if (citationType == BibTeXRefType.INBOOK) typeOfWork = citation.Type;
            String authors = citation.Authors;
            String author = authors.Replace("|", " and ");
            String keywords = citation.Keywords.Replace("|", ",");

            Dictionary<String, String> elements = new Dictionary<string, string>();
            elements.Add(BibTeXRefElementName.TITLE, title);
            if (journal != String.Empty) elements.Add(BibTeXRefElementName.JOURNAL, journal);
            if (volume != String.Empty) elements.Add(BibTeXRefElementName.VOLUME, volume);
            if (series != String.Empty) elements.Add(BibTeXRefElementName.SERIES, series);
            if (number != String.Empty) elements.Add(BibTeXRefElementName.NUMBER, number);
            if (typeOfWork != String.Empty) elements.Add(BibTeXRefElementName.TYPEOFWORK, typeOfWork);
            if (copyrightStatus != String.Empty) elements.Add(BibTeXRefElementName.COPYRIGHT, copyrightStatus);
            if (url != String.Empty) elements.Add(BibTeXRefElementName.URL, url);
            if (note != String.Empty) elements.Add(BibTeXRefElementName.NOTE, note);
            elements.Add(BibTeXRefElementName.PUBLISHER, publisher);
            elements.Add(BibTeXRefElementName.AUTHOR, author);
            elements.Add(BibTeXRefElementName.YEAR, year);
            if (pages != String.Empty && pages != "0") elements.Add(BibTeXRefElementName.PAGES, pages);
            if (keywords != String.Empty) elements.Add(BibTeXRefElementName.KEYWORDS, keywords);

            BibTeX bibtex = new BibTeX(citationType, citationKey, elements);
            citationText = bibtex.GenerateReference();

            return citationText;
        }

        private void CompressFile(String filename, String compressedFilename)
        {
            this.LogMessage("Generating a compressed version of the citations");
            String errorMessage = String.Empty;

            try
            {
                // Write the compressed file
                ICSharpCode.SharpZipLib.Zip.ZipFile zip = ICSharpCode.SharpZipLib.Zip.ZipFile.Create(compressedFilename);
                zip.BeginUpdate();
                zip.Add(filename);
                zip.CommitUpdate();
                zip.Close();

                long originalSize = new FileInfo(filename).Length;
                long compressedSize = new FileInfo(compressedFilename).Length;
                this.LogMessage("Original size: " + originalSize.ToString() + ", Compressed size: " + compressedSize.ToString());

            } // end try
            catch (Exception ex)
            {
                errorMessage = "Error: Unable to compress file: " + ex.Message;
            }
            finally
            {
                if (errorMessage != String.Empty)
                {
                    log.Error(errorMessage);
                    errorMessages.Add(errorMessage);
                }
            }
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
        /// Examine the results of the item/page processing and take the appropriate 
        /// actions (log, send email, do nothing).
        /// </summary>
        private void ProcessResults()
        {
            try
            {
                // send email with process results to Exchange group
                if (processedTitleCitations.Count > 0 || processedItemCitations.Count > 0 || 
                    processedSegmentCitations.Count > 0 || errorMessages.Count > 0)
                {
                    this.LogMessage("Sending Email....");
                    string message = this.GetEmailBody();
                    this.LogMessage(message);
                    this.SendEmail(message);
                }
                else
                {
                    this.LogMessage("No citations processed.  Email not sent.");
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

            sb.Append("BHLBibTeXExport: Processing  on " + thisComputer + " complete." + endOfLine);
            if (this.processedTitleCitations.Count > 0)
            {
                sb.Append(endOfLine + "Processed " + this.processedTitleCitations.Count.ToString() + " Title Citations" + endOfLine);
            }
            if (this.processedItemCitations.Count > 0)
            {
                sb.Append(endOfLine + "Processed " + this.processedItemCitations.Count.ToString() + " Item Citations" + endOfLine);
            }
            if (this.processedSegmentCitations.Count > 0)
            {
                sb.Append(endOfLine + "Processed " + this.processedSegmentCitations.Count.ToString() + " Segment Citations" + endOfLine);
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
                string thisComputer = Environment.MachineName;
                MailMessage mailMessage = new MailMessage();
                MailAddress mailAddress = new MailAddress(configParms.EmailFromAddress);
                mailMessage.From = mailAddress;
                mailMessage.To.Add(configParms.EmailToAddress);
                if (this.errorMessages.Count == 0)
                {
                    mailMessage.Subject = "BHLBibTeXExport: Processing on " + thisComputer + " completed successfully.";
                }
                else
                {
                    mailMessage.Subject = "BHLBibTeXExport: Processing on " + thisComputer + " completed with errors.";
                }
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
    }
}
