
// Generated 1/5/2021 3:25:57 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class KeywordDAL is based upon dbo.Keyword.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DAL
// {
// 		public partial class KeywordDAL
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
	partial class KeywordDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from dbo.Keyword by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="keywordID"></param>
		/// <returns>Object of type Keyword.</returns>
		public Keyword KeywordSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int keywordID)
		{
			return KeywordSelectAuto(	sqlConnection, sqlTransaction, "BHL",	keywordID );
		}
			
		/// <summary>
		/// Select values from dbo.Keyword by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="keywordID"></param>
		/// <returns>Object of type Keyword.</returns>
		public Keyword KeywordSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int keywordID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("KeywordSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("KeywordID", SqlDbType.Int, null, false, keywordID)))
			{
				using (CustomSqlHelper<Keyword> helper = new CustomSqlHelper<Keyword>())
				{
					List<Keyword> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						Keyword o = list[0];
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
		/// Select values from dbo.Keyword by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="keywordID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> KeywordSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int keywordID)
		{
			return KeywordSelectAutoRaw( sqlConnection, sqlTransaction, "BHL", keywordID );
		}
		
		/// <summary>
		/// Select values from dbo.Keyword by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="keywordID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> KeywordSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int keywordID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("KeywordSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("KeywordID", SqlDbType.Int, null, false, keywordID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====

 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into dbo.Keyword.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="keyword"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type Keyword.</returns>
		public Keyword KeywordInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string keyword,
			int? creationUserID,
			int? lastModifiedUserID)
		{
			return KeywordInsertAuto( sqlConnection, sqlTransaction, "BHL", keyword, creationUserID, lastModifiedUserID );
		}
		
		/// <summary>
		/// Insert values into dbo.Keyword.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="keyword"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type Keyword.</returns>
		public Keyword KeywordInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			string keyword,
			int? creationUserID,
			int? lastModifiedUserID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("KeywordInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("KeywordID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("Keyword", SqlDbType.NVarChar, 50, false, keyword),
					CustomSqlHelper.CreateInputParameter("CreationUserID", SqlDbType.Int, null, true, creationUserID),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, true, lastModifiedUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<Keyword> helper = new CustomSqlHelper<Keyword>())
				{
					List<Keyword> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						Keyword o = list[0];
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
		/// Insert values into dbo.Keyword. Returns an object of type Keyword.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type Keyword.</param>
		/// <returns>Object of type Keyword.</returns>
		public Keyword KeywordInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			Keyword value)
		{
			return KeywordInsertAuto(sqlConnection, sqlTransaction, "BHL", value);
		}
		
		/// <summary>
		/// Insert values into dbo.Keyword. Returns an object of type Keyword.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type Keyword.</param>
		/// <returns>Object of type Keyword.</returns>
		public Keyword KeywordInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			Keyword value)
		{
			return KeywordInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.Keyword,
				value.CreationUserID,
				value.LastModifiedUserID);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from dbo.Keyword by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="keywordID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool KeywordDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int keywordID)
		{
			return KeywordDeleteAuto( sqlConnection, sqlTransaction, "BHL", keywordID );
		}
		
		/// <summary>
		/// Delete values from dbo.Keyword by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="keywordID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool KeywordDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int keywordID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("KeywordDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("KeywordID", SqlDbType.Int, null, false, keywordID), 
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
		/// Update values in dbo.Keyword. Returns an object of type Keyword.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="keywordID"></param>
		/// <param name="keyword"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type Keyword.</returns>
		public Keyword KeywordUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int keywordID,
			string keyword,
			int? lastModifiedUserID)
		{
			return KeywordUpdateAuto( sqlConnection, sqlTransaction, "BHL", keywordID, keyword, lastModifiedUserID);
		}
		
		/// <summary>
		/// Update values in dbo.Keyword. Returns an object of type Keyword.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="keywordID"></param>
		/// <param name="keyword"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type Keyword.</returns>
		public Keyword KeywordUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int keywordID,
			string keyword,
			int? lastModifiedUserID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("KeywordUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("KeywordID", SqlDbType.Int, null, false, keywordID),
					CustomSqlHelper.CreateInputParameter("Keyword", SqlDbType.NVarChar, 50, false, keyword),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, true, lastModifiedUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<Keyword> helper = new CustomSqlHelper<Keyword>())
				{
					List<Keyword> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						Keyword o = list[0];
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
		/// Update values in dbo.Keyword. Returns an object of type Keyword.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type Keyword.</param>
		/// <returns>Object of type Keyword.</returns>
		public Keyword KeywordUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			Keyword value)
		{
			return KeywordUpdateAuto(sqlConnection, sqlTransaction, "BHL", value );
		}
		
		/// <summary>
		/// Update values in dbo.Keyword. Returns an object of type Keyword.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type Keyword.</param>
		/// <returns>Object of type Keyword.</returns>
		public Keyword KeywordUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			Keyword value)
		{
			return KeywordUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.KeywordID,
				value.Keyword,
				value.LastModifiedUserID);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage dbo.Keyword object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.Keyword.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type Keyword.</param>
		/// <returns>Object of type CustomDataAccessStatus<Keyword>.</returns>
		public CustomDataAccessStatus<Keyword> KeywordManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			Keyword value , int userId )
		{
			return KeywordManageAuto( sqlConnection, sqlTransaction, "BHL", value , userId );
		}
		
		/// <summary>
		/// Manage dbo.Keyword object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.Keyword.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type Keyword.</param>
		/// <returns>Object of type CustomDataAccessStatus<Keyword>.</returns>
		public CustomDataAccessStatus<Keyword> KeywordManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			Keyword value , int userId )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				value.CreationUserID = userId;
				value.LastModifiedUserID = userId;
				Keyword returnValue = KeywordInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.Keyword,
						value.CreationUserID,
						value.LastModifiedUserID);
				
				return new CustomDataAccessStatus<Keyword>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (KeywordDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.KeywordID))
				{
				return new CustomDataAccessStatus<Keyword>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<Keyword>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				value.LastModifiedUserID = userId;
				Keyword returnValue = KeywordUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.KeywordID,
						value.Keyword,
						value.LastModifiedUserID);
					
				return new CustomDataAccessStatus<Keyword>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<Keyword>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}

