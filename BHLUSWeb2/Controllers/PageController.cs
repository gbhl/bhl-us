using BHL.SiteServiceREST.v1.Client;
using MOBOT.BHL.DataObjects;
using MOBOT.BHL.DataObjects.Enum;
using MOBOT.BHL.Server;
using MOBOT.BHL.Utility;
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
                BHLProvider provider = new BHLProvider();
                PageSummaryView ps = provider.PageSummarySelectByPageId((int)pageid);
                if (ps == null) ps = provider.PageSummarySegmentSelectByPageID((int)pageid);
                string remoteFilePath = provider.GetRemoteFilePath(RemoteFileType.PageText, itemID: ps.BookID, pageID: ps.PageID, pageSeq: ps.SequenceOrder);
                if (string.IsNullOrWhiteSpace(remoteFilePath))
                {
                    // No remote file path, so get the text from Site Services API
                    Client client = new Client(ConfigurationManager.AppSettings["SiteServicesURL"]);
                    string ocrText = client.GetPageText((int)pageid);
                    ContentResult content = new ContentResult();
                    content.Content = ocrText;
                    content.ContentType = "text/plain";
                    return content;
                }
                else
                {
                    // Get the text from the remote location
                    return Redirect(remoteFilePath);
                }
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
                        imageUrl = imageUrl.Replace("_full.webp", "_thumb.webp");
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
