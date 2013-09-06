using System;
using System.Collections.Generic;
using System.Text;
using CustomDataAccess;

namespace MOBOT.BHL.DataObjects
{
    public class TitleSuspectCharacter : ISetValues
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

        private String _fullTitleSuspect;

        public String FullTitleSuspect
        {
            get { return _fullTitleSuspect; }
            set { _fullTitleSuspect = value; }
        }

        private String _fullTitle;

        public String FullTitle
        {
            get { return _fullTitle; }
            set { _fullTitle = value; }
        }

        private String _shortTitleSuspect;

        public String ShortTitleSuspect
        {
            get { return _shortTitleSuspect; }
            set { _shortTitleSuspect = value; }
        }

        private String _shortTitle;

        public String ShortTitle
        {
            get { return _shortTitle; }
            set { _shortTitle = value; }
        }

        private String _sortTitleSuspect;

        public String SortTitleSuspect
        {
            get { return _sortTitleSuspect; }
            set { _sortTitleSuspect = value; }
        }

        private String _sortTitle;

        public String SortTitle
        {
            get { return _sortTitle; }
            set { _sortTitle = value; }
        }

        private String _datafield260aSuspect;

        public String Datafield260aSuspect
        {
            get { return _datafield260aSuspect; }
            set { _datafield260aSuspect = value; }
        }

        private String _datafield260a;

        public String Datafield260a
        {
            get { return _datafield260a; }
            set { _datafield260a = value; }
        }

        private String _datafield260bSuspect;

        public String Datafield260bSuspect
        {
            get { return _datafield260bSuspect; }
            set { _datafield260bSuspect = value; }
        }

        private String _datafield260b;

        public String Datafield260b
        {
            get { return _datafield260b; }
            set { _datafield260b = value; }
        }

        private String _pubDetailsSuspect;

        public String PubDetailsSuspect
        {
            get { return _pubDetailsSuspect; }
            set { _pubDetailsSuspect = value; }
        }

        private String _publicationDetails;

        public String PublicationDetails
        {
            get { return _publicationDetails; }
            set { _publicationDetails = value; }
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
                    case "FullTitleSuspect":
                        {
                            _fullTitleSuspect = (String)column.Value;
                            break;
                        }
                    case "FullTitle":
                        {
                            _fullTitle = (String)column.Value;
                            break;
                        }
                    case "ShortTitleSuspect":
                        {
                            _shortTitleSuspect = (String)column.Value;
                            break;
                        }
                    case "ShortTitle":
                        {
                            _shortTitle = (String)column.Value;
                            break;
                        }
                    case "SortTitleSuspect":
                        {
                            _sortTitleSuspect = (String)column.Value;
                            break;
                        }
                    case "SortTitle":
                        {
                            _sortTitle = (String)column.Value;
                            break;
                        }
                    case "DataField260aSuspect":
                        {
                            _datafield260aSuspect = (String)column.Value;
                            break;
                        }
                    case "DataField_260_a":
                        {
                            _datafield260a = (String)column.Value;
                            break;
                        }
                    case "DataField260bSuspect":
                        {
                            _datafield260bSuspect = (String)column.Value;
                            break;
                        }
                    case "DataField_260_b":
                        {
                            _datafield260b = (String)column.Value;
                            break;
                        }
                    case "PubDetailsSuspect":
                        {
                            _pubDetailsSuspect = (String)column.Value;
                            break;
                        }
                    case "PublicationDetails":
                        {
                            _publicationDetails = (String)column.Value;
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
