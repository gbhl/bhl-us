
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

		public PageType( int pageTypeID, string pageTypeName, string pageTypeDescription )
			: base( pageTypeID, pageTypeName, pageTypeDescription )
		{
		}
	}
}
