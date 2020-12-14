
#region Using

using System;

#endregion Using

namespace MOBOT.BHL.DataObjects
{
	[Serializable]
	public class PageType : __PageType
	{
		public PageType()
		{
		}

		public PageType( int pageTypeID, string pageTypeName, string pageTypeDescription, byte active, DateTime? creationDate, DateTime? lastModifiedDate, int? creationUserId, int? lastModifiedUserId )
			: base( pageTypeID, pageTypeName, pageTypeDescription, active, creationDate, lastModifiedDate, creationUserId, lastModifiedUserId )
		{
		}
	}
}
