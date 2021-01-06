
// Generated 1/5/2021 2:18:05 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class PageFlickrTagDAL is based upon dbo.PageFlickrTag.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHLImport.DAL
// {
// 		public partial class PageFlickrTagDAL
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
using MOBOT.BHLImport.DataObjects;

#endregion using

namespace MOBOT.BHLImport.DAL
{
	partial class PageFlickrTagDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from dbo.PageFlickrTag by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="pageFlickrTagID"></param>
		/// <returns>Object of type PageFlickrTag.</returns>
		public PageFlickrTag PageFlickrTagSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int pageFlickrTagID)
		{
			return PageFlickrTagSelectAuto(	sqlConnection, sqlTransaction, "BHLImport",	pageFlickrTagID );
		}
			
		/// <summary>
		/// Select values from dbo.PageFlickrTag by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="pageFlickrTagID"></param>
		/// <returns>Object of type PageFlickrTag.</returns>
		public PageFlickrTag PageFlickrTagSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int pageFlickrTagID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("PageFlickrTagSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("PageFlickrTagID", SqlDbType.Int, null, false, pageFlickrTagID)))
			{
				using (CustomSqlHelper<PageFlickrTag> helper = new CustomSqlHelper<PageFlickrTag>())
				{
					List<PageFlickrTag> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						PageFlickrTag o = list[0];
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
		/// Select values from dbo.PageFlickrTag by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="pageFlickrTagID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> PageFlickrTagSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int pageFlickrTagID)
		{
			return PageFlickrTagSelectAutoRaw( sqlConnection, sqlTransaction, "BHLImport", pageFlickrTagID );
		}
		
		/// <summary>
		/// Select values from dbo.PageFlickrTag by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="pageFlickrTagID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> PageFlickrTagSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int pageFlickrTagID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("PageFlickrTagSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("PageFlickrTagID", SqlDbType.Int, null, false, pageFlickrTagID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====

 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into dbo.PageFlickrTag.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="pageID"></param>
		/// <param name="photoID"></param>
		/// <param name="isMachineTag"></param>
		/// <param name="tagValue"></param>
		/// <param name="flickrAuthorID"></param>
		/// <param name="flickrAuthorName"></param>
		/// <param name="isActive"></param>
		/// <param name="deleteDate"></param>
		/// <returns>Object of type PageFlickrTag.</returns>
		public PageFlickrTag PageFlickrTagInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int pageID,
			string photoID,
			short isMachineTag,
			string tagValue,
			string flickrAuthorID,
			string flickrAuthorName,
			byte isActive,
			DateTime? deleteDate)
		{
			return PageFlickrTagInsertAuto( sqlConnection, sqlTransaction, "BHLImport", pageID, photoID, isMachineTag, tagValue, flickrAuthorID, flickrAuthorName, isActive, deleteDate );
		}
		
		/// <summary>
		/// Insert values into dbo.PageFlickrTag.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="pageID"></param>
		/// <param name="photoID"></param>
		/// <param name="isMachineTag"></param>
		/// <param name="tagValue"></param>
		/// <param name="flickrAuthorID"></param>
		/// <param name="flickrAuthorName"></param>
		/// <param name="isActive"></param>
		/// <param name="deleteDate"></param>
		/// <returns>Object of type PageFlickrTag.</returns>
		public PageFlickrTag PageFlickrTagInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int pageID,
			string photoID,
			short isMachineTag,
			string tagValue,
			string flickrAuthorID,
			string flickrAuthorName,
			byte isActive,
			DateTime? deleteDate)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("PageFlickrTagInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("PageFlickrTagID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("PageID", SqlDbType.Int, null, false, pageID),
					CustomSqlHelper.CreateInputParameter("PhotoID", SqlDbType.NVarChar, 50, false, photoID),
					CustomSqlHelper.CreateInputParameter("IsMachineTag", SqlDbType.SmallInt, null, false, isMachineTag),
					CustomSqlHelper.CreateInputParameter("TagValue", SqlDbType.NVarChar, 1000, false, tagValue),
					CustomSqlHelper.CreateInputParameter("FlickrAuthorID", SqlDbType.NVarChar, 100, false, flickrAuthorID),
					CustomSqlHelper.CreateInputParameter("FlickrAuthorName", SqlDbType.NVarChar, 150, false, flickrAuthorName),
					CustomSqlHelper.CreateInputParameter("IsActive", SqlDbType.TinyInt, null, false, isActive),
					CustomSqlHelper.CreateInputParameter("DeleteDate", SqlDbType.DateTime, null, true, deleteDate), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<PageFlickrTag> helper = new CustomSqlHelper<PageFlickrTag>())
				{
					List<PageFlickrTag> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						PageFlickrTag o = list[0];
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
		/// Insert values into dbo.PageFlickrTag. Returns an object of type PageFlickrTag.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type PageFlickrTag.</param>
		/// <returns>Object of type PageFlickrTag.</returns>
		public PageFlickrTag PageFlickrTagInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			PageFlickrTag value)
		{
			return PageFlickrTagInsertAuto(sqlConnection, sqlTransaction, "BHLImport", value);
		}
		
		/// <summary>
		/// Insert values into dbo.PageFlickrTag. Returns an object of type PageFlickrTag.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type PageFlickrTag.</param>
		/// <returns>Object of type PageFlickrTag.</returns>
		public PageFlickrTag PageFlickrTagInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			PageFlickrTag value)
		{
			return PageFlickrTagInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.PageID,
				value.PhotoID,
				value.IsMachineTag,
				value.TagValue,
				value.FlickrAuthorID,
				value.FlickrAuthorName,
				value.IsActive,
				value.DeleteDate);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from dbo.PageFlickrTag by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="pageFlickrTagID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool PageFlickrTagDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int pageFlickrTagID)
		{
			return PageFlickrTagDeleteAuto( sqlConnection, sqlTransaction, "BHLImport", pageFlickrTagID );
		}
		
		/// <summary>
		/// Delete values from dbo.PageFlickrTag by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="pageFlickrTagID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool PageFlickrTagDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int pageFlickrTagID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("PageFlickrTagDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("PageFlickrTagID", SqlDbType.Int, null, false, pageFlickrTagID), 
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
		/// Update values in dbo.PageFlickrTag. Returns an object of type PageFlickrTag.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="pageFlickrTagID"></param>
		/// <param name="pageID"></param>
		/// <param name="photoID"></param>
		/// <param name="isMachineTag"></param>
		/// <param name="tagValue"></param>
		/// <param name="flickrAuthorID"></param>
		/// <param name="flickrAuthorName"></param>
		/// <param name="isActive"></param>
		/// <param name="deleteDate"></param>
		/// <returns>Object of type PageFlickrTag.</returns>
		public PageFlickrTag PageFlickrTagUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int pageFlickrTagID,
			int pageID,
			string photoID,
			short isMachineTag,
			string tagValue,
			string flickrAuthorID,
			string flickrAuthorName,
			byte isActive,
			DateTime? deleteDate)
		{
			return PageFlickrTagUpdateAuto( sqlConnection, sqlTransaction, "BHLImport", pageFlickrTagID, pageID, photoID, isMachineTag, tagValue, flickrAuthorID, flickrAuthorName, isActive, deleteDate);
		}
		
		/// <summary>
		/// Update values in dbo.PageFlickrTag. Returns an object of type PageFlickrTag.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="pageFlickrTagID"></param>
		/// <param name="pageID"></param>
		/// <param name="photoID"></param>
		/// <param name="isMachineTag"></param>
		/// <param name="tagValue"></param>
		/// <param name="flickrAuthorID"></param>
		/// <param name="flickrAuthorName"></param>
		/// <param name="isActive"></param>
		/// <param name="deleteDate"></param>
		/// <returns>Object of type PageFlickrTag.</returns>
		public PageFlickrTag PageFlickrTagUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int pageFlickrTagID,
			int pageID,
			string photoID,
			short isMachineTag,
			string tagValue,
			string flickrAuthorID,
			string flickrAuthorName,
			byte isActive,
			DateTime? deleteDate)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("PageFlickrTagUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("PageFlickrTagID", SqlDbType.Int, null, false, pageFlickrTagID),
					CustomSqlHelper.CreateInputParameter("PageID", SqlDbType.Int, null, false, pageID),
					CustomSqlHelper.CreateInputParameter("PhotoID", SqlDbType.NVarChar, 50, false, photoID),
					CustomSqlHelper.CreateInputParameter("IsMachineTag", SqlDbType.SmallInt, null, false, isMachineTag),
					CustomSqlHelper.CreateInputParameter("TagValue", SqlDbType.NVarChar, 1000, false, tagValue),
					CustomSqlHelper.CreateInputParameter("FlickrAuthorID", SqlDbType.NVarChar, 100, false, flickrAuthorID),
					CustomSqlHelper.CreateInputParameter("FlickrAuthorName", SqlDbType.NVarChar, 150, false, flickrAuthorName),
					CustomSqlHelper.CreateInputParameter("IsActive", SqlDbType.TinyInt, null, false, isActive),
					CustomSqlHelper.CreateInputParameter("DeleteDate", SqlDbType.DateTime, null, true, deleteDate), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<PageFlickrTag> helper = new CustomSqlHelper<PageFlickrTag>())
				{
					List<PageFlickrTag> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						PageFlickrTag o = list[0];
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
		/// Update values in dbo.PageFlickrTag. Returns an object of type PageFlickrTag.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type PageFlickrTag.</param>
		/// <returns>Object of type PageFlickrTag.</returns>
		public PageFlickrTag PageFlickrTagUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			PageFlickrTag value)
		{
			return PageFlickrTagUpdateAuto(sqlConnection, sqlTransaction, "BHLImport", value );
		}
		
		/// <summary>
		/// Update values in dbo.PageFlickrTag. Returns an object of type PageFlickrTag.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type PageFlickrTag.</param>
		/// <returns>Object of type PageFlickrTag.</returns>
		public PageFlickrTag PageFlickrTagUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			PageFlickrTag value)
		{
			return PageFlickrTagUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.PageFlickrTagID,
				value.PageID,
				value.PhotoID,
				value.IsMachineTag,
				value.TagValue,
				value.FlickrAuthorID,
				value.FlickrAuthorName,
				value.IsActive,
				value.DeleteDate);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage dbo.PageFlickrTag object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.PageFlickrTag.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type PageFlickrTag.</param>
		/// <returns>Object of type CustomDataAccessStatus<PageFlickrTag>.</returns>
		public CustomDataAccessStatus<PageFlickrTag> PageFlickrTagManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			PageFlickrTag value  )
		{
			return PageFlickrTagManageAuto( sqlConnection, sqlTransaction, "BHLImport", value  );
		}
		
		/// <summary>
		/// Manage dbo.PageFlickrTag object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.PageFlickrTag.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type PageFlickrTag.</param>
		/// <returns>Object of type CustomDataAccessStatus<PageFlickrTag>.</returns>
		public CustomDataAccessStatus<PageFlickrTag> PageFlickrTagManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			PageFlickrTag value  )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				
				
				PageFlickrTag returnValue = PageFlickrTagInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.PageID,
						value.PhotoID,
						value.IsMachineTag,
						value.TagValue,
						value.FlickrAuthorID,
						value.FlickrAuthorName,
						value.IsActive,
						value.DeleteDate);
				
				return new CustomDataAccessStatus<PageFlickrTag>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (PageFlickrTagDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.PageFlickrTagID))
				{
				return new CustomDataAccessStatus<PageFlickrTag>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<PageFlickrTag>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				
				PageFlickrTag returnValue = PageFlickrTagUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.PageFlickrTagID,
						value.PageID,
						value.PhotoID,
						value.IsMachineTag,
						value.TagValue,
						value.FlickrAuthorID,
						value.FlickrAuthorName,
						value.IsActive,
						value.DeleteDate);
					
				return new CustomDataAccessStatus<PageFlickrTag>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<PageFlickrTag>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}

