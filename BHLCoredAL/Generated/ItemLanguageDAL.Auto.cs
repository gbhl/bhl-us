
// Generated 2/4/2011 12:08:43 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class ItemLanguageDAL is based upon ItemLanguage.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DAL
// {
// 		public partial class ItemLanguageDAL
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
	partial class ItemLanguageDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from ItemLanguage by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="itemLanguageID"></param>
		/// <returns>Object of type ItemLanguage.</returns>
		public ItemLanguage ItemLanguageSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int itemLanguageID)
		{
			return ItemLanguageSelectAuto(	sqlConnection, sqlTransaction, "BHL",	itemLanguageID );
		}
			
		/// <summary>
		/// Select values from ItemLanguage by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="itemLanguageID"></param>
		/// <returns>Object of type ItemLanguage.</returns>
		public ItemLanguage ItemLanguageSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int itemLanguageID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("ItemLanguageSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("ItemLanguageID", SqlDbType.Int, null, false, itemLanguageID)))
			{
				using (CustomSqlHelper<ItemLanguage> helper = new CustomSqlHelper<ItemLanguage>())
				{
					CustomGenericList<ItemLanguage> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						ItemLanguage o = list[0];
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
		/// Select values from ItemLanguage by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="itemLanguageID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> ItemLanguageSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int itemLanguageID)
		{
			return ItemLanguageSelectAutoRaw( sqlConnection, sqlTransaction, "BHL", itemLanguageID );
		}
		
		/// <summary>
		/// Select values from ItemLanguage by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="itemLanguageID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> ItemLanguageSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int itemLanguageID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("ItemLanguageSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("ItemLanguageID", SqlDbType.Int, null, false, itemLanguageID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====
	
 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into ItemLanguage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="itemID"></param>
		/// <param name="languageCode"></param>
		/// <param name="creationUserID"></param>
		/// <returns>Object of type ItemLanguage.</returns>
		public ItemLanguage ItemLanguageInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int itemID,
			string languageCode,
			int? creationUserID)
		{
			return ItemLanguageInsertAuto( sqlConnection, sqlTransaction, "BHL", itemID, languageCode, creationUserID );
		}
		
		/// <summary>
		/// Insert values into ItemLanguage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="itemID"></param>
		/// <param name="languageCode"></param>
		/// <param name="creationUserID"></param>
		/// <returns>Object of type ItemLanguage.</returns>
		public ItemLanguage ItemLanguageInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int itemID,
			string languageCode,
			int? creationUserID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("ItemLanguageInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("ItemLanguageID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemID),
					CustomSqlHelper.CreateInputParameter("LanguageCode", SqlDbType.NVarChar, 10, false, languageCode),
					CustomSqlHelper.CreateInputParameter("CreationUserID", SqlDbType.Int, null, true, creationUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<ItemLanguage> helper = new CustomSqlHelper<ItemLanguage>())
				{
					CustomGenericList<ItemLanguage> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						ItemLanguage o = list[0];
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
		/// Insert values into ItemLanguage. Returns an object of type ItemLanguage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type ItemLanguage.</param>
		/// <returns>Object of type ItemLanguage.</returns>
		public ItemLanguage ItemLanguageInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			ItemLanguage value)
		{
			return ItemLanguageInsertAuto(sqlConnection, sqlTransaction, "BHL", value);
		}
		
		/// <summary>
		/// Insert values into ItemLanguage. Returns an object of type ItemLanguage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type ItemLanguage.</param>
		/// <returns>Object of type ItemLanguage.</returns>
		public ItemLanguage ItemLanguageInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			ItemLanguage value)
		{
			return ItemLanguageInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.ItemID,
				value.LanguageCode,
				value.CreationUserID);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from ItemLanguage by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="itemLanguageID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool ItemLanguageDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int itemLanguageID)
		{
			return ItemLanguageDeleteAuto( sqlConnection, sqlTransaction, "BHL", itemLanguageID );
		}
		
		/// <summary>
		/// Delete values from ItemLanguage by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="itemLanguageID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool ItemLanguageDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int itemLanguageID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("ItemLanguageDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("ItemLanguageID", SqlDbType.Int, null, false, itemLanguageID), 
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
		/// Update values in ItemLanguage. Returns an object of type ItemLanguage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="itemLanguageID"></param>
		/// <param name="itemID"></param>
		/// <param name="languageCode"></param>
		/// <returns>Object of type ItemLanguage.</returns>
		public ItemLanguage ItemLanguageUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int itemLanguageID,
			int itemID,
			string languageCode)
		{
			return ItemLanguageUpdateAuto( sqlConnection, sqlTransaction, "BHL", itemLanguageID, itemID, languageCode);
		}
		
		/// <summary>
		/// Update values in ItemLanguage. Returns an object of type ItemLanguage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="itemLanguageID"></param>
		/// <param name="itemID"></param>
		/// <param name="languageCode"></param>
		/// <returns>Object of type ItemLanguage.</returns>
		public ItemLanguage ItemLanguageUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int itemLanguageID,
			int itemID,
			string languageCode)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("ItemLanguageUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("ItemLanguageID", SqlDbType.Int, null, false, itemLanguageID),
					CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemID),
					CustomSqlHelper.CreateInputParameter("LanguageCode", SqlDbType.NVarChar, 10, false, languageCode), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<ItemLanguage> helper = new CustomSqlHelper<ItemLanguage>())
				{
					CustomGenericList<ItemLanguage> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						ItemLanguage o = list[0];
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
		/// Update values in ItemLanguage. Returns an object of type ItemLanguage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type ItemLanguage.</param>
		/// <returns>Object of type ItemLanguage.</returns>
		public ItemLanguage ItemLanguageUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			ItemLanguage value)
		{
			return ItemLanguageUpdateAuto(sqlConnection, sqlTransaction, "BHL", value );
		}
		
		/// <summary>
		/// Update values in ItemLanguage. Returns an object of type ItemLanguage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type ItemLanguage.</param>
		/// <returns>Object of type ItemLanguage.</returns>
		public ItemLanguage ItemLanguageUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			ItemLanguage value)
		{
			return ItemLanguageUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.ItemLanguageID,
				value.ItemID,
				value.LanguageCode);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage ItemLanguage object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in ItemLanguage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type ItemLanguage.</param>
		/// <returns>Object of type CustomDataAccessStatus<ItemLanguage>.</returns>
		public CustomDataAccessStatus<ItemLanguage> ItemLanguageManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			ItemLanguage value , int userId )
		{
			return ItemLanguageManageAuto( sqlConnection, sqlTransaction, "BHL", value , userId );
		}
		
		/// <summary>
		/// Manage ItemLanguage object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in ItemLanguage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type ItemLanguage.</param>
		/// <returns>Object of type CustomDataAccessStatus<ItemLanguage>.</returns>
		public CustomDataAccessStatus<ItemLanguage> ItemLanguageManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			ItemLanguage value , int userId )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				value.CreationUserID = userId;
				
				ItemLanguage returnValue = ItemLanguageInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.ItemID,
						value.LanguageCode,
						value.CreationUserID);
				
				return new CustomDataAccessStatus<ItemLanguage>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (ItemLanguageDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.ItemLanguageID))
				{
				return new CustomDataAccessStatus<ItemLanguage>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<ItemLanguage>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				
				ItemLanguage returnValue = ItemLanguageUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.ItemLanguageID,
						value.ItemID,
						value.LanguageCode);
					
				return new CustomDataAccessStatus<ItemLanguage>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<ItemLanguage>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}
// end of source generation
