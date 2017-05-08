
#region Using

using CustomDataAccess;
using System;
using System.Data;

#endregion Using

namespace MOBOT.BHLImport.DataObjects
{
	[Serializable]
	public class PageFlickrNote : __PageFlickrNote
	{
        // Overriding the default generated definition of this field, because we need to be able to 
        // pass it to a Web Service.  (See the base class for more information about why this does
        // not work with the default implementation.)
        private int _PageFlickrNoteID = default(int);

        [ColumnDefinition("PageFlickrNoteID", DbTargetType = SqlDbType.Int, Ordinal = 1, NumericPrecision = 10, IsAutoKey = true, IsInPrimaryKey = true)]
        public new int PageFlickrNoteID
        {
            get
            {
                return _PageFlickrNoteID;
            }
            set
            {
                if (_PageFlickrNoteID != value)
                {
                    _PageFlickrNoteID = value;
                    _IsDirty = true;
                }
            }
        }

        public override void SetValues(CustomDataRow row)
        {
            base.SetValues(row);

            foreach (CustomDataColumn column in row)
            {
                if (column.Name == "PageFlickrNoteID")
                {
                    _PageFlickrNoteID = (int)column.Value;
                    break;
                }
            }

            IsNew = false;
        }

    }
}

