
#region Using

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHL.DataObjects;

#endregion Using

namespace MOBOT.BHL.DAL
{
	public partial class MonthlyStatsDAL
	{
        public List<MonthlyStats> MonthlyStatsSelectSummary(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            int year, int month)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("MonthlyStatsSelectSummary", connection, transaction,
                CustomSqlHelper.CreateInputParameter("Year", SqlDbType.Int, null, false, year),
                CustomSqlHelper.CreateInputParameter("Month", SqlDbType.Int, null, false, month)))
            {
                using (CustomSqlHelper<MonthlyStats> helper = new CustomSqlHelper<MonthlyStats>())
                {
                    List<MonthlyStats> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }

        public List<MonthlyStats> MonthlyStatsSelectByDateAndInstitution(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            int startYear, int startMonth, int endYear, int endMonth, String institutionCode)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("MonthlyStatsSelectByDateAndInstitution", connection, transaction,
                CustomSqlHelper.CreateInputParameter("StartYear", SqlDbType.Int, null, false, startYear),
                CustomSqlHelper.CreateInputParameter("StartMonth", SqlDbType.Int, null, false, startMonth),
                CustomSqlHelper.CreateInputParameter("EndYear", SqlDbType.Int, null, false, endYear),
                CustomSqlHelper.CreateInputParameter("EndMonth", SqlDbType.Int, null, false, endMonth),
                CustomSqlHelper.CreateInputParameter("InstitutionCode", SqlDbType.NVarChar, 10, false, institutionCode)))
            {
                using (CustomSqlHelper<MonthlyStats> helper = new CustomSqlHelper<MonthlyStats>())
                {
                    List<MonthlyStats> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }

        public List<MonthlyStats> MonthlyStatsSelectByInstitution(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            string institutionCode)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("MonthlyStatsSelectByInstitution", connection, transaction,
                CustomSqlHelper.CreateInputParameter("InstitutionCode", SqlDbType.NVarChar, 10, false, institutionCode)))
            {
                using (CustomSqlHelper<MonthlyStats> helper = new CustomSqlHelper<MonthlyStats>())
                {
                    List<MonthlyStats> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }

        public List<MonthlyStats> MonthlyStatsSelectDetailedForGroup(SqlConnection sqlConnection, SqlTransaction sqlTransaction)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("MonthlyStatsSelectDetailedForGroup", connection, transaction))
            {
                using (CustomSqlHelper<MonthlyStats> helper = new CustomSqlHelper<MonthlyStats>())
                {
                    List<MonthlyStats> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }

        public List<MonthlyStats> MonthlyStatsSelectDetailed(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            bool bhlMemberLibraryOnly)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("MonthlyStatsSelectDetailed", connection, transaction,
                CustomSqlHelper.CreateInputParameter("BHLMemberLibraryOnly", SqlDbType.Bit, null, false, bhlMemberLibraryOnly)))
            {
                using (CustomSqlHelper<MonthlyStats> helper = new CustomSqlHelper<MonthlyStats>())
                {
                    List<MonthlyStats> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }

        public List<MonthlyStats> MonthlyStatsSelectSummaryStats(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            bool bhlMemberLibraryOnly)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("MonthlyStatsSelectSummaryStats", connection, transaction,
                CustomSqlHelper.CreateInputParameter("BHLMemberLibraryOnly", SqlDbType.Bit, null, false, bhlMemberLibraryOnly)))
            {
                using (CustomSqlHelper<MonthlyStats> helper = new CustomSqlHelper<MonthlyStats>())
                {
                    List<MonthlyStats> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }
    }
}
