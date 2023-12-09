
#region Using

using CustomDataAccess;
using MOBOT.BHLImport.DataObjects;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

#endregion Using

namespace MOBOT.BHLImport.DAL
{
    public partial class StatsDAL
    {
        public List<Stats> StatsSelectReadyForProductionBySource(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            int importSourceID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHLImport"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("StatsSelectReadyForProductionBySource", connection, transaction,
                CustomSqlHelper.CreateInputParameter("ImportSourceID", SqlDbType.Int, null, false, importSourceID)))
            {
                List<CustomDataRow> list = CustomSqlHelper.ExecuteReaderAndReturnRows(command);
                List<Stats> listOfStats = new List<Stats>();
                foreach (CustomDataRow row in list)
                {
                    Stats stats = new Stats
                    {
                        Source = row["Source"].Value.ToString(),
                        Status = row["Status"].Value.ToString(),
                        Type = row["Type"].Value.ToString(),
                        NumberOfItems = (int)row["Number Of Items"].Value
                    };
                    listOfStats.Add(stats);
                }

                return listOfStats;
            }
        }

        public Stats StatsCountIAItemPendingApproval(
        SqlConnection sqlConnection,
        SqlTransaction sqlTransaction,
        int ageInDays)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHLImport"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("StatsCountIAItemPendingApproval", connection, transaction,
                CustomSqlHelper.CreateInputParameter("AgeInDays", SqlDbType.Int, null, false, ageInDays)))
            {
                List<CustomDataRow> list = CustomSqlHelper.ExecuteReaderAndReturnRows(command);
                Stats stats = new Stats
                {
                    NumberOfItems = (int)list[0]["Number Of Items"].Value
                };
                return stats;
            }
        }

        public List<Stats> StatsSelectIAItemGroupByStatus(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHLImport"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("StatsSelectIAItemGroupByStatus", connection, transaction))
            {
                List<CustomDataRow> list = CustomSqlHelper.ExecuteReaderAndReturnRows(command);
                List<Stats> listOfStats = new List<Stats>();
                foreach (CustomDataRow row in list)
                {
                    Stats stats = new Stats
                    {
                        ItemStatusID = (int)row["ItemStatusID"].Value,
                        Status = row["Status"].Value.ToString(),
                        Description = row["Description"].Value.ToString(),
                        NumberOfItems = (int)row["Number Of Items"].Value
                    };
                    listOfStats.Add(stats);
                }

                return listOfStats;
            }
        }

        public List<Stats> StatsSelectBSItemGroupByStatus(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHLImport"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("StatsSelectBSItemGroupByStatus", connection, transaction))
            {
                List<CustomDataRow> list = CustomSqlHelper.ExecuteReaderAndReturnRows(command);
                List<Stats> listOfStats = new List<Stats>();
                foreach (CustomDataRow row in list)
                {
                    Stats stats = new Stats
                    {
                        ItemStatusID = (int)row["ItemStatusID"].Value,
                        Status = row["Status"].Value.ToString(),
                        Description = row["Description"].Value.ToString(),
                        NumberOfItems = (int)row["Number Of Items"].Value
                    };
                    listOfStats.Add(stats);
                }

                return listOfStats;
            }
        }
    }
}
