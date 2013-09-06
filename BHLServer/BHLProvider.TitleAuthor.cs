using System;
using CustomDataAccess;
using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;

namespace MOBOT.BHL.Server
{
    public partial class BHLProvider
    {
        public CustomGenericList<TitleAuthor> TitleAuthorSelectByTitle(int titleID)
        {
            return new TitleAuthorDAL().TitleAuthorSelectByTitle(null, null, titleID);
        }
    }
}
