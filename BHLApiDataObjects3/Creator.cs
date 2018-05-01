using System;
using CustomDataAccess;

namespace MOBOT.BHL.API.BHLApiDataObjects3
{
    [Serializable]
    public class Creator : DataObjectBase, ISetValues
    {
        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Creator()
        {
        }

        #endregion Constructors

        #region Properties

        private int _CreatorID = default(int);
        public int CreatorID
        {
            get { return _CreatorID; }
            set { _CreatorID = value; }
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
                            _CreatorID = (int)column.Value;
                            break;
                        }
                    case "FullName":
                        {
                            _Name = (string)column.Value;
                            break;
                        }
                    case "RoleDescription":
                        {
                            _Role = (string)column.Value;
                            break;
                        }
                    case "Numeration":
                        {
                            _Numeration = (string)column.Value;
                            break;
                        }
                    case "Unit":
                        {
                            _Unit = (string)column.Value;
                            break;
                        }
                    case "Title":
                        {
                            _Title = (string)column.Value;
                            break;
                        }
                    case "Location":
                        {
                            _Location = (string)column.Value;
                            break;
                        }
                    case "FullerForm":
                        {
                            _FullerForm = (string)column.Value;
                            break;
                        }
                    case "Relationship":
                        {
                            _relationship = (string)column.Value;
                            break;
                        }
                    case "TitleOfWork":
                        {
                            _titleOfWork = (string)column.Value;
                            break;
                        }
                    case "Dates":
                        {
                            _Dates = (string)column.Value;
                            break;
                        }
                }
            }
        }

        #endregion
    }
}
