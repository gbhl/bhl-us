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
        public ActionResult Index(string searchTerm)
        {
            if (SearchRedirect()) return new RedirectResult("~/search.aspx?" + Request.QueryString);

            // Get the search arguments
            string searchCat = Request["SearchCat"] ?? string.Empty;
            string searchLang = Request["lang"] ?? string.Empty;
            string searchLastName = Request["lname"] ?? string.Empty;
            string searchVolume = Request["vol"] ?? string.Empty;;
            string searchEdition = Request["ed"] ?? string.Empty;;
            string searchYear = Request["yr"] ?? string.Empty;;
            string searchSubject = Request["subj"] ?? string.Empty;;
            string searchCollection = Request["col"] ?? string.Empty;;
            string searchIssue = Request["iss"] ?? string.Empty;;
            string searchStartPage = Request["spage"] ?? string.Empty;;
            string searchSort = Request["sort"] ?? string.Empty;;
            string searchAnnotation = Request["anno"] ?? string.Empty;;
            string titleMax = Request["tMax"] ?? string.Empty;;
            string authorMax = Request["aMax"] ?? string.Empty;;
            string nameMax = Request["nMax"] ?? string.Empty;;
            string subjectMax = Request["sMax"] ?? string.Empty;;
            string annotationMax = Request["anMax"] ?? string.Empty;;
            string annotationConceptMax = Request["anCMax"] ?? string.Empty;;
            string annotationSubjectMax = Request["anSMax"] ?? string.Empty;;
            string segmentMax = Request["segMax"] ?? string.Empty;;
            string returnPage = Request["return"] ?? string.Empty;;
            string searchContainerTitle = Request["cont"] ?? string.Empty;;
            string searchSeries = Request["ser"] ?? string.Empty;;

            // For annotation searches, use the non-elasticsearch search page
            if (searchCat == "O") return new RedirectResult("~/search.aspx?" + Request.QueryString);

            MVCServices.SearchService searchService = new MVCServices.SearchService();

            ViewBag.SearchTerm = searchService.GetSearchCriteriaLabel(searchCat,
                searchTerm, searchLang, searchLastName, searchVolume, searchEdition, searchYear, searchSubject,
                searchCollection, searchIssue, searchStartPage, searchAnnotation, searchContainerTitle);

            // Set up the querystring for opting out of elasticsearch
            ViewBag.QueryString = Request.QueryString.ToString().Replace("elastic=1&", "");
            //@ViewBag.UseElasticSearch = ConfigurationManager.AppSettings["UseElasticSearch"] == "true";



            // TODO: Submit the request to ElasticSearch here!!!



            ViewBag.NumPubs = 0;
            ViewBag.NumAuthors = 0;
            ViewBag.NumSubjects = 0;
            ViewBag.NumNames = 0;

            return View();
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
                queryString = "SearchTerm=" + Server.UrlEncode(Request.Form["txtAuthorName"]) + " & SearchCat=A&return=ADV";
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
