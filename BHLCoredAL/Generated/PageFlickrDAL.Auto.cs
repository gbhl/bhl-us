
// Generated 1/5/2021 3:26:36 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class PageFlickrDAL is based upon dbo.PageFlickr.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DAL
// {
// 		public partial class PageFlickrDAL
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
	partial class PageFlickrDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from dbo.PageFlickr by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="pageFlickrID"></param>
		/// <returns>Object of type PageFlickr.</returns>
		public PageFlickr PageFlickrSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int pageFlickrID)
		{
			return PageFlickrSelectAuto(	sqlConnection, sqlTransaction, "BHL",	pageFlickrID );
		}
			
		/// <summary>
		/// Select values from dbo.PageFlickr by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="pageFlickrID"></param>
		/// <returns>Object of type PageFlickr.</returns>
		public PageFlickr PageFlickrSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int pageFlickrID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("PageFlickrSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("PageFlickrID", SqlDbType.Int, null, false, pageFlickrID)))
			{
				using (CustomSqlHelper<PageFlickr> helper = new CustomSqlHelper<PageFlickr>())
				{
					List<PageFlickr> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						PageFlickr o = list[0];
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
		/// Select values from dbo.PageFlickr by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="pageFlickrID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> PageFlickrSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int pageFlickrID)
		{
			return PageFlickrSelectAutoRaw( sqlConnection, sqlTransaction, "BHL", pageFlickrID );
		}
		
		/// <summary>
		/// Select values from dbo.PageFlickr by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="pageFlickrID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> PageFlickrSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int pageFlickrID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("PageFlickrSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("PageFlickrID", SqlDbType.Int, null, false, pageFlickrID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====

 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into dbo.PageFlickr.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="pageID"></param>
		/// <param name="flickrURL"></param>
		/// <param name="creationUserID"></param>
		/// <returns>Object of type PageFlickr.</returns>
		public PageFlickr PageFlickrInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int pageID,
			string flickrURL,
			int? creationUserID)
		{
			return PageFlickrInsertAuto( sqlConnection, sqlTransaction, "BHL", pageID, flickrURL, creationUserID );
		}
		
		/// <summary>
		/// Insert values into dbo.PageFlickr.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="pageID"></param>
		/// <param name="flickrURL"></param>
		/// <param name="creationUserID"></param>
		/// <returns>Object of type PageFlickr.</returns>
		public PageFlickr PageFlickrInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int pageID,
			string flickrURL,
			int? creationUserID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("PageFlickrInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("PageFlickrID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("PageID", SqlDbType.Int, null, false, pageID),
					CustomSqlHelper.CreateInputParameter("FlickrURL", SqlDbType.VarChar, 500, false, flickrURL),
					CustomSqlHelper.CreateInputParameter("CreationUserID", SqlDbType.Int, null, true, creationUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<PageFlickr> helper = new CustomSqlHelper<PageFlickr>())
				{
					List<PageFlickr> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						PageFlickr o = list[0];
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
		/// Insert values into dbo.PageFlickr. Returns an object of type PageFlickr.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type PageFlickr.</param>
		/// <returns>Object of type PageFlickr.</returns>
		public PageFlickr PageFlickrInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			PageFlickr value)
		{
			return PageFlickrInsertAuto(sqlConnection, sqlTransaction, "BHL", value);
		}
		
		/// <summary>
		/// Insert values into dbo.PageFlickr. Returns an object of type PageFlickr.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type PageFlickr.</param>
		/// <returns>Object of type PageFlickr.</returns>
		public PageFlickr PageFlickrInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			PageFlickr value)
		{
			return PageFlickrInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.PageID,
				value.FlickrURL,
				value.CreationUserID);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from dbo.PageFlickr by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="pageFlickrID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool PageFlickrDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int pageFlickrID)
		{
			return PageFlickrDeleteAuto( sqlConnection, sqlTransaction, "BHL", pageFlickrID );
		}
		
		/// <summary>
		/// Delete values from dbo.PageFlickr by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="pageFlickrID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool PageFlickrDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int pageFlickrID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("PageFlickrDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("PageFlickrID", SqlDbType.Int, null, false, pageFlickrID), 
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
		/// Update values in dbo.PageFlickr. Returns an object of type PageFlickr.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="pageFlickrID"></param>
		/// <param name="pageID"></param>
		/// <param name="flickrURL"></param>
		/// <returns>Object of type PageFlickr.</returns>
		public PageFlickr PageFlickrUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int pageFlickrID,
			int pageID,
			string flickrURL)
		{
			return PageFlickrUpdateAuto( sqlConnection, sqlTransaction, "BHL", pageFlickrID, pageID, flickrURL);
		}
		
		/// <summary>
		/// Update values in dbo.PageFlickr. Returns an object of type PageFlickr.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="pageFlickrID"></param>
		/// <param name="pageID"></param>
		/// <param name="flickrURL"></param>
		/// <returns>Object of type PageFlickr.</returns>
		public PageFlickr PageFlickrUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int pageFlickrID,
			int pageID,
			string flickrURL)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("PageFlickrUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("PageFlickrID", SqlDbType.Int, null, false, pageFlickrID),
					CustomSqlHelper.CreateInputParameter("PageID", SqlDbType.Int, null, false, pageID),
					CustomSqlHelper.CreateInputParameter("FlickrURL", SqlDbType.VarChar, 500, false, flickrURL), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<PageFlickr> helper = new CustomSqlHelper<PageFlickr>())
				{
					List<PageFlickr> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						PageFlickr o = list[0];
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
		/// Update values in dbo.PageFlickr. Returns an object of type PageFlickr.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type PageFlickr.</param>
		/// <returns>Object of type PageFlickr.</returns>
		public PageFlickr PageFlickrUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			PageFlickr value)
		{
			return PageFlickrUpdateAuto(sqlConnection, sqlTransaction, "BHL", value );
		}
		
		/// <summary>
		/// Update values in dbo.PageFlickr. Returns an object of type PageFlickr.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type PageFlickr.</param>
		/// <returns>Object of type PageFlickr.</returns>
		public PageFlickr PageFlickrUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			PageFlickr value)
		{
			return PageFlickrUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.PageFlickrID,
				value.PageID,
				value.FlickrURL);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage dbo.PageFlickr object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.PageFlickr.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type PageFlickr.</param>
		/// <returns>Object of type CustomDataAccessStatus<PageFlickr>.</returns>
		public CustomDataAccessStatus<PageFlickr> PageFlickrManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			PageFlickr value , int userId )
		{
			return PageFlickrManageAuto( sqlConnection, sqlTransaction, "BHL", value , userId );
		}
		
		/// <summary>
		/// Manage dbo.PageFlickr object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.PageFlickr.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type PageFlickr.</param>
		/// <returns>Object of type CustomDataAccessStatus<PageFlickr>.</returns>
		public CustomDataAccessStatus<PageFlickr> PageFlickrManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			PageFlickr value , int userId )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				value.CreationUserID = userId;
				
				PageFlickr returnValue = PageFlickrInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.PageID,
						value.FlickrURL,
						value.CreationUserID);
				
				return new CustomDataAccessStatus<PageFlickr>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (PageFlickrDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.PageFlickrID))
				{
				return new CustomDataAccessStatus<PageFlickr>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<PageFlickr>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				
				PageFlickr returnValue = PageFlickrUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.PageFlickrID,
						value.PageID,
						value.FlickrURL);
					
				return new CustomDataAccessStatus<PageFlickr>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<PageFlickr>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}

