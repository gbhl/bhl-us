using System;
using System.Collections.Generic;

namespace BHL.Search.Offline
{
    public class Search : ISearch
    {
        public bool Facet { get; set; }
        public bool Highlight { get; set; }
        public int NumResults { get; set; }
        public SortField SortField { get; set; }
        public int StartPage { get; set; }
        public bool Suggest { get; set; }

        public bool IsOnline()
        {
            return true;
        }

        public ISearchResult SearchAuthor(string name)
        {
            SearchResult result = GetOfflineSearchResult();
            result.Query.Add(new Tuple<SearchField, string>(SearchField.All, name));
            return result;
        }

        public ISearchResult SearchCatalog(string query, List<Tuple<SearchField, string>> limits = null)
        {
            SearchResult result = GetOfflineSearchResult();
            result.Query.Add(new Tuple<SearchField, string>(SearchField.All, query));
            result.QueryLimits = limits;
            return result;
        }

        public ISearchResult SearchItem(string title, string author, string volume, string year, string keyword, string language, string collection, List<Tuple<SearchField, string>> limits = null)
        {
            SearchResult result = GetOfflineSearchResult();
            if (!string.IsNullOrWhiteSpace(title)) result.Query.Add(new Tuple<SearchField, string>(SearchField.Title, title));
            if (!string.IsNullOrWhiteSpace(author)) result.Query.Add(new Tuple<SearchField, string>(SearchField.AuthorNames, author));
            if (!string.IsNullOrWhiteSpace(volume)) result.Query.Add(new Tuple<SearchField, string>(SearchField.Volume, volume));
            if (!string.IsNullOrWhiteSpace(year)) result.Query.Add(new Tuple<SearchField, string>(SearchField.Dates, year));
            if (!string.IsNullOrWhiteSpace(keyword)) result.Query.Add(new Tuple<SearchField, string>(SearchField.Keyword, keyword));
            if (!string.IsNullOrWhiteSpace(language)) result.Query.Add(new Tuple<SearchField, string>(SearchField.Language, language));
            if (!string.IsNullOrWhiteSpace(collection)) result.Query.Add(new Tuple<SearchField, string>(SearchField.Collections, collection));
            result.QueryLimits = limits;
            return result;
        }

        public ISearchResult SearchItem(string searchTerm, List<Tuple<SearchField, string>> limits = null)
        {
            SearchResult result = GetOfflineSearchResult();
            result.Query.Add(new Tuple<SearchField, string>(SearchField.All, searchTerm));
            result.QueryLimits = limits;
            return result;
        }

        public ISearchResult SearchKeyword(string keyword)
        {
            SearchResult result = GetOfflineSearchResult();
            result.Query.Add(new Tuple<SearchField, string>(SearchField.All, keyword));
            return result;
        }

        public ISearchResult SearchName(string name)
        {
            SearchResult result = GetOfflineSearchResult();
            result.Query.Add(new Tuple<SearchField, string>(SearchField.All, name));
            return result;
        }

        public ISearchResult SearchPage(string query, List<Tuple<SearchField, string>> limits = null)
        {
            SearchResult result = GetOfflineSearchResult();
            result.Query.Add(new Tuple<SearchField, string>(SearchField.All, query));
            result.QueryLimits = limits;
            return result;
        }

        private SearchResult GetOfflineSearchResult()
        {
            SearchResult result = new SearchResult();
            result.PageSize = this.NumResults;
            result.StartPage = this.StartPage;
            result.IsValid = false;
            result.ErrorMessage = "Search services are offline.";
            return result;
        }
    }
}
