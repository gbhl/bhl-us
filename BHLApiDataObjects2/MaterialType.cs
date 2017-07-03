using CustomDataAccess;
using System;

namespace MOBOT.BHL.API.BHLApiDataObjects2
{
    [Serializable]
    public class MaterialType : DataObjectBase, ISetValues
    {
        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public MaterialType()
        {
        }

        #endregion Constructors

        #region Properties

        private int _MaterialTypeID = default(int);
        public int MaterialTypeID
        {
            get { return _MaterialTypeID; }
            set { _MaterialTypeID = value; }
        }

        private string _MaterialTypeName = null;
        public string MaterialTypeName
        {
            get { return _MaterialTypeName; }
            set
            {
                if (value != null) value = CalibrateValue(value, 60);
                _MaterialTypeName = value;
            }
        }

        private string _MaterialTypeLabel = null;
        public string MaterialTypeLabel
        {
            get { return _MaterialTypeLabel; }
            set
            {
                if (value != null) value = CalibrateValue(value, 60);
                _MaterialTypeLabel = value;
            }
        }

        private string _MARCCode = null;
        public string MARCCode
        {
            get { return _MARCCode; }
            set { _MARCCode = value; }
        }

        #endregion Properties

        #region ISetValues Members

        public void SetValues(CustomDataRow row)
        {
            foreach (CustomDataColumn column in row)
            {
                switch (column.Name)
                {
                    case "MaterialTypeID":
                        {
                            _MaterialTypeID = (int)column.Value;
                            break;
                        }
                    case "MaterialTypeName":
                        {
                            _MaterialTypeName = (string)column.Value;
                            break;
                        }
                    case "MaterialTypeLabel":
                        {
                            _MaterialTypeLabel = (string)column.Value;
                            break;
                        }
                    case "MARCCode":
                        {
                            _MARCCode = (string)column.Value;
                            break;
                        }
                }
            }
        }

        #endregion
    }
}
