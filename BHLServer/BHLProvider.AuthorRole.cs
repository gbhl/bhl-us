using CustomDataAccess;
using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;

namespace MOBOT.BHL.Server
{
    public partial class BHLProvider
    {
        public AuthorRole AuthorRoleSelectAuto(int authorRoleId)
        {
            return new AuthorRoleDAL().AuthorRoleSelectAuto(null, null, authorRoleId);
        }

        public CustomGenericList<AuthorRole> AuthorRoleSelectAll()
        {
            return new AuthorRoleDAL().AuthorRoleSelectAll(null, null);
        }
    }
}
