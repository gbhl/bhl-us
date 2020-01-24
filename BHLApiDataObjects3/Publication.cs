using CustomDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOBOT.BHL.API.BHLApiDataObjects3
{
    [Serializable]
    public class Publication : DataObjectBase
    {
        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Publication()
        {
        }

        #endregion Constructors

        #region Properties		

        private string _bhlType = null;
        public string BHLType
        {
            get { return _bhlType; }
            set { _bhlType = value; }
        }

        private string _foundIn = null;
        public string FoundIn
        {
            get { return _foundIn; }
            set { _foundIn = value; }
        }

        private string _ItemID = null;
        public string ItemID
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

        List<Collection> _Collections = null;
        public List<Collection> Collections
        {
            get { return _Collections; }
            set { _Collections = value; }
        }

        #endregion Properties

        #region Properties (from Title)

        private string _MaterialType = null;
        public string MaterialType
        {
            get { return _MaterialType; }
            set { _MaterialType = value; }
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

        private string _partUrl = null;
        public string PartUrl
        {
            get { return _partUrl; }
            set { _partUrl = value; }
        }

        private string _partID = null;
        public string PartID
        {
            get { return _partID; }
            set { _partID = value; }
        }

        private string _startPageID = null;
        public string StartPageID
        {
            get { return _startPageID; }
            set { _startPageID = value; }
        }

        private string _sequenceOrder = null;
        public string SequenceOrder
        {
            get { return _sequenceOrder; }
            set { _sequenceOrder = value; }
        }

        private string _genre = null;
        public string Genre
        {
            get { return _genre; }
            set { _genre = value; }
        }

        private string _title = null;
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        private string _translatedTitle = null;
        public string TranslatedTitle
        {
            get { return _translatedTitle; }
            set { _translatedTitle = value; }
        }

        private string _containerTitle = null;
        public string ContainerTitle
        {
            get { return _containerTitle; }
            set { _containerTitle = value; }
        }

        private string _publicationDetails = null;
        public string PublicationDetails
        {
            get { return _publicationDetails; }
            set { _publicationDetails = value; }
        }

        private List<TitleNote> _notes = null;
        public List<TitleNote> Notes
        {
            get { return _notes; }
            set { _notes = value; }
        }

        private string _series = null;
        public string Series
        {
            get { return _series; }
            set { _series = value; }
        }

        private string _issue = null;
        public string Issue
        {
            get { return _issue; }
            set { _issue = value; }
        }

        private string _date = null;
        public string Date
        {
            get { return _date; }
            set { _date = value; }
        }

        private string _pageRange = null;
        public string PageRange
        {
            get { return _pageRange; }
            set { _pageRange = value; }
        }

        private string _startPageNumber = null;
        public string StartPageNumber
        {
            get { return _startPageNumber; }
            set { _startPageNumber = value; }
        }

        private string _endPageNumber = null;
        public string EndPageNumber
        {
            get { return _endPageNumber; }
            set { _endPageNumber = value; }
        }

        private string _downloadUrl = null;
        public string DownloadUrl
        {
            get { return _downloadUrl; }
            set { _downloadUrl = value; }
        }

        private string _rightsStatus = null;
        public string RightsStatus
        {
            get { return _rightsStatus; }
            set { _rightsStatus = value; }
        }

        private string _licenseName = null;
        public string LicenseName
        {
            get { return _licenseName; }
            set { _licenseName = value; }
        }

        private string _doi = null;
        public string Doi
        {
            get { return _doi; }
            set { _doi = value; }
        }

        private List<Contributor> _contributors = null;
        public List<Contributor> Contributors
        {
            get { return _contributors; }
            set { _contributors = value; }
        }

        private List<Subject> _subjects = null;
        public List<Subject> Subjects
        {
            get { return _subjects; }
            set { _subjects = value; }
        }

        private List<Part> _relatedParts = null;
        public List<Part> RelatedParts
        {
            get { return _relatedParts; }
            set { _relatedParts = value; }
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
                            _ItemID = Utility.NullIfEmpty(column.Value);
                            break;
                        }
                    case "PrimaryTitleID":
                        {
                            _TitleID = Utility.NullIfEmpty(column.Value);
                            break;
                        }
                    case "SegmentID":
                        {
                            _partID = Utility.NullIfEmpty(column.Value);
                            break;
                        }
                    case "ThumbnailPageID":
                        {
                            _ThumbnailPageID = Utility.NullIfEmpty(column.Value);
                            break;
                        }
                    case "SourceName":
                        {
                            _Source = Utility.NullIfEmpty(column.Value);
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
                    case "LanguageName":
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
                    case "RightsStatement":
                        {
                            _Rights = Utility.NullIfEmpty(column.Value);
                            break;
                        }
                    case "DueDiligence":
                        {
                            _DueDiligence = Utility.NullIfEmpty(column.Value);
                            break;
                        }
                    case "ExternalUrl":
                    case "Url":
                        {
                            _ExternalUrl = Utility.NullIfEmpty(column.Value);
                            break;
                        }
                    case "MaterialTypeLabel":
                        {
                            _MaterialType = Utility.NullIfEmpty(column.Value);
                            break;
                        }
                    case "Datafield_260_a":
                    case "PublisherPlace":
                        {
                            _PublisherPlace = Utility.NullIfEmpty(column.Value);
                            break;
                        }
                    case "Datafield_260_b":
                    case "PublisherName":
                        {
                            _PublisherName = Utility.NullIfEmpty(column.Value);
                            break;
                        }
                    case "Datafield_260_c":
                        {
                            _PublicationDate = Utility.NullIfEmpty(column.Value);
                            break;
                        }
                    case "StartPageID":
                        {
                            _startPageID = Utility.NullIfEmpty(column.Value);
                            break;
                        }
                    case "SequenceOrder":
                        {
                            _sequenceOrder = Utility.NullIfEmpty(column.Value);
                            break;
                        }
                    case "GenreName":
                    case "BibliographicLevelName":
                        {
                            _genre = Utility.NullIfEmpty(column.Value);
                            break;
                        }
                    case "Title":
                    case "FullTitle":
                        {
                            _title = Utility.NullIfEmpty(column.Value);
                            break;
                        }
                    case "TranslatedTitle":
                        {
                            _translatedTitle = Utility.NullIfEmpty(column.Value);
                            break;
                        }
                    case "ContainerTitle":
                        {
                            _containerTitle = Utility.NullIfEmpty(column.Value);
                            break;
                        }
                    case "PublicationDetails":
                        {
                            _publicationDetails = Utility.NullIfEmpty(column.Value);
                            break;
                        }
                    case "Notes":
                        {
                            string noteString = Utility.EmptyIfNull(column.Value);
                            if (noteString != string.Empty)
                            {
                                if (this.Notes == null) this.Notes = new List<TitleNote>();

                                string[] notes = noteString.Split(new[] { "|||" }, StringSplitOptions.RemoveEmptyEntries);
                                foreach (string note in notes)
                                {
                                    if (!string.IsNullOrEmpty(note))
                                    {
                                        TitleNote titleNote = new TitleNote();
                                        titleNote.NoteText = note;
                                        this.Notes.Add(titleNote);
                                    }
                                }
                            }
                            break;
                        }
                    case "Series":
                        {
                            _series = Utility.NullIfEmpty(column.Value);
                            break;
                        }
                    case "Issue":
                        {
                            _issue = Utility.NullIfEmpty(column.Value);
                            break;
                        }
                    case "Date":
                        {
                            _date = Utility.NullIfEmpty(column.Value);
                            break;
                        }
                    case "PageRange":
                        {
                            _pageRange = Utility.NullIfEmpty(column.Value);
                            break;
                        }
                    case "StartPageNumber":
                        {
                            _startPageNumber = Utility.NullIfEmpty(column.Value);
                            break;
                        }
                    case "EndPageNumber":
                        {
                            _endPageNumber = Utility.NullIfEmpty(column.Value);
                            break;
                        }
                    case "DownloadUrl":
                        {
                            _downloadUrl = Utility.NullIfEmpty(column.Value);
                            break;
                        }
                    case "RightsStatus":
                    case "CopyrightStatus":
                        {
                            _rightsStatus = Utility.NullIfEmpty(column.Value);
                            break;
                        }
                    case "LicenseName":
                        {
                            _licenseName = Utility.NullIfEmpty(column.Value);
                            break;
                        }
                    case "DOIName":
                        {
                            _doi = Utility.NullIfEmpty(column.Value);
                            break;
                        }
                    case "Authors":
                        {
                            string authorString = Utility.EmptyIfNull(column.Value);
                            if (authorString != string.Empty)
                            {
                                if (this.Authors == null) this.Authors = new List<Author>();

                                string[] authors = authorString.Split(';');
                                foreach (string author in authors)
                                {
                                    if (!string.IsNullOrEmpty(author))
                                    {
                                        Author creator = new Author();
                                        creator.Name = author;
                                        this.Authors.Add(creator);
                                    }
                                }
                            }
                            break;
                        }
                    case "Keywords":
                        {
                            string keywordString = Utility.EmptyIfNull(column.Value);
                            if (keywordString != string.Empty)
                            {
                                if (this.Subjects == null) this.Subjects = new List<Subject>();

                                string[] keywords = keywordString.Split('|');
                                foreach (string keyword in keywords)
                                {
                                    if (!string.IsNullOrEmpty(keyword))
                                    {
                                        Subject subject = new Subject();
                                        subject.SubjectText = keyword;
                                        this.Subjects.Add(subject);
                                    }
                                }
                            }
                            break;
                        }
                }
            }
        }

        #endregion ISetValues Members
    }
}
