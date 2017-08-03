using System.Collections.Generic;

namespace BHL.Search
{
    public class AuthorHit : Hit
    {
        private List<string> _authorNames = new List<string>();
        private string _primaryAuthorName = string.Empty;

        public List<string> AuthorNames
        {
            get { return _authorNames; }
            set { _authorNames = value ?? new List<string>(); }
        }

        public string PrimaryAuthorName
        {
            get { return _primaryAuthorName; }
            set { _primaryAuthorName = value ?? string.Empty; }
        }
    }
}
