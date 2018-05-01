using System;
using CustomDataAccess;

namespace MOBOT.BHL.API.BHLApiDataObjects3
{
    [Serializable]
    public class PageType : DataObjectBase, ISetValues
    {
        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public PageType()
        {
        }

        public PageType(string pageTypeName)
        {
            _PageTypeName = pageTypeName;
        }

        #endregion Constructors

        #region Properties

        private string _PageTypeName = null;
        public string PageTypeName
        {
            get
            {
                return _PageTypeName;
            }
            set
            {
                if (value != null) value = CalibrateValue(value, 30);
                _PageTypeName = value;
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
                    case "PageTypeName":
                        {
                            _PageTypeName = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                }
            }
        }

        #endregion
    }
}
