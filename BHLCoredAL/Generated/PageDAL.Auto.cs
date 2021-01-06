
// Generated 1/5/2021 3:26:32 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class PageDAL is based upon dbo.Page.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DAL
// {
// 		public partial class PageDAL
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
	partial class PageDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from dbo.Page by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="pageID"></param>
		/// <returns>Object of type Page.</returns>
		public Page PageSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int pageID)
		{
			return PageSelectAuto(	sqlConnection, sqlTransaction, "BHL",	pageID );
		}
			
		/// <summary>
		/// Select values from dbo.Page by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="pageID"></param>
		/// <returns>Object of type Page.</returns>
		public Page PageSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int pageID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("PageSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("PageID", SqlDbType.Int, null, false, pageID)))
			{
				using (CustomSqlHelper<Page> helper = new CustomSqlHelper<Page>())
				{
					List<Page> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						Page o = list[0];
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
		/// Select values from dbo.Page by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="pageID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> PageSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int pageID)
		{
			return PageSelectAutoRaw( sqlConnection, sqlTransaction, "BHL", pageID );
		}
		
		/// <summary>
		/// Select values from dbo.Page by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="pageID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> PageSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int pageID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("PageSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("PageID", SqlDbType.Int, null, false, pageID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====

 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into dbo.Page.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="itemID"></param>
		/// <param name="fileNamePrefix"></param>
		/// <param name="sequenceOrder"></param>
		/// <param name="pageDescription"></param>
		/// <param name="illustration"></param>
		/// <param name="note"></param>
		/// <param name="fileSize_Temp"></param>
		/// <param name="fileExtension"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <param name="active"></param>
		/// <param name="year"></param>
		/// <param name="series"></param>
		/// <param name="volume"></param>
		/// <param name="issue"></param>
		/// <param name="externalURL"></param>
		/// <param name="issuePrefix"></param>
		/// <param name="lastPageNameLookupDate"></param>
		/// <param name="paginationUserID"></param>
		/// <param name="paginationDate"></param>
		/// <param name="altExternalURL"></param>
		/// <returns>Object of type Page.</returns>
		public Page PageInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int? itemID,
			string fileNamePrefix,
			int? sequenceOrder,
			string pageDescription,
			bool illustration,
			string note,
			int? fileSize_Temp,
			string fileExtension,
			int? creationUserID,
			int? lastModifiedUserID,
			bool active,
			string year,
			string series,
			string volume,
			string issue,
			string externalURL,
			string issuePrefix,
			DateTime? lastPageNameLookupDate,
			int? paginationUserID,
			DateTime? paginationDate,
			string altExternalURL)
		{
			return PageInsertAuto( sqlConnection, sqlTransaction, "BHL", itemID, fileNamePrefix, sequenceOrder, pageDescription, illustration, note, fileSize_Temp, fileExtension, creationUserID, lastModifiedUserID, active, year, series, volume, issue, externalURL, issuePrefix, lastPageNameLookupDate, paginationUserID, paginationDate, altExternalURL );
		}
		
		/// <summary>
		/// Insert values into dbo.Page.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="itemID"></param>
		/// <param name="fileNamePrefix"></param>
		/// <param name="sequenceOrder"></param>
		/// <param name="pageDescription"></param>
		/// <param name="illustration"></param>
		/// <param name="note"></param>
		/// <param name="fileSize_Temp"></param>
		/// <param name="fileExtension"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <param name="active"></param>
		/// <param name="year"></param>
		/// <param name="series"></param>
		/// <param name="volume"></param>
		/// <param name="issue"></param>
		/// <param name="externalURL"></param>
		/// <param name="issuePrefix"></param>
		/// <param name="lastPageNameLookupDate"></param>
		/// <param name="paginationUserID"></param>
		/// <param name="paginationDate"></param>
		/// <param name="altExternalURL"></param>
		/// <returns>Object of type Page.</returns>
		public Page PageInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int? itemID,
			string fileNamePrefix,
			int? sequenceOrder,
			string pageDescription,
			bool illustration,
			string note,
			int? fileSize_Temp,
			string fileExtension,
			int? creationUserID,
			int? lastModifiedUserID,
			bool active,
			string year,
			string series,
			string volume,
			string issue,
			string externalURL,
			string issuePrefix,
			DateTime? lastPageNameLookupDate,
			int? paginationUserID,
			DateTime? paginationDate,
			string altExternalURL)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("PageInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("PageID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, true, itemID),
					CustomSqlHelper.CreateInputParameter("FileNamePrefix", SqlDbType.NVarChar, 200, false, fileNamePrefix),
					CustomSqlHelper.CreateInputParameter("SequenceOrder", SqlDbType.Int, null, true, sequenceOrder),
					CustomSqlHelper.CreateInputParameter("PageDescription", SqlDbType.NVarChar, 255, true, pageDescription),
					CustomSqlHelper.CreateInputParameter("Illustration", SqlDbType.Bit, null, false, illustration),
					CustomSqlHelper.CreateInputParameter("Note", SqlDbType.NVarChar, 255, true, note),
					CustomSqlHelper.CreateInputParameter("FileSize_Temp", SqlDbType.Int, null, true, fileSize_Temp),
					CustomSqlHelper.CreateInputParameter("FileExtension", SqlDbType.NVarChar, 5, true, fileExtension),
					CustomSqlHelper.CreateInputParameter("CreationUserID", SqlDbType.Int, null, true, creationUserID),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, true, lastModifiedUserID),
					CustomSqlHelper.CreateInputParameter("Active", SqlDbType.Bit, null, false, active),
					CustomSqlHelper.CreateInputParameter("Year", SqlDbType.NVarChar, 20, true, year),
					CustomSqlHelper.CreateInputParameter("Series", SqlDbType.NVarChar, 20, true, series),
					CustomSqlHelper.CreateInputParameter("Volume", SqlDbType.NVarChar, 20, true, volume),
					CustomSqlHelper.CreateInputParameter("Issue", SqlDbType.NVarChar, 20, true, issue),
					CustomSqlHelper.CreateInputParameter("ExternalURL", SqlDbType.NVarChar, 500, true, externalURL),
					CustomSqlHelper.CreateInputParameter("IssuePrefix", SqlDbType.NVarChar, 20, true, issuePrefix),
					CustomSqlHelper.CreateInputParameter("LastPageNameLookupDate", SqlDbType.DateTime, null, true, lastPageNameLookupDate),
					CustomSqlHelper.CreateInputParameter("PaginationUserID", SqlDbType.Int, null, true, paginationUserID),
					CustomSqlHelper.CreateInputParameter("PaginationDate", SqlDbType.DateTime, null, true, paginationDate),
					CustomSqlHelper.CreateInputParameter("AltExternalURL", SqlDbType.NVarChar, 500, true, altExternalURL), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<Page> helper = new CustomSqlHelper<Page>())
				{
					List<Page> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						Page o = list[0];
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
		/// Insert values into dbo.Page. Returns an object of type Page.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type Page.</param>
		/// <returns>Object of type Page.</returns>
		public Page PageInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			Page value)
		{
			return PageInsertAuto(sqlConnection, sqlTransaction, "BHL", value);
		}
		
		/// <summary>
		/// Insert values into dbo.Page. Returns an object of type Page.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type Page.</param>
		/// <returns>Object of type Page.</returns>
		public Page PageInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			Page value)
		{
			return PageInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.ItemID,
				value.FileNamePrefix,
				value.SequenceOrder,
				value.PageDescription,
				value.Illustration,
				value.Note,
				value.FileSize_Temp,
				value.FileExtension,
				value.CreationUserID,
				value.LastModifiedUserID,
				value.Active,
				value.Year,
				value.Series,
				value.Volume,
				value.Issue,
				value.ExternalURL,
				value.IssuePrefix,
				value.LastPageNameLookupDate,
				value.PaginationUserID,
				value.PaginationDate,
				value.AltExternalURL);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from dbo.Page by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="pageID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool PageDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int pageID)
		{
			return PageDeleteAuto( sqlConnection, sqlTransaction, "BHL", pageID );
		}
		
		/// <summary>
		/// Delete values from dbo.Page by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="pageID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool PageDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int pageID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("PageDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("PageID", SqlDbType.Int, null, false, pageID), 
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
		/// Update values in dbo.Page. Returns an object of type Page.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="pageID"></param>
		/// <param name="itemID"></param>
		/// <param name="fileNamePrefix"></param>
		/// <param name="sequenceOrder"></param>
		/// <param name="pageDescription"></param>
		/// <param name="illustration"></param>
		/// <param name="note"></param>
		/// <param name="fileSize_Temp"></param>
		/// <param name="fileExtension"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <param name="active"></param>
		/// <param name="year"></param>
		/// <param name="series"></param>
		/// <param name="volume"></param>
		/// <param name="issue"></param>
		/// <param name="externalURL"></param>
		/// <param name="issuePrefix"></param>
		/// <param name="lastPageNameLookupDate"></param>
		/// <param name="paginationUserID"></param>
		/// <param name="paginationDate"></param>
		/// <param name="altExternalURL"></param>
		/// <returns>Object of type Page.</returns>
		public Page PageUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int pageID,
			int? itemID,
			string fileNamePrefix,
			int? sequenceOrder,
			string pageDescription,
			bool illustration,
			string note,
			int? fileSize_Temp,
			string fileExtension,
			int? lastModifiedUserID,
			bool active,
			string year,
			string series,
			string volume,
			string issue,
			string externalURL,
			string issuePrefix,
			DateTime? lastPageNameLookupDate,
			int? paginationUserID,
			DateTime? paginationDate,
			string altExternalURL)
		{
			return PageUpdateAuto( sqlConnection, sqlTransaction, "BHL", pageID, itemID, fileNamePrefix, sequenceOrder, pageDescription, illustration, note, fileSize_Temp, fileExtension, lastModifiedUserID, active, year, series, volume, issue, externalURL, issuePrefix, lastPageNameLookupDate, paginationUserID, paginationDate, altExternalURL);
		}
		
		/// <summary>
		/// Update values in dbo.Page. Returns an object of type Page.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="pageID"></param>
		/// <param name="itemID"></param>
		/// <param name="fileNamePrefix"></param>
		/// <param name="sequenceOrder"></param>
		/// <param name="pageDescription"></param>
		/// <param name="illustration"></param>
		/// <param name="note"></param>
		/// <param name="fileSize_Temp"></param>
		/// <param name="fileExtension"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <param name="active"></param>
		/// <param name="year"></param>
		/// <param name="series"></param>
		/// <param name="volume"></param>
		/// <param name="issue"></param>
		/// <param name="externalURL"></param>
		/// <param name="issuePrefix"></param>
		/// <param name="lastPageNameLookupDate"></param>
		/// <param name="paginationUserID"></param>
		/// <param name="paginationDate"></param>
		/// <param name="altExternalURL"></param>
		/// <returns>Object of type Page.</returns>
		public Page PageUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int pageID,
			int? itemID,
			string fileNamePrefix,
			int? sequenceOrder,
			string pageDescription,
			bool illustration,
			string note,
			int? fileSize_Temp,
			string fileExtension,
			int? lastModifiedUserID,
			bool active,
			string year,
			string series,
			string volume,
			string issue,
			string externalURL,
			string issuePrefix,
			DateTime? lastPageNameLookupDate,
			int? paginationUserID,
			DateTime? paginationDate,
			string altExternalURL)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("PageUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("PageID", SqlDbType.Int, null, false, pageID),
					CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, true, itemID),
					CustomSqlHelper.CreateInputParameter("FileNamePrefix", SqlDbType.NVarChar, 200, false, fileNamePrefix),
					CustomSqlHelper.CreateInputParameter("SequenceOrder", SqlDbType.Int, null, true, sequenceOrder),
					CustomSqlHelper.CreateInputParameter("PageDescription", SqlDbType.NVarChar, 255, true, pageDescription),
					CustomSqlHelper.CreateInputParameter("Illustration", SqlDbType.Bit, null, false, illustration),
					CustomSqlHelper.CreateInputParameter("Note", SqlDbType.NVarChar, 255, true, note),
					CustomSqlHelper.CreateInputParameter("FileSize_Temp", SqlDbType.Int, null, true, fileSize_Temp),
					CustomSqlHelper.CreateInputParameter("FileExtension", SqlDbType.NVarChar, 5, true, fileExtension),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, true, lastModifiedUserID),
					CustomSqlHelper.CreateInputParameter("Active", SqlDbType.Bit, null, false, active),
					CustomSqlHelper.CreateInputParameter("Year", SqlDbType.NVarChar, 20, true, year),
					CustomSqlHelper.CreateInputParameter("Series", SqlDbType.NVarChar, 20, true, series),
					CustomSqlHelper.CreateInputParameter("Volume", SqlDbType.NVarChar, 20, true, volume),
					CustomSqlHelper.CreateInputParameter("Issue", SqlDbType.NVarChar, 20, true, issue),
					CustomSqlHelper.CreateInputParameter("ExternalURL", SqlDbType.NVarChar, 500, true, externalURL),
					CustomSqlHelper.CreateInputParameter("IssuePrefix", SqlDbType.NVarChar, 20, true, issuePrefix),
					CustomSqlHelper.CreateInputParameter("LastPageNameLookupDate", SqlDbType.DateTime, null, true, lastPageNameLookupDate),
					CustomSqlHelper.CreateInputParameter("PaginationUserID", SqlDbType.Int, null, true, paginationUserID),
					CustomSqlHelper.CreateInputParameter("PaginationDate", SqlDbType.DateTime, null, true, paginationDate),
					CustomSqlHelper.CreateInputParameter("AltExternalURL", SqlDbType.NVarChar, 500, true, altExternalURL), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<Page> helper = new CustomSqlHelper<Page>())
				{
					List<Page> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						Page o = list[0];
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
		/// Update values in dbo.Page. Returns an object of type Page.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type Page.</param>
		/// <returns>Object of type Page.</returns>
		public Page PageUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			Page value)
		{
			return PageUpdateAuto(sqlConnection, sqlTransaction, "BHL", value );
		}
		
		/// <summary>
		/// Update values in dbo.Page. Returns an object of type Page.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type Page.</param>
		/// <returns>Object of type Page.</returns>
		public Page PageUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			Page value)
		{
			return PageUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.PageID,
				value.ItemID,
				value.FileNamePrefix,
				value.SequenceOrder,
				value.PageDescription,
				value.Illustration,
				value.Note,
				value.FileSize_Temp,
				value.FileExtension,
				value.LastModifiedUserID,
				value.Active,
				value.Year,
				value.Series,
				value.Volume,
				value.Issue,
				value.ExternalURL,
				value.IssuePrefix,
				value.LastPageNameLookupDate,
				value.PaginationUserID,
				value.PaginationDate,
				value.AltExternalURL);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage dbo.Page object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.Page.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type Page.</param>
		/// <returns>Object of type CustomDataAccessStatus<Page>.</returns>
		public CustomDataAccessStatus<Page> PageManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			Page value , int userId )
		{
			return PageManageAuto( sqlConnection, sqlTransaction, "BHL", value , userId );
		}
		
		/// <summary>
		/// Manage dbo.Page object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.Page.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type Page.</param>
		/// <returns>Object of type CustomDataAccessStatus<Page>.</returns>
		public CustomDataAccessStatus<Page> PageManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			Page value , int userId )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				value.CreationUserID = userId;
				value.LastModifiedUserID = userId;
				Page returnValue = PageInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.ItemID,
						value.FileNamePrefix,
						value.SequenceOrder,
						value.PageDescription,
						value.Illustration,
						value.Note,
						value.FileSize_Temp,
						value.FileExtension,
						value.CreationUserID,
						value.LastModifiedUserID,
						value.Active,
						value.Year,
						value.Series,
						value.Volume,
						value.Issue,
						value.ExternalURL,
						value.IssuePrefix,
						value.LastPageNameLookupDate,
						value.PaginationUserID,
						value.PaginationDate,
						value.AltExternalURL);
				
				return new CustomDataAccessStatus<Page>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (PageDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.PageID))
				{
				return new CustomDataAccessStatus<Page>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<Page>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				value.LastModifiedUserID = userId;
				Page returnValue = PageUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.PageID,
						value.ItemID,
						value.FileNamePrefix,
						value.SequenceOrder,
						value.PageDescription,
						value.Illustration,
						value.Note,
						value.FileSize_Temp,
						value.FileExtension,
						value.LastModifiedUserID,
						value.Active,
						value.Year,
						value.Series,
						value.Volume,
						value.Issue,
						value.ExternalURL,
						value.IssuePrefix,
						value.LastPageNameLookupDate,
						value.PaginationUserID,
						value.PaginationDate,
						value.AltExternalURL);
					
				return new CustomDataAccessStatus<Page>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<Page>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}

