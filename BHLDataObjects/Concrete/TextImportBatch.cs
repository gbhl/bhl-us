
#region Using

using CustomDataAccess;
using System;

#endregion Using

namespace MOBOT.BHL.DataObjects
{
	[Serializable]
	public class TextImportBatch : __TextImportBatch
	{
        #region Properties

        private string _statusName;

        public string StatusName
        {
            get { return _statusName; }
            set { _statusName = value; }
        }

        private string _creationUser;

        public string CreationUser
        {
            get { return _creationUser; }
            set { _creationUser = value; }
        }

        private string _lastModifiedUser;

        public string LastModifiedUser
        {
            get { return _lastModifiedUser; }
            set { _lastModifiedUser = value; }
        }

        private int _totalRecords;

        public int TotalRecords
        {
            get { return _totalRecords; }
            set { _totalRecords = value; }
        }

        private int _readyRecords;

        public int ReadyRecords
        {
            get { return _readyRecords; }
            set { _readyRecords = value; }
        }

        private int _reviewRecords;

        public int ReviewRecords
        {
            get { return _reviewRecords; }
            set { _reviewRecords = value; }
        }

        private int _rejectedRecords;

        public int RejectedRecords
        {
            get { return _rejectedRecords; }
            set { _rejectedRecords = value; }
        }

        private int _importedRecords;

        public int ImportedRecords
        {
            get { return _importedRecords; }
            set { _importedRecords = value; }
        }

        private int _errorRecords;

        public int ErrorRecords
        {
            get { return _errorRecords; }
            set { _errorRecords = value; }
        }

        #endregion Properties

        #region ISet override

        public override void SetValues(CustomDataRow row)
        {
            foreach (CustomDataColumn column in row)
            {
                switch (column.Name)
                {
                    case "StatusName":
                        {
                            _statusName = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "CreationUser":
                        {
                            _creationUser = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "LastModifiedUser":
                        {
                            _lastModifiedUser = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "TotalRecords":
                        {
                            _totalRecords = Utility.ZeroIfNull(column.Value);
                            break;
                        }
                    case "ReadyRecords":
                        {
                            _readyRecords = Utility.ZeroIfNull(column.Value);
                            break;
                        }
                    case "ReviewRecords":
                        {
                            _reviewRecords = Utility.ZeroIfNull(column.Value);
                            break;
                        }
                    case "ImportedRecords":
                        {
                            _importedRecords = Utility.ZeroIfNull(column.Value);
                            break;
                        }
                    case "RejectedRecords":
                        {
                            _rejectedRecords = Utility.ZeroIfNull(column.Value);
                            break;
                        }
                    case "ErrorRecords":
                        {
                            _errorRecords = Utility.ZeroIfNull(column.Value);
                            break;
                        }
                }
            }

            base.SetValues(row);
        }

        #endregion

    }
}

