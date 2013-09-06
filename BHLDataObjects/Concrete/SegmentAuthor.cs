using System;
using CustomDataAccess;

namespace MOBOT.BHL.DataObjects
{
	[Serializable]
	public class SegmentAuthor : __SegmentAuthor
	{
        private Author _author = null;

        public Author Author
        {
            get { return _author; }
            set { _author = value; }
        }

        private string _fullName = string.Empty;

        public string FullName
        {
            get { return _fullName; }
            set { _fullName = value; }
        }

        public override void SetValues(CustomDataAccess.CustomDataRow row)
        {
            foreach (CustomDataColumn column in row)
            {
                switch (column.Name)
                {
                    case "FullName":
                        FullName = Utility.EmptyIfNull(column.Value);
                        break;
                }
            }
            base.SetValues(row);
        }
    }
}
