using BHL.SiteServiceREST.v1.Client;
using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using MOBOT.BHL.Web.Utilities;
using MvcThrottle;
using System;
using System.Configuration;
using System.Web.Mvc;

namespace MOBOT.BHL.Web2.Controllers
{
    public class PageController : Controller
    {
        [EnableThrottling]
        public ActionResult GetPageText(int pageid)
        {
            Client client = new Client(ConfigurationManager.AppSettings["SiteServicesURL"]);
            string ocrText = client.GetPageText(pageid);
            ContentResult content = new ContentResult();
            content.Content = ocrText;
            content.ContentType = "text/plain";
            return content;
        }

        [EnableThrottling]
        public ActionResult GetPageThumb(int pageid, int h = 300, int w = 200)
        {
            int height = h;
            int width = w;

            Page page = new BHLProvider().PageSelectExternalUrlForPageID(pageid);
            string imageUrl = string.Empty;
            string contentType = "image/jpeg";

            // Make sure we found an active page
            if (page != null)
            {
                imageUrl = page.AltExternalURL;
                if (imageUrl.Contains("archive.org"))
                {
                    // Use the IA URL to get a JPG from the JP2
                    string pageUrlSuffix = "_w" + width.ToString() + ".jpg";
                    imageUrl = imageUrl.Replace(".jpg", "") + pageUrlSuffix;

                }
                else
                {
                    imageUrl = imageUrl.Replace(".jpg", "") + "_thumb.webp";
                    contentType = "image/webp";
                }
            }

            System.Net.WebClient client = new System.Net.WebClient();
            try
            {
                if (imageUrl == String.Empty)
                {
                    imageUrl = ConfigurationManager.AppSettings["ImageNotFoundThumbPath"];
                    Response.StatusCode = 404;
                    Response.TrySkipIisCustomErrors = true;
                }
                return File(client.DownloadData(imageUrl), contentType);
            }
            catch (System.Net.WebException wex)
            {
                if (wex.Message.Contains("404"))
                {
                    Response.StatusCode = 404;
                    Response.TrySkipIisCustomErrors = true;
                    return File(client.DownloadData(ConfigurationManager.AppSettings["ImageNotFoundThumbPath"]), contentType);
                }
                else
                {
                    ExceptionUtility.LogException(wex, "PageController.GetPageThumb");
                    return Redirect("~/error");
                }
            }

        }

        [EnableThrottling]
        public ActionResult GetPageImage(int pageid)
        {
            Page page = new BHLProvider().PageSelectExternalUrlForPageID(pageid);
            String imageUrl = String.Empty;

            // Make sure we found an active page
            if (page != null)
            {
                // Use the IA URL to get a JPG from the JP2
                imageUrl = page.AltExternalURL.Replace(".jp2", ".jpg");
            }

            System.Net.WebClient client = new System.Net.WebClient();
            try
            {
                if (imageUrl == String.Empty)
                {
                    imageUrl = ConfigurationManager.AppSettings["ImageNotFoundPath"];
                    Response.StatusCode = 404;
                    Response.TrySkipIisCustomErrors = true;
                }
                return File(client.DownloadData(imageUrl), "image/jpeg");
            }
            catch (System.Net.WebException wex)
            {
                if (wex.Message.Contains("404"))
                {
                    Response.StatusCode = 404;
                    Response.TrySkipIisCustomErrors = true;
                    return File(client.DownloadData(ConfigurationManager.AppSettings["ImageNotFoundPath"]), "iamge/jpeg");
                }
                else
                {
                    ExceptionUtility.LogException(wex, "PageController.GetPageImage");
                    return Redirect("~/error");
                }
            }
        }
    }
}
