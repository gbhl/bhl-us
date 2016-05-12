using CustomDataAccess;
using System;

namespace MOBOT.BHL.API.BHLApiDataObjects2
{
    [Serializable]
    public class Stats
    {
        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Stats()
        {
        }

        #endregion Constructors

        #region Properties

        private int _titleCount = 0;
        public int TitleCount
        {
            get { return _titleCount; }
            set { _titleCount = value; }
        }

        private int _itemCount = 0;
        public int ItemCount
        {
            get { return _itemCount; }
            set { _itemCount = value; }
        }

        private int _pageCount = 0;
        public int PageCount
        {
            get { return _pageCount; }
            set { _pageCount = value; }
        }

        private int _partCount = 0;
        public int PartCount
        {
            get { return _partCount; }
            set { _partCount = value; }
        }

        #endregion

        #region ISetValues Members

        public void SetValues(CustomDataRow row)
        {
            foreach (CustomDataColumn column in row)
            {
                switch (column.Name)
                {
                    case "TitleCount":
                        {
                            _titleCount = (int)column.Value;
                            break;
                        }
                    case "ItemCount":
                        {
                            _itemCount = (int)column.Value;
                            break;
                        }
                    case "PageCount":
                        {
                            _pageCount = (int)column.Value;
                            break;
                        }
                    case "PartCount":
                        {
                            _partCount = (int)column.Value;
                            break;
                        }
                }
            }
        }

        #endregion
    }
}
