using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Nest;
using Newtonsoft.Json.Serialization;

namespace BHL.Search.Elastic
{
    public class ESSearch : IESSearch
    {
        private ElasticClient _es = null;

        // Index to query
        private string _indexName = ESIndex.DEFAULT;

        // Index object type to query
        private string _typeName = ESType.ALL;

        // Fields to return.  Empty to return all.
        private List<string> _returnFields = new List<string>();

        // Fields to sort.  Empty for no sort.
        private string _sortField = ESSortField.SCORE;

        // Fields on which to facet.
        private List<Tuple<string, ESFacetSortOrder>> _facetFields = new List<Tuple<string, ESFacetSortOrder>>();

        // Fields in which to highlight results
        private List<string> _highlightFields = new List<string>();

        // True to suggest alternative searches
        bool _suggest = false;

        // Number of results to return
        private int _numResults = 100;

        // Return results starting at this page (_numResults = page size)
        private int _startPage = 1;

        // Number of values to return for each facet
        private int _numFacets = 30;

        // True to enable debugging output
        private bool _debug = false;

        public string IndexName
        {
            get { return _indexName; }
            set { _indexName = value ?? ESIndex.DEFAULT; }
        }

        public List<string> ReturnFields
        {
            get { return _returnFields; }
            set { _returnFields = value ?? new List<string>(); }
        }

        public string SortField
        {
            get { return _sortField; }
            set { _sortField = string.IsNullOrWhiteSpace(value) ? ESSortField.SCORE : value; }
        }

        public List<Tuple<string, ESFacetSortOrder>> FacetFields
        {
            get { return _facetFields; }
            set { _facetFields = value ?? new List<Tuple<string, ESFacetSortOrder>>(); }
        }

        public List<string> HighlightFields
        {
            get { return _highlightFields; }
            set { _highlightFields = value ?? new List<string>(); }
        }

        public bool Suggest
        {
            get { return _suggest; }
            set { _suggest = value; }
        }

        public int NumResults
        {
            get { return _numResults; }
            set { _numResults = value; }
        }

        public int StartPage
        {
            get { return _startPage; }
            set { _startPage = (value < 1 ? 1 : value); }
        }

        public int NumFacets
        {
            get { return _numFacets; }
            set { _numFacets = value; }
        }

        public bool Debug
        {
            get { return _debug; }
            set { _debug = value; }
        }

        public ESSearch(string connectionString)
        {
            // Establish a connection to an ElasticSearch server.
            // Defaults to http://locahost:9200 if no connection string supplied.
            ConnectionSettings connectionSettings = new ConnectionSettings(connectionString == null ? (Uri)null : new Uri(connectionString));
            connectionSettings.DefaultIndex(ESIndex.DEFAULT);
            connectionSettings.DisableDirectStreaming(true); // Uncomment this to add req/resp strings to response.debuginformation
            //connectionSettings.ThrowExceptions(true);      // Uncomment to debug uncaught ElasticSearch errors
            _es = new ElasticClient(connectionSettings);

            CheckServerStatus();
        }

        /// <summary>
        /// Initialize the object for a new query, without establishing an entirely new server connection
        /// </summary>
        public void SetSearchDefaults()
        {
            _indexName = ESIndex.DEFAULT;
            _typeName = ESType.ALL;
            _returnFields = new List<string>();
            _sortField = ESSortField.SCORE;
            _facetFields = new List<Tuple<string, ESFacetSortOrder>>();
            _highlightFields = new List<string>();
            _suggest = false;
            _numResults = 100;
            _startPage = 1;
        }

        /// <summary>
        /// Submit a query.  If limit values are specified, filter the results by those limits.
        /// For example, if query = "birds" with a limit of "searchAuthors:smith", this method
        /// will return the results of a search for items that match "birds" and have an author
        /// name like "smith".
        /// 
        /// Logically, the search will fit the following pattern:
        /// (query) AND limit1:"limitvalue1" AND limit2:"limitvalue2"
        /// </summary>
        /// <param name="query">Query string</param>
        /// <param name="limits">List of field/value pairs on which to limit the search</param>
        public SearchResult SearchAll(string query, List<Tuple<string, string>> limits = null)
        {
            ISearchResponse<dynamic> results = null;
            if (limits != null && limits.Count == 0) limits = null;

            if (!string.IsNullOrWhiteSpace(query))
            {
                // Initialize the query object
                SearchDescriptor<dynamic> searchDesc = InitializeQuery();

                // Remove operators from the end of the query string
                if (query.EndsWith(" AND", false, System.Globalization.CultureInfo.CurrentCulture) ||
                    query.EndsWith(" NOT", false, System.Globalization.CultureInfo.CurrentCulture)) query = query.Substring(0, query.Length - 4);
                if (query.EndsWith(" OR", false, System.Globalization.CultureInfo.CurrentCulture)) query = query.Substring(0, query.Length - 3);

                // Query used by the "suggest" feature should not include faceting values
                string suggestQuery = query;

                // Add limits to the query string.
                // A query string of "cat OR dog" and limits of "type=pet" and age=5 should produce this query:
                //      (cat OR dog) AND type:pet AND age:5
                string queryString = string.Empty;
                queryString = CleanQuery(query);

                List<QueryContainer> limitQueries = new List<QueryContainer>();
                if (limits != null)
                {
                    foreach (Tuple<string, string> limit in limits)
                    {
                        limitQueries.Add(new MatchPhraseQuery { Field = limit.Item1, Query = limit.Item2 });
                    }
                }

                // Fields to be searched
                List<Field> fields = new List<Field>();
                fields.Add(new Field("_all"));
                // Add fields to be boosted
                fields.Add(new Field(ESField.TITLE + "^15"));
                fields.Add(new Field(ESField.TITLE_ABBR + "^10"));
                fields.Add(new Field(ESField.ISSN + "^15"));
                fields.Add(new Field(ESField.ISBN + "^15"));
                fields.Add(new Field(ESField.OCLC + "^15"));
                fields.Add(new Field(ESField.DOI + "^15"));
                fields.Add(new Field(ESField.TRANSLATEDTITLE + "^15"));
                fields.Add(new Field(ESField.TRANSLATEDTITLE_ABBR + "^10"));
                fields.Add(new Field(ESField.UNIFORMTITLE + "^15"));
                fields.Add(new Field(ESField.UNIFORMTITLE_ABBR + "^10"));
                fields.Add(new Field(ESField.VARIANTS + "^15"));
                fields.Add(new Field(ESField.VARIANTS_ABBR + "^10"));
                fields.Add(new Field(ESField.ASSOCIATIONS + "^5"));
                fields.Add(new Field(ESField.ASSOCIATIONS_ABBR + "^3"));
                //fields.Add(new Field(ESField.COLLECTIONS + "^5"));
                fields.Add(new Field(ESField.CONTAINER + "^5"));
                fields.Add(new Field(ESField.CONTAINER_ABBR + "^3"));
                fields.Add(new Field(ESField.CONTRIBUTORS + "^5"));
                fields.Add(new Field(ESField.KEYWORDS + "^5"));
                fields.Add(new Field(ESField.PUBLICATIONPLACE + "^5"));
                fields.Add(new Field(ESField.PUBLISHER + "^5"));
                fields.Add(new Field(ESField.SEARCHAUTHORS + "^5"));
                fields.Add(new Field(ESField.NOTES));
                fields.Add(new Field(ESField.TEXT));

                // Construct the query.
                searchDesc.Query(b => b
                    .Bool(q => q
                        .Must(qs => qs
                            .QueryString(qu => qu
                                .Analyzer("default")
                                .Query(queryString)
                                .Fields(fields.ToArray())
                                .DefaultOperator(Operator.And)
                            )
                        )
                        .Filter(limitQueries.ToArray())
                    )                        
                );

                //// TODO: Validate the query string.  Check validateResponse.Valid for result.
                //var validateResponse = _es.ValidateQuery<dynamic>(descriptor => descriptor
                //    .Explain()
                //        .Query(q => q
                //            .QueryString(qu => qu
                //                .Query(query)
                //                .Fields(fields.ToArray())
                //                .DefaultOperator(Operator.And))));

                // Set sort, aggregate (facet), and highlight fields
                SetSortField(searchDesc);
                SetAggregateFields(searchDesc);
                SetHighlightFields(searchDesc);

                // Set the fields to use when determining alternate search suggestions
                if (_suggest)
                {
                    List<Tuple<string, string>> suggestFields = new List<Tuple<string, string>>();
                    suggestFields.Add(new Tuple<string, string>(ESField.ALL, CleanSuggestString(suggestQuery)));
                    SetSuggestFields(searchDesc, suggestFields);
                }

                // Execute the query
                results = ExecuteQuery(searchDesc);
                if (Debug) WriteDebuggingInfo(queryString, limits, results);
            }

            // Build and return the result object
            return GetSearchResult(results);
        }

        /// <summary>
        /// Submit an Item query that searches on all specified field/value combinations.
        /// If limit values are specified, results are filtered by those limits.
        /// </summary>
        /// <param name="query"></param>
        //public SearchResult SearchItem(List<Tuple<string, string>> args, List<Tuple<string, string>> limits = null)
        public SearchResult SearchCatalog(SearchStringParam title, SearchStringParam author, string volume, string year, 
            SearchStringParam keyword, string language, string collection, SearchStringParam notes, SearchStringParam text, 
            List<Tuple<string, string>> limits = null)
        {
            ISearchResponse<dynamic> results = null;
            if (limits != null && limits.Count == 0) limits = null;

            if (!string.IsNullOrWhiteSpace(title.searchValue) ||
                !string.IsNullOrWhiteSpace(author.searchValue) ||
                !string.IsNullOrWhiteSpace(volume) ||
                !string.IsNullOrWhiteSpace(year) ||
                !string.IsNullOrWhiteSpace(keyword.searchValue) ||
                !string.IsNullOrWhiteSpace(language) ||
                !string.IsNullOrWhiteSpace(collection) ||
                !string.IsNullOrWhiteSpace(notes.searchValue) ||
                !string.IsNullOrWhiteSpace(text.searchValue))
            {
                // Initialize the query object
                SearchDescriptor<dynamic> searchDesc = InitializeQuery();

                // Build the query.  Since we have search terms targeted to specific fields, use a
                // Boolean query instead of a Query_String query.
                List<QueryContainer> mustQueries = new List<QueryContainer>();
                List<QueryContainer> shouldQueries = new List<QueryContainer>();
                if (!string.IsNullOrWhiteSpace(title.searchValue))
                {
                    if (title.ParamOperator == SearchStringParamOperator.Phrase)
                    {
                        shouldQueries.Add(new MatchPhraseQuery { Field = ESField.TITLE, Query = CleanQuery(title.searchValue), Boost = 10 });
                        shouldQueries.Add(new MatchPhraseQuery { Field = ESField.ASSOCIATIONS, Query = CleanQuery(title.searchValue) });
                        shouldQueries.Add(new MatchPhraseQuery { Field = ESField.TRANSLATEDTITLE, Query = CleanQuery(title.searchValue), Boost = 10 });
                        shouldQueries.Add(new MatchPhraseQuery { Field = ESField.UNIFORMTITLE, Query = CleanQuery(title.searchValue), Boost = 10 });
                        shouldQueries.Add(new MatchPhraseQuery { Field = ESField.VARIANTS, Query = CleanQuery(title.searchValue), Boost = 10 });
                    }
                    else
                    {
                        Nest.Operator matchOperator = Operator.And;
                        if (title.ParamOperator == SearchStringParamOperator.Or) matchOperator = Operator.Or;
                        shouldQueries.Add(new MatchQuery { Field = ESField.TITLE, Analyzer = "default", Query = CleanQuery(title.searchValue), Boost = 15, Operator = matchOperator, Fuzziness = Fuzziness.EditDistance(0), PrefixLength = 3 });
                        shouldQueries.Add(new MatchQuery { Field = ESField.TITLE_ABBR, Analyzer = "default", Query = CleanQuery(title.searchValue), Boost = 10, Operator = matchOperator, Fuzziness = Fuzziness.EditDistance(0), PrefixLength = 3 });
                        shouldQueries.Add(new MatchQuery { Field = ESField.ASSOCIATIONS, Analyzer = "default", Query = CleanQuery(title.searchValue), Boost = 5, Operator = matchOperator, Fuzziness = Fuzziness.EditDistance(0), PrefixLength = 3 });
                        shouldQueries.Add(new MatchQuery { Field = ESField.ASSOCIATIONS_ABBR, Analyzer = "default", Query = CleanQuery(title.searchValue), Operator = matchOperator, Fuzziness = Fuzziness.EditDistance(0), PrefixLength = 3 });
                        shouldQueries.Add(new MatchQuery { Field = ESField.TRANSLATEDTITLE, Analyzer = "default", Query = CleanQuery(title.searchValue), Boost = 15, Operator = matchOperator, Fuzziness = Fuzziness.EditDistance(0), PrefixLength = 3 });
                        shouldQueries.Add(new MatchQuery { Field = ESField.TRANSLATEDTITLE_ABBR, Analyzer = "default", Query = CleanQuery(title.searchValue), Boost = 10, Operator = matchOperator, Fuzziness = Fuzziness.EditDistance(0), PrefixLength = 3 });
                        shouldQueries.Add(new MatchQuery { Field = ESField.UNIFORMTITLE, Analyzer = "default", Query = CleanQuery(title.searchValue), Boost = 15, Operator = matchOperator, Fuzziness = Fuzziness.EditDistance(0), PrefixLength = 3 });
                        shouldQueries.Add(new MatchQuery { Field = ESField.UNIFORMTITLE_ABBR, Analyzer = "default", Query = CleanQuery(title.searchValue), Boost = 10, Operator = matchOperator, Fuzziness = Fuzziness.EditDistance(0), PrefixLength = 3 });
                        shouldQueries.Add(new MatchQuery { Field = ESField.VARIANTS, Analyzer = "default", Query = CleanQuery(title.searchValue), Boost = 15, Operator = matchOperator, Fuzziness = Fuzziness.EditDistance(0), PrefixLength = 3 });
                        shouldQueries.Add(new MatchQuery { Field = ESField.VARIANTS_ABBR, Analyzer = "default", Query = CleanQuery(title.searchValue), Boost = 10, Operator = matchOperator, Fuzziness = Fuzziness.EditDistance(0), PrefixLength = 3 });
                    }
                }

                if (!string.IsNullOrWhiteSpace(author.searchValue))
                {
                    if (author.ParamOperator == SearchStringParamOperator.Phrase)
                    {
                        mustQueries.Add(new MatchPhraseQuery { Field = ESField.SEARCHAUTHORS, Query = CleanQuery(author.searchValue) });
                    }
                    else
                    {
                        Nest.Operator matchOperator = Operator.And;
                        if (author.ParamOperator == SearchStringParamOperator.Or) matchOperator = Operator.Or;
                        mustQueries.Add(new MatchQuery { Field = ESField.SEARCHAUTHORS, Query = CleanQuery(author.searchValue), Operator = matchOperator, Fuzziness = Fuzziness.EditDistance(0), PrefixLength = 3 });
                    }
                }

                if (!string.IsNullOrWhiteSpace(volume)) mustQueries.Add(new MatchQuery { Field = ESField.VOLUME, Query = CleanQuery(volume) });
                if (!string.IsNullOrWhiteSpace(year)) mustQueries.Add(new MatchQuery { Field = ESField.DATES, Query = CleanQuery(year) });

                if (!string.IsNullOrWhiteSpace(keyword.searchValue))
                {
                    if (keyword.ParamOperator == SearchStringParamOperator.Phrase)
                    {
                        mustQueries.Add(new MatchPhraseQuery { Field = ESField.KEYWORDS, Query = CleanQuery(keyword.searchValue) });
                    }
                    else
                    {
                        Nest.Operator matchOperator = Operator.And;
                        if (keyword.ParamOperator == SearchStringParamOperator.Or) matchOperator = Operator.Or;
                        mustQueries.Add(new MatchQuery { Field = ESField.KEYWORDS, Query = CleanQuery(keyword.searchValue), Operator = matchOperator, Fuzziness = Fuzziness.EditDistance(0), PrefixLength = 3 });
                    }
                }

                if (!string.IsNullOrWhiteSpace(language)) mustQueries.Add(new MatchQuery { Field = ESField.LANGUAGE, Query = language });
                if (!string.IsNullOrWhiteSpace(collection)) mustQueries.Add(new MatchQuery { Field = ESField.COLLECTIONS, Query = collection });

                if (!string.IsNullOrWhiteSpace(notes.searchValue))
                {
                    if (notes.ParamOperator == SearchStringParamOperator.Phrase)
                    {
                        mustQueries.Add(new MatchPhraseQuery { Field = ESField.NOTES, Query = CleanQuery(notes.searchValue) });
                    }
                    else
                    {
                        Nest.Operator matchOperator = Operator.And;
                        if (notes.ParamOperator == SearchStringParamOperator.Or) matchOperator = Operator.Or;
                        mustQueries.Add(new MatchQuery { Field = ESField.NOTES, Query = CleanQuery(notes.searchValue), Operator = matchOperator, Fuzziness = Fuzziness.EditDistance(0), PrefixLength = 3 });
                    }
                }

                if (!string.IsNullOrWhiteSpace(text.searchValue))
                {
                    if (text.ParamOperator == SearchStringParamOperator.Phrase)
                    {
                        mustQueries.Add(new MatchPhraseQuery { Field = ESField.TEXT, Query = CleanQuery(text.searchValue) });
                    }
                    else
                    {
                        Nest.Operator matchOperator = Operator.And;
                        if (text.ParamOperator == SearchStringParamOperator.Or) matchOperator = Operator.Or;
                        mustQueries.Add(new MatchQuery { Field = ESField.TEXT, Query = CleanQuery(text.searchValue), Operator = matchOperator, Fuzziness = Fuzziness.EditDistance(0), PrefixLength = 3 });
                    }
                }

                if (limits != null)
                {
                    foreach (Tuple<string, string> limit in limits)
                    {
                        mustQueries.Add(new MatchQuery { Field = limit.Item1, Query = limit.Item2, Operator = Operator.And });
                    }
                }

                // Set MinimumShouldMatch to ensure that at least one of the "Should" conditions matches
                searchDesc.Query(q => q
                    .Bool(b => b
                        .Should(shouldQueries.ToArray())
                        .MinimumShouldMatch(shouldQueries.Count > 0 ? 1 : 0)
                        .Must(mustQueries.ToArray())
                    )
                );

                // Set sort, aggregate (facet), and highlight fields
                SetSortField(searchDesc);
                SetAggregateFields(searchDesc);
                SetHighlightFields(searchDesc);

                List<Tuple<string, string>> args = new List<Tuple<string, string>>();
                if (!string.IsNullOrWhiteSpace(title.searchValue)) args.Add(new Tuple<string, string>(ESField.TITLE, title.searchValue));
                if (!string.IsNullOrWhiteSpace(author.searchValue)) args.Add(new Tuple<string, string>(ESField.SEARCHAUTHORS, author.searchValue));
                if (!string.IsNullOrWhiteSpace(volume)) args.Add(new Tuple<string, string>(ESField.VOLUME, volume));
                if (!string.IsNullOrWhiteSpace(year)) args.Add(new Tuple<string, string>(ESField.DATES, year));
                if (!string.IsNullOrWhiteSpace(keyword.searchValue)) args.Add(new Tuple<string, string>(ESField.KEYWORDS, keyword.searchValue));
                if (!string.IsNullOrWhiteSpace(language)) args.Add(new Tuple<string, string>(ESField.LANGUAGE, language));
                if (!string.IsNullOrWhiteSpace(collection)) args.Add(new Tuple<string, string>(ESField.COLLECTIONS, collection));
                if (!string.IsNullOrWhiteSpace(notes.searchValue)) args.Add(new Tuple<string, string>(ESField.NOTES, notes.searchValue));
                if (!string.IsNullOrWhiteSpace(text.searchValue)) args.Add(new Tuple<string, string>(ESField.TEXT, text.searchValue));

                // Set the fields to use when determining alternate search suggestions
                if (_suggest)
                {
                    SetSuggestFields(searchDesc, args);
                }

                // Execute the query
                results = ExecuteQuery(searchDesc);
                if (Debug) WriteDebuggingInfo(args, limits, results);
            }

            // Build and return the result object
            return GetSearchResult(results);
        }

        public SearchResult SearchAuthor(string query)
        {
            return SearchSimple(query, new List<string> { ESField.ALL }, null);
        }

        public SearchResult SearchKeyword(string query)
        {
            return SearchSimple(query, new List<string> { ESField.ALL }, null);
        }

        public SearchResult SearchName(string query)
        {
            return SearchSimple(query, new List<string> { ESField.ALL }, null);
        }

        public SearchResult SearchPage(string query, List<Tuple<string, string>> limits = null)
        {
            return SearchSimple(query, new List<string> { ESField.ALL, ESField.TEXT }, limits);
        }

        /// <summary>
        /// Provides "simple" search over the authors, keywords, names, or pages index.
        /// </summary>
        /// <param name="query"></param>
        /// <param name="limits"></param>
        /// <returns></returns>
        public SearchResult SearchSimple(string query, List<string> queryFields, 
            List<Tuple<string, string>> limits = null)
        {
            ISearchResponse<dynamic> results = null;
            if (limits != null && limits.Count == 0) limits = null;

            if (!string.IsNullOrWhiteSpace(query))
            {
                // Initialize the query object
                SearchDescriptor<dynamic> searchDesc = InitializeQuery();

                // Remove operators from the end of the query string
                if (query.EndsWith(" AND", false, System.Globalization.CultureInfo.CurrentCulture) ||
                    query.EndsWith(" NOT", false, System.Globalization.CultureInfo.CurrentCulture)) query = query.Substring(0, query.Length - 4);
                if (query.EndsWith(" OR", false, System.Globalization.CultureInfo.CurrentCulture)) query = query.Substring(0, query.Length - 3);

                // Query used by the "suggest" feature should not include faceting values
                string suggestQuery = query;

                // Add limits to the query string.
                // A query string of "cat OR dog" and limits of "type=pet" and age=5 should produce this query:
                //      (cat OR dog) AND type:pet AND age:5
                string queryString = string.Empty;
                queryString = CleanQuery(query);

                List<QueryContainer> limitQueries = new List<QueryContainer>();
                if (limits != null)
                {
                    foreach (Tuple<string, string> limit in limits)
                    {
                        limitQueries.Add(new MatchPhraseQuery { Field = limit.Item1, Query = limit.Item2 });
                    }
                }

                List<Field> fields = new List<Field>();
                foreach (string field in queryFields)
                {
                    fields.Add(new Field(field));
                }
                // If necessary, add fields to be boosted here - i.e. "fields.Add(new Field("title^2"))

                // Construct the query.
                searchDesc.Query(b => b
                    .Bool(q => q
                        .Must(qs => qs
                            .QueryString(qu => qu
                                .Query(queryString)
                                .Fields(fields.ToArray())
                                .DefaultOperator(Operator.And)
                            )
                        )
                        .Filter(limitQueries.ToArray())
                    )
                );

                //// TODO: Validate the query string.  Check validateResponse.Valid for result.
                //var validateResponse = _es.ValidateQuery<dynamic>(descriptor => descriptor
                //    .Explain()
                //        .Query(q => q
                //            .QueryString(qu => qu
                //                .Query(query)
                //                .Fields(fields.ToArray())
                //                .DefaultOperator(Operator.And))));

                // Set sort, aggregate (facet), and highlight fields
                SetSortField(searchDesc);
                SetAggregateFields(searchDesc);
                SetHighlightFields(searchDesc);

                // Set the fields to use when determining alternate search suggestions
                if (_suggest)
                {
                    List<Tuple<string, string>> suggestFields = new List<Tuple<string, string>>();
                    suggestFields.Add(new Tuple<string, string>(ESField.ALL, CleanSuggestString(suggestQuery)));
                    SetSuggestFields(searchDesc, suggestFields);
                }

                // Execute the query
                results = ExecuteQuery(searchDesc);
                if (Debug) WriteDebuggingInfo(queryString, null, results);
            }

            // Build and return the result object
            return GetSearchResult(results);
        }

        #region Boolean Query Implementations (Removed)

        /*

        /// <summary>
        /// Submit a query.  If limit values are specified, filter the results by those limits.
        /// For example, if query = "birds" with a limit of "searchAuthors:smith", this method
        /// will return the results of a search for items that match "birds" and have an author
        /// name like "smith".
        /// 
        /// Logically, the search will fit the following pattern:
        /// (query) AND (limit1:limitvalue1 AND limit2:limitvalue2)
        /// </summary>
        /// <param name="query">Query string</param>
        /// <param name="limits">List of field/value pairs on which to limit the search</param>
        public SearchResult SearchCatalog(string query, List<Tuple<string, string>> limits = null)
        {
            // * The following queries the _all field, while also applying boost values to
            // * the "title" and "text" fields.  _all and the boost fields are combined into
            // * a "should" boolean query (where the minimum is 1), and the limiting/faceting
            // * fields are combined into a "must" boolean query.
            // * Example raw ElasticSearch query:
            //  POST items/_search
            //  {
            //    "query": {
            //      "bool": {
            //        "should": [
            //          { "match": { "_all": { "query": "fish" } } },
            //          { "match": { "title": { "query": "fish", "boost": "2" } } },
            //          { "match": { "text": { "query": "fish", "boost": "-2" } } }
            //        ],
            //        "minimum_number_should_match": 1, 
            //        "must": [
            //          { "match": { "language": { "query": "English" } } },
            //          { "match": { "collections": { "query": "MBLWHOI Library, Woods Hole" } } }
            //        ]      
            //      }
            //    }
            //  }

            // Initialize the query object
            SearchDescriptor<dynamic> searchDesc = InitializeQuery();

            // Build the limit queries
            // Field = field in which to search, Query = term/phrase for which to search
            List<QueryContainer> mustQueries = new List<QueryContainer>();
            if (limits != null)
            {
                foreach (Tuple<string, string> limit in limits)
                {
                    mustQueries.Add(new MatchQuery { Field = limit.Item1, Query = limit.Item2 });
                }
            }

            List<QueryContainer> shouldQueries = new List<QueryContainer>();
            shouldQueries.Add(new MatchQuery { Field = ESField.ALL, Query = query });
            shouldQueries.Add(new MatchQuery { Field = ESField.TITLE, Query = query, Boost = 2 });
            shouldQueries.Add(new MatchQuery { Field = ESField.TEXT, Query = query, Boost = -2 });

            // Need to include highlight fields in the Should clause in order for highlighting to work correctly
            foreach (string highlight in _highlightFields)
            {
                shouldQueries.Add(new MatchQuery { Field = highlight, Query = query });
            }

            // Construct the complete query.
            // Set MinimumShouldMatch to ensure that at least one of the "Should" conditions matches
            searchDesc.Query(q => q
                .Bool(b => b
                    .Should(shouldQueries.ToArray())
                    .MinimumShouldMatch(1)
                    .Must(mustQueries.ToArray())
                )
            );

            //// TODO: Rather than trying to parse user-supplied query strings, let ES do it.  Check validateResponse.Valid for result.
            //var validateResponse = _es.ValidateQuery<dynamic>(descriptor => descriptor
            //    .Explain()
            //    .Query(q => q
            //    .Bool(b => b
            //        .Should(shouldQueries.ToArray())
            //        .MinimumShouldMatch(1)
            //        .Must(mustQueries.ToArray()))));

            // Set sort, aggregate (facet), and highlight fields
            SetSortField(searchDesc);
            SetAggregateFields(searchDesc);
            SetHighlightFields(searchDesc);

            // Set the fields to use when determining alternate search suggestions
            if (_suggest)
            {
                List<Tuple<string, string>> suggestFields = new List<Tuple<string, string>>();
                suggestFields.Add(new Tuple<string, string>(ESField.ALL, CleanSuggestString(query)));
                SetSuggestFields(searchDesc, suggestFields, limits);
            }

            // Execute the query
            ISearchResponse<dynamic> results = ExecuteQuery(searchDesc);
            if (Debug) WriteDebuggingInfo(query, limits, results);

            // Build and return the result object
            return GetSearchResult(results);
        }

        public SearchResult SearchAuthor(string query)
        {
            return SearchSimple(query, null);
        }

        public SearchResult SearchKeyword(string query)
        {
            return SearchSimple(query, null);
        }

        public SearchResult SearchName(string query)
        {
            return SearchSimple(query, null);
        }

        public SearchResult SearchPage(string query, List<Tuple<string, string>> limits = null)
        {
            return SearchSimple(query, limits);
        }

        /// <summary>
        /// Provides "simple" search over the authors, keywords, names, or pages index.
        /// </summary>
        /// <param name="query"></param>
        /// <param name="limits"></param>
        /// <returns></returns>
        public SearchResult SearchSimple(string query, List<Tuple<string, string>> limits = null)
        {
            // Initialize the query object
            SearchDescriptor<dynamic> searchDesc = InitializeQuery();

            // Build the limit queries
            List<QueryContainer> mustQueries = new List<QueryContainer>();
            //limits = limits ?? new List<Tuple<string, string>>();
            if (limits != null)
            {
                foreach (Tuple<string, string> limit in limits)
                {
                    mustQueries.Add(new MatchQuery { Field = limit.Item1, Query = limit.Item2 });
                }
            }

            // Need to include highlight fields in the query in order for highlighting to work correctly
            List<QueryContainer> shouldQueries = new List<QueryContainer>();
            foreach (string highlight in _highlightFields)
            {
                shouldQueries.Add(new MatchQuery { Field = highlight, Query = query });
            }

            mustQueries.Add(new MatchQuery { Field = ESField.ALL, Query = query });

            // Construct the complete query.
            searchDesc.Query(q => q
                .Bool(b => b
                    .Must(mustQueries.ToArray())
                    .Should(shouldQueries.ToArray())
                    .MinimumShouldMatch(0)
                )
            );

            //// TODO: Rather than trying to parse user-supplied query strings, let ES do it.  Check validateResponse.Valid for result.
            //var validateResponse = _es.ValidateQuery<dynamic>(descriptor => descriptor
            //    .Explain()
            //    .Query(q => q
            //    .Bool(b => b
            //        .Must(mustQueries.ToArray())
            //        .Should(shouldQueries.ToArray())
            //        .MinimumShouldMatch(0))));

            // Set sort, aggregate (facet), and highlight fields
            SetSortField(searchDesc);
            SetAggregateFields(searchDesc);
            SetHighlightFields(searchDesc);

            // Set the fields to use when determining alternate search suggestions
            if (_suggest)
            {
                List<Tuple<string, string>> suggestFields = new List<Tuple<string, string>>();
                suggestFields.Add(new Tuple<string, string>(ESField.ALL, CleanSuggestString(query)));
                SetSuggestFields(searchDesc, suggestFields);
            }

            // Execute the query
            ISearchResponse<dynamic> results = ExecuteQuery(searchDesc);
            if (Debug) WriteDebuggingInfo(query, null, results);

            // Build and return the result object
            return GetSearchResult(results);
        }

        */

        #endregion Boolean Query Implementations (Removed)

        private SearchDescriptor<dynamic> InitializeQuery()
        {
            // Construct the query
            SearchDescriptor<dynamic> searchDesc = new SearchDescriptor<dynamic>();
            searchDesc.Index(_indexName);
            searchDesc.From((_startPage - 1) * _numResults);
            searchDesc.Size(_numResults);               // Max number of results to return
            searchDesc.TrackTotalHits(true);            // Accurately count the number of hits if > 10000
            searchDesc.Timeout("30s");                  // Query timeout (valid units are d, h, m, s, ms, micros, nanos)
            searchDesc.Source(f => f                    // Fields to return
                .Includes(fi => fi
                    .Fields(_returnFields.ToArray())));
            return searchDesc;
        }

        private void SetSortField(SearchDescriptor<dynamic> searchDesc)
        {
            if (_sortField == ESSortField.SCORE)
                searchDesc.Sort(s => s.Descending(_sortField));
            else
                searchDesc.Sort(s => s.Ascending(_sortField));
        }

        private void SetSuggestFields(SearchDescriptor<dynamic> searchDesc,
            List<Tuple<string, string>> args, 
            List<Tuple<string, string>> limits = null)
        {
            var suggestFuncs = new List<Func<SuggestContainerDescriptor<dynamic>, IPromise<ISuggestContainer>>>();
            int argNum = 1;
            foreach (Tuple<string, string> arg in args)
            {
                string suggestKey = arg.Item1 + "." + argNum.ToString();
                suggestFuncs.Add(s => s.Term(suggestKey, t => t.Field(arg.Item1).Text(arg.Item2).Size(3)));
                argNum++;
            }
            if (limits != null)
            {
                foreach (Tuple<string, string> limit in limits)
                {
                    string suggestKey = limit.Item1 + "." + limit.ToString();
                    suggestFuncs.Add(s => s.Term(suggestKey, t => t.Field(limit.Item1).Text(limit.Item2).Size(3)));
                    argNum++;
                }
            }

            searchDesc.Suggest(sugDescriptor =>
                {
                    suggestFuncs.ForEach(sug => sug(sugDescriptor));
                    return sugDescriptor;
                }
            );
        }

        /// <summary>
        /// Remove "noise" from the term that ElasticSearch's "suggest" feature will evaluate.
        /// "suggest" does not recognize AND/OR/NOT as operators, so remove them.
        /// Punctuation (paretheses, quotes, etc) is ignored, so no need to remove.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        private string CleanSuggestString(string query)
        {
            string cleanSuggestQuery = string.Empty;
            cleanSuggestQuery = query.Replace(" AND ", " ").Replace(" OR ", " ").Replace(" NOT ", " ");
            return cleanSuggestQuery;
        }

        /// <summary>
        /// Remove any ElasticSearch special characters and punctuation in the query.
        /// ElasticSearch special characters that are affected are: + - & | ! { } [ ] ~ \ ^ ? : \\ /
        /// ElasticSearch special characters that are NOT affected are: ( ) *
        /// Punctuation that will be affected are: . , ; @ # $ % = < >
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        private string CleanQuery(string query)
        {
            // Remove any ellipsis
            string queryToClean = (query.Replace("...", "").Length > 0 ? query.Replace("...", " ") : query);

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string[] special = { "+", "-", "&", "|", "!", "{", "}", "[", "]", "~", "\\", "^", "?", ":", "/"};
            string[] punctuation = { ",", ";", "@", "#", "$", "%", "=", "<", ">" };
            foreach (char letter in queryToClean)
            {
                // Use this to escape special characters
                //sb.Append(Array.IndexOf(special, letter.ToString()) >= 0 ? "\\" + letter.ToString() : letter.ToString());

                // Use this to remove special characters and punctuation
                sb.Append(
                    Array.IndexOf(special, letter.ToString()) >= 0 || Array.IndexOf(punctuation, letter.ToString()) >= 0
                    ? " "
                    : letter.ToString());
            }
            return sb.ToString();
        }

        private void SetAggregateFields(SearchDescriptor<dynamic> searchDesc)
        {
            var aggFuncs = new List<Func<AggregationContainerDescriptor<dynamic>, IAggregationContainer>>();
            foreach (Tuple<string, ESFacetSortOrder> facet in _facetFields)
            {
                aggFuncs.Add(a => a
                    .Terms(facet.Item1, t => t
                        .Field(facet.Item1)
                        .Size(_numFacets)
                        .Order(f => facet.Item2 == ESFacetSortOrder.COUNT ? f.CountDescending() : f.KeyAscending())));
                        //.Order(facet.Item2 == ESFacetSortOrder.COUNT ? TermsOrder.CountDescending : TermsOrder.KeyAscending)));
            }

            searchDesc.Aggregations(aggDescriptor =>
                {
                    aggFuncs.ForEach(agg => agg(aggDescriptor));
                    return aggDescriptor;
                }
            );
        }

        private void SetHighlightFields(SearchDescriptor<dynamic> searchDesc)
        {
            var highlightFieldFuncs = new List<Func<HighlightFieldDescriptor<dynamic>, IHighlightField>>();
            foreach (string highlight in _highlightFields)
            {
                highlightFieldFuncs.Add(f => f.Field(highlight));
            }

            if (highlightFieldFuncs.Count > 0)
            {
                searchDesc.Highlight(h => h
                    .Fields(highlightFieldFuncs.ToArray())
                    .PreTags("<b>")
                    .PostTags("</b>")
                    .NumberOfFragments(5)
                );
            }
        }

        private ISearchResponse<dynamic> ExecuteQuery(SearchDescriptor<dynamic> searchDesc)
        {
            CheckServerStatus();
            ISearchResponse<dynamic> results = _es.Search<dynamic>(searchDesc);
            if (!results.IsValid || 
                results.TimedOut || 
                results.TerminatedEarly || 
                results.Shards.Failures.Count > 0) ProcessError(results);
            return results;
        }

        /// <summary>
        /// Extract the search results from the ElasticSearch/NEST objects and package them in a SearchResult object.
        /// </summary>
        /// <param name="results"></param>
        /// <returns></returns>
        private SearchResult GetSearchResult(ISearchResponse<dynamic> results)
        {
            SearchResult result = new SearchResult();
            result.PageSize = this._numResults;
            result.StartPage = this._startPage;

            if (results != null)
            {
                // Get metadata about search results
                result.TotalHits = results.HitsMetadata.Total.Value;
                result.TotalPages = (result.TotalHits / (long)result.PageSize) + 1;
                result.IsValid = results.IsValid;
                if (_debug || !results.IsValid) result.DebugInfo = results.DebugInformation;

                if (results.OriginalException != null)
                    result.ErrorMessage = results.OriginalException.Message;
                else if (results.ServerError != null)
                    result.ErrorMessage = results.ServerError.Error.Reason;

                // Get the search hits
                foreach (var hit in results.Hits)
                {
                    //switch (hit.Type)
                    //{
                    if (hit.Index.ToLower() == ESIndex.CATALOG.ToLower())
                    {
                        //case ESType.CATALOGITEM:
                        //string title = hit.Source.title;
                        ItemHit item = HitToObject<ItemHit>(hit);
                        //ItemHit item = hit.Source.ToObject<ItemHit>();
                        item.Score = hit.Score;
                        item.Highlights = GetHighlights(hit);
                        result.Items.Add(item);
                        //break;
                    }
                    if (hit.Index.ToLower() == ESIndex.ITEMS.ToLower())
                    {
                        //case ESType.ITEM:
                        //string title = hit.Source.title;
                        ItemHit item = HitToObject<ItemHit>(hit);
                        //ItemHit item = hit.Source.ToObject<ItemHit>();
                        item.Score = hit.Score;
                        item.Highlights = GetHighlights(hit);
                        result.Items.Add(item);
                        //break;
                    }
                    if (hit.Index.ToLower() == ESIndex.PAGES.ToLower())
                    {
                        //case ESType.PAGE:
                        PageHit page = HitToObject<PageHit>(hit);
                        //PageHit page = hit.Source.ToObject<PageHit>();
                        page.Score = hit.Score;
                        page.Highlights = GetHighlights(hit);
                        result.Pages.Add(page);
                        //break;
                    }
                    if (hit.Index.ToLower() == ESIndex.NAMES.ToLower())
                    {
                        //case ESType.NAME:
                        NameHit name = HitToObject<NameHit>(hit);
                        //NameHit name = hit.Source.ToObject<NameHit>();
                        name.Score = hit.Score;
                        name.Highlights = GetHighlights(hit);
                        result.Names.Add(name);
                        //break;
                    }
                    if (hit.Index.ToLower() == ESIndex.AUTHORS.ToLower())
                    {
                        //case ESType.AUTHOR:
                        AuthorHit author = HitToObject<AuthorHit>(hit);
                        //AuthorHit author = hit.Source.ToObject<AuthorHit>();
                        author.Score = hit.Score;
                        author.Highlights = GetHighlights(hit);
                        result.Authors.Add(author);
                        //break;
                    }
                    if (hit.Index.ToLower() == ESIndex.KEYWORDS.ToLower())
                    {
                        //case ESType.KEYWORD:
                        KeywordHit keyword = HitToObject<KeywordHit>(hit);
                        //KeywordHit keyword = hit.Source.ToObject<KeywordHit>();
                        keyword.Score = hit.Score;
                        keyword.Highlights = GetHighlights(hit);
                        result.Keywords.Add(keyword);
                        //break;
                    }
                    //}
                }

                // Get facets
                foreach (var agg in results.Aggregations)
                {
                    BucketAggregate aggBucket = (BucketAggregate)agg.Value;
                    foreach (KeyedBucket<object> bucket in aggBucket.Items)
                    {
                        string facetCategory = agg.Key;
                        SearchField facetCategoryEnum = GetSearchFieldEnum(facetCategory);

                        if (result.Facets.ContainsKey(facetCategoryEnum))
                        {
                            result.Facets[facetCategoryEnum].Add(bucket.Key.ToString(), bucket.DocCount);
                        }
                        else
                        {
                            Dictionary<string, long?> facetValue = new Dictionary<string, long?>();
                            facetValue.Add(bucket.Key.ToString(), bucket.DocCount);
                            result.Facets.Add(facetCategoryEnum, facetValue);
                        }
                    }
                }

                // Get suggestions
                foreach(var suggestKey in results.Suggest.Keys)
                //foreach (var suggestResults in results.Suggest)
                {
                    foreach (var suggestion in results.Suggest[suggestKey])
                    //foreach (var suggestion in suggestResults.Value)
                    {
                        foreach (var option in suggestion.Options)
                        {
                            SearchField suggestField = GetSearchFieldEnum(suggestKey);
                            //SearchField suggestKey = GetSearchFieldEnum(suggestResults.Key);
                            if (result.Suggestions.ContainsKey(suggestField))
                            //if (result.Suggestions.ContainsKey(suggestKey))
                            {
                                if (!result.Suggestions[suggestField].Contains(option.Text))
                                    result.Suggestions[suggestField].Add(option.Text);
                                //if (!result.Suggestions[suggestKey].Contains(option.Text))
                                //    result.Suggestions[suggestKey].Add(option.Text);
                            }
                            else
                            {
                                result.Suggestions.Add(suggestField, new List<string> { option.Text });
                                //result.Suggestions.Add(suggestKey, new List<string> { option.Text });
                            }

                        }
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Deserialize the specified hit into a particular Hit object
        /// </summary>
        /// <remarks>
        /// An alternate to this method is to install the NEST.JsonNetSerializer nuget package
        /// and allow it to do the deserialization (via Json.NET).  After installing the package,
        /// set up the ElasticSearch connection like so:
        /// 
        ///     var pool = new SingleNodeConnectionPool(new Uri("http://localhost:9200"));
        ///     var connectionSettings = new ConnectionSettings(pool, sourceSerializer: JsonNetSerializer.Default);
        ///     var client = new ElasticClient(connectionSettings);
        /// 
        /// Then, instead of calling this method this way:
        /// 
        ///     AuthorHit author = HitToObject<AuthorHit>(hit);
        ///     
        /// Do this instead:
        /// 
        ///     AuthorHit author = hit.Source.ToObject<AuthorHit>();
        /// 
        /// </remarks>
        /// <typeparam name="T"></typeparam>
        /// <param name="hit"></param>
        /// <returns></returns>
        private T HitToObject<T>(dynamic hit)
        {
            dynamic oHit = new Hit();
            if (typeof(T).ToString().Contains("AuthorHit"))
            {
                oHit = new AuthorHit();
                oHit.PrimaryAuthorName = (string)((Dictionary<string, object>)hit.Source)["primaryAuthorName"];
                List<object> names = (List<object>)((Dictionary<string, object>)hit.Source)["authorNames"];
                foreach (string name in names) oHit.AuthorNames.Add(name);
            }
            if (typeof(T).ToString().Contains("KeywordHit"))
            {
                oHit = new KeywordHit();
                oHit.Keyword = (string)((Dictionary<string, object>)hit.Source)["keyword"];
            }
            if (typeof(T).ToString().Contains("NameHit"))
            {
                oHit = new NameHit();
                oHit.Name = (string)((Dictionary<string, object>)hit.Source)["name"];
                oHit.Count = Convert.ToInt32(((Dictionary<string, object>)hit.Source)["count"]);
            }
            if (typeof(T).ToString().Contains("PageHit"))
            {
                oHit = new PageHit();
                oHit.ItemId = Convert.ToInt32(((Dictionary<string, object>)hit.Source)["itemId"]);
                oHit.Sequence = Convert.ToInt32(((Dictionary<string, object>)hit.Source)["sequence"]);
                List<object> indicators = (List<object>)((Dictionary<string, object>)hit.Source)["pageIndicators"];
                foreach (string indicator in indicators) oHit.pageIndicators.Add(indicator);
                List<object> types = (List<object>)((Dictionary<string, object>)hit.Source)["pageTypes"];
                foreach (string type in types) oHit.PageTypes.Add(type);
            }
            if (typeof(T).ToString().Contains("ItemHit"))
            {
                oHit = new ItemHit();
                oHit.TitleId = Convert.ToInt32(((Dictionary<string, object>)hit.Source)["titleId"]);
                oHit.ItemId = Convert.ToInt32(((Dictionary<string, object>)hit.Source)["itemId"]);
                oHit.SegmentId = Convert.ToInt32(((Dictionary<string, object>)hit.Source)["segmentId"]);
                oHit.StartPageId = Convert.ToInt32(((Dictionary<string, object>)hit.Source)["startPageId"]);
                oHit.Title = (string)((Dictionary<string, object>)hit.Source)["title"];
                oHit.TranslatedTitle = (string)GetHitValue(hit.Source, "translatedTitle");
                oHit.UniformTitle = (string)GetHitValue(hit.Source, "uniformTitle");
                oHit.SortTitle = (string)GetHitValue(hit.Source, "sortTitle");
                oHit.Genre = (string)GetHitValue(hit.Source, "genre");
                oHit.MaterialType = (string)GetHitValue(hit.Source, "materialType");
                oHit.Volume = (string)GetHitValue(hit.Source, "volume");
                oHit.Issue = (string)GetHitValue(hit.Source, "issue");
                oHit.Series = (string)GetHitValue(hit.Source, "series");
                oHit.Publisher = (string)GetHitValue(hit.Source, "publisher");
                oHit.PublicationPlace = (string)GetHitValue(hit.Source, "publicationPlace");
                oHit.Language = (string)GetHitValue(hit.Source, "language");
                oHit.Doi = (string)GetHitValue(hit.Source, "doi");
                oHit.Url = (string)GetHitValue(hit.Source, "url");
                oHit.Container = (string)GetHitValue(hit.Source, "container");
                oHit.PageRange = (string)GetHitValue(hit.Source, "pageRange");
                oHit.Text = (string)GetHitValue(hit.Source, "text");
                oHit.HasSegments = (bool)(GetHitValue(hit.Source, "hasSegments") ?? false);
                oHit.HasLocalContent = (bool)(GetHitValue(hit.Source, "hasLocalContent") ?? false);
                oHit.HasExternalContent = (bool)(GetHitValue(hit.Source, "hasExternalContent") ?? false);
                oHit.Authors = GetHitValueList<string>(hit.Source, "authors");
                oHit.SearchAuthors = GetHitValueList<string>(hit.Source, "searchAuthors");
                oHit.Keywords = GetHitValueList<string>(hit.Source, "keywords");
                oHit.Associations = GetHitValueList<string>(hit.Source, "associations");
                oHit.Variants = GetHitValueList<string>(hit.Source, "variants");
                oHit.Contributors = GetHitValueList<string>(hit.Source, "contributors");
                oHit.Notes = GetHitValueList<string>(hit.Source, "notes");
                oHit.Dates = GetHitValueList<string>(hit.Source, "dates");
                oHit.DateRanges = GetHitValueList<string>(hit.Source, "dateRanges");
                oHit.Oclc = GetHitValueList<string>(hit.Source, "oclc");
                oHit.Issn = GetHitValueList<string>(hit.Source, "issn");
                oHit.Isbn = GetHitValueList<string>(hit.Source, "isbn");
                oHit.Collections = GetHitValueList<string>(hit.Source, "collections");
            }

            oHit.Id = (((Dictionary<string, object>)hit.Source)["id"]).ToString();

            return oHit;
        }

        private object GetHitValue(Dictionary<string, object> hit, string fieldName)
        {
            if (hit.ContainsKey(fieldName))
                return hit[fieldName];
            else
                return null as object;
        }
        private List<T> GetHitValueList<T>(Dictionary<string, object> hit, string fieldName)
        {
            List<T> values = new List<T>();
            if (hit.ContainsKey(fieldName))
            {
                foreach (T value in (List<object>)hit[fieldName]) values.Add(value);
            }
            return values;
        }

        /// <summary>
        /// Extract the highlights from the specified ElasticSearch hit
        /// </summary>
        /// <param name="hit"></param>
        /// <returns></returns>
        private List<Tuple<string, string>> GetHighlights(IHit<dynamic> hit)
        {
            List<Tuple<string, string>> highlights = new List<Tuple<string, string>>();

            foreach (var highlight in hit.Highlight)
            {
                IReadOnlyCollection<string> highlightHit = highlight.Value;
                foreach (string highlightString in highlightHit)
                {
                    // Replace "_abbr" field names with the name of the "parent" field.  For example, replace "title_abbr"
                    // with "title".
                    string highlightField = string.Empty;
                    switch(highlight.Key)
                    {
                        case ESField.ASSOCIATIONS_ABBR:
                            highlightField = ESField.ASSOCIATIONS;
                            break;
                        case ESField.CONTAINER_ABBR:
                            highlightField = ESField.CONTAINER;
                            break;
                        case ESField.TITLE_ABBR:
                            highlightField = ESField.TITLE;
                            break;
                        case ESField.TRANSLATEDTITLE_ABBR:
                            highlightField = ESField.TRANSLATEDTITLE;
                            break;
                        case ESField.UNIFORMTITLE_ABBR:
                            highlightField = ESField.UNIFORMTITLE;
                            break;
                        case ESField.VARIANTS_ABBR:
                            highlightField = ESField.VARIANTS;
                            break;
                        default:
                            highlightField = highlight.Key;
                            break;
                    }

                    // Make sure no duplicate field-value pairs are added to the list of highlights.  This could happen for 
                    // fields that are indexed in multiple ways.
                    bool added = false;
                    foreach (Tuple<string, string> h in highlights)
                    {
                        if (h.Item1 == highlightField && 
                            h.Item2.Replace("<b>", "").Replace("</b>", "") == highlightString.Replace("<b>", "").Replace("</b>", ""))
                        {
                            // Match found
                            added = true; break;
                        }
                    }
                    if (!added) highlights.Add(new Tuple<string, string>(highlightField, highlightString));
                }
            }

            return highlights;
        }

        /// <summary>
        /// Check the current status of the ElasticSearch server 
        /// </summary>
        public void CheckServerStatus()
        {
            ClusterHealthRequest healthRequest = new ClusterHealthRequest();
            healthRequest.Timeout = new Time("30s");
            healthRequest.WaitForStatus = Elasticsearch.Net.WaitForStatus.Yellow;
            var healthResponse = _es.Cluster.Health(healthRequest);
            if (!healthResponse.IsValid) ProcessError(healthResponse);
        }

        /// <summary>
        /// Convert the ElasticSearch field name to a SearchField enum value
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        private SearchField GetSearchFieldEnum(string field)
        {
            SearchField searchField = SearchField.All;

            switch (field)
            {
                case ESField.ALL:
                    searchField = SearchField.All; break;
                case ESField.CONTRIBUTORS_RAW:
                    searchField = SearchField.Contributors; break;
                case ESField.DATERANGES:
                    searchField = SearchField.DateRanges; break;
                case ESField.GENRE:
                    searchField = SearchField.Genre; break;
                case ESField.KEYWORDS_RAW:
                    searchField = SearchField.FacetItemKeywords; break;
                case ESField.LANGUAGE:
                    searchField = SearchField.Language; break;
                case ESField.NAMES_RAW:
                    searchField = SearchField.PageNames; break;
                case ESField.AUTHORNAMES:
                    searchField = SearchField.AuthorNames; break;
                case ESField.COLLECTIONS:
                    searchField = SearchField.Collections; break;
                case ESField.CONTRIBUTORS:
                    searchField = SearchField.Contributors; break;
                case ESField.DATES:
                    searchField = SearchField.Dates; break;
                case ESField.DOI:
                    searchField = SearchField.Doi; break;
                case ESField.ISBN:
                    searchField = SearchField.ISBN; break;
                case ESField.ISSN:
                    searchField = SearchField.ISSN; break;
                case ESField.ISSUE:
                    searchField = SearchField.Issue; break;
                case ESField.AUTHORS:
                    searchField = SearchField.ItemAuthors; break;
                case ESField.FACETAUTHORS:
                    searchField = SearchField.FacetItemAuthors; break;
                case ESField.KEYWORDS:
                    searchField = SearchField.ItemKeywords; break;
                case ESField.KEYWORD:
                    searchField = SearchField.Keyword; break;
                case ESField.MATERIALTYPE:
                    searchField = SearchField.MaterialType; break;
                case ESField.NAME:
                    searchField = SearchField.Name; break;
                case ESField.OCLC:
                    searchField = SearchField.Oclc; break;
                case ESField.NAMES:
                    searchField = SearchField.PageNames; break;
                case ESField.PAGETYPES:
                    searchField = SearchField.PageTypes; break;
                case ESField.PUBLICATIONPLACE:
                    searchField = SearchField.PublicationPlace; break;
                case ESField.PUBLISHER:
                    searchField = SearchField.Publisher; break;
                case ESField.SERIES:
                    searchField = SearchField.Series; break;
                case ESField.TEXT:
                    searchField = SearchField.Text; break;
                case ESField.TITLE:
                    searchField = SearchField.Title; break;
                case ESField.VOLUME:
                    searchField = SearchField.Volume; break;
                default:
                    searchField = SearchField.All; break;
            }

            return searchField;
        }

        /// <summary>
        /// Parse the error information from the specified response and throw an exception.
        /// </summary>
        /// <param name="response"></param>
        private void ProcessError(ISearchResponse<dynamic> response)
        {
            string errorMessage = "Error reported by ElasticSearch server.\n\r";
            if (response.OriginalException != null)
            {
                errorMessage += response.OriginalException.Message + "\n\r";
            }
            else if (response.ServerError != null)
            {
                errorMessage += response.ServerError.Error.Reason + "\n\r";
            }
            else if (response.Shards.Failures.Count > 0)
            {
                foreach(Elasticsearch.Net.ShardFailure failure in response.Shards.Failures)
                {
                    errorMessage += string.Format("Index '{0}': {1}\n\r", failure.Index, failure.Reason.Reason);
                }
            }
            else if (response.TimedOut)
            {
                errorMessage += "Query timed out after " + (response.Took / 1000).ToString() + " seconds\n\r";
            }
            else if (response.TerminatedEarly)
            {
                errorMessage += "Query terminated early";
            }
            errorMessage += response.DebugInformation;

            throw new SearchException(errorMessage);
        }

        private void ProcessError(ClusterHealthResponse response)
        {
            string errorMessage = "Error reported by ElasticSearch server.\n\r";
            if (response.OriginalException != null)
            {
                errorMessage += response.OriginalException.Message + "\n\r";
            }
            else if (response.ServerError != null)
            {
                errorMessage += response.ServerError.Error.Reason + "\n\r";
            }
            errorMessage += response.DebugInformation;

            throw new SearchException(errorMessage);
        }

        /// <summary>
        /// Write the results of a search to the console
        /// </summary>
        /// <param name="args"></param>
        /// <param name="op"></param>
        /// <param name="limits"></param>
        /// <param name="results"></param>
        private void WriteDebuggingInfo(List<Tuple<string, string>> args,
            List<Tuple<string, string>> limits, ISearchResponse<dynamic> results)
        {
            // Format the list of arguments
            string query = string.Empty;
            foreach (Tuple<string, string> arg in args)
            {
                if (!string.IsNullOrWhiteSpace(query)) query += " AND ";
                query += string.Format("{0}:{1}", arg.Item1, arg.Item2);
            }

            WriteDebuggingInfo(query, limits, results);
        }

        /// <summary>
        /// Write the results of a search to the console
        /// </summary>
        /// <param name="query"></param>
        /// <param name="results"></param>
        private void WriteDebuggingInfo(string query, List<Tuple<string, string>> limits,
            ISearchResponse<dynamic> results)
        {
            // Format the list of limits
            string limit = string.Empty;
            limits = limits ?? new List<Tuple<string, string>>();
            foreach(Tuple<string, string> lim in limits)
            {
                if (!string.IsNullOrWhiteSpace(limit)) limit += " AND ";
                limit += string.Format("{0}:{1}", lim.Item1, lim.Item2);
            }

            // Deliver the query results
            Console.WriteLine("ELASTICSEARCH QUERY");
            Console.WriteLine(string.Format("QUERY: {0}", query));
            Console.WriteLine(string.Format("LIMIT: {0}", limit));
            Console.WriteLine(string.Format("INDEX: {0}", IndexName));
            Console.WriteLine(string.Format("RETURN FIELDS: {0}", string.Join(",", ReturnFields.ToArray())));
            Console.WriteLine(string.Format("SORT FIELDS: {0}", SortField));

            string facetFields = string.Empty;
            foreach(Tuple<string, ESFacetSortOrder> facet in FacetFields)
            {
                facetFields += (facetFields.Length > 0 ? "," : "") + facet.Item1; 
            }
            Console.WriteLine(string.Format("FACET FIELDS: {0}", facetFields));

            Console.WriteLine(string.Format("HIGHLIGHT FIELDS: {0}", string.Join(",", HighlightFields.ToArray())));
            Console.WriteLine(string.Format("SUGGEST: {0}", Suggest ? "TRUE" : "FALSE"));
            Console.WriteLine(string.Format("RESULTS: {0} returned out of {1} total hits",
                results.HitsMetadata.Hits.Count, results.HitsMetadata.Total.Value));
            Console.WriteLine();
            Console.WriteLine("REQUEST:");
            Console.WriteLine(System.Text.Encoding.UTF8.GetString(results.ApiCall.RequestBodyInBytes));
            Console.WriteLine();

            foreach (var hit in results.Hits)
            {
                string value = string.Empty;
                switch (hit.Type)
                {
                    case ESType.AUTHOR: value = hit.Source.primaryAuthorName; break;
                    case ESType.ITEM: value = hit.Source.title; break;
                    case ESType.KEYWORD: value = hit.Source.keyword; break;
                    case ESType.NAME: value = hit.Source.name; break;
                    case ESType.PAGE: value = hit.Source.textPath; break;
                }
                Console.WriteLine(string.Format("Hit {0} {1} ({2}): {3}", hit.Type, hit.Id, hit.Score, value));
            }
            if (results.Hits.Count > 0) Console.WriteLine();

            foreach (var agg in results.Aggregations)
            {

                BucketAggregate aggBucket = (BucketAggregate)agg.Value;
                foreach (KeyedBucket<object> bucket in aggBucket.Items)
                {
                    Console.WriteLine(string.Format("Facet {0}: {1} ({2})", agg.Key, bucket.Key, bucket.DocCount.ToString()));
                }
            }
            if (results.Aggregations.Count > 0) Console.WriteLine();

            foreach (var hit in results.Hits)
            {
                foreach (var highlight in hit.Highlight)
                {
                    IReadOnlyCollection<string> highlightHits = highlight.Value;
                    foreach (string highlightString in highlightHits)
                    {
                        Console.WriteLine(string.Format("Highlight {0}:{1}-{2}", hit.Id, highlight.Key, highlightString));
                    }
                }

                if (hit.Highlight.Count > 0) Console.WriteLine();
            }

            Dictionary<string, List<string>> suggestions = new Dictionary<string, List<string>>();
            foreach (var suggestKey in results.Suggest.Keys)
            //foreach (var suggestResults in results.Suggest)
            {
                foreach (var suggestion in results.Suggest[suggestKey])
                //foreach (var suggestion in suggestResults.Value)
                {
                    foreach (var option in suggestion.Options)
                    {
                        if (suggestions.ContainsKey(suggestKey))
                        //if (suggestions.ContainsKey(suggestResults.Key))
                        {
                            if (!suggestions[suggestKey].Contains(option.Text))
                                suggestions[suggestKey].Add(option.Text);
                            //if (!suggestions[suggestResults.Key].Contains(option.Text))
                            //    suggestions[suggestResults.Key].Add(option.Text);
                        }
                        else
                        {
                            suggestions.Add(suggestKey, new List<string> { option.Text });
                            //suggestions.Add(suggestResults.Key, new List<string> { option.Text });
                        }
                    }
                }
            }
            foreach (KeyValuePair<string, List<string>> suggestionList in suggestions)
            {
                foreach(string suggestion in suggestionList.Value)
                    Console.WriteLine(string.Format("Suggestion: {0} - {1}", suggestionList.Key, suggestion));
            }

            Console.WriteLine();
        }
    }
}
