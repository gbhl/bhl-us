using System;
using CustomDataAccess;

namespace MOBOT.BHL.DataObjects
{
    [Serializable]
    public class SearchAnnotationResult : ISetValues
    {
        private string _annotationText = string.Empty;
        public string AnnotationText
        {
            get { return _annotationText; }
            set { _annotationText = value; }
        }

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

        public string FullTitleExtended
        {
            get
            {
                // Append the PartNumber and PartName to the FullTitle, with proper formatting
                return BHL.Utility.DataCleaner.GetFullTitleExtended(this.FullTitle, this.PartNumber, this.PartName);
            }
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

        private string _volume = string.Empty;
        public string Volume
        {
            get { return _volume; }
            set { _volume = value; }
        }

        private string _authors = string.Empty;
        public string Authors
        {
            get { return _authors; }
            set { _authors = value; }
        }

        private string _pageNumbers = string.Empty;
        public string PageNumbers
        {
            get { return _pageNumbers; }
            set { _pageNumbers = value; }
        }

        private string _pageTypeNames = string.Empty;
        public string PageTypeNames
        {
            get { return _pageTypeNames; }
            set { _pageTypeNames = value; }
        }

        #region ISetValues Members

        public void SetValues(CustomDataRow row)
        {
            foreach (CustomDataColumn column in row)
            {
                switch (column.Name)
                {
                    case "AnnotationText":
                        {
                            AnnotationText = Utility.EmptyIfNull(column.Value);
                            break;
                        }
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
                    case "Volume":
                        {
                            Volume = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "Authors":
                        {
                            Authors = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "PageNumbers":
                        {
                            PageNumbers = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "PageTypes":
                        {
                            PageTypeNames = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                }
            }
        }

        #endregion
    }
}
