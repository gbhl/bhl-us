
// Generated 1/10/2014 11:05:49 AM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class ImportRecordErrorLogDAL is based upon ImportRecordErrorLog.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DAL
// {
// 		public partial class ImportRecordErrorLogDAL
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
	partial class ImportRecordErrorLogDAL : IImportRecordErrorLogDAL
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from ImportRecordErrorLog by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="importRecordErrorLogID"></param>
		/// <returns>Object of type ImportRecordErrorLog.</returns>
		public ImportRecordErrorLog ImportRecordErrorLogSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int importRecordErrorLogID)
		{
			return ImportRecordErrorLogSelectAuto(	sqlConnection, sqlTransaction, "BHL",	importRecordErrorLogID );
		}
			
		/// <summary>
		/// Select values from ImportRecordErrorLog by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="importRecordErrorLogID"></param>
		/// <returns>Object of type ImportRecordErrorLog.</returns>
		public ImportRecordErrorLog ImportRecordErrorLogSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int importRecordErrorLogID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("import.ImportRecordErrorLogSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("ImportRecordErrorLogID", SqlDbType.Int, null, false, importRecordErrorLogID)))
			{
				using (CustomSqlHelper<ImportRecordErrorLog> helper = new CustomSqlHelper<ImportRecordErrorLog>())
				{
					CustomGenericList<ImportRecordErrorLog> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						ImportRecordErrorLog o = list[0];
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
		/// Select values from ImportRecordErrorLog by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="importRecordErrorLogID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> ImportRecordErrorLogSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int importRecordErrorLogID)
		{
			return ImportRecordErrorLogSelectAutoRaw( sqlConnection, sqlTransaction, "BHL", importRecordErrorLogID );
		}
		
		/// <summary>
		/// Select values from ImportRecordErrorLog by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="importRecordErrorLogID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> ImportRecordErrorLogSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int importRecordErrorLogID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("import.ImportRecordErrorLogSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("ImportRecordErrorLogID", SqlDbType.Int, null, false, importRecordErrorLogID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====
	
 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into ImportRecordErrorLog.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="importRecordID"></param>
		/// <param name="errorDate"></param>
		/// <param name="errorMessage"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type ImportRecordErrorLog.</returns>
		public ImportRecordErrorLog ImportRecordErrorLogInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int importRecordID,
			DateTime errorDate,
			string errorMessage,
			int creationUserID,
			int lastModifiedUserID)
		{
			return ImportRecordErrorLogInsertAuto( sqlConnection, sqlTransaction, "BHL", importRecordID, errorDate, errorMessage, creationUserID, lastModifiedUserID );
		}
		
		/// <summary>
		/// Insert values into ImportRecordErrorLog.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="importRecordID"></param>
		/// <param name="errorDate"></param>
		/// <param name="errorMessage"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type ImportRecordErrorLog.</returns>
		public ImportRecordErrorLog ImportRecordErrorLogInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int importRecordID,
			DateTime errorDate,
			string errorMessage,
			int creationUserID,
			int lastModifiedUserID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("import.ImportRecordErrorLogInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("ImportRecordErrorLogID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("ImportRecordID", SqlDbType.Int, null, false, importRecordID),
					CustomSqlHelper.CreateInputParameter("ErrorDate", SqlDbType.DateTime, null, false, errorDate),
					CustomSqlHelper.CreateInputParameter("ErrorMessage", SqlDbType.NVarChar, 1073741823, false, errorMessage),
					CustomSqlHelper.CreateInputParameter("CreationUserID", SqlDbType.Int, null, false, creationUserID),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, false, lastModifiedUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<ImportRecordErrorLog> helper = new CustomSqlHelper<ImportRecordErrorLog>())
				{
					CustomGenericList<ImportRecordErrorLog> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						ImportRecordErrorLog o = list[0];
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
		/// Insert values into ImportRecordErrorLog. Returns an object of type ImportRecordErrorLog.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type ImportRecordErrorLog.</param>
		/// <returns>Object of type ImportRecordErrorLog.</returns>
		public ImportRecordErrorLog ImportRecordErrorLogInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			ImportRecordErrorLog value)
		{
			return ImportRecordErrorLogInsertAuto(sqlConnection, sqlTransaction, "BHL", value);
		}
		
		/// <summary>
		/// Insert values into ImportRecordErrorLog. Returns an object of type ImportRecordErrorLog.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type ImportRecordErrorLog.</param>
		/// <returns>Object of type ImportRecordErrorLog.</returns>
		public ImportRecordErrorLog ImportRecordErrorLogInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			ImportRecordErrorLog value)
		{
			return ImportRecordErrorLogInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.ImportRecordID,
				value.ErrorDate,
				value.ErrorMessage,
				value.CreationUserID,
				value.LastModifiedUserID);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from ImportRecordErrorLog by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="importRecordErrorLogID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool ImportRecordErrorLogDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int importRecordErrorLogID)
		{
			return ImportRecordErrorLogDeleteAuto( sqlConnection, sqlTransaction, "BHL", importRecordErrorLogID );
		}
		
		/// <summary>
		/// Delete values from ImportRecordErrorLog by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="importRecordErrorLogID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool ImportRecordErrorLogDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int importRecordErrorLogID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("import.ImportRecordErrorLogDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("ImportRecordErrorLogID", SqlDbType.Int, null, false, importRecordErrorLogID), 
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
		/// Update values in ImportRecordErrorLog. Returns an object of type ImportRecordErrorLog.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="importRecordErrorLogID"></param>
		/// <param name="importRecordID"></param>
		/// <param name="errorDate"></param>
		/// <param name="errorMessage"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type ImportRecordErrorLog.</returns>
		public ImportRecordErrorLog ImportRecordErrorLogUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int importRecordErrorLogID,
			int importRecordID,
			DateTime errorDate,
			string errorMessage,
			int lastModifiedUserID)
		{
			return ImportRecordErrorLogUpdateAuto( sqlConnection, sqlTransaction, "BHL", importRecordErrorLogID, importRecordID, errorDate, errorMessage, lastModifiedUserID);
		}
		
		/// <summary>
		/// Update values in ImportRecordErrorLog. Returns an object of type ImportRecordErrorLog.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="importRecordErrorLogID"></param>
		/// <param name="importRecordID"></param>
		/// <param name="errorDate"></param>
		/// <param name="errorMessage"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type ImportRecordErrorLog.</returns>
		public ImportRecordErrorLog ImportRecordErrorLogUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int importRecordErrorLogID,
			int importRecordID,
			DateTime errorDate,
			string errorMessage,
			int lastModifiedUserID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("import.ImportRecordErrorLogUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("ImportRecordErrorLogID", SqlDbType.Int, null, false, importRecordErrorLogID),
					CustomSqlHelper.CreateInputParameter("ImportRecordID", SqlDbType.Int, null, false, importRecordID),
					CustomSqlHelper.CreateInputParameter("ErrorDate", SqlDbType.DateTime, null, false, errorDate),
					CustomSqlHelper.CreateInputParameter("ErrorMessage", SqlDbType.NVarChar, 1073741823, false, errorMessage),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, false, lastModifiedUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<ImportRecordErrorLog> helper = new CustomSqlHelper<ImportRecordErrorLog>())
				{
					CustomGenericList<ImportRecordErrorLog> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						ImportRecordErrorLog o = list[0];
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
		/// Update values in ImportRecordErrorLog. Returns an object of type ImportRecordErrorLog.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type ImportRecordErrorLog.</param>
		/// <returns>Object of type ImportRecordErrorLog.</returns>
		public ImportRecordErrorLog ImportRecordErrorLogUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			ImportRecordErrorLog value)
		{
			return ImportRecordErrorLogUpdateAuto(sqlConnection, sqlTransaction, "BHL", value );
		}
		
		/// <summary>
		/// Update values in ImportRecordErrorLog. Returns an object of type ImportRecordErrorLog.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type ImportRecordErrorLog.</param>
		/// <returns>Object of type ImportRecordErrorLog.</returns>
		public ImportRecordErrorLog ImportRecordErrorLogUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			ImportRecordErrorLog value)
		{
			return ImportRecordErrorLogUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.ImportRecordErrorLogID,
				value.ImportRecordID,
				value.ErrorDate,
				value.ErrorMessage,
				value.LastModifiedUserID);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage ImportRecordErrorLog object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in ImportRecordErrorLog.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type ImportRecordErrorLog.</param>
		/// <returns>Object of type CustomDataAccessStatus<ImportRecordErrorLog>.</returns>
		public CustomDataAccessStatus<ImportRecordErrorLog> ImportRecordErrorLogManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			ImportRecordErrorLog value , int userId )
		{
			return ImportRecordErrorLogManageAuto( sqlConnection, sqlTransaction, "BHL", value , userId );
		}
		
		/// <summary>
		/// Manage ImportRecordErrorLog object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in ImportRecordErrorLog.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type ImportRecordErrorLog.</param>
		/// <returns>Object of type CustomDataAccessStatus<ImportRecordErrorLog>.</returns>
		public CustomDataAccessStatus<ImportRecordErrorLog> ImportRecordErrorLogManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			ImportRecordErrorLog value , int userId )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				value.CreationUserID = userId;
				value.LastModifiedUserID = userId;
				ImportRecordErrorLog returnValue = ImportRecordErrorLogInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.ImportRecordID,
						value.ErrorDate,
						value.ErrorMessage,
						value.CreationUserID,
						value.LastModifiedUserID);
				
				return new CustomDataAccessStatus<ImportRecordErrorLog>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (ImportRecordErrorLogDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.ImportRecordErrorLogID))
				{
				return new CustomDataAccessStatus<ImportRecordErrorLog>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<ImportRecordErrorLog>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				value.LastModifiedUserID = userId;
				ImportRecordErrorLog returnValue = ImportRecordErrorLogUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.ImportRecordErrorLogID,
						value.ImportRecordID,
						value.ErrorDate,
						value.ErrorMessage,
						value.LastModifiedUserID);
					
				return new CustomDataAccessStatus<ImportRecordErrorLog>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<ImportRecordErrorLog>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}
// end of source generation
