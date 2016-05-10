
#region using

using System;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHL.DataObjects;

#endregion using

namespace MOBOT.BHL.DAL
{
	public interface IStatsDAL
	{
        Stats StatsSelect(SqlConnection sqlConnection, SqlTransaction sqlTransaction);

        Stats StatsSelectExpanded(SqlConnection sqlConnection, SqlTransaction sqlTransaction);

        Stats StatsSelectNames(SqlConnection sqlConnection, SqlTransaction sqlTransaction);

        Stats StatsSelectUniqueNames(SqlConnection sqlConnection, SqlTransaction sqlTransaction);

        Stats StatsSelectVerifiedNames(SqlConnection sqlConnection, SqlTransaction sqlTransaction);

        Stats StatsSelectEOLNames(SqlConnection sqlConnection, SqlTransaction sqlTransaction);

        Stats StatsSelectEOLPages(SqlConnection sqlConnection, SqlTransaction sqlTransaction);

        Stats StatsSelectForCollection(SqlConnection sqlConnection, SqlTransaction sqlTransaction, 
            int collectionID);

        Stats StatsSelectForInstitution(SqlConnection sqlConnection, SqlTransaction sqlTransaction, 
            string institutionCode);

        CustomGenericList<EntityCount> EntityCountSelectLatest(SqlConnection sqlConnection, SqlTransaction sqlTransaction);
    }
}

