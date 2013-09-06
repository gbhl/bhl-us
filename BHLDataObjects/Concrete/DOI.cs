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

        private string _entityDetail = string.Empty;
        public string EntityDetail
        {
            get { return _entityDetail; }
            set { _entityDetail = value; }
        }

        private int _totalDOIs = 0;
        public int TotalDOIs
        {
            get { return _totalDOIs; }
            set { _totalDOIs = value; }
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
                    case "EntityDetail":
                        {
                            _entityDetail = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "TotalDOIs":
                        {
                            _totalDOIs = (int)column.Value;
                            break;
                        }
                }
            }

            base.SetValues(row);
        }
    }
}
