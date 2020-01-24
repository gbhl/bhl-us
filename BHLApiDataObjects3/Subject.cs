using System;
using System.Collections.Generic;
using CustomDataAccess;

namespace MOBOT.BHL.API.BHLApiDataObjects3
{
    [Serializable]
    public class Subject : DataObjectBase, ISetValues
    {
        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Subject()
        {
        }

        #endregion Constructors

        #region Properties

        private string _SubjectText = null;
        public string SubjectText
        {
            get { return _SubjectText; }
            set
            {
                if (value != null) value = this.CalibrateValue(value, 50);
                _SubjectText = value;
            }
        }

        private List<Publication> _publications = null;
        public List<Publication> Publications
        {
            get { return _publications; }
            set { _publications = value; }
        }

        #endregion

        #region ISetValues Members

        public void SetValues(CustomDataRow row)
        {
            foreach (CustomDataColumn column in row)
            {
                switch (column.Name)
                {
                    case "Keyword":
                        {
                            _SubjectText = (string)column.Value;
                            break;
                        }
                }
            }
        }

        #endregion
    }
}
