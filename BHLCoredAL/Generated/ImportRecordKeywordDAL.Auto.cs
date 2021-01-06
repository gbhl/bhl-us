
// Generated 1/10/2014 11:05:49 AM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class ImportRecordKeywordDAL is based upon ImportRecordKeyword.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DAL
// {
// 		public partial class ImportRecordKeywordDAL
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
	partial class ImportRecordKeywordDAL
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from ImportRecordKeyword by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="importRecordKeywordID"></param>
		/// <returns>Object of type ImportRecordKeyword.</returns>
		public ImportRecordKeyword ImportRecordKeywordSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int importRecordKeywordID)
		{
			return ImportRecordKeywordSelectAuto(	sqlConnection, sqlTransaction, "BHL",	importRecordKeywordID );
		}
			
		/// <summary>
		/// Select values from ImportRecordKeyword by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="importRecordKeywordID"></param>
		/// <returns>Object of type ImportRecordKeyword.</returns>
		public ImportRecordKeyword ImportRecordKeywordSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int importRecordKeywordID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("import.ImportRecordKeywordSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("ImportRecordKeywordID", SqlDbType.Int, null, false, importRecordKeywordID)))
			{
				using (CustomSqlHelper<ImportRecordKeyword> helper = new CustomSqlHelper<ImportRecordKeyword>())
				{
					List<ImportRecordKeyword> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						ImportRecordKeyword o = list[0];
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
		/// Select values from ImportRecordKeyword by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="importRecordKeywordID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> ImportRecordKeywordSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int importRecordKeywordID)
		{
			return ImportRecordKeywordSelectAutoRaw( sqlConnection, sqlTransaction, "BHL", importRecordKeywordID );
		}
		
		/// <summary>
		/// Select values from ImportRecordKeyword by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="importRecordKeywordID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> ImportRecordKeywordSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int importRecordKeywordID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("import.ImportRecordKeywordSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("ImportRecordKeywordID", SqlDbType.Int, null, false, importRecordKeywordID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====
	
 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into ImportRecordKeyword.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="importRecordID"></param>
		/// <param name="keyword"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type ImportRecordKeyword.</returns>
		public ImportRecordKeyword ImportRecordKeywordInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int importRecordID,
			string keyword,
			int creationUserID,
			int lastModifiedUserID)
		{
			return ImportRecordKeywordInsertAuto( sqlConnection, sqlTransaction, "BHL", importRecordID, keyword, creationUserID, lastModifiedUserID );
		}
		
		/// <summary>
		/// Insert values into ImportRecordKeyword.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="importRecordID"></param>
		/// <param name="keyword"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type ImportRecordKeyword.</returns>
		public ImportRecordKeyword ImportRecordKeywordInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int importRecordID,
			string keyword,
			int creationUserID,
			int lastModifiedUserID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("import.ImportRecordKeywordInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("ImportRecordKeywordID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("ImportRecordID", SqlDbType.Int, null, false, importRecordID),
					CustomSqlHelper.CreateInputParameter("Keyword", SqlDbType.NVarChar, 50, false, keyword),
					CustomSqlHelper.CreateInputParameter("CreationUserID", SqlDbType.Int, null, false, creationUserID),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, false, lastModifiedUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<ImportRecordKeyword> helper = new CustomSqlHelper<ImportRecordKeyword>())
				{
					List<ImportRecordKeyword> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						ImportRecordKeyword o = list[0];
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
		/// Insert values into ImportRecordKeyword. Returns an object of type ImportRecordKeyword.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type ImportRecordKeyword.</param>
		/// <returns>Object of type ImportRecordKeyword.</returns>
		public ImportRecordKeyword ImportRecordKeywordInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			ImportRecordKeyword value)
		{
			return ImportRecordKeywordInsertAuto(sqlConnection, sqlTransaction, "BHL", value);
		}
		
		/// <summary>
		/// Insert values into ImportRecordKeyword. Returns an object of type ImportRecordKeyword.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type ImportRecordKeyword.</param>
		/// <returns>Object of type ImportRecordKeyword.</returns>
		public ImportRecordKeyword ImportRecordKeywordInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			ImportRecordKeyword value)
		{
			return ImportRecordKeywordInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.ImportRecordID,
				value.Keyword,
				value.CreationUserID,
				value.LastModifiedUserID);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from ImportRecordKeyword by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="importRecordKeywordID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool ImportRecordKeywordDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int importRecordKeywordID)
		{
			return ImportRecordKeywordDeleteAuto( sqlConnection, sqlTransaction, "BHL", importRecordKeywordID );
		}
		
		/// <summary>
		/// Delete values from ImportRecordKeyword by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="importRecordKeywordID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool ImportRecordKeywordDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int importRecordKeywordID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("import.ImportRecordKeywordDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("ImportRecordKeywordID", SqlDbType.Int, null, false, importRecordKeywordID), 
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
		/// Update values in ImportRecordKeyword. Returns an object of type ImportRecordKeyword.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="importRecordKeywordID"></param>
		/// <param name="importRecordID"></param>
		/// <param name="keyword"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type ImportRecordKeyword.</returns>
		public ImportRecordKeyword ImportRecordKeywordUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int importRecordKeywordID,
			int importRecordID,
			string keyword,
			int lastModifiedUserID)
		{
			return ImportRecordKeywordUpdateAuto( sqlConnection, sqlTransaction, "BHL", importRecordKeywordID, importRecordID, keyword, lastModifiedUserID);
		}
		
		/// <summary>
		/// Update values in ImportRecordKeyword. Returns an object of type ImportRecordKeyword.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="importRecordKeywordID"></param>
		/// <param name="importRecordID"></param>
		/// <param name="keyword"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type ImportRecordKeyword.</returns>
		public ImportRecordKeyword ImportRecordKeywordUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int importRecordKeywordID,
			int importRecordID,
			string keyword,
			int lastModifiedUserID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("import.ImportRecordKeywordUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("ImportRecordKeywordID", SqlDbType.Int, null, false, importRecordKeywordID),
					CustomSqlHelper.CreateInputParameter("ImportRecordID", SqlDbType.Int, null, false, importRecordID),
					CustomSqlHelper.CreateInputParameter("Keyword", SqlDbType.NVarChar, 50, false, keyword),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, false, lastModifiedUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<ImportRecordKeyword> helper = new CustomSqlHelper<ImportRecordKeyword>())
				{
					List<ImportRecordKeyword> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						ImportRecordKeyword o = list[0];
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
		/// Update values in ImportRecordKeyword. Returns an object of type ImportRecordKeyword.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type ImportRecordKeyword.</param>
		/// <returns>Object of type ImportRecordKeyword.</returns>
		public ImportRecordKeyword ImportRecordKeywordUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			ImportRecordKeyword value)
		{
			return ImportRecordKeywordUpdateAuto(sqlConnection, sqlTransaction, "BHL", value );
		}
		
		/// <summary>
		/// Update values in ImportRecordKeyword. Returns an object of type ImportRecordKeyword.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type ImportRecordKeyword.</param>
		/// <returns>Object of type ImportRecordKeyword.</returns>
		public ImportRecordKeyword ImportRecordKeywordUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			ImportRecordKeyword value)
		{
			return ImportRecordKeywordUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.ImportRecordKeywordID,
				value.ImportRecordID,
				value.Keyword,
				value.LastModifiedUserID);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage ImportRecordKeyword object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in ImportRecordKeyword.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type ImportRecordKeyword.</param>
		/// <returns>Object of type CustomDataAccessStatus<ImportRecordKeyword>.</returns>
		public CustomDataAccessStatus<ImportRecordKeyword> ImportRecordKeywordManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			ImportRecordKeyword value , int userId )
		{
			return ImportRecordKeywordManageAuto( sqlConnection, sqlTransaction, "BHL", value , userId );
		}
		
		/// <summary>
		/// Manage ImportRecordKeyword object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in ImportRecordKeyword.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type ImportRecordKeyword.</param>
		/// <returns>Object of type CustomDataAccessStatus<ImportRecordKeyword>.</returns>
		public CustomDataAccessStatus<ImportRecordKeyword> ImportRecordKeywordManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			ImportRecordKeyword value , int userId )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				value.CreationUserID = userId;
				value.LastModifiedUserID = userId;
				ImportRecordKeyword returnValue = ImportRecordKeywordInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.ImportRecordID,
						value.Keyword,
						value.CreationUserID,
						value.LastModifiedUserID);
				
				return new CustomDataAccessStatus<ImportRecordKeyword>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (ImportRecordKeywordDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.ImportRecordKeywordID))
				{
				return new CustomDataAccessStatus<ImportRecordKeyword>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<ImportRecordKeyword>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				value.LastModifiedUserID = userId;
				ImportRecordKeyword returnValue = ImportRecordKeywordUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.ImportRecordKeywordID,
						value.ImportRecordID,
						value.Keyword,
						value.LastModifiedUserID);
					
				return new CustomDataAccessStatus<ImportRecordKeyword>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<ImportRecordKeyword>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}
// end of source generation
