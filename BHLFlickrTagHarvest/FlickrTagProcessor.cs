using FlickrNet;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using BHLFlickrTagHarvest.BHLWS;
using BHLFlickrTagHarvest.BHLImportWS;

namespace BHLFlickrTagHarvest
{
    public class FlickrTagProcessor
    {
        static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private ConfigParms configParms = new ConfigParms();
        private List<string> pagesProcessed = new List<string>();
        private List<string> photosNotFound = new List<string>();
        private List<string> tagsAdded = new List<string>();
        private List<string> tagsUpdated = new List<string>();
        private List<string> tagsRemoved = new List<string>();
        private List<string> errorMessages = new List<string>();

        public void Process()
        {
            this.LogMessage("BHLFlickrTagHarvest Processing Started");

            // Load the app settings from the configuration file
            configParms.LoadAppConfig();

            // Read additional app settings from the command line
            // Note: Command line arguments override configuration file settings
            if (!this.ReadCommandLineArguments()) return;

            // validate config values
            if (!this.ValidateConfiguration()) return;

            // Read the pages with flickr images and get the flickr tags for each
            this.HarvestTags();

            // Report the results of item/page processing
            this.ProcessResults();

            this.LogMessage("BHLFlickrTagHarvest Processing Complete");
        }

        private void HarvestTags()
        {
            try
            {
                BHLWSSoapClient bhlWSClient = new BHLWSSoapClient();

                BHLImportWSSoapClient bhlImportWSClient = new BHLImportWSSoapClient();

                PageFlickr[] pages = bhlWSClient.PageFlickrSelectAll();
                LogMessage(string.Format("Found {0} pages to process", pages.Length));
                foreach (PageFlickr page in pages)
                {
                    string photoID = string.Empty;

                    try
                    {
                        // Get the tags from the database for the current page
                        PageFlickrTag[] bhlTags = bhlImportWSClient.PageFlickrTagSelectForPageID(page.PageID);

                        // Get the tags from Flickr for the current page
                        string[] flickrUrlParts = page.FlickrURL.Split('/');
                        photoID = flickrUrlParts[5];
                        List<PageFlickrTag> flickrTags = this.GetFlickrTags(page.PageID, photoID);

                        // Merge the tags in the database with the new list of tags from Flickr
                        List<PageFlickrTag> updateTags = this.CompareTags(new List<PageFlickrTag>(bhlTags), flickrTags);

                        // Update the database with the new set of tags for the page
                        bhlImportWSClient.PageFlickrTagUpdateForPageID(page.PageID, updateTags.ToArray());
                    }
                    catch (FlickrApiException fex)
                    {
                        if (fex.Code == 1)  // Photo not found
                        {
                            photosNotFound.Add(photoID);
                        }
                        else
                        {
                            LogMessage(string.Format("Flickr error processing page {0}", page.PageID.ToString()), fex);
                        }
                    }
                    catch (Exception ex)
                    {
                        LogMessage(string.Format("Flickr error processing page {0}", page.PageID.ToString()), ex);
                    }

                    pagesProcessed.Add(page.PageID.ToString());

                    if ((pagesProcessed.Count % 500) == 0) LogMessage(string.Format("Completed {0} pages", pagesProcessed.Count));
                }
            }
            catch(Exception ex)
            {
                LogMessage("Error harvesting tags", ex);
            }

        }

        /// <summary>
        /// Get the tags assigned to the specified photo at Flickr
        /// </summary>
        /// <param name="pageID"></param>
        /// <param name="photoID"></param>
        /// <returns></returns>
        private List<PageFlickrTag> GetFlickrTags(int pageID, string photoID)
        {
            List<PageFlickrTag> flickrTags = new List<PageFlickrTag>();
            Flickr flickr = new Flickr();
            flickr.ApiKey = configParms.FlickrApiKey;
            PhotoInfo photoInfo = null;

            // Try getting info up to three times per photo before giving up
            int retry = 1;
            while (retry <= 3)
            {
                try
                {
                    photoInfo = flickr.PhotosGetInfo(photoID);
                    break;
                }
                catch (FlickrApiException fex)
                {
                    if (fex.Code == 1)  // Photo not found
                    {
                        throw;
                    }
                    else
                    {
                        retry++;
                        if (retry > 3) throw;
                    }
                }
                catch
                {
                    retry++;
                    if (retry > 3) throw;
                }
            }

            foreach (PhotoInfoTag tag in photoInfo.Tags)
            {
                // Only include tags not entered by BioDivLibrary user
                if (tag.AuthorId != configParms.BHLFlickrUserID)
                {
                    PageFlickrTag flickrTag = new PageFlickrTag();

                    flickrTag.PageID = pageID;
                    flickrTag.PhotoID = photoID;
                    flickrTag.IsMachineTag = (short)(tag.IsMachineTag ? 1 : 0);
                    flickrTag.TagValue = tag.Raw;
                    flickrTag.FlickrAuthorID = tag.AuthorId;
                    flickrTag.FlickrAuthorName = tag.AuthorName;

                    flickrTags.Add(flickrTag);
                }
            }

            return flickrTags;
        }

        /// <summary>
        /// Compare the list of tags in Flickr with the list in tags in BHL, and update the list in BHL
        /// to match the list in Flickr.
        /// </summary>
        /// <param name="bhlTags"></param>
        /// <param name="flickrTags"></param>
        /// <returns></returns>
        private List<PageFlickrTag> CompareTags(List<PageFlickrTag> bhlTags, List<PageFlickrTag> flickrTags)
        {
            DateTime updateDate = DateTime.Now;

            // Add and update tags
            foreach(PageFlickrTag flickrTag in flickrTags)
            {
                bool inBhl = false;
                foreach(PageFlickrTag bhlTag in bhlTags)
                {
                    // Look for the flickr tag in the list of tags in bhl
                    if (flickrTag.PhotoID == bhlTag.PhotoID &&
                        flickrTag.TagValue == bhlTag.TagValue &&
                        flickrTag.FlickrAuthorID == bhlTag.FlickrAuthorID)
                    {
                        // Tag found; update it
                        inBhl = true;
                        bhlTag.IsActive = 1;
                        bhlTag.LastModifiedDate = updateDate;
                        bhlTag.DeleteDate = null;
                        tagsUpdated.Add(bhlTag.TagValue);
                    }
                }

                if (!inBhl)
                {
                    // Tag not found in BHL; add it
                    PageFlickrTag newBhlTag = flickrTag;
                    newBhlTag.IsActive = 1;
                    newBhlTag.CreationDate = updateDate;
                    newBhlTag.LastModifiedDate = updateDate;
                    bhlTags.Add(newBhlTag);
                    tagsAdded.Add(newBhlTag.TagValue);
                }
            }

            // Delete tags
            foreach(PageFlickrTag bhlTag in bhlTags)
            {
                bool inFlickr = false;
                foreach(PageFlickrTag flickrTag in flickrTags)
                {
                    // Look for the bhl tag in the list of tags in Flickr
                    if (flickrTag.PhotoID == bhlTag.PhotoID &&
                        flickrTag.TagValue == bhlTag.TagValue &&
                        flickrTag.FlickrAuthorID == bhlTag.FlickrAuthorID)
                    {
                        inFlickr = true;
                        break;
                    }
                }

                if (!inFlickr)
                {
                    // Tag not found in Flickr; mark it deleted
                    bhlTag.IsActive = 0;
                    bhlTag.LastModifiedDate = updateDate;
                    bhlTag.DeleteDate = updateDate;
                    tagsRemoved.Add(bhlTag.TagValue);
                }
            }

            // Return the updated list of tags in BHL
            return bhlTags;
        }

        #region Process Results

        /// <summary>
        /// Examine the results of the process and take the appropriate 
        /// actions (log, send email, do nothing).
        /// </summary>
        private void ProcessResults()
        {
            try
            {
                // send email with process results to Exchange group
                if (pagesProcessed.Count > 0 || tagsAdded.Count > 0 ||
                    tagsUpdated.Count > 0 || tagsRemoved.Count > 0 || 
                    photosNotFound.Count > 0 || errorMessages.Count > 0)
                {
                    this.LogMessage("Sending Email....");
                    string message = this.GetEmailBody();
                    this.LogMessage(message);
                    this.SendEmail(message);
                }
                else
                {
                    this.LogMessage("No pages processed.  Email not sent.");
                }
            }
            catch (Exception ex)
            {
                this.LogMessage("Exception sending email.", ex);
                return;
            }
        }

        #endregion Process Results

        #region Utility methods

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
        /// Verify that the config file and command line arguments are valid
        /// </summary>
        /// <returns>True if arguments valid, false otherwise</returns>
        private bool ValidateConfiguration()
        {
            return true;
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

            sb.Append("BHLFlickrTagHarvest: Flickr Harvesting on " + thisComputer + " complete." + endOfLine);
            if (this.pagesProcessed.Count > 0)
            {
                sb.Append(endOfLine + "Processed " + this.pagesProcessed.Count.ToString() + " Pages" + endOfLine);
            }
            if (this.tagsAdded.Count > 0)
            {
                sb.Append(endOfLine + "Added " + this.tagsAdded.Count.ToString() + " Tags" + endOfLine);
            }
            if (this.tagsUpdated.Count > 0)
            {
                sb.Append(endOfLine + "Updated " + this.tagsUpdated.Count.ToString() + " Tags" + endOfLine);
            }
            if (this.tagsRemoved.Count > 0)
            {
                sb.Append(endOfLine + "Removed " + this.tagsRemoved.Count.ToString() + " Tags" + endOfLine);
            }
            if (this.photosNotFound.Count > 0)
            {
                sb.Append(endOfLine + this.photosNotFound.Count.ToString() + " Photos not found at Flickr" + endOfLine);
                foreach(string pageID in photosNotFound)
                {
                    sb.Append(endOfLine + "Flickr Photo ID " + pageID + " not found" + endOfLine);
                }
            }
            if (this.errorMessages.Count > 0)
            {
                sb.Append(endOfLine + this.errorMessages.Count.ToString() + " Errors Occurred" + endOfLine + "See the log file for details" + endOfLine);
                foreach (string message in errorMessages)
                {
                    sb.Append(endOfLine + message + endOfLine);
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
                MailMessage mailMessage = new MailMessage(configParms.EmailFromAddress, configParms.EmailToAddress);
                if (this.errorMessages.Count == 0)
                {
                    mailMessage.Subject = "BHLFlickrTagHarvest: Flickr Harvesting on " + thisComputer + " completed successfully.";
                }
                else
                {
                    mailMessage.Subject = "BHLFlickrTagHarvest: Flickr Harvesting on " + thisComputer + " completed with errors.";
                }
                mailMessage.Body = message;

                SmtpClient smtpClient = new SmtpClient(configParms.SMTPHost);
                smtpClient.Send(mailMessage);
            }
            catch (Exception ex)
            {
                LogMessage("Email Exception: ", ex);
            }
        }

        private void LogMessage(string message)
        {
            // logger automatically adds date/time
            if (log.IsInfoEnabled) log.Info(message);
            Console.Write(message + "\r\n");
        }

        private void LogMessage(string message, Exception ex)
        {
            log.Error(message, ex);
            Console.Write(string.Format("{0}: {1}\r\n", message, ex.Message));
        }

        #endregion Utility methods

    }
}
