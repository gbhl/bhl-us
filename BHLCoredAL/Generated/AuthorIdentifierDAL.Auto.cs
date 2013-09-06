
// Generated 5/18/2012 11:11:49 AM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class AuthorIdentifierDAL is based upon AuthorIdentifier.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DAL
// {
// 		public partial class AuthorIdentifierDAL
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
	partial class AuthorIdentifierDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from AuthorIdentifier by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="authorIdentifierID"></param>
		/// <returns>Object of type AuthorIdentifier.</returns>
		public AuthorIdentifier AuthorIdentifierSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int authorIdentifierID)
		{
			return AuthorIdentifierSelectAuto(	sqlConnection, sqlTransaction, "BHL",	authorIdentifierID );
		}
			
		/// <summary>
		/// Select values from AuthorIdentifier by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="authorIdentifierID"></param>
		/// <returns>Object of type AuthorIdentifier.</returns>
		public AuthorIdentifier AuthorIdentifierSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int authorIdentifierID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("AuthorIdentifierSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("AuthorIdentifierID", SqlDbType.Int, null, false, authorIdentifierID)))
			{
				using (CustomSqlHelper<AuthorIdentifier> helper = new CustomSqlHelper<AuthorIdentifier>())
				{
					CustomGenericList<AuthorIdentifier> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						AuthorIdentifier o = list[0];
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
		/// Select values from AuthorIdentifier by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="authorIdentifierID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> AuthorIdentifierSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int authorIdentifierID)
		{
			return AuthorIdentifierSelectAutoRaw( sqlConnection, sqlTransaction, "BHL", authorIdentifierID );
		}
		
		/// <summary>
		/// Select values from AuthorIdentifier by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="authorIdentifierID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> AuthorIdentifierSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int authorIdentifierID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("AuthorIdentifierSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("AuthorIdentifierID", SqlDbType.Int, null, false, authorIdentifierID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====
	
 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into AuthorIdentifier.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="authorID"></param>
		/// <param name="identifierID"></param>
		/// <param name="identifierValue"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type AuthorIdentifier.</returns>
		public AuthorIdentifier AuthorIdentifierInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int authorID,
			int identifierID,
			string identifierValue,
			int? creationUserID,
			int? lastModifiedUserID)
		{
			return AuthorIdentifierInsertAuto( sqlConnection, sqlTransaction, "BHL", authorID, identifierID, identifierValue, creationUserID, lastModifiedUserID );
		}
		
		/// <summary>
		/// Insert values into AuthorIdentifier.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="authorID"></param>
		/// <param name="identifierID"></param>
		/// <param name="identifierValue"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type AuthorIdentifier.</returns>
		public AuthorIdentifier AuthorIdentifierInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int authorID,
			int identifierID,
			string identifierValue,
			int? creationUserID,
			int? lastModifiedUserID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("AuthorIdentifierInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("AuthorIdentifierID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("AuthorID", SqlDbType.Int, null, false, authorID),
					CustomSqlHelper.CreateInputParameter("IdentifierID", SqlDbType.Int, null, false, identifierID),
					CustomSqlHelper.CreateInputParameter("IdentifierValue", SqlDbType.NVarChar, 125, false, identifierValue),
					CustomSqlHelper.CreateInputParameter("CreationUserID", SqlDbType.Int, null, true, creationUserID),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, true, lastModifiedUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<AuthorIdentifier> helper = new CustomSqlHelper<AuthorIdentifier>())
				{
					CustomGenericList<AuthorIdentifier> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						AuthorIdentifier o = list[0];
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
		/// Insert values into AuthorIdentifier. Returns an object of type AuthorIdentifier.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type AuthorIdentifier.</param>
		/// <returns>Object of type AuthorIdentifier.</returns>
		public AuthorIdentifier AuthorIdentifierInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			AuthorIdentifier value)
		{
			return AuthorIdentifierInsertAuto(sqlConnection, sqlTransaction, "BHL", value);
		}
		
		/// <summary>
		/// Insert values into AuthorIdentifier. Returns an object of type AuthorIdentifier.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type AuthorIdentifier.</param>
		/// <returns>Object of type AuthorIdentifier.</returns>
		public AuthorIdentifier AuthorIdentifierInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			AuthorIdentifier value)
		{
			return AuthorIdentifierInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.AuthorID,
				value.IdentifierID,
				value.IdentifierValue,
				value.CreationUserID,
				value.LastModifiedUserID);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from AuthorIdentifier by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="authorIdentifierID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool AuthorIdentifierDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int authorIdentifierID)
		{
			return AuthorIdentifierDeleteAuto( sqlConnection, sqlTransaction, "BHL", authorIdentifierID );
		}
		
		/// <summary>
		/// Delete values from AuthorIdentifier by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="authorIdentifierID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool AuthorIdentifierDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int authorIdentifierID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("AuthorIdentifierDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("AuthorIdentifierID", SqlDbType.Int, null, false, authorIdentifierID), 
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
		/// Update values in AuthorIdentifier. Returns an object of type AuthorIdentifier.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="authorIdentifierID"></param>
		/// <param name="authorID"></param>
		/// <param name="identifierID"></param>
		/// <param name="identifierValue"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type AuthorIdentifier.</returns>
		public AuthorIdentifier AuthorIdentifierUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int authorIdentifierID,
			int authorID,
			int identifierID,
			string identifierValue,
			int? lastModifiedUserID)
		{
			return AuthorIdentifierUpdateAuto( sqlConnection, sqlTransaction, "BHL", authorIdentifierID, authorID, identifierID, identifierValue, lastModifiedUserID);
		}
		
		/// <summary>
		/// Update values in AuthorIdentifier. Returns an object of type AuthorIdentifier.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="authorIdentifierID"></param>
		/// <param name="authorID"></param>
		/// <param name="identifierID"></param>
		/// <param name="identifierValue"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type AuthorIdentifier.</returns>
		public AuthorIdentifier AuthorIdentifierUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int authorIdentifierID,
			int authorID,
			int identifierID,
			string identifierValue,
			int? lastModifiedUserID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("AuthorIdentifierUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("AuthorIdentifierID", SqlDbType.Int, null, false, authorIdentifierID),
					CustomSqlHelper.CreateInputParameter("AuthorID", SqlDbType.Int, null, false, authorID),
					CustomSqlHelper.CreateInputParameter("IdentifierID", SqlDbType.Int, null, false, identifierID),
					CustomSqlHelper.CreateInputParameter("IdentifierValue", SqlDbType.NVarChar, 125, false, identifierValue),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, true, lastModifiedUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<AuthorIdentifier> helper = new CustomSqlHelper<AuthorIdentifier>())
				{
					CustomGenericList<AuthorIdentifier> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						AuthorIdentifier o = list[0];
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
		/// Update values in AuthorIdentifier. Returns an object of type AuthorIdentifier.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type AuthorIdentifier.</param>
		/// <returns>Object of type AuthorIdentifier.</returns>
		public AuthorIdentifier AuthorIdentifierUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			AuthorIdentifier value)
		{
			return AuthorIdentifierUpdateAuto(sqlConnection, sqlTransaction, "BHL", value );
		}
		
		/// <summary>
		/// Update values in AuthorIdentifier. Returns an object of type AuthorIdentifier.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type AuthorIdentifier.</param>
		/// <returns>Object of type AuthorIdentifier.</returns>
		public AuthorIdentifier AuthorIdentifierUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			AuthorIdentifier value)
		{
			return AuthorIdentifierUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.AuthorIdentifierID,
				value.AuthorID,
				value.IdentifierID,
				value.IdentifierValue,
				value.LastModifiedUserID);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage AuthorIdentifier object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in AuthorIdentifier.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type AuthorIdentifier.</param>
		/// <returns>Object of type CustomDataAccessStatus<AuthorIdentifier>.</returns>
		public CustomDataAccessStatus<AuthorIdentifier> AuthorIdentifierManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			AuthorIdentifier value , int userId )
		{
			return AuthorIdentifierManageAuto( sqlConnection, sqlTransaction, "BHL", value , userId );
		}
		
		/// <summary>
		/// Manage AuthorIdentifier object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in AuthorIdentifier.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type AuthorIdentifier.</param>
		/// <returns>Object of type CustomDataAccessStatus<AuthorIdentifier>.</returns>
		public CustomDataAccessStatus<AuthorIdentifier> AuthorIdentifierManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			AuthorIdentifier value , int userId )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				value.CreationUserID = userId;
				value.LastModifiedUserID = userId;
				AuthorIdentifier returnValue = AuthorIdentifierInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.AuthorID,
						value.IdentifierID,
						value.IdentifierValue,
						value.CreationUserID,
						value.LastModifiedUserID);
				
				return new CustomDataAccessStatus<AuthorIdentifier>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (AuthorIdentifierDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.AuthorIdentifierID))
				{
				return new CustomDataAccessStatus<AuthorIdentifier>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<AuthorIdentifier>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				value.LastModifiedUserID = userId;
				AuthorIdentifier returnValue = AuthorIdentifierUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.AuthorIdentifierID,
						value.AuthorID,
						value.IdentifierID,
						value.IdentifierValue,
						value.LastModifiedUserID);
					
				return new CustomDataAccessStatus<AuthorIdentifier>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<AuthorIdentifier>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}
// end of source generation
