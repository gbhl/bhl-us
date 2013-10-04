using System;
using System.Collections.Generic;
using CustomDataAccess;
using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;

namespace MOBOT.BHL.Server
{
    public partial class BHLProvider
    {
        public CustomGenericList<Name> NameSelectByNameString(string name)
        {
            return new NameDAL().NameSelectByNameString(null, null, name);
        }

        public Name NameSelectByNameID(int nameId)
        {
            return new NameDAL().NameSelectByNameID(null, null, nameId);
        }

        public CustomGenericList<CustomDataRow> NameMetadataSelectByItemID(int itemId)
        {
            return new NameDAL().NameMetadataSelectByItemID(null, null, itemId);
        }
    }
}
