using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;

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

        public Stats StatsSelectNames()
        {
            return (new StatsDAL().StatsSelectNames(null, null));
        }

        public Stats StatsSelectUniqueNames()
        {
            return (new StatsDAL().StatsSelectUniqueNames(null, null));
        }

        public Stats StatsSelectVerifiedNames()
        {
            return (new StatsDAL().StatsSelectVerifiedNames(null, null));
        }

        public Stats StatsSelectEOLNames()
        {
            return (new StatsDAL().StatsSelectEOLNames(null, null));
        }

        public Stats StatsSelectEOLPages()
        {
            return (new StatsDAL().StatsSelectEOLPages(null, null));
        }

        public Stats StatsSelectForCollection(int collectionID)
        {
            return new StatsDAL().StatsSelectForCollection(null, null, collectionID);
        }
    }
}

