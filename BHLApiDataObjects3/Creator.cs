using System;
using System.Collections.Generic;
using CustomDataAccess;

namespace MOBOT.BHL.API.BHLApiDataObjects3
{
    [Serializable]
    public class Author : DataObjectBase, ISetValues
    {
        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Author()
        {
        }

        #endregion Constructors

        #region Properties

        private string _AuthorID = null;
        public string AuthorID
        {
            get { return _AuthorID; }
            set { _AuthorID = value; }
        }

        private string _Name = null;
        public string Name
        {
            get { return _Name; }
            set
            {
                if (value != null) value = CalibrateValue(value, 450);
                _Name = value;
            }
        }

        private string _Role = null;
        public string Role
        {
            get { return _Role; }
            set
            {
                if (value != null) value = CalibrateValue(value, 255);
                _Role = value;
            }
        }

        // Applies to personal names
        private string _Numeration = null;
        public string Numeration
        {
            get { return _Numeration; }
            set
            {
                if (value != null) value = CalibrateValue(value, 450);
                _Numeration = value;
            }
        }

        // Applies to corporate authors
        private string _Unit = null;
        public string Unit
        {
            get { return _Unit; }
            set
            {
                if (value != null) value = CalibrateValue(value, 450);
                _Unit = value;
            }
        }

        // Applies to personal names
        private string _Title = null;
        public string Title
        {
            get { return _Title; }
            set
            {
                if (value != null) value = CalibrateValue(value, 450);
                _Title = value;
            }
        }

        // Applies to corporate authors
        private string _Location = null;
        public string Location
        {
            get { return _Location; }
            set
            {
                if (value != null) value = CalibrateValue(value, 450);
                _Location = value;
            }
        }

        // Applies to personal names
        private string _FullerForm = null;
        public string FullerForm
        {
            get { return _FullerForm; }
            set { _FullerForm = value; }
        }

        private string _relationship = null;
        public string Relationship
        {
            get { return _relationship; }
            set { _relationship = value; }
        }

        private string _titleOfWork = null;
        public string TitleOfWork
        {
            get { return _titleOfWork; }
            set { _titleOfWork = value; }
        }

        private string _Dates = null;
        public string Dates
        {
            get { return _Dates; }
            set
            {
                if (value != null) value = CalibrateValue(value, 450);
                _Dates = value;
            }
        }

        private string _CreatorUrl = null;
        public string CreatorUrl
        {
            get { return _CreatorUrl; }
            set { _CreatorUrl = value; }
        }

        private List<Identifier> _identifiers = null;
        public List<Identifier> Identifiers
        {
            get { return _identifiers; }
            set { _identifiers = value; }
        }

        private List<Publication> _publications = null;
        public List<Publication> Publications
        {
            get { return _publications; }
            set { _publications = value; }
        }

        #endregion Properties

        #region ISetValues Members

        public void SetValues(CustomDataRow row)
        {
            foreach (CustomDataColumn column in row)
            {
                switch (column.Name)
                {
                    case "AuthorID":
                        {
                            _AuthorID = column.Value.ToString();
                            break;
                        }
                    case "FullName":
                        {
                            _Name = (string)column.Value;
                            break;
                        }
                    case "RoleDescription":
                        {
                            _Role = Utility.NullIfEmpty(column.Value);
                            break;
                        }
                    case "Numeration":
                        {
                            _Numeration = Utility.NullIfEmpty(column.Value);
                            break;
                        }
                    case "Unit":
                        {
                            _Unit = Utility.NullIfEmpty(column.Value);
                            break;
                        }
                    case "Title":
                        {
                            _Title = Utility.NullIfEmpty(column.Value);
                            break;
                        }
                    case "Location":
                        {
                            _Location = Utility.NullIfEmpty(column.Value);
                            break;
                        }
                    case "FullerForm":
                        {
                            _FullerForm = Utility.NullIfEmpty(column.Value);
                            break;
                        }
                    case "Relationship":
                        {
                            _relationship = Utility.NullIfEmpty(column.Value);
                            break;
                        }
                    case "TitleOfWork":
                        {
                            _titleOfWork = Utility.NullIfEmpty(column.Value);
                            break;
                        }
                    case "Dates":
                        {
                            _Dates = Utility.NullIfEmpty(column.Value);
                            break;
                        }
                }
            }
        }

        #endregion
    }
}
