
// Generated 11/19/2009 2:21:40 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class ItemNameFileLogDAL is based upon ItemNameFileLog.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DAL
// {
// 		public partial class ItemNameFileLogDAL
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
	partial class ItemNameFileLogDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from ItemNameFileLog by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="logID"></param>
		/// <returns>Object of type ItemNameFileLog.</returns>
		public ItemNameFileLog ItemNameFileLogSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int logID)
		{
			return ItemNameFileLogSelectAuto(	sqlConnection, sqlTransaction, "BHL",	logID );
		}
			
		/// <summary>
		/// Select values from ItemNameFileLog by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="logID"></param>
		/// <returns>Object of type ItemNameFileLog.</returns>
		public ItemNameFileLog ItemNameFileLogSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int logID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("ItemNameFileLogSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("LogID", SqlDbType.Int, null, false, logID)))
			{
				using (CustomSqlHelper<ItemNameFileLog> helper = new CustomSqlHelper<ItemNameFileLog>())
				{
					CustomGenericList<ItemNameFileLog> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						ItemNameFileLog o = list[0];
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
		/// Select values from ItemNameFileLog by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="logID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> ItemNameFileLogSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int logID)
		{
			return ItemNameFileLogSelectAutoRaw( sqlConnection, sqlTransaction, "BHL", logID );
		}
		
		/// <summary>
		/// Select values from ItemNameFileLog by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="logID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> ItemNameFileLogSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int logID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("ItemNameFileLogSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("LogID", SqlDbType.Int, null, false, logID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====
	
 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into ItemNameFileLog.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="itemID"></param>
		/// <param name="doCreate"></param>
		/// <param name="doUpload"></param>
		/// <param name="lastCreateDate"></param>
		/// <param name="lastUploadDate"></param>
		/// <returns>Object of type ItemNameFileLog.</returns>
		public ItemNameFileLog ItemNameFileLogInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int itemID,
			bool doCreate,
			bool doUpload,
			DateTime? lastCreateDate,
			DateTime? lastUploadDate)
		{
			return ItemNameFileLogInsertAuto( sqlConnection, sqlTransaction, "BHL", itemID, doCreate, doUpload, lastCreateDate, lastUploadDate );
		}
		
		/// <summary>
		/// Insert values into ItemNameFileLog.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="itemID"></param>
		/// <param name="doCreate"></param>
		/// <param name="doUpload"></param>
		/// <param name="lastCreateDate"></param>
		/// <param name="lastUploadDate"></param>
		/// <returns>Object of type ItemNameFileLog.</returns>
		public ItemNameFileLog ItemNameFileLogInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int itemID,
			bool doCreate,
			bool doUpload,
			DateTime? lastCreateDate,
			DateTime? lastUploadDate)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("ItemNameFileLogInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("LogID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemID),
					CustomSqlHelper.CreateInputParameter("DoCreate", SqlDbType.Bit, null, false, doCreate),
					CustomSqlHelper.CreateInputParameter("DoUpload", SqlDbType.Bit, null, false, doUpload),
					CustomSqlHelper.CreateInputParameter("LastCreateDate", SqlDbType.DateTime, null, true, lastCreateDate),
					CustomSqlHelper.CreateInputParameter("LastUploadDate", SqlDbType.DateTime, null, true, lastUploadDate), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<ItemNameFileLog> helper = new CustomSqlHelper<ItemNameFileLog>())
				{
					CustomGenericList<ItemNameFileLog> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						ItemNameFileLog o = list[0];
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
		/// Insert values into ItemNameFileLog. Returns an object of type ItemNameFileLog.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type ItemNameFileLog.</param>
		/// <returns>Object of type ItemNameFileLog.</returns>
		public ItemNameFileLog ItemNameFileLogInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			ItemNameFileLog value)
		{
			return ItemNameFileLogInsertAuto(sqlConnection, sqlTransaction, "BHL", value);
		}
		
		/// <summary>
		/// Insert values into ItemNameFileLog. Returns an object of type ItemNameFileLog.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type ItemNameFileLog.</param>
		/// <returns>Object of type ItemNameFileLog.</returns>
		public ItemNameFileLog ItemNameFileLogInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			ItemNameFileLog value)
		{
			return ItemNameFileLogInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.ItemID,
				value.DoCreate,
				value.DoUpload,
				value.LastCreateDate,
				value.LastUploadDate);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from ItemNameFileLog by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="logID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool ItemNameFileLogDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int logID)
		{
			return ItemNameFileLogDeleteAuto( sqlConnection, sqlTransaction, "BHL", logID );
		}
		
		/// <summary>
		/// Delete values from ItemNameFileLog by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="logID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool ItemNameFileLogDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int logID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("ItemNameFileLogDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("LogID", SqlDbType.Int, null, false, logID), 
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
		/// Update values in ItemNameFileLog. Returns an object of type ItemNameFileLog.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="logID"></param>
		/// <param name="itemID"></param>
		/// <param name="doCreate"></param>
		/// <param name="doUpload"></param>
		/// <param name="lastCreateDate"></param>
		/// <param name="lastUploadDate"></param>
		/// <returns>Object of type ItemNameFileLog.</returns>
		public ItemNameFileLog ItemNameFileLogUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int logID,
			int itemID,
			bool doCreate,
			bool doUpload,
			DateTime? lastCreateDate,
			DateTime? lastUploadDate)
		{
			return ItemNameFileLogUpdateAuto( sqlConnection, sqlTransaction, "BHL", logID, itemID, doCreate, doUpload, lastCreateDate, lastUploadDate);
		}
		
		/// <summary>
		/// Update values in ItemNameFileLog. Returns an object of type ItemNameFileLog.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="logID"></param>
		/// <param name="itemID"></param>
		/// <param name="doCreate"></param>
		/// <param name="doUpload"></param>
		/// <param name="lastCreateDate"></param>
		/// <param name="lastUploadDate"></param>
		/// <returns>Object of type ItemNameFileLog.</returns>
		public ItemNameFileLog ItemNameFileLogUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int logID,
			int itemID,
			bool doCreate,
			bool doUpload,
			DateTime? lastCreateDate,
			DateTime? lastUploadDate)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("ItemNameFileLogUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("LogID", SqlDbType.Int, null, false, logID),
					CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemID),
					CustomSqlHelper.CreateInputParameter("DoCreate", SqlDbType.Bit, null, false, doCreate),
					CustomSqlHelper.CreateInputParameter("DoUpload", SqlDbType.Bit, null, false, doUpload),
					CustomSqlHelper.CreateInputParameter("LastCreateDate", SqlDbType.DateTime, null, true, lastCreateDate),
					CustomSqlHelper.CreateInputParameter("LastUploadDate", SqlDbType.DateTime, null, true, lastUploadDate), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<ItemNameFileLog> helper = new CustomSqlHelper<ItemNameFileLog>())
				{
					CustomGenericList<ItemNameFileLog> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						ItemNameFileLog o = list[0];
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
		/// Update values in ItemNameFileLog. Returns an object of type ItemNameFileLog.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type ItemNameFileLog.</param>
		/// <returns>Object of type ItemNameFileLog.</returns>
		public ItemNameFileLog ItemNameFileLogUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			ItemNameFileLog value)
		{
			return ItemNameFileLogUpdateAuto(sqlConnection, sqlTransaction, "BHL", value );
		}
		
		/// <summary>
		/// Update values in ItemNameFileLog. Returns an object of type ItemNameFileLog.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type ItemNameFileLog.</param>
		/// <returns>Object of type ItemNameFileLog.</returns>
		public ItemNameFileLog ItemNameFileLogUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			ItemNameFileLog value)
		{
			return ItemNameFileLogUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.LogID,
				value.ItemID,
				value.DoCreate,
				value.DoUpload,
				value.LastCreateDate,
				value.LastUploadDate);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage ItemNameFileLog object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in ItemNameFileLog.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type ItemNameFileLog.</param>
		/// <returns>Object of type CustomDataAccessStatus<ItemNameFileLog>.</returns>
		public CustomDataAccessStatus<ItemNameFileLog> ItemNameFileLogManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			ItemNameFileLog value  )
		{
			return ItemNameFileLogManageAuto( sqlConnection, sqlTransaction, "BHL", value  );
		}
		
		/// <summary>
		/// Manage ItemNameFileLog object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in ItemNameFileLog.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type ItemNameFileLog.</param>
		/// <returns>Object of type CustomDataAccessStatus<ItemNameFileLog>.</returns>
		public CustomDataAccessStatus<ItemNameFileLog> ItemNameFileLogManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			ItemNameFileLog value  )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				
				
				ItemNameFileLog returnValue = ItemNameFileLogInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.ItemID,
						value.DoCreate,
						value.DoUpload,
						value.LastCreateDate,
						value.LastUploadDate);
				
				return new CustomDataAccessStatus<ItemNameFileLog>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (ItemNameFileLogDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.LogID))
				{
				return new CustomDataAccessStatus<ItemNameFileLog>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<ItemNameFileLog>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				
				ItemNameFileLog returnValue = ItemNameFileLogUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.LogID,
						value.ItemID,
						value.DoCreate,
						value.DoUpload,
						value.LastCreateDate,
						value.LastUploadDate);
					
				return new CustomDataAccessStatus<ItemNameFileLog>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<ItemNameFileLog>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}
// end of source generation
