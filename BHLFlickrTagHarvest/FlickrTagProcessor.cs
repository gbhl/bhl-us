using FlickrNet;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Threading;
using MOBOT.BHLImport.Server;
using MOBOT.BHLImport.DataObjects;
using MOBOT.BHL.Server;
using MOBOT.BHL.DataObjects;

namespace BHLFlickrTagHarvest
{
    public class FlickrTagProcessor
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private ConfigParms configParms = new ConfigParms();
        private List<string> pagesProcessed = new List<string>();
        private List<(string PageID, string PhotoID)> photosNotFound = new List<(string PageID, string PhotoID)>();
        private List<string> tagsAdded = new List<string>();
        private List<string> tagsUpdated = new List<string>();
        private List<string> tagsRemoved = new List<string>();
        private List<string> notesAdded = new List<string>();
        private List<string> notesUpdated = new List<string>();
        private List<string> notesRemoved = new List<string>();
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

            // Read the pages with flickr images and get the flickr tags and notes for each
            this.HarvestData();

            // Report the results of item/page processing
            this.ProcessResults();

            this.LogMessage("BHLFlickrTagHarvest Processing Complete");
        }

        private void HarvestData()
        {
            try
            {
                List<PageFlickr> pages = new BHLProvider().PageFlickrSelectAll();
                LogMessage(string.Format("Found {0} pages to process", pages.Count));
                int numRequests = 0;
                DateTime dtStart = DateTime.Now;
                foreach (PageFlickr page in pages)
                {
                    HarvestPage(page);

                    // Make sure we harvest no more than 3600 pages per hour, so that we do not exceed Flickr's API rate limits
                    numRequests++;
                    if (numRequests >= 3600)
                    {
                        TimeSpan ts = DateTime.Now - dtStart;
                        if (ts.TotalSeconds < 3600) Thread.Sleep((int)((3600 - ts.TotalSeconds) * 1000));
                        dtStart = DateTime.Now;
                        numRequests = 0;
                    }
                }
            }
            catch(Exception ex)
            {
                LogMessage("Error harvesting tags", ex);
            }

        }

        private void HarvestPage(PageFlickr page)
        {
            BHLImportProvider bhlImportService = new BHLImportProvider();
            string photoID = string.Empty;

            try
            {
                // Get the tags and notes from Flickr for the current page
                string[] flickrUrlParts = page.FlickrURL.Split('/');
                photoID = flickrUrlParts[5];
                PhotoInfo photoInfo = this.GetFlickrPhotoInfo(photoID);
                List<PageFlickrTag> flickrTags = this.GetFlickrTags(page.PageID, photoID, photoInfo);
                List<PageFlickrNote> flickrNotes = this.GetFlickrNotes(page.PageID, photoID, photoInfo);

                // Get the tags and notes from the database for the current page
                List<PageFlickrTag> bhlTags = bhlImportService.PageFlickrTagSelectForPageID(page.PageID);
                List<PageFlickrNote> bhlNotes = bhlImportService.PageFlickrNoteSelectForPageID(page.PageID);

                // Merge the tags and notes in the database with the new lists from Flickr
                List<PageFlickrTag> updateTags = this.CompareTags(bhlTags, flickrTags);
                List<PageFlickrNote> updateNotes = this.CompareNotes(bhlNotes, flickrNotes);

                // Update the database with the new sets of tags and notes for the page
                bhlImportService.PageFlickrTagUpdateForPageID(page.PageID, updateTags.ToArray());
                bhlImportService.PageFlickrNoteUpdateForPageID(page.PageID, updateNotes.ToArray());
            }
            catch (FlickrApiException fex)
            {
                if (fex.Code == 1)  // Photo not found
                {
                    photosNotFound.Add((page.PageID.ToString(), photoID));
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

        /// <summary>
        /// Get the metadata for a specific photo from Flickr
        /// </summary>
        /// <param name="photoID"></param>
        /// <returns></returns>
        private PhotoInfo GetFlickrPhotoInfo(string photoID)
        {
            Flickr.CacheDisabled = true;
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

            return photoInfo;
        }

        /// <summary>
        /// Get the tags assigned to the specified photo at Flickr
        /// </summary>
        /// <param name="pageID"></param>
        /// <param name="photoID"></param>
        /// <param name="photoInfo"></param>
        /// <returns></returns>
        private List<PageFlickrTag> GetFlickrTags(int pageID, string photoID, PhotoInfo photoInfo)
        {
            List<PageFlickrTag> flickrTags = new List<PageFlickrTag>();

            foreach (PhotoInfoTag tag in photoInfo.Tags)
            {
                // Ignore "bhl:page" and "dc:identifier" tags entered by the BioDivLibrary user.
                // Keep all other tags, no matter to added them.
                if (tag.AuthorId == configParms.BHLFlickrUserID &&
                    (tag.Raw.StartsWith("bhl:page") || tag.Raw.StartsWith("dc:identifier")))
                {
                    // skip this tag
                }
                else
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
        /// Get the notes assigned to the specified photo at Flickr
        /// </summary>
        /// <param name="pageID"></param>
        /// <param name="photoID"></param>
        /// <param name="photoInfo"></param>
        /// <returns></returns>
        private List<PageFlickrNote> GetFlickrNotes(int pageID, string photoID, PhotoInfo photoInfo)
        {
            List<PageFlickrNote> flickrNotes = new List<PageFlickrNote>();

            foreach (PhotoInfoNote note in photoInfo.Notes)
            {
                // Include all notes, including those entered by the BioDivLibrary user.
                // There are no auto-added notes from BioDivLibrary, so no reason to
                // exclude anything.
                PageFlickrNote flickrNote = new PageFlickrNote();

                flickrNote.PageID = pageID;
                flickrNote.PhotoID = photoID;
                flickrNote.FlickrNoteID = note.NoteId;
                flickrNote.FlickrAuthorID = note.AuthorId;
                flickrNote.FlickrAuthorName = note.AuthorName;
                flickrNote.FlickrAuthorRealName = note.AuthorRealName;
                flickrNote.AuthorIsPro = (short)((note.AuthorIsPro ?? false) ? 1 : 0);
                flickrNote.XCoord = note.XPosition;
                flickrNote.YCoord = note.YPosition;
                flickrNote.Width = note.Width;
                flickrNote.Height = note.Height;
                flickrNote.NoteValue = note.NoteText;

                flickrNotes.Add(flickrNote);
            }

            return flickrNotes;
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
            List<PageFlickrTag> updateTags = new List<PageFlickrTag>();
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
                        // Tag found; if necessary, update it
                        inBhl = true;
                        if (bhlTag.IsActive == 0 || bhlTag.DeleteDate != null)
                        {
                            bhlTag.IsActive = 1;
                            bhlTag.LastModifiedDate = updateDate;
                            bhlTag.DeleteDate = null;
                            updateTags.Add(bhlTag);
                            tagsUpdated.Add(bhlTag.TagValue);
                        }
                        break;
                    }
                }

                if (!inBhl)
                {
                    // Tag not found in BHL; add it
                    PageFlickrTag newBhlTag = flickrTag;
                    newBhlTag.IsActive = 1;
                    newBhlTag.CreationDate = updateDate;
                    newBhlTag.LastModifiedDate = updateDate;
                    updateTags.Add(newBhlTag);
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

                // Tag not found in Flickr; mark it deleted (if not already)
                if (!inFlickr && (bhlTag.IsActive == 1 || bhlTag.DeleteDate == null))
                {
                    bhlTag.IsActive = 0;
                    bhlTag.LastModifiedDate = updateDate;
                    bhlTag.DeleteDate = updateDate;
                    updateTags.Add(bhlTag);
                    tagsRemoved.Add(bhlTag.TagValue);
                }
            }

            // Return the updated list of tags in BHL
            return updateTags;
        }

        /// <summary>
        /// Compare the list of notes in Flickr with the list in notes in BHL, and update the list in BHL
        /// to match the list in Flickr.
        /// </summary>
        /// <param name="bhlNotes"></param>
        /// <param name="flickrNotes"></param>
        /// <returns></returns>
        private List<PageFlickrNote> CompareNotes(List<PageFlickrNote> bhlNotes, List<PageFlickrNote> flickrNotes)
        {
            List<PageFlickrNote> updateNotes = new List<PageFlickrNote>();
            DateTime updateDate = DateTime.Now;

            // Add and update tags
            foreach (PageFlickrNote flickrNote in flickrNotes)
            {
                bool inBhl = false;
                foreach (PageFlickrNote bhlNote in bhlNotes)
                {
                    // Look for the flickr note in the list of notes in bhl
                    if (flickrNote.FlickrNoteID == bhlNote.FlickrNoteID)
                    {
                        // Tag found; if necessary, update it
                        inBhl = true;
                        if (bhlNote.NoteValue != flickrNote.NoteValue ||
                            bhlNote.XCoord != flickrNote.XCoord ||
                            bhlNote.YCoord != flickrNote.YCoord ||
                            bhlNote.Width != flickrNote.Width ||
                            bhlNote.Height != flickrNote.Height ||
                            bhlNote.AuthorIsPro != flickrNote.AuthorIsPro ||
                            bhlNote.IsActive != 1 ||
                            bhlNote.DeleteDate != null)
                        {
                            bhlNote.NoteValue = flickrNote.NoteValue;
                            bhlNote.XCoord = flickrNote.XCoord;
                            bhlNote.YCoord = flickrNote.YCoord;
                            bhlNote.Width = flickrNote.Width;
                            bhlNote.Height = flickrNote.Height;
                            bhlNote.AuthorIsPro = flickrNote.AuthorIsPro;
                            bhlNote.IsActive = 1;
                            bhlNote.LastModifiedDate = updateDate;
                            bhlNote.DeleteDate = null;
                            updateNotes.Add(bhlNote);
                            notesUpdated.Add(bhlNote.NoteValue);
                        }
                        break;
                    }
                }

                if (!inBhl)
                {
                    // Tag not found in BHL; add it
                    PageFlickrNote newBhlNote = flickrNote;
                    newBhlNote.IsActive = 1;
                    newBhlNote.CreationDate = updateDate;
                    newBhlNote.LastModifiedDate = updateDate;
                    updateNotes.Add(newBhlNote);
                    notesAdded.Add(newBhlNote.NoteValue);
                }
            }

            // Delete tags
            foreach (PageFlickrNote bhlNote in bhlNotes)
            {
                bool inFlickr = false;
                foreach (PageFlickrNote flickrNote in flickrNotes)
                {
                    // Look for the bhl tag in the list of tags in Flickr
                    if (flickrNote.FlickrNoteID == bhlNote.FlickrNoteID)
                    {
                        inFlickr = true;
                        break;
                    }
                }

                // Note not found in Flickr; mark it deleted (if not already)
                if (!inFlickr && (bhlNote.IsActive == 1 || bhlNote.DeleteDate == null))
                {
                    bhlNote.IsActive = 0;
                    bhlNote.LastModifiedDate = updateDate;
                    bhlNote.DeleteDate = updateDate;
                    updateNotes.Add(bhlNote);
                    notesRemoved.Add(bhlNote.NoteValue);
                }
            }

            // Return the updated list of notes in BHL
            return updateNotes;
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
                    notesAdded.Count > 0 || notesUpdated.Count > 0 ||
                    notesRemoved.Count > 0 || photosNotFound.Count > 0 || 
                    errorMessages.Count > 0)
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
            if (this.notesAdded.Count > 0)
            {
                sb.Append(endOfLine + "Added " + this.notesAdded.Count.ToString() + " Notes" + endOfLine);
            }
            if (this.notesUpdated.Count > 0)
            {
                sb.Append(endOfLine + "Updated " + this.notesUpdated.Count.ToString() + " Notes" + endOfLine);
            }
            if (this.notesRemoved.Count > 0)
            {
                sb.Append(endOfLine + "Removed " + this.notesRemoved.Count.ToString() + " Notes" + endOfLine);
            }
            if (this.photosNotFound.Count > 0)
            {
                sb.Append(endOfLine + this.photosNotFound.Count.ToString() + " Photos not found at Flickr" + endOfLine);
                foreach((string PageID, string PhotoID) photo in photosNotFound)
                {
                    sb.Append(endOfLine + "Flickr Photo ID " + photo.PhotoID + " (associated with BHL Page ID " + photo.PageID + ") not found" + endOfLine);
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
