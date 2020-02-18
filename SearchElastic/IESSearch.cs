using System;
using System.Collections.Generic;

namespace BHL.Search.Elastic
{
    public interface IESSearch
    {
        /// <summary>
        /// Check the status of the server.  Throws an exception if the server is unavailable.
        /// </summary>
        void CheckServerStatus();

        /// <summary>
        /// Initialize the object for a new query, without establishing an entirely new server connection
        /// </summary>
        void SetSearchDefaults();

        SearchResult SearchAll(string query, List<Tuple<string, string>> limits = null);

        SearchResult SearchCatalog(SearchStringParam title, SearchStringParam author, string volume, string year, 
            SearchStringParam keyword, string language, string collection, SearchStringParam notes, SearchStringParam text, 
            List<Tuple<string, string>> limits = null);

        // Obsolete
        //SearchResult SearchItem(List<Tuple<string, string>> args, ESOperator op = ESOperator.And, List<Tuple<string, string>> limits = null);

        SearchResult SearchAuthor(string query);

        SearchResult SearchKeyword(string query);

        SearchResult SearchName(string query);

        SearchResult SearchPage(string query, List<Tuple<string, string>> limits = null);

        /// <summary>
        /// Name of index for the search to target
        /// </summary>
        string IndexName { set; get; }

        /// <summary>
        /// Type of index object for the search to target
        /// </summary>
        string TypeName { set; get; }

        /// <summary>
        /// Fields to return.  Leave empty to return all fields.
        /// </summary>
        List<string> ReturnFields { set; get; }

        /// <summary>
        /// Fields on which to sort.
        /// </summary>
        string SortField { set; get; }

        /// <summary>
        /// Fields on which to facet.
        /// </summary>
        List<Tuple<string, ESFacetSortOrder>> FacetFields { set; get; }

        /// <summary>
        /// Fields in which to highlight hits.
        /// </summary>
        List<string> HighlightFields { set; get; }

        /// <summary>
        /// True to suggest alternative search terms
        /// </summary>
        bool Suggest { set; get; }

        /// <summary>
        /// Maximum number of results to return.
        /// </summary>
        int NumResults { get; set; }

        /// <summary>
        /// Return results starting at this page.  
        /// NumResults defines the size of the page.  Therefore, if 
        /// NumResults = 100, and StartPage = 2, results will be returned
        /// starting with the 101st hit.
        /// </summary>
        int StartPage { get; set; }

        /// <summary>
        /// Maximum number of values to return for each facet.
        /// </summary>
        int NumFacets { get; set; }

        /// <summary>
        /// True to enable debugging output.
        /// </summary>
        bool Debug { set; get; }
    }
}
