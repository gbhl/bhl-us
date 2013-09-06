using System;
using CustomDataAccess;

namespace MOBOT.BHL.API.BHLApiDataObjects2
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

        private int _collectionID;
        public int CollectionID
        {
            get { return _collectionID; }
            set { _collectionID = value; }
        }

        private string _collectionName = null;
        public string CollectionName
        {
            get { return _collectionName; }
            set {
                if (value != null) value = CalibrateValue(value, 50);
                _collectionName = value;
            }
        }

        private string _collectionDescription = null;
        public string CollectionDescription
        {
            get { return _collectionDescription; }
            set {
                if (value != null) value = CalibrateValue(value, 100);
                _collectionDescription = value;
            }
        }

        private Int16? _canContainTitles;
        public Int16? CanContainTitles
        {
            get { return _canContainTitles; }
            set { _canContainTitles = value; }
        }

        private Int16? _canContainItems;
        public Int16? CanContainItems
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
                            _collectionID = (int)column.Value;
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
                            _canContainTitles = (Int16)column.Value;
                            break;
                        }
                    case "CanContainItems":
                        {
                            _canContainItems= (Int16)column.Value;
                            break;
                        }
                }
            }
        }

        #endregion
    }
}
