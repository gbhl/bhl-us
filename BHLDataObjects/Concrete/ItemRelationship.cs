
#region Using

using CustomDataAccess;
using System;

#endregion Using

namespace MOBOT.BHL.DataObjects
{
	[Serializable]
	public class ItemRelationship : __ItemRelationship
	{
		private int? _bookID;

		public int? BookID
        {
			get { return _bookID; }
			set { _bookID = value; }
        }

		private int? _segmentID;

		public int? SegmentID
        {
			get { return _segmentID; }
			set { _segmentID = value; }
        }

		private short _isParent = 0;

		public short IsParent
        {
			get { return _isParent; }
			set { _isParent = value; }
        }


		private short _isChild = 0;
		public short IsChild
        {
			get { return _isChild; }
			set { _isChild = value; }
        }

        public override void SetValues(CustomDataRow row)
        {
            foreach (CustomDataColumn column in row)
            {
                switch (column.Name)
                {
                    case "BookID":
                        {
                            _bookID = (int?)column.Value;
                            break;
                        }
                    case "SegmentID":
                        {
                            _segmentID = (int?)column.Value;
                            break;
                        }
                    case "IsParent":
                        {
                            _isParent = (short)column.Value;
                            break;
                        }
                    case "IsChild":
                        {
                            _isChild = (short)column.Value;
                            break;
                        }
                }
            }

            base.SetValues(row);
        }
    }
}
