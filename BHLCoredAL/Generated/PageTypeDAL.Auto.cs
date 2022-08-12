
// Generated 1/5/2021 3:26:40 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class PageTypeDAL is based upon dbo.PageType.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DAL
// {
// 		public partial class PageTypeDAL
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
	partial class PageTypeDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from dbo.PageType by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="pageTypeID"></param>
		/// <returns>Object of type PageType.</returns>
		public PageType PageTypeSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int pageTypeID)
		{
			return PageTypeSelectAuto(	sqlConnection, sqlTransaction, "BHL",	pageTypeID );
		}
			
		/// <summary>
		/// Select values from dbo.PageType by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="pageTypeID"></param>
		/// <returns>Object of type PageType.</returns>
		public PageType PageTypeSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int pageTypeID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("PageTypeSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("PageTypeID", SqlDbType.Int, null, false, pageTypeID)))
			{
				using (CustomSqlHelper<PageType> helper = new CustomSqlHelper<PageType>())
				{
					List<PageType> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						PageType o = list[0];
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
		/// Select values from dbo.PageType by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="pageTypeID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> PageTypeSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int pageTypeID)
		{
			return PageTypeSelectAutoRaw( sqlConnection, sqlTransaction, "BHL", pageTypeID );
		}
		
		/// <summary>
		/// Select values from dbo.PageType by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="pageTypeID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> PageTypeSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int pageTypeID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("PageTypeSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("PageTypeID", SqlDbType.Int, null, false, pageTypeID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====

 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into dbo.PageType.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="pageTypeName"></param>
		/// <param name="pageTypeDescription"></param>
		/// <param name="active"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type PageType.</returns>
		public PageType PageTypeInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string pageTypeName,
			string pageTypeDescription,
			byte active,
			int? creationUserID,
			int? lastModifiedUserID)
		{
			return PageTypeInsertAuto( sqlConnection, sqlTransaction, "BHL", pageTypeName, pageTypeDescription, active, creationUserID, lastModifiedUserID );
		}
		
		/// <summary>
		/// Insert values into dbo.PageType.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="pageTypeName"></param>
		/// <param name="pageTypeDescription"></param>
		/// <param name="active"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type PageType.</returns>
		public PageType PageTypeInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			string pageTypeName,
			string pageTypeDescription,
			byte active,
			int? creationUserID,
			int? lastModifiedUserID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("PageTypeInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("PageTypeID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("PageTypeName", SqlDbType.NVarChar, 30, false, pageTypeName),
					CustomSqlHelper.CreateInputParameter("PageTypeDescription", SqlDbType.NVarChar, 255, true, pageTypeDescription),
					CustomSqlHelper.CreateInputParameter("Active", SqlDbType.TinyInt, null, false, active),
					CustomSqlHelper.CreateInputParameter("CreationUserID", SqlDbType.Int, null, true, creationUserID),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, true, lastModifiedUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<PageType> helper = new CustomSqlHelper<PageType>())
				{
					List<PageType> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						PageType o = list[0];
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
		/// Insert values into dbo.PageType. Returns an object of type PageType.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type PageType.</param>
		/// <returns>Object of type PageType.</returns>
		public PageType PageTypeInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			PageType value)
		{
			return PageTypeInsertAuto(sqlConnection, sqlTransaction, "BHL", value);
		}
		
		/// <summary>
		/// Insert values into dbo.PageType. Returns an object of type PageType.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type PageType.</param>
		/// <returns>Object of type PageType.</returns>
		public PageType PageTypeInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			PageType value)
		{
			return PageTypeInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.PageTypeName,
				value.PageTypeDescription,
				value.Active,
				value.CreationUserID,
				value.LastModifiedUserID);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from dbo.PageType by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="pageTypeID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool PageTypeDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int pageTypeID)
		{
			return PageTypeDeleteAuto( sqlConnection, sqlTransaction, "BHL", pageTypeID );
		}
		
		/// <summary>
		/// Delete values from dbo.PageType by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="pageTypeID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool PageTypeDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int pageTypeID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("PageTypeDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("PageTypeID", SqlDbType.Int, null, false, pageTypeID), 
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
		/// Update values in dbo.PageType. Returns an object of type PageType.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="pageTypeID"></param>
		/// <param name="pageTypeName"></param>
		/// <param name="pageTypeDescription"></param>
		/// <param name="active"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type PageType.</returns>
		public PageType PageTypeUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int pageTypeID,
			string pageTypeName,
			string pageTypeDescription,
			byte active,
			int? lastModifiedUserID)
		{
			return PageTypeUpdateAuto( sqlConnection, sqlTransaction, "BHL", pageTypeID, pageTypeName, pageTypeDescription, active, lastModifiedUserID);
		}
		
		/// <summary>
		/// Update values in dbo.PageType. Returns an object of type PageType.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="pageTypeID"></param>
		/// <param name="pageTypeName"></param>
		/// <param name="pageTypeDescription"></param>
		/// <param name="active"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type PageType.</returns>
		public PageType PageTypeUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int pageTypeID,
			string pageTypeName,
			string pageTypeDescription,
			byte active,
			int? lastModifiedUserID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("PageTypeUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("PageTypeID", SqlDbType.Int, null, false, pageTypeID),
					CustomSqlHelper.CreateInputParameter("PageTypeName", SqlDbType.NVarChar, 30, false, pageTypeName),
					CustomSqlHelper.CreateInputParameter("PageTypeDescription", SqlDbType.NVarChar, 255, true, pageTypeDescription),
					CustomSqlHelper.CreateInputParameter("Active", SqlDbType.TinyInt, null, false, active),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, true, lastModifiedUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<PageType> helper = new CustomSqlHelper<PageType>())
				{
					List<PageType> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						PageType o = list[0];
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
		/// Update values in dbo.PageType. Returns an object of type PageType.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type PageType.</param>
		/// <returns>Object of type PageType.</returns>
		public PageType PageTypeUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			PageType value)
		{
			return PageTypeUpdateAuto(sqlConnection, sqlTransaction, "BHL", value );
		}
		
		/// <summary>
		/// Update values in dbo.PageType. Returns an object of type PageType.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type PageType.</param>
		/// <returns>Object of type PageType.</returns>
		public PageType PageTypeUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			PageType value)
		{
			return PageTypeUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.PageTypeID,
				value.PageTypeName,
				value.PageTypeDescription,
				value.Active,
				value.LastModifiedUserID);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage dbo.PageType object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.PageType.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type PageType.</param>
		/// <returns>Object of type CustomDataAccessStatus<PageType>.</returns>
		public CustomDataAccessStatus<PageType> PageTypeManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			PageType value , int userId )
		{
			return PageTypeManageAuto( sqlConnection, sqlTransaction, "BHL", value , userId );
		}
		
		/// <summary>
		/// Manage dbo.PageType object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.PageType.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type PageType.</param>
		/// <returns>Object of type CustomDataAccessStatus<PageType>.</returns>
		public CustomDataAccessStatus<PageType> PageTypeManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			PageType value , int userId )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				value.CreationUserID = userId;
				value.LastModifiedUserID = userId;
				PageType returnValue = PageTypeInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.PageTypeName,
						value.PageTypeDescription,
						value.Active,
						value.CreationUserID,
						value.LastModifiedUserID);
				
				return new CustomDataAccessStatus<PageType>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (PageTypeDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.PageTypeID))
				{
				return new CustomDataAccessStatus<PageType>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<PageType>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				value.LastModifiedUserID = userId;
				PageType returnValue = PageTypeUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.PageTypeID,
						value.PageTypeName,
						value.PageTypeDescription,
						value.Active,
						value.LastModifiedUserID);
					
				return new CustomDataAccessStatus<PageType>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<PageType>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}

