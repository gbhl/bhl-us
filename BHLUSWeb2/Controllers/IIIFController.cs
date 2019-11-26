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

            ViewBag.ItemID = itemId;
            ViewBag.PageSequence = (firstPage == null ? 1 : firstPage.SequenceOrder);
            ViewBag.Title = string.Format(ConfigurationManager.AppSettings["PageTitle"], (String.IsNullOrEmpty(page.Volume) ? String.Empty : page.Volume + " - ") + page.ShortTitle);

            return View();
        }

        public ActionResult Page(string pageId)
        {
            PageSummaryView page = new BHLProvider().PageSummarySelectByPageId(Convert.ToInt32(pageId), true);

            // Check to make sure this item is found, is published, and hasn't been replaced.  If it has been replaced, redirect to the appropriate itemid.  
            // That won't find the correct page, but at least puts the user in the correct item... better than "not found".
            if (page == null) return new RedirectResult("~/itemnotfound");
            if (page.RedirectItemID != null) return new RedirectResult("~/item/" + page.RedirectItemID);
            if (page.ItemStatusID != 40) return new RedirectResult("~/itemunavailable");

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

            return Content(manifest);
        }

        [HttpGet]
        public ActionResult TextManifest(string itemId, string pageSeq)
        {
            string manifest = string.Empty;
            int itemIdInt = int.MinValue;
            int pageSeqInt = int.MinValue;
            //IIIF.TextManifest manifestService = new IIIF.TextManifest(Request.Url.GetLeftPart(UriPartial.Authority));
            IIIF.TextManifest manifestService = new IIIF.TextManifest("https://www.biodiversitylibrary.org");
            if (Int32.TryParse(itemId, out itemIdInt) && Int32.TryParse(pageSeq, out pageSeqInt))
            {
                manifest = manifestService.GetManifest(itemIdInt, pageSeqInt);
            }
            else
            {
                manifest = string.Format("Invalid Page Identifier: {0}-{1}", itemId, pageSeq);
            }

            return Content(manifest);
        }
    }
}