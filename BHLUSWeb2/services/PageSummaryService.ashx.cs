using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;

namespace MOBOT.BHL.Web2.Services
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class PageSummaryService1 : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            string response;

            // Clean up inputs
            string pageID = context.Request.QueryString["pageID"] as string;
            pageID = string.IsNullOrEmpty(pageID) ? "0" : pageID;

            switch (context.Request.QueryString["op"])
            {
                case "GetPageNameList":
                    {
                        response = this.GetPageNameList(Convert.ToInt32(pageID));
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

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
