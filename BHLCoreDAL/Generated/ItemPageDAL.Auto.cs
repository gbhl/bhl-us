
// Generated 10/22/2020 8:48:38 AM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class ItemPageDAL is based upon dbo.ItemPage.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DAL
// {
// 		public partial class ItemPageDAL
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
	partial class ItemPageDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from dbo.ItemPage by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="itemPageID"></param>
		/// <returns>Object of type ItemPage.</returns>
		public ItemPage ItemPageSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int itemPageID)
		{
			return ItemPageSelectAuto(	sqlConnection, sqlTransaction, "BHL",	itemPageID );
		}
			
		/// <summary>
		/// Select values from dbo.ItemPage by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="itemPageID"></param>
		/// <returns>Object of type ItemPage.</returns>
		public ItemPage ItemPageSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int itemPageID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("ItemPageSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("ItemPageID", SqlDbType.Int, null, false, itemPageID)))
			{
				using (CustomSqlHelper<ItemPage> helper = new CustomSqlHelper<ItemPage>())
				{
					CustomGenericList<ItemPage> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						ItemPage o = list[0];
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
		/// Select values from dbo.ItemPage by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="itemPageID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> ItemPageSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int itemPageID)
		{
			return ItemPageSelectAutoRaw( sqlConnection, sqlTransaction, "BHL", itemPageID );
		}
		
		/// <summary>
		/// Select values from dbo.ItemPage by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="itemPageID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> ItemPageSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int itemPageID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("ItemPageSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("ItemPageID", SqlDbType.Int, null, false, itemPageID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====

 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into dbo.ItemPage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="itemID"></param>
		/// <param name="pageID"></param>
		/// <param name="sequenceOrder"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type ItemPage.</returns>
		public ItemPage ItemPageInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int itemID,
			int pageID,
			int sequenceOrder,
			int? creationUserID,
			int? lastModifiedUserID)
		{
			return ItemPageInsertAuto( sqlConnection, sqlTransaction, "BHL", itemID, pageID, sequenceOrder, creationUserID, lastModifiedUserID );
		}
		
		/// <summary>
		/// Insert values into dbo.ItemPage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="itemID"></param>
		/// <param name="pageID"></param>
		/// <param name="sequenceOrder"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type ItemPage.</returns>
		public ItemPage ItemPageInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int itemID,
			int pageID,
			int sequenceOrder,
			int? creationUserID,
			int? lastModifiedUserID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("ItemPageInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("ItemPageID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemID),
					CustomSqlHelper.CreateInputParameter("PageID", SqlDbType.Int, null, false, pageID),
					CustomSqlHelper.CreateInputParameter("SequenceOrder", SqlDbType.Int, null, false, sequenceOrder),
					CustomSqlHelper.CreateInputParameter("CreationUserID", SqlDbType.Int, null, true, creationUserID),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, true, lastModifiedUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<ItemPage> helper = new CustomSqlHelper<ItemPage>())
				{
					CustomGenericList<ItemPage> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						ItemPage o = list[0];
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
		/// Insert values into dbo.ItemPage. Returns an object of type ItemPage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type ItemPage.</param>
		/// <returns>Object of type ItemPage.</returns>
		public ItemPage ItemPageInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			ItemPage value)
		{
			return ItemPageInsertAuto(sqlConnection, sqlTransaction, "BHL", value);
		}
		
		/// <summary>
		/// Insert values into dbo.ItemPage. Returns an object of type ItemPage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type ItemPage.</param>
		/// <returns>Object of type ItemPage.</returns>
		public ItemPage ItemPageInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			ItemPage value)
		{
			return ItemPageInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.ItemID,
				value.PageID,
				value.SequenceOrder,
				value.CreationUserID,
				value.LastModifiedUserID);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from dbo.ItemPage by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="itemPageID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool ItemPageDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int itemPageID)
		{
			return ItemPageDeleteAuto( sqlConnection, sqlTransaction, "BHL", itemPageID );
		}
		
		/// <summary>
		/// Delete values from dbo.ItemPage by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="itemPageID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool ItemPageDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int itemPageID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("ItemPageDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("ItemPageID", SqlDbType.Int, null, false, itemPageID), 
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
		/// Update values in dbo.ItemPage. Returns an object of type ItemPage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="itemPageID"></param>
		/// <param name="itemID"></param>
		/// <param name="pageID"></param>
		/// <param name="sequenceOrder"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type ItemPage.</returns>
		public ItemPage ItemPageUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int itemPageID,
			int itemID,
			int pageID,
			int sequenceOrder,
			int? lastModifiedUserID)
		{
			return ItemPageUpdateAuto( sqlConnection, sqlTransaction, "BHL", itemPageID, itemID, pageID, sequenceOrder, lastModifiedUserID);
		}
		
		/// <summary>
		/// Update values in dbo.ItemPage. Returns an object of type ItemPage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="itemPageID"></param>
		/// <param name="itemID"></param>
		/// <param name="pageID"></param>
		/// <param name="sequenceOrder"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type ItemPage.</returns>
		public ItemPage ItemPageUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int itemPageID,
			int itemID,
			int pageID,
			int sequenceOrder,
			int? lastModifiedUserID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("ItemPageUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("ItemPageID", SqlDbType.Int, null, false, itemPageID),
					CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemID),
					CustomSqlHelper.CreateInputParameter("PageID", SqlDbType.Int, null, false, pageID),
					CustomSqlHelper.CreateInputParameter("SequenceOrder", SqlDbType.Int, null, false, sequenceOrder),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, true, lastModifiedUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<ItemPage> helper = new CustomSqlHelper<ItemPage>())
				{
					CustomGenericList<ItemPage> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						ItemPage o = list[0];
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
		/// Update values in dbo.ItemPage. Returns an object of type ItemPage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type ItemPage.</param>
		/// <returns>Object of type ItemPage.</returns>
		public ItemPage ItemPageUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			ItemPage value)
		{
			return ItemPageUpdateAuto(sqlConnection, sqlTransaction, "BHL", value );
		}
		
		/// <summary>
		/// Update values in dbo.ItemPage. Returns an object of type ItemPage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type ItemPage.</param>
		/// <returns>Object of type ItemPage.</returns>
		public ItemPage ItemPageUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			ItemPage value)
		{
			return ItemPageUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.ItemPageID,
				value.ItemID,
				value.PageID,
				value.SequenceOrder,
				value.LastModifiedUserID);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage dbo.ItemPage object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.ItemPage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type ItemPage.</param>
		/// <returns>Object of type CustomDataAccessStatus<ItemPage>.</returns>
		public CustomDataAccessStatus<ItemPage> ItemPageManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			ItemPage value , int userId )
		{
			return ItemPageManageAuto( sqlConnection, sqlTransaction, "BHL", value , userId );
		}
		
		/// <summary>
		/// Manage dbo.ItemPage object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.ItemPage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type ItemPage.</param>
		/// <returns>Object of type CustomDataAccessStatus<ItemPage>.</returns>
		public CustomDataAccessStatus<ItemPage> ItemPageManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			ItemPage value , int userId )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				value.CreationUserID = userId;
				value.LastModifiedUserID = userId;
				ItemPage returnValue = ItemPageInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.ItemID,
						value.PageID,
						value.SequenceOrder,
						value.CreationUserID,
						value.LastModifiedUserID);
				
				return new CustomDataAccessStatus<ItemPage>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (ItemPageDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.ItemPageID))
				{
				return new CustomDataAccessStatus<ItemPage>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<ItemPage>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				value.LastModifiedUserID = userId;
				ItemPage returnValue = ItemPageUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.ItemPageID,
						value.ItemID,
						value.PageID,
						value.SequenceOrder,
						value.LastModifiedUserID);
					
				return new CustomDataAccessStatus<ItemPage>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<ItemPage>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}

