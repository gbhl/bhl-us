using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net.Mail;
using System.Web;
using MOBOT.IA.Utilities;

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

            BHLWS.BHLWSSoapClient service = new BHLWS.BHLWSSoapClient();

            BHLWS.Book[] items = null;
            try
            {
                //1 - Get new Items
                //2 - check for updated items (including title, pages, authors, etc)
                string startDate = DateTime.Now.AddDays(-1).ToString("MM/dd/yyyy 0:00");
                items = service.BookSelectRecentlyChanged(startDate);
            }
            catch (Exception e)
            {
                this.LogMessage("GetItemsChanged Error: " + e.Message, true);
            }

            if (items != null && items.Length > 0)
            {
                foreach (BHLWS.Book item in items)
                {
                    try
                    {
                        //3 - generate mets file for all new & updated items
                        BHLWS.Title title = service.TitleSelectByTitleID((int)item.PrimaryTitleID);
                        string mods = service.GetMODSRecordForItem(item.BookID);
                        BHLWS.Page[] pages = service.PageMetadataSelectByItemID(item.BookID);

                        StringBuilder sb = new StringBuilder("");
                        sb.AppendFormat("<mets:mets xmlns:mods=\"http://www.loc.gov/mods/v3\" xmlns:rights=\"http://www.loc.gov/rights/\" xmlns:xlink=\"http://www.w3.org/1999/xlink\" xmlns:lc=\"http://www.loc.gov/mets/profiles\" xmlns:mets=\"http://www.loc.gov/METS/\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xsi:schemaLocation=\"http://www.loc.gov/METS_Profile/ http://www.loc.gov/standards/mets/profile_docs/mets.profile.v1-2.xsd http://www.loc.gov/METS/ http://www.loc.gov/standards/mets/mets.xsd http://www.loc.gov/mods/v3 http://www.loc.gov/standards/mods/v3/mods-3-7.xsd\" OBJID=\"item/{0}\" LABEL=\"{1}\" PROFILE=\"lc:printMaterial\">", item.BookID, HttpUtility.HtmlEncode(title.FullTitle));
                        sb.AppendFormat("<mets:metsHdr CREATEDATE=\"{0}\">", DateTime.Now.ToString("yyyy-MM-ddThh:mm:ss"));
                        sb.Append("<mets:agent ROLE=\"CREATOR\" TYPE=\"ORGANIZATION\">");
                        sb.Append("<mets:name>Biodiversity Heritage Library</mets:name>");
                        sb.AppendFormat("<mets:note>mailto:{0}</mets:note>", configParms.MetsEmail);
                        sb.Append("</mets:agent>");
                        sb.Append("</mets:metsHdr>");
                        sb.Append("<mets:dmdSec ID=\"MODS1\">");
                        sb.Append("<mets:mdWrap MDTYPE=\"MODS\">");
                        sb.Append("<mets:xmlData>");
                        sb.Append(mods);
                        sb.Append("</mets:xmlData>");
                        sb.Append("</mets:mdWrap>");
                        sb.Append("</mets:dmdSec>");
                        if (item.CopyrightStatus != null && item.CopyrightStatus.Length > 0)
                        {
                            sb.Append("<mets:amdSec>");
                            sb.Append("<mets:rightsMD ID=\"RIGHTS1\">");
                            sb.Append("<mets:mdWrap MDTYPE=\"OTHER\" LABEL=\"RIGHTSMD\">");
                            sb.Append("<mets:xmlData>");
                            sb.Append("<rights:RightsDeclarationMD RIGHTSCATEGORY=\"COPYRIGHTED\">");
                            sb.AppendFormat("<rights:RightsDeclaration>{0}</rights:RightsDeclaration>", HttpUtility.HtmlEncode(item.CopyrightStatus));
                            sb.Append("</rights:RightsDeclarationMD>");
                            sb.Append("</mets:xmlData>");
                            sb.Append("</mets:mdWrap>");
                            sb.Append("</mets:rightsMD>");
                            sb.Append("</mets:amdSec>");
                        }

                        if (pages != null && pages.Length > 0)
                        {
                            StringBuilder refImages = new StringBuilder("");
                            StringBuilder containerPages = new StringBuilder("");
                            StringBuilder pageTypes = new StringBuilder("");

                            int count = 1;
                            foreach (BHLWS.Page p in pages)
                            {
                                refImages.AppendFormat("<mets:file ID=\"pageImg{0}\" GROUPID=\"G{1}\" USE=\"reference image\" MIMETYPE=\"image/jpeg\">", p.PageID, count);
                                refImages.AppendFormat("<mets:FLocat LOCTYPE=\"URL\" xlink:href=\"https://www.biodiversitylibrary.org/pageimage/{0}\" />", p.PageID);
                                refImages.AppendFormat("</mets:file>");

                                containerPages.AppendFormat("<mets:file ID=\"page{0}\" GROUPID=\"G{1}\" USE=\"container page\" MIMETYPE=\"text/html\">", p.PageID, count);
                                containerPages.AppendFormat("<mets:FLocat LOCTYPE=\"URL\" xlink:href=\"https://www.biodiversitylibrary.org/page/{0}\" />", p.PageID);
                                containerPages.AppendFormat("</mets:file>");

                                string orderLabel = "";
                                if (p.IndicatedPages != null && p.IndicatedPages.Length > 0 && p.IndicatedPages.StartsWith("Page "))
                                {
                                    orderLabel = p.IndicatedPages.Substring(5);
                                    pageTypes.AppendFormat("<mets:div TYPE=\"page\" ORDER=\"{0}\" ORDERLABEL=\"{1}\" LABEL=\"{2}\">", count, HttpUtility.HtmlEncode(orderLabel), (p.IndicatedPages != null && p.IndicatedPages.Length > 0 ? HttpUtility.HtmlEncode(p.IndicatedPages) : HttpUtility.HtmlEncode(p.PageTypes)));
                                }
                                else
                                {
                                    pageTypes.AppendFormat("<mets:div TYPE=\"page\" ORDER=\"{0}\" LABEL=\"{1}\">", count, (p.IndicatedPages != null && p.IndicatedPages.Length > 0 ? HttpUtility.HtmlEncode(p.IndicatedPages) : HttpUtility.HtmlEncode(p.PageTypes)));
                                }
                                pageTypes.AppendFormat("<mets:fptr FILEID=\"pageImg{0}\" />", p.PageID);
                                pageTypes.AppendFormat("<mets:fptr FILEID=\"page{0}\" />", p.PageID);
                                pageTypes.AppendFormat("</mets:div>");

                                count++;
                            }

                            sb.Append("<mets:fileSec>");
                            sb.Append("<mets:fileGrp ID=\"FG1\" USE=\"reference images\">");
                            sb.Append(refImages.ToString());
                            sb.Append("</mets:fileGrp>");
                            sb.Append("<mets:fileGrp ID=\"FG2\" USE=\"container web pages\">");
                            sb.Append(containerPages.ToString());
                            sb.Append("</mets:fileGrp>");
                            sb.Append("</mets:fileSec>");

                            sb.Append("<mets:structMap TYPE=\"mixed\">");
                            if (item.CopyrightStatus != null && item.CopyrightStatus.Length > 0)
                            {
                                sb.Append("<mets:div DMDID=\"MODS1\" ADMID=\"RIGHTS1\" TYPE=\"book\">");
                            }
                            else
                            {
                                sb.Append("<mets:div DMDID=\"MODS1\" TYPE=\"book\">");
                            }
                            sb.Append(pageTypes.ToString());
                            sb.Append("</mets:div>");
                            sb.AppendFormat("</mets:structMap>");
                        }
                        sb.Append("</mets:mets>");

                        File.WriteAllText(configParms.UploadFilePath + item.BookID + ".xml", sb.ToString());

                        filesCreated.Add(item.BookID.ToString());
                        this.LogMessage("METS file created (" + item.BookID+ ")");
                    }
                    catch (Exception ex)
                    {
                        this.LogMessage("Error creating mets file(" + item.BookID + "): " + ex.Message, true);
                    }

                    //4 - upload the mets file to IA
                    UploadFiles(item);
                }
            }

            // Report the results of mets generation
            this.ProcessResults();

            this.LogMessage("METSUpload Processing Complete");
        }

        private void UploadFiles(BHLWS.Book item)
        {
            S3 s3 = null;

            try
            {
                this.LogMessage("Uploading file");
                BHLWS.BHLWSSoapClient wsClient = new BHLWS.BHLWSSoapClient();
                
                int consecutiveErrors = 0;
                s3 = new S3(configParms.IA3AccessKey, configParms.IA3SecretKey);

                try
                {
                    // Upload the file
                    string putResult = s3.PutObject(configParms.UploadFilePath + item.BookID.ToString() + ".xml", item.BarCode,
                        item.BarCode + configParms.IAFileName,
                        "application/xml", null, true, false);

                    // Update log information
                    if (putResult == "Success")
                    {
                        filesUploaded.Add(item.BookID.ToString());
                        this.LogMessage("Mets file uploaded for item " + item.BookID.ToString());
                        File.Delete(configParms.UploadFilePath + item.BookID.ToString() + ".xml");
                    }
                    else if (putResult.ToLower().Contains("403"))   // "Forbidden" error; see details below
                    {
                        filesSkipped.Add(item.BookID.ToString());
                        this.LogMessage("Mets file skipped (forbidden) for item " + item.BookID.ToString());
                    }
                    else
                    {
                        this.LogMessage("Error uploading file for item " + item.BookID.ToString() + ": " + putResult, true);
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
                    LogMessage("Error uploading file for item " + item.BookID + ": " + ex.Message, true);

                    // If we've had 10 consecutive upload failures, then it's time to give up
                    if (consecutiveErrors >= 10)
                    {
                        throw new Exception("Ten consecutive upload errors have occurred");
                    }
                }
            }
            catch (Exception ex)
            {
                LogMessage("Error uploading file (" + item.BookID + "): " + ex.Message, true);
            }
            finally
            {
                if (s3 != null) s3 = null;
                if(File.Exists(configParms.UploadFilePath + item.BookID + ".xml"))
                {
                    File.Delete(configParms.UploadFilePath + item.BookID + ".xml");
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

        #endregion Process results

    }
}
