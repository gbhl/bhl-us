using System;

namespace MOBOT.BHL.DataObjects
{
    [Serializable]
    public class OpenUrlCitation
    {
        private int _pageID;

        public int PageID
        {
            get { return _pageID; }
            set { _pageID = value; }
        }

        private int _itemID;

        public int ItemID
        {
            get { return _itemID; }
            set { _itemID = value; }
        }

        private int _titleID;

        public int TitleID
        {
            get { return _titleID; }
            set { _titleID = value; }
        }

        private int _partID;

        public int PartID
        {
            get { return _partID; }
            set { _partID = value; }
        }

        private string _fullTitle;

        public string FullTitle
        {
            get { return _fullTitle; }
            set { _fullTitle = value; }
        }

        private string _articleTitle;

        public string ArticleTitle
        {
            get { return _articleTitle; }
            set { _articleTitle = value; }
        }

        private string _journalTitle;

        public string JournalTitle
        {
            get { return _journalTitle; }
            set { _journalTitle = value; }
        }

        private string _publisherPlace;

        public string PublisherPlace
        {
            get { return _publisherPlace; }
            set { _publisherPlace = value; }
        }

        private string _publisherName;

        public string PublisherName
        {
            get { return _publisherName; }
            set { _publisherName = value; }
        }

        private string _date;

        public string Date
        {
            get { return _date; }
            set { _date = value; }
        }

        private string _languageName;

        public string LanguageName
        {
            get { return _languageName; }
            set { _languageName = value; }
        }

        private string _volume;

        public string Volume
        {
            get { return _volume; }
            set { _volume = value; }
        }

        private string _editionStatement;

        public string EditionStatement
        {
            get { return _editionStatement; }
            set { _editionStatement = value; }
        }

        private string _currentPublicationFrequency;

        public string CurrentPublicationFrequency
        {
            get { return _currentPublicationFrequency; }
            set { _currentPublicationFrequency = value; }
        }

        private string _genre;

        public string Genre
        {
            get { return _genre; }
            set { _genre = value; }
        }

        private string _authors;

        public string Authors
        {
            get { return _authors; }
            set { _authors = value; }
        }

        private string _subjects;

        public string Subjects
        {
            get { return _subjects; }
            set { _subjects = value; }
        }

        private string _startPage;

        public string StartPage
        {
            get { return _startPage; }
            set { _startPage = value; }
        }

        private string _endPage;

        public string EndPage
        {
            get { return _endPage; }
            set { _endPage = value; }
        }

        private string _pages;

        public string Pages
        {
            get { return _pages; }
            set { _pages = value; }
        }

        private string _issn;

        public string Issn
        {
            get { return _issn; }
            set { _issn = value; }
        }

        private string _isbn;

        public string Isbn
        {
            get { return _isbn; }
            set { _isbn = value; }
        }

        private string _lccn;

        public string Lccn
        {
            get { return _lccn; }
            set { _lccn = value; }
        }

        private string _oclc;

        public string Oclc
        {
            get { return _oclc; }
            set { _oclc = value; }
        }

        private string _abbreviation;

        public string Abbreviation
        {
            get { return _abbreviation; }
            set { _abbreviation = value; }
        }
    }
}
