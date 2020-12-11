using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;
using System.Collections.Generic;

namespace MOBOT.BHL.Server
{
    public partial class BHLProvider
    {
		public List<ItemRelationship> ItemRelationshipSelectByItemID(int itemID)
		{
			return new ItemRelationshipDAL().ItemRelationshipSelectByItemID(null, null, itemID);
		}
	}
}
