using System;
using CustomDataAccess;

namespace MOBOT.BHL.API.BHLApiDataObjects3
{
    [Serializable]
    public class TitleVariant : DataObjectBase, ISetValues
    {
        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public TitleVariant()
        {
        }

        #endregion Constructors

        #region Properties

        private string _titleVariantTypeName = null;
        public string TitleVariantTypeName
        {
            get { return _titleVariantTypeName; }
            set
            {
                if (value != null) value = CalibrateValue(value, 30);
                _titleVariantTypeName = value;
            }
        }

        private string _title = null;
        public string Title
        {
            get { return _title; }
            set
            {
                if (value != null) value = CalibrateValue(value, 500);
                _title = value;
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
                    case "TitleVariantTypeName":
                        {
                            _titleVariantTypeName = (string)column.Value;
                            break;
                        }
                    case "Title":
                        {
                            _title = (string)column.Value;
                            break;
                        }
                }
            }
        }

        #endregion
    }
}
