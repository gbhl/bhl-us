
#region Using

using CustomDataAccess;
using System;

#endregion Using

namespace MOBOT.BHL.DataObjects
{
	[Serializable]
	public class Service : __Service
	{
		public string FrequencyLabel { get; set; }
		public int? IntervalInMinutes { get; set; }
		public int? MinutesElapsedSinceLog { get; set; }
		public string SeverityLabel { get; set; }
		public string FGColorHexCode { get; set; }

        public override void SetValues(CustomDataRow row)
        {
            foreach (CustomDataColumn column in row)
            {
                switch (column.Name)
                {
                    case "FrequencyLabel":
                        {
                            FrequencyLabel = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "IntervalInMinutes":
                        {
                            IntervalInMinutes = (int?)column.Value;
                            break;
                        }
                    case "MinutesElapsedSinceLog":
                        {
                            MinutesElapsedSinceLog = (int?)column.Value;
                            break;
                        }
                    case "SeverityLabel":
                        {
                            SeverityLabel = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "FGColorHexCode":
                        {
                            FGColorHexCode = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                }
            }

            base.SetValues(row);
        }
    }
}
