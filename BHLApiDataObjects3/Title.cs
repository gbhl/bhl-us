using System;
using System.Collections.Generic;
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

        private string _Genre = null;
        public string Genre
        {
            get { return _Genre; }
            set { _Genre = value; }
        }

        private string _MaterialType = null;
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
            get { return _TitleUrl; }
            set { _TitleUrl = value; }
        }

        private string _creationDate = null;
        public string CreationDate
        {
            get { return _creationDate; }
            set { _creationDate = value; }
        }

        List<Author> _Authors;
        public List<Author> Authors
        {
            get { return _Authors; }
            set { _Authors = value; }
        }

        List<Subject> _Subjects;
        public List<Subject> Subjects
        {
            get { return _Subjects; }
            set { _Subjects = value; }
        }

        List<Identifier> _Identifiers;
        public List<Identifier> Identifiers
        {
            get { return _Identifiers; }
            set { _Identifiers = value; }
        }

        List<Collection> _Collections;
        public List<Collection> Collections
        {
            get { return _Collections; }
            set { _Collections = value; }
        }

        List<TitleAssociation> _Associations;
        public List<TitleAssociation> Associations
        {
            get { return _Associations; }
            set { _Associations = value; }
        }

        List<TitleVariant> _Variants;
        public List<TitleVariant> Variants
        {
            get { return _Variants; }
            set { _Variants = value; }
        }

        List<Item> _Items;
        public List<Item> Items
        {
            get { return _Items; }
            set { _Items = value; }
        }

        List<TitleNote> _Notes;
        public List<TitleNote> Notes
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
                            _Genre = Utility.NullIfEmpty(column.Value);
                            break;
                        }
                    case "MaterialTypeLabel":
                        {
                            _MaterialType = Utility.NullIfEmpty(column.Value);
                            break;
                        }
                    case "FullTitle":
                        {
                            _FullTitle = Utility.NullIfEmpty(column.Value);
                            break;
                        }
                    case "ShortTitle":
                        {
                            _ShortTitle = Utility.NullIfEmpty(column.Value);
                            break;
                        }
                    case "SortTitle":
                        {
                            _SortTitle = Utility.NullIfEmpty(column.Value);
                            break;
                        }
                    case "PartNumber":
                        {
                            _PartNumber = Utility.NullIfEmpty(column.Value);
                            break;
                        }
                    case "PartName":
                        {
                            _PartName = Utility.NullIfEmpty(column.Value);
                            break;
                        }
                    case "CallNumber":
                        {
                            _CallNumber = Utility.NullIfEmpty(column.Value);
                            break;
                        }
                    case "EditionStatement":
                        {
                            _Edition = Utility.NullIfEmpty(column.Value);
                            break;
                        }
                    case "Datafield_260_a":
                        {
                            _PublisherPlace = Utility.NullIfEmpty(column.Value);
                            break;
                        }
                    case "Datafield_260_b":
                        {
                            _PublisherName = Utility.NullIfEmpty(column.Value);
                            break;
                        }
                    case "Datafield_260_c":
                        {
                            _PublicationDate = Utility.NullIfEmpty(column.Value);
                            break;
                        }
                    case "CurrentPublicationFrequency":
                        {
                            _PublicationFrequency = Utility.NullIfEmpty(column.Value);
                            break;
                        }
                    case "DOIName":
                        {
                            _doi = Utility.NullIfEmpty(column.Value);
                            break;
                        }
                    case "CreationDate":
                        {
                            _creationDate = column.Value == null ? null : ((DateTime)column.Value).ToString("yyyy/MM/dd HH:mm:ss");
                            break;
                        }
                }
            }
        }

        #endregion
    }
}
