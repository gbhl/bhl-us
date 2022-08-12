using System.Collections.Generic;

namespace BHL.Search
{
    public class ItemHit : Hit
    {
        private int _titleId = 0;
        private int _itemId = 0;
        private int _segmentId = 0;
        private int _startPageId = 0;
        private bool _bookIsVirtual = false;
        private string _title = string.Empty;
        private string _translatedTitle = string.Empty;
        private string _uniformTitle = string.Empty;
        private string _sortTitle = string.Empty;
        private string _genre = string.Empty;
        private string _materialType = string.Empty;
        private List<string> _authors = new List<string>();
        private List<string> _searchAuthors = new List<string>();
        private List<string> _keywords = new List<string>();
        private List<string> _associations = new List<string>();
        private List<string> _variants = new List<string>();
        private List<string> _contributors = new List<string>();
        private List<string> _notes = new List<string>();
        private string _volume = string.Empty;
        private string _issue = string.Empty;
        private string _series = string.Empty;
        private string _publisher = string.Empty;
        private string _publicationPlace = string.Empty;
        private string _language = string.Empty;
        private List<string> _dates = new List<string>();
        private List<string> _dateRanges = new List<string>();
        private List<string> _oclc = new List<string>();
        private List<string> _issn = new List<string>();
        private List<string> _isbn = new List<string>();
        private string _doi = string.Empty;
        private string _url = string.Empty;
        private List<string> _collections = new List<string>();
        private string _container = string.Empty;
        private string _pageRange = string.Empty;
        private string _text = string.Empty;
        private bool _hasSegments = false;
        private bool _hasLocalContent = false;
        private bool _hasExternalContent = false;

        public int TitleId
        {
            get { return _titleId; }
            set { _titleId = value; }
        }

        public int ItemId
        {
            get { return _itemId; }
            set { _itemId = value; }
        }

        public int SegmentId
        {
            get { return _segmentId; }
            set { _segmentId = value; }
        }

        public int StartPageId
        {
            get { return _startPageId; }
            set { _startPageId = value; }
        }

        public bool BookIsVirtual
        {
            get { return _bookIsVirtual; }
            set { _bookIsVirtual = value; }
        }

        public string Title
        {
            get { return _title; }
            set { _title = value ?? string.Empty; }
        }

        public string TranslatedTitle
        {
            get { return _translatedTitle; }
            set { _translatedTitle = value ?? string.Empty;}
        }

        public string UniformTitle
        {
            get { return _uniformTitle; }
            set { _uniformTitle = value ?? string.Empty; }
        }

        public string SortTitle
        {
            get { return _sortTitle; }
            set { _sortTitle = value ?? string.Empty; }
        }

        public string Genre
        {
            get { return _genre; }
            set { _genre = value ?? string.Empty; }
        }

        public string MaterialType
        {
            get { return _materialType; }
            set { _materialType = value ?? string.Empty; }
        }

        public List<string> Authors
        {
            get { return _authors; }
            set { _authors = value ?? new List<string>(); }
        }

        public List<string> SearchAuthors
        {
            get { return _searchAuthors; }
            set { _searchAuthors = value ?? new List<string>(); }
        }

        public List<string> Keywords
        {
            get { return _keywords; }
            set { _keywords = value ?? new List<string>(); }
        }

        public List<string> Associations
        {
            get { return _associations; }
            set { _associations = value ?? new List<string>(); }
        }

        public List<string> Variants
        {
            get { return _variants; }
            set { _variants = value ?? new List<string>(); }
        }

        public List<string> Contributors
        {
            get { return _contributors; }
            set { _contributors = value ?? new List<string>(); }
        }

        public List<string> Notes
        {
            get { return _notes; }
            set { _notes = value ?? new List<string>(); }
        }

        public string Volume
        {
            get { return _volume; }
            set { _volume = value ?? string.Empty; }
        }

        public string Issue
        {
            get { return _issue; }
            set { _issue = value ?? string.Empty; }
        }

        public string Series
        {
            get { return _series; }
            set { _series = value ?? string.Empty; }
        }

        public string Publisher
        {
            get { return _publisher; }
            set { _publisher = value ?? string.Empty; }
        }

        public string PublicationPlace
        {
            get { return _publicationPlace; }
            set { _publicationPlace = value ?? string.Empty; }
        }

        public string Language
        {
            get { return _language; }
            set { _language = value ?? string.Empty; }
        }

        public List<string> Dates
        {
            get { return _dates; }
            set { _dates = value ?? new List<string>(); }
        }

        public List<string> DateRanges
        {
            get { return _dateRanges; }
            set { _dateRanges = value ?? new List<string>(); }
        }

        public List<string> Oclc
        {
            get { return _oclc; }
            set { _oclc = value ?? new List<string>(); }
        }

        public List<string> Issn
        {
            get { return _issn; }
            set { _issn = value ?? new List<string>(); }
        }

        public List<string> Isbn
        {
            get { return _isbn; }
            set { _isbn = value ?? new List<string>(); }
        }

        public string Doi
        {
            get { return _doi; }
            set { _doi = value ?? string.Empty; }
        }

        public string Url
        {
            get { return _url; }
            set { _url = value ?? string.Empty; }
        }

        public List<string> Collections
        {
            get { return _collections; }
            set { _collections = value ?? new List<string>(); }
        }

        public string Container
        {
            get { return _container; }
            set { _container = value ?? string.Empty;}
        }

        public string PageRange
        {
            get { return _pageRange; }
            set { _pageRange = value; }
        }

        public string Text
        {
            get { return _text; }
            set { _text = value ?? string.Empty; }
        }

        public bool HasSegments
        {
            get { return _hasSegments; }
            set { _hasSegments = value; }
        }

        public bool HasLocalContent
        {
            get { return _hasLocalContent; }
            set { _hasLocalContent = value; }
        }

        public bool HasExternalContent
        {
            get { return _hasExternalContent; }
            set { _hasExternalContent = value; }
        }
    }
}
