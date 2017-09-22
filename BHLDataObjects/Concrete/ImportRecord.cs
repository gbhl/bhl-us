
#region Using

using CustomDataAccess;
using System;

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
        private string _errorString = string.Empty;
        private string _titleIDString = string.Empty;
        private string _itemIDString = string.Empty;
        private string _startPageIDString = string.Empty;
        private string _endPageIDString = string.Empty;
        private CustomGenericList<string> _pageIDs = new CustomGenericList<string>();
        private CustomGenericList<ImportRecordCreator> _authors = new CustomGenericList<ImportRecordCreator>();
        private CustomGenericList<ImportRecordKeyword> _keywords = new CustomGenericList<ImportRecordKeyword>();
        private CustomGenericList<ImportRecordPage> _pages = new CustomGenericList<ImportRecordPage>();
        private CustomGenericList<ImportRecordErrorLog> _errors = new CustomGenericList<ImportRecordErrorLog>();

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

        public string ErrorString
        {
            get { return _errorString; }
            set { _errorString = value; }
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

        public CustomGenericList<string> PageIDs
        {
            get { return _pageIDs; }
            set { _pageIDs = value; }
        }

        public CustomGenericList<ImportRecordCreator> Authors
        {
            get { return _authors; }
            set { _authors = value; }
        }

        public CustomGenericList<ImportRecordKeyword> Keywords
        {
            get { return _keywords; }
            set { _keywords = value; }
        }

        public CustomGenericList<ImportRecordPage> Pages
        {
            get { return _pages; }
            set { _pages = value; }
        }

        public CustomGenericList<ImportRecordErrorLog> Errors
        {
            get { return _errors; }
            set { _errors = value; }
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
                    case "Errors":
                        ErrorString = Utility.EmptyIfNull(column.Value);
                        break;
                }
            }
            base.SetValues(row);
        }
    }
}

