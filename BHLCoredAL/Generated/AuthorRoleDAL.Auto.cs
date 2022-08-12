
// Generated 1/5/2021 3:24:58 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class AuthorRoleDAL is based upon dbo.AuthorRole.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DAL
// {
// 		public partial class AuthorRoleDAL
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
	partial class AuthorRoleDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from dbo.AuthorRole by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="authorRoleID"></param>
		/// <returns>Object of type AuthorRole.</returns>
		public AuthorRole AuthorRoleSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int authorRoleID)
		{
			return AuthorRoleSelectAuto(	sqlConnection, sqlTransaction, "BHL",	authorRoleID );
		}
			
		/// <summary>
		/// Select values from dbo.AuthorRole by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="authorRoleID"></param>
		/// <returns>Object of type AuthorRole.</returns>
		public AuthorRole AuthorRoleSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int authorRoleID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("AuthorRoleSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("AuthorRoleID", SqlDbType.Int, null, false, authorRoleID)))
			{
				using (CustomSqlHelper<AuthorRole> helper = new CustomSqlHelper<AuthorRole>())
				{
					List<AuthorRole> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						AuthorRole o = list[0];
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
		/// Select values from dbo.AuthorRole by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="authorRoleID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> AuthorRoleSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int authorRoleID)
		{
			return AuthorRoleSelectAutoRaw( sqlConnection, sqlTransaction, "BHL", authorRoleID );
		}
		
		/// <summary>
		/// Select values from dbo.AuthorRole by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="authorRoleID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> AuthorRoleSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int authorRoleID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("AuthorRoleSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("AuthorRoleID", SqlDbType.Int, null, false, authorRoleID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====

 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into dbo.AuthorRole.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="authorRoleID"></param>
		/// <param name="roleDescription"></param>
		/// <param name="mARCDataFieldTag"></param>
		/// <param name="lastModifedDate"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type AuthorRole.</returns>
		public AuthorRole AuthorRoleInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int authorRoleID,
			string roleDescription,
			string mARCDataFieldTag,
			DateTime lastModifedDate,
			int? creationUserID,
			int? lastModifiedUserID)
		{
			return AuthorRoleInsertAuto( sqlConnection, sqlTransaction, "BHL", authorRoleID, roleDescription, mARCDataFieldTag, lastModifedDate, creationUserID, lastModifiedUserID );
		}
		
		/// <summary>
		/// Insert values into dbo.AuthorRole.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="authorRoleID"></param>
		/// <param name="roleDescription"></param>
		/// <param name="mARCDataFieldTag"></param>
		/// <param name="lastModifedDate"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type AuthorRole.</returns>
		public AuthorRole AuthorRoleInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int authorRoleID,
			string roleDescription,
			string mARCDataFieldTag,
			DateTime lastModifedDate,
			int? creationUserID,
			int? lastModifiedUserID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("AuthorRoleInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("AuthorRoleID", SqlDbType.Int, null, false, authorRoleID),
					CustomSqlHelper.CreateInputParameter("RoleDescription", SqlDbType.NVarChar, 255, false, roleDescription),
					CustomSqlHelper.CreateInputParameter("MARCDataFieldTag", SqlDbType.NVarChar, 3, false, mARCDataFieldTag),
					CustomSqlHelper.CreateInputParameter("LastModifedDate", SqlDbType.DateTime, null, false, lastModifedDate),
					CustomSqlHelper.CreateInputParameter("CreationUserID", SqlDbType.Int, null, true, creationUserID),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, true, lastModifiedUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<AuthorRole> helper = new CustomSqlHelper<AuthorRole>())
				{
					List<AuthorRole> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						AuthorRole o = list[0];
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
		/// Insert values into dbo.AuthorRole. Returns an object of type AuthorRole.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type AuthorRole.</param>
		/// <returns>Object of type AuthorRole.</returns>
		public AuthorRole AuthorRoleInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			AuthorRole value)
		{
			return AuthorRoleInsertAuto(sqlConnection, sqlTransaction, "BHL", value);
		}
		
		/// <summary>
		/// Insert values into dbo.AuthorRole. Returns an object of type AuthorRole.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type AuthorRole.</param>
		/// <returns>Object of type AuthorRole.</returns>
		public AuthorRole AuthorRoleInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			AuthorRole value)
		{
			return AuthorRoleInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.AuthorRoleID,
				value.RoleDescription,
				value.MARCDataFieldTag,
				value.LastModifedDate,
				value.CreationUserID,
				value.LastModifiedUserID);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from dbo.AuthorRole by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="authorRoleID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool AuthorRoleDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int authorRoleID)
		{
			return AuthorRoleDeleteAuto( sqlConnection, sqlTransaction, "BHL", authorRoleID );
		}
		
		/// <summary>
		/// Delete values from dbo.AuthorRole by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="authorRoleID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool AuthorRoleDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int authorRoleID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("AuthorRoleDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("AuthorRoleID", SqlDbType.Int, null, false, authorRoleID), 
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
		/// Update values in dbo.AuthorRole. Returns an object of type AuthorRole.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="authorRoleID"></param>
		/// <param name="roleDescription"></param>
		/// <param name="mARCDataFieldTag"></param>
		/// <param name="lastModifedDate"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type AuthorRole.</returns>
		public AuthorRole AuthorRoleUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int authorRoleID,
			string roleDescription,
			string mARCDataFieldTag,
			DateTime lastModifedDate,
			int? lastModifiedUserID)
		{
			return AuthorRoleUpdateAuto( sqlConnection, sqlTransaction, "BHL", authorRoleID, roleDescription, mARCDataFieldTag, lastModifedDate, lastModifiedUserID);
		}
		
		/// <summary>
		/// Update values in dbo.AuthorRole. Returns an object of type AuthorRole.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="authorRoleID"></param>
		/// <param name="roleDescription"></param>
		/// <param name="mARCDataFieldTag"></param>
		/// <param name="lastModifedDate"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type AuthorRole.</returns>
		public AuthorRole AuthorRoleUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int authorRoleID,
			string roleDescription,
			string mARCDataFieldTag,
			DateTime lastModifedDate,
			int? lastModifiedUserID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("AuthorRoleUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("AuthorRoleID", SqlDbType.Int, null, false, authorRoleID),
					CustomSqlHelper.CreateInputParameter("RoleDescription", SqlDbType.NVarChar, 255, false, roleDescription),
					CustomSqlHelper.CreateInputParameter("MARCDataFieldTag", SqlDbType.NVarChar, 3, false, mARCDataFieldTag),
					CustomSqlHelper.CreateInputParameter("LastModifedDate", SqlDbType.DateTime, null, false, lastModifedDate),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, true, lastModifiedUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<AuthorRole> helper = new CustomSqlHelper<AuthorRole>())
				{
					List<AuthorRole> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						AuthorRole o = list[0];
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
		/// Update values in dbo.AuthorRole. Returns an object of type AuthorRole.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type AuthorRole.</param>
		/// <returns>Object of type AuthorRole.</returns>
		public AuthorRole AuthorRoleUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			AuthorRole value)
		{
			return AuthorRoleUpdateAuto(sqlConnection, sqlTransaction, "BHL", value );
		}
		
		/// <summary>
		/// Update values in dbo.AuthorRole. Returns an object of type AuthorRole.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type AuthorRole.</param>
		/// <returns>Object of type AuthorRole.</returns>
		public AuthorRole AuthorRoleUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			AuthorRole value)
		{
			return AuthorRoleUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.AuthorRoleID,
				value.RoleDescription,
				value.MARCDataFieldTag,
				value.LastModifedDate,
				value.LastModifiedUserID);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage dbo.AuthorRole object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.AuthorRole.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type AuthorRole.</param>
		/// <returns>Object of type CustomDataAccessStatus<AuthorRole>.</returns>
		public CustomDataAccessStatus<AuthorRole> AuthorRoleManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			AuthorRole value , int userId )
		{
			return AuthorRoleManageAuto( sqlConnection, sqlTransaction, "BHL", value , userId );
		}
		
		/// <summary>
		/// Manage dbo.AuthorRole object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.AuthorRole.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type AuthorRole.</param>
		/// <returns>Object of type CustomDataAccessStatus<AuthorRole>.</returns>
		public CustomDataAccessStatus<AuthorRole> AuthorRoleManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			AuthorRole value , int userId )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				value.CreationUserID = userId;
				value.LastModifiedUserID = userId;
				AuthorRole returnValue = AuthorRoleInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.AuthorRoleID,
						value.RoleDescription,
						value.MARCDataFieldTag,
						value.LastModifedDate,
						value.CreationUserID,
						value.LastModifiedUserID);
				
				return new CustomDataAccessStatus<AuthorRole>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (AuthorRoleDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.AuthorRoleID))
				{
				return new CustomDataAccessStatus<AuthorRole>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<AuthorRole>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				value.LastModifiedUserID = userId;
				AuthorRole returnValue = AuthorRoleUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.AuthorRoleID,
						value.RoleDescription,
						value.MARCDataFieldTag,
						value.LastModifedDate,
						value.LastModifiedUserID);
					
				return new CustomDataAccessStatus<AuthorRole>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<AuthorRole>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}

