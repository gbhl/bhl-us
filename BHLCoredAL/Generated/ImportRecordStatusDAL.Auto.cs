
// Generated 1/10/2014 11:05:49 AM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class ImportRecordStatusDAL is based upon ImportRecordStatus.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DAL
// {
// 		public partial class ImportRecordStatusDAL
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
	partial class ImportRecordStatusDAL
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from ImportRecordStatus by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="importRecordStatusID"></param>
		/// <returns>Object of type ImportRecordStatus.</returns>
		public ImportRecordStatus ImportRecordStatusSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int importRecordStatusID)
		{
			return ImportRecordStatusSelectAuto(	sqlConnection, sqlTransaction, "BHL",	importRecordStatusID );
		}
			
		/// <summary>
		/// Select values from ImportRecordStatus by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="importRecordStatusID"></param>
		/// <returns>Object of type ImportRecordStatus.</returns>
		public ImportRecordStatus ImportRecordStatusSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int importRecordStatusID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("import.ImportRecordStatusSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("ImportRecordStatusID", SqlDbType.Int, null, false, importRecordStatusID)))
			{
				using (CustomSqlHelper<ImportRecordStatus> helper = new CustomSqlHelper<ImportRecordStatus>())
				{
					List<ImportRecordStatus> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						ImportRecordStatus o = list[0];
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
		/// Select values from ImportRecordStatus by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="importRecordStatusID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> ImportRecordStatusSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int importRecordStatusID)
		{
			return ImportRecordStatusSelectAutoRaw( sqlConnection, sqlTransaction, "BHL", importRecordStatusID );
		}
		
		/// <summary>
		/// Select values from ImportRecordStatus by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="importRecordStatusID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> ImportRecordStatusSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int importRecordStatusID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("import.ImportRecordStatusSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("ImportRecordStatusID", SqlDbType.Int, null, false, importRecordStatusID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====
	
 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into ImportRecordStatus.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="importRecordStatusID"></param>
		/// <param name="statusName"></param>
		/// <param name="statusDescription"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type ImportRecordStatus.</returns>
		public ImportRecordStatus ImportRecordStatusInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int importRecordStatusID,
			string statusName,
			string statusDescription,
			int creationUserID,
			int lastModifiedUserID)
		{
			return ImportRecordStatusInsertAuto( sqlConnection, sqlTransaction, "BHL", importRecordStatusID, statusName, statusDescription, creationUserID, lastModifiedUserID );
		}
		
		/// <summary>
		/// Insert values into ImportRecordStatus.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="importRecordStatusID"></param>
		/// <param name="statusName"></param>
		/// <param name="statusDescription"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type ImportRecordStatus.</returns>
		public ImportRecordStatus ImportRecordStatusInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int importRecordStatusID,
			string statusName,
			string statusDescription,
			int creationUserID,
			int lastModifiedUserID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("import.ImportRecordStatusInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("ImportRecordStatusID", SqlDbType.Int, null, false, importRecordStatusID),
					CustomSqlHelper.CreateInputParameter("StatusName", SqlDbType.NVarChar, 50, false, statusName),
					CustomSqlHelper.CreateInputParameter("StatusDescription", SqlDbType.NVarChar, 500, false, statusDescription),
					CustomSqlHelper.CreateInputParameter("CreationUserID", SqlDbType.Int, null, false, creationUserID),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, false, lastModifiedUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<ImportRecordStatus> helper = new CustomSqlHelper<ImportRecordStatus>())
				{
					List<ImportRecordStatus> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						ImportRecordStatus o = list[0];
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
		/// Insert values into ImportRecordStatus. Returns an object of type ImportRecordStatus.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type ImportRecordStatus.</param>
		/// <returns>Object of type ImportRecordStatus.</returns>
		public ImportRecordStatus ImportRecordStatusInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			ImportRecordStatus value)
		{
			return ImportRecordStatusInsertAuto(sqlConnection, sqlTransaction, "BHL", value);
		}
		
		/// <summary>
		/// Insert values into ImportRecordStatus. Returns an object of type ImportRecordStatus.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type ImportRecordStatus.</param>
		/// <returns>Object of type ImportRecordStatus.</returns>
		public ImportRecordStatus ImportRecordStatusInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			ImportRecordStatus value)
		{
			return ImportRecordStatusInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.ImportRecordStatusID,
				value.StatusName,
				value.StatusDescription,
				value.CreationUserID,
				value.LastModifiedUserID);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from ImportRecordStatus by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="importRecordStatusID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool ImportRecordStatusDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int importRecordStatusID)
		{
			return ImportRecordStatusDeleteAuto( sqlConnection, sqlTransaction, "BHL", importRecordStatusID );
		}
		
		/// <summary>
		/// Delete values from ImportRecordStatus by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="importRecordStatusID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool ImportRecordStatusDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int importRecordStatusID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("import.ImportRecordStatusDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("ImportRecordStatusID", SqlDbType.Int, null, false, importRecordStatusID), 
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
		/// Update values in ImportRecordStatus. Returns an object of type ImportRecordStatus.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="importRecordStatusID"></param>
		/// <param name="statusName"></param>
		/// <param name="statusDescription"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type ImportRecordStatus.</returns>
		public ImportRecordStatus ImportRecordStatusUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int importRecordStatusID,
			string statusName,
			string statusDescription,
			int lastModifiedUserID)
		{
			return ImportRecordStatusUpdateAuto( sqlConnection, sqlTransaction, "BHL", importRecordStatusID, statusName, statusDescription, lastModifiedUserID);
		}
		
		/// <summary>
		/// Update values in ImportRecordStatus. Returns an object of type ImportRecordStatus.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="importRecordStatusID"></param>
		/// <param name="statusName"></param>
		/// <param name="statusDescription"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type ImportRecordStatus.</returns>
		public ImportRecordStatus ImportRecordStatusUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int importRecordStatusID,
			string statusName,
			string statusDescription,
			int lastModifiedUserID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("import.ImportRecordStatusUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("ImportRecordStatusID", SqlDbType.Int, null, false, importRecordStatusID),
					CustomSqlHelper.CreateInputParameter("StatusName", SqlDbType.NVarChar, 50, false, statusName),
					CustomSqlHelper.CreateInputParameter("StatusDescription", SqlDbType.NVarChar, 500, false, statusDescription),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, false, lastModifiedUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<ImportRecordStatus> helper = new CustomSqlHelper<ImportRecordStatus>())
				{
					List<ImportRecordStatus> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						ImportRecordStatus o = list[0];
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
		/// Update values in ImportRecordStatus. Returns an object of type ImportRecordStatus.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type ImportRecordStatus.</param>
		/// <returns>Object of type ImportRecordStatus.</returns>
		public ImportRecordStatus ImportRecordStatusUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			ImportRecordStatus value)
		{
			return ImportRecordStatusUpdateAuto(sqlConnection, sqlTransaction, "BHL", value );
		}
		
		/// <summary>
		/// Update values in ImportRecordStatus. Returns an object of type ImportRecordStatus.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type ImportRecordStatus.</param>
		/// <returns>Object of type ImportRecordStatus.</returns>
		public ImportRecordStatus ImportRecordStatusUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			ImportRecordStatus value)
		{
			return ImportRecordStatusUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.ImportRecordStatusID,
				value.StatusName,
				value.StatusDescription,
				value.LastModifiedUserID);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage ImportRecordStatus object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in ImportRecordStatus.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type ImportRecordStatus.</param>
		/// <returns>Object of type CustomDataAccessStatus<ImportRecordStatus>.</returns>
		public CustomDataAccessStatus<ImportRecordStatus> ImportRecordStatusManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			ImportRecordStatus value , int userId )
		{
			return ImportRecordStatusManageAuto( sqlConnection, sqlTransaction, "BHL", value , userId );
		}
		
		/// <summary>
		/// Manage ImportRecordStatus object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in ImportRecordStatus.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type ImportRecordStatus.</param>
		/// <returns>Object of type CustomDataAccessStatus<ImportRecordStatus>.</returns>
		public CustomDataAccessStatus<ImportRecordStatus> ImportRecordStatusManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			ImportRecordStatus value , int userId )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				value.CreationUserID = userId;
				value.LastModifiedUserID = userId;
				ImportRecordStatus returnValue = ImportRecordStatusInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.ImportRecordStatusID,
						value.StatusName,
						value.StatusDescription,
						value.CreationUserID,
						value.LastModifiedUserID);
				
				return new CustomDataAccessStatus<ImportRecordStatus>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (ImportRecordStatusDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.ImportRecordStatusID))
				{
				return new CustomDataAccessStatus<ImportRecordStatus>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<ImportRecordStatus>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				value.LastModifiedUserID = userId;
				ImportRecordStatus returnValue = ImportRecordStatusUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.ImportRecordStatusID,
						value.StatusName,
						value.StatusDescription,
						value.LastModifiedUserID);
					
				return new CustomDataAccessStatus<ImportRecordStatus>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<ImportRecordStatus>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}
// end of source generation
