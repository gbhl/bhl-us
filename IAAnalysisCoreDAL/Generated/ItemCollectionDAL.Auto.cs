
// Generated 11/12/2008 3:12:29 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class ItemCollectionDAL is based upon ItemCollection.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.IAAnalysis.DAL
// {
// 		public partial class ItemCollectionDAL
//		{
//		}
// }

#endregion How To Implement

#region using

using System;
using System.Data;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.IAAnalysis.DataObjects;

#endregion using

namespace MOBOT.IAAnalysis.DAL
{
	partial class ItemCollectionDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from ItemCollection by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="itemID"></param>
		/// <param name="collectionID"></param>
		/// <returns>Object of type ItemCollection.</returns>
		public ItemCollection ItemCollectionSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int itemID,
			int collectionID)
		{
			return ItemCollectionSelectAuto(	sqlConnection, sqlTransaction, "IAAnalysis",	itemID, collectionID );
		}
			
		/// <summary>
		/// Select values from ItemCollection by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="itemID"></param>
		/// <param name="collectionID"></param>
		/// <returns>Object of type ItemCollection.</returns>
		public ItemCollection ItemCollectionSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int itemID,
			int collectionID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("ItemCollectionSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemID),
					CustomSqlHelper.CreateInputParameter("CollectionID", SqlDbType.Int, null, false, collectionID)))
			{
				using (CustomSqlHelper<ItemCollection> helper = new CustomSqlHelper<ItemCollection>())
				{
					CustomGenericList<ItemCollection> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						ItemCollection o = list[0];
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
		/// Select values from ItemCollection by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="itemID"></param>
		/// <param name="collectionID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> ItemCollectionSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int itemID,
			int collectionID)
		{
			return ItemCollectionSelectAutoRaw( sqlConnection, sqlTransaction, "IAAnalysis", itemID, collectionID );
		}
		
		/// <summary>
		/// Select values from ItemCollection by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="itemID"></param>
		/// <param name="collectionID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> ItemCollectionSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int itemID,
			int collectionID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("ItemCollectionSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemID),
					CustomSqlHelper.CreateInputParameter("CollectionID", SqlDbType.Int, null, false, collectionID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====
	
 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into ItemCollection.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="itemID"></param>
		/// <param name="collectionID"></param>
		/// <returns>Object of type ItemCollection.</returns>
		public ItemCollection ItemCollectionInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int itemID,
			int collectionID)
		{
			return ItemCollectionInsertAuto( sqlConnection, sqlTransaction, "IAAnalysis", itemID, collectionID );
		}
		
		/// <summary>
		/// Insert values into ItemCollection.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="itemID"></param>
		/// <param name="collectionID"></param>
		/// <returns>Object of type ItemCollection.</returns>
		public ItemCollection ItemCollectionInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int itemID,
			int collectionID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("ItemCollectionInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemID),
					CustomSqlHelper.CreateInputParameter("CollectionID", SqlDbType.Int, null, false, collectionID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<ItemCollection> helper = new CustomSqlHelper<ItemCollection>())
				{
					CustomGenericList<ItemCollection> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						ItemCollection o = list[0];
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
		/// Insert values into ItemCollection. Returns an object of type ItemCollection.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type ItemCollection.</param>
		/// <returns>Object of type ItemCollection.</returns>
		public ItemCollection ItemCollectionInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			ItemCollection value)
		{
			return ItemCollectionInsertAuto(sqlConnection, sqlTransaction, "IAAnalysis", value);
		}
		
		/// <summary>
		/// Insert values into ItemCollection. Returns an object of type ItemCollection.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type ItemCollection.</param>
		/// <returns>Object of type ItemCollection.</returns>
		public ItemCollection ItemCollectionInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			ItemCollection value)
		{
			return ItemCollectionInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.ItemID,
				value.CollectionID);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from ItemCollection by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="itemID"></param>
		/// <param name="collectionID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool ItemCollectionDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int itemID,
			int collectionID)
		{
			return ItemCollectionDeleteAuto( sqlConnection, sqlTransaction, "IAAnalysis", itemID, collectionID );
		}
		
		/// <summary>
		/// Delete values from ItemCollection by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="itemID"></param>
		/// <param name="collectionID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool ItemCollectionDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int itemID,
			int collectionID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("ItemCollectionDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemID),
					CustomSqlHelper.CreateInputParameter("CollectionID", SqlDbType.Int, null, false, collectionID), 
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
		/// Update values in ItemCollection. Returns an object of type ItemCollection.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="itemID"></param>
		/// <param name="collectionID"></param>
		/// <returns>Object of type ItemCollection.</returns>
		public ItemCollection ItemCollectionUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int itemID,
			int collectionID)
		{
			return ItemCollectionUpdateAuto( sqlConnection, sqlTransaction, "IAAnalysis", itemID, collectionID);
		}
		
		/// <summary>
		/// Update values in ItemCollection. Returns an object of type ItemCollection.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="itemID"></param>
		/// <param name="collectionID"></param>
		/// <returns>Object of type ItemCollection.</returns>
		public ItemCollection ItemCollectionUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int itemID,
			int collectionID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("ItemCollectionUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemID),
					CustomSqlHelper.CreateInputParameter("CollectionID", SqlDbType.Int, null, false, collectionID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<ItemCollection> helper = new CustomSqlHelper<ItemCollection>())
				{
					CustomGenericList<ItemCollection> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						ItemCollection o = list[0];
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
		/// Update values in ItemCollection. Returns an object of type ItemCollection.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type ItemCollection.</param>
		/// <returns>Object of type ItemCollection.</returns>
		public ItemCollection ItemCollectionUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			ItemCollection value)
		{
			return ItemCollectionUpdateAuto(sqlConnection, sqlTransaction, "IAAnalysis", value );
		}
		
		/// <summary>
		/// Update values in ItemCollection. Returns an object of type ItemCollection.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type ItemCollection.</param>
		/// <returns>Object of type ItemCollection.</returns>
		public ItemCollection ItemCollectionUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			ItemCollection value)
		{
			return ItemCollectionUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.ItemID,
				value.CollectionID);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage ItemCollection object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in ItemCollection.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type ItemCollection.</param>
		/// <returns>Object of type CustomDataAccessStatus<ItemCollection>.</returns>
		public CustomDataAccessStatus<ItemCollection> ItemCollectionManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			ItemCollection value  )
		{
			return ItemCollectionManageAuto( sqlConnection, sqlTransaction, "IAAnalysis", value  );
		}
		
		/// <summary>
		/// Manage ItemCollection object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in ItemCollection.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type ItemCollection.</param>
		/// <returns>Object of type CustomDataAccessStatus<ItemCollection>.</returns>
		public CustomDataAccessStatus<ItemCollection> ItemCollectionManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			ItemCollection value  )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				
				
				ItemCollection returnValue = ItemCollectionInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.ItemID,
						value.CollectionID);
				
				return new CustomDataAccessStatus<ItemCollection>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (ItemCollectionDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.ItemID,
						value.CollectionID))
				{
				return new CustomDataAccessStatus<ItemCollection>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<ItemCollection>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				
				ItemCollection returnValue = ItemCollectionUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.ItemID,
						value.CollectionID);
					
				return new CustomDataAccessStatus<ItemCollection>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<ItemCollection>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}
// end of source generation
