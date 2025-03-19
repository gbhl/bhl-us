using MOBOT.BHL.DataObjects.Enum;
using MOBOT.BHL.Server;
using MOBOT.BHL.Web.Utilities;
using System;
using System.IO;
using System.Net;
using System.Web;

namespace MOBOT.BHL.Web2
{
    /// <summary>
    /// Summary description for GetItemImages
    /// </summary>
    public class GetItemImages : IHttpHandler
    {
        int fileTimeout = 36000000;

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
                    context.Server.ScriptTimeout = fileTimeout / 1000;

                    var filePath = provider.GetRemoteFilePath(RemoteFileType.ImageZip, item.BarCode, item.ImagesFilename);
                    Stream stream = null;
                    long fileSize = 0;

                    try
                    {
                        stream = this.GetFileStream(filePath, out fileSize);
                    }
                    catch (WebException wex)
                    {
                        if (stream != null) stream.Dispose();

                        string redirect = "~/error";
                        var response = wex.Response as HttpWebResponse;
                        if (response != null)
                        {
                            if (response.StatusCode == HttpStatusCode.NotFound) redirect = "~/pagenotfound";
                        }
                        context.Response.Redirect(redirect, true);
                    }

                    if (fileSize > 2147483647)
                    {
                        // > 2GB file, so send it to the client in chucks
                        if (stream != null)
                        {
                            try
                            {
                                context.Response.Clear();
                                context.Response.ClearContent();
                                context.Response.ClearHeaders();
                                context.Response.ContentType = "application/octet-stream";
                                context.Response.AddHeader("content-disposition", "filename=" + item.ImagesFilename);

                                const int bufferSize = 16384;  // 16KB buffer size
                                byte[] buffer = new byte[bufferSize];

                                int bytesRead;
                                while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                                {
                                    context.Response.OutputStream.Write(buffer, 0, bytesRead);
                                    context.Response.Flush();
                                }
                            }
                            catch (Exception ex)
                            {
                                ExceptionUtility.LogException(ex, "GetItemImages.ProcessRequest");
                            }
                            finally
                            {
                                stream.Dispose();
                            }
                        }

                        context.Response.End();
                    }
                    else
                    {
                        if (stream != null)
                        {
                            stream.Dispose();   // Don't need the stream for files < 2GB
                            context.Response.ClearHeaders();
                            context.Response.ContentType = "application/octet-stream";
                            context.Response.Redirect(filePath);
                        }
                        else
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

        private Stream GetFileStream(string url, out long fileSize)
        {
            Stream file = null;
            fileSize = 0;

            HttpWebResponse resp = this.HttpGet(url);
            if (resp != null)
            {
                fileSize = resp.ContentLength;
                file = resp.GetResponseStream();
            }

            return file;
        }

        private HttpWebResponse HttpGet(string url)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "GET";
            // Set timeout values to prevent file truncation while downloading
            req.Timeout = fileTimeout;
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            return resp;
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