using System;
using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;
using CustomDataAccess;

namespace MOBOT.BHL.Server
{
    public partial class BHLProvider
    {
        public CustomGenericList<ItemSource> ItemSourceSelectAll()
        {
            return (new ItemSourceDAL().ItemSourceSelectAll(null, null));
        }
    }
}
