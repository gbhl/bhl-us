using System;
using CustomDataAccess;

namespace MOBOT.BHL.DataObjects
{
    [Serializable]
    public class OAIIdentifier : ISetValues
    {
        private int _id;
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private String _setSpec;
        public String SetSpec
        {
            get { return _setSpec; }
            set { _setSpec = value; }
        }

        private DateTime? _lastModifiedDate;
        public DateTime? LastModifiedDate
        {
            get { return _lastModifiedDate; }
            set { _lastModifiedDate = value; }
        }

        #region ISetValues Members

        public void SetValues(CustomDataRow row)
        {
            foreach (CustomDataColumn column in row)
            {
                switch (column.Name)
                {
                    case "ID":
                        {
                            Id = (int)column.Value;
                            break;
                        }
                    case "SetSpec":
                        {
                            SetSpec = (String)column.Value;
                            break;
                        }
                    case "LastModifiedDate":
                        {
                            LastModifiedDate = (DateTime?)column.Value;
                            break;
                        }
                }
            }
        }

        #endregion
    }
}
