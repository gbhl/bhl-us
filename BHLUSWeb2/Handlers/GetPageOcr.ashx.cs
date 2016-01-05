using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Services;
using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using BHL.SiteServices;

namespace MOBOT.BHL.Web2
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class GetPageOcr : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            //String pageIDString = context.Request.QueryString["PageID"] as String;
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
