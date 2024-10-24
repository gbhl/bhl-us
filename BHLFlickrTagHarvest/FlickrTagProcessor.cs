﻿using BHL.WebServiceREST.v1;
using BHL.WebServiceREST.v1.Client;
using FlickrNet;
using MOBOT.BHL.Server;
using MOBOT.BHLImport.DataObjects;
using MOBOT.BHLImport.Server;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace BHLFlickrTagHarvest
{
    public class FlickrTagProcessor
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private ConfigParms configParms = new();
        private List<string> pagesProcessed = new();
        private List<(string PageID, string PhotoID)> photosNotFound = new();
        private List<string> tagsAdded = new();
        private List<string> tagsUpdated = new();
        private List<string> tagsRemoved = new();
        private List<string> notesAdded = new();
        private List<string> notesUpdated = new();
        private List<string> notesRemoved = new();
        private List<string> errorMessages = new();

        public void Process()
        {
            LogMessage("BHLFlickrTagHarvest Processing Started");

            // Load the app settings from the configuration file
            configParms.LoadAppConfig();

            // Read additional app settings from the command line
            // Note: Command line arguments override configuration file settings
            if (!ReadCommandLineArguments()) return;

            // validate config values
            if (!ValidateConfiguration()) return;

            // Read the pages with flickr images and get the flickr tags and notes for each
            this.HarvestData();

            // Report the results of item/page processing
            this.ProcessResults();

            LogMessage("BHLFlickrTagHarvest Processing Complete");
        }

        private void HarvestData()
        {
            try
            {
                List<MOBOT.BHL.DataObjects.PageFlickr> pages = new BHLProvider().PageFlickrSelectAll();
                LogMessage(string.Format("Found {0} pages to process", pages.Count));
                int numRequests = 0;
                DateTime dtStart = DateTime.Now;
                foreach (MOBOT.BHL.DataObjects.PageFlickr page in pages)
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

        private void HarvestPage(MOBOT.BHL.DataObjects.PageFlickr page)
        {
            BHLImportProvider bhlImportService = new();
            string photoID = string.Empty;

            try
            {
                // Get the tags and notes from Flickr for the current page
                string[] flickrUrlParts = page.FlickrURL.Split('/');
                photoID = flickrUrlParts[5];
                PhotoInfo photoInfo = this.GetFlickrPhotoInfo(photoID);
                List<PageFlickrTag> flickrTags = this.GetFlickrTags(page.PageID, photoID, photoInfo);
                List<PageFlickrNote> flickrNotes = GetFlickrNotes(page.PageID, photoID, photoInfo);

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
            Flickr flickr = new()
            {
                ApiKey = configParms.FlickrApiKey
            };
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
            List<PageFlickrTag> flickrTags = new();

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
                    PageFlickrTag flickrTag = new()
                    {
                        PageID = pageID,
                        PhotoID = photoID,
                        IsMachineTag = (short)(tag.IsMachineTag ? 1 : 0),
                        TagValue = tag.Raw,
                        FlickrAuthorID = tag.AuthorId,
                        FlickrAuthorName = tag.AuthorName
                    };

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
        private static List<PageFlickrNote> GetFlickrNotes(int pageID, string photoID, PhotoInfo photoInfo)
        {
            List<PageFlickrNote> flickrNotes = new();

            foreach (PhotoInfoNote note in photoInfo.Notes)
            {
                // Include all notes, including those entered by the BioDivLibrary user.
                // There are no auto-added notes from BioDivLibrary, so no reason to
                // exclude anything.
                PageFlickrNote flickrNote = new()
                {
                    PageID = pageID,
                    PhotoID = photoID,
                    FlickrNoteID = note.NoteId,
                    FlickrAuthorID = note.AuthorId,
                    FlickrAuthorName = note.AuthorName,
                    FlickrAuthorRealName = note.AuthorRealName,
                    AuthorIsPro = (short)((note.AuthorIsPro ?? false) ? 1 : 0),
                    XCoord = note.XPosition,
                    YCoord = note.YPosition,
                    Width = note.Width,
                    Height = note.Height,
                    NoteValue = note.NoteText
                };

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
            List<PageFlickrTag> updateTags = new();
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
            List<PageFlickrNote> updateNotes = new();
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
                string message;
                string serviceName = "BHLFlickrTagHarvest";
                if (pagesProcessed.Count > 0 || tagsAdded.Count > 0 ||
                    tagsUpdated.Count > 0 || tagsRemoved.Count > 0 || 
                    notesAdded.Count > 0 || notesUpdated.Count > 0 ||
                    notesRemoved.Count > 0 || photosNotFound.Count > 0 || 
                    errorMessages.Count > 0)
                {
                    LogMessage("Sending Email....");
                    message = this.GetEmailBody();
                    LogMessage(message);
                    this.SendServiceLog(serviceName, message);
                    this.SendEmail(serviceName, message);
                }
                else
                {
                    message = "No pages processed";
                    LogMessage(message);
                    this.SendServiceLog(serviceName, message);
                }
            }
            catch (Exception ex)
            {
                LogMessage("Exception processing results.", ex);
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
        private static bool ReadCommandLineArguments()
        {
            return true;
        }

        /// <summary>
        /// Verify that the config file and command line arguments are valid
        /// </summary>
        /// <returns>True if arguments valid, false otherwise</returns>
        private static bool ValidateConfiguration()
        {
            return true;
        }

        /// <summary>
        /// Constructs the body of an email message to be sent
        /// </summary>
        /// <returns>Body of email message to be sent</returns>
        private string GetEmailBody()
        {
            StringBuilder sb = new();
            const string endOfLine = "\r\n";

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
                foreach((string PageID, string PhotoID) in photosNotFound)
                {
                    sb.Append(endOfLine + "Flickr Photo ID " + PhotoID + " (associated with BHL Page ID " + PageID + ") not found" + endOfLine);
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
        /// <param name="serviceName">Name of service</param>
        /// <param name="message">Body of the message to be sent</param>
        private void SendEmail(string serviceName, string message)
        {
            try
            {
                if (this.errorMessages.Count > 0 && configParms.EmailOnError)
                {
                    MailRequestModel mailRequest = new()
                    {
                        Subject = string.Format(
                        "{0}: Flickr Harvesting on {1} completed {2} ",
                        serviceName,
                        Environment.MachineName,
                        (errorMessages.Count == 0 ? "successfully" : "with errors")),
                        Body = message,
                        From = configParms.EmailFromAddress
                    };

                    List<string> recipients = new();
                    foreach (string recipient in configParms.EmailToAddress.Split(',')) recipients.Add(recipient);
                    mailRequest.To = recipients;

                    EmailClient restClient = new(configParms.BHLWSEndpoint);
                    restClient.SendEmail(mailRequest);
                }
            }
            catch (Exception ex)
            {
                LogMessage("Email Exception: ", ex);
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
                serviceLog.Severityname = (errorMessages.Count > 0 ? "Error" : 
                    (photosNotFound.Count > 0 ? "Warning" :  "Information"));
                serviceLog.Message = string.Format("Processing on {0} completed.\n\r{1}", Environment.MachineName, message);

                ServiceLogsClient restClient = new ServiceLogsClient(configParms.BHLWSEndpoint);
                restClient.InsertServiceLog(serviceLog);
            }
            catch (Exception ex)
            {
                log.Error("Service Log Exception: ", ex);
            }
        }

        private static void LogMessage(string message)
        {
            // logger automatically adds date/time
            if (log.IsInfoEnabled) log.Info(message);
            Console.Write(message + "\r\n");
        }

        private static void LogMessage(string message, Exception ex)
        {
            log.Error(message, ex);
            Console.Write(string.Format("{0}: {1}\r\n", message, ex.Message));
        }

        #endregion Utility methods

    }
}
