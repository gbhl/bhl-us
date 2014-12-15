using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net.Mail;
using System.Text;
using MOBOT.BHL.BHLEndNoteExport.BHLWS;
using MOBOT.BHL.Web.Utilities;
using ICSharpCode.SharpZipLib;

namespace MOBOT.BHL.BHLEndNoteExport
{
    public class EndNoteProcessor
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

            // Generate the EndNote files
            this.ProcessEndNote();

            // Report the results of item/page processing
            this.ProcessResults();

            this.LogMessage("Processing Complete");
        }

        private void ProcessEndNote()
        {
            BHLWS.BHLWS service = null;

            try
            {
                service = new BHLWS.BHLWS();
                service.Timeout = 1800000; // wait fifteen minutes for this call to return
                TitleEndNote[] citations = null;

                this.LogMessage("Processing items...");
                this.LogMessage("Getting data for all items.");
                citations = service.TitleEndNoteSelectAllItemCitations();
                this.GenerateCitations(citations, configParms.EndNoteItemTempFile,
                    configParms.EndNoteItemFile, configParms.EndNoteItemZipFile, processedItemCitations);
                this.LogMessage("Item processing complete.");

                this.LogMessage("Processing titles...");
                this.LogMessage("Getting data for all titles.");
                citations = service.TitleEndNoteSelectAllTitleCitations();
                this.GenerateCitations(citations, configParms.EndNoteTitleTempFile,
                    configParms.EndNoteTitleFile, configParms.EndNoteTitleZipFile, processedTitleCitations);
                this.LogMessage("Title processing complete.");

                this.LogMessage("Processing segments...");
                this.LogMessage("Getting data for all segments.");
                citations = service.SegmentSelectAllEndNoteCitations();
                this.GenerateCitations(citations, configParms.EndNoteSegmentTempFile,
                    configParms.EndNoteSegmentFile, configParms.EndNoteSegmentZipFile, processedSegmentCitations);
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

        private void GenerateCitations(TitleEndNote[] citations, String endnoteTempFile,
            String endnoteFile, String endnoteZipFile, List<String> processedCitations)
        {
            // Clean up an existing temp file
            if (File.Exists(endnoteTempFile)) File.Delete(endnoteTempFile);

            if (citations.Length > 0)
            {
                double numErrors = 0;
                foreach (TitleEndNote citation in citations)
                {
                    try
                    {
                        String citationText = this.GenerateCitation(citation);
                        File.AppendAllText(endnoteTempFile, citationText, Encoding.UTF8);
                        processedCitations.Add(citation.TitleID.ToString() + "|" + citation.ItemID.ToString() + "|" + citation.SegmentID.ToString());

                        this.LogMessage("Processing complete for citation: " + citation.TitleID.ToString() + "|" + citation.ItemID.ToString() + "|" + citation.SegmentID.ToString());
                    }
                    catch (Exception ex)
                    {
                        log.Error("Exception processing citation: " + citation.TitleID.ToString() + "|" + citation.ItemID.ToString() + "|" + citation.SegmentID.ToString(), ex);
                        errorMessages.Add("Exception processing citation " + citation.TitleID.ToString() + "|" + citation.ItemID.ToString() + "|" + citation.SegmentID.ToString() + ":  " + ex.Message);
                        numErrors++;
                        // don't bomb.  try next citation
                    }
                }

                if ((numErrors / Convert.ToDouble(citations.Length) * 100) > 1.0)
                {
                    log.Error("EndNote processing failed. " + numErrors.ToString() + " out of " + citations.Length + " item citations produced errors.");
                    errorMessages.Add("EndNote processing failed. " + numErrors.ToString() + " out of " + citations.Length + " item citations produced errors.");
                }
                else
                {
                    // Move the newly created file to "production"
                    File.Delete(endnoteFile);
                    File.Move(endnoteTempFile, endnoteFile);

                    // Create a compressed version of the file
                    this.CompressFile(endnoteFile, endnoteZipFile);
                }
            }

            this.LogMessage("Citation processing complete.");
        }

        private String GenerateCitation(TitleEndNote citation)
        {
            String citationText = String.Empty;

            String type = citation.PublicationType;
            String authors = citation.Authors;
            String year = citation.Year;
            String title = citation.Title;
            String journal = citation.Journal;
            String fullTitle = citation.FullTitle;
            String secondaryTitle = citation.SecondaryTitle;
            String publisher = citation.Publisher;
            String publisherPlace = citation.PublisherPlace;
            String publisherName = citation.PublisherName;
            String volume = citation.Volume;
            String issue = citation.Issue;
            String series = citation.Series;
            String pageRange = citation.PageRange;
            String startPage = citation.StartPage;
            String shortTitle = citation.ShortTitle;
            String abbreviation = citation.Abbreviation;
            String isbnissn = citation.Isbn;
            String callNumber = citation.CallNumber;
            String keywords = citation.Keywords;
            String language = citation.LanguageName;
            String summary = citation.Summary;
            String note = citation.Note;
            String edition = citation.EditionStatement;
            String doi = citation.Doi;
            String url = String.Empty;
            if (citation.SegmentID != 0)
            {
                url = String.Format(configParms.SegmentUrl, citation.SegmentID.ToString());
            }
            else if (citation.ItemID == 0)
            {
                url = String.Format(configParms.TitleUrl, citation.TitleID.ToString());
            }
            else
            {
                url = String.Format(configParms.ItemUrl, citation.ItemID.ToString());
            }

            Dictionary<String, String> elements = new Dictionary<string, string>();
            if (authors != String.Empty) elements.Add(EndNoteRefElementName.AUTHORS, authors);
            if (year != String.Empty) elements.Add(EndNoteRefElementName.YEAR, year);
            if (title != String.Empty) elements.Add(EndNoteRefElementName.TITLE, title);
            if (journal != String.Empty) elements.Add(EndNoteRefElementName.JOURNAL, journal);
            if (fullTitle != String.Empty) elements.Add(EndNoteRefElementName.TITLE, fullTitle);
            if (secondaryTitle != String.Empty) elements.Add(EndNoteRefElementName.SECONDARYTITLE, secondaryTitle);
            if (publisher != string.Empty) elements.Add(EndNoteRefElementName.PUBLISHER, publisher);
            if (publisherPlace != String.Empty) elements.Add(EndNoteRefElementName.CITY, publisherPlace);
            if (publisherName != String.Empty) elements.Add(EndNoteRefElementName.PUBLISHER, publisherName);
            if (volume != String.Empty) elements.Add(EndNoteRefElementName.VOLUME, volume);
            if (issue != String.Empty) elements.Add(EndNoteRefElementName.ISSUE, issue);
            if (series != String.Empty) elements.Add(EndNoteRefElementName.SERIESTITLE, series);
            if (pageRange != String.Empty) elements.Add(EndNoteRefElementName.PAGES, pageRange);
            if (startPage != String.Empty) elements.Add(EndNoteRefElementName.STARTPAGE, startPage);
            if (shortTitle != String.Empty) elements.Add(EndNoteRefElementName.SHORTTITLE, shortTitle);
            if (abbreviation != String.Empty) elements.Add(EndNoteRefElementName.ABBREVIATION, abbreviation);
            if (isbnissn != String.Empty) elements.Add(EndNoteRefElementName.ISBNISSN, isbnissn);
            if (callNumber != String.Empty) elements.Add(EndNoteRefElementName.CALLNUMBER, callNumber);
            if (keywords != String.Empty) elements.Add(EndNoteRefElementName.KEYWORDS, keywords);
            if (language != String.Empty) elements.Add(EndNoteRefElementName.LANGUAGE, language);
            if (summary != String.Empty) elements.Add(EndNoteRefElementName.ABSTRACT, summary);
            if (note != String.Empty) elements.Add(EndNoteRefElementName.NOTE, note);
            if (edition != String.Empty) elements.Add(EndNoteRefElementName.EDITION, edition);
            if (doi != String.Empty) elements.Add(EndNoteRefElementName.DOI, doi);
            if (url != String.Empty) elements.Add(EndNoteRefElementName.URL, url);

            EndNote endnote = new EndNote(type, elements);
            citationText = endnote.GenerateReference();

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

            sb.Append("BHLEndNoteExport: Processing  on " + thisComputer + " complete." + endOfLine);
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
                    mailMessage.Subject = "BHLEndNoteExport: Processing on " + thisComputer + " completed successfully.";
                }
                else
                {
                    mailMessage.Subject = "BHLEndNoteExport: Processing on " + thisComputer + " completed with errors.";
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
