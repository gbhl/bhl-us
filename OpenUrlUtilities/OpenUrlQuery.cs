using System;
using System.Collections.Generic;
using System.Text;

namespace MOBOT.OpenUrl.Utilities
{
    abstract public class OpenUrlQuery : IOpenUrlQuery
    {
        #region IOpenUrlQuery Methods

        abstract public void SetQuery(string queryString);
        abstract public bool ValidateQuery();

        #endregion

        #region IOpenUrlQuery Attributes

        private string _version = string.Empty;
        public string Version
        {
            get { return _version; }
            set { _version = value.Trim(); }
        }

        private string _validationError = string.Empty;
        public string ValidationError
        {
            get { return _validationError; }
            set { _validationError = value.Trim(); }
        }

        private string _format = string.Empty;
        public string Format
        {
            get { return _format; }
            set { _format = value.Trim(); }
        }

        private string _genre = string.Empty;
        public string Genre
        {
            get { return _genre; }
            set { _genre = value.Trim(); }
        }

        private string _privateData = string.Empty;
        public string PrivateData
        {
            get { return _privateData; }
            set { _privateData = value.Trim(); }
        }

        private string _authorLast = string.Empty;
        public string AuthorLast
        {
            get { return _authorLast; }
            set { _authorLast = value.Trim(); }
        }

        private string _authorFirst = string.Empty;
        public string AuthorFirst
        {
            get { return _authorFirst; }
            set { _authorFirst = value.Trim(); }
        }

        private string _authorInitial = string.Empty;
        public string AuthorInitial
        {
            get { return _authorInitial; }
            set { _authorInitial = value.Trim(); }
        }

        private string _authorInitial1 = string.Empty;
        public string AuthorInitial1
        {
            get { return _authorInitial1; }
            set { _authorInitial1 = value.Trim(); }
        }

        private string _authorInitialMiddle = string.Empty;
        public string AuthorInitialMiddle
        {
            get { return _authorInitialMiddle; }
            set { _authorInitialMiddle = value.Trim(); }
        }

        private string _authorSuffix = string.Empty;
        public string AuthorSuffix
        {
            get { return _authorSuffix; }
            set { _authorSuffix = value.Trim(); }
        }

        private string _authorCorporation = string.Empty;
        public string AuthorCorporation
        {
            get { return _authorCorporation; }
            set { _authorCorporation = value.Trim(); }
        }

        private string _issn = string.Empty;
        public string Issn
        {
            get { return _issn; }
            set { _issn = value.Trim(); }
        }

        private string _eissn = string.Empty;
        public string Eissn
        {
            get { return _eissn; }
            set { _eissn = value.Trim(); }
        }

        private string _coden = string.Empty;
        public string Coden
        {
            get { return _coden; }
            set { _coden = value.Trim(); }
        }

        private string _isbn = string.Empty;
        public string Isbn
        {
            get { return _isbn; }
            set { _isbn = value.Trim(); }
        }

        /*
        private string _lccn = string.Empty;
        public string Lccn
        {
            get { return _lccn; }
            set { _lccn = value.Trim(); }
        }

        private string _oclc = string.Empty;
        public string Oclc
        {
            get { return _oclc; }
            set { _oclc = value.Trim(); }
        }
        */

        private NonUniqueHashtable _identifiers = new NonUniqueHashtable();
        public NonUniqueHashtable Identifiers
        {
            get { return _identifiers; }
            set { _identifiers = value; }
        }

        private string _publisher = string.Empty;
        public string Publisher
        {
            get { return _publisher; }
            set { _publisher = value; }
        }

        private string _publisherName = string.Empty;
        public string PublisherName
        {
            get { return _publisherName; }
            set { _publisherName = value.Trim(); }
        }

        private string _publisherPlace = string.Empty;
        public string PublisherPlace
        {
            get { return _publisherPlace; }
            set { _publisherPlace = value.Trim(); }
        }

        private string _bookTitle = string.Empty;
        public string BookTitle
        {
            get { return _bookTitle; }
            set { _bookTitle = value.Trim(); }
        }

        private string _journalTitle = string.Empty;
        public string JournalTitle
        {
            get { return _journalTitle; }
            set { _journalTitle = value.Trim(); }
        }

        private string _articleTitle = string.Empty;
        public string ArticleTitle
        {
            get { return _articleTitle; }
            set { _articleTitle = value.Trim(); }
        }

        private string _shortTitle = string.Empty;
        public string ShortTitle
        {
            get { return _shortTitle; }
            set { _shortTitle = value.Trim(); }
        }

        private string _volume = string.Empty;
        public string Volume
        {
            get { return _volume; }
            set { _volume = value.Trim(); }
        }

        private string _issue = string.Empty;
        public string Issue
        {
            get { return _issue; }
            set { _issue = value.Trim(); }
        }

        private string _part = string.Empty;
        public string Part
        {
            get { return _part; }
            set { _part = value.Trim(); }
        }

        private string _startPage = string.Empty;
        public string StartPage
        {
            get { return _startPage; }
            set { _startPage = value.Trim(); }
        }

        private string _endPage = string.Empty;
        public string EndPage
        {
            get { return _endPage; }
            set { _endPage = value.Trim(); }
        }

        private string _pages = string.Empty;
        public string Pages
        {
            get { return _pages; }
            set { _pages = value.Trim(); }
        }

        private string _articleNumber = string.Empty;
        public string ArticleNumber
        {
            get { return _articleNumber; }
            set { _articleNumber = value.Trim(); }
        }

        private string _season = string.Empty;
        public string Season
        {
            get { return _season; }
            set { _season = value.Trim(); }
        }

        private string _quarter = string.Empty;
        public string Quarter
        {
            get { return _quarter; }
            set { _quarter = value.Trim(); }
        }

        private OpenUrlQueryDate _date = new OpenUrlQueryDate();
        public OpenUrlQueryDate Date
        {
            get { return _date; }
            set { _date = value; }
        }

        #endregion
    }
}
