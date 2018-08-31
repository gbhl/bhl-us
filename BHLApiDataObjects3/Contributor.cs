using System;
using CustomDataAccess;

namespace MOBOT.BHL.API.BHLApiDataObjects3
{
    [Serializable]
    public class Contributor : DataObjectBase, ISetValues
    {
        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Contributor()
        {
        }

        #endregion Constructors

        #region Properties

        private string _contributorName = string.Empty;

        public string ContributorName
        {
            get { return _contributorName; }
            set { _contributorName = value; }
        }

        #endregion Properties

        #region Set Values

        /// <summary>
        /// Set the property values of this instance from the specified <see cref="CustomDataRow"/>.
        /// </summary>
        public virtual void SetValues(CustomDataRow row)
        {
            foreach (CustomDataColumn column in row)
            {
                switch (column.Name)
                {
                    case "InstitutionName":
                        {
                            _contributorName = (string)column.Value;
                            break;
                        }
                }
            }
        }

        #endregion Set Values

    }
}
