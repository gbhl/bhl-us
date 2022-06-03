using BHL.WebServiceREST.v1;
using BHL.WebServiceREST.v1.Client;
using MOBOT.IA.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;

namespace MOBOT.BHL.BHLMETSUpload
{
    public class METSUpload
    {
        // Create a logger for use in this class
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        // NOTE that using System.Reflection.MethodBase.GetCurrentMethod().DeclaringType
        // is equivalent to typeof(LoggingExample) but is more portable
        // i.e. you can copy the code directly into another class without
        // needing to edit the code.

        private ConfigParms configParms = new ConfigParms();
        private List<string> filesCreated = new List<string>();
        private List<string> filesSkipped = new List<string>();
        private List<string> filesUploaded = new List<string>();
        private List<string> errorMessages = new List<string>();

        public void Process()
        {
            this.LogMessage("METSUpload Processing Start");

            try
            {
                // Load app settings from the configuration file
                configParms.LoadAppConfig();
            }
            catch (Exception e)
            {
                this.LogMessage("LoadAppConfig Error: " + e.Message, true);
            }

            string startDate = DateTime.Now.AddDays(-1).ToString("MM/dd/yyyy 0:00");
            ProcessBooks(startDate);
            ProcessSegments(startDate);

            // Report the results of mets generation
            this.ProcessResults();

            this.LogMessage("METSUpload Processing Complete");
        }

        private void ProcessBooks(string startDate)
        {
            ICollection<Book> items = null;
            try
            {
                //1 - check for updated items (including title, pages, authors, etc)
                items = new BooksClient(configParms.BHLWSEndpoint).GetBooksRecentlyChanged(startDate);
            }
            catch (Exception e)
            {
                this.LogMessage("Error getting recently changed items: " + e.Message, true);
            }

            if (items != null && items.Count > 0)
            {
                foreach (Book item in items)
                {
                    string filename = string.Format("{0}{1}{2}.xml", configParms.UploadFilePath, "book", item.BookID.ToString());

                    try
                    {
                        //2 - generate mets file for all new & updated items
                        Title title = new TitlesClient(configParms.BHLWSEndpoint).GetTitle((int)item.PrimaryTitleID);
                        string mods = new ExportsClient(configParms.BHLWSEndpoint).GetItemMODS((int)item.BookID);
                        ICollection<Page> pages = new BooksClient(configParms.BHLWSEndpoint).GetBookPages((int)item.BookID);

                        string mets = GetMETS((int)item.BookID, title.FullTitle, item.CopyrightStatus, mods, pages);
                        File.WriteAllText(filename, mets);

                        filesCreated.Add(item.BookID.ToString());
                        this.LogMessage("METS file created (Book " + item.BookID + ")");
                    }
                    catch (Exception ex)
                    {
                        this.LogMessage("Error creating METS file (Book " + item.BookID + "): " + ex.Message, true);
                    }

                    //3 - upload the mets file to IA
                    UploadFiles("book", item.BookID.ToString(), filename, item.BarCode);
                }
            }
        }

        private void ProcessSegments(string startDate)
        {
            ICollection<Segment> segments = null;
            try
            {
                //1 - check for updated segments (including pages, authors, etc)
                segments = new SegmentsClient(configParms.BHLWSEndpoint).GetSegmentsRecentlyChanged(startDate);
            }
            catch (Exception e)
            {
                this.LogMessage("Error getting recently changed items: " + e.Message, true);
            }

            if (segments != null && segments.Count > 0)
            {
                foreach (Segment segment in segments)
                {
                    string filename = string.Format("{0}{1}{2}.xml", configParms.UploadFilePath, "segment", segment.SegmentID.ToString());

                    try
                    {
                        //2 - generate mets file for all new & updated segments
                        string mods = new ExportsClient(configParms.BHLWSEndpoint).GetSegmentMODS((int)segment.SegmentID);
                        ICollection<Page> pages = new SegmentsClient(configParms.BHLWSEndpoint).GetSegmentPages((int)segment.SegmentID);

                        string mets = GetMETS((int)segment.SegmentID, segment.Title, segment.CopyrightStatus, mods, pages);
                        File.WriteAllText(filename, mets);

                        filesCreated.Add(segment.SegmentID.ToString());
                        this.LogMessage("METS file created (Segment " + segment.SegmentID+ ")");
                    }
                    catch (Exception ex)
                    {
                        this.LogMessage("Error creating METS file (Segment " + segment.SegmentID + "): " + ex.Message, true);
                    }

                    //3 - upload the mets file to IA
                    UploadFiles("segment", segment.SegmentID.ToString(), filename, segment.BarCode);
                }
            }
        }

        private string GetMETS(int entityID, string title, string copyrightStatus, string mods, ICollection<Page> pages)
        {
            StringBuilder sb = new StringBuilder("");
            sb.AppendLine(string.Format("<mets:mets xmlns:mods=\"http://www.loc.gov/mods/v3\" xmlns:rights=\"http://www.loc.gov/rights/\" xmlns:xlink=\"http://www.w3.org/1999/xlink\" xmlns:lc=\"http://www.loc.gov/mets/profiles\" xmlns:mets=\"http://www.loc.gov/METS/\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xsi:schemaLocation=\"http://www.loc.gov/METS_Profile/ http://www.loc.gov/standards/mets/profile_docs/mets.profile.v1-2.xsd http://www.loc.gov/METS/ http://www.loc.gov/standards/mets/mets.xsd http://www.loc.gov/mods/v3 http://www.loc.gov/standards/mods/v3/mods-3-7.xsd\" OBJID=\"part/{0}\" LABEL=\"{1}\" PROFILE=\"lc:printMaterial\">", entityID.ToString(), HttpUtility.HtmlEncode(title)));
            sb.AppendLine(string.Format("<mets:metsHdr CREATEDATE=\"{0}\">", DateTime.Now.ToString("yyyy-MM-ddThh:mm:ss")));
            sb.AppendLine("<mets:agent ROLE=\"CREATOR\" TYPE=\"ORGANIZATION\">");
            sb.AppendLine("<mets:name>Biodiversity Heritage Library</mets:name>");
            sb.AppendLine(string.Format("<mets:note>mailto:{0}</mets:note>", configParms.MetsEmail));
            sb.AppendLine("</mets:agent>");
            sb.AppendLine("</mets:metsHdr>");
            sb.AppendLine("<mets:dmdSec ID=\"MODS1\">");
            sb.AppendLine("<mets:mdWrap MDTYPE=\"MODS\">");
            sb.AppendLine("<mets:xmlData>");
            sb.Append(mods);
            sb.AppendLine("</mets:xmlData>");
            sb.AppendLine("</mets:mdWrap>");
            sb.AppendLine("</mets:dmdSec>");
            if (copyrightStatus != null && copyrightStatus.Length > 0)
            {
                sb.AppendLine("<mets:amdSec>");
                sb.AppendLine("<mets:rightsMD ID=\"RIGHTS1\">");
                sb.AppendLine("<mets:mdWrap MDTYPE=\"OTHER\" LABEL=\"RIGHTSMD\">");
                sb.AppendLine("<mets:xmlData>");
                sb.AppendLine("<rights:RightsDeclarationMD RIGHTSCATEGORY=\"COPYRIGHTED\">");
                sb.AppendLine(string.Format("<rights:RightsDeclaration>{0}</rights:RightsDeclaration>", HttpUtility.HtmlEncode(copyrightStatus)));
                sb.AppendLine("</rights:RightsDeclarationMD>");
                sb.AppendLine("</mets:xmlData>");
                sb.AppendLine("</mets:mdWrap>");
                sb.AppendLine("</mets:rightsMD>");
                sb.AppendLine("</mets:amdSec>");
            }

            if (pages != null && pages.Count > 0)
            {
                StringBuilder refImages = new StringBuilder("");
                StringBuilder containerPages = new StringBuilder("");
                StringBuilder pageTypes = new StringBuilder("");

                int count = 1;
                foreach (Page p in pages)
                {
                    refImages.AppendLine(string.Format("<mets:file ID=\"pageImg{0}\" GROUPID=\"G{1}\" USE=\"reference image\" MIMETYPE=\"image/jpeg\">", p.PageID, count));
                    refImages.AppendLine(string.Format("<mets:FLocat LOCTYPE=\"URL\" xlink:href=\"https://www.biodiversitylibrary.org/pageimage/{0}\" />", p.PageID));
                    refImages.AppendLine("</mets:file>");

                    containerPages.AppendLine(string.Format("<mets:file ID=\"page{0}\" GROUPID=\"G{1}\" USE=\"container page\" MIMETYPE=\"text/html\">", p.PageID, count));
                    containerPages.AppendLine(string.Format("<mets:FLocat LOCTYPE=\"URL\" xlink:href=\"https://www.biodiversitylibrary.org/page/{0}\" />", p.PageID));
                    containerPages.AppendLine("</mets:file>");

                    string orderLabel = "";
                    if (p.IndicatedPages != null && p.IndicatedPages.Length > 0 && p.IndicatedPages.StartsWith("Page "))
                    {
                        orderLabel = p.IndicatedPages.Substring(5);
                        pageTypes.AppendLine(string.Format("<mets:div TYPE=\"page\" ORDER=\"{0}\" ORDERLABEL=\"{1}\" LABEL=\"{2}\">", count, HttpUtility.HtmlEncode(orderLabel), (p.IndicatedPages != null && p.IndicatedPages.Length > 0 ? HttpUtility.HtmlEncode(p.IndicatedPages) : HttpUtility.HtmlEncode(p.PageTypes))));
                    }
                    else
                    {
                        pageTypes.AppendLine(string.Format("<mets:div TYPE=\"page\" ORDER=\"{0}\" LABEL=\"{1}\">", count, (p.IndicatedPages != null && p.IndicatedPages.Length > 0 ? HttpUtility.HtmlEncode(p.IndicatedPages) : HttpUtility.HtmlEncode(p.PageTypes))));
                    }
                    pageTypes.AppendLine(string.Format("<mets:fptr FILEID=\"pageImg{0}\" />", p.PageID));
                    pageTypes.AppendLine(string.Format("<mets:fptr FILEID=\"page{0}\" />", p.PageID));
                    pageTypes.AppendLine("</mets:div>");

                    count++;
                }

                sb.AppendLine("<mets:fileSec>");
                sb.AppendLine("<mets:fileGrp ID=\"FG1\" USE=\"reference images\">");
                sb.AppendLine(refImages.ToString());
                sb.AppendLine("</mets:fileGrp>");
                sb.AppendLine("<mets:fileGrp ID=\"FG2\" USE=\"container web pages\">");
                sb.AppendLine(containerPages.ToString());
                sb.AppendLine("</mets:fileGrp>");
                sb.AppendLine("</mets:fileSec>");

                sb.AppendLine("<mets:structMap TYPE=\"mixed\">");
                if (copyrightStatus != null && copyrightStatus.Length > 0)
                {
                    sb.AppendLine("<mets:div DMDID=\"MODS1\" ADMID=\"RIGHTS1\" TYPE=\"book\">");
                }
                else
                {
                    sb.AppendLine("<mets:div DMDID=\"MODS1\" TYPE=\"book\">");
                }
                sb.AppendLine(pageTypes.ToString());
                sb.AppendLine("</mets:div>");
                sb.AppendLine("</mets:structMap>");
            }
            sb.AppendLine("</mets:mets>");

            return sb.ToString();
        }

        private void UploadFiles(string entityType, string entityID, string filename, string barcode)
        {
            S3 s3 = null;

            try
            {
                this.LogMessage("Uploading file");
                
                int consecutiveErrors = 0;
                s3 = new S3(configParms.IA3AccessKey, configParms.IA3SecretKey);

                try
                {
                    // Upload the file
                    string putResult = s3.PutObject(filename, barcode, barcode + configParms.IAFileName,"application/xml", null, true, false);

                    // Update log information
                    if (putResult == "Success")
                    {
                        filesUploaded.Add(entityID);
                        this.LogMessage(string.Format("Mets file uploaded for {0} {1}", entityType, entityID));
                        File.Delete(filename);
                    }
                    else if (putResult.ToLower().Contains("403"))   // "Forbidden" error; see details below
                    {
                        filesSkipped.Add(entityID);
                        this.LogMessage(string.Format("Mets file skipped (forbidden) for {0} {1}", entityType, entityID));
                    }
                    else
                    {
                        this.LogMessage(string.Format("Error uploading file for {0} {1} - {2}", entityType, entityID, putResult), true);
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
                    LogMessage(string.Format("Error uploading file for {0} {1} : {2}", entityType, entityID, ex.Message), true);

                    // If we've had 10 consecutive upload failures, then it's time to give up
                    if (consecutiveErrors >= 10)
                    {
                        throw new Exception("Ten consecutive upload errors have occurred");
                    }
                }
            }
            catch (Exception ex)
            {
                LogMessage(string.Format("Error uploading file ({0}): {1}", filename, ex.Message), true);
            }
            finally
            {
                if (s3 != null) s3 = null;
                if(File.Exists(filename))
                {
                    File.Delete(filename);
                }
            }
        }

        #region Logging

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

        #endregion Logging

        #region Process results

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
                if (filesCreated.Count > 0 || filesSkipped.Count > 0 || filesUploaded.Count > 0 || errorMessages.Count > 0)
                {
                    String subject = String.Empty;
                    String thisComputer = Environment.MachineName;
                    if (this.errorMessages.Count == 0)
                    {
                        subject = "BHLMETSUpload: METS processing on " + thisComputer + " completed successfully.";
                    }
                    else
                    {
                        subject = "BHLMETSUpload: METS processing on " + thisComputer + " completed with errors.";
                    }

                    this.LogMessage("Sending Email....");
                    String message = this.GetCompletionEmailBody();
                    this.LogMessage(message);
                    this.SendEmail(subject, message, configParms.EmailFromAddress, configParms.EmailToAddress, "");
                }
                else
                {
                    this.LogMessage("Nothing to do.  Email not sent.");
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

            sb.Append("BHLMETSUpload: METS processing on " + thisComputer + " complete." + endOfLine);
            if (this.filesCreated.Count > 0)
            {
                sb.Append(endOfLine + this.filesCreated.Count.ToString() + " METS files were Created" + endOfLine);
            }
            if (this.filesSkipped.Count > 0)
            {
                sb.Append(endOfLine + this.filesSkipped.Count.ToString() + " METS files were Skipped (403 - forbidden)" + endOfLine);
            }
            if (this.filesUploaded.Count > 0)
            {
                sb.Append(endOfLine + this.filesUploaded.Count.ToString() + " METS files were Uploaded" + endOfLine);
            }
            if (this.errorMessages.Count > 0)
            {
                sb.Append(endOfLine + this.errorMessages.Count.ToString() + " Errors Occurred" + endOfLine + "See the log file for details" + endOfLine + endOfLine);
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
        private void SendEmail(String subject, String message, String fromAddress,
            String toAddress, String ccAddresses)
        {
            try
            {
                EmailClient restClient = null;

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

                restClient = new EmailClient(configParms.BHLWSEndpoint);
                restClient.SendEmail(mailRequest);
            }
            catch (Exception ex)
            {
                log.Error("Email Exception: ", ex);
            }
        }

        #endregion Process results

    }
}
