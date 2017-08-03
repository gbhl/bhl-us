using System.Collections.Generic;

namespace BHL.Search
{
    public class PageHit : Hit
    {
        private int _itemId = 0;
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
