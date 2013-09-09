
// Generated 10/23/2012 3:54:50 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class BSItemDAL is based upon BSItem.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHLImport.DAL
// {
// 		public partial class BSItemDAL
//		{
//		}
// }

#endregion How To Implement

#region using

using System;
using System.Data;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHLImport.DataObjects;

#endregion using

namespace MOBOT.BHLImport.DAL
{
	partial class BSItemDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from BSItem by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="itemID"></param>
		/// <returns>Object of type BSItem.</returns>
		public BSItem BSItemSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int itemID)
		{
			return BSItemSelectAuto(	sqlConnection, sqlTransaction, "BHLImport",	itemID );
		}
			
		/// <summary>
		/// Select values from BSItem by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="itemID"></param>
		/// <returns>Object of type BSItem.</returns>
		public BSItem BSItemSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int itemID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("BSItemSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemID)))
			{
				using (CustomSqlHelper<BSItem> helper = new CustomSqlHelper<BSItem>())
				{
					CustomGenericList<BSItem> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						BSItem o = list[0];
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
		/// Select values from BSItem by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="itemID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> BSItemSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int itemID)
		{
			return BSItemSelectAutoRaw( sqlConnection, sqlTransaction, "BHLImport", itemID );
		}
		
		/// <summary>
		/// Select values from BSItem by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="itemID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> BSItemSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int itemID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("BSItemSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====
	
 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into BSItem.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="bHLItemID"></param>
		/// <param name="itemStatusID"></param>
		/// <returns>Object of type BSItem.</returns>
		public BSItem BSItemInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int? bHLItemID,
			int itemStatusID)
		{
			return BSItemInsertAuto( sqlConnection, sqlTransaction, "BHLImport", bHLItemID, itemStatusID );
		}
		
		/// <summary>
		/// Insert values into BSItem.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="bHLItemID"></param>
		/// <param name="itemStatusID"></param>
		/// <returns>Object of type BSItem.</returns>
		public BSItem BSItemInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int? bHLItemID,
			int itemStatusID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("BSItemInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("ItemID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("BHLItemID", SqlDbType.Int, null, true, bHLItemID),
					CustomSqlHelper.CreateInputParameter("ItemStatusID", SqlDbType.Int, null, false, itemStatusID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<BSItem> helper = new CustomSqlHelper<BSItem>())
				{
					CustomGenericList<BSItem> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						BSItem o = list[0];
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
		/// Insert values into BSItem. Returns an object of type BSItem.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type BSItem.</param>
		/// <returns>Object of type BSItem.</returns>
		public BSItem BSItemInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			BSItem value)
		{
			return BSItemInsertAuto(sqlConnection, sqlTransaction, "BHLImport", value);
		}
		
		/// <summary>
		/// Insert values into BSItem. Returns an object of type BSItem.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type BSItem.</param>
		/// <returns>Object of type BSItem.</returns>
		public BSItem BSItemInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			BSItem value)
		{
			return BSItemInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.BHLItemID,
				value.ItemStatusID);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from BSItem by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="itemID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool BSItemDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int itemID)
		{
			return BSItemDeleteAuto( sqlConnection, sqlTransaction, "BHLImport", itemID );
		}
		
		/// <summary>
		/// Delete values from BSItem by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="itemID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool BSItemDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int itemID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("BSItemDeleteAuto", connection, transaction, 
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
		/// Update values in BSItem. Returns an object of type BSItem.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="itemID"></param>
		/// <param name="bHLItemID"></param>
		/// <param name="itemStatusID"></param>
		/// <returns>Object of type BSItem.</returns>
		public BSItem BSItemUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int itemID,
			int? bHLItemID,
			int itemStatusID)
		{
			return BSItemUpdateAuto( sqlConnection, sqlTransaction, "BHLImport", itemID, bHLItemID, itemStatusID);
		}
		
		/// <summary>
		/// Update values in BSItem. Returns an object of type BSItem.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="itemID"></param>
		/// <param name="bHLItemID"></param>
		/// <param name="itemStatusID"></param>
		/// <returns>Object of type BSItem.</returns>
		public BSItem BSItemUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int itemID,
			int? bHLItemID,
			int itemStatusID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("BSItemUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemID),
					CustomSqlHelper.CreateInputParameter("BHLItemID", SqlDbType.Int, null, true, bHLItemID),
					CustomSqlHelper.CreateInputParameter("ItemStatusID", SqlDbType.Int, null, false, itemStatusID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<BSItem> helper = new CustomSqlHelper<BSItem>())
				{
					CustomGenericList<BSItem> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						BSItem o = list[0];
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
		/// Update values in BSItem. Returns an object of type BSItem.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type BSItem.</param>
		/// <returns>Object of type BSItem.</returns>
		public BSItem BSItemUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			BSItem value)
		{
			return BSItemUpdateAuto(sqlConnection, sqlTransaction, "BHLImport", value );
		}
		
		/// <summary>
		/// Update values in BSItem. Returns an object of type BSItem.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type BSItem.</param>
		/// <returns>Object of type BSItem.</returns>
		public BSItem BSItemUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			BSItem value)
		{
			return BSItemUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.ItemID,
				value.BHLItemID,
				value.ItemStatusID);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage BSItem object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in BSItem.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type BSItem.</param>
		/// <returns>Object of type CustomDataAccessStatus<BSItem>.</returns>
		public CustomDataAccessStatus<BSItem> BSItemManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			BSItem value  )
		{
			return BSItemManageAuto( sqlConnection, sqlTransaction, "BHLImport", value  );
		}
		
		/// <summary>
		/// Manage BSItem object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in BSItem.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type BSItem.</param>
		/// <returns>Object of type CustomDataAccessStatus<BSItem>.</returns>
		public CustomDataAccessStatus<BSItem> BSItemManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			BSItem value  )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				
				
				BSItem returnValue = BSItemInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.BHLItemID,
						value.ItemStatusID);
				
				return new CustomDataAccessStatus<BSItem>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (BSItemDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.ItemID))
				{
				return new CustomDataAccessStatus<BSItem>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<BSItem>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				
				BSItem returnValue = BSItemUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.ItemID,
						value.BHLItemID,
						value.ItemStatusID);
					
				return new CustomDataAccessStatus<BSItem>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<BSItem>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}
// end of source generation
