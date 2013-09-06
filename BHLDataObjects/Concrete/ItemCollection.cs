using System;
using CustomDataAccess;

namespace MOBOT.BHL.DataObjects
{
	[Serializable]
	public class ItemCollection : __ItemCollection
	{
        private string _collectionName;
        private string _collectionDescription;

        public string CollectionName
        {
            get { return this._collectionName; }
            set { this._collectionName = value; }
        }

        public string CollectionDescription
        {
            get { return this._collectionDescription; }
            set { this._collectionDescription = value; }
        }

		#region Constructors

		public ItemCollection()
		{
		}

        public ItemCollection(int itemCollectionID, int itemID, int collectionID)
			:
		base( itemCollectionID, itemID, collectionID, DateTime.Now )
		{
		}

		#endregion Constructors

		public override void SetValues( CustomDataRow row )
		{
			foreach ( CustomDataColumn column in row )
			{
				switch ( column.Name )
				{
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
				}
			}

			base.SetValues( row );
		}
	}
}
