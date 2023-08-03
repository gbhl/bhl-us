using MOBOT.BHLImport.DAL;
using MOBOT.BHLImport.DataObjects;
using System;
using System.Collections.Generic;

namespace MOBOT.BHLImport.Server
{
    public partial class BHLImportProvider
    {
        public DateTime BotanicusHarvestLogSelectLatestEndDate()
        {
            return (new BotanicusHarvestLogDAL().BotanicusHarvestLogSelectLatestEndDate(null, null));
        }

        public BotanicusHarvestLog SaveBotanicusHarvestLog(DateTime harvestStartDate,
            DateTime harvestEndDate, bool automaticHarvest, bool successfulHarvest,
            int title, int titleTag, int titleCreator, int creator, int item, int page, 
            int indicatedPage, int pagePageType, int pageName)
        {
            BotanicusHarvestLogDAL dal = new BotanicusHarvestLogDAL();
            BotanicusHarvestLog newLog = new BotanicusHarvestLog
            {
                HarvestStartDate = harvestStartDate,
                HarvestEndDate = harvestEndDate,
                AutomaticHarvest = automaticHarvest,
                SuccessfulHarvest = successfulHarvest,
                Title = title,
                TitleTag = titleTag,
                TitleCreator = titleCreator,
                Creator = creator,
                Item = item,
                Page = page,
                IndicatedPage = indicatedPage,
                PagePageType = pagePageType,
                PageName = pageName
            };
            BotanicusHarvestLog savedLog = dal.BotanicusHarvestLogInsertAuto(null, null, newLog);
            return savedLog;
        }

        public List<BotanicusHarvestLog> BotanicusHarvestLogSelectRecent(int numLogs)
        {
            return (new BotanicusHarvestLogDAL().BotanicusHarvestLogSelectRecent(null, null, numLogs));
        }
    }
}
