
#region Using

using System;

#endregion Using

namespace MOBOT.BHL.DataObjects
{
	[Serializable]
	public class ItemStatus : __ItemStatus
	{

		public ItemStatus()
		{
		}

		public ItemStatus( int itemStatusID, string itemStatusName, string itemStatusDescription, DateTime creationDate, DateTime lastModifiedDate, int creationUserID, int lastModifiedUserID )
			: base( itemStatusID, itemStatusName, itemStatusDescription, creationDate, lastModifiedDate, creationUserID, lastModifiedUserID  )
		{
		}

		public enum ItemStatusValue : int
		{
			Removed = 5,
			Inappropriate = 10,
			Copyright = 15,
			QA = 20,
			New = 30,
			Published = 40
		}
	}
}
