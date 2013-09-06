using System;
using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;
using CustomDataAccess;

namespace MOBOT.BHL.Server
{
    public partial class BHLProvider
    {
        public CustomGenericList<TitleLanguage> TitleLanguageSelectByTitleID(int titleID)
        {
            return new TitleLanguageDAL().TitleLanguageSelectByTitleID(null, null, titleID);
        }
    }
}
