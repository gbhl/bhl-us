
// Generated 6/10/2011 4:56:25 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class ConfigurationDAL is based upon Configuration.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DAL
// {
// 		public partial class ConfigurationDAL
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
	partial class ConfigurationDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from Configuration by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="configurationID"></param>
		/// <returns>Object of type Configuration.</returns>
		public Configuration ConfigurationSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int configurationID)
		{
			return ConfigurationSelectAuto(	sqlConnection, sqlTransaction, "BHL",	configurationID );
		}
			
		/// <summary>
		/// Select values from Configuration by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="configurationID"></param>
		/// <returns>Object of type Configuration.</returns>
		public Configuration ConfigurationSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int configurationID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("ConfigurationSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("ConfigurationID", SqlDbType.Int, null, false, configurationID)))
			{
				using (CustomSqlHelper<Configuration> helper = new CustomSqlHelper<Configuration>())
				{
					CustomGenericList<Configuration> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						Configuration o = list[0];
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
		/// Select values from Configuration by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="configurationID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> ConfigurationSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int configurationID)
		{
			return ConfigurationSelectAutoRaw( sqlConnection, sqlTransaction, "BHL", configurationID );
		}
		
		/// <summary>
		/// Select values from Configuration by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="configurationID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> ConfigurationSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int configurationID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("ConfigurationSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("ConfigurationID", SqlDbType.Int, null, false, configurationID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====
	
 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into Configuration.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="configurationID"></param>
		/// <param name="configurationName"></param>
		/// <param name="configurationValue"></param>
		/// <returns>Object of type Configuration.</returns>
		public Configuration ConfigurationInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int configurationID,
			string configurationName,
			string configurationValue)
		{
			return ConfigurationInsertAuto( sqlConnection, sqlTransaction, "BHL", configurationID, configurationName, configurationValue );
		}
		
		/// <summary>
		/// Insert values into Configuration.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="configurationID"></param>
		/// <param name="configurationName"></param>
		/// <param name="configurationValue"></param>
		/// <returns>Object of type Configuration.</returns>
		public Configuration ConfigurationInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int configurationID,
			string configurationName,
			string configurationValue)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("ConfigurationInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("ConfigurationID", SqlDbType.Int, null, false, configurationID),
					CustomSqlHelper.CreateInputParameter("ConfigurationName", SqlDbType.NVarChar, 50, false, configurationName),
					CustomSqlHelper.CreateInputParameter("ConfigurationValue", SqlDbType.NVarChar, 1000, false, configurationValue), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<Configuration> helper = new CustomSqlHelper<Configuration>())
				{
					CustomGenericList<Configuration> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						Configuration o = list[0];
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
		/// Insert values into Configuration. Returns an object of type Configuration.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type Configuration.</param>
		/// <returns>Object of type Configuration.</returns>
		public Configuration ConfigurationInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			Configuration value)
		{
			return ConfigurationInsertAuto(sqlConnection, sqlTransaction, "BHL", value);
		}
		
		/// <summary>
		/// Insert values into Configuration. Returns an object of type Configuration.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type Configuration.</param>
		/// <returns>Object of type Configuration.</returns>
		public Configuration ConfigurationInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			Configuration value)
		{
			return ConfigurationInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.ConfigurationID,
				value.ConfigurationName,
				value.ConfigurationValue);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from Configuration by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="configurationID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool ConfigurationDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int configurationID)
		{
			return ConfigurationDeleteAuto( sqlConnection, sqlTransaction, "BHL", configurationID );
		}
		
		/// <summary>
		/// Delete values from Configuration by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="configurationID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool ConfigurationDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int configurationID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("ConfigurationDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("ConfigurationID", SqlDbType.Int, null, false, configurationID), 
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
		/// Update values in Configuration. Returns an object of type Configuration.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="configurationID"></param>
		/// <param name="configurationName"></param>
		/// <param name="configurationValue"></param>
		/// <returns>Object of type Configuration.</returns>
		public Configuration ConfigurationUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int configurationID,
			string configurationName,
			string configurationValue)
		{
			return ConfigurationUpdateAuto( sqlConnection, sqlTransaction, "BHL", configurationID, configurationName, configurationValue);
		}
		
		/// <summary>
		/// Update values in Configuration. Returns an object of type Configuration.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="configurationID"></param>
		/// <param name="configurationName"></param>
		/// <param name="configurationValue"></param>
		/// <returns>Object of type Configuration.</returns>
		public Configuration ConfigurationUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int configurationID,
			string configurationName,
			string configurationValue)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("ConfigurationUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("ConfigurationID", SqlDbType.Int, null, false, configurationID),
					CustomSqlHelper.CreateInputParameter("ConfigurationName", SqlDbType.NVarChar, 50, false, configurationName),
					CustomSqlHelper.CreateInputParameter("ConfigurationValue", SqlDbType.NVarChar, 1000, false, configurationValue), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<Configuration> helper = new CustomSqlHelper<Configuration>())
				{
					CustomGenericList<Configuration> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						Configuration o = list[0];
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
		/// Update values in Configuration. Returns an object of type Configuration.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type Configuration.</param>
		/// <returns>Object of type Configuration.</returns>
		public Configuration ConfigurationUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			Configuration value)
		{
			return ConfigurationUpdateAuto(sqlConnection, sqlTransaction, "BHL", value );
		}
		
		/// <summary>
		/// Update values in Configuration. Returns an object of type Configuration.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type Configuration.</param>
		/// <returns>Object of type Configuration.</returns>
		public Configuration ConfigurationUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			Configuration value)
		{
			return ConfigurationUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.ConfigurationID,
				value.ConfigurationName,
				value.ConfigurationValue);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage Configuration object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in Configuration.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type Configuration.</param>
		/// <returns>Object of type CustomDataAccessStatus<Configuration>.</returns>
		public CustomDataAccessStatus<Configuration> ConfigurationManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			Configuration value  )
		{
			return ConfigurationManageAuto( sqlConnection, sqlTransaction, "BHL", value  );
		}
		
		/// <summary>
		/// Manage Configuration object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in Configuration.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type Configuration.</param>
		/// <returns>Object of type CustomDataAccessStatus<Configuration>.</returns>
		public CustomDataAccessStatus<Configuration> ConfigurationManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			Configuration value  )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				
				
				Configuration returnValue = ConfigurationInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.ConfigurationID,
						value.ConfigurationName,
						value.ConfigurationValue);
				
				return new CustomDataAccessStatus<Configuration>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (ConfigurationDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.ConfigurationID))
				{
				return new CustomDataAccessStatus<Configuration>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<Configuration>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				
				Configuration returnValue = ConfigurationUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.ConfigurationID,
						value.ConfigurationName,
						value.ConfigurationValue);
					
				return new CustomDataAccessStatus<Configuration>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<Configuration>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}
// end of source generation
