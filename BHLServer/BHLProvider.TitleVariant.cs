using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;
using System.Collections.Generic;

namespace MOBOT.BHL.Server
{
    public partial class BHLProvider
    {
        public List<TitleVariant> TitleVariantSelectByTitleID(int titleID)
        {
            return new TitleVariantDAL().TitleVariantSelectByTitleID(null, null, titleID);
        }

        public List<TitleVariantType> TitleVariantTypeSelectAll()
        {
            return new TitleVariantDAL().TitleVariantTypeSelectAll(null, null);
        }
    }
}
