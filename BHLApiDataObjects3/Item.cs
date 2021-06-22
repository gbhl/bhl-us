using System;
using System.Collections.Generic;
using CustomDataAccess;

namespace MOBOT.BHL.API.BHLApiDataObjects3
{
    [Serializable]
    public class Item : DataObjectBase, ISetValues
    {
        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Item()
        {
        }

        #endregion Constructors

        #region Properties		

        private int _ItemID = default(int);
        public int ItemID
        {
            get { return _ItemID; }
            set { _ItemID = value; }
        }

        // Primary title ID for the item
        private string _TitleID = null;
        public string TitleID
        {
            get { return _TitleID; }
            set { _TitleID = value; }
        }

        private string _ThumbnailPageID = null;
        public string ThumbnailPageID
        {
            get { return _ThumbnailPageID; }
            set { _ThumbnailPageID = value; }
        }

        private string _Source = null;
        public string Source
        {
            get { return _Source; }
            set { _Source = value; }
        }

        private string _SourceIdentifier = null;
        public string SourceIdentifier
        {
            get { return _SourceIdentifier; }
            set { _SourceIdentifier = value; }
        }

        private string _IsVirtual = null;
        public string IsVirtual
        {
            get { return _IsVirtual; }
            set { _IsVirtual = value; }
        }

        private string _Volume = null;
        public string Volume
        {
            get { return _Volume; }
            set
            {
                if (value != null) value = CalibrateValue(value, 100);
                _Volume = value;
            }
        }

        private string _Year = null;
        public string Year
        {
            get { return _Year; }
            set
            {
                if (value != null) value = CalibrateValue(value, 20);
                _Year = value;
            }
        }

        private string _EndYear = null;
        public string EndYear
        {
            get { return _EndYear; }
            set
            {
                if (value != null) value = CalibrateValue(value, 20);
                _EndYear = value;
            }
        }

        private string _CopySpecificInformation = null;
        public string CopySpecificInformation
        {
            get { return _CopySpecificInformation; }
            set { _CopySpecificInformation = value; }
        }

        private string _HoldingInstitution = null;
        public string HoldingInstitution
        {
            get { return _HoldingInstitution; }
            set
            {
                if (value != null) value = CalibrateValue(value, 255);
                _HoldingInstitution = value;
            }
        }

        private string _RightsHolder = null;
        public string RightsHolder
        {
            get { return _RightsHolder; }
            set
            {
                if (value != null) value = CalibrateValue(value, 255);
                _RightsHolder = value;
            }
        }

        private string _ScanningInstitution = null;
        public string ScanningInstitution
        {
            get { return _ScanningInstitution; }
            set
            {
                if (value != null) value = CalibrateValue(value, 255);
                _ScanningInstitution = value;
            }
        }

        private string _Sponsor = null;
        public string Sponsor
        {
            get { return _Sponsor; }
            set
            {
                if (value != null) value = CalibrateValue(value, 100);
                _Sponsor = value;
            }
        }

        private string _Language = null;
        public string Language
        {
            get { return _Language; }
            set
            {
                if (value != null) value = CalibrateValue(value, 20);
                _Language = value;
            }
        }

        private string _LicenseUrl = null;
        public string LicenseUrl
        {
            get { return _LicenseUrl; }
            set { _LicenseUrl = value; }
        }

        private string _Rights = null;
        public string Rights
        {
            get { return _Rights; }
            set { _Rights = value; }
        }

        private string _DueDiligence = null;
        public string DueDiligence
        {
            get { return _DueDiligence; }
            set { _DueDiligence = value; }
        }

        private string _CopyrightStatus = null;
        public string CopyrightStatus
        {
            get { return _CopyrightStatus; }
            set { _CopyrightStatus = value; }
        }

        private string _CopyrightRegion = null;
        public string CopyrightRegion
        {
            get { return _CopyrightRegion; }
            set
            {
                if (value != null) value = CalibrateValue(value, 50);
                _CopyrightRegion = value;
            }
        }

        private string _ExternalUrl = null;
        public string ExternalUrl
        {
            get { return _ExternalUrl; }
            set { _ExternalUrl = value; }
        }

        private string _ItemUrl = null;
        public string ItemUrl
        {
            get { return _ItemUrl; }
            set { _ItemUrl = value; }
        }

        // Primary title URL for this item
        private string _TitleUrl = null;
        public string TitleUrl
        {
            get { return _TitleUrl; }
            set { _TitleUrl = value; }
        }

        private string _ItemThumbUrl = null;
        public string ItemThumbUrl
        {
            get { return _ItemThumbUrl; }
            set { _ItemThumbUrl = value; }
        }

        private string _ItemTextUrl;
        public string ItemTextUrl { get => _ItemTextUrl; set => _ItemTextUrl = value; }

        private string _ItemPDFUrl;
        public string ItemPDFUrl { get => _ItemPDFUrl; set => _ItemPDFUrl = value; }

        private string _ItemImagesUrl;
        public string ItemImagesUrl { get => _ItemImagesUrl; set => _ItemImagesUrl = value; }

        private string _creationDate = null;
        public string CreationDate
        {
            get { return _creationDate; }
            set { _creationDate = value; }
        }

        List<Page> _Pages = null;
        public List<Page> Pages
        {
            get { return _Pages; }
            set { _Pages = value; }
        }

        List<Part> _Parts = null;
        public List<Part> Parts
        {
            get { return _Parts; }
            set { _Parts = value; }
        }

        List<Collection> _Collections = null;
        public List<Collection> Collections
        {
            get { return _Collections; }
            set { _Collections = value; }
        }

        #endregion Properties

        #region Properties (from Title)

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

        List<Author> _Authors;
        public List<Author> Authors
        {
            get { return _Authors; }
            set { _Authors = value; }
        }

        List<Identifier> _Identifiers;
        public List<Identifier> Identifiers
        {
            get { return _Identifiers; }
            set { _Identifiers = value; }
        }

        #endregion Properties (from Title)

        #region ISetValues Members

        public void SetValues(CustomDataRow row)
        {
            foreach (CustomDataColumn column in row)
            {
                switch (column.Name)
                {
                    case "ItemID":
                        {
                            _ItemID = (int)column.Value;
                            break;
                        }
                    case "PrimaryTitleID":
                        {
                            //_TitleID = Utility.NullIfEmpty(column.Value);
                            _TitleID = (column.Value == null ? null : column.Value.ToString());
                            break;
                        }
                    case "ThumbnailPageID":
                        {
                            //_ThumbnailPageID = Utility.NullIfEmpty(column.Value);
                            _ThumbnailPageID = (column.Value == null ? null : column.Value.ToString());
                            break;
                        }
                    case "SourceName":
                        {
                            _Source = Utility.NullIfEmpty(column.Value);
                            break;
                        }
                    case "IsVirtual":
                        {
                            _IsVirtual = (column.Value == null) ? (string)null : column.Value.ToString();
                            break;
                        }
                    case "Barcode":
                        {
                            _SourceIdentifier = Utility.NullIfEmpty(column.Value);
                            break;
                        }
                    case "Volume":
                        {
                            _Volume = Utility.NullIfEmpty(column.Value);
                            break;
                        }
                    case "Year":
                        {
                            _Year = Utility.NullIfEmpty(column.Value);
                            break;
                        }
                    case "EndYear":
                        {
                            _EndYear = Utility.NullIfEmpty(column.Value);
                            break;
                        }
                    case "ItemDescription":
                        {
                            _CopySpecificInformation = Utility.NullIfEmpty(column.Value);
                            break;
                        }
                    case "InstitutionName":
                        {
                            _HoldingInstitution = Utility.NullIfEmpty(column.Value);
                            break;
                        }
                    case "Sponsor":
                        {
                            _Sponsor = Utility.NullIfEmpty(column.Value);
                            break;
                        }
                    case "Language":
                        {
                            _Language = Utility.NullIfEmpty(column.Value);
                            break;
                        }
                    case "LicenseUrl":
                        {
                            _LicenseUrl = Utility.NullIfEmpty(column.Value);
                            break;
                        }
                    case "Rights":
                        {
                            _Rights = Utility.NullIfEmpty(column.Value);
                            break;
                        }
                    case "DueDiligence":
                        {
                            _DueDiligence = Utility.NullIfEmpty(column.Value);
                            break;
                        }
                    case "CopyrightStatus":
                        {
                            _CopyrightStatus = Utility.NullIfEmpty(column.Value);
                            break;
                        }
                    case "CopyrightRegion":
                        {
                            _CopyrightRegion = Utility.NullIfEmpty(column.Value);
                            break;
                        }
                    case "ExternalUrl":
                        {
                            _ExternalUrl = Utility.NullIfEmpty(column.Value);
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
