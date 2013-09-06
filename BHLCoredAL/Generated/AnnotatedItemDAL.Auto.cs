
// Generated 7/14/2010 1:25:28 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class AnnotatedItemDAL is based upon AnnotatedItem.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DAL
// {
// 		public partial class AnnotatedItemDAL
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
	partial class AnnotatedItemDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from AnnotatedItem by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="annotatedItemID"></param>
		/// <returns>Object of type AnnotatedItem.</returns>
		public AnnotatedItem AnnotatedItemSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int annotatedItemID)
		{
			return AnnotatedItemSelectAuto(	sqlConnection, sqlTransaction, "BHL",	annotatedItemID );
		}
			
		/// <summary>
		/// Select values from AnnotatedItem by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="annotatedItemID"></param>
		/// <returns>Object of type AnnotatedItem.</returns>
		public AnnotatedItem AnnotatedItemSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int annotatedItemID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("annotation.AnnotatedItemSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("AnnotatedItemID", SqlDbType.Int, null, false, annotatedItemID)))
			{
				using (CustomSqlHelper<AnnotatedItem> helper = new CustomSqlHelper<AnnotatedItem>())
				{
					CustomGenericList<AnnotatedItem> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						AnnotatedItem o = list[0];
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
		/// Select values from AnnotatedItem by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="annotatedItemID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> AnnotatedItemSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int annotatedItemID)
		{
			return AnnotatedItemSelectAutoRaw( sqlConnection, sqlTransaction, "BHL", annotatedItemID );
		}
		
		/// <summary>
		/// Select values from AnnotatedItem by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="annotatedItemID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> AnnotatedItemSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int annotatedItemID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("annotation.AnnotatedItemSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("AnnotatedItemID", SqlDbType.Int, null, false, annotatedItemID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====
	
 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into AnnotatedItem.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="annotatedTitleID"></param>
		/// <param name="itemID"></param>
		/// <param name="externalIdentifier"></param>
		/// <param name="volume"></param>
		/// <returns>Object of type AnnotatedItem.</returns>
		public AnnotatedItem AnnotatedItemInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int annotatedTitleID,
			int? itemID,
			string externalIdentifier,
			string volume)
		{
			return AnnotatedItemInsertAuto( sqlConnection, sqlTransaction, "BHL", annotatedTitleID, itemID, externalIdentifier, volume );
		}
		
		/// <summary>
		/// Insert values into AnnotatedItem.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="annotatedTitleID"></param>
		/// <param name="itemID"></param>
		/// <param name="externalIdentifier"></param>
		/// <param name="volume"></param>
		/// <returns>Object of type AnnotatedItem.</returns>
		public AnnotatedItem AnnotatedItemInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int annotatedTitleID,
			int? itemID,
			string externalIdentifier,
			string volume)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("annotation.AnnotatedItemInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("AnnotatedItemID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("AnnotatedTitleID", SqlDbType.Int, null, false, annotatedTitleID),
					CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, true, itemID),
					CustomSqlHelper.CreateInputParameter("ExternalIdentifier", SqlDbType.NVarChar, 50, false, externalIdentifier),
					CustomSqlHelper.CreateInputParameter("Volume", SqlDbType.NVarChar, 10, false, volume), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<AnnotatedItem> helper = new CustomSqlHelper<AnnotatedItem>())
				{
					CustomGenericList<AnnotatedItem> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						AnnotatedItem o = list[0];
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
		/// Insert values into AnnotatedItem. Returns an object of type AnnotatedItem.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type AnnotatedItem.</param>
		/// <returns>Object of type AnnotatedItem.</returns>
		public AnnotatedItem AnnotatedItemInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			AnnotatedItem value)
		{
			return AnnotatedItemInsertAuto(sqlConnection, sqlTransaction, "BHL", value);
		}
		
		/// <summary>
		/// Insert values into AnnotatedItem. Returns an object of type AnnotatedItem.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type AnnotatedItem.</param>
		/// <returns>Object of type AnnotatedItem.</returns>
		public AnnotatedItem AnnotatedItemInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			AnnotatedItem value)
		{
			return AnnotatedItemInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.AnnotatedTitleID,
				value.ItemID,
				value.ExternalIdentifier,
				value.Volume);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from AnnotatedItem by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="annotatedItemID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool AnnotatedItemDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int annotatedItemID)
		{
			return AnnotatedItemDeleteAuto( sqlConnection, sqlTransaction, "BHL", annotatedItemID );
		}
		
		/// <summary>
		/// Delete values from AnnotatedItem by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="annotatedItemID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool AnnotatedItemDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int annotatedItemID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("annotation.AnnotatedItemDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("AnnotatedItemID", SqlDbType.Int, null, false, annotatedItemID), 
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
		/// Update values in AnnotatedItem. Returns an object of type AnnotatedItem.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="annotatedItemID"></param>
		/// <param name="annotatedTitleID"></param>
		/// <param name="itemID"></param>
		/// <param name="externalIdentifier"></param>
		/// <param name="volume"></param>
		/// <returns>Object of type AnnotatedItem.</returns>
		public AnnotatedItem AnnotatedItemUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int annotatedItemID,
			int annotatedTitleID,
			int? itemID,
			string externalIdentifier,
			string volume)
		{
			return AnnotatedItemUpdateAuto( sqlConnection, sqlTransaction, "BHL", annotatedItemID, annotatedTitleID, itemID, externalIdentifier, volume);
		}
		
		/// <summary>
		/// Update values in AnnotatedItem. Returns an object of type AnnotatedItem.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="annotatedItemID"></param>
		/// <param name="annotatedTitleID"></param>
		/// <param name="itemID"></param>
		/// <param name="externalIdentifier"></param>
		/// <param name="volume"></param>
		/// <returns>Object of type AnnotatedItem.</returns>
		public AnnotatedItem AnnotatedItemUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int annotatedItemID,
			int annotatedTitleID,
			int? itemID,
			string externalIdentifier,
			string volume)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("annotation.AnnotatedItemUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("AnnotatedItemID", SqlDbType.Int, null, false, annotatedItemID),
					CustomSqlHelper.CreateInputParameter("AnnotatedTitleID", SqlDbType.Int, null, false, annotatedTitleID),
					CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, true, itemID),
					CustomSqlHelper.CreateInputParameter("ExternalIdentifier", SqlDbType.NVarChar, 50, false, externalIdentifier),
					CustomSqlHelper.CreateInputParameter("Volume", SqlDbType.NVarChar, 10, false, volume), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<AnnotatedItem> helper = new CustomSqlHelper<AnnotatedItem>())
				{
					CustomGenericList<AnnotatedItem> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						AnnotatedItem o = list[0];
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
		/// Update values in AnnotatedItem. Returns an object of type AnnotatedItem.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type AnnotatedItem.</param>
		/// <returns>Object of type AnnotatedItem.</returns>
		public AnnotatedItem AnnotatedItemUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			AnnotatedItem value)
		{
			return AnnotatedItemUpdateAuto(sqlConnection, sqlTransaction, "BHL", value );
		}
		
		/// <summary>
		/// Update values in AnnotatedItem. Returns an object of type AnnotatedItem.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type AnnotatedItem.</param>
		/// <returns>Object of type AnnotatedItem.</returns>
		public AnnotatedItem AnnotatedItemUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			AnnotatedItem value)
		{
			return AnnotatedItemUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.AnnotatedItemID,
				value.AnnotatedTitleID,
				value.ItemID,
				value.ExternalIdentifier,
				value.Volume);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage AnnotatedItem object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in AnnotatedItem.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type AnnotatedItem.</param>
		/// <returns>Object of type CustomDataAccessStatus<AnnotatedItem>.</returns>
		public CustomDataAccessStatus<AnnotatedItem> AnnotatedItemManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			AnnotatedItem value  )
		{
			return AnnotatedItemManageAuto( sqlConnection, sqlTransaction, "BHL", value  );
		}
		
		/// <summary>
		/// Manage AnnotatedItem object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in AnnotatedItem.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type AnnotatedItem.</param>
		/// <returns>Object of type CustomDataAccessStatus<AnnotatedItem>.</returns>
		public CustomDataAccessStatus<AnnotatedItem> AnnotatedItemManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			AnnotatedItem value  )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				
				
				AnnotatedItem returnValue = AnnotatedItemInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.AnnotatedTitleID,
						value.ItemID,
						value.ExternalIdentifier,
						value.Volume);
				
				return new CustomDataAccessStatus<AnnotatedItem>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (AnnotatedItemDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.AnnotatedItemID))
				{
				return new CustomDataAccessStatus<AnnotatedItem>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<AnnotatedItem>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				
				AnnotatedItem returnValue = AnnotatedItemUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.AnnotatedItemID,
						value.AnnotatedTitleID,
						value.ItemID,
						value.ExternalIdentifier,
						value.Volume);
					
				return new CustomDataAccessStatus<AnnotatedItem>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<AnnotatedItem>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}
// end of source generation
