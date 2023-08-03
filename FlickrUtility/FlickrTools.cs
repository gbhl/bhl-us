using System;
using System.Net;
using System.IO;
using System.Configuration;
using System.Windows.Media.Imaging;
using System.Text;

using FlickrNet;
using ImageResizer;

namespace FlickrUtility
{
    public class FlickrTools
    {
        public static bool OAuthAccessTokenValid(string oAuthAccessToken,
            string oAuthAccessTokenSecret)
        {
            bool isValid = false;

            try
            {
                string flickrUserId = ConfigurationManager.AppSettings["FlickrUserId"];
                string flickrUserError = ConfigurationManager.AppSettings["FlickrUserError"];

                Flickr flickr = GetFlickrInstance();
                flickr.OAuthAccessToken = oAuthAccessToken;
                flickr.OAuthAccessTokenSecret = oAuthAccessTokenSecret;

                FoundUser flickrUser = flickr.TestLogin();
                if (flickrUserId.Length > 0 && flickrUser.UserId != flickrUserId)
                {
                    isValid = false;
                }
                else
                {
                    isValid = true;
                }
                
            }
            catch
            {
            }

            return isValid;
        }

        public static void OAuthRequestToken(string redirectUrl, out string oAuthToken, 
            out string oAuthTokenSecret, out string oAuthUrl)
        {
            Flickr flickr = GetFlickrInstance();

            OAuthRequestToken oAuthRequestToken = flickr.OAuthGetRequestToken(redirectUrl);
            oAuthUrl = flickr.OAuthCalculateAuthorizationUrl(oAuthRequestToken.Token, AuthLevel.Write);

            oAuthToken = oAuthRequestToken.Token;
            oAuthTokenSecret = oAuthRequestToken.TokenSecret;
        }

        public static void OAuthAccessToken(string oAuthToken, string oAuthTokenSecret,
            string oAuthVerifier, out string oAuthAccessToken, out string oAuthAccessTokenSecret)
        {
            Flickr flickr = GetFlickrInstance();
            OAuthAccessToken oAuthAccess = flickr.OAuthGetAccessToken(
                oAuthToken, oAuthTokenSecret, oAuthVerifier);
            oAuthAccessToken = oAuthAccess.Token;
            oAuthAccessTokenSecret = oAuthAccess.TokenSecret;

            string flickrUserId = ConfigurationManager.AppSettings["FlickrUserId"];
            string flickrUserError = ConfigurationManager.AppSettings["FlickrUserError"];
            if (flickrUserId.Length > 0)
            {
                if (oAuthAccess.UserId != flickrUserId)
                {
                    throw new Exception(flickrUserError);
                }
            }
        }

        public static string UploadImageToFlickr(string inputJPGUrl, string creator, string description,
            string[] subjects, string creatorWorkURL, string oAuthAccessToken, string oAuthAccessTokenSecret,
            string fileName, string photoTag, double rotation)
        {
            var webClient = new WebClient();

            using (MemoryStream inStream = new MemoryStream(webClient.DownloadData(inputJPGUrl)),
                    outStream = new MemoryStream())
            {
                //if (rotation > 0)
                //{
                // optional - rotate image
                var settings = new ResizeSettings
                {
                    Rotate = rotation,
                    Format = "jpg",
                    Quality = int.Parse(ConfigurationManager.AppSettings["FlickrImageQuality"]),

                    MaxHeight = int.Parse(ConfigurationManager.AppSettings["FlickrMaxHeight"]),
                    MaxWidth = int.Parse(ConfigurationManager.AppSettings["FlickrMaxWidth"])
                };

                // rotating image
                ImageBuilder.Current.Build(inStream, outStream, settings);                
                //}

                // add metadata to image file
                MemoryStream finalStream = new MemoryStream();
                finalStream = AddMetadataToImage(outStream, ConfigurationManager.AppSettings["FlickrImageCredit"], 
                    ConfigurationManager.AppSettings["FlickrImageSource"],
                    creator, description, subjects, ConfigurationManager.AppSettings["FlickrImageRights"], creatorWorkURL);

                // send to Flickr
                Flickr flickr = GetFlickrInstance();

                flickr.OAuthAccessToken = oAuthAccessToken;
                flickr.OAuthAccessTokenSecret = oAuthAccessTokenSecret;

                finalStream.Seek(0, SeekOrigin.Begin);

                string photoID = flickr.UploadPicture(finalStream,
                        fileName, // fileName
                        fileName, // title
                        "", // description
                        "", // tags already on photo "Animal behavior, Hunting, University of California Libraries",     // tags
                        true,  //  isPublic 
                        false,  // isFamily
                        false,  // isFriend
                        ContentType.Photo,
                        SafetyLevel.Safe,
                        HiddenFromSearch.Visible);

                flickr.PhotosAddTags(photoID, photoTag);

                PhotoInfo photoInfo = flickr.PhotosGetInfo(photoID);
                return photoInfo.WebUrl;
            }     
        }

            // Rotates the input image by theta degrees around center.
        private static MemoryStream AddMetadataToImage(MemoryStream inStream, 
            string credit,
            string source,
            string creator,
            string description,
            string[] subjects,
            string rights,
            string creatorWorkURL)
        {

            BitmapMetadata metadata = new BitmapMetadata("jpg");

            //int paddingAmount = 8192;
            //metadata.SetQuery(@"/app1/ifd/PaddingSchema:Padding", paddingAmount);
            //metadata.SetQuery(@"/app1/ifd/exif/PaddingSchema:Padding", paddingAmount);
            //metadata.SetQuery(@"/xmp/PaddingSchema:Padding", paddingAmount);

            metadata.SetQuery("/xmp", new BitmapMetadata("xmp"));

            metadata.SetQuery("/xmp/dc:creator", new BitmapMetadata("xmpseq"));
            metadata.SetQuery(@"/xmp/dc:creator/{ushort=0}", creator);

            metadata.SetQuery("/xmp/dc:description", new BitmapMetadata("xmpalt"));
            metadata.SetQuery(@"/xmp/dc:description/{ushort=0}", description);

            metadata.SetQuery("/xmp/dc:subject", new BitmapMetadata("xmpbag"));
            int subjectIndex = 0;
            foreach (string subject in subjects)
            {
                string path = string.Format(@"/xmp/dc:subject/{{ushort={0}}}", subjectIndex.ToString());
                metadata.SetQuery(path, subject);
                subjectIndex++;
            }

            metadata.SetQuery("/xmp/dc:rights", new BitmapMetadata("xmpalt"));
            metadata.SetQuery(@"/xmp/dc:rights/{ushort=0}", rights);


            metadata.SetQuery(@"/xmp/photoshop:Credit", credit);
            metadata.SetQuery(@"/xmp/photoshop:Source", source);


            //metadata.SetQuery("/xmp/dc:rights", new BitmapMetadata("xmpalt"));
            //metadata.SetQuery(@"/xmp/CreatorContactInfo/CiUrlWork", creatorWorkURL);

            inStream.Seek(0, SeekOrigin.Begin);

            JpegBitmapDecoder decoder = new JpegBitmapDecoder(inStream,
                BitmapCreateOptions.PreservePixelFormat,
                BitmapCacheOption.Default);

            BitmapFrame frameCopy = BitmapFrame.Create(decoder.Frames[0],
                    null, // thumbnail
                    metadata,
                    decoder.ColorContexts);

            // Now we have the image frame that has a fresh IPTC metadata block

            // Create a new encoder and add the frame to it
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(frameCopy);

            // Save the new file with the new metadata
            MemoryStream outStream = new MemoryStream();

            encoder.Save(outStream);

            return outStream;
        }

        //http://flickrnet.codeplex.com/discussions/241542
        private static Flickr GetFlickrInstance()
        {
            Flickr.CacheDisabled = true;
            return new Flickr(ConfigurationManager.AppSettings["FlickrKey"], ConfigurationManager.AppSettings["FlickrSecret"]);
        }

    }
}
