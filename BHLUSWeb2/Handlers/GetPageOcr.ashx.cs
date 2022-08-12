using BHL.SiteServiceREST.v1.Client;
using System;
using System.Configuration;
using System.Web;

namespace MOBOT.BHL.Web2
{
    public class GetPageOcr : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            String pageIDString = HttpContext.Current.Request.RequestContext.RouteData.Values["pageid"].ToString(); 
            if (pageIDString == null) return;

            int pageID;
            if (Int32.TryParse(pageIDString, out pageID))
            {
                Client client = new Client(ConfigurationManager.AppSettings["SiteServicesURL"]);
                string ocrText = client.GetPageText(pageID);
                context.Response.ContentType = "text/plain";
                context.Response.Write(ocrText);
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
