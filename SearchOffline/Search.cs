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

        public bool IsFullTextSupported()
        {
            return true;
        }

        public ISearchResult SearchAuthor(string name)
        {
            SearchResult result = GetOfflineSearchResult();
            result.Query.Add(new Tuple<SearchField, string>(SearchField.All, name));
            return result;
        }

        public ISearchResult SearchAll(string query, List<Tuple<SearchField, string>> limits = null)
        {
            SearchResult result = GetOfflineSearchResult();
            result.Query.Add(new Tuple<SearchField, string>(SearchField.All, query));
            result.QueryLimits = limits;
            return result;
        }

        public ISearchResult SearchCatalog(SearchStringParam title, SearchStringParam author, string volume, string year, 
            SearchStringParam keyword, Tuple<string, string> language, Tuple<string, string> collection, 
            SearchStringParam notes, SearchStringParam text, List<Tuple<SearchField, string>> limits = null)
        {
            SearchResult result = GetOfflineSearchResult();
            if (!string.IsNullOrWhiteSpace(title.searchValue)) result.Query.Add(new Tuple<SearchField, string>(SearchField.Title, title.searchValue));
            if (!string.IsNullOrWhiteSpace(author.searchValue)) result.Query.Add(new Tuple<SearchField, string>(SearchField.AuthorNames, author.searchValue));
            if (!string.IsNullOrWhiteSpace(volume)) result.Query.Add(new Tuple<SearchField, string>(SearchField.Volume, volume));
            if (!string.IsNullOrWhiteSpace(year)) result.Query.Add(new Tuple<SearchField, string>(SearchField.Dates, year));
            if (!string.IsNullOrWhiteSpace(keyword.searchValue)) result.Query.Add(new Tuple<SearchField, string>(SearchField.Keyword, keyword.searchValue));
            if (language != null) result.Query.Add(new Tuple<SearchField, string>(SearchField.Language, language.Item2));
            if (collection != null) result.Query.Add(new Tuple<SearchField, string>(SearchField.Collections, collection.Item2));
            if (!string.IsNullOrWhiteSpace(notes.searchValue)) result.Query.Add(new Tuple<SearchField, string>(SearchField.Notes, notes.searchValue));
            if (!string.IsNullOrWhiteSpace(text.searchValue)) result.Query.Add(new Tuple<SearchField, string>(SearchField.Text, text.searchValue));
            result.QueryLimits = limits;
            return result;
        }

        public ISearchResult SearchCatalog(string searchTerm, List<Tuple<SearchField, string>> limits = null)
        {
            SearchResult result = GetOfflineSearchResult();
            result.Query.Add(new Tuple<SearchField, string>(SearchField.All, searchTerm));
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

        public ISearchResult SearchPage(string query, List<Tuple<SearchField, string>> limits = null,
            bool includeText = false)
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
