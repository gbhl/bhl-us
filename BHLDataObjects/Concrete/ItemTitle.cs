
#region Using

using CustomDataAccess;
using System;

#endregion Using

namespace MOBOT.BHL.DataObjects
{
	[Serializable]
	public class ItemTitle : __ItemTitle
	{
        #region Properties

        private int _bookID;

        public int BookID
        {
            get { return _bookID; }
            set { _bookID = value; }
        }

        private string _marcBibID;

        public string MarcBibID
        {
            get { return _marcBibID; }
            set { _marcBibID = value; }
        }

        private string _shortTitle;

        public string ShortTitle
        {
            get { return _shortTitle; }
            set { _shortTitle = value; }
        }

        private string _barCode;

        public string BarCode
        {
            get { return _barCode; }
            set { _barCode = value; }
        }

        private string _volume;

        public string Volume
        {
            get { return _volume; }
            set { _volume = value; }
        }

        private int _primaryTitleID;

        public int PrimaryTitleID
        {
            get { return _primaryTitleID; }
            set { _primaryTitleID = value; }
        }

        private int _itemStatusID;

        public int ItemStatusID
        {
            get { return _itemStatusID; }
            set { _itemStatusID = value; }
        }

        private bool _titlePublishReady = true;

        public bool TitlePublishReady
        {
            get { return _titlePublishReady; }
            set { _titlePublishReady = value; }
        }

        private bool _hasFlickrImages = false;

        public bool HasFlickrImages
        {
            get { return _hasFlickrImages; }
            set { _hasFlickrImages = value; }
        }

        #endregion

        #region ISet override

        public override void SetValues(CustomDataRow row)
        {
            foreach (CustomDataColumn column in row)
            {
                switch (column.Name)
                {
                    case "BookID":
                        {
                            _bookID = Utility.ZeroIfNull(column.Value);
                            break;
                        }
                    case "MARCBibID":
                        {
                            _marcBibID = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "ShortTitle":
                        {
                            _shortTitle = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "BarCode":
                        {
                            _barCode = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "Volume":
                        {
                            _volume = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "PrimaryTitleID":
                        {
                            _primaryTitleID = Utility.ZeroIfNull(column.Value);
                            break;
                        }
                    case "ItemStatusID":
                        {
                            _itemStatusID = Utility.ZeroIfNull(column.Value);
                            break;
                        }
                    case "PublishReady":
                        {
                            _titlePublishReady = (bool)column.Value;
                            break;
                        }
                    case "HasFlickrImages":
                        {
                            _hasFlickrImages = (((int)column.Value) == 1);
                            break;
                        }
                }
            }

            base.SetValues(row);
        }

        #endregion
    }
}

