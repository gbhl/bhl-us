
// Generated 1/5/2021 3:25:35 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class ItemIdentifierDAL is based upon dbo.ItemIdentifier.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DAL
// {
// 		public partial class ItemIdentifierDAL
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
	partial class ItemIdentifierDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from dbo.ItemIdentifier by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="itemIdentifierID"></param>
		/// <returns>Object of type ItemIdentifier.</returns>
		public ItemIdentifier ItemIdentifierSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int itemIdentifierID)
		{
			return ItemIdentifierSelectAuto(	sqlConnection, sqlTransaction, "BHL",	itemIdentifierID );
		}
			
		/// <summary>
		/// Select values from dbo.ItemIdentifier by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="itemIdentifierID"></param>
		/// <returns>Object of type ItemIdentifier.</returns>
		public ItemIdentifier ItemIdentifierSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int itemIdentifierID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("ItemIdentifierSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("ItemIdentifierID", SqlDbType.Int, null, false, itemIdentifierID)))
			{
				using (CustomSqlHelper<ItemIdentifier> helper = new CustomSqlHelper<ItemIdentifier>())
				{
					List<ItemIdentifier> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						ItemIdentifier o = list[0];
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
		/// Select values from dbo.ItemIdentifier by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="itemIdentifierID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> ItemIdentifierSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int itemIdentifierID)
		{
			return ItemIdentifierSelectAutoRaw( sqlConnection, sqlTransaction, "BHL", itemIdentifierID );
		}
		
		/// <summary>
		/// Select values from dbo.ItemIdentifier by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="itemIdentifierID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> ItemIdentifierSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int itemIdentifierID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("ItemIdentifierSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("ItemIdentifierID", SqlDbType.Int, null, false, itemIdentifierID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====

 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into dbo.ItemIdentifier.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="itemID"></param>
		/// <param name="identifierID"></param>
		/// <param name="identifierValue"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type ItemIdentifier.</returns>
		public ItemIdentifier ItemIdentifierInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int itemID,
			int identifierID,
			string identifierValue,
			int? creationUserID,
			int? lastModifiedUserID)
		{
			return ItemIdentifierInsertAuto( sqlConnection, sqlTransaction, "BHL", itemID, identifierID, identifierValue, creationUserID, lastModifiedUserID );
		}
		
		/// <summary>
		/// Insert values into dbo.ItemIdentifier.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="itemID"></param>
		/// <param name="identifierID"></param>
		/// <param name="identifierValue"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type ItemIdentifier.</returns>
		public ItemIdentifier ItemIdentifierInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int itemID,
			int identifierID,
			string identifierValue,
			int? creationUserID,
			int? lastModifiedUserID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("ItemIdentifierInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("ItemIdentifierID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemID),
					CustomSqlHelper.CreateInputParameter("IdentifierID", SqlDbType.Int, null, false, identifierID),
					CustomSqlHelper.CreateInputParameter("IdentifierValue", SqlDbType.NVarChar, 125, false, identifierValue),
					CustomSqlHelper.CreateInputParameter("CreationUserID", SqlDbType.Int, null, true, creationUserID),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, true, lastModifiedUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<ItemIdentifier> helper = new CustomSqlHelper<ItemIdentifier>())
				{
					List<ItemIdentifier> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						ItemIdentifier o = list[0];
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
		/// Insert values into dbo.ItemIdentifier. Returns an object of type ItemIdentifier.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type ItemIdentifier.</param>
		/// <returns>Object of type ItemIdentifier.</returns>
		public ItemIdentifier ItemIdentifierInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			ItemIdentifier value)
		{
			return ItemIdentifierInsertAuto(sqlConnection, sqlTransaction, "BHL", value);
		}
		
		/// <summary>
		/// Insert values into dbo.ItemIdentifier. Returns an object of type ItemIdentifier.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type ItemIdentifier.</param>
		/// <returns>Object of type ItemIdentifier.</returns>
		public ItemIdentifier ItemIdentifierInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			ItemIdentifier value)
		{
			return ItemIdentifierInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.ItemID,
				value.IdentifierID,
				value.IdentifierValue,
				value.CreationUserID,
				value.LastModifiedUserID);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from dbo.ItemIdentifier by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="itemIdentifierID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool ItemIdentifierDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int itemIdentifierID)
		{
			return ItemIdentifierDeleteAuto( sqlConnection, sqlTransaction, "BHL", itemIdentifierID );
		}
		
		/// <summary>
		/// Delete values from dbo.ItemIdentifier by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="itemIdentifierID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool ItemIdentifierDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int itemIdentifierID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("ItemIdentifierDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("ItemIdentifierID", SqlDbType.Int, null, false, itemIdentifierID), 
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
		/// Update values in dbo.ItemIdentifier. Returns an object of type ItemIdentifier.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="itemIdentifierID"></param>
		/// <param name="itemID"></param>
		/// <param name="identifierID"></param>
		/// <param name="identifierValue"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type ItemIdentifier.</returns>
		public ItemIdentifier ItemIdentifierUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int itemIdentifierID,
			int itemID,
			int identifierID,
			string identifierValue,
			int? lastModifiedUserID)
		{
			return ItemIdentifierUpdateAuto( sqlConnection, sqlTransaction, "BHL", itemIdentifierID, itemID, identifierID, identifierValue, lastModifiedUserID);
		}
		
		/// <summary>
		/// Update values in dbo.ItemIdentifier. Returns an object of type ItemIdentifier.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="itemIdentifierID"></param>
		/// <param name="itemID"></param>
		/// <param name="identifierID"></param>
		/// <param name="identifierValue"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type ItemIdentifier.</returns>
		public ItemIdentifier ItemIdentifierUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int itemIdentifierID,
			int itemID,
			int identifierID,
			string identifierValue,
			int? lastModifiedUserID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("ItemIdentifierUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("ItemIdentifierID", SqlDbType.Int, null, false, itemIdentifierID),
					CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemID),
					CustomSqlHelper.CreateInputParameter("IdentifierID", SqlDbType.Int, null, false, identifierID),
					CustomSqlHelper.CreateInputParameter("IdentifierValue", SqlDbType.NVarChar, 125, false, identifierValue),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, true, lastModifiedUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<ItemIdentifier> helper = new CustomSqlHelper<ItemIdentifier>())
				{
					List<ItemIdentifier> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						ItemIdentifier o = list[0];
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
		/// Update values in dbo.ItemIdentifier. Returns an object of type ItemIdentifier.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type ItemIdentifier.</param>
		/// <returns>Object of type ItemIdentifier.</returns>
		public ItemIdentifier ItemIdentifierUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			ItemIdentifier value)
		{
			return ItemIdentifierUpdateAuto(sqlConnection, sqlTransaction, "BHL", value );
		}
		
		/// <summary>
		/// Update values in dbo.ItemIdentifier. Returns an object of type ItemIdentifier.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type ItemIdentifier.</param>
		/// <returns>Object of type ItemIdentifier.</returns>
		public ItemIdentifier ItemIdentifierUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			ItemIdentifier value)
		{
			return ItemIdentifierUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.ItemIdentifierID,
				value.ItemID,
				value.IdentifierID,
				value.IdentifierValue,
				value.LastModifiedUserID);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage dbo.ItemIdentifier object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.ItemIdentifier.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type ItemIdentifier.</param>
		/// <returns>Object of type CustomDataAccessStatus<ItemIdentifier>.</returns>
		public CustomDataAccessStatus<ItemIdentifier> ItemIdentifierManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			ItemIdentifier value , int userId )
		{
			return ItemIdentifierManageAuto( sqlConnection, sqlTransaction, "BHL", value , userId );
		}
		
		/// <summary>
		/// Manage dbo.ItemIdentifier object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.ItemIdentifier.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type ItemIdentifier.</param>
		/// <returns>Object of type CustomDataAccessStatus<ItemIdentifier>.</returns>
		public CustomDataAccessStatus<ItemIdentifier> ItemIdentifierManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			ItemIdentifier value , int userId )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				value.CreationUserID = userId;
				value.LastModifiedUserID = userId;
				ItemIdentifier returnValue = ItemIdentifierInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.ItemID,
						value.IdentifierID,
						value.IdentifierValue,
						value.CreationUserID,
						value.LastModifiedUserID);
				
				return new CustomDataAccessStatus<ItemIdentifier>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (ItemIdentifierDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.ItemIdentifierID))
				{
				return new CustomDataAccessStatus<ItemIdentifier>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<ItemIdentifier>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				value.LastModifiedUserID = userId;
				ItemIdentifier returnValue = ItemIdentifierUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.ItemIdentifierID,
						value.ItemID,
						value.IdentifierID,
						value.IdentifierValue,
						value.LastModifiedUserID);
					
				return new CustomDataAccessStatus<ItemIdentifier>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<ItemIdentifier>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}

