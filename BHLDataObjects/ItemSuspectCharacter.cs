using System;
using System.Collections.Generic;
using System.Text;
using CustomDataAccess;

namespace MOBOT.BHL.DataObjects
{
    public class ItemSuspectCharacter : ISetValues
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

        private String _shortTitle;

        public String ShortTitle
        {
            get { return _shortTitle; }
            set { _shortTitle = value; }
        }

        private int _itemID;

        public int ItemID
        {
            get { return _itemID; }
            set { _itemID = value; }
        }

        private String _barCode;

        public String BarCode
        {
            get { return _barCode; }
            set { _barCode = value; }
        }

        private String _volumeSuspect;

        public String VolumeSuspect
        {
            get { return _volumeSuspect; }
            set { _volumeSuspect = value; }
        }

        private String _volume;

        public String Volume
        {
            get { return _volume; }
            set { _volume = value; }
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
                    case "ShortTitle":
                        {
                            _shortTitle = (String)column.Value;
                            break;
                        }
                    case "ItemID":
                        {
                            _itemID = (int)column.Value;
                            break;
                        }
                    case "BarCode":
                        {
                            _barCode = (String)column.Value;
                            break;
                        }
                    case "VolumeSuspect":
                        {
                            _volumeSuspect = (String)column.Value;
                            break;
                        }
                    case "Volume":
                        {
                            _volume = (String)column.Value;
                            break;
                        }
                }
            }
        }

        #endregion
    }
}
