
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

        private string _genreName;

        public string GenreName
        {
            get { return _genreName; }
            set { _genreName = value; }
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

        private int _invalidRecords;
        public int InvalidRecords { get => _invalidRecords; set => _invalidRecords = value; }

        private int _warningRecords;
        public int WarningRecords { get => _warningRecords; set => _warningRecords = value; }

        private int _duplicateRecords;
        public int DuplicateRecords { get => _duplicateRecords; set => _duplicateRecords = value; }

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
                    case "GenreName":
                        {
                            _genreName = Utility.EmptyIfNull(column.Value);
                            break;
                        }
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

                    case "InvalidRecords":
                        {
                            _invalidRecords = Utility.ZeroIfNull(column.Value);
                            break;
                        }
                    case "WarningRecords":
                        {
                            _warningRecords = Utility.ZeroIfNull(column.Value);
                            break;
                        }
                    case "DuplicateRecords":
                        {
                            _duplicateRecords = Utility.ZeroIfNull(column.Value);
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
