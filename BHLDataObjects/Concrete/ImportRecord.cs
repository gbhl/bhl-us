
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
        private CustomGenericList<ImportRecordCreator> _authors = new CustomGenericList<ImportRecordCreator>();
        private CustomGenericList<ImportRecordKeyword> _keywords = new CustomGenericList<ImportRecordKeyword>();

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
                }
            }
            base.SetValues(row);
        }
    }
}
