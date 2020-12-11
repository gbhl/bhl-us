
// Generated 10/19/2020 1:12:27 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class ItemKeywordDAL is based upon dbo.ItemKeyword.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DAL
// {
// 		public partial class ItemKeywordDAL
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
	partial class ItemKeywordDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from dbo.ItemKeyword by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="itemKeywordID"></param>
		/// <returns>Object of type ItemKeyword.</returns>
		public ItemKeyword ItemKeywordSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int itemKeywordID)
		{
			return ItemKeywordSelectAuto(	sqlConnection, sqlTransaction, "BHL",	itemKeywordID );
		}
			
		/// <summary>
		/// Select values from dbo.ItemKeyword by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="itemKeywordID"></param>
		/// <returns>Object of type ItemKeyword.</returns>
		public ItemKeyword ItemKeywordSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int itemKeywordID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("ItemKeywordSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("ItemKeywordID", SqlDbType.Int, null, false, itemKeywordID)))
			{
				using (CustomSqlHelper<ItemKeyword> helper = new CustomSqlHelper<ItemKeyword>())
				{
					CustomGenericList<ItemKeyword> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						ItemKeyword o = list[0];
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
		/// Select values from dbo.ItemKeyword by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="itemKeywordID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> ItemKeywordSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int itemKeywordID)
		{
			return ItemKeywordSelectAutoRaw( sqlConnection, sqlTransaction, "BHL", itemKeywordID );
		}
		
		/// <summary>
		/// Select values from dbo.ItemKeyword by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="itemKeywordID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> ItemKeywordSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int itemKeywordID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("ItemKeywordSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("ItemKeywordID", SqlDbType.Int, null, false, itemKeywordID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====

 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into dbo.ItemKeyword.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="itemID"></param>
		/// <param name="keywordID"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type ItemKeyword.</returns>
		public ItemKeyword ItemKeywordInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int itemID,
			int keywordID,
			int? creationUserID,
			int? lastModifiedUserID)
		{
			return ItemKeywordInsertAuto( sqlConnection, sqlTransaction, "BHL", itemID, keywordID, creationUserID, lastModifiedUserID );
		}
		
		/// <summary>
		/// Insert values into dbo.ItemKeyword.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="itemID"></param>
		/// <param name="keywordID"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type ItemKeyword.</returns>
		public ItemKeyword ItemKeywordInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int itemID,
			int keywordID,
			int? creationUserID,
			int? lastModifiedUserID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("ItemKeywordInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("ItemKeywordID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemID),
					CustomSqlHelper.CreateInputParameter("KeywordID", SqlDbType.Int, null, false, keywordID),
					CustomSqlHelper.CreateInputParameter("CreationUserID", SqlDbType.Int, null, true, creationUserID),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, true, lastModifiedUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<ItemKeyword> helper = new CustomSqlHelper<ItemKeyword>())
				{
					CustomGenericList<ItemKeyword> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						ItemKeyword o = list[0];
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
		/// Insert values into dbo.ItemKeyword. Returns an object of type ItemKeyword.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type ItemKeyword.</param>
		/// <returns>Object of type ItemKeyword.</returns>
		public ItemKeyword ItemKeywordInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			ItemKeyword value)
		{
			return ItemKeywordInsertAuto(sqlConnection, sqlTransaction, "BHL", value);
		}
		
		/// <summary>
		/// Insert values into dbo.ItemKeyword. Returns an object of type ItemKeyword.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type ItemKeyword.</param>
		/// <returns>Object of type ItemKeyword.</returns>
		public ItemKeyword ItemKeywordInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			ItemKeyword value)
		{
			return ItemKeywordInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.ItemID,
				value.KeywordID,
				value.CreationUserID,
				value.LastModifiedUserID);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from dbo.ItemKeyword by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="itemKeywordID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool ItemKeywordDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int itemKeywordID)
		{
			return ItemKeywordDeleteAuto( sqlConnection, sqlTransaction, "BHL", itemKeywordID );
		}
		
		/// <summary>
		/// Delete values from dbo.ItemKeyword by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="itemKeywordID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool ItemKeywordDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int itemKeywordID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("ItemKeywordDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("ItemKeywordID", SqlDbType.Int, null, false, itemKeywordID), 
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
		/// Update values in dbo.ItemKeyword. Returns an object of type ItemKeyword.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="itemKeywordID"></param>
		/// <param name="itemID"></param>
		/// <param name="keywordID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type ItemKeyword.</returns>
		public ItemKeyword ItemKeywordUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int itemKeywordID,
			int itemID,
			int keywordID,
			int? lastModifiedUserID)
		{
			return ItemKeywordUpdateAuto( sqlConnection, sqlTransaction, "BHL", itemKeywordID, itemID, keywordID, lastModifiedUserID);
		}
		
		/// <summary>
		/// Update values in dbo.ItemKeyword. Returns an object of type ItemKeyword.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="itemKeywordID"></param>
		/// <param name="itemID"></param>
		/// <param name="keywordID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type ItemKeyword.</returns>
		public ItemKeyword ItemKeywordUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int itemKeywordID,
			int itemID,
			int keywordID,
			int? lastModifiedUserID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("ItemKeywordUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("ItemKeywordID", SqlDbType.Int, null, false, itemKeywordID),
					CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemID),
					CustomSqlHelper.CreateInputParameter("KeywordID", SqlDbType.Int, null, false, keywordID),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, true, lastModifiedUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<ItemKeyword> helper = new CustomSqlHelper<ItemKeyword>())
				{
					CustomGenericList<ItemKeyword> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						ItemKeyword o = list[0];
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
		/// Update values in dbo.ItemKeyword. Returns an object of type ItemKeyword.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type ItemKeyword.</param>
		/// <returns>Object of type ItemKeyword.</returns>
		public ItemKeyword ItemKeywordUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			ItemKeyword value)
		{
			return ItemKeywordUpdateAuto(sqlConnection, sqlTransaction, "BHL", value );
		}
		
		/// <summary>
		/// Update values in dbo.ItemKeyword. Returns an object of type ItemKeyword.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type ItemKeyword.</param>
		/// <returns>Object of type ItemKeyword.</returns>
		public ItemKeyword ItemKeywordUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			ItemKeyword value)
		{
			return ItemKeywordUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.ItemKeywordID,
				value.ItemID,
				value.KeywordID,
				value.LastModifiedUserID);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage dbo.ItemKeyword object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.ItemKeyword.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type ItemKeyword.</param>
		/// <returns>Object of type CustomDataAccessStatus<ItemKeyword>.</returns>
		public CustomDataAccessStatus<ItemKeyword> ItemKeywordManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			ItemKeyword value , int userId )
		{
			return ItemKeywordManageAuto( sqlConnection, sqlTransaction, "BHL", value , userId );
		}
		
		/// <summary>
		/// Manage dbo.ItemKeyword object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.ItemKeyword.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type ItemKeyword.</param>
		/// <returns>Object of type CustomDataAccessStatus<ItemKeyword>.</returns>
		public CustomDataAccessStatus<ItemKeyword> ItemKeywordManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			ItemKeyword value , int userId )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				value.CreationUserID = userId;
				value.LastModifiedUserID = userId;
				ItemKeyword returnValue = ItemKeywordInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.ItemID,
						value.KeywordID,
						value.CreationUserID,
						value.LastModifiedUserID);
				
				return new CustomDataAccessStatus<ItemKeyword>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (ItemKeywordDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.ItemKeywordID))
				{
				return new CustomDataAccessStatus<ItemKeyword>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<ItemKeyword>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				value.LastModifiedUserID = userId;
				ItemKeyword returnValue = ItemKeywordUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.ItemKeywordID,
						value.ItemID,
						value.KeywordID,
						value.LastModifiedUserID);
					
				return new CustomDataAccessStatus<ItemKeyword>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<ItemKeyword>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}

