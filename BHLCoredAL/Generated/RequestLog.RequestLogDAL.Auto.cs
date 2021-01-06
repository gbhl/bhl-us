
// Generated 10/4/2009 6:22:27 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class RequestLogDAL is based upon RequestLog.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.RequestLog.DAL
// {
// 		public partial class RequestLogDAL
//		{
//		}
// }

#endregion How To Implement

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHL.RequestLog.DataObjects;

namespace MOBOT.BHL.RequestLog.DAL
{
	partial class RequestLogDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from RequestLog by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="requestLogID"></param>
		/// <returns>Object of type RequestLog.</returns>
        public MOBOT.BHL.RequestLog.DataObjects.RequestLog RequestLogSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int requestLogID)
		{
			return RequestLogSelectAuto(	sqlConnection, sqlTransaction, "BHL",	requestLogID );
		}
			
		/// <summary>
		/// Select values from RequestLog by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="requestLogID"></param>
		/// <returns>Object of type RequestLog.</returns>
        public MOBOT.BHL.RequestLog.DataObjects.RequestLog RequestLogSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int requestLogID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("[reqlog].[RequestLogSelectAuto]", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("RequestLogID", SqlDbType.Int, null, false, requestLogID)))
			{
                using (CustomSqlHelper<MOBOT.BHL.RequestLog.DataObjects.RequestLog> helper = new CustomSqlHelper<MOBOT.BHL.RequestLog.DataObjects.RequestLog>())
				{
                    List<MOBOT.BHL.RequestLog.DataObjects.RequestLog> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
                        MOBOT.BHL.RequestLog.DataObjects.RequestLog o = list[0];
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
		/// Select values from RequestLog by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="requestLogID"></param>
		/// <returns>List&lt;DataItemRow&gt;</returns>
		public List<CustomDataRow> RequestLogSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int requestLogID)
		{
			return RequestLogSelectAutoRaw( sqlConnection, sqlTransaction, "BHL", requestLogID );
		}
		
		/// <summary>
		/// Select values from RequestLog by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="requestLogID"></param>
		/// <returns>List&lt;DataItemRow&gt;</returns>
		public List<CustomDataRow> RequestLogSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int requestLogID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("[reqlog].[RequestLogSelectAuto]", connection, transaction,
				CustomSqlHelper.CreateInputParameter("RequestLogID", SqlDbType.Int, null, false, requestLogID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====
	
 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into RequestLog.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="applicationID"></param>
		/// <param name="iPAddress"></param>
		/// <param name="userID"></param>
		/// <param name="requestTypeID"></param>
		/// <param name="detail"></param>
		/// <returns>Object of type RequestLog.</returns>
        public MOBOT.BHL.RequestLog.DataObjects.RequestLog RequestLogInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int applicationID,
			string iPAddress,
			int? userID,
			int requestTypeID,
			string detail)
		{
			return RequestLogInsertAuto( sqlConnection, sqlTransaction, "BHL", applicationID, iPAddress, userID, requestTypeID, detail );
		}
		
		/// <summary>
		/// Insert values into RequestLog.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="applicationID"></param>
		/// <param name="iPAddress"></param>
		/// <param name="userID"></param>
		/// <param name="requestTypeID"></param>
		/// <param name="detail"></param>
		/// <returns>Object of type RequestLog.</returns>
        public MOBOT.BHL.RequestLog.DataObjects.RequestLog RequestLogInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int applicationID,
			string iPAddress,
			int? userID,
			int requestTypeID,
			string detail)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("reqlog.RequestLogInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("RequestLogID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("ApplicationID", SqlDbType.Int, null, false, applicationID),
					CustomSqlHelper.CreateInputParameter("IPAddress", SqlDbType.VarChar, 15, true, iPAddress),
					CustomSqlHelper.CreateInputParameter("UserID", SqlDbType.Int, null, true, userID),
					CustomSqlHelper.CreateInputParameter("RequestTypeID", SqlDbType.Int, null, false, requestTypeID),
					CustomSqlHelper.CreateInputParameter("Detail", SqlDbType.VarChar, 2000, true, detail), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
                using (CustomSqlHelper<MOBOT.BHL.RequestLog.DataObjects.RequestLog> helper = new CustomSqlHelper<MOBOT.BHL.RequestLog.DataObjects.RequestLog>())
				{
                    List<MOBOT.BHL.RequestLog.DataObjects.RequestLog> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
                        MOBOT.BHL.RequestLog.DataObjects.RequestLog o = list[0];
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
		/// Insert values into RequestLog. Returns an object of type RequestLog.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type RequestLog.</param>
		/// <returns>Object of type RequestLog.</returns>
        public MOBOT.BHL.RequestLog.DataObjects.RequestLog RequestLogInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction,
            MOBOT.BHL.RequestLog.DataObjects.RequestLog value)
		{
			return RequestLogInsertAuto(sqlConnection, sqlTransaction, "BHL", value);
		}
		
		/// <summary>
		/// Insert values into RequestLog. Returns an object of type RequestLog.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type RequestLog.</param>
		/// <returns>Object of type RequestLog.</returns>
        public MOBOT.BHL.RequestLog.DataObjects.RequestLog RequestLogInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
            MOBOT.BHL.RequestLog.DataObjects.RequestLog value)
		{
			return RequestLogInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.ApplicationID,
				value.IPAddress,
				value.UserID,
				value.RequestTypeID,
				value.Detail);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from RequestLog by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="requestLogID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool RequestLogDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int requestLogID)
		{
			return RequestLogDeleteAuto( sqlConnection, sqlTransaction, "BHL", requestLogID );
		}
		
		/// <summary>
		/// Delete values from RequestLog by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="requestLogID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool RequestLogDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int requestLogID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("reqlog.RequestLogDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("RequestLogID", SqlDbType.Int, null, false, requestLogID), 
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
		/// Update values in RequestLog. Returns an object of type RequestLog.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="requestLogID"></param>
		/// <param name="applicationID"></param>
		/// <param name="iPAddress"></param>
		/// <param name="userID"></param>
		/// <param name="requestTypeID"></param>
		/// <param name="detail"></param>
		/// <returns>Object of type RequestLog.</returns>
        public MOBOT.BHL.RequestLog.DataObjects.RequestLog RequestLogUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int requestLogID,
			int applicationID,
			string iPAddress,
			int? userID,
			int requestTypeID,
			string detail)
		{
			return RequestLogUpdateAuto( sqlConnection, sqlTransaction, "BHL", requestLogID, applicationID, iPAddress, userID, requestTypeID, detail);
		}
		
		/// <summary>
		/// Update values in RequestLog. Returns an object of type RequestLog.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="requestLogID"></param>
		/// <param name="applicationID"></param>
		/// <param name="iPAddress"></param>
		/// <param name="userID"></param>
		/// <param name="requestTypeID"></param>
		/// <param name="detail"></param>
		/// <returns>Object of type RequestLog.</returns>
        public MOBOT.BHL.RequestLog.DataObjects.RequestLog RequestLogUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int requestLogID,
			int applicationID,
			string iPAddress,
			int? userID,
			int requestTypeID,
			string detail)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("reqlog.RequestLogUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("RequestLogID", SqlDbType.Int, null, false, requestLogID),
					CustomSqlHelper.CreateInputParameter("ApplicationID", SqlDbType.Int, null, false, applicationID),
					CustomSqlHelper.CreateInputParameter("IPAddress", SqlDbType.VarChar, 15, true, iPAddress),
					CustomSqlHelper.CreateInputParameter("UserID", SqlDbType.Int, null, true, userID),
					CustomSqlHelper.CreateInputParameter("RequestTypeID", SqlDbType.Int, null, false, requestTypeID),
					CustomSqlHelper.CreateInputParameter("Detail", SqlDbType.VarChar, 2000, true, detail), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
                using (CustomSqlHelper<MOBOT.BHL.RequestLog.DataObjects.RequestLog> helper = new CustomSqlHelper<MOBOT.BHL.RequestLog.DataObjects.RequestLog>())
				{
                    List<MOBOT.BHL.RequestLog.DataObjects.RequestLog> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
                        MOBOT.BHL.RequestLog.DataObjects.RequestLog o = list[0];
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
		/// Update values in RequestLog. Returns an object of type RequestLog.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type RequestLog.</param>
		/// <returns>Object of type RequestLog.</returns>
        public MOBOT.BHL.RequestLog.DataObjects.RequestLog RequestLogUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction,
            MOBOT.BHL.RequestLog.DataObjects.RequestLog value)
		{
			return RequestLogUpdateAuto(sqlConnection, sqlTransaction, "BHL", value );
		}
		
		/// <summary>
		/// Update values in RequestLog. Returns an object of type RequestLog.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type RequestLog.</param>
		/// <returns>Object of type RequestLog.</returns>
        public MOBOT.BHL.RequestLog.DataObjects.RequestLog RequestLogUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
            MOBOT.BHL.RequestLog.DataObjects.RequestLog value)
		{
			return RequestLogUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.RequestLogID,
				value.ApplicationID,
				value.IPAddress,
				value.UserID,
				value.RequestTypeID,
				value.Detail);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage RequestLog object.
		/// If the object is of type ObjectBase, 
		/// then either insert values into, delete values from, or update values in RequestLog.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type RequestLog.</param>
		/// <returns>Object of type DataAccessStatus<RequestLog>.</returns>
        public CustomDataAccessStatus<MOBOT.BHL.RequestLog.DataObjects.RequestLog> RequestLogManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction,
            MOBOT.BHL.RequestLog.DataObjects.RequestLog value)
		{
			return RequestLogManageAuto( sqlConnection, sqlTransaction, "BHL", value  );
		}
		
		/// <summary>
		/// Manage RequestLog object.
		/// If the object is of type ObjectBase, 
		/// then either insert values into, delete values from, or update values in RequestLog.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type RequestLog.</param>
		/// <returns>Object of type DataAccessStatus<RequestLog>.</returns>
        public CustomDataAccessStatus<MOBOT.BHL.RequestLog.DataObjects.RequestLog> RequestLogManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
            MOBOT.BHL.RequestLog.DataObjects.RequestLog value)
		{
			if (value.IsNew && !value.IsDeleted)
			{


                MOBOT.BHL.RequestLog.DataObjects.RequestLog returnValue = RequestLogInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.ApplicationID,
						value.IPAddress,
						value.UserID,
						value.RequestTypeID,
						value.Detail);

                return new CustomDataAccessStatus<MOBOT.BHL.RequestLog.DataObjects.RequestLog>(
                    CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (RequestLogDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.RequestLogID))
				{
                    return new CustomDataAccessStatus<MOBOT.BHL.RequestLog.DataObjects.RequestLog>(
                    CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
                    return new CustomDataAccessStatus<MOBOT.BHL.RequestLog.DataObjects.RequestLog>(
                    CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{

                MOBOT.BHL.RequestLog.DataObjects.RequestLog returnValue = RequestLogUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.RequestLogID,
						value.ApplicationID,
						value.IPAddress,
						value.UserID,
						value.RequestTypeID,
						value.Detail);

                return new CustomDataAccessStatus<MOBOT.BHL.RequestLog.DataObjects.RequestLog>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
                return new CustomDataAccessStatus<MOBOT.BHL.RequestLog.DataObjects.RequestLog>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}
// end of source generation
