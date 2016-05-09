
// Generated 5/9/2016 1:52:57 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class MonthlyStatsDAL is based upon dbo.MonthlyStats.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DAL
// {
// 		public partial class MonthlyStatsDAL
//		{
//		}
// }

#endregion How To Implement

#region using

using System;
using System.Data;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHL.DataObjects;

#endregion using

namespace MOBOT.BHL.DAL
{
	partial class MonthlyStatsDAL : IMonthlyStatsDAL
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from dbo.MonthlyStats by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="year"></param>
		/// <param name="month"></param>
		/// <param name="institutionName"></param>
		/// <param name="statType"></param>
		/// <returns>Object of type MonthlyStats.</returns>
		public MonthlyStats MonthlyStatsSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int year,
			int month,
			string institutionName,
			string statType)
		{
			return MonthlyStatsSelectAuto(	sqlConnection, sqlTransaction, "BHL",	year, month, institutionName, statType );
		}
			
		/// <summary>
		/// Select values from dbo.MonthlyStats by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="year"></param>
		/// <param name="month"></param>
		/// <param name="institutionName"></param>
		/// <param name="statType"></param>
		/// <returns>Object of type MonthlyStats.</returns>
		public MonthlyStats MonthlyStatsSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int year,
			int month,
			string institutionName,
			string statType )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("MonthlyStatsSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("Year", SqlDbType.Int, null, false, year),
					CustomSqlHelper.CreateInputParameter("Month", SqlDbType.Int, null, false, month),
					CustomSqlHelper.CreateInputParameter("InstitutionName", SqlDbType.NVarChar, 255, false, institutionName),
					CustomSqlHelper.CreateInputParameter("StatType", SqlDbType.NVarChar, 100, false, statType)))
			{
				using (CustomSqlHelper<MonthlyStats> helper = new CustomSqlHelper<MonthlyStats>())
				{
					CustomGenericList<MonthlyStats> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						MonthlyStats o = list[0];
						list = null;
						return o;
					}
					else
					{
						return null;
					}
				}
			}
		}
		
		/// <summary>
		/// Select values from dbo.MonthlyStats by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="year"></param>
		/// <param name="month"></param>
		/// <param name="institutionName"></param>
		/// <param name="statType"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> MonthlyStatsSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int year,
			int month,
			string institutionName,
			string statType)
		{
			return MonthlyStatsSelectAutoRaw( sqlConnection, sqlTransaction, "BHL", year, month, institutionName, statType );
		}
		
		/// <summary>
		/// Select values from dbo.MonthlyStats by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="year"></param>
		/// <param name="month"></param>
		/// <param name="institutionName"></param>
		/// <param name="statType"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> MonthlyStatsSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int year,
			int month,
			string institutionName,
			string statType)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("MonthlyStatsSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("Year", SqlDbType.Int, null, false, year),
					CustomSqlHelper.CreateInputParameter("Month", SqlDbType.Int, null, false, month),
					CustomSqlHelper.CreateInputParameter("InstitutionName", SqlDbType.NVarChar, 255, false, institutionName),
					CustomSqlHelper.CreateInputParameter("StatType", SqlDbType.NVarChar, 100, false, statType)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====

 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into dbo.MonthlyStats.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="year"></param>
		/// <param name="month"></param>
		/// <param name="institutionName"></param>
		/// <param name="statType"></param>
		/// <param name="statValue"></param>
		/// <returns>Object of type MonthlyStats.</returns>
		public MonthlyStats MonthlyStatsInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int year,
			int month,
			string institutionName,
			string statType,
			int statValue)
		{
			return MonthlyStatsInsertAuto( sqlConnection, sqlTransaction, "BHL", year, month, institutionName, statType, statValue );
		}
		
		/// <summary>
		/// Insert values into dbo.MonthlyStats.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="year"></param>
		/// <param name="month"></param>
		/// <param name="institutionName"></param>
		/// <param name="statType"></param>
		/// <param name="statValue"></param>
		/// <returns>Object of type MonthlyStats.</returns>
		public MonthlyStats MonthlyStatsInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int year,
			int month,
			string institutionName,
			string statType,
			int statValue)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("MonthlyStatsInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("Year", SqlDbType.Int, null, false, year),
					CustomSqlHelper.CreateInputParameter("Month", SqlDbType.Int, null, false, month),
					CustomSqlHelper.CreateInputParameter("InstitutionName", SqlDbType.NVarChar, 255, false, institutionName),
					CustomSqlHelper.CreateInputParameter("StatType", SqlDbType.NVarChar, 100, false, statType),
					CustomSqlHelper.CreateInputParameter("StatValue", SqlDbType.Int, null, false, statValue), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<MonthlyStats> helper = new CustomSqlHelper<MonthlyStats>())
				{
					CustomGenericList<MonthlyStats> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						MonthlyStats o = list[0];
						list = null;
						return o;
					}
					else
					{
						return null;
					}
				}
			}
		}

		/// <summary>
		/// Insert values into dbo.MonthlyStats. Returns an object of type MonthlyStats.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type MonthlyStats.</param>
		/// <returns>Object of type MonthlyStats.</returns>
		public MonthlyStats MonthlyStatsInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			MonthlyStats value)
		{
			return MonthlyStatsInsertAuto(sqlConnection, sqlTransaction, "BHL", value);
		}
		
		/// <summary>
		/// Insert values into dbo.MonthlyStats. Returns an object of type MonthlyStats.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type MonthlyStats.</param>
		/// <returns>Object of type MonthlyStats.</returns>
		public MonthlyStats MonthlyStatsInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			MonthlyStats value)
		{
			return MonthlyStatsInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.Year,
				value.Month,
				value.InstitutionName,
				value.StatType,
				value.StatValue);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from dbo.MonthlyStats by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="year"></param>
		/// <param name="month"></param>
		/// <param name="institutionName"></param>
		/// <param name="statType"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool MonthlyStatsDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int year,
			int month,
			string institutionName,
			string statType)
		{
			return MonthlyStatsDeleteAuto( sqlConnection, sqlTransaction, "BHL", year, month, institutionName, statType );
		}
		
		/// <summary>
		/// Delete values from dbo.MonthlyStats by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="year"></param>
		/// <param name="month"></param>
		/// <param name="institutionName"></param>
		/// <param name="statType"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool MonthlyStatsDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int year,
			int month,
			string institutionName,
			string statType)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("MonthlyStatsDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("Year", SqlDbType.Int, null, false, year),
					CustomSqlHelper.CreateInputParameter("Month", SqlDbType.Int, null, false, month),
					CustomSqlHelper.CreateInputParameter("InstitutionName", SqlDbType.NVarChar, 255, false, institutionName),
					CustomSqlHelper.CreateInputParameter("StatType", SqlDbType.NVarChar, 100, false, statType), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				int returnCode = CustomSqlHelper.ExecuteNonQuery(command, "ReturnCode");
				
				if (transaction == null)
				{
					CustomSqlHelper.CloseConnection(connection);
				}
				
				if (returnCode == 0)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
		}
		
		#endregion ===== DELETE =====

 		#region ===== UPDATE =====

		/// <summary>
		/// Update values in dbo.MonthlyStats. Returns an object of type MonthlyStats.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="year"></param>
		/// <param name="month"></param>
		/// <param name="institutionName"></param>
		/// <param name="statType"></param>
		/// <param name="statValue"></param>
		/// <returns>Object of type MonthlyStats.</returns>
		public MonthlyStats MonthlyStatsUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int year,
			int month,
			string institutionName,
			string statType,
			int statValue)
		{
			return MonthlyStatsUpdateAuto( sqlConnection, sqlTransaction, "BHL", year, month, institutionName, statType, statValue);
		}
		
		/// <summary>
		/// Update values in dbo.MonthlyStats. Returns an object of type MonthlyStats.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="year"></param>
		/// <param name="month"></param>
		/// <param name="institutionName"></param>
		/// <param name="statType"></param>
		/// <param name="statValue"></param>
		/// <returns>Object of type MonthlyStats.</returns>
		public MonthlyStats MonthlyStatsUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int year,
			int month,
			string institutionName,
			string statType,
			int statValue)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("MonthlyStatsUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("Year", SqlDbType.Int, null, false, year),
					CustomSqlHelper.CreateInputParameter("Month", SqlDbType.Int, null, false, month),
					CustomSqlHelper.CreateInputParameter("InstitutionName", SqlDbType.NVarChar, 255, false, institutionName),
					CustomSqlHelper.CreateInputParameter("StatType", SqlDbType.NVarChar, 100, false, statType),
					CustomSqlHelper.CreateInputParameter("StatValue", SqlDbType.Int, null, false, statValue), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<MonthlyStats> helper = new CustomSqlHelper<MonthlyStats>())
				{
					CustomGenericList<MonthlyStats> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						MonthlyStats o = list[0];
						list = null;
						return o;
					}
					else
					{
						return null;
					}
				}
			}
		}
		
		/// <summary>
		/// Update values in dbo.MonthlyStats. Returns an object of type MonthlyStats.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type MonthlyStats.</param>
		/// <returns>Object of type MonthlyStats.</returns>
		public MonthlyStats MonthlyStatsUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			MonthlyStats value)
		{
			return MonthlyStatsUpdateAuto(sqlConnection, sqlTransaction, "BHL", value );
		}
		
		/// <summary>
		/// Update values in dbo.MonthlyStats. Returns an object of type MonthlyStats.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type MonthlyStats.</param>
		/// <returns>Object of type MonthlyStats.</returns>
		public MonthlyStats MonthlyStatsUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			MonthlyStats value)
		{
			return MonthlyStatsUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.Year,
				value.Month,
				value.InstitutionName,
				value.StatType,
				value.StatValue);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage dbo.MonthlyStats object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.MonthlyStats.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type MonthlyStats.</param>
		/// <returns>Object of type CustomDataAccessStatus<MonthlyStats>.</returns>
		public CustomDataAccessStatus<MonthlyStats> MonthlyStatsManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			MonthlyStats value  )
		{
			return MonthlyStatsManageAuto( sqlConnection, sqlTransaction, "BHL", value  );
		}
		
		/// <summary>
		/// Manage dbo.MonthlyStats object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.MonthlyStats.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type MonthlyStats.</param>
		/// <returns>Object of type CustomDataAccessStatus<MonthlyStats>.</returns>
		public CustomDataAccessStatus<MonthlyStats> MonthlyStatsManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			MonthlyStats value  )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				
				
				MonthlyStats returnValue = MonthlyStatsInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.Year,
						value.Month,
						value.InstitutionName,
						value.StatType,
						value.StatValue);
				
				return new CustomDataAccessStatus<MonthlyStats>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (MonthlyStatsDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.Year,
						value.Month,
						value.InstitutionName,
						value.StatType))
				{
				return new CustomDataAccessStatus<MonthlyStats>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<MonthlyStats>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				
				MonthlyStats returnValue = MonthlyStatsUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.Year,
						value.Month,
						value.InstitutionName,
						value.StatType,
						value.StatValue);
					
				return new CustomDataAccessStatus<MonthlyStats>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<MonthlyStats>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}

