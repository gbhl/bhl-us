using System;
using System.Collections.Generic;

namespace BHL.Search
{
    public interface ISearch
    {
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
        /// Field on which to sort.
        /// </summary>
        SortField SortField { get; set; }

        /// <summary>
        /// Fields on which to facet.
        /// </summary>
        bool Facet { get; set; }

        /// <summary>
        /// True to highlight hits in search results.
        /// </summary>
        bool Highlight { get; set; }

        /// <summary>
        /// True to include search term alternatives in search results.
        /// </summary>
        bool Suggest { get; set; }

        /// <summary>
        /// Check whether the search engine is available.
        /// </summary>
        /// <returns>True if available, false if not.</returns>
        bool IsOnline();

        /// <summary>
        /// Check whether the search provider supports full-text search.
        /// </summary>
        /// <returns>True if full-text searches are supported, false if not.</returns>
        bool IsFullTextSupported();

        /// <summary>
        /// Global full-text search of items, authors, keywords, and names (but NOT pages)
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        ISearchResult SearchAll(string query, List<Tuple<SearchField, string>> limits = null);

        /// <summary>
        /// Catalog search for publications.  Return title-level results unless "text" parameter included.  Then include item-level results.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="author"></param>
        /// <param name="volume"></param>
        /// <param name="year"></param>
        /// <param name="keyword"></param>
        /// <param name="language"></param>
        /// <param name="collection"></param>
        /// <param name="text"></param>
        /// <param name="limits"></param>
        /// <returns>Object containing the publications returned by the search.</returns>
        ISearchResult SearchCatalog(SearchStringParam title, SearchStringParam author, string volume, string year, 
            SearchStringParam keyword, Tuple<string, string> language, Tuple<string, string> collection, 
            SearchStringParam notes, SearchStringParam text, List<Tuple<SearchField, string>> limits = null);

        /// <summary>
        /// Global catalog search for publications
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <param name="limits"></param>
        /// <returns></returns>
        ISearchResult SearchCatalog(string searchTerm, List<Tuple<SearchField, string>> limits = null);

        /// <summary>
        /// Global full-text search for publications
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <param name="limits"></param>
        /// <returns>Object containing the publications returned by the search.</returns>
        ISearchResult SearchItem(string searchTerm, List<Tuple<SearchField, string>> limits = null);

        /// <summary>
        /// Full-text search for pages.  May not be supported by all search providers.
        /// </summary>
        /// <param name="query"></param>
        /// <param name="limits"></param>
        /// <param name="includeText">True to include page text in search results</param>
        /// <returns>Object containing the pages returned by the search</returns>
        ISearchResult SearchPage(string query, List<Tuple<SearchField, string>> limits = null, bool includeText = false);

        /// <summary>
        /// Search for authors
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Object containing the authors returned by the search</returns>
        ISearchResult SearchAuthor(string name);

        /// <summary>
        /// Search for keywords
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns>Object containing the keywords returned by the search</returns>
        ISearchResult SearchKeyword(string keyword);

        /// <summary>
        /// Search for names
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Object containing the names returned by the search</returns>
        ISearchResult SearchName(string name);
    }
}
