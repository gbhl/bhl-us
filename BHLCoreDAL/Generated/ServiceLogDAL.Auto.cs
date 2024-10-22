
// Generated 10/16/2024 4:27:16 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class ServiceLogDAL is based upon servlog.ServiceLog.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DAL
// {
// 		public partial class ServiceLogDAL
//		{
//		}
// }

#endregion How To Implement

#region using

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHL.DataObjects;

#endregion using

namespace MOBOT.BHL.DAL
{
	partial class ServiceLogDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from servlog.ServiceLog by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="serviceLogID"></param>
		/// <returns>Object of type ServiceLog.</returns>
		public ServiceLog ServiceLogSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int serviceLogID)
		{
			return ServiceLogSelectAuto(	sqlConnection, sqlTransaction, "BHL",	serviceLogID );
		}
			
		/// <summary>
		/// Select values from servlog.ServiceLog by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="serviceLogID"></param>
		/// <returns>Object of type ServiceLog.</returns>
		public ServiceLog ServiceLogSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int serviceLogID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("servlog.ServiceLogSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("ServiceLogID", SqlDbType.Int, null, false, serviceLogID)))
			{
				using (CustomSqlHelper<ServiceLog> helper = new CustomSqlHelper<ServiceLog>())
				{
					List<ServiceLog> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						ServiceLog o = list[0];
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
		/// Select values from servlog.ServiceLog by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="serviceLogID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> ServiceLogSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int serviceLogID)
		{
			return ServiceLogSelectAutoRaw( sqlConnection, sqlTransaction, "BHL", serviceLogID );
		}
		
		/// <summary>
		/// Select values from servlog.ServiceLog by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="serviceLogID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> ServiceLogSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int serviceLogID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("servlog.ServiceLogSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("ServiceLogID", SqlDbType.Int, null, false, serviceLogID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====

 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into servlog.ServiceLog.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="serviceID"></param>
		/// <param name="severityID"></param>
		/// <param name="errorNumber"></param>
		/// <param name="procedure"></param>
		/// <param name="line"></param>
		/// <param name="message"></param>
		/// <param name="stackTrace"></param>
		/// <returns>Object of type ServiceLog.</returns>
		public ServiceLog ServiceLogInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int serviceID,
			int severityID,
			int? errorNumber,
			string procedure,
			int? line,
			string message,
			string stackTrace)
		{
			return ServiceLogInsertAuto( sqlConnection, sqlTransaction, "BHL", serviceID, severityID, errorNumber, procedure, line, message, stackTrace );
		}
		
		/// <summary>
		/// Insert values into servlog.ServiceLog.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="serviceID"></param>
		/// <param name="severityID"></param>
		/// <param name="errorNumber"></param>
		/// <param name="procedure"></param>
		/// <param name="line"></param>
		/// <param name="message"></param>
		/// <param name="stackTrace"></param>
		/// <returns>Object of type ServiceLog.</returns>
		public ServiceLog ServiceLogInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int serviceID,
			int severityID,
			int? errorNumber,
			string procedure,
			int? line,
			string message,
			string stackTrace)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("servlog.ServiceLogInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("ServiceLogID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("ServiceID", SqlDbType.Int, null, false, serviceID),
					CustomSqlHelper.CreateInputParameter("SeverityID", SqlDbType.Int, null, false, severityID),
					CustomSqlHelper.CreateInputParameter("ErrorNumber", SqlDbType.Int, null, true, errorNumber),
					CustomSqlHelper.CreateInputParameter("Procedure", SqlDbType.NVarChar, 500, false, procedure),
					CustomSqlHelper.CreateInputParameter("Line", SqlDbType.Int, null, true, line),
					CustomSqlHelper.CreateInputParameter("Message", SqlDbType.NVarChar, 1073741823, false, message),
					CustomSqlHelper.CreateInputParameter("StackTrace", SqlDbType.NVarChar, 1073741823, false, stackTrace), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<ServiceLog> helper = new CustomSqlHelper<ServiceLog>())
				{
					List<ServiceLog> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						ServiceLog o = list[0];
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
		/// Insert values into servlog.ServiceLog. Returns an object of type ServiceLog.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type ServiceLog.</param>
		/// <returns>Object of type ServiceLog.</returns>
		public ServiceLog ServiceLogInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			ServiceLog value)
		{
			return ServiceLogInsertAuto(sqlConnection, sqlTransaction, "BHL", value);
		}
		
		/// <summary>
		/// Insert values into servlog.ServiceLog. Returns an object of type ServiceLog.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type ServiceLog.</param>
		/// <returns>Object of type ServiceLog.</returns>
		public ServiceLog ServiceLogInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			ServiceLog value)
		{
			return ServiceLogInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.ServiceID,
				value.SeverityID,
				value.ErrorNumber,
				value.Procedure,
				value.Line,
				value.Message,
				value.StackTrace);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from servlog.ServiceLog by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="serviceLogID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool ServiceLogDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int serviceLogID)
		{
			return ServiceLogDeleteAuto( sqlConnection, sqlTransaction, "BHL", serviceLogID );
		}
		
		/// <summary>
		/// Delete values from servlog.ServiceLog by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="serviceLogID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool ServiceLogDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int serviceLogID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("servlog.ServiceLogDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("ServiceLogID", SqlDbType.Int, null, false, serviceLogID), 
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
		/// Update values in servlog.ServiceLog. Returns an object of type ServiceLog.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="serviceLogID"></param>
		/// <param name="serviceID"></param>
		/// <param name="severityID"></param>
		/// <param name="errorNumber"></param>
		/// <param name="procedure"></param>
		/// <param name="line"></param>
		/// <param name="message"></param>
		/// <param name="stackTrace"></param>
		/// <returns>Object of type ServiceLog.</returns>
		public ServiceLog ServiceLogUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int serviceLogID,
			int serviceID,
			int severityID,
			int? errorNumber,
			string procedure,
			int? line,
			string message,
			string stackTrace)
		{
			return ServiceLogUpdateAuto( sqlConnection, sqlTransaction, "BHL", serviceLogID, serviceID, severityID, errorNumber, procedure, line, message, stackTrace);
		}
		
		/// <summary>
		/// Update values in servlog.ServiceLog. Returns an object of type ServiceLog.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="serviceLogID"></param>
		/// <param name="serviceID"></param>
		/// <param name="severityID"></param>
		/// <param name="errorNumber"></param>
		/// <param name="procedure"></param>
		/// <param name="line"></param>
		/// <param name="message"></param>
		/// <param name="stackTrace"></param>
		/// <returns>Object of type ServiceLog.</returns>
		public ServiceLog ServiceLogUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int serviceLogID,
			int serviceID,
			int severityID,
			int? errorNumber,
			string procedure,
			int? line,
			string message,
			string stackTrace)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("servlog.ServiceLogUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("ServiceLogID", SqlDbType.Int, null, false, serviceLogID),
					CustomSqlHelper.CreateInputParameter("ServiceID", SqlDbType.Int, null, false, serviceID),
					CustomSqlHelper.CreateInputParameter("SeverityID", SqlDbType.Int, null, false, severityID),
					CustomSqlHelper.CreateInputParameter("ErrorNumber", SqlDbType.Int, null, true, errorNumber),
					CustomSqlHelper.CreateInputParameter("Procedure", SqlDbType.NVarChar, 500, false, procedure),
					CustomSqlHelper.CreateInputParameter("Line", SqlDbType.Int, null, true, line),
					CustomSqlHelper.CreateInputParameter("Message", SqlDbType.NVarChar, 1073741823, false, message),
					CustomSqlHelper.CreateInputParameter("StackTrace", SqlDbType.NVarChar, 1073741823, false, stackTrace), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<ServiceLog> helper = new CustomSqlHelper<ServiceLog>())
				{
					List<ServiceLog> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						ServiceLog o = list[0];
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
		/// Update values in servlog.ServiceLog. Returns an object of type ServiceLog.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type ServiceLog.</param>
		/// <returns>Object of type ServiceLog.</returns>
		public ServiceLog ServiceLogUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			ServiceLog value)
		{
			return ServiceLogUpdateAuto(sqlConnection, sqlTransaction, "BHL", value );
		}
		
		/// <summary>
		/// Update values in servlog.ServiceLog. Returns an object of type ServiceLog.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type ServiceLog.</param>
		/// <returns>Object of type ServiceLog.</returns>
		public ServiceLog ServiceLogUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			ServiceLog value)
		{
			return ServiceLogUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.ServiceLogID,
				value.ServiceID,
				value.SeverityID,
				value.ErrorNumber,
				value.Procedure,
				value.Line,
				value.Message,
				value.StackTrace);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage servlog.ServiceLog object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in servlog.ServiceLog.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type ServiceLog.</param>
		/// <returns>Object of type CustomDataAccessStatus<ServiceLog>.</returns>
		public CustomDataAccessStatus<ServiceLog> ServiceLogManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			ServiceLog value  )
		{
			return ServiceLogManageAuto( sqlConnection, sqlTransaction, "BHL", value  );
		}
		
		/// <summary>
		/// Manage servlog.ServiceLog object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in servlog.ServiceLog.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type ServiceLog.</param>
		/// <returns>Object of type CustomDataAccessStatus<ServiceLog>.</returns>
		public CustomDataAccessStatus<ServiceLog> ServiceLogManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			ServiceLog value  )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				
				
				ServiceLog returnValue = ServiceLogInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.ServiceID,
						value.SeverityID,
						value.ErrorNumber,
						value.Procedure,
						value.Line,
						value.Message,
						value.StackTrace);
				
				return new CustomDataAccessStatus<ServiceLog>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (ServiceLogDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.ServiceLogID))
				{
				return new CustomDataAccessStatus<ServiceLog>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<ServiceLog>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				
				ServiceLog returnValue = ServiceLogUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.ServiceLogID,
						value.ServiceID,
						value.SeverityID,
						value.ErrorNumber,
						value.Procedure,
						value.Line,
						value.Message,
						value.StackTrace);
					
				return new CustomDataAccessStatus<ServiceLog>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<ServiceLog>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}

