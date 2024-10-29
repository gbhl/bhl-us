using CustomDataAccess;
using System;

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
        public DateTime? LogCreationDate { get; set; }

        public string FullName
        {
            get
            {
                return (this.Name + " " + (string.IsNullOrWhiteSpace(this.Param) ? "" : "/") + " " + this.Param).Trim();
            }
        }

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
                    case "LogCreationDate":
                        {
                            LogCreationDate = (DateTime?)column.Value;
                            break;
                        }
                }
            }

            base.SetValues(row);
        }
    }
}

