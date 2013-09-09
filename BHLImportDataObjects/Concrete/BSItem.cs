
#region Using

using System;
using CustomDataAccess;

#endregion Using

namespace MOBOT.BHLImport.DataObjects
{
	[Serializable]
	public class BSItem : __BSItem
	{
        private string _title = string.Empty;

        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        private string _volume = string.Empty;

        public string Volume
        {
            get { return _volume; }
            set { _volume = value; }
        }

        private int _totalSegments = 0;

        public int TotalSegments
        {
            get { return _totalSegments; }
            set { _totalSegments = value; }
        }

        private int _totalItems = 0;

        public int TotalItems
        {
            get { return _totalItems; }
            set { _totalItems = value; }
        }

        public override void SetValues(CustomDataRow row)
        {
            foreach (CustomDataColumn column in row)
            {
                switch (column.Name)
                {
                    case "Title":
                        {
                            _title = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "Volume":
                        {
                            _volume = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "TotalSegments":
                        {
                            _totalSegments = Utility.ZeroIfNull(column.Value);
                            break;
                        }
                    case "TotalItems":
                        {
                            _totalItems = Utility.ZeroIfNull(column.Value);
                            break;
                        }
                }
            }

            base.SetValues(row);
        }
    }
}
