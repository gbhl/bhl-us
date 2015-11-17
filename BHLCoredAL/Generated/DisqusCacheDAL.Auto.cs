
// Generated 11/4/2015 10:49:28 AM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class DisqusCacheDAL is based upon DisqusCache.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DAL
// {
// 		public partial class DisqusCacheDAL
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
	partial class DisqusCacheDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from DisqusCache by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="disqusCacheID"></param>
		/// <returns>Object of type DisqusCache.</returns>
		public DisqusCache DisqusCacheSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int disqusCacheID)
		{
			return DisqusCacheSelectAuto(	sqlConnection, sqlTransaction, "BHL",	disqusCacheID );
		}
			
		/// <summary>
		/// Select values from DisqusCache by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="disqusCacheID"></param>
		/// <returns>Object of type DisqusCache.</returns>
		public DisqusCache DisqusCacheSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int disqusCacheID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("DisqusCacheSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("DisqusCacheID", SqlDbType.Int, null, false, disqusCacheID)))
			{
				using (CustomSqlHelper<DisqusCache> helper = new CustomSqlHelper<DisqusCache>())
				{
					CustomGenericList<DisqusCache> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						DisqusCache o = list[0];
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
		/// Select values from DisqusCache by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="disqusCacheID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> DisqusCacheSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int disqusCacheID)
		{
			return DisqusCacheSelectAutoRaw( sqlConnection, sqlTransaction, "BHL", disqusCacheID );
		}
		
		/// <summary>
		/// Select values from DisqusCache by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="disqusCacheID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> DisqusCacheSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int disqusCacheID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("DisqusCacheSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("DisqusCacheID", SqlDbType.Int, null, false, disqusCacheID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====
	
 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into DisqusCache.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="itemID"></param>
		/// <param name="pageID"></param>
		/// <returns>Object of type DisqusCache.</returns>
		public DisqusCache DisqusCacheInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int itemID,
			int pageID,
			int count)
		{
			return DisqusCacheInsertAuto( sqlConnection, sqlTransaction, "BHL", itemID, pageID, count );
		}
		
		/// <summary>
		/// Insert values into DisqusCache.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="itemID"></param>
		/// <param name="pageID"></param>
		/// <returns>Object of type DisqusCache.</returns>
		public DisqusCache DisqusCacheInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int itemID,
			int pageID,
			int count)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("DisqusCacheInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("DisqusCacheID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemID),
					CustomSqlHelper.CreateInputParameter("PageID", SqlDbType.Int, null, false, pageID),
					CustomSqlHelper.CreateInputParameter("Count", SqlDbType.Int, null, false, count), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<DisqusCache> helper = new CustomSqlHelper<DisqusCache>())
				{
					CustomGenericList<DisqusCache> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						DisqusCache o = list[0];
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
		/// Insert values into DisqusCache. Returns an object of type DisqusCache.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type DisqusCache.</param>
		/// <returns>Object of type DisqusCache.</returns>
		public DisqusCache DisqusCacheInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			DisqusCache value)
		{
			return DisqusCacheInsertAuto(sqlConnection, sqlTransaction, "BHL", value);
		}
		
		/// <summary>
		/// Insert values into DisqusCache. Returns an object of type DisqusCache.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type DisqusCache.</param>
		/// <returns>Object of type DisqusCache.</returns>
		public DisqusCache DisqusCacheInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			DisqusCache value)
		{
			return DisqusCacheInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.ItemID,
				value.PageID,
				value.Count);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from DisqusCache by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="disqusCacheID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool DisqusCacheDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int disqusCacheID)
		{
			return DisqusCacheDeleteAuto( sqlConnection, sqlTransaction, "BHL", disqusCacheID );
		}
		
		/// <summary>
		/// Delete values from DisqusCache by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="disqusCacheID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool DisqusCacheDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int disqusCacheID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("DisqusCacheDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("DisqusCacheID", SqlDbType.Int, null, false, disqusCacheID), 
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
		/// Update values in DisqusCache. Returns an object of type DisqusCache.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="disqusCacheID"></param>
		/// <param name="itemID"></param>
		/// <param name="pageID"></param>
		/// <returns>Object of type DisqusCache.</returns>
		public DisqusCache DisqusCacheUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int disqusCacheID,
			int itemID,
			int pageID,
			int count)
		{
			return DisqusCacheUpdateAuto( sqlConnection, sqlTransaction, "BHL", disqusCacheID, itemID, pageID, count);
		}
		
		/// <summary>
		/// Update values in DisqusCache. Returns an object of type DisqusCache.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="disqusCacheID"></param>
		/// <param name="itemID"></param>
		/// <param name="pageID"></param>
		/// <returns>Object of type DisqusCache.</returns>
		public DisqusCache DisqusCacheUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int disqusCacheID,
			int itemID,
			int pageID,
			int count)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("DisqusCacheUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("DisqusCacheID", SqlDbType.Int, null, false, disqusCacheID),
					CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemID),
					CustomSqlHelper.CreateInputParameter("PageID", SqlDbType.Int, null, false, pageID),
					CustomSqlHelper.CreateInputParameter("Count", SqlDbType.Int, null, false, count), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<DisqusCache> helper = new CustomSqlHelper<DisqusCache>())
				{
					CustomGenericList<DisqusCache> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						DisqusCache o = list[0];
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
		/// Update values in DisqusCache. Returns an object of type DisqusCache.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type DisqusCache.</param>
		/// <returns>Object of type DisqusCache.</returns>
		public DisqusCache DisqusCacheUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			DisqusCache value)
		{
			return DisqusCacheUpdateAuto(sqlConnection, sqlTransaction, "BHL", value );
		}
		
		/// <summary>
		/// Update values in DisqusCache. Returns an object of type DisqusCache.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type DisqusCache.</param>
		/// <returns>Object of type DisqusCache.</returns>
		public DisqusCache DisqusCacheUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			DisqusCache value)
		{
			return DisqusCacheUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.DisqusCacheID,
				value.ItemID,
				value.PageID,
				value.Count);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage DisqusCache object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in DisqusCache.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type DisqusCache.</param>
		/// <returns>Object of type CustomDataAccessStatus<DisqusCache>.</returns>
		public CustomDataAccessStatus<DisqusCache> DisqusCacheManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			DisqusCache value  )
		{
			return DisqusCacheManageAuto( sqlConnection, sqlTransaction, "BHL", value  );
		}
		
		/// <summary>
		/// Manage DisqusCache object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in DisqusCache.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type DisqusCache.</param>
		/// <returns>Object of type CustomDataAccessStatus<DisqusCache>.</returns>
		public CustomDataAccessStatus<DisqusCache> DisqusCacheManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			DisqusCache value  )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				
				
				DisqusCache returnValue = DisqusCacheInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.ItemID,
						value.PageID,
						value.Count);
				
				return new CustomDataAccessStatus<DisqusCache>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (DisqusCacheDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.DisqusCacheID))
				{
				return new CustomDataAccessStatus<DisqusCache>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<DisqusCache>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				
				DisqusCache returnValue = DisqusCacheUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.DisqusCacheID,
						value.ItemID,
						value.PageID,
						value.Count);
					
				return new CustomDataAccessStatus<DisqusCache>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<DisqusCache>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}
// end of source generation
