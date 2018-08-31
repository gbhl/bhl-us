using System;
using System.Collections.Generic;
using System.Configuration;

namespace BHL.Search.Elastic
{
    public class Search : ISearch
    {
        //------------------------------------------------------------
        // Private properties

        IESSearch _esSearch;
        string _esSortField = ESSortField.SCORE;

        //------------------------------------------------------------
        // Public Properties

        private int _numResults = 100;
        private int _startPage = 1;
        private bool _facet = false;
        private bool _highlight = false;
        private bool _suggest = false;
        private SortField _sortField = SortField.Score;

        public bool Facet
        {
            get { return _facet; }
            set { _facet = value; }
        }

        public bool Highlight
        {
            get { return _highlight; }
            set { _highlight = value;  }
        }

        public bool Suggest
        {
            get { return _suggest;  }
            set { _suggest = value; }
        }

        public int NumResults
        {
            get { return _numResults; }
            set { _numResults = value > 0 ? value : 100; }
        }

        public int StartPage
        {
            get { return _startPage; }
            set { _startPage = value > 0 ? value : 1; }
        }

        public SortField SortField
        {
            get { return _sortField; }
            set {
                _sortField = value;
                _esSortField = GetSortField(_sortField);
            }
        }

        //------------------------------------------------------------
        // Methods

        public Search()
        {
            _esSearch = new ESSearch(ConfigurationManager.AppSettings["ElasticSearchServerAddress"]);
        }

        public bool IsOnline()
        {
            bool online = true;
            try
            {
                _esSearch.CheckServerStatus();
            }
            catch (Exception ex)
            {
                // TODO: Consider logging the exception here
                online = false;
            }
            return online;
        }

        public bool IsFullTextSupported()
        {
            return true;
        }

        public ISearchResult SearchAll(string query, List<Tuple<SearchField, string>> limits = null)
        {
            List<Tuple<string, string>> searchLimits = GetSearchLimitsList(limits);
            List<string> returnFields = new List<string> { ESField.ASSOCIATIONS, ESField.AUTHORS,
                ESField.COLLECTIONS, ESField.CONTAINER, ESField.CONTRIBUTORS, ESField.DATERANGES,
                ESField.DATES, ESField.DOI, ESField.GENRE, ESField.HASEXTERNALCONTENT,
                ESField.HASLOCALCONTENT, ESField.HASSEGMENTS, ESField.ID, ESField.ISBN, ESField.ISSN,
                ESField.ISSUE, ESField.ITEMID, ESField.KEYWORDS, ESField.LANGUAGE, ESField.MATERIALTYPE,
                ESField.OCLC, ESField.PAGERANGE, ESField.PUBLICATIONPLACE, ESField.PUBLISHER, ESField.SCORE,
                ESField.SEGMENTID, ESField.SERIES, ESField.STARTPAGEID, ESField.TEXT, ESField.TITLE,
                ESField.TITLEID, ESField.TRANSLATEDTITLE, ESField.UNIFORMTITLE, ESField.URL, ESField.VARIANTS,
                ESField.VOLUME, ESField.AUTHORNAMES, ESField.PRIMARYAUTHORNAME, ESField.KEYWORD,
                ESField.COUNT, ESField.NAME};
            List<Tuple<string, ESFacetSortOrder>> facetFields = new List<Tuple<string, ESFacetSortOrder>> {
                new Tuple<string, ESFacetSortOrder>(ESField.GENRE, ESFacetSortOrder.TERM),
                new Tuple<string, ESFacetSortOrder>(ESField.MATERIALTYPE, ESFacetSortOrder.TERM),
                new Tuple<string, ESFacetSortOrder>(ESField.FACETAUTHORS, ESFacetSortOrder.COUNT),
                new Tuple<string, ESFacetSortOrder>(ESField.DATERANGES, ESFacetSortOrder.TERM),
                new Tuple<string, ESFacetSortOrder>(ESField.KEYWORDS_RAW, ESFacetSortOrder.COUNT),
                new Tuple<string, ESFacetSortOrder>(ESField.LANGUAGE, ESFacetSortOrder.COUNT) };
            List<string> highlightFields = new List<string> { ESField.ASSOCIATIONS, ESField.COLLECTIONS,
                ESField.CONTAINER, ESField.CONTRIBUTORS, ESField.KEYWORDS, ESField.PUBLICATIONPLACE,
                ESField.PUBLISHER, ESField.SEARCHAUTHORS, ESField.TITLE, ESField.TRANSLATEDTITLE,
                ESField.UNIFORMTITLE, ESField.VARIANTS, ESField.AUTHORNAMES, ESField.KEYWORD, ESField.NAME,
                ESField.TEXT, ESField.ISSN, ESField.ISBN, ESField.DOI, ESField.OCLC};

            ConfigureSearch(ESIndex.ALL, returnFields, facetFields, highlightFields);
            ISearchResult result = _esSearch.SearchAll(query, searchLimits);

            // Add the query parameters to the result
            result.Query.Add(new Tuple<SearchField, string>(SearchField.All, query));
            result.QueryLimits = limits;

            return result;
        }

        public ISearchResult SearchCatalog(SearchStringParam title, SearchStringParam author, string volume, string year, 
            SearchStringParam keyword, Tuple<string, string> language, Tuple<string, string> collection, 
            SearchStringParam text, List<Tuple<SearchField, string>> limits = null)
        {
            List<Tuple<string, string>> searchLimits = GetSearchLimitsList(limits);
            List<string> returnFields = new List<string> { ESField.ASSOCIATIONS, ESField.AUTHORS,
                ESField.COLLECTIONS, ESField.CONTAINER, ESField.CONTRIBUTORS, ESField.DATERANGES,
                ESField.DATES, ESField.DOI, ESField.GENRE, ESField.HASEXTERNALCONTENT,
                ESField.HASLOCALCONTENT, ESField.HASSEGMENTS, ESField.ID, ESField.ISBN, ESField.ISSN,
                ESField.ISSUE, ESField.ITEMID, ESField.KEYWORDS, ESField.LANGUAGE, ESField.MATERIALTYPE,
                ESField.OCLC, ESField.PAGERANGE, ESField.PUBLICATIONPLACE, ESField.PUBLISHER, ESField.SCORE,
                ESField.SEGMENTID, ESField.SERIES, ESField.STARTPAGEID, ESField.TEXT, ESField.TITLE,
                ESField.TITLEID, ESField.TRANSLATEDTITLE, ESField.UNIFORMTITLE, ESField.URL, ESField.VARIANTS,
                ESField.VOLUME };
            List<Tuple<string, ESFacetSortOrder>> facetFields = new List<Tuple<string, ESFacetSortOrder>> {
                new Tuple<string, ESFacetSortOrder>(ESField.GENRE, ESFacetSortOrder.TERM),
                new Tuple<string, ESFacetSortOrder>(ESField.MATERIALTYPE, ESFacetSortOrder.TERM),
                new Tuple<string, ESFacetSortOrder>(ESField.FACETAUTHORS, ESFacetSortOrder.COUNT),
                new Tuple<string, ESFacetSortOrder>(ESField.DATERANGES, ESFacetSortOrder.TERM),
                new Tuple<string, ESFacetSortOrder>(ESField.KEYWORDS_RAW, ESFacetSortOrder.COUNT),
                new Tuple<string, ESFacetSortOrder>(ESField.LANGUAGE, ESFacetSortOrder.COUNT) };

            // Highlight only the queried fields
            List<string> highlightFields = new List<string>();
            if (!string.IsNullOrWhiteSpace(title.searchValue)) {
                highlightFields.Add(ESField.ASSOCIATIONS);
                highlightFields.Add(ESField.TITLE);
                highlightFields.Add(ESField.TRANSLATEDTITLE);
                highlightFields.Add(ESField.UNIFORMTITLE);
                highlightFields.Add(ESField.VARIANTS);
            }
            if (!string.IsNullOrWhiteSpace(author.searchValue)) highlightFields.Add(ESField.SEARCHAUTHORS);
            if (!string.IsNullOrWhiteSpace(keyword.searchValue)) highlightFields.Add(ESField.KEYWORDS);
            if (collection != null) highlightFields.Add(ESField.COLLECTIONS);
            if (!string.IsNullOrWhiteSpace(text.searchValue)) highlightFields.Add(ESField.TEXT);

            // Perform the search.  Use the CATALOG index unless a value is specified for the "text"
            // parameter.  In that case, use the ITEMS index to perform a full-text search on the 
            // text of the items.
            ConfigureSearch((string.IsNullOrWhiteSpace(text.searchValue) ? ESIndex.CATALOG : ESIndex.ITEMS), 
                returnFields, facetFields, highlightFields);
            ISearchResult result = _esSearch.SearchCatalog(title, author, volume, year, keyword, 
                (language != null ? language.Item2 : null), 
                (collection != null ? collection.Item2 : null), text, searchLimits);

            // Add the query parameters to the result
            if (!string.IsNullOrWhiteSpace(title.searchValue)) result.Query.Add(new Tuple<SearchField, string>(SearchField.Title, title.searchValue));
            if (!string.IsNullOrWhiteSpace(author.searchValue)) result.Query.Add(new Tuple<SearchField, string>(SearchField.AuthorNames, author.searchValue));
            if (!string.IsNullOrWhiteSpace(volume)) result.Query.Add(new Tuple<SearchField, string>(SearchField.Volume, volume));
            if (!string.IsNullOrWhiteSpace(year)) result.Query.Add(new Tuple<SearchField, string>(SearchField.Dates, year));
            if (!string.IsNullOrWhiteSpace(keyword.searchValue)) result.Query.Add(new Tuple<SearchField, string>(SearchField.Keyword, keyword.searchValue));
            if (language != null) result.Query.Add(new Tuple<SearchField, string>(SearchField.Language, language.Item2));
            if (collection != null) result.Query.Add(new Tuple<SearchField, string>(SearchField.Collections, collection.Item2));
            if (!string.IsNullOrWhiteSpace(text.searchValue)) result.Query.Add(new Tuple<SearchField, string>(SearchField.Text, text.searchValue));
            result.QueryLimits = limits;

            return result;
        }

        public ISearchResult SearchCatalog(string searchTerm, List<Tuple<SearchField, string>> limits = null)
        {
            List<Tuple<string, string>> searchLimits = GetSearchLimitsList(limits);
            List<string> returnFields = new List<string> { ESField.ASSOCIATIONS, ESField.AUTHORS,
                ESField.COLLECTIONS, ESField.CONTAINER, ESField.CONTRIBUTORS, ESField.DATERANGES,
                ESField.DATES, ESField.DOI, ESField.GENRE, ESField.HASEXTERNALCONTENT,
                ESField.HASLOCALCONTENT, ESField.HASSEGMENTS, ESField.ID, ESField.ISBN, ESField.ISSN,
                ESField.ISSUE, ESField.ITEMID, ESField.KEYWORDS, ESField.LANGUAGE, ESField.MATERIALTYPE,
                ESField.OCLC, ESField.PAGERANGE, ESField.PUBLICATIONPLACE, ESField.PUBLISHER, ESField.SCORE,
                ESField.SEGMENTID, ESField.SERIES, ESField.STARTPAGEID, ESField.TEXT, ESField.TITLE,
                ESField.TITLEID, ESField.TRANSLATEDTITLE, ESField.UNIFORMTITLE, ESField.URL, ESField.VARIANTS,
                ESField.VOLUME };
            List<Tuple<string, ESFacetSortOrder>> facetFields = new List<Tuple<string, ESFacetSortOrder>> {
                new Tuple<string, ESFacetSortOrder>(ESField.GENRE, ESFacetSortOrder.TERM),
                new Tuple<string, ESFacetSortOrder>(ESField.MATERIALTYPE, ESFacetSortOrder.TERM),
                new Tuple<string, ESFacetSortOrder>(ESField.FACETAUTHORS, ESFacetSortOrder.COUNT),
                new Tuple<string, ESFacetSortOrder>(ESField.DATERANGES, ESFacetSortOrder.TERM),
                new Tuple<string, ESFacetSortOrder>(ESField.KEYWORDS_RAW, ESFacetSortOrder.COUNT),
                new Tuple<string, ESFacetSortOrder>(ESField.LANGUAGE, ESFacetSortOrder.COUNT) };
            List<string> highlightFields = new List<string> { ESField.ASSOCIATIONS, ESField.COLLECTIONS,
                ESField.CONTAINER, ESField.CONTRIBUTORS, ESField.KEYWORDS, ESField.PUBLICATIONPLACE,
                ESField.PUBLISHER, ESField.SEARCHAUTHORS, ESField.TITLE, ESField.TRANSLATEDTITLE,
                ESField.UNIFORMTITLE, ESField.VARIANTS, ESField.TEXT, ESField.ISSN, ESField.ISBN,
                ESField.DOI, ESField.OCLC };

            ConfigureSearch(ESIndex.CATALOG, returnFields, facetFields, highlightFields);
            ISearchResult result = _esSearch.SearchAll(searchTerm, searchLimits);

            // Add the query parameters to the result
            result.Query.Add(new Tuple<SearchField, string>(SearchField.All, searchTerm));
            result.QueryLimits = limits;

            return result;
        }

        public ISearchResult SearchItem(string searchTerm, List<Tuple<SearchField, string>> limits = null)
        {
            List<Tuple<string, string>> searchLimits = GetSearchLimitsList(limits);
            List<string> returnFields = new List<string> { ESField.ASSOCIATIONS, ESField.AUTHORS,
                ESField.COLLECTIONS, ESField.CONTAINER, ESField.CONTRIBUTORS, ESField.DATERANGES,
                ESField.DATES, ESField.DOI, ESField.GENRE, ESField.HASEXTERNALCONTENT,
                ESField.HASLOCALCONTENT, ESField.HASSEGMENTS, ESField.ID, ESField.ISBN, ESField.ISSN,
                ESField.ISSUE, ESField.ITEMID, ESField.KEYWORDS, ESField.LANGUAGE, ESField.MATERIALTYPE,
                ESField.OCLC, ESField.PAGERANGE, ESField.PUBLICATIONPLACE, ESField.PUBLISHER, ESField.SCORE,
                ESField.SEGMENTID, ESField.SERIES, ESField.STARTPAGEID, ESField.TEXT, ESField.TITLE,
                ESField.TITLEID, ESField.TRANSLATEDTITLE, ESField.UNIFORMTITLE, ESField.URL, ESField.VARIANTS,
                ESField.VOLUME };
            List<Tuple<string, ESFacetSortOrder>> facetFields = new List<Tuple<string, ESFacetSortOrder>> {
                new Tuple<string, ESFacetSortOrder>(ESField.GENRE, ESFacetSortOrder.TERM),
                new Tuple<string, ESFacetSortOrder>(ESField.MATERIALTYPE, ESFacetSortOrder.TERM),
                new Tuple<string, ESFacetSortOrder>(ESField.FACETAUTHORS, ESFacetSortOrder.COUNT),
                new Tuple<string, ESFacetSortOrder>(ESField.DATERANGES, ESFacetSortOrder.TERM),
                new Tuple<string, ESFacetSortOrder>(ESField.KEYWORDS_RAW, ESFacetSortOrder.COUNT),
                new Tuple<string, ESFacetSortOrder>(ESField.LANGUAGE, ESFacetSortOrder.COUNT) };
            List<string> highlightFields = new List<string> { ESField.ASSOCIATIONS, ESField.COLLECTIONS,
                ESField.CONTAINER, ESField.CONTRIBUTORS, ESField.KEYWORDS, ESField.PUBLICATIONPLACE,
                ESField.PUBLISHER, ESField.SEARCHAUTHORS, ESField.TITLE, ESField.TRANSLATEDTITLE,
                ESField.UNIFORMTITLE, ESField.VARIANTS, ESField.TEXT, ESField.ISSN, ESField.ISBN,
                ESField.DOI, ESField.OCLC };

            ConfigureSearch(ESIndex.ITEMS, returnFields, facetFields, highlightFields);
            ISearchResult result = _esSearch.SearchAll(searchTerm, searchLimits);

            // Add the query parameters to the result
            result.Query.Add(new Tuple<SearchField, string>(SearchField.All, searchTerm));
            result.QueryLimits = limits;

            return result;
        }

        public ISearchResult SearchAuthor(string name)
        {
            List<string> returnFields = new List<string> { ESField.ID, ESField.AUTHORNAMES, ESField.PRIMARYAUTHORNAME };
            List<Tuple<string, ESFacetSortOrder>> facetFields = new List<Tuple<string, ESFacetSortOrder>>();
            List<string> highlightFields = new List<string> { ESField.AUTHORNAMES };

            ConfigureSearch(ESIndex.AUTHORS, returnFields, facetFields, highlightFields);
            ISearchResult result = _esSearch.SearchAuthor(name);

            // Add the query parameters to the result
            result.Query.Add(new Tuple<SearchField, string>(SearchField.All, name));

            return result;
        }

        public ISearchResult SearchKeyword(string keyword)
        {
            List<string> returnFields = new List<string> { ESField.ID, ESField.KEYWORD };
            List<Tuple<string, ESFacetSortOrder>> facetFields = new List<Tuple<string, ESFacetSortOrder>>();
            List<string> highlightFields = new List<string> { ESField.KEYWORD };
            
            ConfigureSearch(ESIndex.KEYWORDS, returnFields, facetFields, highlightFields);
            ISearchResult result = _esSearch.SearchKeyword(keyword);

            // Add the query parameters to the result
            result.Query.Add(new Tuple<SearchField, string>(SearchField.All, keyword));

            return result;
        }

        public ISearchResult SearchName(string name)
        {
            List<string> returnFields = new List<string> { ESField.ID, ESField.COUNT, ESField.NAME };
            List<Tuple<string, ESFacetSortOrder>> facetFields = new List<Tuple<string, ESFacetSortOrder>>();
            List<string> highlightFields = new List<string> { ESField.NAME };

            ConfigureSearch(ESIndex.NAMES, returnFields, facetFields, highlightFields);
            ISearchResult result = _esSearch.SearchName(name);

            // Add the query parameters to the result
            result.Query.Add(new Tuple<SearchField, string>(SearchField.All, name));

            return result;
        }

        public ISearchResult SearchPage(string query, List<Tuple<SearchField, string>> limits = null, 
            bool includeText = false)
        {
            List<Tuple<string, string>> searchLimits = GetSearchLimitsList(limits);
            List<string> returnFields = new List<string> {
                    ESField.ID, ESField.ITEMID, ESField.SEQUENCE, ESField.PAGEINDICATORS,
                    ESField.PAGETYPES, ESField.SEGMENTS
                    };
            if (includeText) returnFields.Add(ESField.TEXT);
            List<Tuple<string, ESFacetSortOrder>> facetFields = new List<Tuple<string, ESFacetSortOrder>>();
            List<string> highlightFields = new List<string> { ESField.TEXT };

            ConfigureSearch(ESIndex.PAGES, returnFields, facetFields, highlightFields);
            ISearchResult result = _esSearch.SearchPage(query, searchLimits);

            // Add the query parameters to the result
            result.Query.Add(new Tuple<SearchField, string>(SearchField.All, query));
            result.QueryLimits = limits;

            return result;
        }

        /// <summary>
        /// Configure the search object with the specified search criteria
        /// </summary>
        private void ConfigureSearch(string indexName, List<string> returnFields,
            List<Tuple<string, ESFacetSortOrder>> facetFields, List<string> highlightFields)
        {
            _esSearch.SetSearchDefaults();
            _esSearch.IndexName = indexName;
            _esSearch.NumResults = _numResults;
            _esSearch.StartPage = _startPage;
            _esSearch.ReturnFields = returnFields;
            _esSearch.SortField = _esSortField;
            if (_facet) _esSearch.FacetFields = facetFields;
            if (_highlight) _esSearch.HighlightFields = highlightFields;
            _esSearch.Suggest = _suggest;
            _esSearch.Debug = ConfigurationManager.AppSettings["DebugSearch"].ToLower() == "true";
        }

        /// <summary>
        /// Convert the SortField enum to the appropriate ElasticSearch field name
        /// </summary>
        /// <param name="sortField"></param>
        /// <returns></returns>
        private string GetSortField(SortField sortField)
        {
            string field = ESSortField.SCORE;

            switch(_sortField)
            {
                case SortField.Score:
                    field = ESSortField.SCORE; break;
                case SortField.Author:
                    field = ESSortField.AUTHOR; break;
                case SortField.Date:
                    field = ESSortField.DATE; break;
                case SortField.Title:
                    field = ESSortField.TITLE; break;
                case SortField.Keyword:
                    field = ESSortField.KEYWORD; break;
                case SortField.Name:
                    field = ESSortField.NAME; break;
                case SortField.PrimaryAuthor:
                    field = ESSortField.PRIMARYAUTHOR; break;
                case SortField.Sequence:
                    field = ESSortField.SEQUENCE; break;
                default:
                    field = ESSortField.SCORE; break;
            }

            return field;
        }

        /// <summary>
        /// Convert a list of search limits from field enumeration values to ElasticSearch field names
        /// </summary>
        /// <param name="limits"></param>
        /// <returns></returns>
        private List<Tuple<string,string>> GetSearchLimitsList(List<Tuple<SearchField, string>> limits)
        {
            limits = limits ?? new List<Tuple<SearchField, string>>();

            List<Tuple<string, string>> searchLimits = new List<Tuple<string, string>>();

            foreach(Tuple<SearchField, string> limit in limits)
            {
                string fieldName = string.Empty;
                string fieldValue = limit.Item2;

                switch (limit.Item1)
                {
                    case SearchField.All:
                        fieldName = ESField.ALL; break;
                    case SearchField.FacetContributors:
                        fieldName = ESField.CONTRIBUTORS_RAW; break;
                    case SearchField.FacetDateRanges:
                        fieldName = ESField.DATERANGES; break;
                    case SearchField.FacetGenre:
                        fieldName = ESField.GENRE; break;
                    case SearchField.FacetItemAuthors:
                        fieldName = ESField.SEARCHAUTHORS; break;
                    case SearchField.FacetItemKeywords:
                        fieldName = ESField.KEYWORDS_RAW; break;
                    case SearchField.FacetLanguage:
                        fieldName = ESField.LANGUAGE; break;
                    case SearchField.FacetPageNames:
                        fieldName = ESField.NAMES_RAW; break;
                    case SearchField.FacetPageTypes:
                        fieldName = ESField.PAGETYPES; break;
                    case SearchField.AuthorNames:
                        fieldName = ESField.AUTHORNAMES; break;
                    case SearchField.Collections:
                        fieldName = ESField.COLLECTIONS; break;
                    case SearchField.Contributors:
                        fieldName = ESField.CONTRIBUTORS_RAW; break;
                    case SearchField.DateRanges:
                        fieldName = ESField.DATERANGES; break;
                    case SearchField.Dates:
                        fieldName = ESField.DATES; break;
                    case SearchField.Doi:
                        fieldName = ESField.DOI; break;
                    case SearchField.Genre:
                        fieldName = ESField.GENRE; break;
                    case SearchField.ISBN:
                        fieldName = ESField.ISBN; break;
                    case SearchField.ISSN:
                        fieldName = ESField.ISSN; break;
                    case SearchField.Issue:
                        fieldName = ESField.ISSUE; break;
                    case SearchField.ItemID:
                        fieldName = ESField.ITEMID; break;
                    case SearchField.ItemAuthors:
                        fieldName = ESField.SEARCHAUTHORS; break;
                    case SearchField.ItemKeywords:
                        fieldName = ESField.KEYWORDS; break;
                    case SearchField.Keyword:
                        fieldName = ESField.KEYWORD; break;
                    case SearchField.Language:
                        fieldName = ESField.LANGUAGE; break;
                    case SearchField.MaterialType:
                        fieldName = ESField.MATERIALTYPE; break;
                    case SearchField.Name:
                        fieldName = ESField.NAME; break;
                    case SearchField.Oclc:
                        fieldName = ESField.OCLC; break;
                    case SearchField.PageNames:
                        fieldName = ESField.NAMES; break;
                    case SearchField.PageTypes:
                        fieldName = ESField.PAGETYPES; break;
                    case SearchField.PublicationPlace:
                        fieldName = ESField.PUBLICATIONPLACE; break;
                    case SearchField.Publisher:
                        fieldName = ESField.PUBLISHER; break;
                    case SearchField.Series:
                        fieldName = ESField.SERIES; break;
                    case SearchField.Text:
                        fieldName = ESField.TEXT; break;
                    case SearchField.Title:
                        fieldName = ESField.TITLE; break;
                    case SearchField.Volume:
                        fieldName = ESField.VOLUME; break;
                    default:
                        fieldName = ESField.ALL; break;
                }

                Tuple<string, string> searchLimit = new Tuple<string, string>(fieldName, fieldValue);
                searchLimits.Add(searchLimit);
            }

            return searchLimits;
        }
    }
}
