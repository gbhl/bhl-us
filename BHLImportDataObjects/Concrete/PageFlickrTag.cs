
#region Using

using CustomDataAccess;
using System;
using System.Data;

#endregion Using

namespace MOBOT.BHLImport.DataObjects
{
    [Serializable]
	public class PageFlickrTag : __PageFlickrTag
	{
        // Overriding the default generated definition of this field, because we need to be able to 
        // pass it to a Web Service.  (See the base class for more information about why this does
        // not work with the default implementation.)
        private int _PageFlickrTagID = default(int);

        [ColumnDefinition("PageFlickrTagID", DbTargetType = SqlDbType.Int, Ordinal = 1, NumericPrecision = 10, IsAutoKey = true, IsInPrimaryKey = true)]
        public new int PageFlickrTagID
        {
            get
            {
                return _PageFlickrTagID;
            }
            set
            {
                if (_PageFlickrTagID != value)
                {
                    _PageFlickrTagID = value;
                    _IsDirty = true;
                }
            }
        }

        public override void SetValues(CustomDataRow row)
        {
            base.SetValues(row);

            foreach (CustomDataColumn column in row)
            {
                if (column.Name == "PageFlickrTagID")
                {
                    _PageFlickrTagID = (int)column.Value;
                    break;
                }
            }

            IsNew = false;
        }

    }
}
