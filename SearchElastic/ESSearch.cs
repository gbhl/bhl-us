﻿using System;
using System.Collections.Generic;
using Nest;

namespace BHL.Search.Elastic
{
    public class ESSearch : IESSearch
    {
        private ElasticClient _es = null;

        // Index to query
        private string _indexName = ESIndex.ALL;

        // Index object type to query
        private string _typeName = ESType.ALL;

        // Fields to return.  Empty to return all.
        private List<string> _returnFields = new List<string>();

        // Fields to sort.  Empty for no sort.
        private string _sortField = ESSortField.SCORE;

        // Fields on which to facet.
        private List<string> _facetFields = new List<string>();

        // Fields in which to highlight results
        private List<string> _highlightFields = new List<string>();

        // True to suggest alternative searches
        bool _suggest = false;

        // Number of results to return
        private int _numResults = 100;

        // Return results starting at this page (_numResults = page size)
        private int _startPage = 1;

        // Number of values to return for each facet
        private int _numFacets = 10;

        // True to enable debugging output
        private bool _debug = false;

        public string IndexName
        {
            get { return _indexName; }
            set { _indexName = value ?? ESIndex.ALL; }
        }

        public string TypeName
        {
            get { return _typeName; }
            set { _typeName = value ?? ESType.ALL; }
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

        public List<string> FacetFields
        {
            get { return _facetFields; }
            set { _facetFields = value ?? new List<string>(); }
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
            // Establish a connection to an ElasticSearch server
            ConnectionSettings connectionSettings = new ConnectionSettings(new Uri(connectionString));
            connectionSettings.DefaultIndex(ESIndex.ALL);
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
            _indexName = ESIndex.ALL;
            _typeName = ESType.ALL;
            _returnFields = new List<string>();
            _sortField = ESSortField.SCORE;
            _facetFields = new List<string>();
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
        /// (query) AND (limit1:limitvalue1 AND limit2:limitvalue2)
        /// </summary>
        /// <param name="query">Query string</param>
        /// <param name="limits">List of field/value pairs on which to limit the search</param>
        public SearchResult SearchCatalog(string query, List<Tuple<string, string>> limits = null)
        {
            /*
             * The following queries the _all field, while also applying boost values to
             * the "title" and "text" fields.  _all and the boost fields are combined into
             * a "should" boolean query (where the minimum is 1), and the limiting/faceting
             * fields are combined into a "must" boolean query.
             * Example raw ElasticSearch query:
              POST items/_search
              {
                "query": {
                  "bool": {
                    "should": [
                      { "match": { "_all": { "query": "fish" } } },
                      { "match": { "title": { "query": "fish", "boost": "2" } } },
                      { "match": { "text": { "query": "fish", "boost": "-2" } } }
                    ],
                    "minimum_number_should_match": 1, 
                    "must": [
                      { "match": { "language": { "query": "English" } } },
                      { "match": { "collections": { "query": "MBLWHOI Library, Woods Hole" } } }
                    ]      
                  }
                }
              }
             */

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

            /*
            // TODO: Rather than trying to parse user-supplied query strings, let ES do it.  Check validateResponse.Valid for result.
            var validateResponse = _es.ValidateQuery<dynamic>(descriptor => descriptor
                .Explain()
                .Query(q => q
                .Bool(b => b
                    .Should(shouldQueries.ToArray())
                    .MinimumShouldMatch(1)
                    .Must(mustQueries.ToArray()))));
            */

            // Set sort, aggregate (facet), and highlight fields
            SetSortField(searchDesc);
            SetAggregateFields(searchDesc);
            SetHighlightFields(searchDesc);

            // Set the fields to use when determining alternate search suggestions
            if (_suggest)
            {
                List<Tuple<string, string>> suggestFields = new List<Tuple<string, string>>();
                suggestFields.Add(new Tuple<string, string>(ESField.ALL, query));
                SetSuggestFields(searchDesc, suggestFields, limits);
            }

            // Execute the query
            ISearchResponse<dynamic> results = ExecuteQuery(searchDesc);
            if (Debug) WriteDebuggingInfo(query, limits, results);

            // Build and return the result object
            return GetSearchResult(results);
        }

        /// <summary>
        /// Submit an Item query that searches on all specified field/value combinations.
        /// If limit values are specified, results are filtered by those limits.
        /// </summary>
        /// <param name="query"></param>
        //public SearchResult SearchItem(List<Tuple<string, string>> args, List<Tuple<string, string>> limits = null)
        public SearchResult SearchItem(string title, string author, string volume, string year, string keyword,
            string language, string collection, List<Tuple<string, string>> limits = null)
        {
            // Initialize the query object
            SearchDescriptor<dynamic> searchDesc = InitializeQuery();

            // Build the query
            List<QueryContainer> mustQueries = new List<QueryContainer>();
            List<QueryContainer> shouldQueries = new List<QueryContainer>();
            if (!string.IsNullOrWhiteSpace(title))
            {
                shouldQueries.Add(new MatchQuery { Field = ESField.TITLE, Query = title, Boost = 2 });
                shouldQueries.Add(new MatchQuery { Field = ESField.ASSOCIATIONS, Query = title });
                shouldQueries.Add(new MatchQuery { Field = ESField.TRANSLATEDTITLE, Query = title });
                shouldQueries.Add(new MatchQuery { Field = ESField.UNIFORMTITLE, Query = title });
                shouldQueries.Add(new MatchQuery { Field = ESField.VARIANTS, Query = title });
            }

            if (!string.IsNullOrWhiteSpace(author)) mustQueries.Add(new MatchQuery { Field = ESField.SEARCHAUTHORS, Query = author });
            if (!string.IsNullOrWhiteSpace(volume)) mustQueries.Add(new MatchQuery { Field = ESField.VOLUME, Query = volume });
            if (!string.IsNullOrWhiteSpace(year)) mustQueries.Add(new MatchQuery { Field = ESField.DATES, Query = year });
            if (!string.IsNullOrWhiteSpace(keyword)) mustQueries.Add(new MatchQuery { Field = ESField.KEYWORDS, Query = keyword });
            if (!string.IsNullOrWhiteSpace(language)) mustQueries.Add(new MatchQuery { Field = ESField.LANGUAGE, Query = language });
            if (!string.IsNullOrWhiteSpace(collection)) mustQueries.Add(new MatchQuery { Field = ESField.COLLECTIONS, Query = collection });

            if (limits != null)
            {
                foreach (Tuple<string, string> limit in limits)
                {
                    mustQueries.Add(new MatchQuery { Field = limit.Item1, Query = limit.Item2 });
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
            if (!string.IsNullOrWhiteSpace(title)) args.Add(new Tuple<string, string>(ESField.TITLE, title));
            if (!string.IsNullOrWhiteSpace(author)) args.Add(new Tuple<string, string>(ESField.SEARCHAUTHORS, author));
            if (!string.IsNullOrWhiteSpace(volume)) args.Add(new Tuple<string, string>(ESField.VOLUME, volume));
            if (!string.IsNullOrWhiteSpace(year)) args.Add(new Tuple<string, string>(ESField.DATES, year));
            if (!string.IsNullOrWhiteSpace(keyword)) args.Add(new Tuple<string, string>(ESField.KEYWORD, keyword));
            if (!string.IsNullOrWhiteSpace(language)) args.Add(new Tuple<string, string>(ESField.LANGUAGE, language));
            if (!string.IsNullOrWhiteSpace(collection)) args.Add(new Tuple<string, string>(ESField.COLLECTIONS, collection));

            // Set the fields to use when determining alternate search suggestions
            if (_suggest)
            {
                SetSuggestFields(searchDesc, args, limits);
            }

            // Execute the query
            ISearchResponse<dynamic> results = ExecuteQuery(searchDesc);
            if (Debug) WriteDebuggingInfo(args, limits, results);

            // Build and return the result object
            return GetSearchResult(results);
        }

        public SearchResult SearchAuthor(string query)
        {
            // Initialize the query object
            SearchDescriptor<dynamic> searchDesc = InitializeQuery();

            // Need to include highlight fields in the query in order for highlighting to work correctly
            List<QueryContainer> shouldQueries = new List<QueryContainer>();
            foreach (string highlight in _highlightFields)
            {
                shouldQueries.Add(new MatchQuery { Field = highlight, Query = query });
            }

            List<QueryContainer> mustQueries = new List<QueryContainer>();
            mustQueries.Add(new MatchQuery { Field = ESField.ALL, Query = query });

            // Construct the complete query.
            searchDesc.Query(q => q
                .Bool(b => b
                    .Must(mustQueries.ToArray())
                    .Should(shouldQueries.ToArray())
                    .MinimumShouldMatch(0)
                )
            );

            /*
            // TODO: Rather than trying to parse user-supplied query strings, let ES do it.  Check validateResponse.Valid for result.
            var validateResponse = _es.ValidateQuery<dynamic>(descriptor => descriptor
                .Explain()
                .Query(q => q
                .Bool(b => b
                    .Must(mustQueries.ToArray())
                    .Should(shouldQueries.ToArray())
                    .MinimumShouldMatch(0))));
            */

            // Set sort, aggregate (facet), and highlight fields
            SetSortField(searchDesc);
            SetAggregateFields(searchDesc);
            SetHighlightFields(searchDesc);

            // Set the fields to use when determining alternate search suggestions
            if (_suggest)
            {
                List<Tuple<string, string>> suggestFields = new List<Tuple<string, string>>();
                suggestFields.Add(new Tuple<string, string>(ESField.ALL, query));
                SetSuggestFields(searchDesc, suggestFields);
            }

            // Execute the query
            ISearchResponse<dynamic> results = ExecuteQuery(searchDesc);
            if (Debug) WriteDebuggingInfo(query, null, results);

            // Build and return the result object
            return GetSearchResult(results);
        }

        public SearchResult SearchKeyword(string query)
        {
            // Initialize the query object
            SearchDescriptor<dynamic> searchDesc = InitializeQuery();

            // Need to include highlight fields in the query in order for highlighting to work correctly
            List<QueryContainer> shouldQueries = new List<QueryContainer>();
            foreach (string highlight in _highlightFields)
            {
                shouldQueries.Add(new MatchQuery { Field = highlight, Query = query });
            }

            List<QueryContainer> mustQueries = new List<QueryContainer>();
            mustQueries.Add(new MatchQuery { Field = ESField.ALL, Query = query });

            // Construct the complete query.
            searchDesc.Query(q => q
                .Bool(b => b
                    .Must(mustQueries.ToArray())
                    .Should(shouldQueries.ToArray())
                    .MinimumShouldMatch(0)
                )
            );

            /*
            // TODO: Rather than trying to parse user-supplied query strings, let ES do it.  Check validateResponse.Valid for result.
            var validateResponse = _es.ValidateQuery<dynamic>(descriptor => descriptor
                .Explain()
                .Query(q => q
                .Bool(b => b
                    .Must(mustQueries.ToArray())
                    .Should(shouldQueries.ToArray())
                    .MinimumShouldMatch(0))));
            */

            // Set sort, aggregate (facet), and highlight fields
            SetSortField(searchDesc);
            SetAggregateFields(searchDesc);
            SetHighlightFields(searchDesc);

            // Set the fields to use when determining alternate search suggestions
            if (_suggest)
            {
                List<Tuple<string, string>> suggestFields = new List<Tuple<string, string>>();
                suggestFields.Add(new Tuple<string, string>(ESField.ALL, query));
                SetSuggestFields(searchDesc, suggestFields);
            }

            // Execute the query
            ISearchResponse<dynamic> results = ExecuteQuery(searchDesc);
            if (Debug) WriteDebuggingInfo(query, null, results);

            // Build and return the result object
            return GetSearchResult(results);
        }

        public SearchResult SearchName(string query)
        {
            // Initialize the query object
            SearchDescriptor<dynamic> searchDesc = InitializeQuery();

            // Need to include highlight fields in the query in order for highlighting to work correctly
            List<QueryContainer> shouldQueries = new List<QueryContainer>();
            foreach (string highlight in _highlightFields)
            {
                shouldQueries.Add(new MatchQuery { Field = highlight, Query = query });
            }

            List<QueryContainer> mustQueries = new List<QueryContainer>();
            mustQueries.Add(new MatchQuery { Field = ESField.ALL, Query = query });

            // Construct the complete query.
            searchDesc.Query(q => q
                .Bool(b => b
                    .Must(mustQueries.ToArray())
                    .Should(shouldQueries.ToArray())
                    .MinimumShouldMatch(0)
                )
            );

            /*
            // TODO: Rather than trying to parse user-supplied query strings, let ES do it.  Check validateResponse.Valid for result.
            var validateResponse = _es.ValidateQuery<dynamic>(descriptor => descriptor
                .Explain()
                .Query(q => q
                .Bool(b => b
                    .Must(mustQueries.ToArray())
                    .Should(shouldQueries.ToArray())
                    .MinimumShouldMatch(0))));
            */

            // Set sort, aggregate (facet), and highlight fields
            SetSortField(searchDesc);
            SetAggregateFields(searchDesc);
            SetHighlightFields(searchDesc);

            // Set the fields to use when determining alternate search suggestions
            if (_suggest)
            {
                List<Tuple<string, string>> suggestFields = new List<Tuple<string, string>>();
                suggestFields.Add(new Tuple<string, string>(ESField.ALL, query));
                SetSuggestFields(searchDesc, suggestFields);
            }

            // Execute the query
            ISearchResponse<dynamic> results = ExecuteQuery(searchDesc);
            if (Debug) WriteDebuggingInfo(query, null, results);

            // Build and return the result object
            return GetSearchResult(results);
        }

        public SearchResult SearchPage(string query, List<Tuple<string, string>> limits = null)
        {
            // Initialize the query object
            SearchDescriptor<dynamic> searchDesc = InitializeQuery();

            // Build the limit queries
            List<QueryContainer> mustQueries = new List<QueryContainer>();
            limits = limits ?? new List<Tuple<string, string>>();
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

            /*
            // TODO: Rather than trying to parse user-supplied query strings, let ES do it.  Check validateResponse.Valid for result.
            var validateResponse = _es.ValidateQuery<dynamic>(descriptor => descriptor
                .Explain()
                .Query(q => q
                .Bool(b => b
                    .Must(queries.ToArray()))));
            */

            // Set sort, aggregate (facet), and highlight fields
            SetSortField(searchDesc);
            SetAggregateFields(searchDesc);
            SetHighlightFields(searchDesc);

            // Set the fields to use when determining alternate search suggestions
            if (_suggest)
            {
                List<Tuple<string, string>> suggestFields = new List<Tuple<string, string>>();
                suggestFields.Add(new Tuple<string, string>(ESField.ALL, query));
                SetSuggestFields(searchDesc, suggestFields);
            }

            // Execute the query
            ISearchResponse<dynamic> results = ExecuteQuery(searchDesc);
            if (Debug) WriteDebuggingInfo(query, limits, results);

            // Build and return the result object
            return GetSearchResult(results);
        }

        private SearchDescriptor<dynamic> InitializeQuery()
        {
            // Construct the query
            SearchDescriptor<dynamic> searchDesc = new SearchDescriptor<dynamic>();
            searchDesc.Index(_indexName);
            searchDesc.Type(TypeName ?? "");
            searchDesc.From((_startPage - 1) * _numResults);
            searchDesc.Size(_numResults);               // Max number of results to return
            searchDesc.Timeout("10s");                  // 10 second timeout (valid units are d, h, m, s, ms, micros, nanos)
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

        private void SetAggregateFields(SearchDescriptor<dynamic> searchDesc)
        {
            var aggFuncs = new List<Func<AggregationContainerDescriptor<dynamic>, IAggregationContainer>>();
            foreach (string facet in _facetFields)
            {
                aggFuncs.Add(a => a.Terms(facet, t => t.Field(facet).Size(_numFacets)));
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
                // Require field match is needed when using a QueryString query
                //highlightFieldFuncs.Add(f => f.Field(highlight).RequireFieldMatch(false));
                highlightFieldFuncs.Add(f => f.Field(highlight));
            }

            if (highlightFieldFuncs.Count > 0)
            {
                searchDesc.Highlight(h => h
                    .Fields(highlightFieldFuncs.ToArray())
                    .PreTags("<b>")
                    .PostTags("</b>")
                );
            }
        }

        private ISearchResponse<dynamic> ExecuteQuery(SearchDescriptor<dynamic> searchDesc)
        {
            CheckServerStatus();
            ISearchResponse<dynamic> results = _es.Search<dynamic>(searchDesc);
            if (!results.IsValid) ProcessError(results);
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

            // Get metadata about search results
            result.PageSize = this._numResults;
            result.StartPage = this._startPage;
            result.TotalHits = results.HitsMetaData.Total;
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
                switch (hit.Type)
                {
                    case ESType.ITEM:
                        string title = hit.Source.title;
                        ItemHit item = hit.Source.ToObject<ItemHit>();
                        item.Score = hit.Score;
                        item.Highlights = GetHighlights(hit);
                        result.Items.Add(item);
                        break;
                    case ESType.PAGE:
                        PageHit page = hit.Source.ToObject<PageHit>();
                        page.Score = hit.Score;
                        page.Highlights = GetHighlights(hit);
                        result.Pages.Add(page);
                        break;
                    case ESType.NAME:
                        NameHit name = hit.Source.ToObject<NameHit>();
                        name.Score = hit.Score;
                        name.Highlights = GetHighlights(hit);
                        result.Names.Add(name);
                        break;
                    case ESType.AUTHOR:
                        AuthorHit author = hit.Source.ToObject<AuthorHit>();
                        author.Score = hit.Score;
                        author.Highlights = GetHighlights(hit);
                        result.Authors.Add(author);
                        break;
                    case ESType.KEYWORD:
                        KeywordHit keyword = hit.Source.ToObject<KeywordHit>();
                        keyword.Score = hit.Score;
                        keyword.Highlights = GetHighlights(hit);
                        result.Keywords.Add(keyword);
                        break;
                }
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
            foreach (var suggestResults in results.Suggest)
            {
                foreach (var suggestion in suggestResults.Value)
                {
                    foreach (var option in suggestion.Options)
                    {
                        SearchField suggestKey = GetSearchFieldEnum(suggestResults.Key);
                        if (result.Suggestions.ContainsKey(suggestKey))
                        {
                            if (!result.Suggestions[suggestKey].Contains(option.Text))
                                result.Suggestions[suggestKey].Add(option.Text);
                        }
                        else
                        {
                            result.Suggestions.Add(suggestKey, new List<string> { option.Text });
                        }

                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Extract the highlights from the specified ElasticSearch hit
        /// </summary>
        /// <param name="hit"></param>
        /// <returns></returns>
        private List<Tuple<string, string>> GetHighlights(IHit<dynamic> hit)
        {
            List<Tuple<string, string>> highlights = new List<Tuple<string, string>>();

            foreach (var highlight in hit.Highlights)
            {
                HighlightHit highlightHit = highlight.Value;
                foreach (string highlightString in highlightHit.Highlights)
                {
                    highlights.Add(new Tuple<string, string>(highlightHit.Field, highlightString));
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
            var healthResponse = _es.ClusterHealth(healthRequest);
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
                    searchField = SearchField.ItemKeywords; break;
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
        private void ProcessError(IResponse response)
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
            Console.WriteLine(string.Format("TYPE: {0}", TypeName));
            Console.WriteLine(string.Format("RETURN FIELDS: {0}", string.Join(",", ReturnFields.ToArray())));
            Console.WriteLine(string.Format("SORT FIELDS: {0}", SortField));
            Console.WriteLine(string.Format("FACET FIELDS: {0}", string.Join(",", FacetFields.ToArray())));
            Console.WriteLine(string.Format("HIGHLIGHT FIELDS: {0}", string.Join(",", HighlightFields.ToArray())));
            Console.WriteLine(string.Format("SUGGEST: {0}", Suggest ? "TRUE" : "FALSE"));
            Console.WriteLine(string.Format("RESULTS: {0} returned out of {1} total hits",
                results.HitsMetaData.Hits.Count, results.HitsMetaData.Total));
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
                foreach (var highlight in hit.Highlights)
                {
                    HighlightHit highlightHit = highlight.Value;
                    foreach (string highlightString in highlightHit.Highlights)
                    {
                        Console.WriteLine(string.Format("Highlight {0}:{1}-{2}", hit.Id, highlightHit.Field, highlightString));
                    }
                }

                if (hit.Highlights.Count > 0) Console.WriteLine();
            }

            Dictionary<string, List<string>> suggestions = new Dictionary<string, List<string>>();
            foreach (var suggestResults in results.Suggest)
            {
                foreach (var suggestion in suggestResults.Value)
                {
                    foreach (var option in suggestion.Options)
                    {
                        if (suggestions.ContainsKey(suggestResults.Key))
                        {
                            if (!suggestions[suggestResults.Key].Contains(option.Text))
                                suggestions[suggestResults.Key].Add(option.Text);
                        }
                        else
                        {
                            suggestions.Add(suggestResults.Key, new List<string> { option.Text });
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