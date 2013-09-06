
#region Using

using System;
using CustomDataAccess;

#endregion Using

namespace MOBOT.BHL.DataObjects
{
	[Serializable]
	public class Annotation : __Annotation
    {
        private int _pageID = -1, _pageSequenceOrder = -1;

        public int PageID
        {
            get { return _pageID; }
            set { _pageID = value; }
        }

        public int PageSequenceOrder
        {
            get { return _pageSequenceOrder; }
            set { _pageSequenceOrder = value; }
        }

        public override void SetValues(CustomDataRow row)
        {
            foreach (CustomDataColumn column in row)
            {
                if (column.Name == "PageID")
                {
                    _pageID = Utility.ZeroIfNull(row["PageID"].Value);
                }
                if (column.Name == "SequenceOrder")
                {
                    _pageSequenceOrder = Utility.ZeroIfNull(row["SequenceOrder"].Value);
                }
            }
            base.SetValues(row);
        }
	}
}
