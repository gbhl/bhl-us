using System;
using CustomDataAccess;

namespace MOBOT.BHL.DataObjects
{
	[Serializable]
	public class Author : __Author
	{
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

        private int _authorRoleID;

        public int AuthorRoleID
        {
            get { return _authorRoleID; }
            set { _authorRoleID = value; }
        }

        private string _marcDataFieldTag;

        public string MarcDataFieldTag
        {
            get { return _marcDataFieldTag; }
            set { _marcDataFieldTag = value; }
        }

        private CustomGenericList<AuthorName> _authorNames;

        public CustomGenericList<AuthorName> AuthorNames
        {
            get { return _authorNames; }
            set { _authorNames = value; }
        }

        private CustomGenericList<AuthorIdentifier> _authorIdentifiers;

        public CustomGenericList<AuthorIdentifier> AuthorIdentifiers
        {
            get { return _authorIdentifiers; }
            set { _authorIdentifiers = value; }
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
                    case "FullerForm":
                        FullerForm = Utility.EmptyIfNull(column.Value);
                        break;
                    case "AuthorRoleID":
                        AuthorRoleID = Utility.ZeroIfNull(column.Value);
                        break;
                    case "MARCDataFieldTag":
                        MarcDataFieldTag = Utility.EmptyIfNull(column.Value);
                        break;
                }
            }
            base.SetValues(row);
        }

    }
}
