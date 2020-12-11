
// Generated 10/16/2020 2:39:29 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class ItemTitleDAL is based upon dbo.ItemTitle.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DAL
// {
// 		public partial class ItemTitleDAL
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
	partial class ItemTitleDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from dbo.ItemTitle by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="itemTitleID"></param>
		/// <returns>Object of type ItemTitle.</returns>
		public ItemTitle ItemTitleSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int itemTitleID)
		{
			return ItemTitleSelectAuto(	sqlConnection, sqlTransaction, "BHL",	itemTitleID );
		}
			
		/// <summary>
		/// Select values from dbo.ItemTitle by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="itemTitleID"></param>
		/// <returns>Object of type ItemTitle.</returns>
		public ItemTitle ItemTitleSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int itemTitleID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("ItemTitleSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("ItemTitleID", SqlDbType.Int, null, false, itemTitleID)))
			{
				using (CustomSqlHelper<ItemTitle> helper = new CustomSqlHelper<ItemTitle>())
				{
					CustomGenericList<ItemTitle> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						ItemTitle o = list[0];
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
		/// Select values from dbo.ItemTitle by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="itemTitleID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> ItemTitleSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int itemTitleID)
		{
			return ItemTitleSelectAutoRaw( sqlConnection, sqlTransaction, "BHL", itemTitleID );
		}
		
		/// <summary>
		/// Select values from dbo.ItemTitle by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="itemTitleID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> ItemTitleSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int itemTitleID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("ItemTitleSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("ItemTitleID", SqlDbType.Int, null, false, itemTitleID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====

 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into dbo.ItemTitle.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="itemID"></param>
		/// <param name="titleID"></param>
		/// <param name="itemSequence"></param>
		/// <param name="isPrimary"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type ItemTitle.</returns>
		public ItemTitle ItemTitleInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int itemID,
			int titleID,
			short? itemSequence,
			short isPrimary,
			int? creationUserID,
			int? lastModifiedUserID)
		{
			return ItemTitleInsertAuto( sqlConnection, sqlTransaction, "BHL", itemID, titleID, itemSequence, isPrimary, creationUserID, lastModifiedUserID );
		}
		
		/// <summary>
		/// Insert values into dbo.ItemTitle.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="itemID"></param>
		/// <param name="titleID"></param>
		/// <param name="itemSequence"></param>
		/// <param name="isPrimary"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type ItemTitle.</returns>
		public ItemTitle ItemTitleInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int itemID,
			int titleID,
			short? itemSequence,
			short isPrimary,
			int? creationUserID,
			int? lastModifiedUserID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("ItemTitleInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("ItemTitleID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemID),
					CustomSqlHelper.CreateInputParameter("TitleID", SqlDbType.Int, null, false, titleID),
					CustomSqlHelper.CreateInputParameter("ItemSequence", SqlDbType.SmallInt, null, true, itemSequence),
					CustomSqlHelper.CreateInputParameter("IsPrimary", SqlDbType.SmallInt, null, false, isPrimary),
					CustomSqlHelper.CreateInputParameter("CreationUserID", SqlDbType.Int, null, true, creationUserID),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, true, lastModifiedUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<ItemTitle> helper = new CustomSqlHelper<ItemTitle>())
				{
					CustomGenericList<ItemTitle> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						ItemTitle o = list[0];
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
		/// Insert values into dbo.ItemTitle. Returns an object of type ItemTitle.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type ItemTitle.</param>
		/// <returns>Object of type ItemTitle.</returns>
		public ItemTitle ItemTitleInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			ItemTitle value)
		{
			return ItemTitleInsertAuto(sqlConnection, sqlTransaction, "BHL", value);
		}
		
		/// <summary>
		/// Insert values into dbo.ItemTitle. Returns an object of type ItemTitle.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type ItemTitle.</param>
		/// <returns>Object of type ItemTitle.</returns>
		public ItemTitle ItemTitleInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			ItemTitle value)
		{
			return ItemTitleInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.ItemID,
				value.TitleID,
				value.ItemSequence,
				value.IsPrimary,
				value.CreationUserID,
				value.LastModifiedUserID);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from dbo.ItemTitle by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="itemTitleID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool ItemTitleDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int itemTitleID)
		{
			return ItemTitleDeleteAuto( sqlConnection, sqlTransaction, "BHL", itemTitleID );
		}
		
		/// <summary>
		/// Delete values from dbo.ItemTitle by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="itemTitleID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool ItemTitleDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int itemTitleID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("ItemTitleDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("ItemTitleID", SqlDbType.Int, null, false, itemTitleID), 
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
		/// Update values in dbo.ItemTitle. Returns an object of type ItemTitle.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="itemTitleID"></param>
		/// <param name="itemID"></param>
		/// <param name="titleID"></param>
		/// <param name="itemSequence"></param>
		/// <param name="isPrimary"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type ItemTitle.</returns>
		public ItemTitle ItemTitleUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int itemTitleID,
			int itemID,
			int titleID,
			short? itemSequence,
			short isPrimary,
			int? lastModifiedUserID)
		{
			return ItemTitleUpdateAuto( sqlConnection, sqlTransaction, "BHL", itemTitleID, itemID, titleID, itemSequence, isPrimary, lastModifiedUserID);
		}
		
		/// <summary>
		/// Update values in dbo.ItemTitle. Returns an object of type ItemTitle.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="itemTitleID"></param>
		/// <param name="itemID"></param>
		/// <param name="titleID"></param>
		/// <param name="itemSequence"></param>
		/// <param name="isPrimary"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type ItemTitle.</returns>
		public ItemTitle ItemTitleUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int itemTitleID,
			int itemID,
			int titleID,
			short? itemSequence,
			short isPrimary,
			int? lastModifiedUserID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("ItemTitleUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("ItemTitleID", SqlDbType.Int, null, false, itemTitleID),
					CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemID),
					CustomSqlHelper.CreateInputParameter("TitleID", SqlDbType.Int, null, false, titleID),
					CustomSqlHelper.CreateInputParameter("ItemSequence", SqlDbType.SmallInt, null, true, itemSequence),
					CustomSqlHelper.CreateInputParameter("IsPrimary", SqlDbType.SmallInt, null, false, isPrimary),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, true, lastModifiedUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<ItemTitle> helper = new CustomSqlHelper<ItemTitle>())
				{
					CustomGenericList<ItemTitle> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						ItemTitle o = list[0];
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
		/// Update values in dbo.ItemTitle. Returns an object of type ItemTitle.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type ItemTitle.</param>
		/// <returns>Object of type ItemTitle.</returns>
		public ItemTitle ItemTitleUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			ItemTitle value)
		{
			return ItemTitleUpdateAuto(sqlConnection, sqlTransaction, "BHL", value );
		}
		
		/// <summary>
		/// Update values in dbo.ItemTitle. Returns an object of type ItemTitle.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type ItemTitle.</param>
		/// <returns>Object of type ItemTitle.</returns>
		public ItemTitle ItemTitleUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			ItemTitle value)
		{
			return ItemTitleUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.ItemTitleID,
				value.ItemID,
				value.TitleID,
				value.ItemSequence,
				value.IsPrimary,
				value.LastModifiedUserID);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage dbo.ItemTitle object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.ItemTitle.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type ItemTitle.</param>
		/// <returns>Object of type CustomDataAccessStatus<ItemTitle>.</returns>
		public CustomDataAccessStatus<ItemTitle> ItemTitleManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			ItemTitle value , int userId )
		{
			return ItemTitleManageAuto( sqlConnection, sqlTransaction, "BHL", value , userId );
		}
		
		/// <summary>
		/// Manage dbo.ItemTitle object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.ItemTitle.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type ItemTitle.</param>
		/// <returns>Object of type CustomDataAccessStatus<ItemTitle>.</returns>
		public CustomDataAccessStatus<ItemTitle> ItemTitleManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			ItemTitle value , int userId )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				value.CreationUserID = userId;
				value.LastModifiedUserID = userId;
				ItemTitle returnValue = ItemTitleInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.ItemID,
						value.TitleID,
						value.ItemSequence,
						value.IsPrimary,
						value.CreationUserID,
						value.LastModifiedUserID);
				
				return new CustomDataAccessStatus<ItemTitle>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (ItemTitleDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.ItemTitleID))
				{
				return new CustomDataAccessStatus<ItemTitle>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<ItemTitle>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				value.LastModifiedUserID = userId;
				ItemTitle returnValue = ItemTitleUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.ItemTitleID,
						value.ItemID,
						value.TitleID,
						value.ItemSequence,
						value.IsPrimary,
						value.LastModifiedUserID);
					
				return new CustomDataAccessStatus<ItemTitle>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<ItemTitle>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}

