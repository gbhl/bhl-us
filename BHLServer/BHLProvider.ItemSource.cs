using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;
using System.Collections.Generic;

namespace MOBOT.BHL.Server
{
    public partial class BHLProvider
    {
        public List<ItemSource> ItemSourceSelectAll()
        {
            return (new ItemSourceDAL().ItemSourceSelectAll(null, null));
        }
    }
}
