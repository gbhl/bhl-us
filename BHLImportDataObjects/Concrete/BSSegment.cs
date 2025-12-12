
#region Using

using CustomDataAccess;
using System;
using System.Collections.Generic;

#endregion Using

namespace MOBOT.BHLImport.DataObjects
{
	[Serializable]
	public class BSSegment : __BSSegment
	{
        private string _statusLabel = string.Empty;

        public string StatusLabel
        {
            get { return _statusLabel; }
            set { _statusLabel = value; }
        }

        private List<BSSegmentPage> _bsSegmentPages = new List<BSSegmentPage>();

        public List<BSSegmentPage> BSSegmentPages
        {
            get { return _bsSegmentPages; }
            set { _bsSegmentPages = value; }
        }

        public override void SetValues(CustomDataRow row)
        {
            foreach (CustomDataColumn column in row)
            {
                switch (column.Name)
                {
                    case "StatusLabel":
                        {
                            _statusLabel = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                }
            }

            base.SetValues(row);
        }
    }
}
