using System;
using System.Collections.Generic;
using System.Configuration;

namespace BHL.Search.SQL
{
    public class Search : ISearch
    {
        private string _connectionString = string.Empty;

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
            bool online = true;
            try
            {
                online = new DataAccess(_connectionString).IsOnline();
            }
            catch (Exception ex)
            {
                // TODO: Consider logging the exception here
                online = false;
            }
            return online;
        }

        public ISearchResult SearchAuthor(string name)
        {
            SearchResult result = new SearchResult();
            long totalHits = 0;
            result.Authors = new DataAccess(_connectionString).SearchAuthor(name, out totalHits, StartPage, NumResults);
            GetSearchResultStats(result, totalHits);
            result.Query.Add(new Tuple<SearchField, string>(SearchField.All, name));
            return result;
        }

        public ISearchResult SearchCatalog(string query, List<Tuple<SearchField, string>> limits = null)
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

        public ISearchResult SearchItem(string title, string author, string volume, string year, string keyword, Tuple<string, string> language, Tuple<string, string> collection, List<Tuple<SearchField, string>> limits = null)
        {
            SearchResult result = new SearchResult();

            long totalHits = 0;
            result.Items = new DataAccess(_connectionString).SearchItem(title, author, volume, year, keyword, 
                (language != null ? language.Item1 : null), 
                (collection != null ? collection.Item1 : null), out totalHits, StartPage, NumResults);
            GetSearchResultStats(result, totalHits);

            if (!string.IsNullOrWhiteSpace(title)) result.Query.Add(new Tuple<SearchField, string>(SearchField.Title, title));
            if (!string.IsNullOrWhiteSpace(author)) result.Query.Add(new Tuple<SearchField, string>(SearchField.AuthorNames, author));
            if (!string.IsNullOrWhiteSpace(volume)) result.Query.Add(new Tuple<SearchField, string>(SearchField.Volume, volume));
            if (!string.IsNullOrWhiteSpace(year)) result.Query.Add(new Tuple<SearchField, string>(SearchField.Dates, year));
            if (!string.IsNullOrWhiteSpace(keyword)) result.Query.Add(new Tuple<SearchField, string>(SearchField.Keyword, keyword));
            if (language != null) result.Query.Add(new Tuple<SearchField, string>(SearchField.Language, language.Item1));
            if (collection != null) result.Query.Add(new Tuple<SearchField, string>(SearchField.Collections, collection.Item1));
            result.QueryLimits = limits;
            return result;
        }

        public ISearchResult SearchItem(string searchTerm, List<Tuple<SearchField, string>> limits = null)
        {
            SearchResult result = new SearchResult();

            long totalHits = 0;
            result.Items = new DataAccess(_connectionString).SearchItem(searchTerm, out totalHits, StartPage, NumResults);
            GetSearchResultStats(result, totalHits);

            result.Query.Add(new Tuple<SearchField, string>(SearchField.All, searchTerm));
            result.QueryLimits = limits;
            return result;
        }

        public ISearchResult SearchKeyword(string keyword)
        {
            SearchResult result = new SearchResult();
            long totalHits = 0;
            result.Keywords = new DataAccess(_connectionString).SearchKeyword(keyword, out totalHits, StartPage, NumResults);
            GetSearchResultStats(result, totalHits);
            result.Query.Add(new Tuple<SearchField, string>(SearchField.All, keyword));
            return result;
        }

        public ISearchResult SearchName(string name)
        {
            SearchResult result = new SearchResult();
            long totalHits = 0;
            result.Names = new DataAccess(_connectionString).SearchName(name, out totalHits, StartPage, NumResults);
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
