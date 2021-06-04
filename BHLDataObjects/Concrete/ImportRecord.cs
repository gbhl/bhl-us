
#region Using

using CustomDataAccess;
using System;
using System.Collections.Generic;

#endregion Using

namespace MOBOT.BHL.DataObjects
{
	[Serializable]
	public class ImportRecord : __ImportRecord
	{
        #region Properties

        private int _totalRecords = 0;
        private string _statusName = string.Empty;
        private string _authorString = string.Empty;
        private string _keywordString = string.Empty;
        private string _contributorString = string.Empty;
        private string _pageString = string.Empty;
        private string _errorString = string.Empty;
        private string _warningString = string.Empty;
        private string _titleIDString = string.Empty;
        private string _itemIDString = string.Empty;
        private string _segmentIDString = string.Empty;
        private string _startPageIDString = string.Empty;
        private string _endPageIDString = string.Empty;
        private List<string> _pageIDs = new List<string>();
        private List<ImportRecordCreator> _authors = new List<ImportRecordCreator>();
        private List<ImportRecordKeyword> _keywords = new List<ImportRecordKeyword>();
        private List<ImportRecordContributor> _contributors = new List<ImportRecordContributor>();
        private List<ImportRecordPage> _pages = new List<ImportRecordPage>();
        private List<ImportRecordErrorLog> _errors = new List<ImportRecordErrorLog>();
        private List<ImportRecordErrorLog> _warnings = new List<ImportRecordErrorLog>();

        public int TotalRecords
        {
            get { return _totalRecords; }
            set { _totalRecords = value; }
        }

        public string StatusName
        {
            get { return _statusName; }
            set { _statusName = value; }
        }

        public string AuthorString
        {
            get { return _authorString; }
            set { _authorString = value; }
        }

        public string KeywordString
        {
            get { return _keywordString; }
            set { _keywordString = value; }
        }

        public string ContributorString
        {
            get { return _contributorString; }
            set { _contributorString = value; }
        }

        public string PageString
        {
            get { return _pageString; }
            set { _pageString = value; }
        }

        public string ErrorString
        {
            get { return _errorString; }
            set { _errorString = value; }
        }

        public string WarningString
        {
            get { return _warningString; }
            set { _warningString = value; }
        }

        public string TitleIDString
        {
            get { return _titleIDString; }
            set { _titleIDString = value; }
        }

        public string ItemIDString
        {
            get { return _itemIDString; }
            set { _itemIDString = value; }
        }

        public string SegmentIDString
        {
            get { return _segmentIDString; }
            set { _segmentIDString = value; }
        }

        public string StartPageIDString
        {
            get { return _startPageIDString; }
            set { _startPageIDString = value; }
        }

        public string EndPageIDString
        {
            get { return _endPageIDString; }
            set { _endPageIDString = value; }
        }

        public List<string> PageIDs
        {
            get { return _pageIDs; }
            set { _pageIDs = value; }
        }

        public List<ImportRecordCreator> Authors
        {
            get { return _authors; }
            set { _authors = value; }
        }

        public List<ImportRecordKeyword> Keywords
        {
            get { return _keywords; }
            set { _keywords = value; }
        }

        public List<ImportRecordContributor> Contributors
        {
            get { return _contributors; }
            set { _contributors = value; }
        }

        public List<ImportRecordPage> Pages
        {
            get { return _pages; }
            set { _pages = value; }
        }

        public List<ImportRecordErrorLog> Errors
        {
            get { return _errors; }
            set { _errors = value; }
        }

        public List<ImportRecordErrorLog> Warnings
        {
            get { return _warnings; }
            set { _warnings = value; }
        }

        #endregion Properties

        public override void SetValues(CustomDataAccess.CustomDataRow row)
        {
            foreach (CustomDataColumn column in row)
            {
                switch (column.Name)
                {
                    case "TotalRecords":
                        TotalRecords = Utility.ZeroIfNull(column.Value);
                        break;
                    case "StatusName":
                        StatusName = Utility.EmptyIfNull(column.Value);
                        break;
                    case "Authors":
                        AuthorString = Utility.EmptyIfNull(column.Value);
                        break;
                    case "Keywords":
                        KeywordString = Utility.EmptyIfNull(column.Value);
                        break;
                    case "Contributors":
                        ContributorString = Utility.EmptyIfNull(column.Value);
                        break;
                    case "Pages":
                        PageString = Utility.EmptyIfNull(column.Value);
                        break;
                    case "Errors":
                        ErrorString = Utility.EmptyIfNull(column.Value);
                        break;
                    case "Warnings":
                        WarningString = Utility.EmptyIfNull(column.Value);
                        break;
                }
            }
            base.SetValues(row);
        }
    }
}

