
// Generated 10/19/2020 12:51:49 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class ItemRelationshipDAL is based upon dbo.ItemRelationship.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DAL
// {
// 		public partial class ItemRelationshipDAL
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
	partial class ItemRelationshipDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from dbo.ItemRelationship by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="relationshipID"></param>
		/// <returns>Object of type ItemRelationship.</returns>
		public ItemRelationship ItemRelationshipSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int relationshipID)
		{
			return ItemRelationshipSelectAuto(	sqlConnection, sqlTransaction, "BHL",	relationshipID );
		}
			
		/// <summary>
		/// Select values from dbo.ItemRelationship by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="relationshipID"></param>
		/// <returns>Object of type ItemRelationship.</returns>
		public ItemRelationship ItemRelationshipSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int relationshipID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("ItemRelationshipSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("RelationshipID", SqlDbType.Int, null, false, relationshipID)))
			{
				using (CustomSqlHelper<ItemRelationship> helper = new CustomSqlHelper<ItemRelationship>())
				{
					CustomGenericList<ItemRelationship> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						ItemRelationship o = list[0];
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
		/// Select values from dbo.ItemRelationship by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="relationshipID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> ItemRelationshipSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int relationshipID)
		{
			return ItemRelationshipSelectAutoRaw( sqlConnection, sqlTransaction, "BHL", relationshipID );
		}
		
		/// <summary>
		/// Select values from dbo.ItemRelationship by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="relationshipID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> ItemRelationshipSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int relationshipID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("ItemRelationshipSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("RelationshipID", SqlDbType.Int, null, false, relationshipID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====

 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into dbo.ItemRelationship.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="parentID"></param>
		/// <param name="childID"></param>
		/// <param name="sequenceOrder"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type ItemRelationship.</returns>
		public ItemRelationship ItemRelationshipInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int parentID,
			int childID,
			int sequenceOrder,
			int? creationUserID,
			int? lastModifiedUserID)
		{
			return ItemRelationshipInsertAuto( sqlConnection, sqlTransaction, "BHL", parentID, childID, sequenceOrder, creationUserID, lastModifiedUserID );
		}
		
		/// <summary>
		/// Insert values into dbo.ItemRelationship.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="parentID"></param>
		/// <param name="childID"></param>
		/// <param name="sequenceOrder"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type ItemRelationship.</returns>
		public ItemRelationship ItemRelationshipInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int parentID,
			int childID,
			int sequenceOrder,
			int? creationUserID,
			int? lastModifiedUserID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("ItemRelationshipInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("RelationshipID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("ParentID", SqlDbType.Int, null, false, parentID),
					CustomSqlHelper.CreateInputParameter("ChildID", SqlDbType.Int, null, false, childID),
					CustomSqlHelper.CreateInputParameter("SequenceOrder", SqlDbType.Int, null, false, sequenceOrder),
					CustomSqlHelper.CreateInputParameter("CreationUserID", SqlDbType.Int, null, true, creationUserID),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, true, lastModifiedUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<ItemRelationship> helper = new CustomSqlHelper<ItemRelationship>())
				{
					CustomGenericList<ItemRelationship> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						ItemRelationship o = list[0];
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
		/// Insert values into dbo.ItemRelationship. Returns an object of type ItemRelationship.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type ItemRelationship.</param>
		/// <returns>Object of type ItemRelationship.</returns>
		public ItemRelationship ItemRelationshipInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			ItemRelationship value)
		{
			return ItemRelationshipInsertAuto(sqlConnection, sqlTransaction, "BHL", value);
		}
		
		/// <summary>
		/// Insert values into dbo.ItemRelationship. Returns an object of type ItemRelationship.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type ItemRelationship.</param>
		/// <returns>Object of type ItemRelationship.</returns>
		public ItemRelationship ItemRelationshipInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			ItemRelationship value)
		{
			return ItemRelationshipInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.ParentID,
				value.ChildID,
				value.SequenceOrder,
				value.CreationUserID,
				value.LastModifiedUserID);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from dbo.ItemRelationship by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="relationshipID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool ItemRelationshipDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int relationshipID)
		{
			return ItemRelationshipDeleteAuto( sqlConnection, sqlTransaction, "BHL", relationshipID );
		}
		
		/// <summary>
		/// Delete values from dbo.ItemRelationship by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="relationshipID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool ItemRelationshipDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int relationshipID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("ItemRelationshipDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("RelationshipID", SqlDbType.Int, null, false, relationshipID), 
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
		/// Update values in dbo.ItemRelationship. Returns an object of type ItemRelationship.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="relationshipID"></param>
		/// <param name="parentID"></param>
		/// <param name="childID"></param>
		/// <param name="sequenceOrder"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type ItemRelationship.</returns>
		public ItemRelationship ItemRelationshipUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int relationshipID,
			int parentID,
			int childID,
			int sequenceOrder,
			int? lastModifiedUserID)
		{
			return ItemRelationshipUpdateAuto( sqlConnection, sqlTransaction, "BHL", relationshipID, parentID, childID, sequenceOrder, lastModifiedUserID);
		}
		
		/// <summary>
		/// Update values in dbo.ItemRelationship. Returns an object of type ItemRelationship.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="relationshipID"></param>
		/// <param name="parentID"></param>
		/// <param name="childID"></param>
		/// <param name="sequenceOrder"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type ItemRelationship.</returns>
		public ItemRelationship ItemRelationshipUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int relationshipID,
			int parentID,
			int childID,
			int sequenceOrder,
			int? lastModifiedUserID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("ItemRelationshipUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("RelationshipID", SqlDbType.Int, null, false, relationshipID),
					CustomSqlHelper.CreateInputParameter("ParentID", SqlDbType.Int, null, false, parentID),
					CustomSqlHelper.CreateInputParameter("ChildID", SqlDbType.Int, null, false, childID),
					CustomSqlHelper.CreateInputParameter("SequenceOrder", SqlDbType.Int, null, false, sequenceOrder),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, true, lastModifiedUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<ItemRelationship> helper = new CustomSqlHelper<ItemRelationship>())
				{
					CustomGenericList<ItemRelationship> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						ItemRelationship o = list[0];
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
		/// Update values in dbo.ItemRelationship. Returns an object of type ItemRelationship.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type ItemRelationship.</param>
		/// <returns>Object of type ItemRelationship.</returns>
		public ItemRelationship ItemRelationshipUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			ItemRelationship value)
		{
			return ItemRelationshipUpdateAuto(sqlConnection, sqlTransaction, "BHL", value );
		}
		
		/// <summary>
		/// Update values in dbo.ItemRelationship. Returns an object of type ItemRelationship.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type ItemRelationship.</param>
		/// <returns>Object of type ItemRelationship.</returns>
		public ItemRelationship ItemRelationshipUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			ItemRelationship value)
		{
			return ItemRelationshipUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.RelationshipID,
				value.ParentID,
				value.ChildID,
				value.SequenceOrder,
				value.LastModifiedUserID);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage dbo.ItemRelationship object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.ItemRelationship.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type ItemRelationship.</param>
		/// <returns>Object of type CustomDataAccessStatus<ItemRelationship>.</returns>
		public CustomDataAccessStatus<ItemRelationship> ItemRelationshipManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			ItemRelationship value , int userId )
		{
			return ItemRelationshipManageAuto( sqlConnection, sqlTransaction, "BHL", value , userId );
		}
		
		/// <summary>
		/// Manage dbo.ItemRelationship object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.ItemRelationship.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type ItemRelationship.</param>
		/// <returns>Object of type CustomDataAccessStatus<ItemRelationship>.</returns>
		public CustomDataAccessStatus<ItemRelationship> ItemRelationshipManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			ItemRelationship value , int userId )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				value.CreationUserID = userId;
				value.LastModifiedUserID = userId;
				ItemRelationship returnValue = ItemRelationshipInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.ParentID,
						value.ChildID,
						value.SequenceOrder,
						value.CreationUserID,
						value.LastModifiedUserID);
				
				return new CustomDataAccessStatus<ItemRelationship>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (ItemRelationshipDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.RelationshipID))
				{
				return new CustomDataAccessStatus<ItemRelationship>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<ItemRelationship>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				value.LastModifiedUserID = userId;
				ItemRelationship returnValue = ItemRelationshipUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.RelationshipID,
						value.ParentID,
						value.ChildID,
						value.SequenceOrder,
						value.LastModifiedUserID);
					
				return new CustomDataAccessStatus<ItemRelationship>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<ItemRelationship>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}

