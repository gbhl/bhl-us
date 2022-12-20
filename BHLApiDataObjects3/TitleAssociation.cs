using System;
using CustomDataAccess;

namespace MOBOT.BHL.API.BHLApiDataObjects3
{
    [Serializable]
    public class TitleAssociation : DataObjectBase, ISetValues
    {
        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public TitleAssociation()
        {
        }

        #endregion Constructors

        #region Properties

        private string _titleAssociationTypeName = null;
        public string TitleAssociationTypeName
        {
            get { return _titleAssociationTypeName; }
            set
            {
                if (value != null) value = CalibrateValue(value, 30);
                _titleAssociationTypeName = value;
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

        private int? _associatedTitleID = null;
        public int? AssociatedTitleID
        {
            get { return _associatedTitleID; }
            set { _associatedTitleID = value; }
        }

        #endregion

        #region ISetValues Members

        public void SetValues(CustomDataRow row)
        {
            foreach (CustomDataColumn column in row)
            {
                switch (column.Name)
                {
                    case "TitleAssociationTypeName":
                        {
                            _titleAssociationTypeName = (string)column.Value;
                            break;
                        }
                    case "Title":
                        {
                            _title = (string)column.Value;
                            break;
                        }
                    case "AssociatedTitleID":
                        {
                            _associatedTitleID = (int?)column.Value;
                            break;
                        }
                }
            }
        }

        #endregion
    }
}
