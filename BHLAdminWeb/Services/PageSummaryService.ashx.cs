using BHL.SiteServiceREST.v1.Client;
using BHL.SiteServicesREST.v1;
using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;

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
                        response = this.PageSummarySelectForViewer("Book", itemID);
                        break;
                    }
                case "PageSummarySelectForViewerBySegmentID":
                    {
                        response = this.PageSummarySelectForViewer("Segment", itemID);
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

        private string PageSummarySelectForViewer(string idType, string idString)
        {
            try
            {
                int id;
                if (Int32.TryParse(idString, out id))
                {
                    List<PageSummaryView> pages = null;
                    if (idType == "Book") pages = new BHLProvider().PageSummarySelectForViewerByItemID(id);
                    if (idType == "Segment") pages = new BHLProvider().PageSummarySelectForViewerBySegmentID(id);

                    // Serialize only the information we need
                    List<ViewerPageModel> viewerPages = new List<ViewerPageModel>();

                    //List<SiteService.ViewerPage> viewerPages = new List<SiteService.ViewerPage>();
                    foreach (PageSummaryView page in pages)
                    {
                        //SiteService.ViewerPage viewerPage = new SiteService.ViewerPage();
                        ViewerPageModel viewerPage = new ViewerPageModel();
                        viewerPage.ExternalBaseUrl = page.ExternalBaseURL;
                        viewerPage.AltExternalUrl = page.ExternalURL;
                        viewerPage.BarCode = page.BarCode;
                        viewerPage.SequenceOrder = page.SequenceOrder;
                        viewerPages.Add(viewerPage);
                    }

                    // Add the height and width of each page to the list
                    Client client = new Client(ConfigurationManager.AppSettings["SiteServicesURL"]);
                    if (idType == "Segment")
                    {
                        viewerPages = client.GetSegmentPageImageDimensions(id, viewerPages).ToList<ViewerPageModel>();
                    }
                    else
                    {
                        viewerPages = client.GetItemPageImageDimensions(id, viewerPages).ToList<ViewerPageModel>();
                    }

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
