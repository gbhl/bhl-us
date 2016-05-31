using System;
using CustomDataAccess;

namespace MOBOT.BHL.API.BHLApiDataObjects2
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

        /*
        private string _contributorUrl = string.Empty;

        public string ContributorUrl
        {
            get { return _contributorUrl; }
            set { _contributorUrl = value; }
        }

        private bool _bhlMemberLibrary = false;

        public bool BHLMemberLibrary
        {
            get { return _bhlMemberLibrary; }
            set { _bhlMemberLibrary = value; }
        }
        */

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
                    /*
                    case "InstitutionUrl":
                        {
                            _contributorUrl = (string)column.Value;
                            break;
                        }
                    case "BHLMemberLibrary":
                        {
                            _bhlMemberLibrary = (bool)column.Value;
                            break;
                        }
                    */
                }
            }
        }

        #endregion Set Values

    }
}
