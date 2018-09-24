
#region Using

using CustomDataAccess;
using System;

#endregion Using

namespace MOBOT.BHL.DataObjects
{
	[Serializable]
	public class TextImportBatchFile : __TextImportBatchFile
	{
        #region Properties

        private int _totalFiles;

        public int TotalFiles
        {
            get { return _totalFiles; }
            set { _totalFiles = value; }
        }

        private string _statusName;

        public string StatusName
        {
            get { return _statusName; }
            set { _statusName = value; }
        }

        private string _title;

        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        private string _volume;

        public string Volume
        {
            get { return _volume; }
            set { _volume = value; }
        }

        private string _year;

        public string Year
        {
            get { return _year; }
            set { _year = value; }
        }

        public string ItemDescription
        {
            get
            {
                return this.Title +
                    (string.IsNullOrWhiteSpace(this.Volume) ? "" : " - " + this.Volume) +
                    (string.IsNullOrWhiteSpace(this.Year) ? "" : " - " + this.Year);
            }
        }

        #endregion Properties

        #region ISet override

        public override void SetValues(CustomDataRow row)
        {
            foreach (CustomDataColumn column in row)
            {
                switch (column.Name)
                {
                    case "TotalFiles":
                        {
                            _totalFiles = Utility.ZeroIfNull(column.Value);
                            break;
                        }
                    case "StatusName":
                        {
                            _statusName = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "Title":
                        {
                            _title = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "Volume":
                        {
                            _volume = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "Year":
                        {
                            _year = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                }
            }

            base.SetValues(row);
        }

        #endregion

    }
}

