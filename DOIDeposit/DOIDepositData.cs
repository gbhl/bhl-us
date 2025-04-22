using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MOBOT.BHL.DOIDeposit
{
    public class DOIDepositData
    {
        public DOIDepositData()
        {
            _timestamp = DateTime.Now.ToString("yyyyMMddhhmmssffff");
        }

        private bool _isUpdate = false;

        public bool IsUpdate
        {
            get { return _isUpdate; }
            set { _isUpdate = value; }
        }

        #region Deposit Head properties

        private int _entityID;
        public int EntityID
        {
            get { return _entityID; }
            set { _entityID = value; }
        }

        private int _entityTypeID;
        public int EntityTypeID
        {
            get { return _entityTypeID; }
            set { _entityTypeID = value; }
        }

        private string _batchID = string.Empty;
        public string BatchID
        {
            get { return _batchID; }
            set { _batchID = value; }
        }

        private string _timestamp;
        public string Timestamp
        {
            get { return _timestamp; }
            set { _timestamp = value; }
        }

        private string _depositorName = string.Empty;
        public string DepositorName
        {
            get { return _depositorName; }
            set { _depositorName = value; }
        }

        private string _depositorEmail = string.Empty;
        public string DepositorEmail
        {
            get { return _depositorEmail; }
            set { _depositorEmail = value; }
        }

        private string _registrant = string.Empty;
        public string Registrant
        {
            get { return _registrant; }
            set { _registrant = value; }
        }

        #endregion Deposit Head properties

        #region Deposit Body properties

        private PublicationTypeValue _publicationType;
        public PublicationTypeValue PublicationType
        {
            get { return _publicationType; }
            set { _publicationType = value; }
        }

        // Optional; use ISO 639 language codes
        private string _language = string.Empty;
        public string Language
        {
            get { return _language; }
            set { _language = value; }
        }

        private string _title = string.Empty;
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        private string _titleDOIName = string.Empty;
        public string TitleDOIName
        {
            get { return _titleDOIName; }
            set { _titleDOIName = value; }
        }

        private string _titleDOIResource = string.Empty;
        public string TitleDOIResource
        {
            get { return _titleDOIResource; }
            set { _titleDOIResource = value; }
        }

        private string _publicationDate = string.Empty;
        public string PublicationDate
        {
            get { return _publicationDate; }
            set { _publicationDate = value; }
        }

        // Required for monographs only
        private string _isbn = string.Empty;
        public string Isbn
        {
            get { return _isbn; }
            set { _isbn = value; }
        }

        // Required for journals only.  Tuple 
        private List<(string MediaType, string Value)> _issn = new List<(string MediaType, string Value)>();
        public List<(string MediaType, string Value)> Issn
        {
            get { return _issn; }
            set { _issn = value; }
        }

        private string _publisherName = string.Empty;
        public string PublisherName
        {
            get { return _publisherName; }
            set { _publisherName = value; }
        }

        // Optional
        private string _publisherPlace = string.Empty;
        public string PublisherPlace
        {
            get { return _publisherPlace; }
            set { _publisherPlace = value; }
        }

        // The DOI assigned to the entity
        private string _doiName = string.Empty;
        public string DoiName
        {
            get { return _doiName; }
            set { _doiName = value; }
        }

        // The BHL URL to associate with the DOI
        private string _doiResource = string.Empty;
        public string DoiResource
        {
            get { return _doiResource; }
            set { _doiResource = value; }
        }

        // Optional
        private List<Contributor> _contributors = new List<Contributor>();
        public List<Contributor> Contributors
        {
            get { return _contributors; }
            set { _contributors = value; }
        }

        // Optional.  Should contain only a number; no additional text.
        private string _edition = string.Empty;
        public string Edition
        {
            get { return _edition; }
            set { _edition = value; }
        }

        // Optional
        private string _volume = string.Empty;
        public string Volume
        {
            get { return _volume; }
            set { _volume = value; }
        }

        // Optional
        private string _issue = string.Empty;
        public string Issue
        {
            get { return _issue; }
            set { _issue = value; }
        }

        // Optional
        private string _coden = string.Empty;
        public string Coden
        {
            get { return _coden; }
            set { _coden = value; }
        }

        // Optional
        private string _abbreviatedTitle = string.Empty;
        public string AbbreviatedTitle
        {
            get { return _abbreviatedTitle; }
            set { _abbreviatedTitle = value; }
        }

        // Required for articles
        private string _articleTitle = string.Empty;
        public string ArticleTitle
        {
            get { return _articleTitle; }
            set { _articleTitle = value; }
        }

        // Optional
        private string _articlePublicationDate = string.Empty;
        public string ArticlePublicationDate
        {
            get { return _articlePublicationDate; }
            set { _articlePublicationDate = value; }
        }

        // Optional
        private string _firstPage = string.Empty;
        public string FirstPage
        {
            get { return _firstPage; }
            set { _firstPage = value; }
        }

        // Optional
        private string _lastPage = string.Empty;
        public string LastPage
        {
            get { return _lastPage; }
            set { _lastPage = value; }
        }

        // Only applies to monographic series
        private string _seriesTitle = string.Empty;
        public string SeriesTitle
        {
            get { return _seriesTitle; }
            set { _seriesTitle = value; }
        }

        // Only applies to monographic series
        private string _seriesISSN = string.Empty;
        public string SeriesISSN
        {
            get { return _seriesISSN; }
            set { _seriesISSN = value; }
        }

        // Only applies to monographic series. Optional.
        private string _seriesVolume = string.Empty;
        public string SeriesVolume
        {
            get { return _seriesVolume; }
            set { _seriesVolume = value; }
        }

        // Some deposits (example: chapters) require full metadata for both the entity being deposited and the parent/container entity
        private DOIDepositData _publicationContainerData = null;
        public DOIDepositData PublicationContainerData
        {
            get { return _publicationContainerData; }
            set { _publicationContainerData = value; }
        }

        #endregion Deposit Body properties

        public enum PublicationTypeValue
        {
            EditedBook,
            Monograph,
            MonographicSeries,
            Reference,
            Journal,
            Article,
            Other,
            Chapter
        }

        public enum PersonNameSequence
        {
            First,
            Additional
        }

        public enum ContributorRole
        {
            Author,
            Editor,
            Chair,
            Translator
        }

        public class Contributor
        {
            public string PersonName;
            public string OrganizationName;
            public string Suffix;
            public PersonNameSequence Sequence;
            public ContributorRole Role;
            public string ORCID;
        }
    }
}
