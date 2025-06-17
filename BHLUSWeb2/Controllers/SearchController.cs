using BHL.Search;
using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using MOBOT.BHL.Web2.Models;
using MvcThrottle;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MOBOT.BHL.Web2.Controllers
{
    public class SearchController : Controller
    {
        // GET: Index
        [EnableThrottling]
        [HttpGet]
        public ActionResult Index(string searchTerm, string tinc, string stype, string searchCat, string lname, string ninc,
            string yr, string subj, string sinc, string lang, string col, string nt, string ntinc, string txt, string txinc, 
            string ppage, string apage, string kpage, string npage, string psort, string[] facet)
        {
            // Prevent browser Back button page caching
            Response.Cache.SetCacheability(HttpCacheability.NoCache);  // HTTP 1.1
            Response.Cache.AppendCacheExtension("no-store, must-revalidate");
            Response.AppendHeader("Pragma", "no-cache"); // HTTP 1.0
            Response.AppendHeader("Expires", "0"); // Proxies

            SearchModel model = new SearchModel();
            model.Params.SearchTerm = searchTerm;

            // Get the search arguments
            model.Params.TermInclude = (tinc ?? string.Empty).Trim().ToUpper();
            model.Params.SearchCategory = (searchCat ?? string.Empty).Trim().ToUpper();
            model.Params.SearchType = (stype ?? string.Empty).Trim().ToUpper();
            model.Params.LastName = lname ?? string.Empty;
            model.Params.LastNameInclude = (ninc ?? string.Empty).Trim().ToUpper();
            model.Params.Year = yr ?? string.Empty;
            model.Params.Subject = subj ?? string.Empty;
            model.Params.SubjectInclude = (sinc ?? string.Empty).Trim().ToUpper();
            if (!string.IsNullOrWhiteSpace(lang))
            {
                if (lang.StartsWith("(") && lang.EndsWith(")"))
                {
                    if (lang.Length == 2) lang = string.Empty;
                    else lang = lang.Substring(1, lang.Length - 2);
                }

                string languageCode;
                string languageName = string.Empty;
                if (!lang.Contains(","))
                {
                    languageCode = lang;
                    Language language = new BHLProvider().LanguageSelectAuto(languageCode);
                    if (language != null) languageName = language.LanguageName;
                }
                else
                {
                    languageCode = lang.Substring(0, lang.IndexOf(',')).Trim();
                    languageName = lang.Substring(lang.IndexOf(',') + 1).Trim();
                }

                model.Params.Language = new Tuple<string, string>(languageCode, languageName);
            }
            if (!string.IsNullOrWhiteSpace(col))
            {
                if (col.StartsWith("(") && col.EndsWith(")"))
                {
                    if (col.Length == 2) col = string.Empty;
                    else col = col.Substring(1, col.Length - 2);
                }

                int collectionID;
                string collectionName = string.Empty;
                if (!col.Contains(","))
                {
                    collectionID = Convert.ToInt32(col);
                    Collection collection = new BHLProvider().CollectionSelectAuto(collectionID);
                    if (collection != null) collectionName = collection.CollectionName;
                }
                else
                {
                    collectionID = Convert.ToInt32(col.Substring(0, col.IndexOf(',')));
                    collectionName = col.Substring(col.IndexOf(',') + 1).Trim();
                }

                model.Params.Collection = new Tuple<string, string>(collectionID.ToString(), collectionName);
            }
            model.Params.Notes = nt ?? string.Empty;
            model.Params.NotesInclude = (ntinc ?? string.Empty).Trim().ToUpper();
            model.Params.Text = txt ?? string.Empty;
            model.Params.TextInclude = (txinc ?? string.Empty).Trim().ToUpper();
            if (!Int32.TryParse(ppage ?? "1", out int startPage)) startPage = 1;
            model.ItemPage = startPage;
            if (!Int32.TryParse(apage ?? "1", out startPage)) startPage = 1;
            model.AuthorPage = startPage;
            if (!Int32.TryParse(kpage ?? "1", out startPage)) startPage = 1;
            model.KeywordPage = startPage;
            if (!Int32.TryParse(npage ?? "1", out startPage)) startPage = 1;
            model.NamePage = startPage;
            model.ItemSort = psort ?? "rd"; // Default to Relevance Descending

            // For annotation searches, use the non-elasticsearch search page
            if (model.Params.SearchCategory == "O") return new RedirectResult("~/search.aspx?" + Request.QueryString);

            // Add facets to the search
            List<Tuple<SearchField, string>> limits = null;
            if (facet != null)
            {
                if (facet.Length > 0) limits = new List<Tuple<SearchField, string>>();
                foreach (var f in facet)
                {
                    limits.Add(
                        new Tuple<SearchField, string>(
                            (SearchField)Enum.Parse(typeof(SearchField), f.Split('_')[0], true),
                            f.Split('_')[1]));
                }
            }

            return SearchAction(model, limits);
        }

        // GET: Advanced
        public ActionResult Advanced()
        {
            MVCServices.SearchService searchService = new MVCServices.SearchService();
            ViewBag.Languages = searchService.LanguageList();
            ViewBag.Collections = searchService.CollectionList();
            return View();
        }

        // POST: Advanced
        [HttpPost]
        public ActionResult Advanced(string foo)
        {
            string queryString = string.Empty;

            if (!string.IsNullOrWhiteSpace(Request.Form["btnSearchTitle"]))
            {
                queryString = "SearchTerm=" + Server.UrlEncode(Request.Form["txtPubTitle"]) +
                    "&tinc=" + Server.UrlEncode(Request.Form["rdoTitleInclude"]) +
                    "&lname=" + Server.UrlEncode(Request.Form["txtPubAuthorLastName"]) +
                    "&yr=" + Server.UrlEncode(Request.Form["txtPubYear"]) +
                    "&subj=" + Server.UrlEncode(Request.Form["txtPubSubject"]) +
                    "&lang=" + Server.UrlEncode(Request.Form["ddlPubLanguage"]) +
                    "&col=" + Server.UrlEncode(Request.Form["ddlPubCollection"]) +
                    "&nt=" + Server.UrlEncode(Request.Form["txtPubNote"]) +
                    "&ntinc=" + Server.UrlEncode(Request.Form["rdoNoteInclude"]) +
                    "&txt=" + Server.UrlEncode(Request.Form["txtPubText"] ?? string.Empty) + 
                    "&txinc=" + Server.UrlEncode(Request.Form["rdoTextInclude"]) + 
                    "&SearchCat=T&stype=C&return=ADV";

            }

            if (!string.IsNullOrWhiteSpace(Request.Form["btnSearchAuthor"]))
            {
                queryString = "SearchTerm=" + Server.UrlEncode(Request.Form["txtAuthorName"]) + " &SearchCat=A&return=ADV";
            }

            if (!string.IsNullOrWhiteSpace(Request.Form["btnSearchSubject"]))
            {
                queryString = "SearchTerm=" + Server.UrlEncode(Request.Form["txtSubject"]) + "&SearchCat=S&return=ADV";
            }

            if (!string.IsNullOrWhiteSpace(Request.Form["btnSearchName"]))
            {
                queryString = "SearchTerm=" + Server.UrlEncode(Request.Form["txtName"]) + "&SearchCat=M&return=ADV";
            }

            return new RedirectResult("~/search?" + queryString);
        }

        [HttpGet]
        public ActionResult Pages(string q, int itemId)
        {
            ISearch search = new SearchFactory().GetSearch(ConfigurationManager.AppSettings["SearchProviders"]);
            search.NumResults = Convert.ToInt32(ConfigurationManager.AppSettings["PageResultPageSize"]); ;
            search.Highlight = true;
            search.Suggest = false;
            search.SortField = (SortField)Enum.Parse(typeof(SortField), ConfigurationManager.AppSettings["PageResultDefaultSort"]);
            search.SortDirection = (SortDirection)Enum.Parse(typeof(SortDirection), ConfigurationManager.AppSettings["PageResultDefaultSortDirection"]);
            List<Tuple<SearchField, string>> limits = new List<Tuple<SearchField, string>>();
            Tuple<SearchField, string> itemLimit = new Tuple<SearchField, string>(SearchField.ItemID, itemId.ToString());
            limits.Add(itemLimit);
            List<IHit> pages = search.SearchPage(q ?? "", limits).Pages;
            pages = CleanPageResults(pages);

            return Json(pages, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Clean up the page results by constructing proper page descriptions
        /// </summary>
        /// <param name="pages"></param>
        /// <returns></returns>
        private List<IHit> CleanPageResults(List<IHit> pages)
        {
            foreach(IHit page in pages)
            {
                string pageDescription;
                string pageIndicators = string.Empty;
                string pageTypes = string.Empty;
                foreach(string indicator in ((PageHit)page).PageIndicators)
                {
                    if (!string.IsNullOrWhiteSpace(pageIndicators)) pageIndicators += ",";
                    pageIndicators += indicator;
                }
                foreach(string type in ((PageHit)page).PageTypes)
                {
                    if (!string.IsNullOrWhiteSpace(pageTypes)) pageTypes += ",";
                    pageTypes += type;
                }

                if (!string.IsNullOrWhiteSpace(pageIndicators) && !string.IsNullOrWhiteSpace(pageTypes))
                {
                    pageDescription = string.Format("{0} ({1})", pageIndicators, pageTypes);
                }
                else if(!string.IsNullOrWhiteSpace(pageIndicators))
                {
                    pageDescription = pageIndicators;
                }
                else
                {
                    pageDescription = pageTypes;
                }

                ((PageHit)page).PageDescription = pageDescription;
            }

            return pages;
        }

        public ActionResult SearchAction(SearchModel model, List<Tuple<SearchField, string>> limits)
        {
            int publicationPageSize = Convert.ToInt32(ConfigurationManager.AppSettings["PublicationResultPageSize"]);
            int authorPageSize = Convert.ToInt32(ConfigurationManager.AppSettings["AuthorResultPageSize"]);
            int keywordPageSize = Convert.ToInt32(ConfigurationManager.AppSettings["KeywordResultPageSize"]);
            int namePageSize = Convert.ToInt32(ConfigurationManager.AppSettings["NameResultPageSize"]);
            int numFacets = Convert.ToInt32(ConfigurationManager.AppSettings["FacetSize"]);

            // Submit the request to ElasticSearch here
            ISearch search = new SearchFactory().GetSearch(ConfigurationManager.AppSettings["SearchProviders"]);
            search.Highlight = true;
            search.Suggest = true;
            string searchTerm = model.Params.SearchTerm ?? "";

            if (string.IsNullOrWhiteSpace(model.Params.SearchCategory))
            {
                // Standard search
                search.StartPage = model.AuthorPage;
                search.NumResults = authorPageSize;
                search.SortField = (SortField)Enum.Parse(typeof(SortField), ConfigurationManager.AppSettings["AuthorResultDefaultSort"]);
                search.SortDirection = (SortDirection)Enum.Parse(typeof(SortDirection), ConfigurationManager.AppSettings["AuthorResultDefaultSortDirection"]);
                model.AuthorResult = search.SearchAuthor(searchTerm);
                search.StartPage = model.KeywordPage;
                search.NumResults = keywordPageSize;
                search.SortField = (SortField)Enum.Parse(typeof(SortField), ConfigurationManager.AppSettings["KeywordResultDefaultSort"]);
                search.SortDirection = (SortDirection)Enum.Parse(typeof(SortDirection), ConfigurationManager.AppSettings["KeywordResultDefaultSortDirection"]);
                model.KeywordResult = search.SearchKeyword(searchTerm);
                search.StartPage = model.NamePage;
                search.NumResults = namePageSize;
                search.SortField = (SortField)Enum.Parse(typeof(SortField), ConfigurationManager.AppSettings["NameResultDefaultSort"]);
                search.SortDirection = (SortDirection)Enum.Parse(typeof(SortDirection), ConfigurationManager.AppSettings["NameResultDefaultSortDirection"]);
                model.NameResult = search.SearchName(searchTerm);
                search.Facet = true;
                search.StartPage = model.ItemPage;
                search.NumResults = publicationPageSize;
                search.NumFacets = numFacets;

                if (model.ItemSort[0] == 't')
                {
                    search.SortField = SortField.Title;
                    search.SortDirection = (model.ItemSort[1] == 'd' ? SortDirection.Descending : SortDirection.Ascending);
                }
                else if (model.ItemSort[0] == 'd')
                {
                    search.SortField = SortField.Date;
                    search.SortDirection = (model.ItemSort[1] == 'd' ? SortDirection.Descending : SortDirection.Ascending);
                }
                else
                {
                    search.SortField = (SortField)Enum.Parse(typeof(SortField), ConfigurationManager.AppSettings["PublicationResultDefaultSort"]); // Score
                    search.SortDirection = (SortDirection)Enum.Parse(typeof(SortDirection), ConfigurationManager.AppSettings["PublicationResultDefaultSortDirection"]);
                }

                if (model.Params.SearchType == "F")
                {
                    // Full-text search
                    model.ItemResult = search.SearchItem(searchTerm, limits);
                }
                else
                {
                    // Catalog search
                    model.ItemResult = search.SearchCatalog(searchTerm, limits);
                }
            }
            else
            {
                // Advanced search
                if (model.Params.SearchCategory.Equals("A"))
                {
                    search.StartPage = model.AuthorPage;
                    search.NumResults = authorPageSize;
                    search.SortField = (SortField)Enum.Parse(typeof(SortField), ConfigurationManager.AppSettings["AuthorResultDefaultSort"]);
                    search.SortDirection = (SortDirection)Enum.Parse(typeof(SortDirection), ConfigurationManager.AppSettings["AuthorResultDefaultSortDirection"]);
                    model.AuthorResult = search.SearchAuthor(searchTerm);
                }
                if ((model.Params.SearchCategory.Equals("N") || model.Params.SearchCategory.Equals("M")))
                {
                    search.StartPage = model.NamePage;
                    search.NumResults = namePageSize;
                    search.SortField = (SortField)Enum.Parse(typeof(SortField), ConfigurationManager.AppSettings["NameResultDefaultSort"]);
                    search.SortDirection = (SortDirection)Enum.Parse(typeof(SortDirection), ConfigurationManager.AppSettings["NameResultDefaultSortDirection"]);
                    model.NameResult = search.SearchName(searchTerm);
                }
                if (model.Params.SearchCategory.Equals("T"))
                {
                    search.StartPage = model.ItemPage;
                    search.NumResults = publicationPageSize;
                    search.NumFacets = numFacets;

                    if (model.ItemSort[0] == 't')
                    {
                        search.SortField = SortField.Title;
                        search.SortDirection = (model.ItemSort[1] == 'd' ? SortDirection.Descending : SortDirection.Ascending);
                    }
                    else if (model.ItemSort[0] == 'd')
                    {
                        search.SortField = SortField.Date;
                        search.SortDirection = (model.ItemSort[1] == 'd' ? SortDirection.Descending : SortDirection.Ascending);
                    }
                    else
                    {
                        search.SortField = (SortField)Enum.Parse(typeof(SortField), ConfigurationManager.AppSettings["PublicationResultDefaultSort"]); // Score
                        search.SortDirection = (SortDirection)Enum.Parse(typeof(SortDirection), ConfigurationManager.AppSettings["PublicationResultDefaultSortDirection"]);
                    }

                    search.Facet = true;
                    model.ItemResult = search.SearchCatalog(
                        new SearchStringParam(searchTerm, GetParamOperator(model.Params.TermInclude)),
                        new SearchStringParam(model.Params.LastName, GetParamOperator(model.Params.LastNameInclude)),
                        model.Params.Volume, model.Params.Year, 
                        new SearchStringParam(model.Params.Subject, GetParamOperator(model.Params.SubjectInclude)), 
                        model.Params.Language, model.Params.Collection, 
                        new SearchStringParam(model.Params.Notes, GetParamOperator(model.Params.NotesInclude)),
                        new SearchStringParam(model.Params.Text, GetParamOperator(model.Params.TextInclude)), limits);
                }
                if (model.Params.SearchCategory.Equals("S"))
                {
                    search.StartPage = model.KeywordPage;
                    search.NumResults = keywordPageSize;
                    search.SortField = (SortField)Enum.Parse(typeof(SortField), ConfigurationManager.AppSettings["KeywordResultDefaultSort"]);
                    search.SortDirection = (SortDirection)Enum.Parse(typeof(SortDirection), ConfigurationManager.AppSettings["KeywordResultDefaultSortDirection"]);
                    model.KeywordResult = search.SearchKeyword(searchTerm);
                }
            }

            if (search.Facet) AddFacetsToModel(model, limits);

            return View(model);
        }

        private SearchStringParamOperator GetParamOperator(string op)
        {
            SearchStringParamOperator paramOperator;

            switch (op)
            {
                case "P": paramOperator = SearchStringParamOperator.Phrase; break;
                case "O": paramOperator = SearchStringParamOperator.Or; break;
                case "A":
                default: paramOperator = SearchStringParamOperator.And; break;
            }

            return paramOperator;
        }

        /// <summary>
        /// Parse the facets from the search results into bindable model fields
        /// </summary>
        /// <param name="model"></param>
        private void AddFacetsToModel(SearchModel model, List<Tuple<SearchField, string>> limits)
        {
            // https://stackoverflow.com/questions/31343732/mvc-model-with-a-list-of-objects-as-property
            foreach (var facet in model.ItemResult.Facets)
            {
                switch (facet.Key)
                {
                    case SearchField.Genre:
                        foreach (var facetValue in facet.Value)
                        {
                            model.Params.GenreFacets.Add(new FacetParam(facet.Key.ToString(), facetValue.Key, facetValue.Value, IsFacetLimited(facet.Key, facetValue.Key, limits)));
                        }
                        break;
                    case SearchField.MaterialType:
                        foreach (var facetValue in facet.Value)
                        {
                            model.Params.MaterialTypeFacets.Add(new FacetParam(facet.Key.ToString(), facetValue.Key, facetValue.Value, IsFacetLimited(facet.Key, facetValue.Key, limits)));
                        }
                        break;
                    case SearchField.FacetItemAuthors:
                        foreach (var facetValue in facet.Value)
                        {
                            model.Params.AuthorFacets.Add(new FacetParam(facet.Key.ToString(), facetValue.Key, facetValue.Value, IsFacetLimited(facet.Key, facetValue.Key, limits)));
                        }
                        break;
                    case SearchField.DateRanges:
                        foreach (var facetValue in facet.Value)
                        {
                            model.Params.DateRangeFacets.Add(new FacetParam(facet.Key.ToString(), facetValue.Key, facetValue.Value, IsFacetLimited(facet.Key, facetValue.Key, limits)));
                        }
                        break;
                    case SearchField.Contributors:
                        foreach (var facetValue in facet.Value)
                        {
                            model.Params.ContributorFacets.Add(new FacetParam(facet.Key.ToString(), facetValue.Key, facetValue.Value, IsFacetLimited(facet.Key, facetValue.Key, limits)));
                        }
                        break;
                    case SearchField.FacetItemKeywords:
                        foreach (var facetValue in facet.Value)
                        {
                            model.Params.KeywordFacets.Add(new FacetParam(facet.Key.ToString(), facetValue.Key, facetValue.Value, IsFacetLimited(facet.Key, facetValue.Key, limits)));
                        }
                        break;
                    case SearchField.Language:
                        foreach (var facetValue in facet.Value)
                        {
                            model.Params.LanguageFacets.Add(new FacetParam(facet.Key.ToString(), facetValue.Key, facetValue.Value, IsFacetLimited(facet.Key, facetValue.Key, limits)));
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// Determine if the SearchField and value combination is included in the list of limits
        /// </summary>
        /// <param name="facet"></param>
        /// <param name="value"></param>
        /// <param name="limits"></param>
        /// <returns></returns>
        private bool IsFacetLimited(SearchField facet, string value, List<Tuple<SearchField, string>> limits)
        {
            bool isLimited = false;

            if (limits != null)
            {
                foreach (var limit in limits)
                {
                    if (limit.Item1 == facet && limit.Item2 == value)
                    {
                        isLimited = true;
                        break;
                    }
                }
            }

            return isLimited;
        }
    }
}
