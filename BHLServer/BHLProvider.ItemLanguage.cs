using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;
using System.Collections.Generic;

namespace MOBOT.BHL.Server
{
    public partial class BHLProvider
    {
        public List<ItemLanguage> ItemLanguageSelectByItemID(int itemID)
        {
            return new ItemLanguageDAL().ItemLanguageSelectByItemID(null, null, itemID);
        }
    }
}
