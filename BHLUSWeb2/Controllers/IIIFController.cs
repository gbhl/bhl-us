using MOBOT.BHL.Server;
using MOBOT.BHL.DataObjects;
using System;
using System.Web.Mvc;
using IIIF = BHL.IIIF;
using System.Configuration;

namespace MOBOT.BHL.Web2.Controllers
{
    public class IIIFController : Controller
    {
        [HttpGet]
        public ActionResult Item(string itemId)
        {
            if (ViewerRedirect()) return new RedirectResult("/item/" + itemId); // IIIF toggle action

            BHLProvider provider = new BHLProvider();
            PageSummaryView page = provider.PageSummarySelectByItemId(Convert.ToInt32(itemId), true);

            // Check to make sure this item exists, is published, hasn't been replaced, and isn't external.  Redirect to the appropriate itemid, as necessary.
            if (page == null)
            {
                // If no pages then see if we should redirect to an external url
                Item item = provider.ItemSelectAuto(Convert.ToInt32(itemId));
                if (item != null) if (!string.IsNullOrWhiteSpace(item.ExternalUrl)) return new RedirectResult(item.ExternalUrl);
                return new RedirectResult("~/itemnotfound");
            }
            if (page.RedirectItemID != null) return new RedirectResult("~/item/" + page.RedirectItemID);
            if (page.ItemStatusID != 40) return new RedirectResult("~/itemunavailable");

            // Get the details of the first page to display
            Page firstPage = provider.PageSelectFirstPageForItem(Convert.ToInt32(itemId));

            ViewBag.IIIFLinkTarget = "/item/" + itemId; // Used for IIIF toggle
            ViewBag.ItemID = itemId;
            ViewBag.PageSequence = (firstPage == null ? 1 : firstPage.SequenceOrder);
            ViewBag.Title = string.Format(ConfigurationManager.AppSettings["PageTitle"], (String.IsNullOrEmpty(page.Volume) ? String.Empty : page.Volume + " - ") + page.ShortTitle);

            return View();
        }

        public ActionResult Page(string pageId)
        {
            if (ViewerRedirect()) return new RedirectResult("/page/" + pageId); // IIIF toggle action

            PageSummaryView page = new BHLProvider().PageSummarySelectByPageId(Convert.ToInt32(pageId), true);

            // Check to make sure this item is found, is published, and hasn't been replaced.  If it has been replaced, redirect to the appropriate itemid.  
            // That won't find the correct page, but at least puts the user in the correct item... better than "not found".
            if (page == null) return new RedirectResult("~/itemnotfound");
            if (page.RedirectItemID != null) return new RedirectResult("~/item/" + page.RedirectItemID);
            if (page.ItemStatusID != 40) return new RedirectResult("~/itemunavailable");

            ViewBag.IIIFLinkTarget = "/page/" + pageId; // Used for IIIF toggle
            ViewBag.ItemID = page.ItemID;
            ViewBag.PageSequence = page.SequenceOrder;
            ViewBag.Title = string.Format(ConfigurationManager.AppSettings["PageTitle"], (String.IsNullOrEmpty(page.Volume) ? String.Empty : page.Volume + " - ") + page.ShortTitle);

            return View();
        }

        [HttpGet]
        public ActionResult Manifest(string itemId)
        {
            string manifest = string.Empty;
            int itemIdInt = int.MinValue;
            IIIF.Manifest manifestService = new IIIF.Manifest(Request.Url.GetLeftPart(UriPartial.Authority));
            if (Int32.TryParse(itemId, out itemIdInt))
            {
                manifest = manifestService.GetManifest(itemIdInt);
            }
            else
            {
                manifest = string.Format("Invalid Item Identifier: {0}", itemId);
            }

            //HttpContext.Response.AppendHeader("Access-Control-Allow-Origin", "*");
            return Content(manifest);
        }

        [HttpGet]
        public ActionResult TextManifest(string itemId, string pageSeq)
        {
            string manifest = string.Empty;
            int itemIdInt = int.MinValue;
            int pageSeqInt = int.MinValue;
            IIIF.TextManifest manifestService = new IIIF.TextManifest(Request.Url.GetLeftPart(UriPartial.Authority));
            if (Int32.TryParse(itemId, out itemIdInt) && Int32.TryParse(pageSeq, out pageSeqInt))
            {
                manifest = manifestService.GetManifest(itemIdInt, pageSeqInt);
            }
            else
            {
                manifest = string.Format("Invalid Page Identifier: {0}-{1}", itemId, pageSeq);
            }

            //HttpContext.Response.AppendHeader("Access-Control-Allow-Origin", "*");
            return Content(manifest);
        }

        [HttpGet]
        public ActionResult NameManifest(string itemId, string pageSeq)
        {
            string manifest = string.Empty;
            int itemIdInt = int.MinValue;
            int pageSeqInt = int.MinValue;
            IIIF.NameManifest manifestService = new IIIF.NameManifest(Request.Url.GetLeftPart(UriPartial.Authority));
            if (Int32.TryParse(itemId, out itemIdInt) && Int32.TryParse(pageSeq, out pageSeqInt))
            {
                manifest = manifestService.GetManifest(itemIdInt, pageSeqInt);
            }
            else
            {
                manifest = string.Format("Invalid Page Identifier: {0}-{1}", itemId, pageSeq);
            }

            //HttpContext.Response.AppendHeader("Access-Control-Allow-Origin", "*");
            return Content(manifest);
        }

        /// <summary>
        /// Toggle IIIF behavior
        /// </summary>
        /// <returns></returns>
        private bool ViewerRedirect()
        {
            // If IIIF usage is turned off, immediately redirect to the original search
            if (ConfigurationManager.AppSettings["IIIFState"] == "off") return true;

            // If IIIF usage is turned on, never redirect
            if (ConfigurationManager.AppSettings["IIIFState"] == "on") return false;

            // Toggle mode

            // User requested to switch to iiif book viewer, so set cookie to enable IIIF viewer for seven days
            if (Request.QueryString["iiif"] == "1") 
            {
                // Set cookie to use the iiif viewer
                System.Web.HttpCookie cookie = new System.Web.HttpCookie("iiifviewer");
                cookie.Value = "1";
                cookie.Expires = DateTime.Now.AddDays(7);
                cookie.Domain = ".biodiversitylibrary.org";
                Response.Cookies.Add(cookie);
            }

            return false;
        }
    }
}
