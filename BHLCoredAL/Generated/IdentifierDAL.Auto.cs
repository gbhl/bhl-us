
// Generated 1/5/2021 3:25:16 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class IdentifierDAL is based upon dbo.Identifier.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DAL
// {
// 		public partial class IdentifierDAL
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
	partial class IdentifierDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from dbo.Identifier by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="identifierID"></param>
		/// <returns>Object of type Identifier.</returns>
		public Identifier IdentifierSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int identifierID)
		{
			return IdentifierSelectAuto(	sqlConnection, sqlTransaction, "BHL",	identifierID );
		}
			
		/// <summary>
		/// Select values from dbo.Identifier by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="identifierID"></param>
		/// <returns>Object of type Identifier.</returns>
		public Identifier IdentifierSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int identifierID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IdentifierSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("IdentifierID", SqlDbType.Int, null, false, identifierID)))
			{
				using (CustomSqlHelper<Identifier> helper = new CustomSqlHelper<Identifier>())
				{
					List<Identifier> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						Identifier o = list[0];
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
		/// Select values from dbo.Identifier by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="identifierID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> IdentifierSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int identifierID)
		{
			return IdentifierSelectAutoRaw( sqlConnection, sqlTransaction, "BHL", identifierID );
		}
		
		/// <summary>
		/// Select values from dbo.Identifier by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="identifierID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> IdentifierSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int identifierID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IdentifierSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("IdentifierID", SqlDbType.Int, null, false, identifierID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====

 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into dbo.Identifier.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="identifierName"></param>
		/// <param name="identifierLabel"></param>
		/// <param name="display"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type Identifier.</returns>
		public Identifier IdentifierInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string identifierName,
			string identifierLabel,
			short display,
			int? creationUserID,
			int? lastModifiedUserID)
		{
			return IdentifierInsertAuto( sqlConnection, sqlTransaction, "BHL", identifierName, identifierLabel, display, creationUserID, lastModifiedUserID );
		}
		
		/// <summary>
		/// Insert values into dbo.Identifier.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="identifierName"></param>
		/// <param name="identifierLabel"></param>
		/// <param name="display"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type Identifier.</returns>
		public Identifier IdentifierInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			string identifierName,
			string identifierLabel,
			short display,
			int? creationUserID,
			int? lastModifiedUserID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IdentifierInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("IdentifierID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("IdentifierName", SqlDbType.NVarChar, 40, false, identifierName),
					CustomSqlHelper.CreateInputParameter("IdentifierLabel", SqlDbType.NVarChar, 50, false, identifierLabel),
					CustomSqlHelper.CreateInputParameter("Display", SqlDbType.SmallInt, null, false, display),
					CustomSqlHelper.CreateInputParameter("CreationUserID", SqlDbType.Int, null, true, creationUserID),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, true, lastModifiedUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<Identifier> helper = new CustomSqlHelper<Identifier>())
				{
					List<Identifier> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						Identifier o = list[0];
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
		/// Insert values into dbo.Identifier. Returns an object of type Identifier.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type Identifier.</param>
		/// <returns>Object of type Identifier.</returns>
		public Identifier IdentifierInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			Identifier value)
		{
			return IdentifierInsertAuto(sqlConnection, sqlTransaction, "BHL", value);
		}
		
		/// <summary>
		/// Insert values into dbo.Identifier. Returns an object of type Identifier.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type Identifier.</param>
		/// <returns>Object of type Identifier.</returns>
		public Identifier IdentifierInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			Identifier value)
		{
			return IdentifierInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.IdentifierName,
				value.IdentifierLabel,
				value.Display,
				value.CreationUserID,
				value.LastModifiedUserID);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from dbo.Identifier by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="identifierID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool IdentifierDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int identifierID)
		{
			return IdentifierDeleteAuto( sqlConnection, sqlTransaction, "BHL", identifierID );
		}
		
		/// <summary>
		/// Delete values from dbo.Identifier by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="identifierID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool IdentifierDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int identifierID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IdentifierDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("IdentifierID", SqlDbType.Int, null, false, identifierID), 
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
		/// Update values in dbo.Identifier. Returns an object of type Identifier.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="identifierID"></param>
		/// <param name="identifierName"></param>
		/// <param name="identifierLabel"></param>
		/// <param name="display"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type Identifier.</returns>
		public Identifier IdentifierUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int identifierID,
			string identifierName,
			string identifierLabel,
			short display,
			int? lastModifiedUserID)
		{
			return IdentifierUpdateAuto( sqlConnection, sqlTransaction, "BHL", identifierID, identifierName, identifierLabel, display, lastModifiedUserID);
		}
		
		/// <summary>
		/// Update values in dbo.Identifier. Returns an object of type Identifier.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="identifierID"></param>
		/// <param name="identifierName"></param>
		/// <param name="identifierLabel"></param>
		/// <param name="display"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type Identifier.</returns>
		public Identifier IdentifierUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int identifierID,
			string identifierName,
			string identifierLabel,
			short display,
			int? lastModifiedUserID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IdentifierUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("IdentifierID", SqlDbType.Int, null, false, identifierID),
					CustomSqlHelper.CreateInputParameter("IdentifierName", SqlDbType.NVarChar, 40, false, identifierName),
					CustomSqlHelper.CreateInputParameter("IdentifierLabel", SqlDbType.NVarChar, 50, false, identifierLabel),
					CustomSqlHelper.CreateInputParameter("Display", SqlDbType.SmallInt, null, false, display),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, true, lastModifiedUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<Identifier> helper = new CustomSqlHelper<Identifier>())
				{
					List<Identifier> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						Identifier o = list[0];
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
		/// Update values in dbo.Identifier. Returns an object of type Identifier.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type Identifier.</param>
		/// <returns>Object of type Identifier.</returns>
		public Identifier IdentifierUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			Identifier value)
		{
			return IdentifierUpdateAuto(sqlConnection, sqlTransaction, "BHL", value );
		}
		
		/// <summary>
		/// Update values in dbo.Identifier. Returns an object of type Identifier.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type Identifier.</param>
		/// <returns>Object of type Identifier.</returns>
		public Identifier IdentifierUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			Identifier value)
		{
			return IdentifierUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.IdentifierID,
				value.IdentifierName,
				value.IdentifierLabel,
				value.Display,
				value.LastModifiedUserID);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage dbo.Identifier object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.Identifier.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type Identifier.</param>
		/// <returns>Object of type CustomDataAccessStatus<Identifier>.</returns>
		public CustomDataAccessStatus<Identifier> IdentifierManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			Identifier value , int userId )
		{
			return IdentifierManageAuto( sqlConnection, sqlTransaction, "BHL", value , userId );
		}
		
		/// <summary>
		/// Manage dbo.Identifier object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.Identifier.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type Identifier.</param>
		/// <returns>Object of type CustomDataAccessStatus<Identifier>.</returns>
		public CustomDataAccessStatus<Identifier> IdentifierManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			Identifier value , int userId )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				value.CreationUserID = userId;
				value.LastModifiedUserID = userId;
				Identifier returnValue = IdentifierInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.IdentifierName,
						value.IdentifierLabel,
						value.Display,
						value.CreationUserID,
						value.LastModifiedUserID);
				
				return new CustomDataAccessStatus<Identifier>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (IdentifierDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.IdentifierID))
				{
				return new CustomDataAccessStatus<Identifier>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<Identifier>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				value.LastModifiedUserID = userId;
				Identifier returnValue = IdentifierUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.IdentifierID,
						value.IdentifierName,
						value.IdentifierLabel,
						value.Display,
						value.LastModifiedUserID);
					
				return new CustomDataAccessStatus<Identifier>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<Identifier>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}

