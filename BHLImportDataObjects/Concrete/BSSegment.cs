
#region Using

using CustomDataAccess;
using System;

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
