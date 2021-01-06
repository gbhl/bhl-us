
// Generated 1/5/2021 3:26:38 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class PageTextLogDAL is based upon dbo.PageTextLog.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DAL
// {
// 		public partial class PageTextLogDAL
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
	partial class PageTextLogDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from dbo.PageTextLog by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="pageTextLogID"></param>
		/// <returns>Object of type PageTextLog.</returns>
		public PageTextLog PageTextLogSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int pageTextLogID)
		{
			return PageTextLogSelectAuto(	sqlConnection, sqlTransaction, "BHL",	pageTextLogID );
		}
			
		/// <summary>
		/// Select values from dbo.PageTextLog by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="pageTextLogID"></param>
		/// <returns>Object of type PageTextLog.</returns>
		public PageTextLog PageTextLogSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int pageTextLogID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("PageTextLogSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("PageTextLogID", SqlDbType.Int, null, false, pageTextLogID)))
			{
				using (CustomSqlHelper<PageTextLog> helper = new CustomSqlHelper<PageTextLog>())
				{
					List<PageTextLog> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						PageTextLog o = list[0];
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
		/// Select values from dbo.PageTextLog by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="pageTextLogID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> PageTextLogSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int pageTextLogID)
		{
			return PageTextLogSelectAutoRaw( sqlConnection, sqlTransaction, "BHL", pageTextLogID );
		}
		
		/// <summary>
		/// Select values from dbo.PageTextLog by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="pageTextLogID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> PageTextLogSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int pageTextLogID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("PageTextLogSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("PageTextLogID", SqlDbType.Int, null, false, pageTextLogID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====

 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into dbo.PageTextLog.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="pageID"></param>
		/// <param name="textSource"></param>
		/// <param name="textImportBatchFileID"></param>
		/// <param name="creationUserID"></param>
		/// <returns>Object of type PageTextLog.</returns>
		public PageTextLog PageTextLogInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int pageID,
			string textSource,
			int? textImportBatchFileID,
			int creationUserID)
		{
			return PageTextLogInsertAuto( sqlConnection, sqlTransaction, "BHL", pageID, textSource, textImportBatchFileID, creationUserID );
		}
		
		/// <summary>
		/// Insert values into dbo.PageTextLog.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="pageID"></param>
		/// <param name="textSource"></param>
		/// <param name="textImportBatchFileID"></param>
		/// <param name="creationUserID"></param>
		/// <returns>Object of type PageTextLog.</returns>
		public PageTextLog PageTextLogInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int pageID,
			string textSource,
			int? textImportBatchFileID,
			int creationUserID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("PageTextLogInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("PageTextLogID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("PageID", SqlDbType.Int, null, false, pageID),
					CustomSqlHelper.CreateInputParameter("TextSource", SqlDbType.NVarChar, 50, false, textSource),
					CustomSqlHelper.CreateInputParameter("TextImportBatchFileID", SqlDbType.Int, null, true, textImportBatchFileID),
					CustomSqlHelper.CreateInputParameter("CreationUserID", SqlDbType.Int, null, false, creationUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<PageTextLog> helper = new CustomSqlHelper<PageTextLog>())
				{
					List<PageTextLog> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						PageTextLog o = list[0];
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
		/// Insert values into dbo.PageTextLog. Returns an object of type PageTextLog.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type PageTextLog.</param>
		/// <returns>Object of type PageTextLog.</returns>
		public PageTextLog PageTextLogInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			PageTextLog value)
		{
			return PageTextLogInsertAuto(sqlConnection, sqlTransaction, "BHL", value);
		}
		
		/// <summary>
		/// Insert values into dbo.PageTextLog. Returns an object of type PageTextLog.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type PageTextLog.</param>
		/// <returns>Object of type PageTextLog.</returns>
		public PageTextLog PageTextLogInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			PageTextLog value)
		{
			return PageTextLogInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.PageID,
				value.TextSource,
				value.TextImportBatchFileID,
				value.CreationUserID);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from dbo.PageTextLog by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="pageTextLogID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool PageTextLogDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int pageTextLogID)
		{
			return PageTextLogDeleteAuto( sqlConnection, sqlTransaction, "BHL", pageTextLogID );
		}
		
		/// <summary>
		/// Delete values from dbo.PageTextLog by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="pageTextLogID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool PageTextLogDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int pageTextLogID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("PageTextLogDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("PageTextLogID", SqlDbType.Int, null, false, pageTextLogID), 
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
		/// Update values in dbo.PageTextLog. Returns an object of type PageTextLog.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="pageTextLogID"></param>
		/// <param name="pageID"></param>
		/// <param name="textSource"></param>
		/// <param name="textImportBatchFileID"></param>
		/// <returns>Object of type PageTextLog.</returns>
		public PageTextLog PageTextLogUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int pageTextLogID,
			int pageID,
			string textSource,
			int? textImportBatchFileID)
		{
			return PageTextLogUpdateAuto( sqlConnection, sqlTransaction, "BHL", pageTextLogID, pageID, textSource, textImportBatchFileID);
		}
		
		/// <summary>
		/// Update values in dbo.PageTextLog. Returns an object of type PageTextLog.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="pageTextLogID"></param>
		/// <param name="pageID"></param>
		/// <param name="textSource"></param>
		/// <param name="textImportBatchFileID"></param>
		/// <returns>Object of type PageTextLog.</returns>
		public PageTextLog PageTextLogUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int pageTextLogID,
			int pageID,
			string textSource,
			int? textImportBatchFileID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("PageTextLogUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("PageTextLogID", SqlDbType.Int, null, false, pageTextLogID),
					CustomSqlHelper.CreateInputParameter("PageID", SqlDbType.Int, null, false, pageID),
					CustomSqlHelper.CreateInputParameter("TextSource", SqlDbType.NVarChar, 50, false, textSource),
					CustomSqlHelper.CreateInputParameter("TextImportBatchFileID", SqlDbType.Int, null, true, textImportBatchFileID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<PageTextLog> helper = new CustomSqlHelper<PageTextLog>())
				{
					List<PageTextLog> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						PageTextLog o = list[0];
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
		/// Update values in dbo.PageTextLog. Returns an object of type PageTextLog.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type PageTextLog.</param>
		/// <returns>Object of type PageTextLog.</returns>
		public PageTextLog PageTextLogUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			PageTextLog value)
		{
			return PageTextLogUpdateAuto(sqlConnection, sqlTransaction, "BHL", value );
		}
		
		/// <summary>
		/// Update values in dbo.PageTextLog. Returns an object of type PageTextLog.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type PageTextLog.</param>
		/// <returns>Object of type PageTextLog.</returns>
		public PageTextLog PageTextLogUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			PageTextLog value)
		{
			return PageTextLogUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.PageTextLogID,
				value.PageID,
				value.TextSource,
				value.TextImportBatchFileID);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage dbo.PageTextLog object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.PageTextLog.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type PageTextLog.</param>
		/// <returns>Object of type CustomDataAccessStatus<PageTextLog>.</returns>
		public CustomDataAccessStatus<PageTextLog> PageTextLogManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			PageTextLog value , int userId )
		{
			return PageTextLogManageAuto( sqlConnection, sqlTransaction, "BHL", value , userId );
		}
		
		/// <summary>
		/// Manage dbo.PageTextLog object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.PageTextLog.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type PageTextLog.</param>
		/// <returns>Object of type CustomDataAccessStatus<PageTextLog>.</returns>
		public CustomDataAccessStatus<PageTextLog> PageTextLogManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			PageTextLog value , int userId )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				value.CreationUserID = userId;
				
				PageTextLog returnValue = PageTextLogInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.PageID,
						value.TextSource,
						value.TextImportBatchFileID,
						value.CreationUserID);
				
				return new CustomDataAccessStatus<PageTextLog>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (PageTextLogDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.PageTextLogID))
				{
				return new CustomDataAccessStatus<PageTextLog>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<PageTextLog>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				
				PageTextLog returnValue = PageTextLogUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.PageTextLogID,
						value.PageID,
						value.TextSource,
						value.TextImportBatchFileID);
					
				return new CustomDataAccessStatus<PageTextLog>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<PageTextLog>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}

