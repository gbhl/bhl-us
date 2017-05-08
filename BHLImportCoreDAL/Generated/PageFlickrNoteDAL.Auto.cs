
// Generated 5/5/2017 4:51:32 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class PageFlickrNoteDAL is based upon dbo.PageFlickrNote.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHLImport.DAL
// {
// 		public partial class PageFlickrNoteDAL
//		{
//		}
// }

#endregion How To Implement

#region using

using System;
using System.Data;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHLImport.DataObjects;

#endregion using

namespace MOBOT.BHLImport.DAL
{
	partial class PageFlickrNoteDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from dbo.PageFlickrNote by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="pageFlickrNoteID"></param>
		/// <returns>Object of type PageFlickrNote.</returns>
		public PageFlickrNote PageFlickrNoteSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int pageFlickrNoteID)
		{
			return PageFlickrNoteSelectAuto(	sqlConnection, sqlTransaction, "BHLImport",	pageFlickrNoteID );
		}
			
		/// <summary>
		/// Select values from dbo.PageFlickrNote by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="pageFlickrNoteID"></param>
		/// <returns>Object of type PageFlickrNote.</returns>
		public PageFlickrNote PageFlickrNoteSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int pageFlickrNoteID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("PageFlickrNoteSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("PageFlickrNoteID", SqlDbType.Int, null, false, pageFlickrNoteID)))
			{
				using (CustomSqlHelper<PageFlickrNote> helper = new CustomSqlHelper<PageFlickrNote>())
				{
					CustomGenericList<PageFlickrNote> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						PageFlickrNote o = list[0];
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
		/// Select values from dbo.PageFlickrNote by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="pageFlickrNoteID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> PageFlickrNoteSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int pageFlickrNoteID)
		{
			return PageFlickrNoteSelectAutoRaw( sqlConnection, sqlTransaction, "BHLImport", pageFlickrNoteID );
		}
		
		/// <summary>
		/// Select values from dbo.PageFlickrNote by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="pageFlickrNoteID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> PageFlickrNoteSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int pageFlickrNoteID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("PageFlickrNoteSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("PageFlickrNoteID", SqlDbType.Int, null, false, pageFlickrNoteID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====

 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into dbo.PageFlickrNote.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="pageID"></param>
		/// <param name="photoID"></param>
		/// <param name="flickrNoteID"></param>
		/// <param name="flickrAuthorID"></param>
		/// <param name="flickrAuthorName"></param>
		/// <param name="flickrAuthorRealName"></param>
		/// <param name="authorIsPro"></param>
		/// <param name="xCoord"></param>
		/// <param name="yCoord"></param>
		/// <param name="width"></param>
		/// <param name="height"></param>
		/// <param name="noteValue"></param>
		/// <param name="isActive"></param>
		/// <param name="deleteDate"></param>
		/// <returns>Object of type PageFlickrNote.</returns>
		public PageFlickrNote PageFlickrNoteInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int pageID,
			string photoID,
			string flickrNoteID,
			string flickrAuthorID,
			string flickrAuthorName,
			string flickrAuthorRealName,
			short authorIsPro,
			int? xCoord,
			int? yCoord,
			int? width,
			int? height,
			string noteValue,
			byte isActive,
			DateTime? deleteDate)
		{
			return PageFlickrNoteInsertAuto( sqlConnection, sqlTransaction, "BHLImport", pageID, photoID, flickrNoteID, flickrAuthorID, flickrAuthorName, flickrAuthorRealName, authorIsPro, xCoord, yCoord, width, height, noteValue, isActive, deleteDate );
		}
		
		/// <summary>
		/// Insert values into dbo.PageFlickrNote.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="pageID"></param>
		/// <param name="photoID"></param>
		/// <param name="flickrNoteID"></param>
		/// <param name="flickrAuthorID"></param>
		/// <param name="flickrAuthorName"></param>
		/// <param name="flickrAuthorRealName"></param>
		/// <param name="authorIsPro"></param>
		/// <param name="xCoord"></param>
		/// <param name="yCoord"></param>
		/// <param name="width"></param>
		/// <param name="height"></param>
		/// <param name="noteValue"></param>
		/// <param name="isActive"></param>
		/// <param name="deleteDate"></param>
		/// <returns>Object of type PageFlickrNote.</returns>
		public PageFlickrNote PageFlickrNoteInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int pageID,
			string photoID,
			string flickrNoteID,
			string flickrAuthorID,
			string flickrAuthorName,
			string flickrAuthorRealName,
			short authorIsPro,
			int? xCoord,
			int? yCoord,
			int? width,
			int? height,
			string noteValue,
			byte isActive,
			DateTime? deleteDate)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("PageFlickrNoteInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("PageFlickrNoteID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("PageID", SqlDbType.Int, null, false, pageID),
					CustomSqlHelper.CreateInputParameter("PhotoID", SqlDbType.NVarChar, 50, false, photoID),
					CustomSqlHelper.CreateInputParameter("FlickrNoteID", SqlDbType.NVarChar, 100, false, flickrNoteID),
					CustomSqlHelper.CreateInputParameter("FlickrAuthorID", SqlDbType.NVarChar, 100, false, flickrAuthorID),
					CustomSqlHelper.CreateInputParameter("FlickrAuthorName", SqlDbType.NVarChar, 150, false, flickrAuthorName),
					CustomSqlHelper.CreateInputParameter("FlickrAuthorRealName", SqlDbType.NVarChar, 150, false, flickrAuthorRealName),
					CustomSqlHelper.CreateInputParameter("AuthorIsPro", SqlDbType.SmallInt, null, false, authorIsPro),
					CustomSqlHelper.CreateInputParameter("XCoord", SqlDbType.Int, null, true, xCoord),
					CustomSqlHelper.CreateInputParameter("YCoord", SqlDbType.Int, null, true, yCoord),
					CustomSqlHelper.CreateInputParameter("Width", SqlDbType.Int, null, true, width),
					CustomSqlHelper.CreateInputParameter("Height", SqlDbType.Int, null, true, height),
					CustomSqlHelper.CreateInputParameter("NoteValue", SqlDbType.NVarChar, 1073741823, false, noteValue),
					CustomSqlHelper.CreateInputParameter("IsActive", SqlDbType.TinyInt, null, false, isActive),
					CustomSqlHelper.CreateInputParameter("DeleteDate", SqlDbType.DateTime, null, true, deleteDate), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<PageFlickrNote> helper = new CustomSqlHelper<PageFlickrNote>())
				{
					CustomGenericList<PageFlickrNote> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						PageFlickrNote o = list[0];
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
		/// Insert values into dbo.PageFlickrNote. Returns an object of type PageFlickrNote.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type PageFlickrNote.</param>
		/// <returns>Object of type PageFlickrNote.</returns>
		public PageFlickrNote PageFlickrNoteInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			PageFlickrNote value)
		{
			return PageFlickrNoteInsertAuto(sqlConnection, sqlTransaction, "BHLImport", value);
		}
		
		/// <summary>
		/// Insert values into dbo.PageFlickrNote. Returns an object of type PageFlickrNote.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type PageFlickrNote.</param>
		/// <returns>Object of type PageFlickrNote.</returns>
		public PageFlickrNote PageFlickrNoteInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			PageFlickrNote value)
		{
			return PageFlickrNoteInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.PageID,
				value.PhotoID,
				value.FlickrNoteID,
				value.FlickrAuthorID,
				value.FlickrAuthorName,
				value.FlickrAuthorRealName,
				value.AuthorIsPro,
				value.XCoord,
				value.YCoord,
				value.Width,
				value.Height,
				value.NoteValue,
				value.IsActive,
				value.DeleteDate);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from dbo.PageFlickrNote by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="pageFlickrNoteID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool PageFlickrNoteDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int pageFlickrNoteID)
		{
			return PageFlickrNoteDeleteAuto( sqlConnection, sqlTransaction, "BHLImport", pageFlickrNoteID );
		}
		
		/// <summary>
		/// Delete values from dbo.PageFlickrNote by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="pageFlickrNoteID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool PageFlickrNoteDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int pageFlickrNoteID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("PageFlickrNoteDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("PageFlickrNoteID", SqlDbType.Int, null, false, pageFlickrNoteID), 
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
		/// Update values in dbo.PageFlickrNote. Returns an object of type PageFlickrNote.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="pageFlickrNoteID"></param>
		/// <param name="pageID"></param>
		/// <param name="photoID"></param>
		/// <param name="flickrNoteID"></param>
		/// <param name="flickrAuthorID"></param>
		/// <param name="flickrAuthorName"></param>
		/// <param name="flickrAuthorRealName"></param>
		/// <param name="authorIsPro"></param>
		/// <param name="xCoord"></param>
		/// <param name="yCoord"></param>
		/// <param name="width"></param>
		/// <param name="height"></param>
		/// <param name="noteValue"></param>
		/// <param name="isActive"></param>
		/// <param name="deleteDate"></param>
		/// <returns>Object of type PageFlickrNote.</returns>
		public PageFlickrNote PageFlickrNoteUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int pageFlickrNoteID,
			int pageID,
			string photoID,
			string flickrNoteID,
			string flickrAuthorID,
			string flickrAuthorName,
			string flickrAuthorRealName,
			short authorIsPro,
			int? xCoord,
			int? yCoord,
			int? width,
			int? height,
			string noteValue,
			byte isActive,
			DateTime? deleteDate)
		{
			return PageFlickrNoteUpdateAuto( sqlConnection, sqlTransaction, "BHLImport", pageFlickrNoteID, pageID, photoID, flickrNoteID, flickrAuthorID, flickrAuthorName, flickrAuthorRealName, authorIsPro, xCoord, yCoord, width, height, noteValue, isActive, deleteDate);
		}
		
		/// <summary>
		/// Update values in dbo.PageFlickrNote. Returns an object of type PageFlickrNote.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="pageFlickrNoteID"></param>
		/// <param name="pageID"></param>
		/// <param name="photoID"></param>
		/// <param name="flickrNoteID"></param>
		/// <param name="flickrAuthorID"></param>
		/// <param name="flickrAuthorName"></param>
		/// <param name="flickrAuthorRealName"></param>
		/// <param name="authorIsPro"></param>
		/// <param name="xCoord"></param>
		/// <param name="yCoord"></param>
		/// <param name="width"></param>
		/// <param name="height"></param>
		/// <param name="noteValue"></param>
		/// <param name="isActive"></param>
		/// <param name="deleteDate"></param>
		/// <returns>Object of type PageFlickrNote.</returns>
		public PageFlickrNote PageFlickrNoteUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int pageFlickrNoteID,
			int pageID,
			string photoID,
			string flickrNoteID,
			string flickrAuthorID,
			string flickrAuthorName,
			string flickrAuthorRealName,
			short authorIsPro,
			int? xCoord,
			int? yCoord,
			int? width,
			int? height,
			string noteValue,
			byte isActive,
			DateTime? deleteDate)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("PageFlickrNoteUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("PageFlickrNoteID", SqlDbType.Int, null, false, pageFlickrNoteID),
					CustomSqlHelper.CreateInputParameter("PageID", SqlDbType.Int, null, false, pageID),
					CustomSqlHelper.CreateInputParameter("PhotoID", SqlDbType.NVarChar, 50, false, photoID),
					CustomSqlHelper.CreateInputParameter("FlickrNoteID", SqlDbType.NVarChar, 100, false, flickrNoteID),
					CustomSqlHelper.CreateInputParameter("FlickrAuthorID", SqlDbType.NVarChar, 100, false, flickrAuthorID),
					CustomSqlHelper.CreateInputParameter("FlickrAuthorName", SqlDbType.NVarChar, 150, false, flickrAuthorName),
					CustomSqlHelper.CreateInputParameter("FlickrAuthorRealName", SqlDbType.NVarChar, 150, false, flickrAuthorRealName),
					CustomSqlHelper.CreateInputParameter("AuthorIsPro", SqlDbType.SmallInt, null, false, authorIsPro),
					CustomSqlHelper.CreateInputParameter("XCoord", SqlDbType.Int, null, true, xCoord),
					CustomSqlHelper.CreateInputParameter("YCoord", SqlDbType.Int, null, true, yCoord),
					CustomSqlHelper.CreateInputParameter("Width", SqlDbType.Int, null, true, width),
					CustomSqlHelper.CreateInputParameter("Height", SqlDbType.Int, null, true, height),
					CustomSqlHelper.CreateInputParameter("NoteValue", SqlDbType.NVarChar, 1073741823, false, noteValue),
					CustomSqlHelper.CreateInputParameter("IsActive", SqlDbType.TinyInt, null, false, isActive),
					CustomSqlHelper.CreateInputParameter("DeleteDate", SqlDbType.DateTime, null, true, deleteDate), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<PageFlickrNote> helper = new CustomSqlHelper<PageFlickrNote>())
				{
					CustomGenericList<PageFlickrNote> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						PageFlickrNote o = list[0];
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
		/// Update values in dbo.PageFlickrNote. Returns an object of type PageFlickrNote.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type PageFlickrNote.</param>
		/// <returns>Object of type PageFlickrNote.</returns>
		public PageFlickrNote PageFlickrNoteUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			PageFlickrNote value)
		{
			return PageFlickrNoteUpdateAuto(sqlConnection, sqlTransaction, "BHLImport", value );
		}
		
		/// <summary>
		/// Update values in dbo.PageFlickrNote. Returns an object of type PageFlickrNote.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type PageFlickrNote.</param>
		/// <returns>Object of type PageFlickrNote.</returns>
		public PageFlickrNote PageFlickrNoteUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			PageFlickrNote value)
		{
			return PageFlickrNoteUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.PageFlickrNoteID,
				value.PageID,
				value.PhotoID,
				value.FlickrNoteID,
				value.FlickrAuthorID,
				value.FlickrAuthorName,
				value.FlickrAuthorRealName,
				value.AuthorIsPro,
				value.XCoord,
				value.YCoord,
				value.Width,
				value.Height,
				value.NoteValue,
				value.IsActive,
				value.DeleteDate);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage dbo.PageFlickrNote object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.PageFlickrNote.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type PageFlickrNote.</param>
		/// <returns>Object of type CustomDataAccessStatus<PageFlickrNote>.</returns>
		public CustomDataAccessStatus<PageFlickrNote> PageFlickrNoteManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			PageFlickrNote value  )
		{
			return PageFlickrNoteManageAuto( sqlConnection, sqlTransaction, "BHLImport", value  );
		}
		
		/// <summary>
		/// Manage dbo.PageFlickrNote object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.PageFlickrNote.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type PageFlickrNote.</param>
		/// <returns>Object of type CustomDataAccessStatus<PageFlickrNote>.</returns>
		public CustomDataAccessStatus<PageFlickrNote> PageFlickrNoteManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			PageFlickrNote value  )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				
				
				PageFlickrNote returnValue = PageFlickrNoteInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.PageID,
						value.PhotoID,
						value.FlickrNoteID,
						value.FlickrAuthorID,
						value.FlickrAuthorName,
						value.FlickrAuthorRealName,
						value.AuthorIsPro,
						value.XCoord,
						value.YCoord,
						value.Width,
						value.Height,
						value.NoteValue,
						value.IsActive,
						value.DeleteDate);
				
				return new CustomDataAccessStatus<PageFlickrNote>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (PageFlickrNoteDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.PageFlickrNoteID))
				{
				return new CustomDataAccessStatus<PageFlickrNote>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<PageFlickrNote>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				
				PageFlickrNote returnValue = PageFlickrNoteUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.PageFlickrNoteID,
						value.PageID,
						value.PhotoID,
						value.FlickrNoteID,
						value.FlickrAuthorID,
						value.FlickrAuthorName,
						value.FlickrAuthorRealName,
						value.AuthorIsPro,
						value.XCoord,
						value.YCoord,
						value.Width,
						value.Height,
						value.NoteValue,
						value.IsActive,
						value.DeleteDate);
					
				return new CustomDataAccessStatus<PageFlickrNote>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<PageFlickrNote>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}

