using System;
using CustomDataAccess;


namespace MOBOT.BHL.DataObjects
{
    [Serializable]
    public class DisqusCache : __DisqusCache
    {
        private int _itemID;
        public new int ItemID
        {
            get
            {
                if (_itemID == 0)
                {
                    _itemID = base.ItemID;
                }
                return _itemID;
            }
            set
            {
                _itemID = value;
            }
        }

        private int _pageID;
        public new int PageID
        {
            get
            {
                if (_pageID == 0)
                {
                    _pageID = base.PageID;
                }
                return _pageID;
            }
            set
            {
                _pageID = value;
            }
        }

        private int _count;
        public new int Count
        {
            get
            {
                if (_count == 0)
                {
                    _count = base.Count;
                }
                return _count;
            }
            set
            {
                _count = value;
            }
        }
       
    }
}
