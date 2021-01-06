
// Generated 1/5/2021 3:26:19 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class NameIdentifierDAL is based upon dbo.NameIdentifier.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DAL
// {
// 		public partial class NameIdentifierDAL
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
	partial class NameIdentifierDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from dbo.NameIdentifier by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="nameIdentifierID"></param>
		/// <returns>Object of type NameIdentifier.</returns>
		public NameIdentifier NameIdentifierSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int nameIdentifierID)
		{
			return NameIdentifierSelectAuto(	sqlConnection, sqlTransaction, "BHL",	nameIdentifierID );
		}
			
		/// <summary>
		/// Select values from dbo.NameIdentifier by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="nameIdentifierID"></param>
		/// <returns>Object of type NameIdentifier.</returns>
		public NameIdentifier NameIdentifierSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int nameIdentifierID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("NameIdentifierSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("NameIdentifierID", SqlDbType.Int, null, false, nameIdentifierID)))
			{
				using (CustomSqlHelper<NameIdentifier> helper = new CustomSqlHelper<NameIdentifier>())
				{
					List<NameIdentifier> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						NameIdentifier o = list[0];
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
		/// Select values from dbo.NameIdentifier by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="nameIdentifierID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> NameIdentifierSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int nameIdentifierID)
		{
			return NameIdentifierSelectAutoRaw( sqlConnection, sqlTransaction, "BHL", nameIdentifierID );
		}
		
		/// <summary>
		/// Select values from dbo.NameIdentifier by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="nameIdentifierID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> NameIdentifierSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int nameIdentifierID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("NameIdentifierSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("NameIdentifierID", SqlDbType.Int, null, false, nameIdentifierID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====

 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into dbo.NameIdentifier.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="nameResolvedID"></param>
		/// <param name="identifierID"></param>
		/// <param name="identifierValue"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type NameIdentifier.</returns>
		public NameIdentifier NameIdentifierInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int nameResolvedID,
			int identifierID,
			string identifierValue,
			int? creationUserID,
			int? lastModifiedUserID)
		{
			return NameIdentifierInsertAuto( sqlConnection, sqlTransaction, "BHL", nameResolvedID, identifierID, identifierValue, creationUserID, lastModifiedUserID );
		}
		
		/// <summary>
		/// Insert values into dbo.NameIdentifier.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="nameResolvedID"></param>
		/// <param name="identifierID"></param>
		/// <param name="identifierValue"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type NameIdentifier.</returns>
		public NameIdentifier NameIdentifierInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int nameResolvedID,
			int identifierID,
			string identifierValue,
			int? creationUserID,
			int? lastModifiedUserID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("NameIdentifierInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("NameIdentifierID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("NameResolvedID", SqlDbType.Int, null, false, nameResolvedID),
					CustomSqlHelper.CreateInputParameter("IdentifierID", SqlDbType.Int, null, false, identifierID),
					CustomSqlHelper.CreateInputParameter("IdentifierValue", SqlDbType.NVarChar, 100, false, identifierValue),
					CustomSqlHelper.CreateInputParameter("CreationUserID", SqlDbType.Int, null, true, creationUserID),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, true, lastModifiedUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<NameIdentifier> helper = new CustomSqlHelper<NameIdentifier>())
				{
					List<NameIdentifier> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						NameIdentifier o = list[0];
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
		/// Insert values into dbo.NameIdentifier. Returns an object of type NameIdentifier.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type NameIdentifier.</param>
		/// <returns>Object of type NameIdentifier.</returns>
		public NameIdentifier NameIdentifierInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			NameIdentifier value)
		{
			return NameIdentifierInsertAuto(sqlConnection, sqlTransaction, "BHL", value);
		}
		
		/// <summary>
		/// Insert values into dbo.NameIdentifier. Returns an object of type NameIdentifier.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type NameIdentifier.</param>
		/// <returns>Object of type NameIdentifier.</returns>
		public NameIdentifier NameIdentifierInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			NameIdentifier value)
		{
			return NameIdentifierInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.NameResolvedID,
				value.IdentifierID,
				value.IdentifierValue,
				value.CreationUserID,
				value.LastModifiedUserID);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from dbo.NameIdentifier by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="nameIdentifierID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool NameIdentifierDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int nameIdentifierID)
		{
			return NameIdentifierDeleteAuto( sqlConnection, sqlTransaction, "BHL", nameIdentifierID );
		}
		
		/// <summary>
		/// Delete values from dbo.NameIdentifier by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="nameIdentifierID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool NameIdentifierDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int nameIdentifierID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("NameIdentifierDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("NameIdentifierID", SqlDbType.Int, null, false, nameIdentifierID), 
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
		/// Update values in dbo.NameIdentifier. Returns an object of type NameIdentifier.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="nameIdentifierID"></param>
		/// <param name="nameResolvedID"></param>
		/// <param name="identifierID"></param>
		/// <param name="identifierValue"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type NameIdentifier.</returns>
		public NameIdentifier NameIdentifierUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int nameIdentifierID,
			int nameResolvedID,
			int identifierID,
			string identifierValue,
			int? lastModifiedUserID)
		{
			return NameIdentifierUpdateAuto( sqlConnection, sqlTransaction, "BHL", nameIdentifierID, nameResolvedID, identifierID, identifierValue, lastModifiedUserID);
		}
		
		/// <summary>
		/// Update values in dbo.NameIdentifier. Returns an object of type NameIdentifier.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="nameIdentifierID"></param>
		/// <param name="nameResolvedID"></param>
		/// <param name="identifierID"></param>
		/// <param name="identifierValue"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type NameIdentifier.</returns>
		public NameIdentifier NameIdentifierUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int nameIdentifierID,
			int nameResolvedID,
			int identifierID,
			string identifierValue,
			int? lastModifiedUserID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("NameIdentifierUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("NameIdentifierID", SqlDbType.Int, null, false, nameIdentifierID),
					CustomSqlHelper.CreateInputParameter("NameResolvedID", SqlDbType.Int, null, false, nameResolvedID),
					CustomSqlHelper.CreateInputParameter("IdentifierID", SqlDbType.Int, null, false, identifierID),
					CustomSqlHelper.CreateInputParameter("IdentifierValue", SqlDbType.NVarChar, 100, false, identifierValue),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, true, lastModifiedUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<NameIdentifier> helper = new CustomSqlHelper<NameIdentifier>())
				{
					List<NameIdentifier> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						NameIdentifier o = list[0];
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
		/// Update values in dbo.NameIdentifier. Returns an object of type NameIdentifier.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type NameIdentifier.</param>
		/// <returns>Object of type NameIdentifier.</returns>
		public NameIdentifier NameIdentifierUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			NameIdentifier value)
		{
			return NameIdentifierUpdateAuto(sqlConnection, sqlTransaction, "BHL", value );
		}
		
		/// <summary>
		/// Update values in dbo.NameIdentifier. Returns an object of type NameIdentifier.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type NameIdentifier.</param>
		/// <returns>Object of type NameIdentifier.</returns>
		public NameIdentifier NameIdentifierUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			NameIdentifier value)
		{
			return NameIdentifierUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.NameIdentifierID,
				value.NameResolvedID,
				value.IdentifierID,
				value.IdentifierValue,
				value.LastModifiedUserID);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage dbo.NameIdentifier object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.NameIdentifier.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type NameIdentifier.</param>
		/// <returns>Object of type CustomDataAccessStatus<NameIdentifier>.</returns>
		public CustomDataAccessStatus<NameIdentifier> NameIdentifierManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			NameIdentifier value , int userId )
		{
			return NameIdentifierManageAuto( sqlConnection, sqlTransaction, "BHL", value , userId );
		}
		
		/// <summary>
		/// Manage dbo.NameIdentifier object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.NameIdentifier.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type NameIdentifier.</param>
		/// <returns>Object of type CustomDataAccessStatus<NameIdentifier>.</returns>
		public CustomDataAccessStatus<NameIdentifier> NameIdentifierManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			NameIdentifier value , int userId )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				value.CreationUserID = userId;
				value.LastModifiedUserID = userId;
				NameIdentifier returnValue = NameIdentifierInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.NameResolvedID,
						value.IdentifierID,
						value.IdentifierValue,
						value.CreationUserID,
						value.LastModifiedUserID);
				
				return new CustomDataAccessStatus<NameIdentifier>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (NameIdentifierDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.NameIdentifierID))
				{
				return new CustomDataAccessStatus<NameIdentifier>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<NameIdentifier>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				value.LastModifiedUserID = userId;
				NameIdentifier returnValue = NameIdentifierUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.NameIdentifierID,
						value.NameResolvedID,
						value.IdentifierID,
						value.IdentifierValue,
						value.LastModifiedUserID);
					
				return new CustomDataAccessStatus<NameIdentifier>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<NameIdentifier>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}

