using MOBOT.BHL.Server;
using System;
using System.Configuration;
using System.Web;

namespace MOBOT.BHL.Web2
{
    /// <summary>
    /// Summary description for GetItemPdf
    /// </summary>
    public class GetItemPdf : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string itemIDString = HttpContext.Current.Request.RequestContext.RouteData.Values["itemid"].ToString();
            if (itemIDString == null) return;

            int itemID;
            if (Int32.TryParse(itemIDString, out itemID))
            {
                BHLProvider provider = new BHLProvider();
                DataObjects.Item item = provider.ItemSelectFilenames(itemID);

                context.Response.ContentType = "application/pdf";
                if (!string.IsNullOrWhiteSpace(item.TextFilename))
                {
                    System.Net.WebClient client = new System.Net.WebClient();

                    try
                    {
                        context.Response.BinaryWrite(client.DownloadData(string.Format(ConfigurationManager.AppSettings["IADownloadLink"], item.BarCode, item.PdfFilename)));
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