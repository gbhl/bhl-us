using System;
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
                SiteService.SiteServiceSoapClient service = new SiteService.SiteServiceSoapClient();
                string ocrText = service.GetOcrText(pageID);
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
