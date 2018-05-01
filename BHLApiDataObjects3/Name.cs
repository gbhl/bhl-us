using System;
using System.Collections.Generic;
using CustomDataAccess;

namespace MOBOT.BHL.API.BHLApiDataObjects3
{
    [Serializable]
    public class Name : DataObjectBase, ISetValues
    {
        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Name()
        {
        }

        #endregion Constructors

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
                    case "NameBankID":
                        {
                            _identifiers.Add(new Identifier("NameBank", Utility.EmptyIfNull(column.Value)));
                            break;
                        }
                    case "EOLID":
                        {
                            _identifiers.Add(new Identifier("EOL", Utility.EmptyIfNull(column.Value)));
                            break;
                        }
                    case "NameString":
                        {
                            _NameFound = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "ResolvedNameString":
                        {
                            _NameConfirmed = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                }
            }
        }

        #endregion Set Values

        #region Properties		

        private List<Identifier> _identifiers = new List<Identifier>();
        public List<Identifier> Identifiers
        {
            get { return _identifiers; }
            set { _identifiers = value; }
        }

        private string _NameFound = null;
        public string NameFound
        {
            get { return _NameFound; }
            set
            {
                if (value != null) value = CalibrateValue(value, 100);
                _NameFound = value;
            }
        }


        private string _NameConfirmed = null;
        public string NameConfirmed
        {
            get { return _NameConfirmed; }
            set
            {
                if (value != null) value = CalibrateValue(value, 100);
                _NameConfirmed = value;
            }
        }

        CustomGenericList<Title> _Titles;
        public CustomGenericList<Title> Titles
        {
            get { return _Titles; }
            set { _Titles = value; }
        }

        #endregion Properties
    }
}
