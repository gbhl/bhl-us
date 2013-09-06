
#region Using

using System;
using CustomDataAccess;

#endregion Using

namespace MOBOT.BHL.DataObjects
{
	[Serializable]
	public class TitleItem : __TitleItem
	{
        #region Properties

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
        
        #endregion

        #region ISet override

        public override void SetValues(CustomDataRow row)
        {
            foreach (CustomDataColumn column in row)
            {
                switch (column.Name)
                {
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
                }
            }

            base.SetValues(row);
        }

        #endregion

    }
}
