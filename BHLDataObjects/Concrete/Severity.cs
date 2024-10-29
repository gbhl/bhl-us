using CustomDataAccess;
using System;

namespace MOBOT.BHL.DataObjects
{
	[Serializable]
	public class Severity : __Severity
    {
        public int TotalLogs { get; set; }

        public override void SetValues(CustomDataRow row)
        {
            foreach (CustomDataColumn column in row)
            {
                switch (column.Name)
                {
                    case "TotalRecords":
                        {
                            TotalLogs = Utility.ZeroIfNull(column.Value);
                            break;
                        }
                }
            }

            base.SetValues(row);
        }
    }
}
