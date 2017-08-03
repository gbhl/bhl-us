using System;

namespace BHL.Search
{
    public class SearchException : Exception
    {
        public SearchException()
        { }

        public SearchException(string message) : base(message)
        { }

        public SearchException(string message, Exception inner) : base (message, inner) { }
    }
}
