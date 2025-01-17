using System;
using System.Data;
using CustomDataAccess;

namespace MOBOT.BHL.DataObjects
{
    [Serializable]
    public class PermissionsTitle : ISetValues
    {
        public int TotalRecords { get; set; }
        public int TitleID { get; set; }
        public string FullTitle { get; set; }
		public string BibliographicLevelName { get; set; }
        public string MaterialTypeLabel { get; set; }
        public string StartYear { get; set; }
		public string EndYear { get; set; }
        public string Years
        {
            get
            {
                string dateRange = StartYear;
                if (dateRange.Length > 0)
                {
                    if (EndYear.Length > 0) dateRange += "-" + EndYear;
                }
                else
                {
                    dateRange = EndYear;
                }
                return dateRange;
            }
        }
        public bool HasMovingWall { get; set; }
        public int NumNoKnownCopyright { get; set; }
        public int NumInCopyright { get; set; }
        public int NumNotProvided { get; set; }
        public string Issn { get; set; }
        public string Oclc { get; set; }
        public bool HasDocumentation { get; set; }

        public void SetValues(CustomDataRow row)
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
                    case "TitleID":
                        {
                            TitleID = (int)column.Value;
                            break;
                        }
                    case "FullTitle":
                        {
                            FullTitle = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "BibliographicLevelName":
                        {
                            BibliographicLevelName = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "MaterialTypeLabel":
                        {
                            MaterialTypeLabel = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "StartYear":
                        {
                            StartYear = column.Value == null ? "" : column.Value.ToString();
                            break;
                        }
                    case "EndYear":
                        {
                            EndYear = column.Value == null ? "" : column.Value.ToString();
                            break;
                        }
                    case "OCLC":
                        {
                            Oclc = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "ISSN":
                        {
                            Issn = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "HasMovingWall":
                        {
                            HasMovingWall = ((short)column.Value == 1);
                            break;
                        }
                    case "NumNoKnownCopyright":
                        {
                            NumNoKnownCopyright = Utility.ZeroIfNull(column.Value);
                            break;
                        }
                    case "NumInCopyright":
                        {
                            NumInCopyright = Utility.ZeroIfNull(column.Value);
                            break;
                        }
                    case "NumNotProvided":
                        {
                            NumNotProvided = Utility.ZeroIfNull(column.Value);
                            break;
                        }
                    case "HasDocumentation":
                        {
                            HasDocumentation = ((int)column.Value == 1);
                            break;
                        }
                }
            }
        }
    }
}
