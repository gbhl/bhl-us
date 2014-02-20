
#region Using

using CustomDataAccess;
using System;

#endregion Using

namespace MOBOT.BHL.DataObjects
{
	[Serializable]
	public class ImportFile : __ImportFile
    {
        #region Properties

        public string ImportFileNameClean
        {
            get
            {
                int position = ImportFileName.IndexOf('.');
                int length = ImportFileName.Length;
                return ImportFileName.Substring((position >= 0 && position != length - 1) ? position + 1 : 0);
            }
        }

        private string _statusName;

        public string StatusName
        {
            get { return _statusName; }
            set { _statusName = value; }
        }

        private string _contributorName;

        public string ContributorName
        {
            get { return _contributorName; }
            set { _contributorName = value; }
        }

        private int _totalRecords;

        public int TotalRecords
        {
            get { return _totalRecords; }
            set { _totalRecords = value; }
        }

        private int _newRecords;

        public int NewRecords
        {
            get { return _newRecords; }
            set { _newRecords = value; }
        }

        private int _importedRecords;

        public int ImportedRecords
        {
            get { return _importedRecords; }
            set { _importedRecords = value; }
        }

        private int _rejectedRecords;

        public int RejectedRecords
        {
            get { return _rejectedRecords; }
            set { _rejectedRecords = value; }
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
                    case "InstitutionName":
                        {
                            _contributorName = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "StatusName":
                        {
                            _statusName = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "TotalRecords":
                        {
                            _totalRecords = Utility.ZeroIfNull(column.Value);
                            break;
                        }
                    case "NewRecords":
                        {
                            _newRecords = Utility.ZeroIfNull(column.Value);
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
