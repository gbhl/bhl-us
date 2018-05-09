using System;
using CustomDataAccess;

namespace MOBOT.BHL.API.BHLApiDataObjects3
{ 
    [Serializable]
    public class Collection : DataObjectBase, ISetValues
    {
        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Collection()
        {
        }

        #endregion Constructors

        #region Properties

        private string _collectionID;
        public string CollectionID
        {
            get { return _collectionID; }
            set { _collectionID = value; }
        }

        private string _collectionName = null;
        public string CollectionName
        {
            get { return _collectionName; }
            set
            {
                if (value != null) value = CalibrateValue(value, 50);
                _collectionName = value;
            }
        }

        private string _collectionDescription = null;
        public string CollectionDescription
        {
            get { return _collectionDescription; }
            set
            {
                if (value != null) value = CalibrateValue(value, 100);
                _collectionDescription = value;
            }
        }

        private string _canContainTitles;
        public string CanContainTitles
        {
            get { return _canContainTitles; }
            set { _canContainTitles = value; }
        }

        private string _canContainItems;
        public string CanContainItems
        {
            get { return _canContainItems; }
            set { _canContainItems = value; }
        }

        #endregion


        #region ISetValues Members

        public void SetValues(CustomDataRow row)
        {
            foreach (CustomDataColumn column in row)
            {
                switch (column.Name)
                {
                    case "CollectionID":
                        {
                            _collectionID = column.Value.ToString();
                            break;
                        }
                    case "CollectionName":
                        {
                            _collectionName = (string)column.Value;
                            break;
                        }
                    case "CollectionDescription":
                        {
                            _collectionDescription = (string)column.Value;
                            break;
                        }
                    case "CanContainTitles":
                        {
                            _canContainTitles = column.Value.ToString();
                            break;
                        }
                    case "CanContainItems":
                        {
                            _canContainItems = column.Value.ToString();
                            break;
                        }
                }
            }
        }

        #endregion
    }
}
