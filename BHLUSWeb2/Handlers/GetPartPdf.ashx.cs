using MOBOT.BHL.DataObjects.Enum;
using MOBOT.BHL.Server;
using System;
using System.Configuration;
using System.Web;

namespace MOBOT.BHL.Web2
{
    /// <summary>
    /// Summary description for GetPartPdf
    /// </summary>
    public class GetPartPdf : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            string idString = HttpContext.Current.Request.RequestContext.RouteData.Values["id"].ToString();
            if (idString == null) return;

            if (Int32.TryParse(idString, out int id))
            {
                BHLProvider provider = new BHLProvider();
                DataObjects.Item item = provider.ItemSelectFilenames(ItemType.Segment, id);

                context.Response.ContentType = "application/pdf";
                if (!string.IsNullOrWhiteSpace(item.PdfFilename))
                {
                    try
                    {
                        context.Response.Redirect(string.Format(ConfigurationManager.AppSettings["IADownloadLink"], item.BarCode, item.PdfFilename));
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