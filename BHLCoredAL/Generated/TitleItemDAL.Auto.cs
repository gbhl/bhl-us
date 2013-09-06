
// Generated 2/4/2011 3:25:21 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class TitleItemDAL is based upon TitleItem.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DAL
// {
// 		public partial class TitleItemDAL
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
	partial class TitleItemDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from TitleItem by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="titleItemID"></param>
		/// <returns>Object of type TitleItem.</returns>
		public TitleItem TitleItemSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int titleItemID)
		{
			return TitleItemSelectAuto(	sqlConnection, sqlTransaction, "BHL",	titleItemID );
		}
			
		/// <summary>
		/// Select values from TitleItem by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="titleItemID"></param>
		/// <returns>Object of type TitleItem.</returns>
		public TitleItem TitleItemSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int titleItemID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleItemSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("TitleItemID", SqlDbType.Int, null, false, titleItemID)))
			{
				using (CustomSqlHelper<TitleItem> helper = new CustomSqlHelper<TitleItem>())
				{
					CustomGenericList<TitleItem> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						TitleItem o = list[0];
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
		/// Select values from TitleItem by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="titleItemID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> TitleItemSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int titleItemID)
		{
			return TitleItemSelectAutoRaw( sqlConnection, sqlTransaction, "BHL", titleItemID );
		}
		
		/// <summary>
		/// Select values from TitleItem by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="titleItemID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> TitleItemSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int titleItemID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleItemSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("TitleItemID", SqlDbType.Int, null, false, titleItemID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====
	
 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into TitleItem.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="titleID"></param>
		/// <param name="itemID"></param>
		/// <param name="itemSequence"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type TitleItem.</returns>
		public TitleItem TitleItemInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int titleID,
			int itemID,
			short? itemSequence,
			int? creationUserID,
			int? lastModifiedUserID)
		{
			return TitleItemInsertAuto( sqlConnection, sqlTransaction, "BHL", titleID, itemID, itemSequence, creationUserID, lastModifiedUserID );
		}
		
		/// <summary>
		/// Insert values into TitleItem.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="titleID"></param>
		/// <param name="itemID"></param>
		/// <param name="itemSequence"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type TitleItem.</returns>
		public TitleItem TitleItemInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int titleID,
			int itemID,
			short? itemSequence,
			int? creationUserID,
			int? lastModifiedUserID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleItemInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("TitleItemID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("TitleID", SqlDbType.Int, null, false, titleID),
					CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemID),
					CustomSqlHelper.CreateInputParameter("ItemSequence", SqlDbType.SmallInt, null, true, itemSequence),
					CustomSqlHelper.CreateInputParameter("CreationUserID", SqlDbType.Int, null, true, creationUserID),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, true, lastModifiedUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<TitleItem> helper = new CustomSqlHelper<TitleItem>())
				{
					CustomGenericList<TitleItem> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						TitleItem o = list[0];
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
		/// Insert values into TitleItem. Returns an object of type TitleItem.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type TitleItem.</param>
		/// <returns>Object of type TitleItem.</returns>
		public TitleItem TitleItemInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			TitleItem value)
		{
			return TitleItemInsertAuto(sqlConnection, sqlTransaction, "BHL", value);
		}
		
		/// <summary>
		/// Insert values into TitleItem. Returns an object of type TitleItem.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type TitleItem.</param>
		/// <returns>Object of type TitleItem.</returns>
		public TitleItem TitleItemInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			TitleItem value)
		{
			return TitleItemInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.TitleID,
				value.ItemID,
				value.ItemSequence,
				value.CreationUserID,
				value.LastModifiedUserID);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from TitleItem by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="titleItemID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool TitleItemDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int titleItemID)
		{
			return TitleItemDeleteAuto( sqlConnection, sqlTransaction, "BHL", titleItemID );
		}
		
		/// <summary>
		/// Delete values from TitleItem by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="titleItemID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool TitleItemDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int titleItemID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleItemDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("TitleItemID", SqlDbType.Int, null, false, titleItemID), 
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
		/// Update values in TitleItem. Returns an object of type TitleItem.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="titleItemID"></param>
		/// <param name="titleID"></param>
		/// <param name="itemID"></param>
		/// <param name="itemSequence"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type TitleItem.</returns>
		public TitleItem TitleItemUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int titleItemID,
			int titleID,
			int itemID,
			short? itemSequence,
			int? lastModifiedUserID)
		{
			return TitleItemUpdateAuto( sqlConnection, sqlTransaction, "BHL", titleItemID, titleID, itemID, itemSequence, lastModifiedUserID);
		}
		
		/// <summary>
		/// Update values in TitleItem. Returns an object of type TitleItem.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="titleItemID"></param>
		/// <param name="titleID"></param>
		/// <param name="itemID"></param>
		/// <param name="itemSequence"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type TitleItem.</returns>
		public TitleItem TitleItemUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int titleItemID,
			int titleID,
			int itemID,
			short? itemSequence,
			int? lastModifiedUserID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleItemUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("TitleItemID", SqlDbType.Int, null, false, titleItemID),
					CustomSqlHelper.CreateInputParameter("TitleID", SqlDbType.Int, null, false, titleID),
					CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemID),
					CustomSqlHelper.CreateInputParameter("ItemSequence", SqlDbType.SmallInt, null, true, itemSequence),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, true, lastModifiedUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<TitleItem> helper = new CustomSqlHelper<TitleItem>())
				{
					CustomGenericList<TitleItem> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						TitleItem o = list[0];
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
		/// Update values in TitleItem. Returns an object of type TitleItem.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type TitleItem.</param>
		/// <returns>Object of type TitleItem.</returns>
		public TitleItem TitleItemUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			TitleItem value)
		{
			return TitleItemUpdateAuto(sqlConnection, sqlTransaction, "BHL", value );
		}
		
		/// <summary>
		/// Update values in TitleItem. Returns an object of type TitleItem.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type TitleItem.</param>
		/// <returns>Object of type TitleItem.</returns>
		public TitleItem TitleItemUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			TitleItem value)
		{
			return TitleItemUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.TitleItemID,
				value.TitleID,
				value.ItemID,
				value.ItemSequence,
				value.LastModifiedUserID);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage TitleItem object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in TitleItem.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type TitleItem.</param>
		/// <returns>Object of type CustomDataAccessStatus<TitleItem>.</returns>
		public CustomDataAccessStatus<TitleItem> TitleItemManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			TitleItem value , int userId )
		{
			return TitleItemManageAuto( sqlConnection, sqlTransaction, "BHL", value , userId );
		}
		
		/// <summary>
		/// Manage TitleItem object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in TitleItem.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type TitleItem.</param>
		/// <returns>Object of type CustomDataAccessStatus<TitleItem>.</returns>
		public CustomDataAccessStatus<TitleItem> TitleItemManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			TitleItem value , int userId )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				value.CreationUserID = userId;
				value.LastModifiedUserID = userId;
				TitleItem returnValue = TitleItemInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.TitleID,
						value.ItemID,
						value.ItemSequence,
						value.CreationUserID,
						value.LastModifiedUserID);
				
				return new CustomDataAccessStatus<TitleItem>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (TitleItemDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.TitleItemID))
				{
				return new CustomDataAccessStatus<TitleItem>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<TitleItem>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				value.LastModifiedUserID = userId;
				TitleItem returnValue = TitleItemUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.TitleItemID,
						value.TitleID,
						value.ItemID,
						value.ItemSequence,
						value.LastModifiedUserID);
					
				return new CustomDataAccessStatus<TitleItem>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<TitleItem>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}
// end of source generation
