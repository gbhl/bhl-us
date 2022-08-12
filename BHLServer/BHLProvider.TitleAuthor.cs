using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;
using System.Collections.Generic;

namespace MOBOT.BHL.Server
{
    public partial class BHLProvider
    {
        public List<TitleAuthor> TitleAuthorSelectByTitle(int titleID)
        {
            return new TitleAuthorDAL().TitleAuthorSelectByTitle(null, null, titleID);
        }
    }
}
