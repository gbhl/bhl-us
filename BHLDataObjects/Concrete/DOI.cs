using System;
using CustomDataAccess;

namespace MOBOT.BHL.DataObjects
{
	[Serializable]
	public class DOI : __DOI
	{
        private string _doiEntityTypeName = string.Empty;
        public string DOIEntityTypeName
        {
            get { return _doiEntityTypeName; }
            set { _doiEntityTypeName = value; }
        }

        private string _doiStatusName = string.Empty;
        public string DOIStatusName
        {
            get { return _doiStatusName; }
            set { _doiStatusName = value; }
        }

        private string _action = string.Empty;
        public string Action
        {
            get { return _action; }
            set { _action = value; }
        }

        private string _entityDetail = string.Empty;
        public string EntityDetail
        {
            get { return _entityDetail; }
            set { _entityDetail = value; }
        }

        private int? _containerTitleID = null;
        public int? ContainerTitleID
        {
            get { return _containerTitleID; }
            set { _containerTitleID = value; }
        }

        private int _totalDOIs = 0;
        public int TotalDOIs
        {
            get { return _totalDOIs; }
            set { _totalDOIs = value; }
        }

        private string _creationUserName = string.Empty;
        public string CreationUserName
        { 
            get { return _creationUserName; } 
            set { _creationUserName = value; }
        }

        public override void SetValues(CustomDataRow row)
        {
            foreach (CustomDataColumn column in row)
            {
                switch (column.Name)
                {
                    case "DOIEntityTypeName":
                        {
                            _doiEntityTypeName = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "DOIStatusName":
                        {
                            _doiStatusName = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "Action":
                        {
                            _action = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "EntityDetail":
                        {
                            _entityDetail = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "ContainerTitleID":
                        {
                            _containerTitleID = (int?)column.Value;
                            break;
                        }
                    case "TotalDOIs":
                        {
                            _totalDOIs = (int)column.Value;
                            break;
                        }
                    case "CreationUserName":
                        {
                            _creationUserName = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                }
            }

            base.SetValues(row);
        }
    }
}
