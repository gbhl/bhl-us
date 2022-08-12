using MOBOT.BHL.DataObjects.Enum;
using MOBOT.BHL.Server;
using System;
using System.Configuration;
using System.Web;

namespace MOBOT.BHL.Web2
{
    /// <summary>
    /// Summary description for GetPartImages
    /// </summary>
    public class GetPartImages : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string idString = HttpContext.Current.Request.RequestContext.RouteData.Values["id"].ToString();
            if (idString == null) return;

            if (Int32.TryParse(idString, out int id))
            {
                BHLProvider provider = new BHLProvider();
                DataObjects.Item item = provider.ItemSelectFilenames(ItemType.Segment, id);

                context.Response.ContentType = "application/octet-stream";
                if (!string.IsNullOrWhiteSpace(item.TextFilename))
                {
                    try
                    {
                        context.Response.Redirect(string.Format(ConfigurationManager.AppSettings["IADownloadLink"], item.BarCode, item.ImagesFilename));
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