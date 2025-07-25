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
    public class PartController : Controller
    {
        int pdfTimeout = 36000000;

        [EnableThrottling]
        public ActionResult GetPartText(int partid)
        {
            string partText;
            string cacheKey = "PartText" + partid.ToString();
            System.Web.Caching.Cache cache = new System.Web.Caching.Cache();

            if (cache[cacheKey] != null)
            {
                // Use cached version
                partText = cache[cacheKey].ToString();
            }
            else
            {
                // Refresh cache
                Client client = new Client(ConfigurationManager.AppSettings["SiteServicesURL"]);
                partText = client.GetSegmentText(partid);
                cache.Add(cacheKey, partText, null, DateTime.Now.AddMinutes(
                    Convert.ToDouble(ConfigurationManager.AppSettings["ItemTextCacheTime"])),
                    System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.Normal, null);
            }

            Response.Cache.SetNoTransforms();
            ContentResult content = new ContentResult();
            content.Content = partText;
            content.ContentType = "text/plain";
            return content;
        }

        [EnableThrottling]
        public ActionResult GetPartPdf(int id)
        {
            Server.ScriptTimeout = pdfTimeout / 1000;

            BHLProvider provider = new BHLProvider();
            DataObjects.Item item = provider.ItemSelectFilenames(ItemType.Segment, id);

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
                        return Redirect("~/error");
                    }
                }
            }
            else
            {
                if (ConfigurationManager.AppSettings["UsePregeneratedPDFs"] == "true")
                {
                    return this.GetPregeneratedPDF(id);
                }
                else
                {
                    return Redirect("~/pagenotfound");
                }
            }
        }

        private ActionResult GetPregeneratedPDF(int id)
        {
            // Send the PDF to the client
            Stream stream = null;
            try
            {
                stream = this.GetPdfStream(id);
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
                return Redirect(redirect);
            }

            if (stream != null)
            {
                try
                {
                    FileStreamResult streamResult = new FileStreamResult(stream, "application/pdf");
                    streamResult.FileDownloadName = string.Format("part{0}.pdf", id.ToString());
                    return streamResult;
                }
                catch (Exception ex)
                {
                    ExceptionUtility.LogException(ex, "GetPartPDF.GetPregeneratedPdf");
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

        private Stream GetPdfStream(int id)
        {
            Client client = new Client(ConfigurationManager.AppSettings["SiteServicesURL"]);
            var pdf = client.GetSegmentPdf(id);
            return pdf;
        }

        [EnableThrottling]
        public ActionResult GetPartImages(int id)
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
                        return Redirect("~/error");
                    }
                }
            }
            else
            {
                return Redirect("~/pagenotfound");
            }
        }
    }
}