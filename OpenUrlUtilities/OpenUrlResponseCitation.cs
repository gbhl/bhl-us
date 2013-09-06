using System;
using System.Collections.Generic;
using System.Text;

namespace MOBOT.OpenUrl.Utilities
{
    [Serializable]
    public class OpenUrlResponseCitation
    {
        private String _url = String.Empty;

        public String Url
        {
            get { return _url; }
            set { _url = value; }
        }

        private String _partUrl = String.Empty;

        public String PartUrl
        {
            get { return _partUrl; }
            set { _partUrl = value; }
        }

        private String _itemUrl = String.Empty;

        public String ItemUrl
        {
            get { return _itemUrl; }
            set { _itemUrl = value; }
        }

        private String _titleUrl = String.Empty;

        public String TitleUrl
        {
            get { return _titleUrl; }
            set { _titleUrl = value; }
        }

        private String _title = String.Empty;

        public String Title
        {
            get { return _title; }
            set { _title = value; }
        }

        private String _sTitle = String.Empty;

        public String STitle
        {
            get { return _sTitle; }
            set { _sTitle = value; }
        }

        private String _genre = String.Empty;

        public String Genre
        {
            get { return _genre; }
            set { _genre = value; }
        }

        private List<String> _authors = new List<string>();

        public List<String> Authors
        {
            get { return _authors; }
            set { _authors = value; }
        }

        private List<String> _subjects = new List<string>();

        public List<String> Subjects
        {
            get { return _subjects; }
            set { _subjects = value; }
        }

        private String _publisherName = String.Empty;

        public String PublisherName
        {
            get { return _publisherName; }
            set { _publisherName = value; }
        }

        private String _publisherPlace = String.Empty;

        public String PublisherPlace
        {
            get { return _publisherPlace; }
            set { _publisherPlace = value; }
        }

        private String _date = String.Empty;

        public String Date
        {
            get { return _date; }
            set { _date = value; }
        }

        private String _volume = String.Empty;

        public String Volume
        {
            get { return _volume; }
            set { _volume = value; }
        }

        private String _edition = String.Empty;

        public String Edition
        {
            get { return _edition; }
            set { _edition = value; }
        }

        private String _publicationFrequency = String.Empty;

        public String PublicationFrequency
        {
            get { return _publicationFrequency; }
            set { _publicationFrequency = value; }
        }

        private String _language = String.Empty;

        public String Language
        {
            get { return _language; }
            set { _language = value; }
        }

        private String _oclc = String.Empty;

        public String Oclc
        {
            get { return _oclc; }
            set { _oclc = value; }
        }

        private String _lccn = String.Empty;

        public String Lccn
        {
            get { return _lccn; }
            set { _lccn = value; }
        }

        private String _issn = String.Empty;

        public String Issn
        {
            get { return _issn; }
            set { _issn = value; }
        }

        private String _isbn = String.Empty;

        public String Isbn
        {
            get { return _isbn; }
            set { _isbn = value; }
        }

        private String _aTitle = String.Empty;

        public String ATitle
        {
            get { return _aTitle; }
            set { _aTitle = value; }
        }

        private String _sPage = String.Empty;

        public String SPage
        {
            get { return _sPage; }
            set { _sPage = value; }
        }

        private String _ePage = String.Empty;

        public String EPage
        {
            get { return _ePage; }
            set { _ePage = value; }
        }

        private String _pages = String.Empty;

        public String Pages
        {
            get { return _pages; }
            set { _pages = value; }
        }
    }
}
