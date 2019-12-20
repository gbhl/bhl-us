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

namespace MOBOT.BHL.Web2.Services
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
                case "FetchPageUrl":
                    {
                        response = this.FetchPageUrl(context,
                                            Convert.ToInt32(pageID));
                        break;
                    }
                case "GetPageNameList":
                    {
                        response = this.GetPageNameList(
                                            Convert.ToInt32(pageID));
                        break;
                    }
                case "PageSummarySelectForViewerByItemID":
                    {
                        response = this.PageSummarySelectForViewerByItemID(itemID);
                        break;
                    }
                case "GetPageOcrText":
                    {
                        response = GetPageOcrText(Convert.ToInt32(pageID));
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

        private string FetchPageUrl(HttpContext context, int pageID)
        {
            PageSummaryService pss = new PageSummaryService();
            string[] sa = pss.FetchPageUrl(pageID);

            JavaScriptSerializer js = new JavaScriptSerializer();
            return js.Serialize(sa);
        }

        private string GetPageOcrText(int pageID)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            try
            {
                string ocrText;

                using (WebClient client = new WebClient())
                {
                    client.Encoding = System.Text.Encoding.UTF8; 
                    ocrText = HttpUtility.HtmlEncode(client.DownloadString(ConfigurationManager.AppSettings["BaseUrl"] + "/pagetext/" + pageID));
                }

                if (string.IsNullOrWhiteSpace(ocrText))
                {
                    js.Serialize(new { ocrText = "Text unavailable for this page.", success = false });
                }

                return js.Serialize(new { ocrText, success = true });
            }
            catch
            {
                return js.Serialize(new { ocrText = "Text unavailable for this page.", success = false });
            }
        }


        private string GetPageNameList(int pageID)
        {
            this.PopulatePageNames(pageID);
            List<NameResolved> namePageList = new BHLProvider().NameResolvedSelectByPageID(pageID);
            List<NameResolved> returnList = new List<NameResolved>();
            foreach (NameResolved namePage in namePageList)
            {
                if (!string.IsNullOrEmpty(namePage.ResolvedNameString))
                {
                    namePage.UrlName = namePage.ResolvedNameString.Replace(' ', '_').Replace('.', '$').Replace('?', '^').Replace('&', '~');
                    returnList.Add(namePage);
                }
            }

            JavaScriptSerializer js = new JavaScriptSerializer();
            return js.Serialize(returnList);
        }

        /// <summary>
        /// Uses the OCR for the page to look up any names that weren't previously identified by SciLINC
        /// </summary>
        /// <param name="itemID"></param>
        /// <param name="sequenceOrder"></param>
        private void PopulatePageNames(int pageID)
        {
            BHLProvider provider = new BHLProvider();
            Page page = provider.PageSelectAuto(pageID);
            bool doLookup = false;

            // Check the configuration setting to make sure we're looking up names remotely
            if (ConfigurationManager.AppSettings["DoRemoteNameLookup"] == "true")
            {
                // Look up the page names if we never have for this page, or if it's been longer
                // than the maximum page name age since we've looked them up
                if (page.LastPageNameLookupDate == null)
                {
                    doLookup = true;
                }
                else
                {
                    TimeSpan ts = DateTime.Now.Subtract((DateTime)page.LastPageNameLookupDate);
                    if (ts.Days > Convert.ToInt32(ConfigurationManager.AppSettings["MaximumPageNameAge"]))
                        doLookup = true;
                }
            }

            if (doLookup)
            {
                try
                {
                    List<NameFinderResponse> names = new PageSummaryService().GetNamesFromOcr(pageID);
                    provider.PageNameUpdateList(pageID, names, ConfigurationManager.AppSettings["NameServiceSourceName"]);
                    provider.PageUpdateLastPageNameLookupDate(page.PageID);
                }
                catch (System.Net.WebException wex)
                {
                    // Catch and ignore web exceptions, as they likely represent a
                    // communication failure with the uBio service.
                }
                catch
                {
                }
            }
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
