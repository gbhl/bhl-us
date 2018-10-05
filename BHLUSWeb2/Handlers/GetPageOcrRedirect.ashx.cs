using System;
using System.Web;

namespace MOBOT.BHL.Web2
{
    public class GetPageOcrRedirect : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            String pageIDString = HttpContext.Current.Request.RequestContext.RouteData.Values["pageid"].ToString() ?? string.Empty;
            context.Response.Redirect(string.Format("/pagetext/{0}", pageIDString));
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