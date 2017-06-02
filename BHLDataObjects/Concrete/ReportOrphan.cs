using CustomDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MOBOT.BHL.DataObjects
{
    public class ReportOrphan : ISetValues
    {
        private string _type = string.Empty;

        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }

        private int _id;

        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _status = string.Empty;

        public string Status
        {
            get { return _status; }
            set { _status = value; }
        }

        private int? _replacedBy;

        public int? ReplacedBy
        {
            get { return _replacedBy; }
            set { _replacedBy = value; }
        }

        private string _holdingInstitution = string.Empty;

        public string HoldingInstitution
        {
            get { return _holdingInstitution; }
            set { _holdingInstitution = value; }
        }

        private bool? _hasActiveTitles = null;

        public bool? HasActiveTitles
        {
            get { return _hasActiveTitles; }
            set { _hasActiveTitles = value; }
        }

        private bool? _hasActiveItems = null;

        public bool? HasActiveItems
        {
            get { return _hasActiveItems; }
            set { _hasActiveItems = value; }
        }

        private bool? _hasActiveSegments = null;

        public bool? HasActiveSegments
        {
            get { return _hasActiveSegments; }
            set { _hasActiveSegments = value; }
        }

        public void SetValues(CustomDataRow row)
        {
            foreach (CustomDataColumn column in row)
            {
                switch (column.Name)
                {
                    case "Type":
                        {
                            Type = (String)column.Value;
                            break;
                        }
                    case "ID":
                        {
                            ID = (int)column.Value;
                            break;
                        }
                    case "Status":
                        {
                            Status = (String)column.Value;
                            break;
                        }
                    case "ReplacedBy":
                        {
                            ReplacedBy = (int?)column.Value;
                            break;
                        }
                    case "InstitutionName":
                        {
                            HoldingInstitution = (String)column.Value;
                            break;
                        }
                    case "HasActiveTitles":
                        {
                            HasActiveTitles = BoolOrNull(column.Value);
                            break;
                        }
                    case "HasActiveItems":
                        {
                            HasActiveItems = BoolOrNull(column.Value);
                            break;
                        }
                    case "HasActiveSegments":
                        {
                            HasActiveSegments = BoolOrNull(column.Value);
                            break;
                        }
                }
            }
        }

        // Return null if a null value is passed in, true if 1, or false otherwise
        private bool? BoolOrNull(object value)
        {
            if (value == null) return (bool?)null;
            return (bool?)((int?)value == 1);
        }

    }
}
