using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;
using System.Collections.Generic;

namespace MOBOT.BHL.Server
{
    public partial class BHLProvider
    {
        public List<TitleExternalResource> TitleExternalResourceSelectByTitleID(int titleID)
        {
            return new TitleExternalResourceDAL().TitleExternalResourceSelectByTitleID(null, null, titleID);
        }
    }
}
