
// Generated 1/10/2014 11:05:49 AM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class ImportFileStatusDAL is based upon ImportFileStatus.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DAL
// {
// 		public partial class ImportFileStatusDAL
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
	partial class ImportFileStatusDAL : IImportFileStatusDAL
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from ImportFileStatus by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="importFileStatusID"></param>
		/// <returns>Object of type ImportFileStatus.</returns>
		public ImportFileStatus ImportFileStatusSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int importFileStatusID)
		{
			return ImportFileStatusSelectAuto(	sqlConnection, sqlTransaction, "BHL",	importFileStatusID );
		}
			
		/// <summary>
		/// Select values from ImportFileStatus by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="importFileStatusID"></param>
		/// <returns>Object of type ImportFileStatus.</returns>
		public ImportFileStatus ImportFileStatusSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int importFileStatusID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("import.ImportFileStatusSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("ImportFileStatusID", SqlDbType.Int, null, false, importFileStatusID)))
			{
				using (CustomSqlHelper<ImportFileStatus> helper = new CustomSqlHelper<ImportFileStatus>())
				{
					CustomGenericList<ImportFileStatus> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						ImportFileStatus o = list[0];
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
		/// Select values from ImportFileStatus by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="importFileStatusID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> ImportFileStatusSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int importFileStatusID)
		{
			return ImportFileStatusSelectAutoRaw( sqlConnection, sqlTransaction, "BHL", importFileStatusID );
		}
		
		/// <summary>
		/// Select values from ImportFileStatus by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="importFileStatusID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> ImportFileStatusSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int importFileStatusID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("import.ImportFileStatusSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("ImportFileStatusID", SqlDbType.Int, null, false, importFileStatusID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====
	
 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into ImportFileStatus.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="importFileStatusID"></param>
		/// <param name="statusName"></param>
		/// <param name="statusDescription"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type ImportFileStatus.</returns>
		public ImportFileStatus ImportFileStatusInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int importFileStatusID,
			string statusName,
			string statusDescription,
			int creationUserID,
			int lastModifiedUserID)
		{
			return ImportFileStatusInsertAuto( sqlConnection, sqlTransaction, "BHL", importFileStatusID, statusName, statusDescription, creationUserID, lastModifiedUserID );
		}
		
		/// <summary>
		/// Insert values into ImportFileStatus.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="importFileStatusID"></param>
		/// <param name="statusName"></param>
		/// <param name="statusDescription"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type ImportFileStatus.</returns>
		public ImportFileStatus ImportFileStatusInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int importFileStatusID,
			string statusName,
			string statusDescription,
			int creationUserID,
			int lastModifiedUserID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("import.ImportFileStatusInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("ImportFileStatusID", SqlDbType.Int, null, false, importFileStatusID),
					CustomSqlHelper.CreateInputParameter("StatusName", SqlDbType.NVarChar, 50, false, statusName),
					CustomSqlHelper.CreateInputParameter("StatusDescription", SqlDbType.NVarChar, 500, false, statusDescription),
					CustomSqlHelper.CreateInputParameter("CreationUserID", SqlDbType.Int, null, false, creationUserID),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, false, lastModifiedUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<ImportFileStatus> helper = new CustomSqlHelper<ImportFileStatus>())
				{
					CustomGenericList<ImportFileStatus> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						ImportFileStatus o = list[0];
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
		/// Insert values into ImportFileStatus. Returns an object of type ImportFileStatus.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type ImportFileStatus.</param>
		/// <returns>Object of type ImportFileStatus.</returns>
		public ImportFileStatus ImportFileStatusInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			ImportFileStatus value)
		{
			return ImportFileStatusInsertAuto(sqlConnection, sqlTransaction, "BHL", value);
		}
		
		/// <summary>
		/// Insert values into ImportFileStatus. Returns an object of type ImportFileStatus.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type ImportFileStatus.</param>
		/// <returns>Object of type ImportFileStatus.</returns>
		public ImportFileStatus ImportFileStatusInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			ImportFileStatus value)
		{
			return ImportFileStatusInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.ImportFileStatusID,
				value.StatusName,
				value.StatusDescription,
				value.CreationUserID,
				value.LastModifiedUserID);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from ImportFileStatus by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="importFileStatusID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool ImportFileStatusDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int importFileStatusID)
		{
			return ImportFileStatusDeleteAuto( sqlConnection, sqlTransaction, "BHL", importFileStatusID );
		}
		
		/// <summary>
		/// Delete values from ImportFileStatus by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="importFileStatusID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool ImportFileStatusDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int importFileStatusID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("import.ImportFileStatusDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("ImportFileStatusID", SqlDbType.Int, null, false, importFileStatusID), 
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
		/// Update values in ImportFileStatus. Returns an object of type ImportFileStatus.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="importFileStatusID"></param>
		/// <param name="statusName"></param>
		/// <param name="statusDescription"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type ImportFileStatus.</returns>
		public ImportFileStatus ImportFileStatusUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int importFileStatusID,
			string statusName,
			string statusDescription,
			int lastModifiedUserID)
		{
			return ImportFileStatusUpdateAuto( sqlConnection, sqlTransaction, "BHL", importFileStatusID, statusName, statusDescription, lastModifiedUserID);
		}
		
		/// <summary>
		/// Update values in ImportFileStatus. Returns an object of type ImportFileStatus.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="importFileStatusID"></param>
		/// <param name="statusName"></param>
		/// <param name="statusDescription"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type ImportFileStatus.</returns>
		public ImportFileStatus ImportFileStatusUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int importFileStatusID,
			string statusName,
			string statusDescription,
			int lastModifiedUserID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("import.ImportFileStatusUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("ImportFileStatusID", SqlDbType.Int, null, false, importFileStatusID),
					CustomSqlHelper.CreateInputParameter("StatusName", SqlDbType.NVarChar, 50, false, statusName),
					CustomSqlHelper.CreateInputParameter("StatusDescription", SqlDbType.NVarChar, 500, false, statusDescription),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, false, lastModifiedUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<ImportFileStatus> helper = new CustomSqlHelper<ImportFileStatus>())
				{
					CustomGenericList<ImportFileStatus> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						ImportFileStatus o = list[0];
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
		/// Update values in ImportFileStatus. Returns an object of type ImportFileStatus.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type ImportFileStatus.</param>
		/// <returns>Object of type ImportFileStatus.</returns>
		public ImportFileStatus ImportFileStatusUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			ImportFileStatus value)
		{
			return ImportFileStatusUpdateAuto(sqlConnection, sqlTransaction, "BHL", value );
		}
		
		/// <summary>
		/// Update values in ImportFileStatus. Returns an object of type ImportFileStatus.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type ImportFileStatus.</param>
		/// <returns>Object of type ImportFileStatus.</returns>
		public ImportFileStatus ImportFileStatusUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			ImportFileStatus value)
		{
			return ImportFileStatusUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.ImportFileStatusID,
				value.StatusName,
				value.StatusDescription,
				value.LastModifiedUserID);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage ImportFileStatus object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in ImportFileStatus.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type ImportFileStatus.</param>
		/// <returns>Object of type CustomDataAccessStatus<ImportFileStatus>.</returns>
		public CustomDataAccessStatus<ImportFileStatus> ImportFileStatusManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			ImportFileStatus value , int userId )
		{
			return ImportFileStatusManageAuto( sqlConnection, sqlTransaction, "BHL", value , userId );
		}
		
		/// <summary>
		/// Manage ImportFileStatus object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in ImportFileStatus.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type ImportFileStatus.</param>
		/// <returns>Object of type CustomDataAccessStatus<ImportFileStatus>.</returns>
		public CustomDataAccessStatus<ImportFileStatus> ImportFileStatusManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			ImportFileStatus value , int userId )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				value.CreationUserID = userId;
				value.LastModifiedUserID = userId;
				ImportFileStatus returnValue = ImportFileStatusInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.ImportFileStatusID,
						value.StatusName,
						value.StatusDescription,
						value.CreationUserID,
						value.LastModifiedUserID);
				
				return new CustomDataAccessStatus<ImportFileStatus>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (ImportFileStatusDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.ImportFileStatusID))
				{
				return new CustomDataAccessStatus<ImportFileStatus>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<ImportFileStatus>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				value.LastModifiedUserID = userId;
				ImportFileStatus returnValue = ImportFileStatusUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.ImportFileStatusID,
						value.StatusName,
						value.StatusDescription,
						value.LastModifiedUserID);
					
				return new CustomDataAccessStatus<ImportFileStatus>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<ImportFileStatus>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}
// end of source generation
