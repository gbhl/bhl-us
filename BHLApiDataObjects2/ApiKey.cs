using System;
using CustomDataAccess;

namespace MOBOT.BHL.API.BHLApiDataObjects2
{
    [Serializable]
    public class ApiKey : DataObjectBase, ISetValues
    {
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public ApiKey()
		{
		}

		#endregion Constructors
		
		#region Properties

        private int _ApiKeyID = default(int);
        public int ApiKeyID
        {
            get { return _ApiKeyID; }
            set { _ApiKeyID = value; }
        }

        private string _ContactName = null;
        public string ContactName
        {
            get { return _ContactName; }
            set
            {
                if (value != null) value = CalibrateValue(value, 200);
                _ContactName = value;
            }
        }

        private string _EmailAddress = null;
        public string EmailAddress
        {
            get { return _EmailAddress; }
            set
            {
                if (value != null) value = CalibrateValue(value, 200);
                _EmailAddress = value;
            }
        }

        private string _ApiKeyValue = null;
        public string ApiKeyValue
        {
            get { return _ApiKeyValue; }
            set { _ApiKeyValue = value; }
        }

        private int _IsActive = 1;
        public int IsActive
        {
            get { return _IsActive; }
            set { _IsActive = value; }
        }

        #endregion Properties

        #region ISetValues Members

        public void SetValues(CustomDataRow row)
        {
            foreach (CustomDataColumn column in row)
            {
                switch (column.Name)
                {
                    case "ApiKeyID":
                        {
                            _ApiKeyID = (int)column.Value;
                            break;
                        }
                    case "ContactName":
                        {
                            _ContactName = (string)column.Value;
                            break;
                        }
                    case "EmailAddress":
                        {
                            _EmailAddress = (string)column.Value;
                            break;
                        }
                    case "ApiKeyValue":
                        {
                            _ApiKeyValue = (string)column.Value;
                            break;
                        }
                    case "IsActive":
                        {
                            _IsActive = Convert.ToInt32(column.Value);
                            break;
                        }
                }
            }
        }

        #endregion
    }
}
