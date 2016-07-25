
// Generated 6/2/2016 9:31:31 AM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class ItemInstitutionDAL is based upon dbo.ItemInstitution.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DAL
// {
// 		public partial class ItemInstitutionDAL
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
	partial class ItemInstitutionDAL : IItemInstitutionDAL
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from dbo.ItemInstitution by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="itemInstitutionID"></param>
		/// <returns>Object of type ItemInstitution.</returns>
		public ItemInstitution ItemInstitutionSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int itemInstitutionID)
		{
			return ItemInstitutionSelectAuto(	sqlConnection, sqlTransaction, "BHL",	itemInstitutionID );
		}
			
		/// <summary>
		/// Select values from dbo.ItemInstitution by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="itemInstitutionID"></param>
		/// <returns>Object of type ItemInstitution.</returns>
		public ItemInstitution ItemInstitutionSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int itemInstitutionID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("ItemInstitutionSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("ItemInstitutionID", SqlDbType.Int, null, false, itemInstitutionID)))
			{
				using (CustomSqlHelper<ItemInstitution> helper = new CustomSqlHelper<ItemInstitution>())
				{
					CustomGenericList<ItemInstitution> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						ItemInstitution o = list[0];
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
		/// Select values from dbo.ItemInstitution by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="itemInstitutionID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> ItemInstitutionSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int itemInstitutionID)
		{
			return ItemInstitutionSelectAutoRaw( sqlConnection, sqlTransaction, "BHL", itemInstitutionID );
		}
		
		/// <summary>
		/// Select values from dbo.ItemInstitution by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="itemInstitutionID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> ItemInstitutionSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int itemInstitutionID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("ItemInstitutionSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("ItemInstitutionID", SqlDbType.Int, null, false, itemInstitutionID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====

 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into dbo.ItemInstitution.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="itemID"></param>
		/// <param name="institutionCode"></param>
		/// <param name="institutionRoleID"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type ItemInstitution.</returns>
		public ItemInstitution ItemInstitutionInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int itemID,
			string institutionCode,
			int institutionRoleID,
			int creationUserID,
			int lastModifiedUserID)
		{
			return ItemInstitutionInsertAuto( sqlConnection, sqlTransaction, "BHL", itemID, institutionCode, institutionRoleID, creationUserID, lastModifiedUserID );
		}
		
		/// <summary>
		/// Insert values into dbo.ItemInstitution.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="itemID"></param>
		/// <param name="institutionCode"></param>
		/// <param name="institutionRoleID"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type ItemInstitution.</returns>
		public ItemInstitution ItemInstitutionInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int itemID,
			string institutionCode,
			int institutionRoleID,
			int creationUserID,
			int lastModifiedUserID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("ItemInstitutionInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("ItemInstitutionID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemID),
					CustomSqlHelper.CreateInputParameter("InstitutionCode", SqlDbType.NVarChar, 10, false, institutionCode),
					CustomSqlHelper.CreateInputParameter("InstitutionRoleID", SqlDbType.Int, null, false, institutionRoleID),
					CustomSqlHelper.CreateInputParameter("CreationUserID", SqlDbType.Int, null, false, creationUserID),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, false, lastModifiedUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<ItemInstitution> helper = new CustomSqlHelper<ItemInstitution>())
				{
					CustomGenericList<ItemInstitution> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						ItemInstitution o = list[0];
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
		/// Insert values into dbo.ItemInstitution. Returns an object of type ItemInstitution.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type ItemInstitution.</param>
		/// <returns>Object of type ItemInstitution.</returns>
		public ItemInstitution ItemInstitutionInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			ItemInstitution value)
		{
			return ItemInstitutionInsertAuto(sqlConnection, sqlTransaction, "BHL", value);
		}
		
		/// <summary>
		/// Insert values into dbo.ItemInstitution. Returns an object of type ItemInstitution.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type ItemInstitution.</param>
		/// <returns>Object of type ItemInstitution.</returns>
		public ItemInstitution ItemInstitutionInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			ItemInstitution value)
		{
			return ItemInstitutionInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.ItemID,
				value.InstitutionCode,
				value.InstitutionRoleID,
				value.CreationUserID,
				value.LastModifiedUserID);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from dbo.ItemInstitution by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="itemInstitutionID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool ItemInstitutionDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int itemInstitutionID)
		{
			return ItemInstitutionDeleteAuto( sqlConnection, sqlTransaction, "BHL", itemInstitutionID );
		}
		
		/// <summary>
		/// Delete values from dbo.ItemInstitution by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="itemInstitutionID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool ItemInstitutionDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int itemInstitutionID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("ItemInstitutionDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("ItemInstitutionID", SqlDbType.Int, null, false, itemInstitutionID), 
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
		/// Update values in dbo.ItemInstitution. Returns an object of type ItemInstitution.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="itemInstitutionID"></param>
		/// <param name="itemID"></param>
		/// <param name="institutionCode"></param>
		/// <param name="institutionRoleID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type ItemInstitution.</returns>
		public ItemInstitution ItemInstitutionUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int itemInstitutionID,
			int itemID,
			string institutionCode,
			int institutionRoleID,
			int lastModifiedUserID)
		{
			return ItemInstitutionUpdateAuto( sqlConnection, sqlTransaction, "BHL", itemInstitutionID, itemID, institutionCode, institutionRoleID, lastModifiedUserID);
		}
		
		/// <summary>
		/// Update values in dbo.ItemInstitution. Returns an object of type ItemInstitution.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="itemInstitutionID"></param>
		/// <param name="itemID"></param>
		/// <param name="institutionCode"></param>
		/// <param name="institutionRoleID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type ItemInstitution.</returns>
		public ItemInstitution ItemInstitutionUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int itemInstitutionID,
			int itemID,
			string institutionCode,
			int institutionRoleID,
			int lastModifiedUserID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("ItemInstitutionUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("ItemInstitutionID", SqlDbType.Int, null, false, itemInstitutionID),
					CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemID),
					CustomSqlHelper.CreateInputParameter("InstitutionCode", SqlDbType.NVarChar, 10, false, institutionCode),
					CustomSqlHelper.CreateInputParameter("InstitutionRoleID", SqlDbType.Int, null, false, institutionRoleID),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, false, lastModifiedUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<ItemInstitution> helper = new CustomSqlHelper<ItemInstitution>())
				{
					CustomGenericList<ItemInstitution> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						ItemInstitution o = list[0];
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
		/// Update values in dbo.ItemInstitution. Returns an object of type ItemInstitution.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type ItemInstitution.</param>
		/// <returns>Object of type ItemInstitution.</returns>
		public ItemInstitution ItemInstitutionUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			ItemInstitution value)
		{
			return ItemInstitutionUpdateAuto(sqlConnection, sqlTransaction, "BHL", value );
		}
		
		/// <summary>
		/// Update values in dbo.ItemInstitution. Returns an object of type ItemInstitution.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type ItemInstitution.</param>
		/// <returns>Object of type ItemInstitution.</returns>
		public ItemInstitution ItemInstitutionUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			ItemInstitution value)
		{
			return ItemInstitutionUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.ItemInstitutionID,
				value.ItemID,
				value.InstitutionCode,
				value.InstitutionRoleID,
				value.LastModifiedUserID);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage dbo.ItemInstitution object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.ItemInstitution.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type ItemInstitution.</param>
		/// <returns>Object of type CustomDataAccessStatus<ItemInstitution>.</returns>
		public CustomDataAccessStatus<ItemInstitution> ItemInstitutionManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			ItemInstitution value , int userId )
		{
			return ItemInstitutionManageAuto( sqlConnection, sqlTransaction, "BHL", value , userId );
		}
		
		/// <summary>
		/// Manage dbo.ItemInstitution object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.ItemInstitution.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type ItemInstitution.</param>
		/// <returns>Object of type CustomDataAccessStatus<ItemInstitution>.</returns>
		public CustomDataAccessStatus<ItemInstitution> ItemInstitutionManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			ItemInstitution value , int userId )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				value.CreationUserID = userId;
				value.LastModifiedUserID = userId;
				ItemInstitution returnValue = ItemInstitutionInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.ItemID,
						value.InstitutionCode,
						value.InstitutionRoleID,
						value.CreationUserID,
						value.LastModifiedUserID);
				
				return new CustomDataAccessStatus<ItemInstitution>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (ItemInstitutionDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.ItemInstitutionID))
				{
				return new CustomDataAccessStatus<ItemInstitution>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<ItemInstitution>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				value.LastModifiedUserID = userId;
				ItemInstitution returnValue = ItemInstitutionUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.ItemInstitutionID,
						value.ItemID,
						value.InstitutionCode,
						value.InstitutionRoleID,
						value.LastModifiedUserID);
					
				return new CustomDataAccessStatus<ItemInstitution>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<ItemInstitution>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}

