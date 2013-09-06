
#region Using

using System;
using System.Data;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHL.DataObjects;

#endregion Using

namespace MOBOT.BHL.DAL
{
	public partial class MonthlyStatsDAL
	{
        public CustomGenericList<MonthlyStats> MonthlyStatsSelectByStatType(SqlConnection sqlConnection, SqlTransaction sqlTransaction, 
            string statType, string institutionName, bool showMonthly)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("MonthlyStatsSelectByStatType", connection, transaction,
                CustomSqlHelper.CreateInputParameter("StatType", SqlDbType.NVarChar, 100, false, statType),
                CustomSqlHelper.CreateInputParameter("InstitutionName", SqlDbType.NVarChar, 255, false, institutionName),
                CustomSqlHelper.CreateInputParameter("ShowMonthly", SqlDbType.Bit, null, false, showMonthly)))
            {
                using (CustomSqlHelper<MonthlyStats> helper = new CustomSqlHelper<MonthlyStats>())
                {
                    CustomGenericList<MonthlyStats> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }

        public CustomGenericList<MonthlyStats> MonthlyStatsSelectCurrentYearSummary(SqlConnection sqlConnection, SqlTransaction sqlTransaction)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("MonthlyStatsSelectCurrentYearSummary", connection, transaction))
            {
                using (CustomSqlHelper<MonthlyStats> helper = new CustomSqlHelper<MonthlyStats>())
                {
                    CustomGenericList<MonthlyStats> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }

        public CustomGenericList<MonthlyStats> MonthlyStatsSelectCurrentMonthSummary(SqlConnection sqlConnection, SqlTransaction sqlTransaction)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("MonthlyStatsSelectCurrentMonthSummary", connection, transaction))
            {
                using (CustomSqlHelper<MonthlyStats> helper = new CustomSqlHelper<MonthlyStats>())
                {
                    CustomGenericList<MonthlyStats> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }

        public CustomGenericList<MonthlyStats> MonthlyStatsSelectByDateAndInstitution(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            int startYear, int startMonth, int endYear, int endMonth, String institutionName)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("MonthlyStatsSelectByDateAndInstitution", connection, transaction,
                CustomSqlHelper.CreateInputParameter("StartYear", SqlDbType.Int, null, false, startYear),
                CustomSqlHelper.CreateInputParameter("StartMonth", SqlDbType.Int, null, false, startMonth),
                CustomSqlHelper.CreateInputParameter("EndYear", SqlDbType.Int, null, false, endYear),
                CustomSqlHelper.CreateInputParameter("EndMonth", SqlDbType.Int, null, false, endMonth),
                CustomSqlHelper.CreateInputParameter("InstitutionName", SqlDbType.NVarChar, 255, false, institutionName)))
            {
                using (CustomSqlHelper<MonthlyStats> helper = new CustomSqlHelper<MonthlyStats>())
                {
                    CustomGenericList<MonthlyStats> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }

    }
}
