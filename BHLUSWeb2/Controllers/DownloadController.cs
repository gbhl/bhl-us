using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using MOBOT.BHL.Web.Utilities;
using MvcThrottle;
using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MOBOT.BHL.Web2.Controllers
{
    public class DownloadController : Controller
    {
        [EnableThrottling]
        public ActionResult BibTeX()
        {
            string idType = Request.RequestContext.RouteData.Values["type"] as string;
            string idString = Request.RequestContext.RouteData.Values["id"] as string;
            string tidString = Request.QueryString["t"] as string;  // Secondary ID containing TitleID associated with "id"

            if (!string.IsNullOrWhiteSpace(idString) && string.IsNullOrWhiteSpace(idType)) idType = "item";
            if (string.IsNullOrWhiteSpace(idString))
            {
                idString = Request.QueryString["tid"] as string;
                idType = "title";
            }
            if (string.IsNullOrWhiteSpace(idString))
            {
                idString = Request.QueryString["pid"] as string;
                idType = "part";
            }

            string response = string.Empty;
            string filename = "bhl";

            if (Int32.TryParse(idString, out int id))
            {
                try
                {
                    int? tid = int.TryParse(tidString, out int tmp) ? (int?)tmp : null;
                    filename += idType + idString;
                    switch (idType.ToLower())
                    {
                        case "title":
                            response = new BHLProvider().TitleBibTeXGetCitationStringForTitleID(id);
                            break;
                        case "part":
                            response = new BHLProvider().SegmentBibTeXGetCitationStringForSegmentID(id, tid, false);
                            break;
                        case "item":
                            response = new BHLProvider().BookBibTeXGetCitationStringForBookID(id, tid);
                            break;
                        case "page":
                            response = new BHLProvider().PageBibTeXGetCitationStringForPageID(id, tid);
                            break;
                    }
                }
                catch
                {
                    response = "Error retrieving BibTex citations for this " + idType + ".";
                }
            }

            ContentResult content = new ContentResult();
            content.Content = response;
            content.ContentType = "application/x-bibtex";
            Response.Headers.Add("Content-Disposition", "attachment; filename=" + filename + ".bib");
            return content;
        }

        [EnableThrottling]
        public ActionResult MODS()
        {
            int id;
            string idType = Request.RequestContext.RouteData.Values["type"] as string;
            string idString = Request.RequestContext.RouteData.Values["id"] as string;
            string tidString = Request.QueryString["t"] as string;  // Secondary ID containing TitleID associated with "id"

            if (!string.IsNullOrWhiteSpace(idString) && string.IsNullOrWhiteSpace(idType)) idType = "title";
            if (string.IsNullOrEmpty(idString))
            {
                idString = Request.QueryString["pid"] as string;
                idType = "part";
            }

            string response = string.Empty;
            string filename = "bhl";

            if (Int32.TryParse(idString, out id))
            {
                try
                {
                    filename += idType + idString;
                    OAI2.OAIRecord record = new OAI2.OAIRecord("oai:" + ConfigurationManager.AppSettings["OAIIdentifierNamespace"] + ":" + idType + "/" + id.ToString());
                    if (!string.IsNullOrWhiteSpace(tidString))
                    {
                        if (int.TryParse(tidString, out int tidInt))
                        {
                            Title title = new BHLProvider().TitleSelectAuto(tidInt);
                            if (idType == "item" && record.Title != title.FullTitle)
                            {
                                record.Title = title.FullTitle;
                                record.PartName = title.PartName;
                                record.PartNumber = title.PartNumber;
                                record.Publisher = title.Datafield_260_b;
                                record.PublicationPlace = title.Datafield_260_a;
                            }
                            if (idType == "part" && record.JournalTitle != title.FullTitle)
                            {
                                record.JournalTitle = title.FullTitle;
                                record.PartName = title.PartName;
                                record.PartNumber = title.PartNumber;
                                record.Publisher = title.Datafield_260_b;
                                record.PublicationPlace = title.Datafield_260_a;
                            }
                        }
                    }
                    OAIMODS.Convert mods = new OAIMODS.Convert(record);
                    response = mods.ToString();
                }
                catch
                {
                    response = "Error retrieving MODS for " + idType + ".";
                }
            }

            ContentResult content = new ContentResult();
            content.Content = response;
            content.ContentType = "application/xml";
            Response.Headers.Add("Content-Disposition", "attachment; filename=" + filename + "_mods.xml");
            return content;
        }

        [EnableThrottling]
        public ActionResult RIS()
        {
            string idType = Request.RequestContext.RouteData.Values["type"] as string;
            string idString = Request.RequestContext.RouteData.Values["id"] as string;
            string tidString = Request.QueryString["t"] as string;  // Secondary ID containing TitleID associated with "id"

            if (!string.IsNullOrWhiteSpace(idString) && string.IsNullOrWhiteSpace(idType)) idType = "item";
            if (string.IsNullOrWhiteSpace(idString))
            {
                idString = Request.QueryString["tid"] as string;
                idType = "title";
            }
            if (string.IsNullOrWhiteSpace(idString))
            {
                idString = Request.QueryString["pid"] as string;
                idType = "part";
            }

            string response = string.Empty;
            string filename = "bhl";

            if (Int32.TryParse(idString, out int id))
            {
                try
                {
                    int? tid = int.TryParse(tidString, out int tmp) ? (int?)tmp : null;
                    filename += idType + idString;
                    switch (idType.ToLower())
                    {
                        case "title":
                            response = new BHLProvider().ItemSelectRISCitationsForTitleID(id);
                            break;
                        case "part":
                            response = new BHLProvider().SegmentGetRISCitationStringForSegmentID(id, tid);
                            break;
                        case "item":
                            response = new BHLProvider().BookSelectRISCitationStringForBookID(id, tid);
                            break;
                        case "page":
                            response = new BHLProvider().PageSelectRISCitationStringForPageID(id, tid);
                            break;
                    }
                }
                catch
                {
                    response = "Error retrieving RIS citations for this " + idType + ".";
                }
            }

            ContentResult content = new ContentResult();
            content.Content = response;
            content.ContentType = "application/x-research-info-systems";
            Response.Headers.Add("Content-Disposition", "attachment; filename=" + filename + ".ris");
            return content;
        }

        [EnableThrottling]
        public ActionResult CSL()
        {
            int id;
            string idType = Request.RequestContext.RouteData.Values["type"] as string;
            string idString = Request.RequestContext.RouteData.Values["id"] as string;
            string tidString = Request.QueryString["t"] as string;  // Secondary ID containing TitleID associated with "id"

            string response = string.Empty;
            string filename = "bhl";
            if (Int32.TryParse(idString, out id))
            {
                try
                {
                    filename += idType + idString;
                    string idTypeArg = string.Empty;
                    var baseAddress = new Uri(string.Format("{0}{1}/",
                        Request.Url.GetLeftPart(UriPartial.Authority),
                        Request.ApplicationPath.TrimEnd('/')));

                    switch (idType.ToLower())
                    {
                        case "title":
                            idTypeArg = "t"; break;
                        case "item":
                            idTypeArg = "i"; break;
                        case "part":
                            idTypeArg = "s"; break;
                        case "page":
                            idTypeArg = "p"; break;
                    }

                    string path = string.Format("/service/GetCitationJSON?idType={0}&id1={1}&id2={2}", idTypeArg, idString, tidString);
                    response = new HttpClient().GetStringAsync(new Uri(baseAddress, path)).Result;
                }
                catch
                {
                    response = string.Format("{{ \"Error\" : \"Error retrieving CSL for {0}:{1}\" }}", idType, idString);
                }
            }

            ContentResult content = new ContentResult();
            content.Content = response;
            content.ContentType = "application/json";
            Response.Headers.Add("Content-Disposition", "attachment; filename=" + filename + "_csl.json");
            return content;
        }

        [EnableThrottling]
        public async Task<ActionResult> PDF()
        {
            string folder = Request.RequestContext.RouteData.Values["folder"] as string;
            string filename = Request.RequestContext.RouteData.Values["filename"] as string;
            string pdfPath = string.Format(ConfigurationManager.AppSettings["PdfUrl"], folder, filename);
            int pdfTimeout = 36000000;
            Server.ScriptTimeout = pdfTimeout / 1000;

            Stream stream = null;
            try
            {
                stream = this.GetPdfStream(pdfPath, pdfTimeout);
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
                    ExceptionUtility.LogException(wex, "DownloadController.PDF");
                    return Redirect("~/error");
                }
            }

            if (stream != null)
            {
                try
                {
                    Response.ContentType = "application/octet-stream";
                    Response.Headers.Add("content-disposition", "attachment;filename=" + filename);
                    Response.BufferOutput = false;

                    await stream.CopyToAsync(Response.OutputStream);
                    Response.Flush();
                    HttpContext.ApplicationInstance.CompleteRequest();
                }
                catch (Exception ex)
                {
                    ExceptionUtility.LogException(ex, "DownloadController.PDF");
                }
            }

            return new EmptyResult();
        }

        private Stream GetPdfStream(string url, int pdfTimeout)
        {
            Stream pdf = null;
            HttpWebResponse resp = this.HttpGet(url, pdfTimeout);
            if (resp != null) pdf = resp.GetResponseStream();
            return pdf;
        }

        private HttpWebResponse HttpGet(string url, int pdfTimeout)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "GET";
            // Set timeout values to prevent file truncation while downloading
            req.Timeout = pdfTimeout;
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            return resp;
        }
    }
}