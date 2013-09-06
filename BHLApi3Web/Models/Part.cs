using System.Collections.Generic;
using Newtonsoft.Json;

namespace BHLApi3Web.Models
{
    public class Part : DataObjectBase
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

        [JsonProperty("id")]
        private int _partID;

        [JsonIgnore]
        public int PartID
        {
            get { return _partID; }
            set { _partID = value; }
        }

        [JsonProperty("itemid")]
        private int? _itemID = null;

        [JsonIgnore]
        public int? ItemID
        {
            get { return _itemID; }
            set { _itemID = value; }
        }

        [JsonProperty("startpageid")]
        private int? _startPageID = null;

        [JsonIgnore]
        public int? StartPageID
        {
            get { return _startPageID; }
            set { _startPageID = value; }
        }

        [JsonProperty("sequenceorder")]
        private short _sequenceOrder;

        [JsonIgnore]
        public short SequenceOrder
        {
            get { return _sequenceOrder; }
            set { _sequenceOrder = value; }
        }

        [JsonProperty("contributor")]
        private string _contributor = null;

        [JsonIgnore]
        public string Contributor
        {
            get { return _contributor; }
            set { _contributor = value; }
        }

        [JsonProperty("genre")]
        private string _genreName = null;

        [JsonIgnore]
        public string GenreName
        {
            get { return _genreName; }
            set { _genreName = value; }
        }

        [JsonProperty("title")]
        private string _title = null;

        [JsonIgnore]
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        [JsonProperty("translatedtitle")]
        private string _translatedTitle = null;

        [JsonIgnore]
        public string TranslatedTitle
        {
            get { return _translatedTitle; }
            set { _translatedTitle = value; }
        }

        [JsonProperty("containertitle")]
        private string _containerTitle = null;

        [JsonIgnore]
        public string ContainerTitle
        {
            get { return _containerTitle; }
            set { _containerTitle = value; }
        }

        [JsonProperty("publicationdetails")]
        private string _publicationDetails = null;

        [JsonIgnore]
        public string PublicationDetails
        {
            get { return _publicationDetails; }
            set { _publicationDetails = value; }
        }

        [JsonProperty("publishername")]
        private string _publisherName = null;

        [JsonIgnore]
        public string PublisherName
        {
            get { return _publisherName; }
            set { _publisherName = value; }
        }

        [JsonProperty("publisherplace")]
        private string _publisherPlace = null;

        [JsonIgnore]
        public string PublisherPlace
        {
            get { return _publisherPlace; }
            set { _publisherPlace = value; }
        }

        [JsonProperty("notes")]
        private string _notes = null;

        [JsonIgnore]
        public string Notes
        {
            get { return _notes; }
            set { _notes = value; }
        }

        [JsonProperty("volume")]
        private string _volume = null;

        [JsonIgnore]
        public string Volume
        {
            get { return _volume; }
            set { _volume = value; }
        }

        [JsonProperty("series")]
        private string _series = null;

        [JsonIgnore]
        public string Series
        {
            get { return _series; }
            set { _series = value; }
        }

        [JsonProperty("issue")]
        private string _issue = null;

        [JsonIgnore]
        public string Issue
        {
            get { return _issue; }
            set { _issue = value; }
        }

        [JsonProperty("date")]
        private string _date = null;

        [JsonIgnore]
        public string Date
        {
            get { return _date; }
            set { _date = value; }
        }

        [JsonProperty("pagerange")]
        private string _pageRange = null;

        [JsonIgnore]
        public string PageRange
        {
            get { return _pageRange; }
            set { _pageRange = value; }
        }

        [JsonProperty("startpagenumber")]
        private string _startPageNumber = null;

        [JsonIgnore]
        public string StartPageNumber
        {
            get { return _startPageNumber; }
            set { _startPageNumber = value; }
        }

        [JsonProperty("endpagenumber")]
        private string _endPageNumber = null;

        [JsonIgnore]
        public string EndPageNumber
        {
            get { return _endPageNumber; }
            set { _endPageNumber = value; }
        }

        [JsonProperty("language")]
        private string _language = null;

        [JsonIgnore]
        public string Language
        {
            get { return _language; }
            set { _language = value; }
        }

        [JsonProperty("externalurl")]
        private string _externalUrl = null;

        [JsonIgnore]
        public string ExternalUrl
        {
            get { return _externalUrl; }
            set { _externalUrl = value; }
        }

        [JsonProperty("downloadurl")]
        private string _downloadUrl = null;

        [JsonIgnore]
        public string DownloadUrl
        {
            get { return _downloadUrl; }
            set { _downloadUrl = value; }
        }

        [JsonProperty("rightsstatus")]
        private string _rightsStatus = null;

        [JsonIgnore]
        public string RightsStatus
        {
            get { return _rightsStatus; }
            set { _rightsStatus = value; }
        }

        [JsonProperty("rightsstatement")]
        private string _rightsStatement = null;

        [JsonIgnore]
        public string RightsStatement
        {
            get { return _rightsStatement; }
            set { _rightsStatement = value; }
        }

        [JsonProperty("licensename")]
        private string _licenseName = null;

        [JsonIgnore]
        public string LicenseName
        {
            get { return _licenseName; }
            set { _licenseName = value; }
        }

        [JsonProperty("licenseurl")]
        private string _licenseUrl = null;

        [JsonIgnore]
        public string LicenseUrl
        {
            get { return _licenseUrl; }
            set { _licenseUrl = value; }
        }

        [JsonProperty("doi")]
        private string _doi = null;

        [JsonIgnore]
        public string Doi
        {
            get { return _doi; }
            set { _doi = value; }
        }

        [JsonProperty("authors")]
        private IEnumerable<Author> _authors = null;

        [JsonIgnore]
        public IEnumerable<Author> Authors
        {
            get { return _authors; }
            set { _authors = value; }
        }

        [JsonProperty("subjects")]
        private IEnumerable<Subject> _subjects = null;

        [JsonIgnore]
        public IEnumerable<Subject> Subjects
        {
            get { return _subjects; }
            set { _subjects = value; }
        }

        [JsonProperty("identifiers")]
        private IEnumerable<Identifier> _identifiers = null;

        [JsonIgnore]
        public IEnumerable<Identifier> Identifiers
        {
            get { return _identifiers; }
            set { _identifiers = value; }
        }

        [JsonProperty("pages")]
        private IEnumerable<Page> _pages = null;

        [JsonIgnore]
        public IEnumerable<Page> Pages
        {
            get { return _pages; }
            set { _pages = value; }
        }

        [JsonProperty("relatedparts")]
        private IEnumerable<Part> _relatedParts = null;

        [JsonIgnore]
        public IEnumerable<Part> RelatedParts
        {
            get { return _relatedParts; }
            set { _relatedParts = value; }
        }

        #endregion Properties

    }
}
