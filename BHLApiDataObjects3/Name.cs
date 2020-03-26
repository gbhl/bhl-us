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
                    /*
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
                    */
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
                    case "CanonicalNameString":
                        {
                            _NameCanonical = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "CreationDate":
                        {
                            _creationDate = column.Value == null ? null : ((DateTime)column.Value).ToString("yyyy/MM/dd HH:mm:ss");
                            break;
                        }
                }
            }
        }

        #endregion Set Values

        #region Properties		

        private List<Identifier> _identifiers = null;
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

        private string _NameCanonical = null;
        public string NameCanonical
        {
            get { return _NameCanonical; }
            set
            {
                if (value != null) value = CalibrateValue(value, 100);
                _NameCanonical = value;
            }
        }

        private string _creationDate = null;
        public string CreationDate
        {
            get { return _creationDate; }
            set { _creationDate = value; }
        }

        List<Title> _Titles;
        public List<Title> Titles
        {
            get { return _Titles; }
            set { _Titles = value; }
        }

        #endregion Properties
    }
}
