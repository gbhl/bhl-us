using CustomDataAccess;
using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;
using System.Collections.Generic;

namespace MOBOT.BHL.Server
{
    public partial class BHLProvider
    {
        public List<Name> NameSelectByNameString(string name)
        {
            return new NameDAL().NameSelectByNameString(null, null, name);
        }

        public Name NameSelectByNameID(int nameId)
        {
            return new NameDAL().NameSelectByNameID(null, null, nameId);
        }

        public List<CustomDataRow> NameMetadataSelectByItemID(int itemId)
        {
            return new NameDAL().NameMetadataSelectByItemID(null, null, itemId);
        }

        public List<NameSourceGNFinder> NameSourceGNFinderSelectAll()
        {
            return new NameSourceGNFinderDAL().NameSourceGNFinderSelectAll(null, null);
        }
    }
}
