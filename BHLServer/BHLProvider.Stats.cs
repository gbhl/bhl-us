using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;
using System.Collections.Generic;

namespace MOBOT.BHL.Server
{
    public partial class BHLProvider
    {
        public Stats StatsSelect()
        {
            return (new StatsDAL().StatsSelect(null, null));
        }

        public Stats StatsSelectExpanded()
        {
            return (new StatsDAL().StatsSelectExpanded(null, null));
        }

        public Stats StatsSelectForCollection(int collectionID)
        {
            return new StatsDAL().StatsSelectForCollection(null, null, collectionID);
        }

        public Stats StatsSelectForInstitution(string institutionCode)
        {
            return new StatsDAL().StatsSelectForInstitution(null, null, institutionCode);
        }

        public List<EntityCount> EntityCountSelectLatest()
        {
            return new StatsDAL().EntityCountSelectLatest(null, null);
        }

        public Stats CurrentStatsSelect()
        {
            return (new StatsDAL().CurrentStatsSelect(null, null));
        }
    }
}

