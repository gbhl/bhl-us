using System;
using CustomDataAccess;

namespace MOBOT.BHL.API.BHLApiDataObjects2
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

        private int? _itemID = null;

        public int? ItemID
        {
            get { return _itemID; }
            set { _itemID = value; }
        }

        private int? _startPageID = null;

        public int? StartPageID
        {
            get { return _startPageID; }
            set { _startPageID = value; }
        }

        private short _sequenceOrder = default(int);

        public short SequenceOrder
        {
            get { return _sequenceOrder; }
            set { _sequenceOrder = value; }
        }

        private string _contributor = string.Empty;

        public string Contributor
        {
            get { return _contributor; }
            set { _contributor = value; }
        }

        private string _contributorID = string.Empty;

        public string ContributorID
        {
            get { return _contributorID; }
            set { _contributorID = value; }
        }

        private string _genreName = string.Empty;

        public string GenreName
        {
            get { return _genreName; }
            set { _genreName = value; }
        }

        private string _title = string.Empty;

        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        private string _translatedTitle = string.Empty;

        public string TranslatedTitle
        {
            get { return _translatedTitle; }
            set { _translatedTitle = value; }
        }

        private string _containerTitle = string.Empty;

        public string ContainerTitle
        {
            get { return _containerTitle; }
            set { _containerTitle = value; }
        }

        private string _publicationDetails = string.Empty;

        public string PublicationDetails
        {
            get { return _publicationDetails; }
            set { _publicationDetails = value; }
        }

        private string _publisherName = string.Empty;

        public string PublisherName
        {
            get { return _publisherName; }
            set { _publisherName = value; }
        }

        private string _publisherPlace = string.Empty;

        public string PublisherPlace
        {
            get { return _publisherPlace; }
            set { _publisherPlace = value; }
        }

        private string _notes = string.Empty;

        public string Notes
        {
            get { return _notes; }
            set { _notes = value; }
        }

        private string _volume = string.Empty;

        public string Volume
        {
            get { return _volume; }
            set { _volume = value; }
        }

        private string _series = string.Empty;

        public string Series
        {
            get { return _series; }
            set { _series = value; }
        }

        private string _issue = string.Empty;

        public string Issue
        {
            get { return _issue; }
            set { _issue = value; }
        }

        private string _date = string.Empty;

        public string Date
        {
            get { return _date; }
            set { _date = value; }
        }

        private string _pageRange = string.Empty;

        public string PageRange
        {
            get { return _pageRange; }
            set { _pageRange = value; }
        }

        private string _startPageNumber = string.Empty;

        public string StartPageNumber
        {
            get { return _startPageNumber; }
            set { _startPageNumber = value; }
        }

        private string _endPageNumber = string.Empty;

        public string EndPageNumber
        {
            get { return _endPageNumber; }
            set { _endPageNumber = value; }
        }

        private string _language = string.Empty;

        public string Language
        {
            get { return _language; }
            set { _language = value; }
        }

        private string _externalUrl = string.Empty;

        public string ExternalUrl
        {
            get { return _externalUrl; }
            set { _externalUrl = value; }
        }

        private string _downloadUrl = string.Empty;

        public string DownloadUrl
        {
            get { return _downloadUrl; }
            set { _downloadUrl = value; }
        }

        private string _rightsStatus = string.Empty;

        public string RightsStatus
        {
            get { return _rightsStatus; }
            set { _rightsStatus = value; }
        }

        private string _rightsStatement = string.Empty;

        public string RightsStatement
        {
            get { return _rightsStatement; }
            set { _rightsStatement = value; }
        }

        private string _licenseName = string.Empty;

        public string LicenseName
        {
            get { return _licenseName; }
            set { _licenseName = value; }
        }

        private string _licenseUrl = string.Empty;

        public string LicenseUrl
        {
            get { return _licenseUrl; }
            set { _licenseUrl = value; }
        }

        private string _doi = string.Empty;

        public string Doi
        {
            get { return _doi; }
            set { _doi = value; }
        }

        private CustomGenericList<Creator> _authors = null;

        public CustomGenericList<Creator> Authors
        {
            get { return _authors; }
            set { _authors = value; }
        }

        private CustomGenericList<Subject> _subjects = null;

        public CustomGenericList<Subject> Subjects
        {
            get { return _subjects; }
            set { _subjects = value; }
        }

        private CustomGenericList<PartIdentifier> _identifiers = null;

        public CustomGenericList<PartIdentifier> Identifiers
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
                            _itemID = Utility.ZeroIfNull(column.Value);
                            break;
                        }
                    case "StartPageID":
                        {
                            _startPageID = Utility.ZeroIfNull(column.Value);
                            break;
                        }
                    case "SequenceOrder":
                        {
                            _sequenceOrder = (short)column.Value;
                            break;
                        }
                    case "ContributorName":
                        {
                            _contributor = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "ContributorSegmentID":
                        {
                            _contributorID = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "GenreName":
                        {
                            _genreName = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "Title":
                        {
                            _title = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "TranslatedTitle":
                        {
                            _translatedTitle = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "ContainerTitle":
                        {
                            _containerTitle = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "PublicationDetails":
                        {
                            _publicationDetails = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "PublisherName":
                        {
                            _publisherName = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "PublisherPlace":
                        {
                            _publisherPlace = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "Notes":
                        {
                            _notes = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "Volume":
                        {
                            _volume = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "Series":
                        {
                            _series = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "Issue":
                        {
                            _issue = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "Date":
                        {
                            _date = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "PageRange":
                        {
                            _pageRange = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "StartPageNumber":
                        {
                            _startPageNumber = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "EndPageNumber":
                        {
                            _endPageNumber = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "LanguageName":
                        {
                            _language = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "Url":
                        {
                            _externalUrl = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "DownloadUrl":
                        {
                            _downloadUrl = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "RightsStatus":
                        {
                            _rightsStatus = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "RightsStatement":
                        {
                            _rightsStatement = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "LicenseName":
                        {
                            _licenseName = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "LicenseUrl":
                        {
                            _licenseUrl = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "DOIName":
                        {
                            _doi = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "Authors":
                        {
                            string authorString = Utility.EmptyIfNull(column.Value);
                            if (authorString != string.Empty)
                            {
                                if (this.Authors == null) this.Authors = new CustomGenericList<Creator>();

                                string[] authors = authorString.Split(';');
                                foreach (string author in authors)
                                {
                                    if (!string.IsNullOrEmpty(author))
                                    {
                                        Creator creator = new Creator();
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
