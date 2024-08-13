using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using System.Collections.Generic;
using System.Web.Mvc;

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

        public ActionResult GetCitationJSON(string idType, int id)
        {
            Dictionary<string, object> cslData;

            switch (idType)
            {
                case "s":
                    cslData = GetCSLPartData(id);
                    break;
                case "p":
                    cslData = GetCSLPageData(id);
                    break;
                case "i":
                    cslData = GetCSLItemData(id);
                    break;
                case "t":
                default:
                    cslData = GetCSLTitleData(id);
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

        private Dictionary<string, object> GetCSLItemData(int id)
        {
            BHLProvider bhlProvider = new BHLProvider();

            // Get the title-level metadata
            PageSummaryView psv = bhlProvider.PageSummarySelectByItemId(id, true);
            Dictionary<string, object> cslData = GetCSLTitleData(psv.TitleID);

            // Add volume-specific metadata
            DataObjects.Book book = bhlProvider.BookSelectAuto(id);
            if (!string.IsNullOrWhiteSpace(book.StartVolume)) cslData.Add("volume", book.StartVolume);
            if (!string.IsNullOrWhiteSpace(book.StartIssue)) cslData.Add("issue", book.StartIssue);

            // Override some attributes with page-level values
            cslData["id"] = "BHL_Item_" + id.ToString() + "_Citation";
            cslData["citation-label"] = "BHL_Item_" + id.ToString() + "_Citation";
            cslData["URL"] = "https://www.biodiversitylibrary.org/item/" + id.ToString();
            if (cslData.ContainsKey("DOI")) cslData.Remove("DOI");

            return cslData;
        }

        private Dictionary<string, object> GetCSLPartData(int id)
        {
            Segment segment = new BHLProvider().SegmentSelectExtended(id);
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
            cslData.Add("container-title", segment.ContainerTitle);
            cslData.Add("type", cslType);
            cslData.Add("id", "BHL_Part_" + id.ToString() + "_Citation");
            cslData.Add("citation-label", "BHL_Part_" + id.ToString() + "_Citation");
            if (!string.IsNullOrWhiteSpace(segment.Date))
            {
                cslData.Add(
                    "issued",
                    new Dictionary<string, object> {
                                { "date-parts", new object[] { new string[] { segment.Date } } }
                    });
            }
            foreach (ItemIdentifier ii in segment.IdentifierList)
            {
                if (ii.IdentifierName == "ISSN" && !cslData.ContainsKey("ISSN")) cslData.Add("ISSN", ii.IdentifierValue);
                if (ii.IdentifierName == "ISBN" && !cslData.ContainsKey("ISBN")) cslData.Add("ISBN", ii.IdentifierValue);
                if (ii.IdentifierName == "DOI" && !cslData.ContainsKey("DOI")) cslData.Add("DOI", ii.IdentifierValue);
            }
            cslData.Add("title", segment.Title);
            cslData.Add("URL", "https://www.biodiversitylibrary.org/part/" + id.ToString());
            if (!string.IsNullOrWhiteSpace(segment.LanguageCode)) cslData.Add("language", segment.LanguageCode);
            if (!string.IsNullOrWhiteSpace(segment.Volume)) cslData.Add("volume", segment.Volume);
            if (!string.IsNullOrWhiteSpace(segment.Issue)) cslData.Add("issue", segment.Issue);
            if (!string.IsNullOrWhiteSpace(segment.PageRange)) cslData.Add("page", segment.PageRange);
            if (!string.IsNullOrWhiteSpace(segment.RightsStatus)) cslData.Add("license", segment.RightsStatus);
            if (!string.IsNullOrWhiteSpace(segment.PublisherName)) cslData.Add("publisher", segment.PublisherName);
            if (!string.IsNullOrWhiteSpace(segment.PublisherPlace)) cslData.Add("publisher-place", segment.PublisherPlace);

            return cslData;
        }

        private Dictionary<string, object> GetCSLPageData(int id)
        {
            // Get most of the metadata for the page from the related title
            BHLProvider bhlProvider = new BHLProvider();
            PageSummaryView psv = bhlProvider.PageSummarySelectByPageId(id);
            Dictionary<string, object> cslData = GetCSLTitleData(psv.TitleID);

            // Get volume-specific metadata for the page citation
            Page page = bhlProvider.PageMetadataSelectByPageID(id);
            if (!string.IsNullOrWhiteSpace(page.Volume)) cslData.Add("volume", page.Volume);
            if (!string.IsNullOrWhiteSpace(page.Issue)) cslData.Add("issue", page.Issue);

            // Override some attributes with page-level values
            cslData["id"] = "BHL_Page_" + id.ToString() + "_Citation";
            cslData["citation-label"] = "BHL_Page_" + id.ToString() + "_Citation";
            cslData["URL"] = "https://www.biodiversitylibrary.org/page/" + id.ToString();
            if (cslData.ContainsKey("DOI")) cslData.Remove("DOI");

            List<IndicatedPage> pageIndicators  = bhlProvider.IndicatedPageSelectByPageID(id);
            bool firstPageIndicator = true;
            foreach(IndicatedPage ip in pageIndicators)
            {
                if (ip.PagePrefix.ToLower() == "page")
                {
                    if (cslData.ContainsKey("page") && firstPageIndicator)
                    {
                        cslData["page"] = ip.PageNumber;
                        firstPageIndicator = !firstPageIndicator;
                    }
                    else if (!cslData.ContainsKey("page"))
                    {
                        cslData.Add("page", ip.PageNumber);
                    }
                }
            }

            return cslData;
        }
    }
}
