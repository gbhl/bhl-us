
// Generated 5/9/2016 1:52:57 PM
// Do not modify the contents of this code file.
// Interface IMonthlyStatsDAL based upon dbo.MonthlyStats.

#region using

using System;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHL.DataObjects;

#endregion using

namespace MOBOT.BHL.DAL
{
	public interface IMonthlyStatsDAL
	{
		MonthlyStats MonthlyStatsSelectAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			int year,
			int month,
			string institutionName,
			string statType);

		MonthlyStats MonthlyStatsSelectAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName,
			int year,
			int month,
			string institutionName,
			string statType);

		CustomGenericList<CustomDataRow> MonthlyStatsSelectAutoRaw(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			int year,
			int month,
			string institutionName,
			string statType);

		CustomGenericList<CustomDataRow> MonthlyStatsSelectAutoRaw(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName,
			int year,
			int month,
			string institutionName,
			string statType);

		MonthlyStats MonthlyStatsInsertAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			int year,
			int month,
			string institutionName,
			string statType,
			int statValue);

		MonthlyStats MonthlyStatsInsertAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName,
			int year,
			int month,
			string institutionName,
			string statType,
			int statValue);

		MonthlyStats MonthlyStatsInsertAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, MonthlyStats value);

		MonthlyStats MonthlyStatsInsertAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName, MonthlyStats value);

		bool MonthlyStatsDeleteAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			int year,
			int month,
			string institutionName,
			string statType);

		bool MonthlyStatsDeleteAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName,
			int year,
			int month,
			string institutionName,
			string statType);

		MonthlyStats MonthlyStatsUpdateAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			int year,
			int month,
			string institutionName,
			string statType,
			int statValue);

		MonthlyStats MonthlyStatsUpdateAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName,
			int year,
			int month,
			string institutionName,
			string statType,
			int statValue);

		MonthlyStats MonthlyStatsUpdateAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, MonthlyStats value);

		MonthlyStats MonthlyStatsUpdateAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName, MonthlyStats value);

		CustomDataAccessStatus<MonthlyStats> MonthlyStatsManageAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, MonthlyStats value);

		CustomDataAccessStatus<MonthlyStats> MonthlyStatsManageAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName, MonthlyStats value);


	}
}

