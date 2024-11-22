using MOBOT.BHL.DataObjects.Enum;
using MOBOT.BHL.Server;
using System;
using System.Net.Http;
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

                if (!string.IsNullOrWhiteSpace(item.ImagesFilename))
                {
                    try
                    {
                        var filePath = provider.GetRemoteFilePath(RemoteFileType.ImageZip, item.BarCode, item.ImagesFilename);

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