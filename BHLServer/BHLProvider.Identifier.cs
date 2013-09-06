using CustomDataAccess;
using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;

namespace MOBOT.BHL.Server
{
    public partial class BHLProvider
    {
        public CustomGenericList<Identifier> IdentifierSelectAll()
        {
            return (new IdentifierDAL().IdentifierSelectAll(null, null));
        }
    }
}
