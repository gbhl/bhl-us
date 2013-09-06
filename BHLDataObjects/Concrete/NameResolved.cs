
#region Using

using System;
using CustomDataAccess;

#endregion Using

namespace MOBOT.BHL.DataObjects
{
	[Serializable]
	public class NameResolved : __NameResolved
	{
        private string _urlName = string.Empty;

        public string UrlName
        {
            get { return _urlName; }
            set { _urlName = value; }
        }

        private string _nameBankID = string.Empty;

        public string NameBankID
        {
            get { return _nameBankID; }
            set { _nameBankID = value; }
        }

        private string _eolID = string.Empty;

        public string EOLID
        {
            get { return _eolID; }
            set
            {
                if (_eolID != value)
                {
                    _eolID = value; _IsDirty = true;
                }
            }
        }

        private int _pageCount = 0;

        public int PageCount
        {
            get { return _pageCount; }
            set { _pageCount = value; }
        }

        public override void SetValues(CustomDataRow row)
        {
            foreach (CustomDataColumn column in row)
            {
                if (column.Name == "NameBankID")
                {
                    _nameBankID = Utility.EmptyIfNull(column.Value);
                }
                if (column.Name == "EOLID")
                {
                    _eolID = Utility.EmptyIfNull(column.Value);
                }
                if (column.Name == "PageCount")
                {
                    _pageCount = Utility.ZeroIfNull(column.Value);
                }
            }
            base.SetValues(row);
        }
    }
}
