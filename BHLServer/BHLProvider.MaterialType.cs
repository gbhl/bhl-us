using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;
using System.Collections.Generic;

namespace MOBOT.BHL.Server
{
    public partial class BHLProvider
    {
        public List<MaterialType> MaterialTypeSelectAll()
        {
            return new MaterialTypeDAL().MaterialTypeSelectAll(null, null);
        }

        public MaterialType MaterialTypeSelect(int materialTypeId)
        {
            return new MaterialTypeDAL().MaterialTypeSelectAuto(null, null, materialTypeId);
        }
    }
}
