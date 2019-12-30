using CustomDataAccess;
using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;

namespace MOBOT.BHL.Web2
{
    public partial class Search : BasePage
    {
        public string searchVolume = string.Empty;
        public string searchStartPage = string.Empty;
        protected string sortBy { get; set; }
        protected string titleCount { get; set; }
        protected string authorCount { get; set; }
        protected string subjectCount { get; set; }
        protected string nameCount { get; set; }
        protected string annotationCount { get; set; }
        protected string annoConceptCount { get; set; }
        protected string annoSubjectCount { get; set; }
        protected string segmentCount { get; set; }

        protected string uiSearchTerm { get; set; } //for displaying the search term in the UI

        protected string queryString { get; set; } // for the elasticsearch redirect

        // Log an Exception
        public static void LogMessage(string message)
        {
            // Include logic for logging exceptions
            // Get the absolute path to the log file
            string logFile = "logs/MessageLog.txt";
            logFile = HttpContext.Current.Server.MapPath(logFile);

            // Open the log file for append and write the log
            StreamWriter sw = new StreamWriter(logFile, true);
            sw.WriteLine("********** {0} **********", DateTime.Now);
            sw.WriteLine("Source: " + message);
            sw.WriteLine();
            sw.Close();
        }


        protected void Page_Load(object sender, EventArgs e)
        {
          //  ControlGenerator.AddScriptControl(Page.Master.Page.Header.Controls, "/Scripts/ResizeBrowseUtils.js");
          //  ControlGenerator.AddAttributesAndPreserveExisting(main.Body, "onload", "ResizeBrowseDivs();ResizeContentPanel('browseContentPanel', 258);");
          //  ControlGenerator.AddAttributesAndPreserveExisting(main.Body, "onresize", "ResizeBrowseDivs();ResizeContentPanel('browseContentPanel', 258);");
            sortBy = string.Empty;
            string searchTerm = string.Empty;
            string searchCat = string.Empty;
            string searchLang = string.Empty;
            string searchLastName = string.Empty;
            string searchEdition = string.Empty;
            string searchYear = string.Empty;
            string searchSubject = string.Empty;
            string searchCollection = string.Empty;
            string searchIssue = string.Empty;
            string searchAnnotation = string.Empty;
            string searchContainerTitle = string.Empty;
            string searchSeries = string.Empty;
            string searchSort = "rank";
            if (!Convert.ToBoolean(ConfigurationManager.AppSettings["EnableFullTextSearch"])) searchSort = "title";
            string titleMax = "0";
            string authorMax = "0";
            string nameMax = "0";
            string subjectMax = "0";
            string annotationMax = "0";
            string annotationConceptMax = "0";
            string annotationSubjectMax = "0";
            string segmentMax = "0";

            string returnPage = string.Empty;
            int searchCollectionInt;
            int searchYearInt;

            // Get all of the possible search arguments
            if (Request["SearchTerm"] != null) searchTerm = Request["SearchTerm"].ToString();
            if (Request["SearchCat"] != null) searchCat = Request["SearchCat"].ToString();
            if (Request["lang"] != null) searchLang = Request["lang"].ToString();
            if (Request["lname"] != null) searchLastName = Request["lname"].ToString();
            if (Request["vol"] != null) searchVolume = Request["vol"].ToString();
            if (Request["ed"] != null) searchEdition = Request["ed"].ToString();
            if (Request["yr"] != null) searchYear = Request["yr"].ToString();
            if (Request["subj"] != null) searchSubject = Request["subj"].ToString();
            if (Request["col"] != null) searchCollection = Request["col"].ToString();
            if (Request["iss"] != null) searchIssue = Request["iss"].ToString();
            if (Request["spage"] != null) searchStartPage = Request["spage"].ToString();
            if (Request["sort"] != null) searchSort = Request["sort"].ToString();
            if (Request["anno"] != null) searchAnnotation = Request["anno"].ToString();
            if (Request["tMax"] != null) titleMax = Request["tMax"].ToString();
            if (Request["aMax"] != null) authorMax = Request["aMax"].ToString();
            if (Request["nMax"] != null) nameMax = Request["nMax"].ToString();
            if (Request["sMax"] != null) subjectMax = Request["sMax"].ToString();
            if (Request["anMax"] != null) annotationMax = Request["anMax"].ToString();
            if (Request["anCMax"] != null) annotationConceptMax = Request["anCMax"].ToString();
            if (Request["anSMax"] != null) annotationSubjectMax = Request["anSMax"].ToString();
            if (Request["segMax"] != null) segmentMax = Request["segMax"].ToString();
            if (Request["return"] != null) returnPage = Request["return"].ToString();
            if (Request["cont"] != null) searchContainerTitle = Request["cont"].ToString();
            if (Request["ser"] != null) searchSeries = Request["ser"].ToString();

            queryString = Request.QueryString.ToString().Replace("elastic=0&", "");

            //get sortdetails
            if (!string.IsNullOrEmpty(searchSort)) {
                sortBy = searchSort;
            }

            // Make sure we have valid search terms
            if ((searchTerm != string.Empty || searchLastName != string.Empty ||
                    searchCollection != string.Empty || searchAnnotation != string.Empty) &&
                (Int32.TryParse((searchYear == string.Empty ? "0" : searchYear), out searchYearInt) &&
                Int32.TryParse((searchCollection == string.Empty ? "0" : searchCollection), out searchCollectionInt)))
            {
                string searchCriteria = GetSearchCriteriaLabel(searchCat, searchTerm, searchLang, searchLastName, searchVolume,
                    searchEdition, searchYear, searchSubject, searchCollection, searchIssue, searchStartPage, searchAnnotation, searchContainerTitle);

                Page.Title = String.Format(ConfigurationManager.AppSettings["PageTitle"], "Search Results");

                navbar.searchTerm = searchTerm;
                uiSearchTerm = searchCriteria;
                PerformSearch(searchCat, searchTerm, searchLang, searchLastName, searchVolume, searchEdition, searchYear,
                    searchSubject, searchCollection, searchIssue, searchStartPage, searchAnnotation, titleMax, authorMax,
                    nameMax, subjectMax, annotationMax, annotationConceptMax, annotationSubjectMax, segmentMax, searchSort,
                    returnPage, searchContainerTitle, searchSeries);
            }
        }

        /// <summary>
        /// Build the text for the label that echoes the search criteria
        /// </summary>
        /// <param name="searchCat"></param>
        /// <param name="searchTerm"></param>
        /// <param name="searchLang"></param>
        /// <param name="searchLastName"></param>
        /// <param name="searchVolume"></param>
        /// <param name="searchEdition"></param>
        /// <param name="searchYear"></param>
        /// <param name="searchSubject"></param>
        /// <param name="searchCollection"></param>
        /// <param name="searchIssue"></param>
        /// <param name="searchStartPage"></param>
        /// <param name="searchAnnotation"></param>
        /// <param name="searchContainerTitle"></param>
        /// <returns></returns>
        private string GetSearchCriteriaLabel(string searchCat, string searchTerm, string searchLang, string searchLastName,
            string searchVolume, string searchEdition, string searchYear, string searchSubject, string searchCollection,
            string searchIssue, string searchStartPage, string searchAnnotation, string searchContainerTitle)
        {
            StringBuilder searchCriteria = new StringBuilder();

            if (searchCat.Length == 0)
            {
                // search box at top of page was used; just echo the input
                searchCriteria.Append(searchTerm);
            }
            else
            {
                if (searchCat.ToUpper() == "A") searchCriteria.Append(searchTerm);    // author search
                if (searchCat.ToUpper() == "N" || searchCat.ToUpper() == "M") searchCriteria.Append(searchTerm);  // name search
                if (searchCat.ToUpper() == "S") searchCriteria.Append(searchTerm);    // subject search
                if (searchCat.ToUpper() == "T")
                {
                    // title search
                    if (searchTerm != string.Empty) searchCriteria.Append(" title:" + searchTerm.Replace(' ', '-'));
                    if (searchLastName != string.Empty) searchCriteria.Append(" author:" + searchLastName.Replace(' ', '-'));
                    if (searchVolume != string.Empty) searchCriteria.Append(" vol:" + searchVolume.Replace(' ', '-'));
                    if (searchEdition != string.Empty) searchCriteria.Append(" ed:" + searchEdition.Replace(' ', '-'));
                    if (searchYear != string.Empty) searchCriteria.Append(" year:" + searchYear.Replace(' ', '-'));
                    if (searchSubject != string.Empty) searchCriteria.Append(" subject:" + searchSubject.Replace(' ', '-'));
                    if (searchLang != string.Empty)
                    {
                        Language lang = new BHLProvider().LanguageSelectAuto(searchLang);
                        if (lang != null) searchCriteria.Append(" lang:" + lang.LanguageName.Replace(' ', '-'));
                    }
                    if (searchCollection != string.Empty)
                    {
                        Collection collection = new BHLProvider().CollectionSelectAuto(Convert.ToInt32(searchCollection));
                        if (collection != null) searchCriteria.Append(" collection:" + collection.CollectionName.Replace(' ', '-'));
                    }
                }
                if (searchCat.ToUpper() == "SG")
                {
                    // article search
                    if (searchTerm != string.Empty) searchCriteria.Append(" article:" + searchTerm.Replace(' ', '-'));
                    if (searchContainerTitle != string.Empty) searchCriteria.Append(" journal:" + searchContainerTitle.Replace(' ', '-'));
                    if (searchLastName != string.Empty) searchCriteria.Append(" author:" + searchLastName.Replace(' ', '-'));
                    if (searchYear != string.Empty) searchCriteria.Append(" year:" + searchYear.Replace(' ', '-'));
                }
                if (searchCat.ToUpper() == "O")
                {
                    // annotation search
                    searchCriteria.Append(searchAnnotation);
                    if (searchTerm != string.Empty) searchCriteria.Append(" title:" + searchTerm.Replace(' ', '-'));
                    if (searchLastName != string.Empty) searchCriteria.Append(" author:" + searchLastName.Replace(' ', '-'));
                    if (searchVolume != string.Empty) searchCriteria.Append(" vol:" + searchVolume.Replace(' ', '-'));
                    if (searchEdition != string.Empty) searchCriteria.Append(" ed:" + searchEdition.Replace(' ', '-'));
                    if (searchYear != string.Empty) searchCriteria.Append(" year:" + searchYear.Replace(' ', '-'));
                }
            }

            return searchCriteria.ToString().Trim();
        }

        /// <summary>
        /// Execute the search and output the results
        /// </summary>
        /// <param name="searchCat"></param>
        /// <param name="searchTerm"></param>
        /// <param name="searchLang"></param>
        /// <param name="searchLastName"></param>
        /// <param name="searchVolume"></param>
        /// <param name="searchEdition"></param>
        /// <param name="searchYear"></param>
        /// <param name="searchSubject"></param>
        /// <param name="searchCollection"></param>
        /// <param name="searchIssue"></param>
        /// <param name="searchStartPage"></param>
        /// <param name="searchAnnotation"></param>
        /// <param name="titleMax"></param>
        /// <param name="authorMax"></param>
        /// <param name="nameMax"></param>
        /// <param name="subjectMax"></param>
        /// <param name="annotationMax"></param>
        /// <param name="annotationConceptMax"></param>
        /// <param name="annotationSubjectMax"></param>
        /// <param name="searchSort"></param>
        /// <param name="returnPage"></param>
        private void PerformSearch(string searchCat, string searchTerm, string searchLang,
            string searchLastName, string searchVolume, string searchEdition, string searchYear,
            string searchSubject, string searchCollection, string searchIssue, string searchStartPage,
            string searchAnnotation, string titleMax, string authorMax, string nameMax, string subjectMax, 
            string annotationMax, string annotationConceptMax, string annotationSubjectMax, string segmentMax, 
            string searchSort, string returnPage, string searchContainerTitle, string searchSeries)
        {
            bool nameFindOnly = false;
            if (searchCat.Length > 0)
            {
                authors.Visible = searchCat.ToUpper().Equals("A");
                names.Visible = (searchCat.ToUpper().Equals("N") || searchCat.ToUpper().Equals("M"));
                nameFindOnly = (searchCat.ToUpper() == "M");
                titles.Visible = searchCat.ToUpper().Equals("T");
                subjects.Visible = searchCat.ToUpper().Equals("S");
                annotations.Visible = searchCat.ToUpper().Equals("O");
                annotationConcepts.Visible = annotations.Visible;
                annotationSubjects.Visible = annotations.Visible;
                sections.Visible = searchCat.ToUpper().Equals("SG");

                //TODO section panel visible when?
                if (returnPage.ToUpper() == "DL") lnkNewSearch.NavigateUrl = "/dlsearch.aspx";
                if (returnPage.ToUpper() == "ADV") lnkNewSearch.NavigateUrl = "/advsearch";
            }
            else
            {
                authors.Visible = true;
                names.Visible = true;
                titles.Visible = true;
                subjects.Visible = true;
                sections.Visible = true;
            }

            if (!Convert.ToBoolean(ConfigurationManager.AppSettings["EnableFullTextSearch"])) lnkRankSort.Visible = false;

            switch (searchSort)
            {
                case "author":
                    lnkRankSort.Enabled = true;
                    lnkTitleSort.Enabled = true;
                    lnkAuthorSort.Enabled = false;
                    lnkDateSort.Enabled = true;
                    lnkRankSegSort.Enabled = true;
                    lnkTitleSegSort.Enabled = true;
                    lnkAuthorSegSort.Enabled = false;
                    lnkDateSegSort.Enabled = true;
                    break;
                case "date":
                    lnkRankSort.Enabled = true;
                    lnkTitleSort.Enabled = true;
                    lnkAuthorSort.Enabled = true;
                    lnkDateSort.Enabled = false;
                    lnkRankSegSort.Enabled = true;
                    lnkTitleSegSort.Enabled = true;
                    lnkAuthorSegSort.Enabled = true;
                    lnkDateSegSort.Enabled = false;
                    break;
                case "title":
                    lnkRankSort.Enabled = true;
                    lnkTitleSort.Enabled = false;
                    lnkAuthorSort.Enabled = true;
                    lnkDateSort.Enabled = true;
                    lnkRankSegSort.Enabled = true;
                    lnkTitleSegSort.Enabled = false;
                    lnkAuthorSegSort.Enabled = true;
                    lnkDateSegSort.Enabled = true;
                    break;
                default:
                    lnkRankSort.Enabled = false;
                    lnkTitleSort.Enabled = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableFullTextSearch"]);
                    lnkAuthorSort.Enabled = true;
                    lnkDateSort.Enabled = true;
                    lnkRankSegSort.Enabled = false;
                    lnkTitleSegSort.Enabled = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableFullTextSearch"]);
                    lnkAuthorSegSort.Enabled = true;
                    lnkDateSegSort.Enabled = true;
                    break;
            }

            spanAuthorSummary.Visible = authors.Visible;
            spanNameSummary.Visible = names.Visible;
            spanSubjectSummary.Visible = subjects.Visible;
            spanTitleSummary.Visible = titles.Visible;
            spanAnnotationSummary.Visible = annotations.Visible;
            spanAnnotationConceptSummary.Visible = annotations.Visible;
            spanAnnotationSubjectSummary.Visible = annotations.Visible;
            spanSectionSummary.Visible = sections.Visible;

            int maxExpandedResults = Convert.ToInt32(ConfigurationManager.AppSettings["MaximumExpandedResults"].ToString());
            int maxDefaultResults = Convert.ToInt32(ConfigurationManager.AppSettings["MaximumDefaultResults"].ToString());
            int titleReturnCount = (titleMax == "1" ? maxExpandedResults : maxDefaultResults);
            int authorReturnCount = (authorMax == "1" ? maxExpandedResults : maxDefaultResults);
            int nameReturnCount = (nameMax == "1" ? maxExpandedResults : maxDefaultResults);
            int subjectReturnCount = (subjectMax == "1" ? maxExpandedResults : maxDefaultResults);
            int annotationReturnCount = (annotationMax == "1" ? maxExpandedResults : maxDefaultResults);
            int annotationConceptReturnCount = (annotationConceptMax == "1" ? maxExpandedResults : maxDefaultResults);
            int annotationSubjectReturnCount = (annotationSubjectMax == "1" ? maxExpandedResults : maxDefaultResults);
            int segmentReturnCount = (segmentMax == "1" ? maxExpandedResults : maxDefaultResults);

            if (authors.Visible)
            {
                CustomGenericList<Author> authorsList = null;
                if (Convert.ToBoolean(ConfigurationManager.AppSettings["EnableFullTextSearch"]))
                {
                    authorsList = bhlProvider.SearchAuthor(searchTerm, authorReturnCount);
                }
                else
                {
                    authorsList = bhlProvider.AuthorSelectByNameLike(searchTerm, authorReturnCount);
                }

                if ((authorsList.Count == maxDefaultResults) && (authorMax == "0"))
                {
                    lnkAuthorMore.NavigateUrl = String.Format("{0}?searchTerm={1}&searchCat={2}&lang={3}&tMax={4}&aMax=1&nMax={5}&sMax={6}&segMax={7}#/authors", Request.Path, searchTerm, searchCat, searchLang, titleMax, nameMax, subjectMax, segmentMax);
                    lnkAuthorMoreTop.NavigateUrl = lnkAuthorMore.NavigateUrl;
                    lnkAuthorMore.Visible = true;
                    lnkAuthorMoreTop.Visible = true;
                }
                if (authorsList.Count == maxExpandedResults)
                {
                    litAuthorRefine.Visible = true;
                    litAuthorRefineTop.Visible = true;
                }
                authorRepeater.DataSource = authorsList;
                authorRepeater.DataBind();

                authorCount = authorRepeater.Items.Count.ToString();
        
            }
            if (titles.Visible)
            {
                int? searchYearInt = null;
                int? searchCollectionInt = null;
                int canContainItems = 0;
                searchYearInt = (searchYear == string.Empty ? null : (int?)Convert.ToInt32(searchYear));
                searchCollectionInt = (searchCollection == string.Empty ? null : (int?)Convert.ToInt32(searchCollection));

                // If a collection is being search, check whether it can contain items
                if (searchCollectionInt != null)
                {
                    Collection collection = bhlProvider.CollectionSelectAuto((int)searchCollectionInt);
                    if (collection != null) canContainItems = collection.CanContainItems;
                }

                CustomGenericList<SearchBookResult> books = null;
                if (Convert.ToBoolean(ConfigurationManager.AppSettings["EnableFullTextSearch"]))
                {
                    if (searchCat.Length > 0)
                    {
                        // Search terms specified individually (title, author, volume, etc)
                        books = bhlProvider.SearchBookFullText(searchTerm, searchLastName, searchVolume, searchEdition,
                            searchYearInt, searchSubject, searchLang, searchCollectionInt, titleReturnCount, searchSort);
                    }
                    else
                    {
                        // Single search box
                        books = bhlProvider.SearchBookFullText(searchTerm, titleReturnCount, searchSort);
                    }
                }
                else
                {
                    books = bhlProvider.SearchBook(searchTerm, searchLastName, searchVolume, searchEdition,
                        searchYearInt, searchSubject, searchLang, searchCollectionInt, titleReturnCount, searchSort);
                }
                if ((books.Count == maxDefaultResults) && (titleMax == "0"))
                {
                    lnkTitleMore.NavigateUrl = String.Format("{0}?searchTerm={1}&searchCat={2}&lang={3}&lname={4}&vol={5}&ed={6}&yr={7}&subj={8}&col={9}&tMax=1&aMax={10}&nMax={11}&sMax={12}&segMax={14}&sort={13}#/titles",
                        Request.Path, searchTerm, searchCat, searchLang, searchLastName, searchVolume, searchEdition, searchYear, searchSubject, searchCollection, authorMax, nameMax, subjectMax, searchSort, segmentMax);
                    lnkTitleMoreTop.NavigateUrl = lnkTitleMore.NavigateUrl;
                    lnkTitleMore.Visible = true;
                    lnkTitleMoreTop.Visible = true;
                }
                if (books.Count > 0)
                {
                    hypBookDownload.NavigateUrl = String.Format("/services/searchdownloadservice.ashx?searchTerm={0}&searchCat={1}&lang={2}&lname={3}&vol={4}&ed={5}&yr={6}&subj={7}&col={8}&dltype=T",
                        searchTerm, searchCat, searchLang, searchLastName, searchVolume, searchEdition, searchYear, searchSubject, searchCollection);
                    hypBookDownload.Visible = true;
                }
                if (books.Count == maxExpandedResults)
                {
                    litTitleRefine.Visible = true;
                    litTitleRefineTop.Visible = true;
                }

                // Set the sort links
                lnkRankSort.NavigateUrl = String.Format("{0}?searchTerm={1}&searchCat={2}&lang={3}&lname={4}&vol={5}&ed={6}&yr={7}&subj={8}&col={9}&tMax={10}&aMax={11}&nMax={12}&sMax={13}&segMax={14}&sort=rank",
                        Request.Path, searchTerm, searchCat, searchLang, searchLastName, searchVolume, searchEdition, searchYear, searchSubject, searchCollection, titleMax, authorMax, nameMax, subjectMax, segmentMax); ;
                lnkTitleSort.NavigateUrl = String.Format("{0}?searchTerm={1}&searchCat={2}&lang={3}&lname={4}&vol={5}&ed={6}&yr={7}&subj={8}&col={9}&tMax={10}&aMax={11}&nMax={12}&sMax={13}&segMax={14}&sort=title",
                        Request.Path, searchTerm, searchCat, searchLang, searchLastName, searchVolume, searchEdition, searchYear, searchSubject, searchCollection, titleMax, authorMax, nameMax, subjectMax, segmentMax); ;
                lnkAuthorSort.NavigateUrl = String.Format("{0}?searchTerm={1}&searchCat={2}&lang={3}&lname={4}&vol={5}&ed={6}&yr={7}&subj={8}&col={9}&tMax={10}&aMax={11}&nMax={12}&sMax={13}&segMax={14}&sort=author",
                        Request.Path, searchTerm, searchCat, searchLang, searchLastName, searchVolume, searchEdition, searchYear, searchSubject, searchCollection, titleMax, authorMax, nameMax, subjectMax, segmentMax);
                lnkDateSort.NavigateUrl = String.Format("{0}?searchTerm={1}&searchCat={2}&lang={3}&lname={4}&vol={5}&ed={6}&yr={7}&subj={8}&col={9}&tMax={10}&aMax={11}&nMax={12}&sMax={13}&segMax={14}&sort=date",
                        Request.Path, searchTerm, searchCat, searchLang, searchLastName, searchVolume, searchEdition, searchYear, searchSubject, searchCollection, titleMax, authorMax, nameMax, subjectMax, segmentMax);

                BookBrowse.Data = books;
                BookBrowse.ShowVolume = (searchVolume != string.Empty || canContainItems > 0);
                titleCount =  books.Count.ToString();
                
               
            }
            if (names.Visible)
            {
                List<NameResolved> namesList = bhlProvider.NameResolvedSelectByNameLike(searchTerm, nameReturnCount);
                if (nameFindOnly && namesList.Count == 1)
                {
                    // If this search was initiated by the Names browse page, and only one name was found,
                    // then redirect directly to the bibliography for that name
                    Response.Redirect("/name/" + Server.HtmlEncode(namesList[0].ResolvedNameString.Replace(' ', '_').Replace('.', '$').Replace('?', '^').Replace('&', '~')));
                }
                else
                {
                    if ((namesList.Count == maxDefaultResults) && (nameMax == "0"))
                    {
                        lnkNameMore.NavigateUrl = String.Format("{0}?searchTerm={1}&searchCat={2}&lang={3}&tMax={4}&aMax={5}&nMax=1&sMax={6}&segMax={7}#/names", Request.Path, searchTerm, searchCat, searchLang, titleMax, authorMax, subjectMax,segmentMax);
                        lnkNameMoreTop.NavigateUrl = lnkNameMore.NavigateUrl;
                        lnkNameMore.Visible = true;
                        lnkNameMoreTop.Visible = true;
                    }
                    if (namesList.Count == maxExpandedResults)
                    {
                        litNameRefine.Visible = true;
                        litNameRefineTop.Visible = true;
                    }
                    nameRepeater.DataSource = namesList;
                    nameRepeater.DataBind();
                }
                nameCount =  nameRepeater.Items.Count.ToString();
               
            }
            if (subjects.Visible)
            {
                CustomGenericList<TitleKeyword> keywords = null;
                if (Convert.ToBoolean(ConfigurationManager.AppSettings["EnableFullTextSearch"]))
                {
                    keywords = bhlProvider.SearchTitleKeyword(searchTerm, searchLang, subjectReturnCount);
                }
                else
                {
                    keywords = bhlProvider.TitleKeywordSelectLikeTag(searchTerm, searchLang, subjectReturnCount);
                }
                if ((keywords.Count == maxDefaultResults) && (subjectMax == "0"))
                {
                    lnkSubjectMore.NavigateUrl = String.Format("{0}?searchTerm={1}&searchCat={2}&lang={3}&tMax={4}&aMax={5}&nMax={6}&sMax=1&segMax={7}#/subjects", Request.Path, searchTerm, searchCat, searchLang, titleMax, authorMax, nameMax, segmentMax);
                    lnkSubjectMoreTop.NavigateUrl = lnkSubjectMore.NavigateUrl;
                    lnkSubjectMore.Visible = true;
                    lnkSubjectMoreTop.Visible = true;
                }
                if (keywords.Count == maxExpandedResults)
                {
                    litSubjectRefine.Visible = true;
                    litSubjectRefineTop.Visible = true;
                }
                foreach (TitleKeyword keyword in keywords)
                {
                    keyword.MarcDataFieldTag = keyword.Keyword.Replace(' ', '+');
                }
                subjectRepeater.DataSource = keywords;
                subjectRepeater.DataBind();

               subjectCount= subjectRepeater.Items.Count.ToString();
            }

            // Segment/Section Area

            if (sections.Visible)
            {
                int? searchYearInt = null;
                int? searchCollectionInt = null;
                searchYearInt = (searchYear == string.Empty ? null : (int?)Convert.ToInt32(searchYear));
                searchCollectionInt = (searchCollection == string.Empty ? null : (int?)Convert.ToInt32(searchCollection));
                //do section search
                CustomGenericList<Segment> segments = null;
                if (Convert.ToBoolean(ConfigurationManager.AppSettings["EnableFullTextSearch"]))
                {
                    if (searchCat.Length > 0)
                    {
                        // Search terms specified individually (title, author, volume, etc)
                          segments = bhlProvider.SearchSegmentAdvancedFullText(searchTerm, searchContainerTitle, searchLastName, searchYearInt.ToString(), searchVolume, searchSeries, searchIssue, segmentReturnCount, searchSort);
                    }
                    else
                    {
                        // Single search box
                        segments = bhlProvider.SearchSegmentFullText(searchTerm, segmentReturnCount, searchSort);
                    }
                }
                else
                {
                    segments = bhlProvider.SearchSegment(searchTerm, searchContainerTitle, searchLastName, searchYearInt.ToString(), searchVolume, searchSeries, searchIssue, segmentReturnCount, searchSort);
                }

                if ((segments.Count == maxDefaultResults) && (segmentMax == "0"))
                {
                    lnkSegmentMore.NavigateUrl = String.Format("{0}?searchTerm={1}&searchCat={2}&cont={3}&lname={4}&yr={5}&lang={6}&tMax={7}&aMax={8}&nMax={9}&sMax={10}&segMax=1#/sections", 
                        Request.Path, searchTerm, searchCat, searchContainerTitle, searchLastName, searchYear, searchLang, titleMax, authorMax, nameMax, subjectMax);
                    lnkSegmentMoreTop.NavigateUrl = lnkSegmentMore.NavigateUrl;
                    lnkSegmentMore.Visible = true;
                    lnkSegmentMoreTop.Visible = true;
                }
                if (segments.Count > 0)
                {
                    hypSegmentDownload.NavigateUrl = String.Format("/services/searchdownloadservice.ashx?searchTerm={0}&searchCat={1}&cont={2}&lname={3}&yr={4}&dltype=S",
                        searchTerm, searchCat, searchContainerTitle, searchLastName, searchYear);
                    hypSegmentDownload.Visible = true;
                }
                if (segments.Count == maxExpandedResults)
                {
                    litSegmentRefine.Visible = true;
                    litSegmentRefineTop.Visible = true;
                }

                // Set the sort links
                lnkRankSegSort.NavigateUrl = String.Format("{0}?searchTerm={1}&searchCat={2}&cont={3}&lang={4}&lname={5}&vol={6}&ed={7}&yr={8}&subj={9}&col={10}&tMax={11}&aMax={12}&nMax={13}&sMax={14}&segMax={15}&sort=rank#/sections",
                        Request.Path, searchTerm, searchCat, searchContainerTitle, searchLang, searchLastName, searchVolume, searchEdition, searchYear, searchSubject, searchCollection, titleMax, authorMax, nameMax, subjectMax, segmentMax); ;
                lnkTitleSegSort.NavigateUrl = String.Format("{0}?searchTerm={1}&searchCat={2}&cont={3}&lang={4}&lname={5}&vol={6}&ed={7}&yr={8}&subj={9}&col={10}&tMax={11}&aMax={12}&nMax={13}&sMax={14}&segMax={15}&sort=title#/sections",
                        Request.Path, searchTerm, searchCat, searchContainerTitle, searchLang, searchLastName, searchVolume, searchEdition, searchYear, searchSubject, searchCollection, titleMax, authorMax, nameMax, subjectMax, segmentMax); ;
                lnkAuthorSegSort.NavigateUrl = String.Format("{0}?searchTerm={1}&searchCat={2}&cont={3}&lang={4}&lname={5}&vol={6}&ed={7}&yr={8}&subj={9}&col={10}&tMax={11}&aMax={12}&nMax={13}&sMax={14}&segMax={15}&sort=author#/sections",
                        Request.Path, searchTerm, searchCat, searchContainerTitle, searchLang, searchLastName, searchVolume, searchEdition, searchYear, searchSubject, searchCollection, titleMax, authorMax, nameMax, subjectMax, segmentMax);
                lnkDateSegSort.NavigateUrl = String.Format("{0}?searchTerm={1}&searchCat={2}&cont={3}&lang={4}&lname={5}&vol={6}&ed={7}&yr={8}&subj={9}&col={10}&tMax={11}&aMax={12}&nMax={13}&sMax={14}&segMax={15}&sort=date#/sections",
                        Request.Path, searchTerm, searchCat, searchContainerTitle, searchLang, searchLastName, searchVolume, searchEdition, searchYear, searchSubject, searchCollection, titleMax, authorMax, nameMax, subjectMax, segmentMax);

                SectionBrowse.Data = segments;
                segmentCount = segments.Count.ToString();
            }

            if (annotations.Visible)
            {
                int? searchYearInt = null;
                int? searchCollectionInt = null;
                searchYearInt = (searchYear == string.Empty ? null : (int?)Convert.ToInt32(searchYear));
                searchCollectionInt = (searchCollection == string.Empty ? null : (int?)Convert.ToInt32(searchCollection));

                CustomGenericList<SearchAnnotationResult> annotationsList = bhlProvider.SearchAnnotation(searchAnnotation, searchTerm,
                    searchLastName, searchVolume, searchEdition, searchYearInt, searchCollectionInt, 1, 10000);
                if (annotations != null)
                {
                    if ((annotationsList.Count == maxDefaultResults) && (annotationMax == "0"))
                    {
                        lnkAnnotationMore.NavigateUrl = String.Format("{0}?anno={1}&searchTerm={2}&lname={3}&vol={4}&ed={5}&yr={6}&col={7}&anMax=1&anCMax={8}&anSMax={9}&segMax={10}#/annotations",
                            Request.Path, searchAnnotation, searchTerm, searchLastName, searchVolume, searchEdition, searchYear, searchCollection, annotationConceptMax, annotationSubjectMax, segmentMax);
                        lnkAnnotationMoreTop.NavigateUrl = lnkAnnotationMore.NavigateUrl;
                        lnkAnnotationMore.Visible = true;
                        lnkAnnotationMoreTop.Visible = true;
                    }
                    if (annotationsList.Count == maxExpandedResults)
                    {
                        litAnnotationRefine.Visible = true;
                        litAnnotationRefineTop.Visible = true;
                    }
                }

                annotationRepeater.DataSource = annotationsList;
                annotationRepeater.DataBind();
                annotationCount = annotationRepeater.Items.Count.ToString();
            }

            if (annotationConcepts.Visible)
            {
                CustomGenericList<AnnotationConcept> concepts = bhlProvider.AnnotationConceptSelectByConceptText(searchAnnotation, 1);
                if (concepts != null)
                {
                    if ((concepts.Count == maxDefaultResults) && (annotationConceptMax == "0"))
                    {
                        lnkAnnotationConceptMore.NavigateUrl = String.Format("{0}?anno={1}&searchTerm={2}&lname={3}&vol={4}&ed={5}&yr={6}&col={7}&anMax={8}&anCMax=1&anSMax={9}&segMax={10}#/annotationconcepts",
                            Request.Path, searchAnnotation, searchTerm, searchLastName, searchVolume, searchEdition, searchYear, searchCollection, annotationMax, annotationSubjectMax, segmentMax);
                        lnkAnnotationConceptMoreTop.NavigateUrl = lnkAnnotationConceptMore.NavigateUrl;
                        lnkAnnotationConceptMore.Visible = true;
                        lnkAnnotationConceptMoreTop.Visible = true;
                    }
                    if (concepts.Count == maxExpandedResults)
                    {
                        litAnnotationConceptRefine.Visible = true;
                        litAnnotationConceptRefineTop.Visible = true;
                    }
                }

                annotationConceptRepeater.DataSource = concepts;
                annotationConceptRepeater.DataBind();
                annoConceptCount = annotationConceptRepeater.Items.Count.ToString();
            }

            if (annotationSubjects.Visible)
            {
                CustomGenericList<AnnotationSubject> subjectsList = bhlProvider.AnnotationSubjectSelectBySubjectText(searchAnnotation, 1);
                if (subjectsList != null)
                {
                    if ((subjectsList.Count == maxDefaultResults) && (annotationConceptMax == "0"))
                    {
                        lnkAnnotationSubjectMore.NavigateUrl = String.Format("{0}?anno={1}&searchTerm={2}&lname={3}&vol={4}&ed={5}&yr={6}&col={7}&anMax={8}&anCMax={9}1&anSMax=1&seg#/annotationsubjects",
                            Request.Path, searchAnnotation, searchTerm, searchLastName, searchVolume, searchEdition, searchYear, searchCollection, annotationMax, annotationConceptMax);
                        lnkAnnotationSubjectMoreTop.NavigateUrl = lnkAnnotationSubjectMore.NavigateUrl;
                        lnkAnnotationSubjectMore.Visible = true;
                        lnkAnnotationSubjectMoreTop.Visible = true;
                    }
                    if (subjectsList.Count == maxExpandedResults)
                    {
                        litAnnotationSubjectRefine.Visible = true;
                        litAnnotationSubjectRefineTop.Visible = true;
                    }
                }

                annotationSubjectRepeater.DataSource = subjectsList;
                annotationSubjectRepeater.DataBind();
                annoSubjectCount = annotationSubjectRepeater.Items.Count.ToString();
            }

        }


        public string SetSortClass(string sortType)
        {
            if (!String.IsNullOrEmpty(sortBy))
            {
                return sortBy.Equals(sortType, StringComparison.OrdinalIgnoreCase) ? "activesort" : string.Empty;
            }
            else
            {   //set Relevance to active
                return sortType.Equals("relevance", StringComparison.OrdinalIgnoreCase) ? "activesort" : string.Empty;
            }
        }

        public string ZerotoNo(string number, string single, string plural)
        {
            if (number.Equals("1")){
                return string.Concat("1 <span class=\"highlight\">" + single + "</span>");
            } 
            else if(number.Equals("0")){
                return string.Concat("No <span class=\"highlight\">" + plural + "</span>");
            }
            else{
                return string.Concat(number +" <span class=\"highlight\">" + plural + "</span>");
            }
                
        }
    }
}