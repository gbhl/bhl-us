using System;
using System.Collections.Generic;
using System.Text;
using CustomDataAccess;

namespace MOBOT.BHL.DataObjects
{
    public class AuthorSuspectCharacter : ISetValues
    {
        private int _titleID;

        public int TitleID
        {
            get { return _titleID; }
            set { _titleID = value; }
        }

        private String _institutionCode;

        public String InstitutionCode
        {
            get { return _institutionCode; }
            set { _institutionCode = value; }
        }

        private String _institutionName;

        public String InstitutionName
        {
            get { return _institutionName; }
            set { _institutionName = value; }
        }

        private DateTime _creationDate;

        public DateTime CreationDate
        {
            get { return _creationDate; }
            set { _creationDate = value; }
        }

        private String _oclc;

        public String Oclc
        {
            get { return _oclc; }
            set { _oclc = value; }
        }

        private String _zQuery;

        public String ZQuery
        {
            get { return _zQuery; }
            set { _zQuery = value; }
        }

        private int _authorID;

        public int AuthorID
        {
            get { return _authorID; }
            set { _authorID = value; }
        }

        private String _nameSuspect;

        public String NameSuspect
        {
            get { return _nameSuspect; }
            set { _nameSuspect = value; }
        }

        private String _fullName;

        public String FullName
        {
            get { return _fullName; }
            set { _fullName = value; }
        }

        private String _numerationSuspect;

        public String NumerationSuspect
        {
            get { return _numerationSuspect; }
            set { _numerationSuspect = value; }
        }

        private String _numeration;

        public String Numeration
        {
            get { return _numeration; }
            set { _numeration = value; }
        }

        private String _unitSuspect;

        public String UnitSuspect
        {
            get { return _unitSuspect; }
            set { _unitSuspect = value; }
        }

        private String _unit;

        public String Unit
        {
            get { return _unit; }
            set { _unit = value; }
        }

        private String _titleSuspect;

        public String TitleSuspect
        {
            get { return _titleSuspect; }
            set { _titleSuspect = value; }
        }

        private String _title;

        public String Title
        {
            get { return _title; }
            set { _title = value; }
        }

        private String _locationSuspect;

        public String LocationSuspect
        {
            get { return _locationSuspect; }
            set { _locationSuspect = value; }
        }

        private String _location;

        public String Location
        {
            get { return _location; }
            set { _location = value; }
        }

        private String _fullerFormSuspect;

        public String FullerFormSuspect
        {
            get { return _fullerFormSuspect; }
            set { _fullerFormSuspect = value; }
        }

        private String _fullerForm;

        public String FullerForm
        {
            get { return _fullerForm; }
            set { _fullerForm = value; }
        }

        #region ISetValues Members

        public void SetValues(CustomDataRow row)
        {
            foreach (CustomDataColumn column in row)
            {
                switch (column.Name)
                {
                    case "TitleID":
                        {
                            _titleID = (int)column.Value;
                            break;
                        }
                    case "InstitutionCode":
                        {
                            _institutionCode = (String)column.Value;
                            break;
                        }
                    case "InstitutionName":
                        {
                            _institutionName = (String)column.Value;
                            break;
                        }
                    case "CreationDate":
                        {
                            _creationDate = (DateTime)column.Value;
                            break;
                        }
                    case "OCLC":
                        {
                            _oclc = (String)column.Value;
                            break;
                        }
                    case "ZQuery":
                        {
                            _zQuery = (String)column.Value;
                            break;
                        }
                    case "AuthorID":
                        {
                            _authorID = (int)column.Value;
                            break;
                        }
                    case "NameSuspect":
                        {
                            _nameSuspect = (String)column.Value;
                            break;
                        }
                    case "FullName":
                        {
                            _fullName = (String)column.Value;
                            break;
                        }
                    case "NumerationSuspect":
                        {
                            _numerationSuspect = (String)column.Value;
                            break;
                        }
                    case "Numeration":
                        {
                            _numeration = (String)column.Value;
                            break;
                        }
                    case "UnitSuspect":
                        {
                            _unitSuspect = (String)column.Value;
                            break;
                        }
                    case "Unit":
                        {
                            _unit = (String)column.Value;
                            break;
                        }
                    case "TitleSuspect":
                        {
                            _titleSuspect = (String)column.Value;
                            break;
                        }
                    case "Title":
                        {
                            _title = (String)column.Value;
                            break;
                        }
                    case "LocationSuspect":
                        {
                            _locationSuspect = (String)column.Value;
                            break;
                        }
                    case "Location":
                        {
                            _location = (String)column.Value;
                            break;
                        }
                    case "FullerFormSuspect":
                        {
                            _fullerFormSuspect = (String)column.Value;
                            break;
                        }
                    case "FullerForm":
                        {
                            _fullerForm = (String)column.Value;
                            break;
                        }
                }
            }
        }

        #endregion
    }
}
