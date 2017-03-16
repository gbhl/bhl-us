using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using MOBOT.BHL.BHLRISExport.BHLWS;
using System.IO;
using MOBOT.BHL.Web.Utilities;

namespace MOBOT.BHL.BHLRISExport
{
    public class RISProcessor
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

            // Generate the RIS
            this.ProcessRIS();

            // Report the results of item/page processing
            this.ProcessResults();

            this.LogMessage("BHLRISExport Processing Complete");
        }

        private void ProcessRIS()
        {
            BHLWS.BHLWSSoapClient service = null;

            try
            {
                service = new BHLWS.BHLWSSoapClient();
                service.InnerChannel.OperationTimeout = new TimeSpan(0, 30, 0); // wait thirty minutes for this call to return
                RISCitation[] citations = null;

                this.LogMessage("Processing items...");
                this.LogMessage("Getting RIS data for all items.");
                citations = service.ItemSelectAllRISCitations();
                this.GenerateCitations(citations, configParms.RISItemTempFile,
                    configParms.RISItemFile, configParms.RISItemZipFile, processedItemCitations);
                this.LogMessage("Item processing complete.");

                this.LogMessage("Processing titles...");
                this.LogMessage("Getting RIS data for all titles.");
                citations = service.TitleSelectAllRISCitations();
                this.GenerateCitations(citations, configParms.RISTitleTempFile,
                    configParms.RISTitleFile, configParms.RISTitleZipFile, processedTitleCitations);
                this.LogMessage("Title processing complete.");

                this.LogMessage("Processing segments...");
                this.LogMessage("Getting RIS data for all segments.");
                citations = service.SegmentSelectAllRISCitations();
                this.GenerateCitations(citations, configParms.RISSegmentTempFile,
                    configParms.RISSegmentFile, configParms.RISSegmentZipFile, processedSegmentCitations);
                this.LogMessage("Segment processing complete.");
            }
            catch (Exception ex)
            {
                log.Error("Exception processing citations.", ex);
                errorMessages.Add("Exception processing citation:  " + ex.Message);
            }
        }

        private void GenerateCitations(RISCitation[] citations, String risTempFile,
            String risFile, String risZipFile, List<String> processedCitations)
        {
            // Clean up an existing temp file
            if (File.Exists(risTempFile)) File.Delete(risTempFile);

            if (citations.Length > 0)
            {
                BHLWS.BHLWSSoapClient service = new BHLWSSoapClient();

                double numErrors = 0;
                foreach (RISCitation citation in citations)
                {
                    try
                    {
                        String citationText = service.GenerateRISCitation(citation);
                        File.AppendAllText(risTempFile, citationText, Encoding.UTF8);
                        processedCitations.Add(citation.Url);

                        this.LogMessage("Processing complete for citation: " + citation.Url);
                    }
                    catch (Exception ex)
                    {
                        log.Error("Exception processing citation: " + citation.Url, ex);
                        errorMessages.Add("Exception processing citation " + citation.Url + ":  " + ex.Message);
                        numErrors++;
                        // don't bomb.  try next citation
                    }
                }

                if ((numErrors / Convert.ToDouble(citations.Length) * 100) > 1.0)
                {
                    log.Error("RIS processing failed. " + numErrors.ToString() + " out of " + citations.Length + " item citations produced errors.");
                    errorMessages.Add("RIS processing failed. " + numErrors.ToString() + " out of " + citations.Length + " item citations produced errors.");
                }
                else
                {
                    // Move the newly created file to "production"
                    File.Delete(risFile);
                    File.Move(risTempFile, risFile);

                    // Create a compressed version of the file
                    this.CompressFile(risFile, risZipFile);
                }
            }

            this.LogMessage("Citation processing complete.");
        }

        /*
        private string GenerateCitation(RISCitation citation)
        {
            string citationText = string.Empty;
            
            string citationType = string.Empty;
            switch (citation.Genre)
            {
                case "Article":
                    citationType = RISRefType.ARTICLE;
                    break;
                case "Chapter":
                case "Treatment":
                    citationType = RISRefType.BOOKSECTION;
                    break;
                case "Proceeding":
                    citationType = RISRefType.CONFERENCEPROCEEDING;
                    break;
                case "Conference":
                    citationType = RISRefType.CONFERENCEPAPER;
                    break;
                case "Thesis":
                    citationType = RISRefType.THESIS;
                    break;
                case "Letter":
                    citationType = RISRefType.UNPUBLISHED;
                    break;
                default:
                    citationType = RISRefType.BOOK;
                    break;
            }

            string title = citation.Title;
            string journal = citation.Journal;
            string volume = citation.Volume;
            string issue = citation.Issue;
            string url = citation.Url;
            string publisher = citation.Publisher;
            string publisherPlace = citation.PublicationPlace;
            string year = citation.Year;
            string[] authors = citation.Authors;
            string[] keywords = citation.Keywords;
            string callNumber = citation.CallNumber;
            string doi = citation.Doi;
            string edition = citation.Edition;
            string issnisbn = citation.IssnIsbn;
            string language = citation.Language;
            string notes = citation.Notes;
            string summary = citation.Abstract;
            string startPage = citation.StartPage;
            string endPage = citation.EndPage;

            List<Tuple<string, string>> elements = new List<Tuple<string, string>>();
            elements.Add(GetRISElement(RISElementName.TITLE, title));
            if (!string.IsNullOrWhiteSpace(journal)) elements.Add(GetRISElement(RISElementName.TITLECONTAINER, journal));
            if (!string.IsNullOrWhiteSpace(volume)) elements.Add(GetRISElement(RISElementName.VOLUME, volume));
            if (!string.IsNullOrWhiteSpace(issue)) elements.Add(GetRISElement(RISElementName.ISSUE, issue));
            if (!string.IsNullOrWhiteSpace(url)) elements.Add(GetRISElement(RISElementName.URL, url));
            if (!string.IsNullOrWhiteSpace(publisher)) elements.Add(GetRISElement(RISElementName.PUBLISHER, publisher));
            if (!string.IsNullOrWhiteSpace(publisherPlace)) elements.Add(GetRISElement(RISElementName.PUBLISHINGPLACE, publisherPlace));
            if (!string.IsNullOrWhiteSpace(year)) elements.Add(GetRISElement(RISElementName.YEAR, year));
            if (!string.IsNullOrWhiteSpace(startPage)) elements.Add(GetRISElement(RISElementName.STARTPAGE, startPage));
            if (!string.IsNullOrWhiteSpace(endPage)) elements.Add(GetRISElement(RISElementName.ENDPAGE, endPage));
            if (!string.IsNullOrWhiteSpace(callNumber)) elements.Add(GetRISElement(RISElementName.CALLNUMBER, callNumber));
            if (!string.IsNullOrWhiteSpace(doi)) elements.Add(GetRISElement(RISElementName.DOI, doi));
            if (!string.IsNullOrWhiteSpace(edition)) elements.Add(GetRISElement(RISElementName.EDITION, edition));
            if (!string.IsNullOrWhiteSpace(issnisbn)) elements.Add(GetRISElement(RISElementName.ISSNISBN, issnisbn));
            if (!string.IsNullOrWhiteSpace(language)) elements.Add(GetRISElement(RISElementName.LANGUAGE, language));
            if (!string.IsNullOrWhiteSpace(notes)) elements.Add(GetRISElement(RISElementName.NOTES, notes));
            if (!string.IsNullOrWhiteSpace(summary)) elements.Add(GetRISElement(RISElementName.ABSTRACT, summary));
            foreach (string author in authors) elements.Add(GetRISElement(RISElementName.AUTHOR, author));
            foreach (string keyword in keywords) elements.Add(GetRISElement(RISElementName.KEYWORD, keyword));

            RIS ris = new RIS(citationType, elements);
            citationText = ris.GenerateReference();

            return citationText;
        }

        public Tuple<string, string> GetRISElement(string name, string value)
        {
            return new Tuple<string, string>(name, value);
        }
        */

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

            sb.Append("BHLRISExport: Processing  on " + thisComputer + " complete." + endOfLine);
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
        private void SendEmail(String message)
        {
            try
            {
                string thisComputer = Environment.MachineName;

                MailMessage mailMessage = new MailMessage();
                MailAddress mailAddress = new MailAddress(configParms.EmailFromAddress);
                mailMessage.From = mailAddress;
                mailMessage.To.Add(configParms.EmailToAddress);
                mailMessage.Body = message;

                if (this.errorMessages.Count == 0)
                {
                    mailMessage.Subject = "BHLRISExport: Processing on " + thisComputer + " completed successfully.";
                }
                else
                {
                    mailMessage.Subject = "BHLRISExport: Processing on " + thisComputer + " completed with errors.";
                }

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
