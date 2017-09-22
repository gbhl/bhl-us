
// Generated 10/24/2017 12:44:16 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class ImportRecordPageDAL is based upon import.ImportRecordPage.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DAL
// {
// 		public partial class ImportRecordPageDAL
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
	partial class ImportRecordPageDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from import.ImportRecordPage by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="importRecordPageID"></param>
		/// <returns>Object of type ImportRecordPage.</returns>
		public ImportRecordPage ImportRecordPageSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int importRecordPageID)
		{
			return ImportRecordPageSelectAuto(	sqlConnection, sqlTransaction, "BHL",	importRecordPageID );
		}
			
		/// <summary>
		/// Select values from import.ImportRecordPage by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="importRecordPageID"></param>
		/// <returns>Object of type ImportRecordPage.</returns>
		public ImportRecordPage ImportRecordPageSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int importRecordPageID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("import.ImportRecordPageSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("ImportRecordPageID", SqlDbType.Int, null, false, importRecordPageID)))
			{
				using (CustomSqlHelper<ImportRecordPage> helper = new CustomSqlHelper<ImportRecordPage>())
				{
					CustomGenericList<ImportRecordPage> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						ImportRecordPage o = list[0];
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
		/// Select values from import.ImportRecordPage by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="importRecordPageID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> ImportRecordPageSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int importRecordPageID)
		{
			return ImportRecordPageSelectAutoRaw( sqlConnection, sqlTransaction, "BHL", importRecordPageID );
		}
		
		/// <summary>
		/// Select values from import.ImportRecordPage by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="importRecordPageID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> ImportRecordPageSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int importRecordPageID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("import.ImportRecordPageSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("ImportRecordPageID", SqlDbType.Int, null, false, importRecordPageID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====

 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into import.ImportRecordPage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="importRecordID"></param>
		/// <param name="pageID"></param>
		/// <param name="sequenceOrder"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type ImportRecordPage.</returns>
		public ImportRecordPage ImportRecordPageInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int importRecordID,
			int pageID,
			short sequenceOrder,
			int creationUserID,
			int lastModifiedUserID)
		{
			return ImportRecordPageInsertAuto( sqlConnection, sqlTransaction, "BHL", importRecordID, pageID, sequenceOrder, creationUserID, lastModifiedUserID );
		}
		
		/// <summary>
		/// Insert values into import.ImportRecordPage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="importRecordID"></param>
		/// <param name="pageID"></param>
		/// <param name="sequenceOrder"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type ImportRecordPage.</returns>
		public ImportRecordPage ImportRecordPageInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int importRecordID,
			int pageID,
			short sequenceOrder,
			int creationUserID,
			int lastModifiedUserID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("import.ImportRecordPageInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("ImportRecordPageID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("ImportRecordID", SqlDbType.Int, null, false, importRecordID),
					CustomSqlHelper.CreateInputParameter("PageID", SqlDbType.Int, null, false, pageID),
					CustomSqlHelper.CreateInputParameter("SequenceOrder", SqlDbType.SmallInt, null, false, sequenceOrder),
					CustomSqlHelper.CreateInputParameter("CreationUserID", SqlDbType.Int, null, false, creationUserID),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, false, lastModifiedUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<ImportRecordPage> helper = new CustomSqlHelper<ImportRecordPage>())
				{
					CustomGenericList<ImportRecordPage> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						ImportRecordPage o = list[0];
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
		/// Insert values into import.ImportRecordPage. Returns an object of type ImportRecordPage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type ImportRecordPage.</param>
		/// <returns>Object of type ImportRecordPage.</returns>
		public ImportRecordPage ImportRecordPageInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			ImportRecordPage value)
		{
			return ImportRecordPageInsertAuto(sqlConnection, sqlTransaction, "BHL", value);
		}
		
		/// <summary>
		/// Insert values into import.ImportRecordPage. Returns an object of type ImportRecordPage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type ImportRecordPage.</param>
		/// <returns>Object of type ImportRecordPage.</returns>
		public ImportRecordPage ImportRecordPageInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			ImportRecordPage value)
		{
			return ImportRecordPageInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.ImportRecordID,
				value.PageID,
				value.SequenceOrder,
				value.CreationUserID,
				value.LastModifiedUserID);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from import.ImportRecordPage by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="importRecordPageID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool ImportRecordPageDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int importRecordPageID)
		{
			return ImportRecordPageDeleteAuto( sqlConnection, sqlTransaction, "BHL", importRecordPageID );
		}
		
		/// <summary>
		/// Delete values from import.ImportRecordPage by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="importRecordPageID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool ImportRecordPageDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int importRecordPageID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("import.ImportRecordPageDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("ImportRecordPageID", SqlDbType.Int, null, false, importRecordPageID), 
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
		/// Update values in import.ImportRecordPage. Returns an object of type ImportRecordPage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="importRecordPageID"></param>
		/// <param name="importRecordID"></param>
		/// <param name="pageID"></param>
		/// <param name="sequenceOrder"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type ImportRecordPage.</returns>
		public ImportRecordPage ImportRecordPageUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int importRecordPageID,
			int importRecordID,
			int pageID,
			short sequenceOrder,
			int lastModifiedUserID)
		{
			return ImportRecordPageUpdateAuto( sqlConnection, sqlTransaction, "BHL", importRecordPageID, importRecordID, pageID, sequenceOrder, lastModifiedUserID);
		}
		
		/// <summary>
		/// Update values in import.ImportRecordPage. Returns an object of type ImportRecordPage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="importRecordPageID"></param>
		/// <param name="importRecordID"></param>
		/// <param name="pageID"></param>
		/// <param name="sequenceOrder"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type ImportRecordPage.</returns>
		public ImportRecordPage ImportRecordPageUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int importRecordPageID,
			int importRecordID,
			int pageID,
			short sequenceOrder,
			int lastModifiedUserID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("import.ImportRecordPageUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("ImportRecordPageID", SqlDbType.Int, null, false, importRecordPageID),
					CustomSqlHelper.CreateInputParameter("ImportRecordID", SqlDbType.Int, null, false, importRecordID),
					CustomSqlHelper.CreateInputParameter("PageID", SqlDbType.Int, null, false, pageID),
					CustomSqlHelper.CreateInputParameter("SequenceOrder", SqlDbType.SmallInt, null, false, sequenceOrder),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, false, lastModifiedUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<ImportRecordPage> helper = new CustomSqlHelper<ImportRecordPage>())
				{
					CustomGenericList<ImportRecordPage> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						ImportRecordPage o = list[0];
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
		/// Update values in import.ImportRecordPage. Returns an object of type ImportRecordPage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type ImportRecordPage.</param>
		/// <returns>Object of type ImportRecordPage.</returns>
		public ImportRecordPage ImportRecordPageUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			ImportRecordPage value)
		{
			return ImportRecordPageUpdateAuto(sqlConnection, sqlTransaction, "BHL", value );
		}
		
		/// <summary>
		/// Update values in import.ImportRecordPage. Returns an object of type ImportRecordPage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type ImportRecordPage.</param>
		/// <returns>Object of type ImportRecordPage.</returns>
		public ImportRecordPage ImportRecordPageUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			ImportRecordPage value)
		{
			return ImportRecordPageUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.ImportRecordPageID,
				value.ImportRecordID,
				value.PageID,
				value.SequenceOrder,
				value.LastModifiedUserID);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage import.ImportRecordPage object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in import.ImportRecordPage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type ImportRecordPage.</param>
		/// <returns>Object of type CustomDataAccessStatus<ImportRecordPage>.</returns>
		public CustomDataAccessStatus<ImportRecordPage> ImportRecordPageManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			ImportRecordPage value , int userId )
		{
			return ImportRecordPageManageAuto( sqlConnection, sqlTransaction, "BHL", value , userId );
		}
		
		/// <summary>
		/// Manage import.ImportRecordPage object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in import.ImportRecordPage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type ImportRecordPage.</param>
		/// <returns>Object of type CustomDataAccessStatus<ImportRecordPage>.</returns>
		public CustomDataAccessStatus<ImportRecordPage> ImportRecordPageManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			ImportRecordPage value , int userId )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				value.CreationUserID = userId;
				value.LastModifiedUserID = userId;
				ImportRecordPage returnValue = ImportRecordPageInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.ImportRecordID,
						value.PageID,
						value.SequenceOrder,
						value.CreationUserID,
						value.LastModifiedUserID);
				
				return new CustomDataAccessStatus<ImportRecordPage>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (ImportRecordPageDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.ImportRecordPageID))
				{
				return new CustomDataAccessStatus<ImportRecordPage>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<ImportRecordPage>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				value.LastModifiedUserID = userId;
				ImportRecordPage returnValue = ImportRecordPageUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.ImportRecordPageID,
						value.ImportRecordID,
						value.PageID,
						value.SequenceOrder,
						value.LastModifiedUserID);
					
				return new CustomDataAccessStatus<ImportRecordPage>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<ImportRecordPage>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}

