
// Generated 1/18/2008 11:10:47 AM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class ItemStatusDAL is based upon ItemStatus.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DAL
// {
// 		public partial class ItemStatusDAL
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
	partial class ItemStatusDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from ItemStatus by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="itemStatusID"></param>
		/// <returns>Object of type ItemStatus.</returns>
		public ItemStatus ItemStatusSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int itemStatusID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("ItemStatusSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("ItemStatusID", SqlDbType.Int, null, false, itemStatusID)))
			{
				using (CustomSqlHelper<ItemStatus> helper = new CustomSqlHelper<ItemStatus>())
				{
					CustomGenericList<ItemStatus> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						ItemStatus o = list[0];
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
		/// Select values from ItemStatus by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="itemStatusID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> ItemStatusSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int itemStatusID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("ItemStatusSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("ItemStatusID", SqlDbType.Int, null, false, itemStatusID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====
	
 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into ItemStatus.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="itemStatusID"></param>
		/// <param name="itemStatusName"></param>
		/// <returns>Object of type ItemStatus.</returns>
		public ItemStatus ItemStatusInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int itemStatusID,
			string itemStatusName)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("ItemStatusInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("ItemStatusID", SqlDbType.Int, null, false, itemStatusID),
					CustomSqlHelper.CreateInputParameter("ItemStatusName", SqlDbType.NVarChar, 50, false, itemStatusName), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<ItemStatus> helper = new CustomSqlHelper<ItemStatus>())
				{
					CustomGenericList<ItemStatus> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						ItemStatus o = list[0];
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
		/// Insert values into ItemStatus. Returns an object of type ItemStatus.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type ItemStatus.</param>
		/// <returns>Object of type ItemStatus.</returns>
		public ItemStatus ItemStatusInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			ItemStatus value)
		{
			return ItemStatusInsertAuto(sqlConnection, sqlTransaction, 
				value.ItemStatusID,
				value.ItemStatusName);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from ItemStatus by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="itemStatusID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool ItemStatusDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int itemStatusID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("ItemStatusDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("ItemStatusID", SqlDbType.Int, null, false, itemStatusID), 
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
		/// Update values in ItemStatus. Returns an object of type ItemStatus.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="itemStatusID"></param>
		/// <param name="itemStatusName"></param>
		/// <returns>Object of type ItemStatus.</returns>
		public ItemStatus ItemStatusUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int itemStatusID,
			string itemStatusName)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("ItemStatusUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("ItemStatusID", SqlDbType.Int, null, false, itemStatusID),
					CustomSqlHelper.CreateInputParameter("ItemStatusName", SqlDbType.NVarChar, 50, false, itemStatusName), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<ItemStatus> helper = new CustomSqlHelper<ItemStatus>())
				{
					CustomGenericList<ItemStatus> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						ItemStatus o = list[0];
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
		/// Update values in ItemStatus. Returns an object of type ItemStatus.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type ItemStatus.</param>
		/// <returns>Object of type ItemStatus.</returns>
		public ItemStatus ItemStatusUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			ItemStatus value)
		{
			return ItemStatusUpdateAuto(sqlConnection, sqlTransaction,
				value.ItemStatusID,
				value.ItemStatusName);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage ItemStatus object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in ItemStatus.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type ItemStatus.</param>
		/// <returns>Object of type CustomDataAccessStatus<ItemStatus>.</returns>
		public CustomDataAccessStatus<ItemStatus> ItemStatusManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			ItemStatus value  )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				
				
				ItemStatus returnValue = ItemStatusInsertAuto(sqlConnection, sqlTransaction, 
					value.ItemStatusID,
						value.ItemStatusName);
				
				return new CustomDataAccessStatus<ItemStatus>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (ItemStatusDeleteAuto(sqlConnection, sqlTransaction, 
					value.ItemStatusID))
				{
				return new CustomDataAccessStatus<ItemStatus>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<ItemStatus>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				
				ItemStatus returnValue = ItemStatusUpdateAuto(sqlConnection, sqlTransaction, 
					value.ItemStatusID,
						value.ItemStatusName);
					
				return new CustomDataAccessStatus<ItemStatus>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<ItemStatus>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}
// end of source generation
