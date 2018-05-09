using System;
using CustomDataAccess;

namespace MOBOT.BHL.API.BHLApiDataObjects3
{
    public class SearchBookResult : DataObjectBase, ISetValues
    {

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public SearchBookResult()
        {
        }

        #endregion Constructors

        #region Properties

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

        private string _fullTitle = null;
        public string FullTitle
        {
            get { return _fullTitle; }
            set
            {
                if (value != null) value = CalibrateValue(value, 2000);
                _fullTitle = value;
            }
        }

        private string _partNumber = null;
        public string PartNumber
        {
            get { return _partNumber; }
            set
            {
                if (value != null) value = CalibrateValue(value, 255);
                _partNumber = value;
            }
        }

        private string _partName = null;
        public string PartName
        {
            get { return _partName; }
            set
            {
                if (value != null) value = CalibrateValue(value, 255);
                _partName = value;
            }
        }

        private string editionStatement = null;
        public string EditionStatement
        {
            get { return editionStatement; }
            set
            {
                if (value != null) value = CalibrateValue(value, 450);
                editionStatement = value;
            }
        }

        private string _publisherPlace = null;
        public string PublisherPlace
        {
            get { return _publisherPlace; }
            set
            {
                if (value != null) value = CalibrateValue(value, 150);
                _publisherPlace = value;
            }
        }

        private string _publisherName = null;
        public string PublisherName
        {
            get { return _publisherName; }
            set
            {
                if (value != null) value = CalibrateValue(value, 255);
                _publisherName = value;
            }
        }

        private string _publicationDate = null;
        public string PublicationDate
        {
            get { return _publicationDate; }
            set
            {
                if (value != null) value = CalibrateValue(value, 100);
                _publicationDate = value;
            }
        }

        private string _volume = null;
        public string Volume
        {
            get { return _volume; }
            set
            {
                if (value != null) value = CalibrateValue(value, 100);
                _volume = value;
            }
        }

        private string _holdingInstitution = null;
        public string HoldingInstitution
        {
            get { return _holdingInstitution; }
            set
            {
                if (value != null) value = CalibrateValue(value, 255);
                _holdingInstitution = value;
            }
        }

        private CustomGenericList<Author> _authors = null;
        public CustomGenericList<Author> Authors
        {
            get { return _authors; }
            set { _authors = value; }
        }

        private CustomGenericList<Collection> _collections = null;
        public CustomGenericList<Collection> Collections
        {
            get { return _collections; }
            set { _collections = value; }
        }

        #endregion Properties


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
                    case "FullTitle":
                        {
                            FullTitle = Utility.EmptyIfNull(column.Value);
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
                    case "Datafield_260_a":
                        {
                            PublisherPlace = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "Datafield_260_b":
                        {
                            PublisherName = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "Datafield_260_c":
                        {
                            PublicationDate = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "Volume":
                        {
                            Volume = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "InstitutionName":
                        {
                            HoldingInstitution = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                }
            }
        }

        #endregion
    }
}
