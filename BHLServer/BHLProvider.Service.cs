using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;
using System.Collections.Generic;

namespace MOBOT.BHL.Server
{
    public partial class BHLProvider
    {
        public List<Service> ServiceSelectAll()
        {
            return new ServiceDAL().ServiceSelectAll(null, null);
        }
    }
}
