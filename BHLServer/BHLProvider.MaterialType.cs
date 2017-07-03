using CustomDataAccess;
using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;

namespace MOBOT.BHL.Server
{
    public partial class BHLProvider
    {
        public CustomGenericList<MaterialType> MaterialTypeSelectAll()
        {
            return new MaterialTypeDAL().MaterialTypeSelectAll(null, null);
        }

        public MaterialType MaterialTypeSelect(int materialTypeId)
        {
            return new MaterialTypeDAL().MaterialTypeSelectAuto(null, null, materialTypeId);
        }
    }
}
