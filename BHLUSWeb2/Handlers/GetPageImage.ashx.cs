using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using System;
using System.Configuration;
using System.Web;
using System.Web.Services;

namespace MOBOT.BHL.Web2
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class GetPageImage : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            //String pageIDString = context.Request.QueryString["PageID"] as String;
            String pageIDString = HttpContext.Current.Request.RequestContext.RouteData.Values["pageid"].ToString(); 
            if (pageIDString == null) return;

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
                    imageUrl = page.AltExternalURL.Replace(".jp2", ".jpg");
                }

                System.Net.WebClient client = new System.Net.WebClient();
                context.Response.ContentType = "image/jpeg";
                try
                {
                    if (imageUrl == String.Empty)
                    {
                        imageUrl = ConfigurationManager.AppSettings["ImageNotFoundPath"];
                        context.Response.StatusCode = 404;
                        context.Response.TrySkipIisCustomErrors = true;
                    }
                    context.Response.BinaryWrite(client.DownloadData(imageUrl));
                }
                catch (System.Net.WebException wex)
                {
                    if (wex.Message.Contains("404"))
                    {
                        context.Response.BinaryWrite(client.DownloadData(ConfigurationManager.AppSettings["ImageNotFoundPath"]));
                        context.Response.StatusCode = 404;
                        context.Response.TrySkipIisCustomErrors = true;
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
