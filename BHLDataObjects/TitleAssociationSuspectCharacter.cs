using System;
using System.Collections.Generic;
using System.Text;
using CustomDataAccess;

namespace MOBOT.BHL.DataObjects
{
    public class TitleAssociationSuspectCharacter : ISetValues
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

        private int _titleAssociationID;

        public int TitleAssociationID
        {
            get { return _titleAssociationID; }
            set { _titleAssociationID = value; }
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

        private String _sectionSuspect;

        public String SectionSuspect
        {
            get { return _sectionSuspect; }
            set { _sectionSuspect = value; }
        }

        private String _section;

        public String Section
        {
            get { return _section; }
            set { _section = value; }
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

        private String _headingSuspect;

        public String HeadingSuspect
        {
            get { return _headingSuspect; }
            set { _headingSuspect = value; }
        }

        private String _heading;

        public String Heading
        {
            get { return _heading; }
            set { _heading = value; }
        }

        private String _publicationSuspect;

        public String PublicationSuspect
        {
            get { return _publicationSuspect; }
            set { _publicationSuspect = value; }
        }

        private String _publication;

        public String Publication
        {
            get { return _publication; }
            set { _publication = value; }
        }

        private String _relationshipSuspect;

        public String RelationshipSuspect
        {
            get { return _relationshipSuspect; }
            set { _relationshipSuspect = value; }
        }

        private String _relationship;

        public String Relationship
        {
            get { return _relationship; }
            set { _relationship = value; }
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
                    case "TitleAssociationID":
                        {
                            _titleAssociationID = (int)column.Value;
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
                    case "SectionSuspect":
                        {
                            _sectionSuspect = (String)column.Value;
                            break;
                        }
                    case "Section":
                        {
                            _section = (String)column.Value;
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
                    case "HeadingSuspect":
                        {
                            _headingSuspect = (String)column.Value;
                            break;
                        }
                    case "Heading":
                        {
                            _heading = (String)column.Value;
                            break;
                        }
                    case "PublicationSuspect":
                        {
                            _publicationSuspect = (String)column.Value;
                            break;
                        }
                    case "Publication":
                        {
                            _publication = (String)column.Value;
                            break;
                        }
                    case "RelationshipSuspect":
                        {
                            _relationshipSuspect = (String)column.Value;
                            break;
                        }
                    case "Relationship":
                        {
                            _relationship = (String)column.Value;
                            break;
                        }

                }
            }
        }

        #endregion
    }
}
