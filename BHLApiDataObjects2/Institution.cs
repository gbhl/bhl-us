using System;
using CustomDataAccess;

namespace MOBOT.BHL.API.BHLApiDataObjects2
{
    [Serializable]
    public class Institution : DataObjectBase, ISetValues
    {
        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Institution()
        {
        }

        #endregion Constructors

        #region Properties

        private string _institutionCode = string.Empty;

        public string InstitutionCode
        {
            get { return _institutionCode; }
            set { _institutionCode = value; }
        }

        private string _institutionName = string.Empty;

        public string InstitutionName
        {
            get { return _institutionName; }
            set { _institutionName = value; }
        }

        private string _institutionUrl = string.Empty;

        public string InstitutionUrl
        {
            get { return _institutionUrl; }
            set { _institutionUrl = value; }
        }

        private bool _bhlMember = false;

        public bool BHLMember
        {
            get { return _bhlMember; }
            set { _bhlMember = value; }
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
                    case "InstitutionCode":
                        {
                            _institutionCode = (string)column.Value;
                            break;
                        }
                    case "InstitutionName":
                        {
                            _institutionName = (string)column.Value;
                            break;
                        }
                    case "InstitutionUrl":
                        {
                            _institutionUrl = (string)column.Value;
                            break;
                        }
                    case "BHLMemberLibrary":
                        {
                            _bhlMember = (bool)column.Value;
                            break;
                        }
                }
            }
        }

        #endregion Set Values

    }
}
