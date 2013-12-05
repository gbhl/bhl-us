using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using MOBOT.BHL.Web.Utilities;

namespace MOBOT.BHL.Web2
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class GetPageThumb : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            //String pageIDString = context.Request.QueryString["PageID"] as String;
            String pageIDString = HttpContext.Current.Request.RequestContext.RouteData.Values["pageid"].ToString(); 
            if (pageIDString == null) return;

            //String heightString = context.Request.QueryString["h"] as String;
            //String widthString = context.Request.QueryString["w"] as String;
            String heightString = (HttpContext.Current.Request.RequestContext.RouteData.Values["h"] ?? String.Empty).ToString();
            String widthString = (HttpContext.Current.Request.RequestContext.RouteData.Values["w"] ?? String.Empty).ToString();

            int height;
            int width;
            if (!Int32.TryParse(heightString, out height)) height = 300;
            if (!Int32.TryParse(widthString, out width)) width = 200;

            int pageID;
            if (Int32.TryParse(pageIDString, out pageID))
            {
                BHLProvider provider = new BHLProvider();
                Page page = provider.PageSelectExternalUrlForPageID(pageID);
                String imageUrl = String.Empty;

                // Make sure we found an active page
                if (page != null)
                {
                    // Use the IA URL to get a JPG from the JP2
                    String pageUrlSuffix = "_w" + width.ToString() + ".jpg";
                    imageUrl = page.AltExternalURL.Replace(".jpg", "") + pageUrlSuffix;
                }

                System.Net.WebClient client = new System.Net.WebClient();
                context.Response.ContentType = "image/jpeg";
                try
                {
                    if (imageUrl == String.Empty)
                    {
                        imageUrl = ConfigurationManager.AppSettings["ImageNotFoundThumbPath"];
                        context.Response.StatusCode = 404;
                    }
                    context.Response.BinaryWrite(client.DownloadData(imageUrl));
                }
                catch (System.Net.WebException wex)
                {
                    if (wex.Message.Contains("404"))
                    {
                        context.Response.BinaryWrite(client.DownloadData(ConfigurationManager.AppSettings["ImageNotFoundThumbPath"]));
                        context.Response.StatusCode = 404;
                    }
                }
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
