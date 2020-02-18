namespace MOBOT.BHL.DataObjects
{
    public class NameSearchPage
	{
        private int _titleID = 0;
        private int _itemID = 0;
		private int _pageID = 0;
        private string _bibliographicLevelLabel = string.Empty;
        private string _fullTitle = string.Empty;
        private string _shortTitle = string.Empty;
        private string _partNumber = string.Empty;
        private string _partName = string.Empty;
        private string _publisherPlace = string.Empty;
        private string _publisher = string.Empty;
        private string _authors = string.Empty;
        private string _volume = string.Empty;
        private string _date = string.Empty;
        private string _callNumber = string.Empty;
        private string _languageName = string.Empty;
		private int _sequenceOrder = 0;
		private string _indicatedPages = "";
        private int _totalPages = 0;
		private int _nameBankId;

        public int TitleID
        {
            get { return _titleID; }
            set { _titleID = value; }
        }

        public int ItemID
        {
            get { return _itemID; }
            set { _itemID = value; }
        }

        public int PageID
		{
			get { return _pageID; }
			set { _pageID = value; }
		}

        public string BibliographicLevelLabel
        {
            get { return _bibliographicLevelLabel; }
            set { _bibliographicLevelLabel = value; }
        }

        public string FullTitle
        {
            get { return _fullTitle; }
            set { _fullTitle = value; }
        }

        public string ShortTitle
        {
            get { return _shortTitle; }
            set { _shortTitle = value; }
        }

        public string PartNumber
        {
            get { return _partNumber; }
            set { _partNumber = value; }
        }

        public string PartName
        {
            get { return _partName; }
            set { _partName = value; }
        }

        public string PublisherPlace
        {
            get { return _publisherPlace; }
            set { _publisherPlace = value; }
        }

        public string Publisher
        {
            get { return _publisher; }
            set { _publisher = value; }
        }
        
        public string Authors
        {
            get { return _authors; }
            set { _authors = value; }
        }

        public string Volume
        {
            get { return _volume; }
            set { _volume = value; }
        }

        public string Date
        {
            get { return _date; }
            set { _date = value; }
        }

        public int SequenceOrder
		{
			get { return _sequenceOrder; }
			set { _sequenceOrder = value; }
		}

		public string IndicatedPages
		{
			get { return _indicatedPages; }
			set { _indicatedPages = value; }
		}

        public string CallNumber
        {
            get { return _callNumber; }
            set { _callNumber = value; }
        }

        public string LanguageName
        {
            get { return _languageName; }
            set { _languageName = value; }
        }

        public int TotalPages
        {
            get { return _totalPages; }
            set { _totalPages = value; }
        }

        public int NameBankId
		{
			get { return this._nameBankId; }
			set { this._nameBankId = value; }
		}
	}
}
