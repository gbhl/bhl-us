﻿using System;
using System.Collections.Generic;
using CustomDataAccess;

namespace MOBOT.BHL.API.BHLApiDataObjects3
{
    [Serializable]
    public class Page : DataObjectBase, ISetValues
    {
        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Page()
        {
        }

        #endregion Constructors

        #region Properties

        private int _PageID = default(int);
        public int PageID
        {
            get { return _PageID; }
            set { _PageID = value; }
        }

        private string _bhlType = null;
        public string BHLType
        {
            get { return _bhlType; }
            set { _bhlType = value; }
        }

        private string _ItemID = null;
        public string ItemID
        {
            get { return _ItemID; }
            set { _ItemID = value; }
        }

        private string _PartID = null;
        public string PartID
        {
            get { return _PartID; }
            set { _PartID = value; }
        }

        private string _Volume = null;
        public string Volume
        {
            get
            {
                return _Volume;
            }
            set
            {
                if (value != null) value = CalibrateValue(value, 20);
                _Volume = value;
            }
        }

        private string _Issue = null;
        public string Issue
        {
            get
            {
                return _Issue;
            }
            set
            {
                if (value != null) value = CalibrateValue(value, 20);
                _Issue = value;
            }
        }

        private string _Year = null;
        public string Year
        {
            get
            {
                return _Year;
            }
            set
            {
                if (value != null) value = CalibrateValue(value, 20);
                _Year = value;
            }
        }

        private string _TextSource = null;
        public string TextSource
        {
            get { return _TextSource; }
            set { _TextSource = value; }
        }

        private string _PageUrl = null;
        public string PageUrl
        {
            get { return _PageUrl; }
            set { _PageUrl = value; }
        }

        private string _ThumbnailUrl = null;
        public string ThumbnailUrl
        {
            get { return _ThumbnailUrl; }
            set { _ThumbnailUrl = value; }
        }

        private string _FullSizeImageUrl = null;
        public string FullSizeImageUrl
        {
            get { return _FullSizeImageUrl; }
            set { _FullSizeImageUrl = value; }
        }

        private string _OcrUrl = null;
        public string OcrUrl
        {
            get { return _OcrUrl; }
            set { _OcrUrl = value; }
        }

        private string _OcrText = null;
        public string OcrText
        {
            get { return _OcrText; }
            set { _OcrText = value; }
        }

        private string _creationDate = null;
        public string CreationDate
        {
            get { return _creationDate; }
            set { _creationDate = value; }
        }

        List<PageType> _PageTypes = null;
        public List<PageType> PageTypes
        {
            get { return _PageTypes; }
            set { _PageTypes = value; }
        }

        List<PageNumber> _PageNumbers = null;
        public List<PageNumber> PageNumbers
        {
            get { return _PageNumbers; }
            set { _PageNumbers = value; }
        }

        List<Name> _Names = null;
        public List<Name> Names
        {
            get { return _Names; }
            set { _Names = value; }
        }

        #endregion Properties

        #region ISetValues Members

        public void SetValues(CustomDataRow row)
        {
            foreach (CustomDataColumn column in row)
            {
                switch (column.Name)
                {
                    case "PageID":
                        {
                            _PageID = (int)column.Value;
                            break;
                        }
                    case "BHLType":
                        {
                            _bhlType = (string)column.Value;
                            break;
                        }
                    case "ItemID":
                        {
                            _ItemID = column.Value == null ? null : Utility.NullIfEmpty(column.Value.ToString());
                            break;
                        }
                    case "PartID":
                        {
                            _PartID = column.Value == null ? null : Utility.NullIfEmpty(column.Value.ToString());
                            break;
                        }
                    case "Volume":
                        {
                            _Volume = (string)column.Value;
                            break;
                        }
                    case "Issue":
                        {
                            _Issue = (string)column.Value;
                            break;
                        }
                    case "Year":
                        {
                            _Year = (string)column.Value;
                            break;
                        }
                    case "TextSource":
                        {
                            _TextSource = (string)column.Value;
                            break;
                        }
                    case "CreationDate":
                        {
                            _creationDate = column.Value == null ? null : ((DateTime)column.Value).ToString("yyyy/MM/dd HH:mm:ss");
                            break;
                        }
                }
            }
        }

        #endregion
    }
}
