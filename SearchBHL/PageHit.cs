using System.Collections.Generic;

namespace BHL.Search
{
    public class PageHit : Hit
    {
        private int _itemId = 0;
        private int _pageId = 0;
        private int _sequence = 0;
        private string _pageDescription = string.Empty;
        private List<string> _pageIndicators = new List<string>();
        private List<string> _pageTypes = new List<string>();
        private List<string> _names = new List<string>();
        private List<int> _segments = new List<int>();
        private string _text = string.Empty;
        private string _textPath = string.Empty;

        public int ItemId
        {
            get { return _itemId; }
            set { _itemId = value; }
        }

        public int PageId
        {
            get { return _pageId; }
            set { _pageId = value; }
        }

        public int Sequence
        {
            get { return _sequence; }
            set { _sequence = value; }
        }

        public string PageDescription
        {
            get { return _pageDescription; }
            set { _pageDescription = value; }
        }

        public List<string> PageIndicators
        {
            get { return _pageIndicators; }
            set { _pageIndicators = value;  }
        }

        public List<string> PageTypes
        {
            get { return _pageTypes; }
            set { _pageTypes = value ?? new List<string>(); }
        }

        public List<string> Names
        {
            get { return _names; }
            set { _names = value ?? new List<string>(); }
        }

        public List<int> Segments
        {
            get { return _segments; }
            set { _segments = value ?? new List<int>(); }
        }

        public string Text
        {
            get { return _text; }
            set { _text = value ?? string.Empty; }
        }

        public string TextPath
        {
            get { return _textPath; }
            set { _textPath = value ?? string.Empty; }
        }
    }
}
