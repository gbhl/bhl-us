
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

		public ItemStatus( int itemStatusID, string itemStatusName )
			: base( itemStatusID, itemStatusName )
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
