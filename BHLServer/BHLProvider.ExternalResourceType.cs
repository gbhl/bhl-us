using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;
using System.Collections.Generic;

namespace MOBOT.BHL.Server
{
    public partial class BHLProvider
    {
        public List<ExternalResourceType> ExternalResourceTypeSelectByIDType(string idTypeName)
        {
            return new ExternalResourceTypeDAL().ExternalResourceTypeSelectByIDType(null, null, idTypeName);
        }
    }
}
