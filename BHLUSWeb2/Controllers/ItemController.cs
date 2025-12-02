using BHL.SiteServiceREST.v1.Client;
using MOBOT.BHL.DataObjects.Enum;
using MOBOT.BHL.Server;
using MOBOT.BHL.Web.Utilities;
using MvcThrottle;
using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;

namespace MOBOT.BHL.Web2.Controllers
{
    public class ItemController : Controller
    {
        int imagesTimeout = 36000000;

        [EnableThrottling]
        public ActionResult GetItemText(int itemid)
        {
            string itemText;
            string cacheKey = "ItemText" + itemid.ToString();
            System.Web.Caching.Cache cache = new System.Web.Caching.Cache();

            if (cache[cacheKey] != null)
            {
                // Use cached version
                itemText = cache[cacheKey].ToString();
            }
            else
            {
                // Refresh cache
                Client client = new Client(ConfigurationManager.AppSettings["SiteServicesURL"]);
                itemText = client.GetItemText(itemid);
                cache.Add(cacheKey, itemText, null, DateTime.Now.AddMinutes(
                    Convert.ToDouble(ConfigurationManager.AppSettings["ItemTextCacheTime"])),
                    System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.Normal, null);
            }

            Response.Cache.SetNoTransforms();
            ContentResult content = new ContentResult();
            content.Content = itemText;
            content.ContentType = "text/plain";
            return content;
        }

        [EnableThrottling]
        public ActionResult GetItemPdf(int itemid)
        {
            BHLProvider provider = new BHLProvider();
            DataObjects.Item item = provider.ItemSelectFilenames(ItemType.Book, itemid);

            if (!string.IsNullOrWhiteSpace(item.PdfFilename))
            {
                try
                {
                    var filePath = provider.GetRemoteFilePath(RemoteFileType.Pdf, item.BarCode, item.PdfFilename);

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
                        return Redirect(filePath);
                    }
                    else
                    {
                        return Redirect("~/pagenotfound");
                    }
                }
                catch (WebException wex)
                {
                    if (wex.Message.Contains("404"))
                    {
                        return Redirect("~/pagenotfound");
                    }
                    else
                    {
                        ExceptionUtility.LogException(wex, "ItemController.GetItemPdf");
                        return Redirect("~/error");
                    }
                }
            }
            else
            {
                return Redirect("~/pagenotfound");
            }
        }

        [EnableThrottling]
        public ActionResult GetItemImages(int itemid)
        {
            BHLProvider provider = new BHLProvider();
            DataObjects.Item item = provider.ItemSelectFilenames(ItemType.Book, itemid);

            if (!string.IsNullOrWhiteSpace(item.ImagesFilename))
            {
                Server.ScriptTimeout = imagesTimeout / 1000;

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
                    if (wex.Response is HttpWebResponse response)
                    {
                        if (response.StatusCode == HttpStatusCode.NotFound) return Redirect("~/pagenotfound");
                    }
                    else
                    {
                        ExceptionUtility.LogException(wex, "ItemController.GetItemImages");
                        return Redirect("~/error");
                    }
                }

                if (fileSize > 2147483647)
                {
                    // > 2GB file, so send it to the client in chunks
                    if (stream != null)
                    {
                        try
                        {
                            Response.Clear();
                            Response.ContentType = "application/octet-stream";
                            Response.Headers.Add("content-disposition", "filename=" + item.ImagesFilename);

                            const int bufferSize = 16384;  // 16KB buffer size
                            byte[] buffer = new byte[bufferSize];

                            int bytesRead;
                            while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                Response.OutputStream.Write(buffer, 0, bytesRead);
                                Response.Flush();
                            }

                            return new EmptyResult();
                        }
                        catch (Exception ex)
                        {
                            ExceptionUtility.LogException(ex, "ItemController.GetItemImages");
                            Response.Clear();
                            return Redirect("~/error");
                        }
                        finally
                        {
                            stream.Dispose();
                        }
                    }
                    else
                    {
                        return Redirect("~/pagenotfound");
                    }
                }
                else
                {
                    if (stream != null)
                    {
                        stream.Dispose();   // Don't need the stream for files < 2GB
                        Response.ClearHeaders();
                        Response.ContentType = "application/octet-stream";
                        return Redirect(filePath);
                    }
                    else
                    {
                        return Redirect("~/pagenotfound");
                    }
                }
            }
            else
            {
                return Redirect("~/pagenotfound");
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
            req.Timeout = imagesTimeout;
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            return resp;
        }
    }
}