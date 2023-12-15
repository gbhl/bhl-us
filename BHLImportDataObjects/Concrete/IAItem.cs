
#region Using

using System;
using CustomDataAccess;

#endregion Using

namespace MOBOT.BHLImport.DataObjects
{
	[Serializable]
	public class IAItem : __IAItem
	{

        private int _totalItems;
        private string _status;
        private string _holdingInstitution;
        private string _createdUser;
        private string _lastModifiedUser;

        public int TotalItems
        {
            get { return _totalItems; }
            set { _totalItems = value; }
        }

        public string Status
        {
            get { return _status; }
            set { _status = value; }
        }

        public string HoldingInstitution
        {
            get { return _holdingInstitution; }
            set { _holdingInstitution = value; }
        }

        public string CreatedUser
        {
            get { return _createdUser; }
            set { _createdUser = value; }
        }

        public string LastModifiedUser
        {
            get { return _lastModifiedUser; }
            set { _lastModifiedUser = value; }
        }

        public override void SetValues(CustomDataRow row)
        {
            foreach (CustomDataColumn column in row)
            {
                switch (column.Name)
                {
                    case "TotalItems":
                        {
                            _totalItems = (int)column.Value;
                            break;
                        }
                    case "Status":
                        {
                            _status = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "HoldingInstitution":
                        {
                            _holdingInstitution = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "CreatedUser":
                        {
                            _createdUser = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "LastModifiedUser":
                        {
                            _lastModifiedUser = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                }
            }

            base.SetValues(row);
        }

	}
}
