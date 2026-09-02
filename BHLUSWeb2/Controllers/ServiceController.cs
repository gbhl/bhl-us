using Countersoft.Gemini.Commons.Entity.SLA;
using MOBOT.BHL.DataObjects;
using MOBOT.BHL.OAI2;
using MOBOT.BHL.Server;
using MOBOT.BHL.Web.Utilities;
using MvcThrottle;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace MOBOT.BHL.Web2.Controllers
{
    public class ServiceController : Controller
    {
        // GET: Service
        public ActionResult GetNameDataSources(string name)
        {
            List<GNVerifierResponse> nameSources = new BHLProvider().GetNameDetailFromGNVerifier(name);

            return Json(nameSources, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Get the CSL citation (json) for the specified identifiers
        /// </summary>
        /// <param name="idType">Type of id1: s=segment, p=page, i=item, t=title</param>
        /// <param name="id1">Primary identifier of the entity being cited</param>
        /// <param name="id2">Title identifier related to the entity being cited.  Needed when entity is related to more than 1 title.</param>
        /// <returns></returns>
        public ActionResult GetCitationJSON(string idType, int id1, int? id2)
        {
            Dictionary<string, object> cslData;

            switch (idType)
            {
                case "s":
                    cslData = GetCSLPartData(id1, id2);
                    break;
                case "p":
                    cslData = GetCSLPageData(id1, id2);
                    break;
                case "i":
                    cslData = GetCSLItemData(id1, id2);
                    break;
                case "t":
                default:
                    cslData = GetCSLTitleData(id1);
                    break;
            }

            return Json(cslData, "application/json", JsonRequestBehavior.AllowGet);
        }

        private Dictionary<string, object> GetCSLTitleData(int id)
        {
            Title title = new BHLProvider().TitleSelectExtended(id);
            List<object> authors = new List<object>();

            foreach (TitleAuthor a in title.TitleAuthors)
            {
                string[] authorName = a.FullName.Split(',');
                if (a.AuthorRoleID == 1 || a.AuthorRoleID == 4)
                {
                    Dictionary<string, object> author = new Dictionary<string, object>();
                    if (authorName.Length > 1) author.Add("given", authorName[1]);
                    author.Add("family", authorName[0]);
                    authors.Add(author);
                }
                else
                {
                    authors.Add(new Dictionary<string, object>() { { "literal", a.FullName } });
                }
            }

            Dictionary<string, object> cslData = new Dictionary<string, object>();
            if (authors.Count > 0) cslData.Add("author", authors.ToArray());
            cslData.Add("type", "book");
            cslData.Add("id", "BHL_Title_" + id.ToString() + "_Citation");
            cslData.Add("citation-label", "BHL_Title_" + id.ToString() + "_Citation");
            if (title.StartYear != null)
            {
                cslData.Add(
                    "issued",
                    new Dictionary<string, object> {
                                { "date-parts", new object[] { new short?[] { title.StartYear } } }
                    });
            }
            foreach (Title_Identifier ti in title.TitleIdentifiers)
            {
                if (ti.IdentifierName == "ISSN" && !cslData.ContainsKey("ISSN")) cslData.Add("ISSN", ti.IdentifierValue);                    
                if (ti.IdentifierName == "ISBN" && !cslData.ContainsKey("ISBN")) cslData.Add("ISBN", ti.IdentifierValue);
                if (ti.IdentifierName == "DOI" && !cslData.ContainsKey("DOI")) cslData.Add("DOI", ti.IdentifierValue);
            }
            cslData.Add("title", title.FullTitle);
            cslData.Add("URL", "https://www.biodiversitylibrary.org/bibliography/" + id.ToString());
            if (!string.IsNullOrWhiteSpace(title.LanguageCode)) cslData.Add("language", title.LanguageCode);
            if (!string.IsNullOrWhiteSpace(title.CallNumber)) cslData.Add("call-number", title.CallNumber);
            if (!string.IsNullOrWhiteSpace(title.Datafield_260_b)) cslData.Add("publisher", title.Datafield_260_b);
            if (!string.IsNullOrWhiteSpace(title.Datafield_260_a)) cslData.Add("publisher-place", title.Datafield_260_a);

            return cslData;
        }

        private Dictionary<string, object> GetCSLItemData(int id1, int? id2)
        {
            BHLProvider bhlProvider = new BHLProvider();

            // Get the title-level metadata
            PageSummaryView psv = bhlProvider.PageSummarySelectByItemId(id1, id2);
            Dictionary<string, object> cslData = GetCSLTitleData(psv.TitleID);

            // Add volume-specific metadata
            DataObjects.Book book = bhlProvider.BookSelectAuto(id1);
            if (!string.IsNullOrWhiteSpace(book.StartVolume)) cslData.Add("volume", book.StartVolume);
            if (!string.IsNullOrWhiteSpace(book.StartIssue)) cslData.Add("issue", book.StartIssue);

            // Override some attributes with page-level values
            cslData["id"] = "BHL_Item_" + id1.ToString() + "_Citation";
            cslData["citation-label"] = "BHL_Item_" + id1.ToString() + "_Citation";
            cslData["URL"] = "https://www.biodiversitylibrary.org/item/" + id1.ToString() + (id2 == null ? "" : "?t=" + id2.ToString());
            if (cslData.ContainsKey("DOI")) cslData.Remove("DOI");

            return cslData;
        }

        private Dictionary<string, object> GetCSLPartData(int id1, int? id2)
        {
            Segment segment = new BHLProvider().SegmentSelectExtended(id1);
            Title title = (id2 == null) ? null : new BHLProvider().TitleSelectAuto((int)id2);

            List<object> authors = new List<object>();

            foreach (ItemAuthor a in segment.AuthorList)
            {
                string[] authorName = a.FullName.Split(',');
                Dictionary<string, object> author = new Dictionary<string, object>();
                if (authorName.Length > 1) author.Add("given", authorName[1]);
                author.Add("family", authorName[0]);
                authors.Add(author);
            }

            string cslType;
            switch (segment.GenreName.ToLower())
            {
                case "book":
                case "issue":
                    cslType = "book";
                    break;
                case "chapter":
                    cslType = "chapter";
                    break;
                case "conference":
                case "proceeding":
                    cslType = "paper-conference";
                    break;
                case "correspondence":
                    cslType = "personal_communication";
                    break;
                case "manuscript":
                    cslType = "manuscript";
                    break;
                case "thesis":
                    cslType = "thesis";
                    break;
                case "article":
                case "list":
                case "notes":
                case "preprint":
                case "review":
                case "treatment":
                case "unknown":
                default:
                    cslType = "article-journal";
                    break;
            }

            Dictionary<string, object> cslData = new Dictionary<string, object>();
            if (authors.Count > 0) cslData.Add("author", authors.ToArray());
            cslData.Add("container-title", (title == null ? segment.ContainerTitle : title.FullTitle));
            cslData.Add("type", cslType);
            cslData.Add("id", "BHL_Part_" + id1.ToString() + "_Citation");
            cslData.Add("citation-label", "BHL_Part_" + id1.ToString() + "_Citation");
            if (!string.IsNullOrWhiteSpace(segment.Date))
            {
                cslData.Add(
                    "issued",
                    new Dictionary<string, object> {
                                { "date-parts", new object[] { new string[] { segment.Date } } }
                    });
            }
            string eLocator = string.Empty;
            foreach (ItemIdentifier ii in segment.IdentifierList)
            {
                if (ii.IdentifierName == "ISSN" && !cslData.ContainsKey("ISSN")) cslData.Add("ISSN", ii.IdentifierValue);
                if (ii.IdentifierName == "ISBN" && !cslData.ContainsKey("ISBN")) cslData.Add("ISBN", ii.IdentifierValue);
                if (ii.IdentifierName == "DOI" && !cslData.ContainsKey("DOI")) cslData.Add("DOI", ii.IdentifierValue);
                if (ii.IdentifierName == "eLocator") eLocator = ii.IdentifierValue;
            }
            cslData.Add("title", segment.Title);
            cslData.Add("URL", "https://www.biodiversitylibrary.org/part/" + id1.ToString());
            if (!string.IsNullOrWhiteSpace(segment.LanguageCode)) cslData.Add("language", segment.LanguageCode);
            if (!string.IsNullOrWhiteSpace(segment.Volume)) cslData.Add("volume", segment.Volume);
            if (!string.IsNullOrWhiteSpace(segment.Issue)) cslData.Add("issue", segment.Issue);
            // If an eLocator exists, substitute it for the page number
            if (!string.IsNullOrWhiteSpace(segment.PageRange)) cslData.Add("page", (string.IsNullOrWhiteSpace(eLocator) ? segment.PageRange : eLocator));
            if (!string.IsNullOrWhiteSpace(segment.RightsStatus)) cslData.Add("license", segment.RightsStatus);
            if (!string.IsNullOrWhiteSpace(segment.PublisherName)) cslData.Add("publisher", segment.PublisherName);
            if (!string.IsNullOrWhiteSpace(segment.PublisherPlace)) cslData.Add("publisher-place", segment.PublisherPlace);

            return cslData;
        }

        private Dictionary<string, object> GetCSLPageData(int id1, int? id2)
        {
            // Get most of the metadata for the page from the related title
            BHLProvider bhlProvider = new BHLProvider();
            PageSummaryView psv;
            psv = bhlProvider.PageSummarySelectByPageId(id1, id2);
            if (psv == null) psv = bhlProvider.PageSummarySegmentSelectByPageID(id1, id2);
            Dictionary<string, object> cslData = GetCSLTitleData(psv.TitleID);

            // Get volume-specific metadata for the page citation
            Page page = bhlProvider.PageMetadataSelectByPageID(id1);
            if (!string.IsNullOrWhiteSpace(page.Volume)) cslData.Add("volume", page.Volume);
            if (!string.IsNullOrWhiteSpace(page.Issue)) cslData.Add("issue", page.Issue);

            // Override some attributes with page-level values
            cslData["id"] = "BHL_Page_" + id1.ToString() + "_Citation";
            cslData["citation-label"] = "BHL_Page_" + id1.ToString() + "_Citation";
            cslData["URL"] = "https://www.biodiversitylibrary.org/page/" + id1.ToString() + (id2 == null ? "" : "?t=" + id2.ToString());
            if (cslData.ContainsKey("DOI")) cslData.Remove("DOI");

            List<IndicatedPage> pageIndicators  = bhlProvider.IndicatedPageSelectByPageID(id1);

            // Select first page indicator with a prefix of "Page".  If no match, use first indicator with no prefix.
            var indicator = pageIndicators.FirstOrDefault(i => i.PagePrefix.ToLower() == "page");
            if (indicator != null)
            {
                cslData.Add("page", indicator.PageNumber);
            }
            else {
                indicator = pageIndicators.FirstOrDefault(i => i.PagePrefix == "");
                if (indicator != null) cslData.Add("page", indicator.PageNumber);
            }

            return cslData;
        }

        [EnableThrottling]
        public ActionResult OAIResolver()
        {
            OAI2Publisher oai = new OAI2Publisher(
                ConfigurationManager.AppSettings["OAIBaseUrl"],
                ConfigurationManager.AppSettings["OAIRepositoryName"],
                ConfigurationManager.AppSettings["OAIAdminEmail"],
                ConfigurationManager.AppSettings["OAIIdentifierNamespace"],
                ConfigurationManager.AppSettings["OAIMetadataFormats"],
                ConfigurationManager.AppSettings["OAIMaxListSets"],
                ConfigurationManager.AppSettings["OAIMaxListIdentifiers"],
                ConfigurationManager.AppSettings["OAIMaxListRecords"]
                );

            Response.ContentType = "text/xml";
            Response.AddHeader("pragma", "no-cache");
            Response.AddHeader("cache-control", "private");

            return Content(oai.Request(Request.QueryString), "text/xml");
        }

        public ActionResult GeneratePDF()
        {
            bool isSuccess = false;
            PDF pdf = null;
            int parsedId;
            if (int.TryParse(Request["itemId"], out parsedId))
            {
                int itemId = int.Parse(Request["itemId"]);
                List<int> pageIds = Request["pages"].Split(',').Select(x => int.Parse(x)).ToList();
                string email = Request["email"];
                string title = Request["title"] ?? string.Empty;
                string authors = Request["authors"] ?? string.Empty;
                string subjects = Request["subjects"] ?? string.Empty;
                bool imagesOnly = Request["imagesOnly"] != null;

                BHLProvider bhlProvider = new BHLProvider();

                try
                {
                    if (pageIds.Count > 0 && !string.IsNullOrWhiteSpace(email) &&
                        (string.IsNullOrWhiteSpace(title + authors + subjects) || (!string.IsNullOrWhiteSpace(title))))
                    {
                        pdf = bhlProvider.AddNewPdf(itemId, email, string.Empty, imagesOnly, title, authors, subjects, pageIds);
                        isSuccess = true;
                    }
                }
                catch (Exception ex)
                {
                    ExceptionUtility.LogException(ex, "ServiceController.GeneratePDF");
                }
            }

            Dictionary<string, object> responseData = new Dictionary<string, object>();
            responseData.Add("isSuccess", isSuccess);
            responseData.Add("pdfId", (pdf != null) ? pdf.PdfID : 0);
            return Json(responseData, "application/json", JsonRequestBehavior.AllowGet);
        }

        public ActionResult PageSummary()
        {
            object response;

            // Clean up inputs
            string pageID = Request.QueryString["pageID"] as string;
            pageID = string.IsNullOrEmpty(pageID) ? "0" : pageID;

            switch (Request.QueryString["op"])
            {
                case "GetPageNameList":
                    {
                        response = this.GetPageNameList(Convert.ToInt32(pageID));
                        break;
                    }
                case "GetPageOcrText":
                    {
                        response = GetPageOcrText(Convert.ToInt32(pageID));
                        break;
                    }
                default:
                    {
                        response = null;
                        break;
                    }
            }

            //Response.ContentType = "application/json";
            //Response.Write(response);
            return Json(response, "application/json", JsonRequestBehavior.AllowGet);
        }

        private Dictionary<string, object> GetPageOcrText(int pageID)
        {
            Dictionary<string, object> responseData = new Dictionary<string, object>();
            //JavaScriptSerializer js = new JavaScriptSerializer();
            try
            {
                string ocrText;

                using (WebClient client = new WebClient())
                {
                    client.Encoding = System.Text.Encoding.UTF8;
                    // Set a user-agent header to avoid 403 errors
                    client.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36");
                    string textUrl = string.Format("{0}/pagetext/{1}", ConfigurationManager.AppSettings["BaseUrl"], pageID);
                    ocrText = Server.HtmlEncode(client.DownloadString(textUrl));
                }

                if (string.IsNullOrWhiteSpace(ocrText))
                {
                    responseData.Add("ocrText", "Text unavailable for this page.");
                    responseData.Add("success", false);
                    //js.Serialize(new { ocrText = "Text unavailable for this page.", success = false });
                }
                else
                {
                    responseData.Add("ocrText", ocrText);
                    responseData.Add("success", true);
                }

                return responseData;
            }
            catch (Exception ex)
            {
                if (HttpContext.IsDebuggingEnabled) ExceptionUtility.LogException(ex, "ServiceController.GetPageOcrText)");
                //return js.Serialize(new { ocrText = "Text unavailable for this page.", success = false });
                responseData.Add("ocrText", "Text unavailable for this page.");
                responseData.Add("success", false);
                return responseData;
            }
        }

        private List<NameResolved> GetPageNameList(int pageID)
        {
            List<NameResolved> namePageList = new BHLProvider().NameResolvedSelectByPageID(pageID);
            List<NameResolved> returnList = new List<NameResolved>();
            foreach (NameResolved namePage in namePageList)
            {
                if (!string.IsNullOrEmpty(namePage.ResolvedNameString))
                {
                    namePage.UrlName = namePage.ResolvedNameString.Replace(' ', '_').Replace('.', '$').Replace('?', '^').Replace('&', '~');
                    returnList.Add(namePage);
                }
            }

            //JavaScriptSerializer js = new JavaScriptSerializer();
            //return js.Serialize(returnList);
            return returnList;
        }
    }
}
