
// Generated 1/5/2021 3:27:31 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class Title_IdentifierDAL is based upon dbo.Title_Identifier.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DAL
// {
// 		public partial class Title_IdentifierDAL
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
	partial class Title_IdentifierDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from dbo.Title_Identifier by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="titleIdentifierID"></param>
		/// <returns>Object of type Title_Identifier.</returns>
		public Title_Identifier Title_IdentifierSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int titleIdentifierID)
		{
			return Title_IdentifierSelectAuto(	sqlConnection, sqlTransaction, "BHL",	titleIdentifierID );
		}
			
		/// <summary>
		/// Select values from dbo.Title_Identifier by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="titleIdentifierID"></param>
		/// <returns>Object of type Title_Identifier.</returns>
		public Title_Identifier Title_IdentifierSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int titleIdentifierID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("Title_IdentifierSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("TitleIdentifierID", SqlDbType.Int, null, false, titleIdentifierID)))
			{
				using (CustomSqlHelper<Title_Identifier> helper = new CustomSqlHelper<Title_Identifier>())
				{
					List<Title_Identifier> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						Title_Identifier o = list[0];
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
		/// Select values from dbo.Title_Identifier by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="titleIdentifierID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> Title_IdentifierSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int titleIdentifierID)
		{
			return Title_IdentifierSelectAutoRaw( sqlConnection, sqlTransaction, "BHL", titleIdentifierID );
		}
		
		/// <summary>
		/// Select values from dbo.Title_Identifier by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="titleIdentifierID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> Title_IdentifierSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int titleIdentifierID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("Title_IdentifierSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("TitleIdentifierID", SqlDbType.Int, null, false, titleIdentifierID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====

 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into dbo.Title_Identifier.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="titleID"></param>
		/// <param name="identifierID"></param>
		/// <param name="identifierValue"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type Title_Identifier.</returns>
		public Title_Identifier Title_IdentifierInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int titleID,
			int identifierID,
			string identifierValue,
			int? creationUserID,
			int? lastModifiedUserID)
		{
			return Title_IdentifierInsertAuto( sqlConnection, sqlTransaction, "BHL", titleID, identifierID, identifierValue, creationUserID, lastModifiedUserID );
		}
		
		/// <summary>
		/// Insert values into dbo.Title_Identifier.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="titleID"></param>
		/// <param name="identifierID"></param>
		/// <param name="identifierValue"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type Title_Identifier.</returns>
		public Title_Identifier Title_IdentifierInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int titleID,
			int identifierID,
			string identifierValue,
			int? creationUserID,
			int? lastModifiedUserID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("Title_IdentifierInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("TitleIdentifierID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("TitleID", SqlDbType.Int, null, false, titleID),
					CustomSqlHelper.CreateInputParameter("IdentifierID", SqlDbType.Int, null, false, identifierID),
					CustomSqlHelper.CreateInputParameter("IdentifierValue", SqlDbType.NVarChar, 125, false, identifierValue),
					CustomSqlHelper.CreateInputParameter("CreationUserID", SqlDbType.Int, null, true, creationUserID),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, true, lastModifiedUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<Title_Identifier> helper = new CustomSqlHelper<Title_Identifier>())
				{
					List<Title_Identifier> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						Title_Identifier o = list[0];
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
		/// Insert values into dbo.Title_Identifier. Returns an object of type Title_Identifier.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type Title_Identifier.</param>
		/// <returns>Object of type Title_Identifier.</returns>
		public Title_Identifier Title_IdentifierInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			Title_Identifier value)
		{
			return Title_IdentifierInsertAuto(sqlConnection, sqlTransaction, "BHL", value);
		}
		
		/// <summary>
		/// Insert values into dbo.Title_Identifier. Returns an object of type Title_Identifier.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type Title_Identifier.</param>
		/// <returns>Object of type Title_Identifier.</returns>
		public Title_Identifier Title_IdentifierInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			Title_Identifier value)
		{
			return Title_IdentifierInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.TitleID,
				value.IdentifierID,
				value.IdentifierValue,
				value.CreationUserID,
				value.LastModifiedUserID);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from dbo.Title_Identifier by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="titleIdentifierID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool Title_IdentifierDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int titleIdentifierID)
		{
			return Title_IdentifierDeleteAuto( sqlConnection, sqlTransaction, "BHL", titleIdentifierID );
		}
		
		/// <summary>
		/// Delete values from dbo.Title_Identifier by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="titleIdentifierID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool Title_IdentifierDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int titleIdentifierID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("Title_IdentifierDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("TitleIdentifierID", SqlDbType.Int, null, false, titleIdentifierID), 
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
		/// Update values in dbo.Title_Identifier. Returns an object of type Title_Identifier.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="titleIdentifierID"></param>
		/// <param name="titleID"></param>
		/// <param name="identifierID"></param>
		/// <param name="identifierValue"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type Title_Identifier.</returns>
		public Title_Identifier Title_IdentifierUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int titleIdentifierID,
			int titleID,
			int identifierID,
			string identifierValue,
			int? lastModifiedUserID)
		{
			return Title_IdentifierUpdateAuto( sqlConnection, sqlTransaction, "BHL", titleIdentifierID, titleID, identifierID, identifierValue, lastModifiedUserID);
		}
		
		/// <summary>
		/// Update values in dbo.Title_Identifier. Returns an object of type Title_Identifier.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="titleIdentifierID"></param>
		/// <param name="titleID"></param>
		/// <param name="identifierID"></param>
		/// <param name="identifierValue"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type Title_Identifier.</returns>
		public Title_Identifier Title_IdentifierUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int titleIdentifierID,
			int titleID,
			int identifierID,
			string identifierValue,
			int? lastModifiedUserID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("Title_IdentifierUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("TitleIdentifierID", SqlDbType.Int, null, false, titleIdentifierID),
					CustomSqlHelper.CreateInputParameter("TitleID", SqlDbType.Int, null, false, titleID),
					CustomSqlHelper.CreateInputParameter("IdentifierID", SqlDbType.Int, null, false, identifierID),
					CustomSqlHelper.CreateInputParameter("IdentifierValue", SqlDbType.NVarChar, 125, false, identifierValue),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, true, lastModifiedUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<Title_Identifier> helper = new CustomSqlHelper<Title_Identifier>())
				{
					List<Title_Identifier> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						Title_Identifier o = list[0];
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
		/// Update values in dbo.Title_Identifier. Returns an object of type Title_Identifier.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type Title_Identifier.</param>
		/// <returns>Object of type Title_Identifier.</returns>
		public Title_Identifier Title_IdentifierUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			Title_Identifier value)
		{
			return Title_IdentifierUpdateAuto(sqlConnection, sqlTransaction, "BHL", value );
		}
		
		/// <summary>
		/// Update values in dbo.Title_Identifier. Returns an object of type Title_Identifier.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type Title_Identifier.</param>
		/// <returns>Object of type Title_Identifier.</returns>
		public Title_Identifier Title_IdentifierUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			Title_Identifier value)
		{
			return Title_IdentifierUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.TitleIdentifierID,
				value.TitleID,
				value.IdentifierID,
				value.IdentifierValue,
				value.LastModifiedUserID);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage dbo.Title_Identifier object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.Title_Identifier.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type Title_Identifier.</param>
		/// <returns>Object of type CustomDataAccessStatus<Title_Identifier>.</returns>
		public CustomDataAccessStatus<Title_Identifier> Title_IdentifierManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			Title_Identifier value , int userId )
		{
			return Title_IdentifierManageAuto( sqlConnection, sqlTransaction, "BHL", value , userId );
		}
		
		/// <summary>
		/// Manage dbo.Title_Identifier object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.Title_Identifier.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type Title_Identifier.</param>
		/// <returns>Object of type CustomDataAccessStatus<Title_Identifier>.</returns>
		public CustomDataAccessStatus<Title_Identifier> Title_IdentifierManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			Title_Identifier value , int userId )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				value.CreationUserID = userId;
				value.LastModifiedUserID = userId;
				Title_Identifier returnValue = Title_IdentifierInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.TitleID,
						value.IdentifierID,
						value.IdentifierValue,
						value.CreationUserID,
						value.LastModifiedUserID);
				
				return new CustomDataAccessStatus<Title_Identifier>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (Title_IdentifierDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.TitleIdentifierID))
				{
				return new CustomDataAccessStatus<Title_Identifier>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<Title_Identifier>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				value.LastModifiedUserID = userId;
				Title_Identifier returnValue = Title_IdentifierUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.TitleIdentifierID,
						value.TitleID,
						value.IdentifierID,
						value.IdentifierValue,
						value.LastModifiedUserID);
					
				return new CustomDataAccessStatus<Title_Identifier>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<Title_Identifier>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}

