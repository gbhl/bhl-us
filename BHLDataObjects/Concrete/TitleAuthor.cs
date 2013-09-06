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
                }
            }
            base.SetValues(row);
        }
	}
}
