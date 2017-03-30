using System;
using CustomDataAccess;

namespace MOBOT.BHL.DataObjects
{
	[Serializable]
	public class TitleAuthor : __TitleAuthor
	{
        private Author _author = null;

        public Author Author
        {
            get { return _author; }
            set { _author = value; }
        }

        private string _roleDescription = string.Empty;
        public string RoleDescription
        {
            get { return _roleDescription; }
            set { _roleDescription = value; }
        }

        private string _fullName = string.Empty;
        public string FullName
        {
            get { return _fullName; }
            set { _fullName = value; }
        }

        private string _fullerForm = string.Empty;
        public string FullerForm
        {
            get { return _fullerForm; }
            set { _fullerForm = value; }
        }

        private string _numeration = string.Empty;
        public string Numeration
        {
            get { return _numeration; }
            set { _numeration = value; }
        }

        private string _title = string.Empty;
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        private string _unit = string.Empty;
        public string Unit
        {
            get { return _unit; }
            set { _unit = value; }
        }

        private string _location = string.Empty;
        public string Location
        {
            get { return _location; }
            set { _location = value; }
        }

        public override void SetValues(CustomDataAccess.CustomDataRow row)
        {
            foreach (CustomDataColumn column in row)
            {
                switch (column.Name)
                {
                    case "RoleDescription":
                        RoleDescription = Utility.EmptyIfNull(column.Value);
                        break;
                    case "FullName":
                        FullName = Utility.EmptyIfNull(column.Value);
                        break;
                    case "FullerForm":
                        FullerForm = Utility.EmptyIfNull(column.Value);
                        break;
                    case "Numeration":
                        Numeration = Utility.EmptyIfNull(column.Value);
                        break;
                    case "Title":
                        Title = Utility.EmptyIfNull(column.Value);
                        break;
                    case "Unit":
                        Unit = Utility.EmptyIfNull(column.Value);
                        break;
                    case "Location":
                        Location = Utility.EmptyIfNull(column.Value);
                        break;
                }
            }
            base.SetValues(row);
        }
	}
}
