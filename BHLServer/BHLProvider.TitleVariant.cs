using System;
using CustomDataAccess;
using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;

namespace MOBOT.BHL.Server
{
    public partial class BHLProvider
    {
        public CustomGenericList<TitleVariant> TitleVariantSelectByTitleID(int titleID)
        {
            return new TitleVariantDAL().TitleVariantSelectByTitleID(null, null, titleID);
        }

        public CustomGenericList<TitleVariantType> TitleVariantTypeSelectAll()
        {
            return new TitleVariantDAL().TitleVariantTypeSelectAll(null, null);
        }
    }
}
