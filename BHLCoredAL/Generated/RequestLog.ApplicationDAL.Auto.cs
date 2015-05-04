
// Generated 10/4/2009 6:22:27 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class ApplicationDAL is based upon Application.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.RequestLog.DAL
// {
// 		public partial class ApplicationDAL
//		{
//		}
// }

#endregion How To Implement

using System;
using System.Data;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHL.RequestLog.DataObjects;

namespace MOBOT.BHL.RequestLog.DAL
{
	partial class ApplicationDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from Application by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="applicationID"></param>
		/// <returns>Object of type Application.</returns>
		public Application ApplicationSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int applicationID)
		{
			return ApplicationSelectAuto(	sqlConnection, sqlTransaction, "BHL",	applicationID );
		}
			
		/// <summary>
		/// Select values from Application by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="applicationID"></param>
		/// <returns>Object of type Application.</returns>
		public Application ApplicationSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int applicationID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("[reqlog].[ApplicationSelectAuto]", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("ApplicationID", SqlDbType.Int, null, false, applicationID)))
			{
				using (CustomSqlHelper<Application> helper = new CustomSqlHelper<Application>())
				{
					CustomGenericList<Application> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						Application o = list[0];
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
		/// Select values from Application by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="applicationID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> ApplicationSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int applicationID)
		{
			return ApplicationSelectAutoRaw( sqlConnection, sqlTransaction, "BHL", applicationID );
		}
		
		/// <summary>
		/// Select values from Application by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="applicationID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> ApplicationSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int applicationID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("[reqlog].[ApplicationSelectAuto]", connection, transaction,
				CustomSqlHelper.CreateInputParameter("ApplicationID", SqlDbType.Int, null, false, applicationID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====
	
 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into Application.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="applicationID"></param>
		/// <param name="applicationName"></param>
		/// <returns>Object of type Application.</returns>
		public Application ApplicationInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int applicationID,
			string applicationName)
		{
			return ApplicationInsertAuto( sqlConnection, sqlTransaction, "BHL", applicationID, applicationName );
		}
		
		/// <summary>
		/// Insert values into Application.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="applicationID"></param>
		/// <param name="applicationName"></param>
		/// <returns>Object of type Application.</returns>
		public Application ApplicationInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int applicationID,
			string applicationName)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("reqlog.ApplicationInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("ApplicationID", SqlDbType.Int, null, false, applicationID),
					CustomSqlHelper.CreateInputParameter("ApplicationName", SqlDbType.VarChar, 50, false, applicationName), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<Application> helper = new CustomSqlHelper<Application>())
				{
					CustomGenericList<Application> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						Application o = list[0];
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
		/// Insert values into Application. Returns an object of type Application.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type Application.</param>
		/// <returns>Object of type Application.</returns>
		public Application ApplicationInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			Application value)
		{
			return ApplicationInsertAuto(sqlConnection, sqlTransaction, "BHL", value);
		}
		
		/// <summary>
		/// Insert values into Application. Returns an object of type Application.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type Application.</param>
		/// <returns>Object of type Application.</returns>
		public Application ApplicationInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			Application value)
		{
			return ApplicationInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.ApplicationID,
				value.ApplicationName);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from Application by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="applicationID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool ApplicationDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int applicationID)
		{
			return ApplicationDeleteAuto( sqlConnection, sqlTransaction, "BHL", applicationID );
		}
		
		/// <summary>
		/// Delete values from Application by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="applicationID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool ApplicationDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int applicationID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("reqlog.ApplicationDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("ApplicationID", SqlDbType.Int, null, false, applicationID), 
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
		/// Update values in Application. Returns an object of type Application.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="applicationID"></param>
		/// <param name="applicationName"></param>
		/// <returns>Object of type Application.</returns>
		public Application ApplicationUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int applicationID,
			string applicationName)
		{
			return ApplicationUpdateAuto( sqlConnection, sqlTransaction, "BHL", applicationID, applicationName);
		}
		
		/// <summary>
		/// Update values in Application. Returns an object of type Application.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="applicationID"></param>
		/// <param name="applicationName"></param>
		/// <returns>Object of type Application.</returns>
		public Application ApplicationUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int applicationID,
			string applicationName)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("reqlog.ApplicationUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("ApplicationID", SqlDbType.Int, null, false, applicationID),
					CustomSqlHelper.CreateInputParameter("ApplicationName", SqlDbType.VarChar, 50, false, applicationName), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<Application> helper = new CustomSqlHelper<Application>())
				{
					CustomGenericList<Application> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						Application o = list[0];
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
		/// Update values in Application. Returns an object of type Application.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type Application.</param>
		/// <returns>Object of type Application.</returns>
		public Application ApplicationUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			Application value)
		{
			return ApplicationUpdateAuto(sqlConnection, sqlTransaction, "BHL", value );
		}
		
		/// <summary>
		/// Update values in Application. Returns an object of type Application.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type Application.</param>
		/// <returns>Object of type Application.</returns>
		public Application ApplicationUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			Application value)
		{
			return ApplicationUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.ApplicationID,
				value.ApplicationName);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage Application object.
		/// If the object is of type ObjectBase, 
		/// then either insert values into, delete values from, or update values in Application.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type Application.</param>
		/// <returns>Object of type CustomDataAccessStatus<Application>.</returns>
		public CustomDataAccessStatus<Application> ApplicationManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			Application value  )
		{
			return ApplicationManageAuto( sqlConnection, sqlTransaction, "BHL", value  );
		}
		
		/// <summary>
		/// Manage Application object.
		/// If the object is of type ObjectBase, 
		/// then either insert values into, delete values from, or update values in Application.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type Application.</param>
		/// <returns>Object of type CustomDataAccessStatus<Application>.</returns>
        public CustomDataAccessStatus<Application> ApplicationManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			Application value  )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				
				
				Application returnValue = ApplicationInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.ApplicationID,
						value.ApplicationName);

                return new CustomDataAccessStatus<Application>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (ApplicationDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.ApplicationID))
				{
                    return new CustomDataAccessStatus<Application>(
                    CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
                    return new CustomDataAccessStatus<Application>(
                    CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				
				Application returnValue = ApplicationUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.ApplicationID,
						value.ApplicationName);

                return new CustomDataAccessStatus<Application>(
                    CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<Application>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}
// end of source generation
