using CustomDataAccess;
using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;

namespace MOBOT.BHL.Server
{
    public partial class BHLProvider
    {
        public CustomGenericList<TitleInstitution> TitleSelectWithExternalContentProvider()
        {
            return new TitleInstitutionDAL().TitleSelectWithExternalContentProvider(null, null);
        }
    }
}
