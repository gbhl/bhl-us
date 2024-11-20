using MOBOT.BHL.DataObjects.Enum;
using MOBOT.BHL.Server;
using System;
using System.Net.Http;
using System.Web;

namespace MOBOT.BHL.Web2
{
    /// <summary>
    /// Summary description for GetItemImages
    /// </summary>
    public class GetItemImages : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string itemIDString = HttpContext.Current.Request.RequestContext.RouteData.Values["itemid"].ToString();
            if (itemIDString == null) return;

            int itemID;
            if (Int32.TryParse(itemIDString, out itemID))
            {
                BHLProvider provider = new BHLProvider();
                DataObjects.Item item = provider.ItemSelectFilenames(ItemType.Book, itemID);

                if (!string.IsNullOrWhiteSpace(item.ImagesFilename))
                {
                    try
                    {
                        var filePath = provider.GetRemoteFilePath(BHLProvider.RemoteFileType.ImageZip, item.BarCode, item.ImagesFilename);

                        // Check if the file exists before redirecting to it
                        var exists = false;
                        using (var client = new HttpClient())
                        {
                            var request = new HttpRequestMessage(HttpMethod.Head, filePath);
                            var response = client.SendAsync(request).GetAwaiter().GetResult();
                            exists = response.IsSuccessStatusCode;
                        }
                        if (exists)
                        {
                            context.Response.ClearHeaders();
                            context.Response.ContentType = "application/octet-stream";
                            context.Response.Redirect(filePath);
                        }
                        else
                        {
                            context.Response.Redirect("~/pagenotfound");
                        }
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