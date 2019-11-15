using System;
using System.Web.Mvc;
using IIIF = BHL.IIIF;

namespace MOBOT.BHL.Web2.Controllers
{
    public class IIIFController : Controller
    {
        [HttpGet]
        public ActionResult Manifest(string itemId)
        {
            string manifest = string.Empty;
            int itemIdInt = int.MinValue;
            IIIF.Manifest manifestService = new IIIF.Manifest();
            if (Int32.TryParse(itemId, out itemIdInt))
            {
                manifest = manifestService.GetManifest(itemIdInt);
            }
            else
            {
                manifest = string.Format("Invalid Item Identifier: {0}", itemId);
            }

            // TODO:  Open to title page of book, rather than to first image
            return Content(manifest);
        }
    }
}