using CustomDataAccess;
using System;

namespace MOBOT.BHL.DataObjects
{
	[Serializable]
	public class ServiceLog : __ServiceLog
	{
        public int TotalRecords { get; set; }
		public string Name { get; set; }
		public string Param { get; set; }
		public string FrequencyLabel { get; set; }
		public string SeverityLabel { get; set; }
        public int? IntervalInMinutes { get; set; }
        public int? MinutesElapsedSinceLog { get; set; }
        public string FGColorHexCode { get; set; }

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
                    case "TotalRecords":
                        {
                            TotalRecords = (int)column.Value;
                            break;
                        }
                    case "Name":
                        {
                            Name = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "Param":
                        {
                            Param = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "FrequencyLabel":
                        {
                            FrequencyLabel = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "SeverityLabel":
                        {
                            SeverityLabel = Utility.EmptyIfNull(column.Value);
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

