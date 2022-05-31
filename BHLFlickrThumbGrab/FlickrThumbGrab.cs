using WS = BHL.WebServiceREST.v1;
using BHL.WebServiceREST.v1.Client;
using FreeImageAPI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Xml.Linq;
using BHL.WebServiceREST.v1;

namespace MOBOT.BHL.BHLFlickrThumbGrab
{
    public class FlickrThumbGrab
    {
        // Create a logger for use in this class
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        // NOTE that using System.Reflection.MethodBase.GetCurrentMethod().DeclaringType
        // is equivalent to typeof(LoggingExample) but is more portable
        // i.e. you can copy the code directly into another class without
        // needing to edit the code.

        private ConfigParms configParms = new ConfigParms();
        private List<string> imagesDownloaded = new List<string>();
        private List<string> imagesResized = new List<string>();
        private List<string> errorMessages = new List<string>();

        public void Process()
        {
            this.LogMessage("BHLFlickrThumbGrab Processing Start");

            try
            {
                DateTime startTime = DateTime.Now;

                // Load app settings from the configuration file
                configParms.LoadAppConfig();

                // Download and resize the images
                List<FlickrImageDetail> imageList = this.GetFlickrImageList();
                foreach (FlickrImageDetail image in imageList)
                {
                    this.DownloadFromFlickr(image.PageID, image.PhotoID);
                    this.ResizeImage(image.PageID);
                }
                if (errorMessages.Count == 0) this.WriteImageList(imageList);

                // If something went wrong, then use a default set of images instead
                if (errorMessages.Count > 0) this.WriteDefaultImageList();

                // As long as no errors have occurred, clean up old image files
                if (errorMessages.Count == 0) this.DeleteOldFiles(startTime);
            }
            catch (Exception e)
            {
                this.LogMessage("BHLFlickrThumbGrab Error: " + e.Message, true);
            }

            // Report the results of mets generation
            this.ProcessResults();

            this.LogMessage("BHLFlickrThumbGrab Processing Complete");
        }

        /// <summary>
        /// Get a random list of Flickr images to download
        /// </summary>
        private List<FlickrImageDetail> GetFlickrImageList()
        {
            List<FlickrImageDetail> imageList = new List<FlickrImageDetail>();

            PageFlickrClient restClient = new PageFlickrClient(configParms.BHLWSEndpoint);
            ICollection<WS.PageFlickr> randomImages = restClient.GetPageFlickrRandom(10);

            foreach (WS.PageFlickr image in randomImages)
            {
                FlickrImageDetail imageDetail = new FlickrImageDetail();

                imageDetail.PageID = image.PageID.ToString();
                imageDetail.FlickrURL = image.FlickrURL.Trim();
                imageDetail.ShortTitle = image.ShortTitle;
                imageDetail.IndicatedPages = image.IndicatedPage;
                imageDetail.PageTypes = image.PageType;
                string url = imageDetail.FlickrURL.Substring(0, imageDetail.FlickrURL.Length - 1);
                imageDetail.PhotoID = url.Substring(url.LastIndexOf('/') + 1);

                imageList.Add(imageDetail);
            }

            this.LogMessage("Image List retreived from database");

            return imageList;
        }

        /// <summary>
        /// Download the small (240px) file for the specified flickr photo
        /// </summary>
        /// <param name="photoID"></param>
        private void DownloadFromFlickr(string pageID, string photoID)
        {
            try
            {
                // Call the Flickr API to get the photo metadata
                string flickrApiKey = configParms.FlickrAPIKey;
                string flickrApiPhotoGetInfo = string.Format(configParms.FlickrAPIUrl_photoGetInfo, photoID, flickrApiKey);

                XDocument apiResponse = XDocument.Load(flickrApiPhotoGetInfo);
                XElement photoElement = apiResponse.Element("rsp").Element("photo");
                string farm = photoElement.Attribute("farm").Value;
                string server = photoElement.Attribute("server").Value;
                string secret = photoElement.Attribute("secret").Value;

                // Use the metadata to download and save the image
                string downloadUrl = string.Format(configParms.FlickrDownloadUrl, farm, server, photoID, secret);
                WebClient client = new WebClient();
                client.DownloadFile(downloadUrl, string.Format(configParms.ImageFileName, pageID));

                imagesDownloaded.Add(pageID);

                this.LogMessage("Image downloaded for page " + pageID);
            }
            catch (Exception ex)
            {
                this.LogMessage("Error downloading image for page " + pageID + "): " + ex.Message, true);
            }
        }
        
        /// <summary>
        /// Resize the image for the specified page to be 200 pixels on its longest dimension/
        /// Use the FreeImage library for the resize operation.  It does a better job of perserving
        /// image quality and file sizes than the native .NET graphics libraries.
        /// </summary>
        /// <param name="pageID"></param>
        private void ResizeImage(string pageID)
        {
            FIBITMAP fiBitmap = new FIBITMAP();

            try
            {
                FREE_IMAGE_FORMAT format = FREE_IMAGE_FORMAT.FIF_JPEG;
                fiBitmap = FreeImage.LoadEx(string.Format(configParms.ImageFileName, pageID), ref format);

                if (!fiBitmap.IsNull)
                {
                    int height = 0;
                    int width = 0;
                    uint origHeight = FreeImage.GetHeight(fiBitmap);
                    uint origWidth = FreeImage.GetWidth(fiBitmap);

                    if (origHeight > origWidth)
                    {
                        height = 200;
                        width = (int)Math.Round(((double)height / (double)origHeight) * (double)origWidth);
                    }
                    else
                    {
                        width = 200;
                        height = (int)Math.Round(((double)width / (double)origWidth) * (double)origHeight);
                    }

                    fiBitmap = FreeImage.Rescale(fiBitmap, width, height, FREE_IMAGE_FILTER.FILTER_BILINEAR);

                    FreeImage.Save(FREE_IMAGE_FORMAT.FIF_JPEG, fiBitmap, string.Format(configParms.ImageFileName, pageID),
                        FREE_IMAGE_SAVE_FLAGS.JPEG_QUALITYSUPERB);

                    imagesResized.Add(pageID);

                    this.LogMessage("Image resized for page " + pageID);
                }
                else
                {
                    this.LogMessage("Error loading image for resize operation for page " + pageID + ")", true);
                }
            }
            catch (Exception ex)
            {
                this.LogMessage("Error resizing image for page " + pageID + "): " + ex.Message, true);
            }
            finally
            {
                FreeImage.Unload(fiBitmap);
            }
        }

        /// <summary>
        /// Write the new list of flickr images.
        /// </summary>
        /// <param name="imageList"></param>
        private void WriteImageList(List<FlickrImageDetail> imageList)
        {
            try
            {
                if (imageList.Count > 0)
                {
                    StringBuilder sb = new StringBuilder();

                    foreach (FlickrImageDetail image in imageList)
                    {
                        sb.Append(
                            string.Format(
                                "{0}\t{1}\t{2}\t{3}\t{4}\n",
                                image.PageID,
                                image.FlickrURL,
                                image.ShortTitle,
                                image.IndicatedPages,
                                image.PageTypes));
                    }

                    File.WriteAllText(configParms.ImageListFilePath, sb.ToString());
                    this.LogMessage("Image list written to file");
                }
            }
            catch (Exception ex)
            {
                this.LogMessage("Error writing image list to file: " + ex.Message, true);
            }
        }

        /// <summary>
        /// Set the list of flickr images to a default list by copying the default files to the proper location
        /// </summary>
        private void WriteDefaultImageList()
        {
            try
            {
                string[] images = Directory.GetFiles(configParms.DefaultFilesFolder, "*.jpg");
                foreach (string image in images)
                {
                    File.Copy(image, configParms.ImageFolder + Path.GetFileName(image), true);
                }

                string[] imageList = Directory.GetFiles(configParms.DefaultFilesFolder, "*.txt");
                foreach (string list in imageList)
                {
                    File.Copy(list, configParms.ImageListFilePath, true);
                }

                this.LogMessage("Default image list in use.");
            }
            catch (Exception ex)
            {
                this.LogMessage("Error copying default files: " + ex.Message, true);
            }
        }

        /// <summary>
        /// Remove old image files
        /// </summary>
        private void DeleteOldFiles(DateTime startTime)
        {
            try
            {
                string[] images = Directory.GetFiles(configParms.ImageFolder, "*.jpg");
                foreach (string image in images)
                {
                    DateTime fileDate = File.GetCreationTime(image);
                    if (DateTime.Compare(startTime, fileDate) > 0) File.Delete(image);
                }
            }
            catch (Exception ex)
            {
                this.LogMessage("Error removing old image files: " + ex.Message, true);
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
        /// Examine the results of the image processing and take the appropriate 
        /// actions (log, send email, do nothing).
        /// </summary>
        private void ProcessResults()
        {
            try
            {
                // Send email if an error occurred.
                // Don't send an email each time images are grabbed.
                if (errorMessages.Count > 0)
                {
                    String thisComputer = Environment.MachineName;
                    String subject = "BHLFlickrThumbGrab: Processing on " + thisComputer + " completed with errors.";

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

            sb.Append("BHLFlickrThumbGrab: Processing on " + thisComputer + " complete." + endOfLine);
            if (this.imagesDownloaded.Count > 0)
            {
                sb.Append(endOfLine + this.imagesDownloaded.Count.ToString() + " images were Downloaded" + endOfLine);
            }
            if (this.imagesResized.Count > 0)
            {
                sb.Append(endOfLine + this.imagesResized.Count.ToString() + " images were Resized" + endOfLine);
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

    class FlickrImageDetail
    {
        public string PageID { get; set; }
        public string PhotoID { get; set; }
        public string FlickrURL { get; set; }
        public string ShortTitle { get; set; }
        public string IndicatedPages { get; set; }
        public string PageTypes { get; set; }
    }
}
