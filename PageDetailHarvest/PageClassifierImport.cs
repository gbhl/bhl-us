using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PageDetailHarvest
{
    internal class PageClassifierImport
    {
        private int _itemID = 0;

        public int ItemID
        {
            get { return _itemID; }
            set { _itemID = value; }
        }

        private int _pageID = 0;

        public int PageID
        {
            get { return _pageID; }
            set { _pageID = value; }
        }

        private string _bwColor = string.Empty;

        public string BwColor
        {
            get { return _bwColor; }
            set { _bwColor = value; }
        }

        private bool _noImage = false;

        public bool NoImage
        {
            get { return _noImage; }
            set { _noImage = value; }
        }

        private List<string> _pageTypes = new List<string>();

        public List<string> PageTypes
        {
            get { return _pageTypes; }
            set { _pageTypes = value; }
        }
    }
}
