using System;
using CustomDataAccess;

namespace MOBOT.BHL.API.BHLApiDataObjects3
{
    [Serializable]
    public class Part : DataObjectBase, ISetValues
    {
        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Part()
        {
        }

        #endregion Constructors

        #region Properties

        private string _partUrl = string.Empty;
        public string PartUrl
        {
            get { return _partUrl; }
            set { _partUrl = value; }
        }

        private int _partID = default(int);
        public int PartID
        {
            get { return _partID; }
            set { _partID = value; }
        }

        private string _itemID = null;
        public string ItemID
        {
            get { return _itemID; }
            set { _itemID = value; }
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

        private string _genreName = null;
        public string GenreName
        {
            get { return _genreName; }
            set { _genreName = value; }
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

        private string _publisherName = null;
        public string PublisherName
        {
            get { return _publisherName; }
            set { _publisherName = value; }
        }

        private string _publisherPlace = null;
        public string PublisherPlace
        {
            get { return _publisherPlace; }
            set { _publisherPlace = value; }
        }

        private string _notes = null;
        public string Notes
        {
            get { return _notes; }
            set { _notes = value; }
        }

        private string _volume = null;
        public string Volume
        {
            get { return _volume; }
            set { _volume = value; }
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

        private string _language = null;
        public string Language
        {
            get { return _language; }
            set { _language = value; }
        }

        private string _externalUrl = null;
        public string ExternalUrl
        {
            get { return _externalUrl; }
            set { _externalUrl = value; }
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

        private string _rightsStatement = null;
        public string RightsStatement
        {
            get { return _rightsStatement; }
            set { _rightsStatement = value; }
        }

        private string _licenseName = null;
        public string LicenseName
        {
            get { return _licenseName; }
            set { _licenseName = value; }
        }

        private string _licenseUrl = null;
        public string LicenseUrl
        {
            get { return _licenseUrl; }
            set { _licenseUrl = value; }
        }

        private string _doi = null;
        public string Doi
        {
            get { return _doi; }
            set { _doi = value; }
        }

        private CustomGenericList<Author> _authors = null;
        public CustomGenericList<Author> Authors
        {
            get { return _authors; }
            set { _authors = value; }
        }

        private CustomGenericList<Contributor> _contributors = null;
        public CustomGenericList<Contributor> Contributors
        {
            get { return _contributors; }
            set { _contributors = value; }
        }

        private CustomGenericList<Subject> _subjects = null;
        public CustomGenericList<Subject> Subjects
        {
            get { return _subjects; }
            set { _subjects = value; }
        }

        private CustomGenericList<Identifier> _identifiers = null;
        public CustomGenericList<Identifier> Identifiers
        {
            get { return _identifiers; }
            set { _identifiers = value; }
        }

        private CustomGenericList<Page> _pages = null;
        public CustomGenericList<Page> Pages
        {
            get { return _pages; }
            set { _pages = value; }
        }

        private CustomGenericList<Part> _relatedParts = null;
        public CustomGenericList<Part> RelatedParts
        {
            get { return _relatedParts; }
            set { _relatedParts = value; }
        }

        #endregion Properties

        #region ISetValues Members

        public void SetValues(CustomDataRow row)
        {
            foreach (CustomDataColumn column in row)
            {
                switch (column.Name)
                {
                    case "SegmentID":
                        {
                            _partID = Utility.ZeroIfNull(column.Value);
                            break;
                        }
                    case "ItemID":
                        {
                            _itemID = (string)column.Value;
                            break;
                        }
                    case "StartPageID":
                        {
                            _startPageID = (string)column.Value;
                            break;
                        }
                    case "SequenceOrder":
                        {
                            _sequenceOrder = (string)column.Value;
                            break;
                        }
                    case "GenreName":
                        {
                            _genreName = (string)column.Value;
                            break;
                        }
                    case "Title":
                        {
                            _title = (string)column.Value;
                            break;
                        }
                    case "TranslatedTitle":
                        {
                            _translatedTitle = (string)column.Value;
                            break;
                        }
                    case "ContainerTitle":
                        {
                            _containerTitle = (string)column.Value;
                            break;
                        }
                    case "PublicationDetails":
                        {
                            _publicationDetails = (string)column.Value;
                            break;
                        }
                    case "PublisherName":
                        {
                            _publisherName = (string)column.Value;
                            break;
                        }
                    case "PublisherPlace":
                        {
                            _publisherPlace = (string)column.Value;
                            break;
                        }
                    case "Notes":
                        {
                            _notes = (string)column.Value;
                            break;
                        }
                    case "Volume":
                        {
                            _volume = (string)column.Value;
                            break;
                        }
                    case "Series":
                        {
                            _series = (string)column.Value;
                            break;
                        }
                    case "Issue":
                        {
                            _issue = (string)column.Value;
                            break;
                        }
                    case "Date":
                        {
                            _date = (string)column.Value;
                            break;
                        }
                    case "PageRange":
                        {
                            _pageRange = (string)column.Value;
                            break;
                        }
                    case "StartPageNumber":
                        {
                            _startPageNumber = (string)column.Value;
                            break;
                        }
                    case "EndPageNumber":
                        {
                            _endPageNumber = (string)column.Value;
                            break;
                        }
                    case "LanguageName":
                        {
                            _language = (string)column.Value;
                            break;
                        }
                    case "Url":
                        {
                            _externalUrl = (string)column.Value;
                            break;
                        }
                    case "DownloadUrl":
                        {
                            _downloadUrl = (string)column.Value;
                            break;
                        }
                    case "RightsStatus":
                        {
                            _rightsStatus = (string)column.Value;
                            break;
                        }
                    case "RightsStatement":
                        {
                            _rightsStatement = (string)column.Value;
                            break;
                        }
                    case "LicenseName":
                        {
                            _licenseName = (string)column.Value;
                            break;
                        }
                    case "LicenseUrl":
                        {
                            _licenseUrl = (string)column.Value;
                            break;
                        }
                    case "DOIName":
                        {
                            _doi = (string)column.Value;
                            break;
                        }
                    case "Authors":
                        {
                            string authorString = Utility.EmptyIfNull(column.Value);
                            if (authorString != string.Empty)
                            {
                                if (this.Authors == null) this.Authors = new CustomGenericList<Author>();

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
                                if (this.Subjects == null) this.Subjects = new CustomGenericList<Subject>();

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
