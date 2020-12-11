
#region Using

using System;
using CustomDataAccess;

#endregion Using

namespace MOBOT.BHL.DataObjects
{
	[Serializable]
	public class PDF : __PDF
    {
        #region Properties

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

        private int _numberOfPages;

        public int NumberOfPages
        {
            get { return _numberOfPages; }
            set { _numberOfPages = value; }
        }

        private int _minutesToGenerate;

        public int MinutesToGenerate
        {
            get { return _minutesToGenerate; }
            set { _minutesToGenerate = value; }
        }

        #endregion Properties

        #region ISet override

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
                    case "NumberOfPages":
                        {
                            _numberOfPages = (int)column.Value;
                            break;
                        }
                    case "MinutesToGenerate":
                        {
                            _minutesToGenerate = Utility.ZeroIfNull(column.Value);
                            break;
                        }
                }
            }

            base.SetValues(row);
        }

        #endregion

    }
}
