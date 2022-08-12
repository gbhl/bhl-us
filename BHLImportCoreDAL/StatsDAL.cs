
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
                    Stats stats = new Stats();
                    stats.Source = row["Source"].Value.ToString();
                    stats.Status = row["Status"].Value.ToString();
                    stats.Type = row["Type"].Value.ToString();
                    stats.NumberOfItems = (int)row["Number Of Items"].Value;
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
                Stats stats = new Stats();
                stats.NumberOfItems = (int)list[0]["Number Of Items"].Value;
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
                    Stats stats = new Stats();
                    stats.ItemStatusID = (int)row["ItemStatusID"].Value;
                    stats.Status = row["Status"].Value.ToString();
                    stats.Description = row["Description"].Value.ToString();
                    stats.NumberOfItems = (int)row["Number Of Items"].Value;
                    listOfStats.Add(stats);
                }

                return listOfStats;
            }
        }

        public List<Stats> StatsSelectIAItemPendingApprovalGroupByAge(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            int ageInDays)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHLImport"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("StatsSelectIAItemPendingApprovalGroupByAge", connection, transaction,
                CustomSqlHelper.CreateInputParameter("AgeInDays", SqlDbType.Int, null, false, ageInDays)))
            {
                List<CustomDataRow> list = CustomSqlHelper.ExecuteReaderAndReturnRows(command);
                List<Stats> listOfStats = new List<Stats>();
                foreach (CustomDataRow row in list)
                {
                    Stats stats = new Stats();
                    stats.AgeInDays = (int)row["Age In Days"].Value;
                    stats.NumberOfItems = (int)row["Number Of Items"].Value;
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
                    Stats stats = new Stats();
                    stats.ItemStatusID = (int)row["ItemStatusID"].Value;
                    stats.Status = row["Status"].Value.ToString();
                    stats.Description = row["Description"].Value.ToString();
                    stats.NumberOfItems = (int)row["Number Of Items"].Value;
                    listOfStats.Add(stats);
                }

                return listOfStats;
            }
        }
    }
}
