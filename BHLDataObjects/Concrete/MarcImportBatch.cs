
#region Using

using System;
using CustomDataAccess;

#endregion Using

namespace MOBOT.BHL.DataObjects
{
	[Serializable]
	public class MarcImportBatch : __MarcImportBatch
    {
        #region Properties

        private int _newBatch;

        public int NewBatch
        {
            get { return _newBatch; }
            set { _newBatch = value; }
        }

        private int _pendingImport;

        public int PendingImport
        {
            get { return _pendingImport; }
            set { _pendingImport = value; }
        }

        private int _complete;

        public int Complete
        {
            get { return _complete; }
            set { _complete = value; }
        }

        private int _error;

        public int Error
        {
            get { return _error; }
            set { _error = value; }
        }

        private String _institutionName;

        public String InstitutionName
        {
            get { return _institutionName; }
            set { _institutionName = value; }
        }

        #endregion

        #region ISet override

        public override void SetValues(CustomDataRow row)
        {
            foreach (CustomDataColumn column in row)
            {
                switch (column.Name)
                {
                    case "New":
                        {
                            _newBatch = Utility.ZeroIfNull(column.Value);
                            break;
                        }
                    case "PendingImport":
                        {
                            _pendingImport = Utility.ZeroIfNull(column.Value);
                            break;
                        }
                    case "Complete":
                        {
                            _complete = Utility.ZeroIfNull(column.Value);
                            break;
                        }
                    case "Error":
                        {
                            _error = Utility.ZeroIfNull(column.Value);
                            break;
                        }
                    case "InstitutionName":
                        {
                            _institutionName = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                }
            }

            base.SetValues(row);
        }

        #endregion

    }
}
