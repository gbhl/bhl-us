
// Generated 5/29/2012 12:59:27 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class AuthorNameDAL is based upon AuthorName.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DAL
// {
// 		public partial class AuthorNameDAL
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
	partial class AuthorNameDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from AuthorName by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="authorNameID"></param>
		/// <returns>Object of type AuthorName.</returns>
		public AuthorName AuthorNameSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int authorNameID)
		{
			return AuthorNameSelectAuto(	sqlConnection, sqlTransaction, "BHL",	authorNameID );
		}
			
		/// <summary>
		/// Select values from AuthorName by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="authorNameID"></param>
		/// <returns>Object of type AuthorName.</returns>
		public AuthorName AuthorNameSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int authorNameID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("AuthorNameSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("AuthorNameID", SqlDbType.Int, null, false, authorNameID)))
			{
				using (CustomSqlHelper<AuthorName> helper = new CustomSqlHelper<AuthorName>())
				{
					CustomGenericList<AuthorName> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						AuthorName o = list[0];
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
		/// Select values from AuthorName by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="authorNameID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> AuthorNameSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int authorNameID)
		{
			return AuthorNameSelectAutoRaw( sqlConnection, sqlTransaction, "BHL", authorNameID );
		}
		
		/// <summary>
		/// Select values from AuthorName by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="authorNameID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> AuthorNameSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int authorNameID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("AuthorNameSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("AuthorNameID", SqlDbType.Int, null, false, authorNameID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====
	
 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into AuthorName.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="authorID"></param>
		/// <param name="fullName"></param>
		/// <param name="lastName"></param>
		/// <param name="firstName"></param>
		/// <param name="fullerForm"></param>
		/// <param name="isPreferredName"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type AuthorName.</returns>
		public AuthorName AuthorNameInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int authorID,
			string fullName,
			string lastName,
			string firstName,
			string fullerForm,
			short isPreferredName,
			int? creationUserID,
			int? lastModifiedUserID)
		{
			return AuthorNameInsertAuto( sqlConnection, sqlTransaction, "BHL", authorID, fullName, lastName, firstName, fullerForm, isPreferredName, creationUserID, lastModifiedUserID );
		}
		
		/// <summary>
		/// Insert values into AuthorName.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="authorID"></param>
		/// <param name="fullName"></param>
		/// <param name="lastName"></param>
		/// <param name="firstName"></param>
		/// <param name="fullerForm"></param>
		/// <param name="isPreferredName"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type AuthorName.</returns>
		public AuthorName AuthorNameInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int authorID,
			string fullName,
			string lastName,
			string firstName,
			string fullerForm,
			short isPreferredName,
			int? creationUserID,
			int? lastModifiedUserID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("AuthorNameInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("AuthorNameID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("AuthorID", SqlDbType.Int, null, false, authorID),
					CustomSqlHelper.CreateInputParameter("FullName", SqlDbType.NVarChar, 300, false, fullName),
					CustomSqlHelper.CreateInputParameter("LastName", SqlDbType.NVarChar, 150, false, lastName),
					CustomSqlHelper.CreateInputParameter("FirstName", SqlDbType.NVarChar, 150, false, firstName),
					CustomSqlHelper.CreateInputParameter("FullerForm", SqlDbType.NVarChar, 150, false, fullerForm),
					CustomSqlHelper.CreateInputParameter("IsPreferredName", SqlDbType.SmallInt, null, false, isPreferredName),
					CustomSqlHelper.CreateInputParameter("CreationUserID", SqlDbType.Int, null, true, creationUserID),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, true, lastModifiedUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<AuthorName> helper = new CustomSqlHelper<AuthorName>())
				{
					CustomGenericList<AuthorName> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						AuthorName o = list[0];
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
		/// Insert values into AuthorName. Returns an object of type AuthorName.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type AuthorName.</param>
		/// <returns>Object of type AuthorName.</returns>
		public AuthorName AuthorNameInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			AuthorName value)
		{
			return AuthorNameInsertAuto(sqlConnection, sqlTransaction, "BHL", value);
		}
		
		/// <summary>
		/// Insert values into AuthorName. Returns an object of type AuthorName.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type AuthorName.</param>
		/// <returns>Object of type AuthorName.</returns>
		public AuthorName AuthorNameInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			AuthorName value)
		{
			return AuthorNameInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.AuthorID,
				value.FullName,
				value.LastName,
				value.FirstName,
				value.FullerForm,
				value.IsPreferredName,
				value.CreationUserID,
				value.LastModifiedUserID);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from AuthorName by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="authorNameID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool AuthorNameDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int authorNameID)
		{
			return AuthorNameDeleteAuto( sqlConnection, sqlTransaction, "BHL", authorNameID );
		}
		
		/// <summary>
		/// Delete values from AuthorName by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="authorNameID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool AuthorNameDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int authorNameID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("AuthorNameDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("AuthorNameID", SqlDbType.Int, null, false, authorNameID), 
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
		/// Update values in AuthorName. Returns an object of type AuthorName.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="authorNameID"></param>
		/// <param name="authorID"></param>
		/// <param name="fullName"></param>
		/// <param name="lastName"></param>
		/// <param name="firstName"></param>
		/// <param name="fullerForm"></param>
		/// <param name="isPreferredName"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type AuthorName.</returns>
		public AuthorName AuthorNameUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int authorNameID,
			int authorID,
			string fullName,
			string lastName,
			string firstName,
			string fullerForm,
			short isPreferredName,
			int? lastModifiedUserID)
		{
			return AuthorNameUpdateAuto( sqlConnection, sqlTransaction, "BHL", authorNameID, authorID, fullName, lastName, firstName, fullerForm, isPreferredName, lastModifiedUserID);
		}
		
		/// <summary>
		/// Update values in AuthorName. Returns an object of type AuthorName.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="authorNameID"></param>
		/// <param name="authorID"></param>
		/// <param name="fullName"></param>
		/// <param name="lastName"></param>
		/// <param name="firstName"></param>
		/// <param name="fullerForm"></param>
		/// <param name="isPreferredName"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type AuthorName.</returns>
		public AuthorName AuthorNameUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int authorNameID,
			int authorID,
			string fullName,
			string lastName,
			string firstName,
			string fullerForm,
			short isPreferredName,
			int? lastModifiedUserID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("AuthorNameUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("AuthorNameID", SqlDbType.Int, null, false, authorNameID),
					CustomSqlHelper.CreateInputParameter("AuthorID", SqlDbType.Int, null, false, authorID),
					CustomSqlHelper.CreateInputParameter("FullName", SqlDbType.NVarChar, 300, false, fullName),
					CustomSqlHelper.CreateInputParameter("LastName", SqlDbType.NVarChar, 150, false, lastName),
					CustomSqlHelper.CreateInputParameter("FirstName", SqlDbType.NVarChar, 150, false, firstName),
					CustomSqlHelper.CreateInputParameter("FullerForm", SqlDbType.NVarChar, 150, false, fullerForm),
					CustomSqlHelper.CreateInputParameter("IsPreferredName", SqlDbType.SmallInt, null, false, isPreferredName),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, true, lastModifiedUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<AuthorName> helper = new CustomSqlHelper<AuthorName>())
				{
					CustomGenericList<AuthorName> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						AuthorName o = list[0];
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
		/// Update values in AuthorName. Returns an object of type AuthorName.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type AuthorName.</param>
		/// <returns>Object of type AuthorName.</returns>
		public AuthorName AuthorNameUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			AuthorName value)
		{
			return AuthorNameUpdateAuto(sqlConnection, sqlTransaction, "BHL", value );
		}
		
		/// <summary>
		/// Update values in AuthorName. Returns an object of type AuthorName.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type AuthorName.</param>
		/// <returns>Object of type AuthorName.</returns>
		public AuthorName AuthorNameUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			AuthorName value)
		{
			return AuthorNameUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.AuthorNameID,
				value.AuthorID,
				value.FullName,
				value.LastName,
				value.FirstName,
				value.FullerForm,
				value.IsPreferredName,
				value.LastModifiedUserID);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage AuthorName object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in AuthorName.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type AuthorName.</param>
		/// <returns>Object of type CustomDataAccessStatus<AuthorName>.</returns>
		public CustomDataAccessStatus<AuthorName> AuthorNameManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			AuthorName value , int userId )
		{
			return AuthorNameManageAuto( sqlConnection, sqlTransaction, "BHL", value , userId );
		}
		
		/// <summary>
		/// Manage AuthorName object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in AuthorName.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type AuthorName.</param>
		/// <returns>Object of type CustomDataAccessStatus<AuthorName>.</returns>
		public CustomDataAccessStatus<AuthorName> AuthorNameManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			AuthorName value , int userId )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				value.CreationUserID = userId;
				value.LastModifiedUserID = userId;
				AuthorName returnValue = AuthorNameInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.AuthorID,
						value.FullName,
						value.LastName,
						value.FirstName,
						value.FullerForm,
						value.IsPreferredName,
						value.CreationUserID,
						value.LastModifiedUserID);
				
				return new CustomDataAccessStatus<AuthorName>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (AuthorNameDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.AuthorNameID))
				{
				return new CustomDataAccessStatus<AuthorName>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<AuthorName>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				value.LastModifiedUserID = userId;
				AuthorName returnValue = AuthorNameUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.AuthorNameID,
						value.AuthorID,
						value.FullName,
						value.LastName,
						value.FirstName,
						value.FullerForm,
						value.IsPreferredName,
						value.LastModifiedUserID);
					
				return new CustomDataAccessStatus<AuthorName>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<AuthorName>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}
// end of source generation
