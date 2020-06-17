using CustomDataAccess;
using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;
using System.Collections.Generic;

namespace MOBOT.BHL.Server
{
    public partial class BHLProvider
    {
        public List<Identifier> IdentifierSelectAll()
        {
            return (new IdentifierDAL().IdentifierSelectAll(null, null));
        }

        public List<Identifier> IdentifierSelectByIDType(string idTypeName)
        {
            return (new IdentifierDAL().IdentifierSelectByIDType(null, null, idTypeName));
        }

        public Identifier IdentifierSelectByGNFinderDataSource(int gnDataSourceID)
        {
            return (new IdentifierDAL().IdentifierSelectByGNFinderDataSource(null, null, gnDataSourceID));
        }
    }
}
