
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
	}
}
