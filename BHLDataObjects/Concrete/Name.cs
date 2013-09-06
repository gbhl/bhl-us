
#region Using

using System;
using CustomDataAccess;

#endregion Using

namespace MOBOT.BHL.DataObjects
{
	[Serializable]
	public class Name : __Name
	{
        private string _resolvedNameString = string.Empty;

        public string ResolvedNameString
        {
            get { return _resolvedNameString; }
            set { _resolvedNameString = value; }
        }

        private string _canonicalNameString = string.Empty;

        public string CanonicalNameString
        {
            get { return _canonicalNameString; }
            set { _canonicalNameString = value; }
        }

        private string _namebankID;

        public string NamebankID
        {
            get { return _namebankID; }
            set { _namebankID = value; }
        }

        private string _eolID;

        public string EOLID
        {
            get { return _eolID; }
            set { _eolID = value; }
        }

        public override void SetValues(CustomDataRow row)
        {
            foreach (CustomDataColumn column in row)
            {
                if (column.Name == "ResolvedNameString")
                {
                    _resolvedNameString = Utility.EmptyIfNull(column.Value);
                }
                if (column.Name == "CanonicalNameString")
                {
                    _canonicalNameString = Utility.EmptyIfNull(column.Value);
                }
                if (column.Name == "NamebankID")
                {
                    _namebankID = Utility.EmptyIfNull(column.Value);
                }
                if (column.Name == "EOLID")
                {
                    _eolID = Utility.EmptyIfNull(column.Value);
                }
            }
            base.SetValues(row);
        }
    }
}
