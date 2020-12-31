using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;
using System.Collections.Generic;

namespace MOBOT.BHL.Server
{
    public partial class BHLProvider
    {
        public List<TitleLanguage> TitleLanguageSelectByTitleID(int titleID)
        {
            return new TitleLanguageDAL().TitleLanguageSelectByTitleID(null, null, titleID);
        }
    }
}
