using System;
using System.Collections.Generic;
using System.Configuration;

namespace BHL.Search.SQL
{
    public class Search : ISearch
    {
        private readonly string _connectionString = string.Empty;

        public Search()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["BHL"].ConnectionString;
        }

        public Search(string connectionString)
        {
            _connectionString = connectionString;
        }

        public bool Facet { get; set; }
        public bool Highlight { get; set; }
        public int NumResults { get; set; }
        public SortField SortField { get; set; }
        public int StartPage { get; set; }
        public bool Suggest { get; set; }

        public bool IsOnline()
        {
            bool online;
            try
            {
                online = new DataAccess(_connectionString).IsOnline();
            }
            catch
            {
                // TODO: Consider logging the exception here
                online = false;
            }
            return online;
        }

        public bool IsFullTextSupported()
        {
            return false;
        }

        public ISearchResult SearchAuthor(string name)
        {
            SearchResult result = new SearchResult
            {
                Authors = new DataAccess(_connectionString).SearchAuthor(name, out long totalHits, StartPage, NumResults)
            };
            GetSearchResultStats(result, totalHits);
            result.Query.Add(new Tuple<SearchField, string>(SearchField.All, name));
            return result;
        }

        public ISearchResult SearchAll(string query, List<Tuple<SearchField, string>> limits = null)
        {
            throw new NotImplementedException();

            /*
            SearchResult result = new SearchResult();
            // Implement search here
            result.Query.Add(new Tuple<SearchField, string>(SearchField.All, query));
            result.QueryLimits = limits;
            return result;
            */
        }

        public ISearchResult SearchCatalog(SearchStringParam title, SearchStringParam author, string volume, string year, 
            SearchStringParam keyword, Tuple<string, string> language, Tuple<string, string> collection, 
            SearchStringParam notes, SearchStringParam text, List<Tuple<SearchField, string>> limits = null)
        {
            SearchResult result = new SearchResult
            {
                Items = new DataAccess(_connectionString).SearchItem(title.SearchValue, author.SearchValue, volume,
                    year, keyword.SearchValue, (language?.Item1),
                    (collection?.Item1), notes.SearchValue, out long totalHits, StartPage, NumResults)
            };
            GetSearchResultStats(result, totalHits);

            if (!string.IsNullOrWhiteSpace(title.SearchValue)) result.Query.Add(new Tuple<SearchField, string>(SearchField.Title, title.SearchValue));
            if (!string.IsNullOrWhiteSpace(author.SearchValue)) result.Query.Add(new Tuple<SearchField, string>(SearchField.AuthorNames, author.SearchValue));
            if (!string.IsNullOrWhiteSpace(volume)) result.Query.Add(new Tuple<SearchField, string>(SearchField.Volume, volume));
            if (!string.IsNullOrWhiteSpace(year)) result.Query.Add(new Tuple<SearchField, string>(SearchField.Dates, year));
            if (!string.IsNullOrWhiteSpace(keyword.SearchValue)) result.Query.Add(new Tuple<SearchField, string>(SearchField.Keyword, keyword.SearchValue));
            if (language != null) result.Query.Add(new Tuple<SearchField, string>(SearchField.Language, language.Item1));
            if (collection != null) result.Query.Add(new Tuple<SearchField, string>(SearchField.Collections, collection.Item1));
            if (!string.IsNullOrWhiteSpace(notes.SearchValue)) result.Query.Add(new Tuple<SearchField, string>(SearchField.Notes, notes.SearchValue));
            if (!string.IsNullOrWhiteSpace(text.SearchValue)) result.Query.Add(new Tuple<SearchField, string>(SearchField.Text, text.SearchValue));
            result.QueryLimits = limits;
            return result;
        }

        public ISearchResult SearchCatalog(string searchTerm, List<Tuple<SearchField, string>> limits = null)
        {
            return SearchItem(searchTerm, limits);
        }

        public ISearchResult SearchItem(string searchTerm, List<Tuple<SearchField, string>> limits = null)
        {
            SearchResult result = new SearchResult
            {
                Items = new DataAccess(_connectionString).SearchItem(searchTerm, out long totalHits, StartPage, NumResults)
            };
            GetSearchResultStats(result, totalHits);

            result.Query.Add(new Tuple<SearchField, string>(SearchField.All, searchTerm));
            result.QueryLimits = limits;
            return result;
        }

        public ISearchResult SearchKeyword(string keyword)
        {
            SearchResult result = new SearchResult
            {
                Keywords = new DataAccess(_connectionString).SearchKeyword(keyword, out long totalHits, StartPage, NumResults)
            };
            GetSearchResultStats(result, totalHits);
            result.Query.Add(new Tuple<SearchField, string>(SearchField.All, keyword));
            return result;
        }

        public ISearchResult SearchName(string name)
        {
            SearchResult result = new SearchResult
            {
                Names = new DataAccess(_connectionString).SearchName(name, out long totalHits, StartPage, NumResults)
            };
            GetSearchResultStats(result, totalHits);
            result.Query.Add(new Tuple<SearchField, string>(SearchField.All, name));
            return result;
        }

        public ISearchResult SearchPage(string query, List<Tuple<SearchField, string>> limits = null, 
            bool includeText = false)
        {
            throw new NotImplementedException();

            /*
            SearchResult result = new SearchResult();
            // Implement search here
            result.Query.Add(new Tuple<SearchField, string>(SearchField.All, query));
            result.QueryLimits = limits;
            return result;
            */
        }

        private void GetSearchResultStats(ISearchResult result, long totalHits)
        {
            result.TotalHits = totalHits;
            result.PageSize = NumResults;
            result.StartPage = StartPage;
            result.TotalPages = (result.TotalHits / (long)result.PageSize) + 1;
        }
    }
}
