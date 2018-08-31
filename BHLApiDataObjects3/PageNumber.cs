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
                }
            }
        }

        #endregion
    }
}
