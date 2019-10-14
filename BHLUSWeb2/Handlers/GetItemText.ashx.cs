using MOBOT.BHL.Server;
using System;
using System.Configuration;
using System.Web;

namespace MOBOT.BHL.Web2
{
    public class GetItemText : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string itemIDString = HttpContext.Current.Request.RequestContext.RouteData.Values["itemid"].ToString();
            if (itemIDString == null) return;

            int itemID;
            if (Int32.TryParse(itemIDString, out itemID))
            {
                SiteService.SiteServiceSoapClient service = new SiteService.SiteServiceSoapClient();
                string ocrText = service.GetItemText(itemID);
                context.Response.ContentType = "text/plain";
                context.Response.Cache.SetNoTransforms();
                context.Response.Write(ocrText);

                /*
                BHLProvider provider = new BHLProvider();
                DataObjects.Item item = provider.ItemSelectFilenames(itemID);

                context.Response.ContentType = "text/plain";
                if (!string.IsNullOrWhiteSpace(item.TextFilename))
                {
                    System.Net.WebClient client = new System.Net.WebClient();

                    try
                    {
                        context.Response.Write(client.DownloadString(string.Format(ConfigurationManager.AppSettings["IADownloadLink"], item.BarCode, item.TextFilename)));
                    }
                    catch (System.Net.WebException wex)
                    {
                        if (wex.Message.Contains("404"))
                        {
                            context.Response.Redirect("~/pagenotfound");
                        }
                    }

                }
                else
                {
                    context.Response.Redirect("~/pagenotfound");
                }
                */
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