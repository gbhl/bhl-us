using BHL.SiteServiceREST.v1.Client;
using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using MvcThrottle;
using System;
using System.Configuration;
using System.Web.Mvc;

namespace MOBOT.BHL.Web2.Controllers
{
    public class PageController : Controller
    {
        [EnableThrottling]
        public ActionResult GetPageText(int? pageid)
        {
            if (!pageid.HasValue)
            {
                return Redirect("~/pagenotfound");
            }
            else
            {
                Client client = new Client(ConfigurationManager.AppSettings["SiteServicesURL"]);
                string ocrText = client.GetPageText((int)pageid);
                ContentResult content = new ContentResult();
                content.Content = ocrText;
                content.ContentType = "text/plain";
                return content;
            }
        }

        [EnableThrottling]
        public ActionResult GetPageThumb(int? pageid, int h = 300, int w = 200)
        {
            if (!pageid.HasValue)
            {
                return Redirect("~/pagenotfound");
            }
            else
            {
                int height = h;
                int width = w;

                Page page = new BHLProvider().PageSelectExternalUrlForPageID((int)pageid);
                string imageUrl = string.Empty;

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
                    }
                }

                if (imageUrl == String.Empty) imageUrl = ConfigurationManager.AppSettings["ImageNotFoundThumbPath"];
                return Redirect(imageUrl);
            }
        }

        [EnableThrottling]
        public ActionResult GetPageImage(int? pageid)
        {
            if (!pageid.HasValue)
            {
                return Redirect("~/pagenotfound");
            }
            else
            {
                Page page = new BHLProvider().PageSelectExternalUrlForPageID((int)pageid);
                String imageUrl = String.Empty;

                // Make sure we found an active page
                if (page != null)
                {
                    // Use the IA URL to get a JPG from the JP2
                    imageUrl = page.AltExternalURL.Replace(".jp2", ".jpg");
                }

                if (imageUrl == String.Empty) imageUrl = ConfigurationManager.AppSettings["ImageNotFoundPath"];
                return Redirect(imageUrl);
            }
        }
    }
}
