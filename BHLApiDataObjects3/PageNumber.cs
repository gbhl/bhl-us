using System;
using CustomDataAccess;

namespace MOBOT.BHL.API.BHLApiDataObjects3
{
    [Serializable]
    public class PageNumber : DataObjectBase, ISetValues
    {
        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public PageNumber()
        {
        }

        public PageNumber(string prefix, string number)
        {
            _Prefix = prefix;
            _Number = number;
        }

        public PageNumber(string prefix, string number, string implied)
        {
            _Prefix = prefix;
            _Number = number;
            _Implied = implied;
        }

        #endregion Constructors

        #region Properties

        private string _Prefix = null;
        public string Prefix
        {
            get { return _Prefix; }
            set
            {
                if (value != null) value = CalibrateValue(value, 20);
                _Prefix = value;
            }
        }

        private string _Number = null;
        public string Number
        {
            get { return _Number; }
            set
            {
                if (value != null) value = CalibrateValue(value, 20);
                _Number = value;
            }
        }

        private string _Implied = null;
        public string Implied
        {
            get { return _Implied; }
            set { _Implied = value; }
        }

        #endregion Properties

        #region ISetValues Members

        public void SetValues(CustomDataRow row)
        {
            foreach (CustomDataColumn column in row)
            {
                switch (column.Name)
                {
                    case "PagePrefix":
                        {
                            _Prefix = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "PageNumber":
                        {
                            _Number = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "Implied":
                        {
                            _Implied = (column.Value == null) ? (string)null : ((bool)column.Value ? 1 : 0).ToString();
                            break;
                        }
                }
            }
        }

        #endregion
    }
}
