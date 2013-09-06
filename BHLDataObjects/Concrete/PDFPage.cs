
#region Using

using System;
using CustomDataAccess;

#endregion Using

namespace MOBOT.BHL.DataObjects
{
	[Serializable]
	public class PDFPage : __PDFPage
	{
        #region Properties

        private String _fileNamePrefix;

        public String FileNamePrefix
        {
            get { return _fileNamePrefix; }
            set { _fileNamePrefix = value; }
        }

        private String _pagePrefix;

        public String PagePrefix
        {
            get { return _pagePrefix; }
            set { _pagePrefix = value; }
        }

        private String _pageNumber;

        public String PageNumber
        {
            get { return _pageNumber; }
            set { _pageNumber = value; }
        }

        private String _pageTypeName;

        public String PageTypeName
        {
            get { return _pageTypeName; }
            set { _pageTypeName = value; }
        }

        #endregion Properties

        #region ISet override

        public override void SetValues(CustomDataRow row)
        {
            foreach (CustomDataColumn column in row)
            {
                switch (column.Name)
                {
                    case "FileNamePrefix":
                        {
                            _fileNamePrefix = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "PagePrefix":
                        {
                            _pagePrefix = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "PageNumber":
                        {
                            _pageNumber = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "PageTypeName":
                        {
                            _pageTypeName = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                }
            }

            base.SetValues(row);
        }

        #endregion
    }
}
