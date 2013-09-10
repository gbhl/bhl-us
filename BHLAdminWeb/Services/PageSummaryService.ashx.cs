using System;
using System.Data;
using System.Web;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Web.Script.Serialization;
using System.Net;
using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using CustomDataAccess;
using MOBOT.BHL.Web.Utilities;

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
                    CustomGenericList<PageSummaryView> pages = new BHLProvider().PageSummarySelectForViewerByItemID(itemID);

                    // Serialize only the information we need
                    List<BHLProvider.ViewerPage> viewerPages = new List<BHLProvider.ViewerPage>();
                    foreach (PageSummaryView page in pages)
                    {
                        BHLProvider.ViewerPage viewerPage = new BHLProvider.ViewerPage();
                        viewerPage.ExternalBaseUrl = page.ExternalBaseURL;
                        viewerPage.AltExternalUrl = page.AltExternalURL;
                        viewerPage.BarCode = page.BarCode;
                        viewerPage.SequenceOrder = page.SequenceOrder;
                        viewerPages.Add(viewerPage);
                    }

                    // Add the height and width of each page to the list
                    viewerPages = new BHLProvider().PageGetImageDimensions(viewerPages, itemID);

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
