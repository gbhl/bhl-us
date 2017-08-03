using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHL.Search
{
    public class SearchResult : ISearchResult
    {
        /// <summary>
        /// Total number of hits returned by the search
        /// </summary>
        private long _totalHits = 0;

        /// <summary>
        /// True if the search completed with no errors; false otherwise.
        /// </summary>
        private bool _isValid = true;

        /// <summary>
        /// Message describing the current error.
        /// </summary>
        private string _errorMessage = string.Empty;

        /// <summary>
        /// Detailed information about the current error.
        /// </summary>
        private string _debugInfo = string.Empty;

        /// <summary>
        /// List of items returned by the search
        /// </summary>
        private List<IHit> _items = new List<IHit>();

        /// <summary>
        /// List of pages returned by the search
        /// </summary>
        private List<IHit> _pages = new List<IHit>();

        /// <summary>
        /// List of names returned by the search
        /// </summary>
        private List<IHit> _names = new List<IHit>();

        /// <summary>
        /// List of keywords returned by the search
        /// </summary>
        private List<IHit> _keywords = new List<IHit>();

        /// <summary>
        /// List of authors returned by the search
        /// </summary>
        private List<IHit> _authors = new List<IHit>();

        /// <summary>
        /// List of search string alternatives returned by the search
        /// </summary>
        private Dictionary<SearchField, List<string>> _suggestions = new Dictionary<SearchField, List<string>>();

        /// <summary>
        /// List of facets returned by the search.  First element of the dictionary is the facet 
        /// category (i.e. "keyword", "date").  Second element of the dictionary is a dictionary
        /// of facet values and counts.  The counts are the numbers of occurrences of each facet value.
        /// </summary>
        private Dictionary<SearchField, Dictionary<string, long?>> _facets = new Dictionary<SearchField, Dictionary<string, long?>>();

        /// <summary>
        /// Query fields and values that produced this result
        /// </summary>
        private List<Tuple<SearchField, string>> _query = new List<Tuple<SearchField, string>>();

        /// <summary>
        /// Query limits (facets) that produced this result
        /// </summary>
        private List<Tuple<SearchField, string>> _queryLimits = new List<Tuple<SearchField, string>>();

        public long TotalHits
        {
            get { return _totalHits; }
            set { _totalHits = value; }
        }

        public bool IsValid
        {
            get { return _isValid; }
            set { _isValid = value; }
        }

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { _errorMessage = value ?? string.Empty; }
        }

        public string DebugInfo
        {
            get { return _debugInfo; }
            set { _debugInfo = value ?? string.Empty; }
        }

        public List<IHit> Items
        {
            get { return _items; }
            set { _items = value ?? new List<IHit>(); }
        }

        public List<IHit> Pages
        {
            get { return _pages; }
            set { _pages = value ?? new List<IHit>(); }
        }

        public List<IHit> Names
        {
            get { return _names; }
            set { _names = value ?? new List<IHit>(); }
        }

        public List<IHit> Keywords
        {
            get { return _keywords; }
            set { _keywords = value ?? new List<IHit>(); }
        }

        public List<IHit> Authors
        {
            get { return _authors; }
            set { _authors = value ?? new List<IHit>(); }
        }

        public Dictionary<SearchField, List<string>> Suggestions
        {
            get { return _suggestions; }
            set { _suggestions = value ?? new Dictionary<SearchField, List<string>>(); }
        }

        public Dictionary<SearchField, Dictionary<string, long?>> Facets
        {
            get { return _facets; }
            set { _facets = value ?? new Dictionary<SearchField, Dictionary<string, long?>>(); }
        }

        public List<Tuple<SearchField, string>> Query
        {
            get { return _query; }
            set { _query = value ?? new List<Tuple<SearchField, string>>(); }
        }

        public List<Tuple<SearchField, string>> QueryLimits
        {
            get { return _queryLimits; }
            set { _queryLimits = value ?? new List<Tuple<SearchField, string>>(); }
        }
    }
}
