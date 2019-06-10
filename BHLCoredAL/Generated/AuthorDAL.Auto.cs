
// Generated 6/6/2019 11:14:00 AM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class AuthorDAL is based upon dbo.Author.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DAL
// {
// 		public partial class AuthorDAL
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
	partial class AuthorDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from dbo.Author by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="authorID"></param>
		/// <returns>Object of type Author.</returns>
		public Author AuthorSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int authorID)
		{
			return AuthorSelectAuto(	sqlConnection, sqlTransaction, "BHL",	authorID );
		}
			
		/// <summary>
		/// Select values from dbo.Author by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="authorID"></param>
		/// <returns>Object of type Author.</returns>
		public Author AuthorSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int authorID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("AuthorSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("AuthorID", SqlDbType.Int, null, false, authorID)))
			{
				using (CustomSqlHelper<Author> helper = new CustomSqlHelper<Author>())
				{
					CustomGenericList<Author> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						Author o = list[0];
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
		/// Select values from dbo.Author by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="authorID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> AuthorSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int authorID)
		{
			return AuthorSelectAutoRaw( sqlConnection, sqlTransaction, "BHL", authorID );
		}
		
		/// <summary>
		/// Select values from dbo.Author by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="authorID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> AuthorSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int authorID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("AuthorSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("AuthorID", SqlDbType.Int, null, false, authorID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====

 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into dbo.Author.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="authorTypeID"></param>
		/// <param name="startDate"></param>
		/// <param name="endDate"></param>
		/// <param name="numeration"></param>
		/// <param name="title"></param>
		/// <param name="unit"></param>
		/// <param name="location"></param>
		/// <param name="note"></param>
		/// <param name="isActive"></param>
		/// <param name="redirectAuthorID"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type Author.</returns>
		public Author AuthorInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int? authorTypeID,
			string startDate,
			string endDate,
			string numeration,
			string title,
			string unit,
			string location,
			string note,
			short isActive,
			int? redirectAuthorID,
			int? creationUserID,
			int? lastModifiedUserID)
		{
			return AuthorInsertAuto( sqlConnection, sqlTransaction, "BHL", authorTypeID, startDate, endDate, numeration, title, unit, location, note, isActive, redirectAuthorID, creationUserID, lastModifiedUserID );
		}
		
		/// <summary>
		/// Insert values into dbo.Author.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="authorTypeID"></param>
		/// <param name="startDate"></param>
		/// <param name="endDate"></param>
		/// <param name="numeration"></param>
		/// <param name="title"></param>
		/// <param name="unit"></param>
		/// <param name="location"></param>
		/// <param name="note"></param>
		/// <param name="isActive"></param>
		/// <param name="redirectAuthorID"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type Author.</returns>
		public Author AuthorInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int? authorTypeID,
			string startDate,
			string endDate,
			string numeration,
			string title,
			string unit,
			string location,
			string note,
			short isActive,
			int? redirectAuthorID,
			int? creationUserID,
			int? lastModifiedUserID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("AuthorInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("AuthorID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("AuthorTypeID", SqlDbType.Int, null, true, authorTypeID),
					CustomSqlHelper.CreateInputParameter("StartDate", SqlDbType.NVarChar, 25, false, startDate),
					CustomSqlHelper.CreateInputParameter("EndDate", SqlDbType.NVarChar, 25, false, endDate),
					CustomSqlHelper.CreateInputParameter("Numeration", SqlDbType.NVarChar, 300, false, numeration),
					CustomSqlHelper.CreateInputParameter("Title", SqlDbType.NVarChar, 200, false, title),
					CustomSqlHelper.CreateInputParameter("Unit", SqlDbType.NVarChar, 300, false, unit),
					CustomSqlHelper.CreateInputParameter("Location", SqlDbType.NVarChar, 200, false, location),
					CustomSqlHelper.CreateInputParameter("Note", SqlDbType.NVarChar, 1073741823, false, note),
					CustomSqlHelper.CreateInputParameter("IsActive", SqlDbType.SmallInt, null, false, isActive),
					CustomSqlHelper.CreateInputParameter("RedirectAuthorID", SqlDbType.Int, null, true, redirectAuthorID),
					CustomSqlHelper.CreateInputParameter("CreationUserID", SqlDbType.Int, null, true, creationUserID),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, true, lastModifiedUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<Author> helper = new CustomSqlHelper<Author>())
				{
					CustomGenericList<Author> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						Author o = list[0];
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
		/// Insert values into dbo.Author. Returns an object of type Author.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type Author.</param>
		/// <returns>Object of type Author.</returns>
		public Author AuthorInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			Author value)
		{
			return AuthorInsertAuto(sqlConnection, sqlTransaction, "BHL", value);
		}
		
		/// <summary>
		/// Insert values into dbo.Author. Returns an object of type Author.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type Author.</param>
		/// <returns>Object of type Author.</returns>
		public Author AuthorInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			Author value)
		{
			return AuthorInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.AuthorTypeID,
				value.StartDate,
				value.EndDate,
				value.Numeration,
				value.Title,
				value.Unit,
				value.Location,
				value.Note,
				value.IsActive,
				value.RedirectAuthorID,
				value.CreationUserID,
				value.LastModifiedUserID);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from dbo.Author by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="authorID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool AuthorDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int authorID)
		{
			return AuthorDeleteAuto( sqlConnection, sqlTransaction, "BHL", authorID );
		}
		
		/// <summary>
		/// Delete values from dbo.Author by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="authorID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool AuthorDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int authorID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("AuthorDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("AuthorID", SqlDbType.Int, null, false, authorID), 
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
		/// Update values in dbo.Author. Returns an object of type Author.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="authorID"></param>
		/// <param name="authorTypeID"></param>
		/// <param name="startDate"></param>
		/// <param name="endDate"></param>
		/// <param name="numeration"></param>
		/// <param name="title"></param>
		/// <param name="unit"></param>
		/// <param name="location"></param>
		/// <param name="note"></param>
		/// <param name="isActive"></param>
		/// <param name="redirectAuthorID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type Author.</returns>
		public Author AuthorUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int authorID,
			int? authorTypeID,
			string startDate,
			string endDate,
			string numeration,
			string title,
			string unit,
			string location,
			string note,
			short isActive,
			int? redirectAuthorID,
			int? lastModifiedUserID)
		{
			return AuthorUpdateAuto( sqlConnection, sqlTransaction, "BHL", authorID, authorTypeID, startDate, endDate, numeration, title, unit, location, note, isActive, redirectAuthorID, lastModifiedUserID);
		}
		
		/// <summary>
		/// Update values in dbo.Author. Returns an object of type Author.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="authorID"></param>
		/// <param name="authorTypeID"></param>
		/// <param name="startDate"></param>
		/// <param name="endDate"></param>
		/// <param name="numeration"></param>
		/// <param name="title"></param>
		/// <param name="unit"></param>
		/// <param name="location"></param>
		/// <param name="note"></param>
		/// <param name="isActive"></param>
		/// <param name="redirectAuthorID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type Author.</returns>
		public Author AuthorUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int authorID,
			int? authorTypeID,
			string startDate,
			string endDate,
			string numeration,
			string title,
			string unit,
			string location,
			string note,
			short isActive,
			int? redirectAuthorID,
			int? lastModifiedUserID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("AuthorUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("AuthorID", SqlDbType.Int, null, false, authorID),
					CustomSqlHelper.CreateInputParameter("AuthorTypeID", SqlDbType.Int, null, true, authorTypeID),
					CustomSqlHelper.CreateInputParameter("StartDate", SqlDbType.NVarChar, 25, false, startDate),
					CustomSqlHelper.CreateInputParameter("EndDate", SqlDbType.NVarChar, 25, false, endDate),
					CustomSqlHelper.CreateInputParameter("Numeration", SqlDbType.NVarChar, 300, false, numeration),
					CustomSqlHelper.CreateInputParameter("Title", SqlDbType.NVarChar, 200, false, title),
					CustomSqlHelper.CreateInputParameter("Unit", SqlDbType.NVarChar, 300, false, unit),
					CustomSqlHelper.CreateInputParameter("Location", SqlDbType.NVarChar, 200, false, location),
					CustomSqlHelper.CreateInputParameter("Note", SqlDbType.NVarChar, 1073741823, false, note),
					CustomSqlHelper.CreateInputParameter("IsActive", SqlDbType.SmallInt, null, false, isActive),
					CustomSqlHelper.CreateInputParameter("RedirectAuthorID", SqlDbType.Int, null, true, redirectAuthorID),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, true, lastModifiedUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<Author> helper = new CustomSqlHelper<Author>())
				{
					CustomGenericList<Author> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						Author o = list[0];
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
		/// Update values in dbo.Author. Returns an object of type Author.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type Author.</param>
		/// <returns>Object of type Author.</returns>
		public Author AuthorUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			Author value)
		{
			return AuthorUpdateAuto(sqlConnection, sqlTransaction, "BHL", value );
		}
		
		/// <summary>
		/// Update values in dbo.Author. Returns an object of type Author.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type Author.</param>
		/// <returns>Object of type Author.</returns>
		public Author AuthorUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			Author value)
		{
			return AuthorUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.AuthorID,
				value.AuthorTypeID,
				value.StartDate,
				value.EndDate,
				value.Numeration,
				value.Title,
				value.Unit,
				value.Location,
				value.Note,
				value.IsActive,
				value.RedirectAuthorID,
				value.LastModifiedUserID);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage dbo.Author object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.Author.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type Author.</param>
		/// <returns>Object of type CustomDataAccessStatus<Author>.</returns>
		public CustomDataAccessStatus<Author> AuthorManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			Author value , int userId )
		{
			return AuthorManageAuto( sqlConnection, sqlTransaction, "BHL", value , userId );
		}
		
		/// <summary>
		/// Manage dbo.Author object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.Author.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type Author.</param>
		/// <returns>Object of type CustomDataAccessStatus<Author>.</returns>
		public CustomDataAccessStatus<Author> AuthorManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			Author value , int userId )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				value.CreationUserID = userId;
				value.LastModifiedUserID = userId;
				Author returnValue = AuthorInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.AuthorTypeID,
						value.StartDate,
						value.EndDate,
						value.Numeration,
						value.Title,
						value.Unit,
						value.Location,
						value.Note,
						value.IsActive,
						value.RedirectAuthorID,
						value.CreationUserID,
						value.LastModifiedUserID);
				
				return new CustomDataAccessStatus<Author>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (AuthorDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.AuthorID))
				{
				return new CustomDataAccessStatus<Author>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<Author>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				value.LastModifiedUserID = userId;
				Author returnValue = AuthorUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.AuthorID,
						value.AuthorTypeID,
						value.StartDate,
						value.EndDate,
						value.Numeration,
						value.Title,
						value.Unit,
						value.Location,
						value.Note,
						value.IsActive,
						value.RedirectAuthorID,
						value.LastModifiedUserID);
					
				return new CustomDataAccessStatus<Author>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<Author>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}

