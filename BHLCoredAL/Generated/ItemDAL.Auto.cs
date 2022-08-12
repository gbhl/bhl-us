
// Generated 1/5/2021 3:25:29 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class ItemDAL is based upon dbo.Item.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DAL
// {
// 		public partial class ItemDAL
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
	partial class ItemDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from dbo.Item by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="itemID"></param>
		/// <returns>Object of type Item.</returns>
		public Item ItemSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int itemID)
		{
			return ItemSelectAuto(	sqlConnection, sqlTransaction, "BHL",	itemID );
		}
			
		/// <summary>
		/// Select values from dbo.Item by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="itemID"></param>
		/// <returns>Object of type Item.</returns>
		public Item ItemSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int itemID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("ItemSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemID)))
			{
				using (CustomSqlHelper<Item> helper = new CustomSqlHelper<Item>())
				{
					List<Item> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						Item o = list[0];
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
		/// Select values from dbo.Item by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="itemID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> ItemSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int itemID)
		{
			return ItemSelectAutoRaw( sqlConnection, sqlTransaction, "BHL", itemID );
		}
		
		/// <summary>
		/// Select values from dbo.Item by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="itemID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> ItemSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int itemID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("ItemSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====

 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into dbo.Item.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="itemTypeID"></param>
		/// <param name="itemStatusID"></param>
		/// <param name="itemSourceID"></param>
		/// <param name="vaultID"></param>
		/// <param name="fileRootFolder"></param>
		/// <param name="itemDescription"></param>
		/// <param name="note"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type Item.</returns>
		public Item ItemInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int? itemTypeID,
			int itemStatusID,
			int? itemSourceID,
			int? vaultID,
			string fileRootFolder,
			string itemDescription,
			string note,
			int? creationUserID,
			int? lastModifiedUserID)
		{
			return ItemInsertAuto( sqlConnection, sqlTransaction, "BHL", itemTypeID, itemStatusID, itemSourceID, vaultID, fileRootFolder, itemDescription, note, creationUserID, lastModifiedUserID );
		}
		
		/// <summary>
		/// Insert values into dbo.Item.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="itemTypeID"></param>
		/// <param name="itemStatusID"></param>
		/// <param name="itemSourceID"></param>
		/// <param name="vaultID"></param>
		/// <param name="fileRootFolder"></param>
		/// <param name="itemDescription"></param>
		/// <param name="note"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type Item.</returns>
		public Item ItemInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int? itemTypeID,
			int itemStatusID,
			int? itemSourceID,
			int? vaultID,
			string fileRootFolder,
			string itemDescription,
			string note,
			int? creationUserID,
			int? lastModifiedUserID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("ItemInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("ItemID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("ItemTypeID", SqlDbType.Int, null, true, itemTypeID),
					CustomSqlHelper.CreateInputParameter("ItemStatusID", SqlDbType.Int, null, false, itemStatusID),
					CustomSqlHelper.CreateInputParameter("ItemSourceID", SqlDbType.Int, null, true, itemSourceID),
					CustomSqlHelper.CreateInputParameter("VaultID", SqlDbType.Int, null, true, vaultID),
					CustomSqlHelper.CreateInputParameter("FileRootFolder", SqlDbType.NVarChar, 250, true, fileRootFolder),
					CustomSqlHelper.CreateInputParameter("ItemDescription", SqlDbType.NVarChar, 1073741823, true, itemDescription),
					CustomSqlHelper.CreateInputParameter("Note", SqlDbType.NVarChar, 1073741823, true, note),
					CustomSqlHelper.CreateInputParameter("CreationUserID", SqlDbType.Int, null, true, creationUserID),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, true, lastModifiedUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<Item> helper = new CustomSqlHelper<Item>())
				{
					List<Item> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						Item o = list[0];
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
		/// Insert values into dbo.Item. Returns an object of type Item.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type Item.</param>
		/// <returns>Object of type Item.</returns>
		public Item ItemInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			Item value)
		{
			return ItemInsertAuto(sqlConnection, sqlTransaction, "BHL", value);
		}
		
		/// <summary>
		/// Insert values into dbo.Item. Returns an object of type Item.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type Item.</param>
		/// <returns>Object of type Item.</returns>
		public Item ItemInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			Item value)
		{
			return ItemInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.ItemTypeID,
				value.ItemStatusID,
				value.ItemSourceID,
				value.VaultID,
				value.FileRootFolder,
				value.ItemDescription,
				value.Note,
				value.CreationUserID,
				value.LastModifiedUserID);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from dbo.Item by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="itemID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool ItemDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int itemID)
		{
			return ItemDeleteAuto( sqlConnection, sqlTransaction, "BHL", itemID );
		}
		
		/// <summary>
		/// Delete values from dbo.Item by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="itemID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool ItemDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int itemID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("ItemDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemID), 
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
		/// Update values in dbo.Item. Returns an object of type Item.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="itemID"></param>
		/// <param name="itemTypeID"></param>
		/// <param name="itemStatusID"></param>
		/// <param name="itemSourceID"></param>
		/// <param name="vaultID"></param>
		/// <param name="fileRootFolder"></param>
		/// <param name="itemDescription"></param>
		/// <param name="note"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type Item.</returns>
		public Item ItemUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int itemID,
			int? itemTypeID,
			int itemStatusID,
			int? itemSourceID,
			int? vaultID,
			string fileRootFolder,
			string itemDescription,
			string note,
			int? lastModifiedUserID)
		{
			return ItemUpdateAuto( sqlConnection, sqlTransaction, "BHL", itemID, itemTypeID, itemStatusID, itemSourceID, vaultID, fileRootFolder, itemDescription, note, lastModifiedUserID);
		}
		
		/// <summary>
		/// Update values in dbo.Item. Returns an object of type Item.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="itemID"></param>
		/// <param name="itemTypeID"></param>
		/// <param name="itemStatusID"></param>
		/// <param name="itemSourceID"></param>
		/// <param name="vaultID"></param>
		/// <param name="fileRootFolder"></param>
		/// <param name="itemDescription"></param>
		/// <param name="note"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type Item.</returns>
		public Item ItemUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int itemID,
			int? itemTypeID,
			int itemStatusID,
			int? itemSourceID,
			int? vaultID,
			string fileRootFolder,
			string itemDescription,
			string note,
			int? lastModifiedUserID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("ItemUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemID),
					CustomSqlHelper.CreateInputParameter("ItemTypeID", SqlDbType.Int, null, true, itemTypeID),
					CustomSqlHelper.CreateInputParameter("ItemStatusID", SqlDbType.Int, null, false, itemStatusID),
					CustomSqlHelper.CreateInputParameter("ItemSourceID", SqlDbType.Int, null, true, itemSourceID),
					CustomSqlHelper.CreateInputParameter("VaultID", SqlDbType.Int, null, true, vaultID),
					CustomSqlHelper.CreateInputParameter("FileRootFolder", SqlDbType.NVarChar, 250, true, fileRootFolder),
					CustomSqlHelper.CreateInputParameter("ItemDescription", SqlDbType.NVarChar, 1073741823, true, itemDescription),
					CustomSqlHelper.CreateInputParameter("Note", SqlDbType.NVarChar, 1073741823, true, note),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, true, lastModifiedUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<Item> helper = new CustomSqlHelper<Item>())
				{
					List<Item> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						Item o = list[0];
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
		/// Update values in dbo.Item. Returns an object of type Item.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type Item.</param>
		/// <returns>Object of type Item.</returns>
		public Item ItemUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			Item value)
		{
			return ItemUpdateAuto(sqlConnection, sqlTransaction, "BHL", value );
		}
		
		/// <summary>
		/// Update values in dbo.Item. Returns an object of type Item.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type Item.</param>
		/// <returns>Object of type Item.</returns>
		public Item ItemUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			Item value)
		{
			return ItemUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.ItemID,
				value.ItemTypeID,
				value.ItemStatusID,
				value.ItemSourceID,
				value.VaultID,
				value.FileRootFolder,
				value.ItemDescription,
				value.Note,
				value.LastModifiedUserID);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage dbo.Item object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.Item.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type Item.</param>
		/// <returns>Object of type CustomDataAccessStatus<Item>.</returns>
		public CustomDataAccessStatus<Item> ItemManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			Item value , int userId )
		{
			return ItemManageAuto( sqlConnection, sqlTransaction, "BHL", value , userId );
		}
		
		/// <summary>
		/// Manage dbo.Item object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.Item.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type Item.</param>
		/// <returns>Object of type CustomDataAccessStatus<Item>.</returns>
		public CustomDataAccessStatus<Item> ItemManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			Item value , int userId )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				value.CreationUserID = userId;
				value.LastModifiedUserID = userId;
				Item returnValue = ItemInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.ItemTypeID,
						value.ItemStatusID,
						value.ItemSourceID,
						value.VaultID,
						value.FileRootFolder,
						value.ItemDescription,
						value.Note,
						value.CreationUserID,
						value.LastModifiedUserID);
				
				return new CustomDataAccessStatus<Item>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (ItemDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.ItemID))
				{
				return new CustomDataAccessStatus<Item>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<Item>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				value.LastModifiedUserID = userId;
				Item returnValue = ItemUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.ItemID,
						value.ItemTypeID,
						value.ItemStatusID,
						value.ItemSourceID,
						value.VaultID,
						value.FileRootFolder,
						value.ItemDescription,
						value.Note,
						value.LastModifiedUserID);
					
				return new CustomDataAccessStatus<Item>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<Item>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}

