using System;
using CustomDataAccess;

namespace MOBOT.BHL.DataObjects
{
	[Serializable]
	public class SegmentAuthor : __SegmentAuthor
	{
        private Author _author = null;

        public Author Author
        {
            get { return _author; }
            set { _author = value; }
        }

        private string _fullName = string.Empty;

        public string FullName
        {
            get { return _fullName; }
            set { _fullName = value; }
        }

        private string _startDate = string.Empty;

        public string StartDate
        {
            get { return _startDate; }
            set { _startDate = value; }
        }

        private string _endDate = string.Empty;

        public string EndDate
        {
            get { return _endDate; }
            set { _endDate = value; }
        }

        private string _numeration = string.Empty;

        public string Numeration
        {
            get { return _numeration; }
            set { _numeration = value; }
        }

        private string _unit = string.Empty;

        public string Unit
        {
            get { return _unit; }
            set { _unit = value; }
        }

        private string _title = string.Empty;

        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        private string _location = string.Empty;

        public string Location
        {
            get { return _location; }
            set { _location = value; }
        }

        private string _fullerForm = string.Empty;

        public string FullerForm
        {
            get { return _fullerForm; }
            set { _fullerForm = value; }
        }

        public string NameExtended
        {
            get
            {
                return _fullName + ' ' + this.Numeration + ' ' + this.Unit + ' ' + this.Title + ' ' + this.Location + ' ' + this.FullerForm + ' ' + this.Dates;
            }
        }

        public string Dates
        {
            get
            {
                return this.StartDate + (!string.IsNullOrEmpty(this.StartDate) ? "-" : string.Empty) + this.EndDate;
            }
        }

        public override void SetValues(CustomDataAccess.CustomDataRow row)
        {
            foreach (CustomDataColumn column in row)
            {
                switch (column.Name)
                {
                    case "FullName":
                        FullName = Utility.EmptyIfNull(column.Value);
                        break;
                    case "StartDate":
                        StartDate = Utility.EmptyIfNull(column.Value);
                        break;
                    case "EndDate":
                        EndDate = Utility.EmptyIfNull(column.Value);
                        break;
                    case "Numeration":
                        Numeration = Utility.EmptyIfNull(column.Value);
                        break;
                    case "Unit":
                        Unit = Utility.EmptyIfNull(column.Value);
                        break;
                    case "Title":
                        Title = Utility.EmptyIfNull(column.Value);
                        break;
                    case "Location":
                        Location = Utility.EmptyIfNull(column.Value);
                        break;
                    case "FullerForm":
                        FullerForm = Utility.EmptyIfNull(column.Value);
                        break;
                }
            }
            base.SetValues(row);
        }
    }
}
