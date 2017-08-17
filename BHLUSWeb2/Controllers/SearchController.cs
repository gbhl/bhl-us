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
        public ActionResult Index(string searchTerm)
        {
            SearchModel model = new SearchModel();
            model.Params.SearchTerm = searchTerm;

            // Get the search arguments
            model.Params.SearchCategory = (Request["SearchCat"] ?? string.Empty).Trim().ToUpper();
            model.Params.LastName = Request["lname"] ?? string.Empty;
            model.Params.Volume = Request["vol"] ?? string.Empty; ;
            model.Params.Year = Request["yr"] ?? string.Empty; ;
            model.Params.Subject = Request["subj"] ?? string.Empty; ;
            model.Params.Language = Request["lang"] ?? string.Empty;
            model.Params.Collection = Request["col"] ?? string.Empty; ;
            int startPage;
            if (!Int32.TryParse(Request["ppage"] ?? "1", out startPage)) startPage = 1;
            model.ItemPage = startPage;
            if (!Int32.TryParse(Request["apage"] ?? "1", out startPage)) startPage = 1;
            model.AuthorPage = startPage;
            if (!Int32.TryParse(Request["kpage"] ?? "1", out startPage)) startPage = 1;
            model.KeywordPage = startPage;
            if (!Int32.TryParse(Request["npage"] ?? "1", out startPage)) startPage = 1;
            model.NamePage = startPage;

            // For annotation searches, use the non-elasticsearch search page
            if (model.Params.SearchCategory == "O") return new RedirectResult("~/search.aspx?" + Request.QueryString);

            return SearchAction(model, null);
        }

        [HttpPost]
        public ActionResult Index(SearchModel model)
        {
            // Convert the selected facets to limits for search
            List<Tuple<SearchField, string>> limits = null;
            string selectedFacets = Request["facet.Checked"];
            List<string> facets = selectedFacets.Replace("false", "").Split(new char[] { '~' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            for (int x = facets.Count - 1; x >= 0; x--)
            {
                if (facets[x].StartsWith(",")) facets.RemoveAt(x);
            }

            if (facets.Count > 0) limits = new List<Tuple<SearchField, string>>();
            foreach(var facet in facets)
            {
                limits.Add(
                    new Tuple<SearchField, string>(
                        (SearchField)Enum.Parse(typeof(SearchField), facet.Split('_')[0], true),
                        facet.Split('_')[1]));
            }

            return SearchAction(model, limits);
        }

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

        public ActionResult SearchAction(SearchModel model, List<Tuple<SearchField, string>> limits)
        {
            int pageSize = 10;

            if (SearchRedirect()) return new RedirectResult("~/search.aspx?" + Request.QueryString);

            // Get featured collection name
            BHLProvider provider = new BHLProvider();
            Collection collection = provider.CollectionSelectFeatured();
            if (collection != null)
            {
                ViewBag.CollectionName = collection.CollectionName;
                ViewBag.CollectionUrl = string.IsNullOrWhiteSpace(collection.CollectionURL) ? collection.CollectionID.ToString() : collection.CollectionURL;
            }

            // Submit the request to ElasticSearch here
            ISearch search = new SearchFactory().GetSearch(ConfigurationManager.AppSettings["SearchProviders"]);
            search.NumResults = pageSize;
            search.Highlight = true;
            search.Suggest = true;

            if (string.IsNullOrWhiteSpace(model.Params.SearchCategory))
            {
                // Standard search
                search.StartPage = model.AuthorPage;
                model.AuthorResult = search.SearchAuthor(model.Params.SearchTerm);
                search.StartPage = model.KeywordPage;
                model.KeywordResult = search.SearchKeyword(model.Params.SearchTerm);
                search.StartPage = model.NamePage;
                model.NameResult = search.SearchName(model.Params.SearchTerm);
                search.Facet = true;
                search.StartPage = model.ItemPage;
                model.ItemResult = search.SearchItem(model.Params.SearchTerm, limits);
            }
            else
            {
                // Advanced search
                if (model.Params.SearchCategory.Equals("A"))
                {
                    search.StartPage = model.AuthorPage;
                    model.AuthorResult = search.SearchAuthor(model.Params.SearchTerm);
                }
                if ((model.Params.SearchCategory.Equals("N") || model.Params.SearchCategory.Equals("M")))
                {
                    search.StartPage = model.NamePage;
                    model.NameResult = search.SearchName(model.Params.SearchTerm);
                }
                if (model.Params.SearchCategory.Equals("T"))
                {
                    search.StartPage = model.ItemPage;
                    search.Facet = true;
                    model.ItemResult = search.SearchItem(model.Params.SearchTerm, model.Params.LastName,
                        model.Params.Volume, model.Params.Year, model.Params.Subject, model.Params.Language, 
                        model.Params.Collection, limits);
                }
                if (model.Params.SearchCategory.Equals("S"))
                {
                    search.StartPage = model.KeywordPage;
                    model.KeywordResult = search.SearchKeyword(model.Params.SearchTerm);
                }
            }

            if (model.Params.SearchCategory == "M" && model.NameResult.Names.Count == 1)
            {
                if (model.NameResult.IsValid)
                {
                    // Only one result to a name-only search... redirect directly 
                    // to the bibliography for that name
                    return new RedirectResult("~/name/" +
                        Server.HtmlEncode(((NameHit)model.NameResult.Names[0]).Name
                            .Replace(' ', '_')
                            .Replace('.', '$')
                            .Replace('?', '^')
                            .Replace('&', '~')
                        )
                    );
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
