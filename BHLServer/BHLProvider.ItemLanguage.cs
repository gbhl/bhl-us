using System;
using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;
using CustomDataAccess;

namespace MOBOT.BHL.Server
{
    public partial class BHLProvider
    {
        public CustomGenericList<ItemLanguage> ItemLanguageSelectByItemID(int itemID)
        {
            return new ItemLanguageDAL().ItemLanguageSelectByItemID(null, null, itemID);
        }
    }
}
