
// Generated 1/5/2021 3:25:00 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class AuthorTypeDAL is based upon dbo.AuthorType.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DAL
// {
// 		public partial class AuthorTypeDAL
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
	partial class AuthorTypeDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from dbo.AuthorType by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="authorTypeID"></param>
		/// <returns>Object of type AuthorType.</returns>
		public AuthorType AuthorTypeSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int authorTypeID)
		{
			return AuthorTypeSelectAuto(	sqlConnection, sqlTransaction, "BHL",	authorTypeID );
		}
			
		/// <summary>
		/// Select values from dbo.AuthorType by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="authorTypeID"></param>
		/// <returns>Object of type AuthorType.</returns>
		public AuthorType AuthorTypeSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int authorTypeID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("AuthorTypeSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("AuthorTypeID", SqlDbType.Int, null, false, authorTypeID)))
			{
				using (CustomSqlHelper<AuthorType> helper = new CustomSqlHelper<AuthorType>())
				{
					List<AuthorType> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						AuthorType o = list[0];
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
		/// Select values from dbo.AuthorType by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="authorTypeID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> AuthorTypeSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int authorTypeID)
		{
			return AuthorTypeSelectAutoRaw( sqlConnection, sqlTransaction, "BHL", authorTypeID );
		}
		
		/// <summary>
		/// Select values from dbo.AuthorType by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="authorTypeID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> AuthorTypeSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int authorTypeID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("AuthorTypeSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("AuthorTypeID", SqlDbType.Int, null, false, authorTypeID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====

 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into dbo.AuthorType.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="authorTypeName"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type AuthorType.</returns>
		public AuthorType AuthorTypeInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string authorTypeName,
			int? creationUserID,
			int? lastModifiedUserID)
		{
			return AuthorTypeInsertAuto( sqlConnection, sqlTransaction, "BHL", authorTypeName, creationUserID, lastModifiedUserID );
		}
		
		/// <summary>
		/// Insert values into dbo.AuthorType.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="authorTypeName"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type AuthorType.</returns>
		public AuthorType AuthorTypeInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			string authorTypeName,
			int? creationUserID,
			int? lastModifiedUserID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("AuthorTypeInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("AuthorTypeID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("AuthorTypeName", SqlDbType.NVarChar, 50, false, authorTypeName),
					CustomSqlHelper.CreateInputParameter("CreationUserID", SqlDbType.Int, null, true, creationUserID),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, true, lastModifiedUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<AuthorType> helper = new CustomSqlHelper<AuthorType>())
				{
					List<AuthorType> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						AuthorType o = list[0];
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
		/// Insert values into dbo.AuthorType. Returns an object of type AuthorType.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type AuthorType.</param>
		/// <returns>Object of type AuthorType.</returns>
		public AuthorType AuthorTypeInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			AuthorType value)
		{
			return AuthorTypeInsertAuto(sqlConnection, sqlTransaction, "BHL", value);
		}
		
		/// <summary>
		/// Insert values into dbo.AuthorType. Returns an object of type AuthorType.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type AuthorType.</param>
		/// <returns>Object of type AuthorType.</returns>
		public AuthorType AuthorTypeInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			AuthorType value)
		{
			return AuthorTypeInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.AuthorTypeName,
				value.CreationUserID,
				value.LastModifiedUserID);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from dbo.AuthorType by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="authorTypeID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool AuthorTypeDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int authorTypeID)
		{
			return AuthorTypeDeleteAuto( sqlConnection, sqlTransaction, "BHL", authorTypeID );
		}
		
		/// <summary>
		/// Delete values from dbo.AuthorType by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="authorTypeID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool AuthorTypeDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int authorTypeID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("AuthorTypeDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("AuthorTypeID", SqlDbType.Int, null, false, authorTypeID), 
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
		/// Update values in dbo.AuthorType. Returns an object of type AuthorType.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="authorTypeID"></param>
		/// <param name="authorTypeName"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type AuthorType.</returns>
		public AuthorType AuthorTypeUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int authorTypeID,
			string authorTypeName,
			int? lastModifiedUserID)
		{
			return AuthorTypeUpdateAuto( sqlConnection, sqlTransaction, "BHL", authorTypeID, authorTypeName, lastModifiedUserID);
		}
		
		/// <summary>
		/// Update values in dbo.AuthorType. Returns an object of type AuthorType.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="authorTypeID"></param>
		/// <param name="authorTypeName"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type AuthorType.</returns>
		public AuthorType AuthorTypeUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int authorTypeID,
			string authorTypeName,
			int? lastModifiedUserID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("AuthorTypeUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("AuthorTypeID", SqlDbType.Int, null, false, authorTypeID),
					CustomSqlHelper.CreateInputParameter("AuthorTypeName", SqlDbType.NVarChar, 50, false, authorTypeName),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, true, lastModifiedUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<AuthorType> helper = new CustomSqlHelper<AuthorType>())
				{
					List<AuthorType> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						AuthorType o = list[0];
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
		/// Update values in dbo.AuthorType. Returns an object of type AuthorType.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type AuthorType.</param>
		/// <returns>Object of type AuthorType.</returns>
		public AuthorType AuthorTypeUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			AuthorType value)
		{
			return AuthorTypeUpdateAuto(sqlConnection, sqlTransaction, "BHL", value );
		}
		
		/// <summary>
		/// Update values in dbo.AuthorType. Returns an object of type AuthorType.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type AuthorType.</param>
		/// <returns>Object of type AuthorType.</returns>
		public AuthorType AuthorTypeUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			AuthorType value)
		{
			return AuthorTypeUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.AuthorTypeID,
				value.AuthorTypeName,
				value.LastModifiedUserID);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage dbo.AuthorType object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.AuthorType.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type AuthorType.</param>
		/// <returns>Object of type CustomDataAccessStatus<AuthorType>.</returns>
		public CustomDataAccessStatus<AuthorType> AuthorTypeManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			AuthorType value , int userId )
		{
			return AuthorTypeManageAuto( sqlConnection, sqlTransaction, "BHL", value , userId );
		}
		
		/// <summary>
		/// Manage dbo.AuthorType object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.AuthorType.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type AuthorType.</param>
		/// <returns>Object of type CustomDataAccessStatus<AuthorType>.</returns>
		public CustomDataAccessStatus<AuthorType> AuthorTypeManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			AuthorType value , int userId )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				value.CreationUserID = userId;
				value.LastModifiedUserID = userId;
				AuthorType returnValue = AuthorTypeInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.AuthorTypeName,
						value.CreationUserID,
						value.LastModifiedUserID);
				
				return new CustomDataAccessStatus<AuthorType>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (AuthorTypeDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.AuthorTypeID))
				{
				return new CustomDataAccessStatus<AuthorType>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<AuthorType>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				value.LastModifiedUserID = userId;
				AuthorType returnValue = AuthorTypeUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.AuthorTypeID,
						value.AuthorTypeName,
						value.LastModifiedUserID);
					
				return new CustomDataAccessStatus<AuthorType>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<AuthorType>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}

