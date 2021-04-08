using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Linq;
using MOBOT.BHL.DataObjects.Enum;

namespace MOBOT.BHL.AdminWeb.Services
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class PageSummaryService1 : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            String response;

            // Clean up inputs
            String pageTypeID = context.Request.QueryString["pageTypeID"] as String;
            String titleID = context.Request.QueryString["titleID"] as String;
            String pageID = context.Request.QueryString["pageID"] as String;
            String itemID = context.Request.QueryString["itemID"] as String;
            pageTypeID = String.IsNullOrEmpty(pageTypeID) ? "0" : pageTypeID;
            titleID = String.IsNullOrEmpty(titleID) ? "0" : titleID;
            pageID = String.IsNullOrEmpty(pageID) ? "0" : pageID;
            itemID = String.IsNullOrEmpty(itemID) ? "0" : itemID;

            switch (context.Request.QueryString["op"])
            {
                case "PageSummarySelectForViewerByItemID":
                    {
                        response = this.PageSummarySelectForViewerByItemID(itemID);
                        break;
                    }
                default:
                    {
                        response = null;
                        break;
                    }
            }

            context.Response.ContentType = "application/json";
            context.Response.Write(response);
        }

        private string PageSummarySelectForViewerByItemID(string itemIDString)
        {
            try
            {
                int itemID;
                if (Int32.TryParse(itemIDString, out itemID))
                {
                    List<PageSummaryView> pages = new BHLProvider().PageSummarySelectForViewerByItemID(itemID);

                    // Serialize only the information we need
                    List<SiteService.ViewerPage> viewerPages = new List<SiteService.ViewerPage>();
                    foreach (PageSummaryView page in pages)
                    {
                        SiteService.ViewerPage viewerPage = new SiteService.ViewerPage();
                        viewerPage.ExternalBaseUrl = page.ExternalBaseURL;
                        viewerPage.AltExternalUrl = page.ExternalURL;
                        viewerPage.BarCode = page.BarCode;
                        viewerPage.SequenceOrder = page.SequenceOrder;
                        viewerPages.Add(viewerPage);
                    }

                    // Add the height and width of each page to the list
                    viewerPages = (new SiteService.SiteServiceSoapClient().PageGetImageDimensions(viewerPages.ToArray(), (int)ItemType.Book, itemID)).ToList();

                    JavaScriptSerializer js = new JavaScriptSerializer();
                    return js.Serialize(viewerPages);
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
