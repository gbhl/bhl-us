using CustomDataAccess;
using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;

namespace MOBOT.BHL.Server
{
    public partial class BHLProvider
    {
        public CustomGenericList<AuthorType> AuthorTypeSelectAll()
        {
            return new AuthorTypeDAL().AuthorTypeSelectAll(null, null);
        }
    }
}
