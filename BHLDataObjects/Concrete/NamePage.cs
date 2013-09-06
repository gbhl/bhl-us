
#region Using

using System;
using CustomDataAccess;

#endregion Using

namespace MOBOT.BHL.DataObjects
{
	[Serializable]
	public class NamePage : __NamePage
	{
        private string _urlName = string.Empty;

        public string UrlName
        {
            get { return _urlName; }
            set { _urlName = value; }
        }

        private string _sourceName = string.Empty;

        public string SourceName
        {
            get { return _sourceName; }
            set 
            {
                if (_sourceName != value)
                {
                    _sourceName = value; _IsDirty = true;
                }
            }
        }

        private string _nameString = string.Empty;

        public string NameString
        {
            get { return _nameString; }
            set
            {
                if (_nameString != value)
                {
                    _nameString = value; _IsDirty = true;
                }
            }
        }

        private string _resolvedNameString = string.Empty;

        public string ResolvedNameString
        {
            get { return _resolvedNameString; }
            set
            {
                if (_resolvedNameString != value)
                {
                    _resolvedNameString = value; _IsDirty = true;
                }
            }
        }

        private string _nameBankID = string.Empty;

        public string NameBankID
        {
            get { return _nameBankID; }
            set
            {
                if (_nameBankID != value)
                {
                    _nameBankID = value; _IsDirty = true;
                }
            }
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

        private short _isActive = 0;

        public short IsActive
        {
            get { return _isActive; }
            set
            {
                if (_isActive != value)
                {
                    _isActive = value; _IsDirty = true;
                }
            }
        }

        public override void SetValues(CustomDataRow row)
        {
            foreach (CustomDataColumn column in row)
            {
                if (column.Name == "SourceName")
                {
                    _sourceName = Utility.EmptyIfNull(column.Value);
                }
                if (column.Name == "NameString")
                {
                    _nameString = Utility.EmptyIfNull(column.Value);
                }
                if (column.Name == "ResolvedNameString")
                {
                    _resolvedNameString = Utility.EmptyIfNull(column.Value);
                }
                if (column.Name == "NameBankID")
                {
                    _nameBankID = Utility.EmptyIfNull(column.Value);
                }
                if (column.Name == "EOLID")
                {
                    _eolID = Utility.EmptyIfNull(column.Value);
                }
                if (column.Name == "IsActive")
                {
                    _isActive = (short)Utility.ZeroIfNull(Convert.ToInt32(column.Value));
                }
            }
            base.SetValues(row);
        }
    }
}
