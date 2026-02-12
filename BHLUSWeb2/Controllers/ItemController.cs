using BHL.SiteServiceREST.v1.Client;
using MOBOT.BHL.DataObjects;
using MOBOT.BHL.DataObjects.Enum;
using MOBOT.BHL.Server;
using MOBOT.BHL.Web.Utilities;
using MvcThrottle;
using System;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;

namespace MOBOT.BHL.Web2.Controllers
{
    public class ItemController : Controller
    {
        [EnableThrottling]
        public ActionResult GetItemText(int? itemid)
        {
            if (!itemid.HasValue)
            {
                return Redirect("~/pagenotfound");
            }
            else
            {
                BHLProvider provider = new BHLProvider();

                Item item = provider.ItemSelectFilenames(ItemType.Book, (int)itemid);
                string itemtextPath = provider.GetRemoteFilePath(RemoteFileType.ItemText, item.BarCode, item.TextFilename, itemid);

                if (itemtextPath.Contains("archive.org"))
                {
                    // Before redirecting to archive.org, make sure BHL doesn't have an updated version of the text
                    if (provider.ItemSelectHasNonOcrText(ItemType.Book, (int)itemid))
                    {
                        // Read from local BHL storage if the text includes non-OCR sources
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
                            itemText = client.GetItemText((int)itemid);
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
                    else
                    {
                        // No updated copy of the text, so redirect to remote storage at archive.org
                        return Redirect(itemtextPath);
                    }
                }
                else
                {
                    // Remote path is not archive.org, so redirect to it
                    return Redirect(itemtextPath);
                }
            }
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
                var filePath = provider.GetRemoteFilePath(RemoteFileType.ImageZip, item.BarCode, item.ImagesFilename);
                return Redirect(filePath);
            }
            else
            {
                return Redirect("~/pagenotfound");
            }
        }
    }
}