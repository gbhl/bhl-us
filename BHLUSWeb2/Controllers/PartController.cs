using BHL.SiteServiceREST.v1.Client;
using MOBOT.BHL.DataObjects;
using MOBOT.BHL.DataObjects.Enum;
using MOBOT.BHL.Server;
using MOBOT.BHL.Web.Utilities;
using MOBOT.BHL.Web2.Models;
using MvcThrottle;
using System;
using System.Collections.Generic;
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
        [HttpGet]
        public ActionResult Index(int partid)
        {
            BHLProvider bhlProvider = new BHLProvider();
            PartModel model = new PartModel();
            model.Segment = bhlProvider.SegmentSelectExtended(partid);
            if (model.Segment == null)
            {
                return Redirect("~/pagenotfound");
            }
            else
            {
                // Check to make sure this title hasn't been replaced.  If it has, redirect
                // to the appropriate titleid.
                if (model.Segment.RedirectSegmentID != null)
                {
                    return Redirect("~/part/" + model.Segment.RedirectSegmentID);
                }

                // Make sure the title is published.
                if (model.Segment.SegmentStatusID != (int)ItemStatus.ItemStatusValue.New &&
                    model.Segment.SegmentStatusID != (int)ItemStatus.ItemStatusValue.Published)
                {
                    return Redirect("~/itemunavailable");
                }
            }

            List<PageSummaryView> psv = bhlProvider.PageSummarySegmentSelectBySegmentID(partid);
            if (psv.Count > 0)
            {
                model.IsVirtual = psv[0].IsVirtual; model.Segment.BarCode = psv[0].BarCode;
            }
            else
            {
                model.HasLocalContent = 0;
            }

            model.Segment.IdentifierList = bhlProvider.ItemIdentifierSelectForDisplayBySegmentID(partid);
            InstitutionNameComparer comp = new InstitutionNameComparer();
            model.Segment.ContributorList.Sort(comp);

            // Get the rights holder of the container item
            if (model.Segment.BookID != null)
            {
                DataObjects.Book book = bhlProvider.BookSelectAuto((int)model.Segment.BookID);
                List<Institution> institutions = bhlProvider.InstitutionSelectByItemID(book.ItemID);
                foreach (Institution institution in institutions)
                {
                    if (institution.InstitutionRoleName == "Rights Holder") model.RightsHolder = institution;
                }
            }

            // Add Google Scholar metadata to the page headers
            model.GoogleScholarTags = bhlProvider.GetGoogleScholarMetadataForSegment(partid, ConfigurationManager.AppSettings["PartPageUrl"]);

            // Set the data for the COinS output
            model.COinS.SegmentID = partid;
            model.COinS.ItemIdentifiers = model.Segment.IdentifierList;
            model.COinS.ItemKeywords = model.Segment.KeywordList;
            model.COinS.ItemAuthors = model.Segment.AuthorList;
            model.COinS.Genre = model.Segment.GenreName;
            model.COinS.ArticleTitle = model.Segment.Title;
            model.COinS.Title = model.Segment.ContainerTitle;
            model.COinS.Volume = model.Segment.Volume;
            model.COinS.Issue = model.Segment.Issue;
            model.COinS.StartPageNumber = model.Segment.StartPageNumber;
            model.COinS.EndPageNumber = model.Segment.EndPageNumber;
            model.COinS.PageRange = model.Segment.PageRange;
            model.COinS.Language = model.Segment.LanguageCode;
            model.COinS.Date = model.Segment.Date;

            ViewBag.COinS = "<span class=\"Z3988\" title=\"" + model.COinS.GetCOinS() + "\"></span>";

            // Set the Schema.org itemtype
            switch (model.Segment.GenreName)
            {
                case "Book":
                case "Journal":
                    model.SchemaType = "https://schema.org/Book";
                    break;
                case "Article":
                case "Preprint":
                    model.SchemaType = "https://schema.org/ScholarlyArticle";
                    break;
                default: // BookItem, Chapter, Issue, Proceeding, Conference, Unknown, Treatment
                    model.SchemaType = "https://schema.org/CreativeWork";
                    break;
            }

            return View(model);
        }

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
                    if (stream != null) stream.Dispose();
                    ExceptionUtility.LogException(ex, "GetPartPDF.GetPregeneratedPdf");
                    return Redirect("~/error");
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