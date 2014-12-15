using System;
using System.Collections.Generic;
using System.Text;
using CustomDataAccess;

namespace MOBOT.BHL.DataObjects
{
    public class TitleEndNote : ISetValues
    {
        private int _titleID = 0;

        public int TitleID
        {
            get { return _titleID; }
            set { _titleID = value; }
        }

        private int _itemID = 0;

        public int ItemID
        {
            get { return _itemID; }
            set { _itemID = value; }
        }

        private int _segmentID = 0;

        public int SegmentID
        {
            get { return _segmentID; }
            set { _segmentID = value; }
        }

        private String _publicationType = String.Empty;

        public String PublicationType
        {
            get { return _publicationType; }
            set { _publicationType = value; }
        }

        private String _authors = String.Empty;

        public String Authors
        {
            get { return _authors; }
            set { _authors = value; }
        }

        private String _year = String.Empty;

        public String Year
        {
            get { return _year; }
            set { _year = value; }
        }

        private string _title = String.Empty;

        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        private String _fullTitle = String.Empty;

        public String FullTitle
        {
            get { return _fullTitle; }
            set { _fullTitle = value; }
        }

        private String _secondaryTitle = String.Empty;

        public String SecondaryTitle
        {
            get { return _secondaryTitle; }
            set { _secondaryTitle = value; }
        }

        private String _journal = String.Empty;

        public String Journal
        {
            get { return _journal; }
            set { _journal = value; }
        }

        private String _publisher = String.Empty;

        public String Publisher
        {
            get { return _publisher; }
            set { _publisher = value; }
        }

        private String _publisherPlace = String.Empty;

        public String PublisherPlace
        {
            get { return _publisherPlace; }
            set { _publisherPlace = value; }
        }

        private String _publisherName = String.Empty;

        public String PublisherName
        {
            get { return _publisherName; }
            set { _publisherName = value; }
        }

        private String _volume = String.Empty;

        public String Volume
        {
            get { return _volume; }
            set { _volume = value; }
        }

        private String _series = String.Empty;

        public String Series
        {
            get { return _series; }
            set { _series = value; }
        }

        private String _issue = String.Empty;

        public String Issue
        {
            get { return _issue; }
            set { _issue = value; }
        }

        private String _pageRange = String.Empty;

        public String PageRange
        {
            get { return _pageRange; }
            set { _pageRange = value; }
        }

        private String _startPage = String.Empty;

        public String StartPage
        {
            get { return _startPage; }
            set { _startPage = value; }
        }

        private String _shortTitle = String.Empty;

        public String ShortTitle
        {
            get { return _shortTitle; }
            set { _shortTitle = value; }
        }

        private String _abbreviation = String.Empty;

        public String Abbreviation
        {
            get { return _abbreviation; }
            set { _abbreviation = value; }
        }

        private String _isbn = String.Empty;

        public String Isbn
        {
            get { return _isbn; }
            set { _isbn = value; }
        }

        private String _callNumber = String.Empty;

        public String CallNumber
        {
            get { return _callNumber; }
            set { _callNumber = value; }
        }

        private String _keywords = String.Empty;

        public String Keywords
        {
            get { return _keywords; }
            set { _keywords = value; }
        }

        private String _languageName = String.Empty;

        public String LanguageName
        {
            get { return _languageName; }
            set { _languageName = value; }
        }

        private String _summary = String.Empty;

        public String Summary
        {
            get { return _summary; }
            set { _summary = value; }
        }

        private String _note = String.Empty;

        public String Note
        {
            get { return _note; }
            set { _note = value; }
        }

        private String _editionStatement = String.Empty;

        public String EditionStatement
        {
            get { return _editionStatement; }
            set { _editionStatement = value; }
        }

        private String _doi = String.Empty;

        public String Doi
        {
            get { return _doi; }
            set { _doi = value; }
        }

        #region ISetValues Members

        public void SetValues(CustomDataRow row)
        {
            foreach (CustomDataColumn column in row)
            {
                switch (column.Name)
                {
                    case "TitleID":
                        {
                            TitleID = Utility.ZeroIfNull(column.Value);
                            break;
                        }
                    case "ItemID":
                        {
                            ItemID = Utility.ZeroIfNull(column.Value);
                            break;
                        }
                    case "SegmentID":
                        {
                            SegmentID = Utility.ZeroIfNull(column.Value);
                            break;
                        }
                    case "PublicationType":
                        {
                            PublicationType = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "Authors":
                        {
                            Authors = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "Year":
                        {
                            Year = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "Title":
                        {
                            Title = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "FullTitle":
                        {
                            FullTitle = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "SecondaryTitle":
                        {
                            SecondaryTitle = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "Journal":
                        {
                            Journal = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "Publisher":
                        {
                            Publisher = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "PublisherPlace":
                        {
                            PublisherPlace = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "PublisherName":
                        {
                            PublisherName = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "Volume":
                        {
                            Volume = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "Series":
                        {
                            Series = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "Issue":
                        {
                            Issue = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "PageRange":
                        {
                            PageRange = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "StartPageNumber":
                        {
                            StartPage = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "ShortTitle":
                        {
                            ShortTitle = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "Abbreviation":
                        {
                            Abbreviation = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "ISBN":
                        {
                            Isbn = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "CallNumber":
                        {
                            CallNumber = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "Keywords":
                        {
                            Keywords = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "LanguageName":
                        {
                            LanguageName = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "Summary":
                        {
                            Summary = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "Note":
                        {
                            Note = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "EditionStatement":
                        {
                            EditionStatement = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "DOI":
                        {
                            Doi = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                }
            }
        }

        #endregion
    }
}
