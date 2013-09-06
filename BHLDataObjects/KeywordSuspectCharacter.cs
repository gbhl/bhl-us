using System;
using System.Collections.Generic;
using System.Text;
using CustomDataAccess;

namespace MOBOT.BHL.DataObjects
{
    public class KeywordSuspectCharacter : ISetValues
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

        private String _keywordSuspect;

        public String KeywordSuspect
        {
            get { return _keywordSuspect; }
            set { _keywordSuspect = value; }
        }

        private String _keyword;

        public String Keyword
        {
            get { return _keyword; }
            set { _keyword = value; }
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
                    case "KeywordSuspect":
                        {
                            _keywordSuspect = (String)column.Value;
                            break;
                        }
                    case "Keyword":
                        {
                            _keyword = (String)column.Value;
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
                }
            }
        }

        #endregion
    }
}
