using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MOBOT.IAFileGenerator
{
    public class PageData
    {
        private string _fileName;

        public string FileName
        {
            get { return _fileName; }
            set { _fileName = value; }
        }

        private string _sequence;

        public string Sequence
        {
            get { return _sequence; }
            set { _sequence = value; }
        }

        private string _pagePrefix;

        public string PagePrefix
        {
            get { return _pagePrefix; }
            set { _pagePrefix = value; }
        }

        private string _pageNumber;

        public string PageNumber
        {
            get { return _pageNumber; }
            set { _pageNumber = value; }
        }

        private string _impliedPageNumber;

        public string ImpliedPageNumber
        {
            get { return _impliedPageNumber; }
            set { _impliedPageNumber = value; }
        }

        private string _pageType;

        public string PageType
        {
            get { return _pageType; }
            set { _pageType = value; }
        }

        private string _year;

        public string Year
        {
            get { return _year; }
            set { _year = value; }
        }

        private string _volume;

        public string Volume
        {
            get { return _volume; }
            set { _volume = value; }
        }

        private string _issuePrefix;

        public string IssuePrefix
        {
            get { return _issuePrefix; }
            set { _issuePrefix = value; }
        }

        private string _issue;

        public string Issue
        {
            get { return _issue; }
            set { _issue = value; }
        }

        private string _dateCreated;

        public string DateCreated
        {
            get { return _dateCreated; }
            set { _dateCreated = value; }
        }

        private string _dateModified;

        public string DateModified
        {
            get { return _dateModified; }
            set { _dateModified = value; }
        }
    }
}
