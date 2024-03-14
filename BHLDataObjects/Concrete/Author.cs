using System;
using System.Collections.Generic;
using CustomDataAccess;

namespace MOBOT.BHL.DataObjects
{
	[Serializable]
	public class Author : __Author
	{
        public Author() : base()
        {
        }

        public Author(int authorID,
            int? authorTypeID,
            string startDate,
            string endDate,
            string numeration,
            string title,
            string unit,
            string location,
            string note,
            short isActive,
            int? redirectAuthorID,
            DateTime? creationDate,
            DateTime? lastModifiedDate,
            int? creationUserID,
            int? lastModifiedUserID) : base(authorID, authorTypeID, startDate, endDate, numeration, title, unit, location,
                note, isActive, redirectAuthorID, creationDate, lastModifiedDate, creationUserID, lastModifiedUserID)
        {
        }

        private string _authorTypeName = string.Empty;

        public string AuthorTypeName
        {
            get { return _authorTypeName; }
            set { _authorTypeName = value; }
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

        private string _relationship;

        public string Relationship
        {
            get { return _relationship; }
            set { _relationship = value; }
        }

        private string _titleOfWork;

        public string TitleOfWork
        {
            get { return _titleOfWork; }
            set { _titleOfWork = value; }
        }

        private List<AuthorName> _authorNames;

        public List<AuthorName> AuthorNames
        {
            get { return _authorNames; }
            set { _authorNames = value; }
        }

        private List<AuthorIdentifier> _authorIdentifiers;

        public List<AuthorIdentifier> AuthorIdentifiers
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
                    case "AuthorTypeName":
                        AuthorTypeName = Utility.EmptyIfNull(column.Value);
                        break;
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
                    case "Relationship":
                        Relationship = Utility.EmptyIfNull(column.Value);
                        break;
                    case "TitleOfWork":
                        TitleOfWork = Utility.EmptyIfNull(column.Value);
                        break;
                }
            }
            base.SetValues(row);
        }

    }
}
