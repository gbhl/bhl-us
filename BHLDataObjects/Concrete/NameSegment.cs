
#region Using

using System;
using CustomDataAccess;

#endregion Using

namespace MOBOT.BHL.DataObjects
{
	[Serializable]
	public class NameSegment : __NameSegment
	{
        private int? _nameResolvedID = null;

        public int? NameResolvedID
        {
            get { return _nameResolvedID; }
            set { _nameResolvedID = value; }
        }

        private string _nameString = string.Empty;

        public string NameString
        {
            get { return _nameString; }
            set { _nameString = value; }
        }

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

        public override void SetValues(CustomDataRow row)
        {
            foreach (CustomDataColumn column in row)
            {
                if (column.Name == "NameResolvedID")
                {
                    _nameResolvedID = Utility.ZeroIfNull(column.Value);
                }
                if (column.Name == "NameString")
                {
                    _nameString = Utility.EmptyIfNull(column.Value);
                }
                if (column.Name == "ResolvedNameString")
                {
                    _resolvedNameString = Utility.EmptyIfNull(column.Value);
                }
                if (column.Name == "CanonicalNameString")
                {
                    _canonicalNameString = Utility.EmptyIfNull(column.Value);
                }
            }
            base.SetValues(row);
        }
	}
}
