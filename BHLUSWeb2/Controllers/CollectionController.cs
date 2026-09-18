using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;

namespace MOBOT.BHL.Web2.Controllers
{
    public class CollectionController : Controller
    {
        // GET: Collection
        public ActionResult Index()
        {
            int collectionID;

            List<Collection> collections = new BHLProvider().CollectionSelectByUrl((string)RouteData.Values["collectionid"]);
            if (collections.Count > 0)
            {
                int.TryParse(collections[0].CollectionID.ToString(), out collectionID);
            }
            else
            {
                if (!int.TryParse((string)RouteData.Values["collectionid"], out collectionID))
                {
                    Response.Redirect("~/collectionnotfound");
                }
            }

            Collection collection = new BHLProvider().CollectionSelectAuto(collectionID);

            if (collection != null)
            {
                ViewBag.HtmlContent = this.GetHtmlContent(collection);
                ViewBag.Title = this.GetPageTitle(collection);
            }

            return View();
        }

        private string GetHtmlContent(Collection collection)
        {
            string html = string.Empty;

            if (collection == null)
            {
                html = "Collection not found";
            }
            else
            {
                html = collection.HtmlContent;
            }

            return html;
        }

        private string GetPageTitle(Collection collection)
        {
            string title = String.Format(ConfigurationManager.AppSettings["PageTitle"], collection.CollectionName);
            return title;
        }
    }
}