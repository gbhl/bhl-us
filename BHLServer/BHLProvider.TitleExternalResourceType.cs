using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;
using System.Collections.Generic;

namespace MOBOT.BHL.Server
{
    public partial class BHLProvider
    {
        public List<TitleExternalResourceType> TitleExternalResourceTypeSelectAll()
        {
            return new TitleExternalResourceTypeDAL().TitleExternalResourceTypeSelectAll(null, null);
        }

    }
}
