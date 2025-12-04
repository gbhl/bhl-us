using MOBOT.BHL.DataObjects;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace MOBOT.BHL.DAL
{
    public interface IStatsDAL
	{
        Stats StatsSelect(SqlConnection sqlConnection, SqlTransaction sqlTransaction);
        Stats StatsSelectExpanded(SqlConnection sqlConnection, SqlTransaction sqlTransaction);
        Stats StatsSelectForCollection(SqlConnection sqlConnection, SqlTransaction sqlTransaction, int collectionID);
        Stats StatsSelectForInstitution(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string institutionCode);
        List<EntityCount> EntityCountSelectLatest(SqlConnection sqlConnection, SqlTransaction sqlTransaction);
        Stats CurrentStatsSelect(SqlConnection sqlConnection, SqlTransaction sqlTransaction);
    }
}

