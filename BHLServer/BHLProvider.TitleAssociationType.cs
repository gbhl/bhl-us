using CustomDataAccess;
using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;

namespace MOBOT.BHL.Server
{
    public partial class BHLProvider
    {
        public CustomGenericList<TitleAssociationType> TitleAssociationTypeSelectAll()
        {
            return new TitleAssociationTypeDAL().SelectAll(null, null);
        }

    }
}
