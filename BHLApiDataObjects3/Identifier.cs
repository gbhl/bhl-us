using CustomDataAccess;
using System;

namespace MOBOT.BHL.API.BHLApiDataObjects3
{
    [Serializable]
    public class Identifier : DataObjectBase, ISetValues
    {
        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Identifier()
        {
        }

        public Identifier(string type, string value)
        {
            _IdentifierName = type;
            _IdentifierValue = value;
        }

        #endregion Constructors

        #region Properties

        private string _IdentifierName = null;
        public string IdentifierName
        {
            get { return _IdentifierName; }
            set
            {
                if (value != null) value = CalibrateValue(value, 40);
                _IdentifierName = value;
            }
        }

        private string _IdentifierValue = null;
        public string IdentifierValue
        {
            get { return _IdentifierValue; }
            set
            {
                if (value != null) value = CalibrateValue(value, 125);
                _IdentifierValue = value;
            }
        }

        #endregion

        #region ISetValues Members

        public void SetValues(CustomDataRow row)
        {
            foreach (CustomDataColumn column in row)
            {
                switch (column.Name)
                {
                    case "IdentifierName":
                        {
                            _IdentifierName = (string)column.Value;
                            break;
                        }
                    case "IdentifierValue":
                        {
                            _IdentifierValue = (string)column.Value;
                            break;
                        }
                }
            }
        }

        #endregion
    }
}
