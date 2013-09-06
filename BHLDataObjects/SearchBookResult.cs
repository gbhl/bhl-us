using System;
using CustomDataAccess;

namespace MOBOT.BHL.DataObjects
{
    [Serializable]
    public class SearchBookResult : ISetValues
    {
        private int _titleID;
        public int TitleID
        {
            get { return _titleID; }
            set { _titleID = value; }
        }

        private int _itemID;
        public int ItemID
        {
            get { return _itemID; }
            set { _itemID = value; }
        }

        private int? _pageID;
        public int? PageID
        {
            get { return _pageID; }
            set { _pageID = value; }
        }

        private string _fullTitle = string.Empty;
        public string FullTitle
        {
            get { return _fullTitle; }
            set { _fullTitle = value; }
        }

        private string _shortTitle = string.Empty;
        public string ShortTitle
        {
            get { return _shortTitle; }
            set { _shortTitle = value; }
        }

        private string _sortTitle = string.Empty;
        public string SortTitle
        {
            get { return _sortTitle; }
            set { _sortTitle = value; }
        }

        private string _partNumber = string.Empty;
        public string PartNumber
        {
            get { return _partNumber; }
            set { _partNumber = value; }
        }

        private string _partName = string.Empty;
        public string PartName
        {
            get { return _partName; }
            set { _partName = value; }
        }

        private string _editionStatement = string.Empty;
        public string EditionStatement
        {
            get { return _editionStatement; }
            set { _editionStatement = value; }
        }

        private string _publicationDetails = string.Empty;
        public string PublicationDetails
        {
            get { return _publicationDetails; }
            set { _publicationDetails = value; }
        }

        private string _year = string.Empty;
        public string Year
        {
            get { return _year; }
            set { _year = value; }
        }

        private string _volume = string.Empty;
        public string Volume
        {
            get { return _volume; }
            set { _volume = value; }
        }

        private string _externalUrl = string.Empty;
        public string ExternalUrl
        {
            get { return _externalUrl; }
            set { _externalUrl = value; }
        }

        private string _authors = string.Empty;
        public string Authors
        {
            get { return _authors; }
            set { _authors = value; }
        }

        private string _collections = string.Empty;
        public string Collections
        {
            get { return _collections; }
            set { _collections = value; }
        }

        private string _associations = string.Empty;
        public string Associations
        {
            get { return _associations; }
            set { _associations = value; }
        }

        private string _subjects = string.Empty;
        public string Subjects
        {
            get { return _subjects; }
            set { _subjects = value; }
        }

        private string _institutionName = string.Empty;
        public string InstitutionName
        {
            get { return _institutionName; }
            set { _institutionName = value; }
        }

        private string _pagePrefix = string.Empty;
        public string PagePrefix
        {
            get { return _pagePrefix; }
            set { _pagePrefix = value; }
        }

        private string _pageNumber = string.Empty;
        public string PageNumber
        {
            get { return _pageNumber; }
            set { _pageNumber = value; }
        }

        private string _pageTypeName = string.Empty;
        public string PageTypeName
        {
            get { return _pageTypeName; }
            set { _pageTypeName = value; }
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
                            TitleID = (int)column.Value;
                            break;
                        }
                    case "ItemID":
                        {
                            ItemID = (int)column.Value;
                            break;
                        }
                    case "PageID":
                        {
                            PageID = (int?)column.Value;
                            break;
                        }
                    case "FullTitle":
                        {
                            FullTitle = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "ShortTitle":
                        {
                            ShortTitle = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "SortTitle":
                        {
                            SortTitle = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "PartNumber":
                        {
                            PartNumber = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "PartName":
                        {
                            PartName = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "EditionStatement":
                        {
                            EditionStatement = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "PublicationDetails":
                        {
                            PublicationDetails = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "Year":
                        {
                            Year = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "Volume":
                        {
                            Volume = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "ExternalUrl":
                        {
                            ExternalUrl = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "Authors":
                        {
                            Authors = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "Collections":
                        {
                            Collections = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "Associations":
                        {
                            Associations = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "Subjects":
                        {
                            Subjects = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "InstitutionName":
                        {
                            InstitutionName = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "PagePrefix":
                        {
                            PagePrefix = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "PageNumber":
                        {
                            PageNumber = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "PageTypeName":
                        {
                            PageTypeName = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                }
            }
        }

        #endregion
    }
}
