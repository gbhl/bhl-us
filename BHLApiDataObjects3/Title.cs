using System;
using CustomDataAccess;

namespace MOBOT.BHL.API.BHLApiDataObjects3
{
    [Serializable]
    public class Title : DataObjectBase, ISetValues
    {
        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Title()
        {
        }

        #endregion Constructors

        #region Properties

        private int _TitleID = default(int);
        public int TitleID
        {
            get { return _TitleID; }
            set { _TitleID = value; }
        }

        private string _BibliographicLevel = string.Empty;
        public string BibliographicLevel
        {
            get { return _BibliographicLevel; }
            set { _BibliographicLevel = value; }
        }

        private string _MaterialType = string.Empty;
        public string MaterialType
        {
            get { return _MaterialType; }
            set { _MaterialType = value; }
        }

        private string _FullTitle = null;
        public string FullTitle
        {
            get { return _FullTitle; }
            set
            {
                if (value != null) value = CalibrateValue(value, 2000);
                _FullTitle = value;
            }
        }

        private string _ShortTitle = null;
        public string ShortTitle
        {
            get { return _ShortTitle; }
            set
            {
                if (value != null) value = CalibrateValue(value, 255);
                _ShortTitle = value;
            }
        }

        private string _SortTitle = null;
        public string SortTitle
        {
            get { return _SortTitle; }
            set
            {
                if (value != null) value = CalibrateValue(value, 60);
                _SortTitle = value;
            }
        }

        private string _PartNumber = null;
        public string PartNumber
        {
            get { return _PartNumber; }
            set
            {
                if (value != null) value = CalibrateValue(value, 255);
                _PartNumber = value;
            }
        }

        private string _PartName = null;
        public string PartName
        {
            get { return _PartName; }
            set
            {
                if (value != null) value = CalibrateValue(value, 255);
                _PartName = value;
            }
        }

        private string _CallNumber = null;
        public string CallNumber
        {
            get { return _CallNumber; }
            set
            {
                if (value != null) value = CalibrateValue(value, 100);
                _CallNumber = value;
            }
        }

        private string _Edition = null;
        public string Edition
        {
            get { return _Edition; }
            set
            {
                if (value != null) value = CalibrateValue(value, 450);
                _Edition = value;
            }
        }

        private string _PublisherPlace = null;
        public string PublisherPlace
        {
            get
            {
                return _PublisherPlace;
            }
            set
            {
                if (value != null) value = CalibrateValue(value, 150);
                _PublisherPlace = value;
            }
        }

        private string _PublisherName = null;
        public string PublisherName
        {
            get { return _PublisherName; }
            set
            {
                if (value != null) value = CalibrateValue(value, 255);
                _PublisherName = value;
            }
        }

        private string _PublicationDate = null;
        public string PublicationDate
        {
            get { return _PublicationDate; }
            set
            {
                if (value != null) value = CalibrateValue(value, 100);
                _PublicationDate = value;
            }
        }

        private string _PublicationFrequency = null;
        public string PublicationFrequency
        {
            get { return _PublicationFrequency; }
            set
            {
                if (value != null) value = CalibrateValue(value, 100);
                _PublicationFrequency = value;
            }
        }

        private string _doi = null;
        public string Doi
        {
            get { return _doi; }
            set { _doi = value; }
        }

        private string _TitleUrl = null;
        public string TitleUrl
        {
            get
            {
                return _TitleUrl;
            }
            set
            {
                _TitleUrl = value;
            }
        }

        CustomGenericList<Creator> _Authors;
        public CustomGenericList<Creator> Authors
        {
            get { return _Authors; }
            set { _Authors = value; }
        }

        CustomGenericList<Subject> _Subjects;
        public CustomGenericList<Subject> Subjects
        {
            get { return _Subjects; }
            set { _Subjects = value; }
        }

        CustomGenericList<Identifier> _Identifiers;
        public CustomGenericList<Identifier> Identifiers
        {
            get { return _Identifiers; }
            set { _Identifiers = value; }
        }

        CustomGenericList<Collection> _Collections;
        public CustomGenericList<Collection> Collections
        {
            get { return _Collections; }
            set { _Collections = value; }
        }

        CustomGenericList<TitleVariant> _Variants;
        public CustomGenericList<TitleVariant> Variants
        {
            get { return _Variants; }
            set { _Variants = value; }
        }

        CustomGenericList<Item> _Items;
        public CustomGenericList<Item> Items
        {
            get { return _Items; }
            set { _Items = value; }
        }

        CustomGenericList<TitleNote> _Notes;
        public CustomGenericList<TitleNote> Notes
        {
            get { return _Notes; }
            set { _Notes = value; }
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
                            _TitleID = (int)column.Value;
                            break;
                        }
                    case "BibliographicLevelName":
                        {
                            _BibliographicLevel = (string)column.Value;
                            break;
                        }
                    case "MaterialTypeLabel":
                        {
                            _MaterialType = (string)column.Value;
                            break;
                        }
                    case "FullTitle":
                        {
                            _FullTitle = (string)column.Value;
                            break;
                        }
                    case "ShortTitle":
                        {
                            _ShortTitle = (string)column.Value;
                            break;
                        }
                    case "SortTitle":
                        {
                            _SortTitle = (string)column.Value;
                            break;
                        }
                    case "PartNumber":
                        {
                            _PartNumber = (string)column.Value;
                            break;
                        }
                    case "PartName":
                        {
                            _PartName = (string)column.Value;
                            break;
                        }
                    case "CallNumber":
                        {
                            _CallNumber = (string)column.Value;
                            break;
                        }
                    case "EditionStatement":
                        {
                            _Edition = (string)column.Value;
                            break;
                        }
                    case "Datafield_260_a":
                        {
                            _PublisherPlace = (string)column.Value;
                            break;
                        }
                    case "Datafield_260_b":
                        {
                            _PublisherName = (string)column.Value;
                            break;
                        }
                    case "Datafield_260_c":
                        {
                            _PublicationDate = (string)column.Value;
                            break;
                        }
                    case "CurrentPublicationFrequency":
                        {
                            _PublicationFrequency = (string)column.Value;
                            break;
                        }
                    case "DOIName":
                        {
                            _doi = (string)column.Value;
                            break;
                        }
                }
            }
        }

        #endregion
    }
}
