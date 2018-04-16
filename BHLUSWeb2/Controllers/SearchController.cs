using BHL.Search;
using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using MOBOT.BHL.Web2.Models;
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
        [HttpGet]
        public ActionResult Index(string searchTerm, string searchCat, string lname, string vol, string yr, 
            string subj, string lang, string col, string ppage, string apage, string kpage, string npage, 
            string[] facet)
        {
            // Prevent browser Back button page caching
            Response.Cache.SetCacheability(HttpCacheability.NoCache);  // HTTP 1.1
            Response.Cache.AppendCacheExtension("no-store, must-revalidate");
            Response.AppendHeader("Pragma", "no-cache"); // HTTP 1.0
            Response.AppendHeader("Expires", "0"); // Proxies

            SearchModel model = new SearchModel();
            model.Params.SearchTerm = searchTerm;

            // Get the search arguments
            model.Params.SearchCategory = (searchCat ?? string.Empty).Trim().ToUpper();
            model.Params.LastName = lname ?? string.Empty;
            model.Params.Volume = vol ?? string.Empty; ;
            model.Params.Year = yr ?? string.Empty; ;
            model.Params.Subject = subj ?? string.Empty; ;
            if (!string.IsNullOrWhiteSpace(lang))
            {
                string[] langParts = lang.Split('|');
                string languageName = string.Empty;

                // Get the language name
                if (langParts.Length == 1)
                {
                    Language language = new BHLProvider().LanguageSelectAuto(langParts[0]);
                    if (language != null) languageName = language.LanguageName;
                }
                else
                {
                    languageName = langParts[1];
                }

                model.Params.Language = new Tuple<string, string>(langParts[0], languageName);
            }
            if (!string.IsNullOrWhiteSpace(col))
            {
                string[] colParts = col.Split('|');
                string collectionName = string.Empty;

                // Get the collection name
                if (colParts.Length == 1)
                {
                    Collection collection = new BHLProvider().CollectionSelectAuto(Convert.ToInt32(colParts[0]));
                    if (collection != null) collectionName = collection.CollectionName;
                }
                else
                {
                    collectionName = colParts[1];
                }

                model.Params.Collection = new Tuple<string, string>(colParts[0], collectionName);
            }
            int startPage;
            if (!Int32.TryParse(ppage ?? "1", out startPage)) startPage = 1;
            model.ItemPage = startPage;
            if (!Int32.TryParse(apage ?? "1", out startPage)) startPage = 1;
            model.AuthorPage = startPage;
            if (!Int32.TryParse(kpage ?? "1", out startPage)) startPage = 1;
            model.KeywordPage = startPage;
            if (!Int32.TryParse(npage ?? "1", out startPage)) startPage = 1;
            model.NamePage = startPage;

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

        //[HttpPost]
        //public ActionResult Index(SearchModel model)
        //{
        //    // Convert the selected facets to limits for search
        //    List<Tuple<SearchField, string>> limits = null;
        //    string selectedFacets = Request["facet.Checked"];
        //    if (selectedFacets != null)
        //    {
        //        List<string> facets = selectedFacets.Replace("false", "").Split(new char[] { '~' }, StringSplitOptions.RemoveEmptyEntries).ToList();
        //        for (int x = facets.Count - 1; x >= 0; x--)
        //        {
        //            if (facets[x].StartsWith(",")) facets.RemoveAt(x);
        //        }

        //        if (facets.Count > 0) limits = new List<Tuple<SearchField, string>>();
        //        foreach (var facet in facets)
        //        {
        //            limits.Add(
        //                new Tuple<SearchField, string>(
        //                    (SearchField)Enum.Parse(typeof(SearchField), facet.Split('_')[0], true),
        //                    facet.Split('_')[1]));
        //        }
        //    }

        //    return SearchAction(model, limits);
        //}

        // GET: Advanced
        public ActionResult Advanced()
        {
            if (SearchRedirect()) return new RedirectResult("~/advancedsearch.aspx");

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
                    "&lname=" + Server.UrlEncode(Request.Form["txtPubAuthorLastName"]) +
                    "&vol=" + Server.UrlEncode(Request.Form["txtPubVolume"]) +
                    "&yr=" + Server.UrlEncode(Request.Form["txtPubYear"]) +
                    "&subj=" + Server.UrlEncode(Request.Form["txtPubSubject"]) +
                    "&lang=" + Server.UrlEncode(Request.Form["ddlPubLanguage"]) +
                    "&col=" + Server.UrlEncode(Request.Form["ddlPubCollection"]) +
                    "&SearchCat=T&return=ADV";

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
                string pageDescription = string.Empty;
                string pageIndicators = string.Empty;
                string pageTypes = string.Empty;
                foreach(string indicator in ((PageHit)page).pageIndicators)
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
            if (SearchRedirect()) return new RedirectResult("~/search.aspx?" + Request.QueryString);

            int publicationPageSize = Convert.ToInt32(ConfigurationManager.AppSettings["PublicationResultPageSize"]);
            int authorPageSize = Convert.ToInt32(ConfigurationManager.AppSettings["AuthorResultPageSize"]);
            int keywordPageSize = Convert.ToInt32(ConfigurationManager.AppSettings["KeywordResultPageSize"]);
            int namePageSize = Convert.ToInt32(ConfigurationManager.AppSettings["NameResultPageSize"]);

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
                model.AuthorResult = search.SearchAuthor(searchTerm);
                search.StartPage = model.KeywordPage;
                search.NumResults = keywordPageSize;
                search.SortField = (SortField)Enum.Parse(typeof(SortField), ConfigurationManager.AppSettings["KeywordResultDefaultSort"]);
                model.KeywordResult = search.SearchKeyword(searchTerm);
                search.StartPage = model.NamePage;
                search.NumResults = namePageSize;
                search.SortField = (SortField)Enum.Parse(typeof(SortField), ConfigurationManager.AppSettings["NameResultDefaultSort"]);
                model.NameResult = search.SearchName(searchTerm);
                search.Facet = true;
                search.StartPage = model.ItemPage;
                search.NumResults = publicationPageSize;
                search.SortField = (SortField)Enum.Parse(typeof(SortField), ConfigurationManager.AppSettings["PublicationResultDefaultSort"]);
                model.ItemResult = search.SearchItem(searchTerm, limits);
            }
            else
            {
                // Advanced search
                if (model.Params.SearchCategory.Equals("A"))
                {
                    search.StartPage = model.AuthorPage;
                    search.NumResults = authorPageSize;
                    model.AuthorResult = search.SearchAuthor(searchTerm);
                }
                if ((model.Params.SearchCategory.Equals("N") || model.Params.SearchCategory.Equals("M")))
                {
                    search.StartPage = model.NamePage;
                    search.NumResults = namePageSize;
                    model.NameResult = search.SearchName(searchTerm);
                }
                if (model.Params.SearchCategory.Equals("T"))
                {
                    search.StartPage = model.ItemPage;
                    search.NumResults = publicationPageSize;
                    search.Facet = true;
                    model.ItemResult = search.SearchItem(searchTerm, model.Params.LastName,
                        model.Params.Volume, model.Params.Year, model.Params.Subject, model.Params.Language, 
                        model.Params.Collection, limits);
                }
                if (model.Params.SearchCategory.Equals("S"))
                {
                    search.StartPage = model.KeywordPage;
                    search.NumResults = keywordPageSize;
                    model.KeywordResult = search.SearchKeyword(searchTerm);
                }
            }

            if (search.Facet) AddFacetsToModel(model, limits);

            return View(model);
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
                    case SearchField.ItemAuthors:
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
                    case SearchField.ItemKeywords:
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

        private bool SearchRedirect()
        {
            bool redirect = false;
            bool switchToNew = false;

            // If ElasticSearch usage is turned off, immediately redirect to the original search
            if (ConfigurationManager.AppSettings["UseElasticSearch"] != "true") return true;

            // User requested to switch to original search
            if (Request.QueryString["elastic"] == "0")
            {
                // Set cookie to use the original search for 7 days
                HttpCookie cookie = new HttpCookie("originalsearch");
                cookie.Value = "1";
                cookie.Expires = DateTime.Now.AddHours(168); // 7-day expiration
                cookie.Domain = ".biodiversitylibrary.org";
                Response.Cookies.Add(cookie);

                // User chose to use old search interface, so redirect
                redirect = true;
            }
            else if (Request.QueryString["elastic"] == "1") // User requested to switch to elasticsearch
            {
                // Set cookie to use the new search
                HttpCookie cookie = new HttpCookie("originalsearch");
                cookie.Value = "0";
                cookie.Expires = DateTime.Now.AddHours(168); // 7-day expiration
                cookie.Domain = ".biodiversitylibrary.org";
                Response.Cookies.Add(cookie);

                switchToNew = true;
            }

            // User switched to original search within last 7 days, and is not switching back
            if (Request.Cookies.AllKeys.Contains("originalsearch") && !switchToNew)
            {
                if (Request.Cookies["originalsearch"].Value == "1") redirect = true;
            }

            return redirect;
        }
    }
}
