
// Generated 1/5/2021 2:14:29 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class IAItemStatusDAL is based upon dbo.IAItemStatus.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHLImport.DAL
// {
// 		public partial class IAItemStatusDAL
//		{
//		}
// }

#endregion How To Implement

#region using

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHLImport.DataObjects;

#endregion using

namespace MOBOT.BHLImport.DAL
{
	partial class IAItemStatusDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from dbo.IAItemStatus by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="itemStatusID"></param>
		/// <returns>Object of type IAItemStatus.</returns>
		public IAItemStatus IAItemStatusSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int itemStatusID)
		{
			return IAItemStatusSelectAuto(	sqlConnection, sqlTransaction, "BHLImport",	itemStatusID );
		}
			
		/// <summary>
		/// Select values from dbo.IAItemStatus by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="itemStatusID"></param>
		/// <returns>Object of type IAItemStatus.</returns>
		public IAItemStatus IAItemStatusSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int itemStatusID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IAItemStatusSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("ItemStatusID", SqlDbType.Int, null, false, itemStatusID)))
			{
				using (CustomSqlHelper<IAItemStatus> helper = new CustomSqlHelper<IAItemStatus>())
				{
					List<IAItemStatus> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						IAItemStatus o = list[0];
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
		/// Select values from dbo.IAItemStatus by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="itemStatusID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> IAItemStatusSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int itemStatusID)
		{
			return IAItemStatusSelectAutoRaw( sqlConnection, sqlTransaction, "BHLImport", itemStatusID );
		}
		
		/// <summary>
		/// Select values from dbo.IAItemStatus by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="itemStatusID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> IAItemStatusSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int itemStatusID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IAItemStatusSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("ItemStatusID", SqlDbType.Int, null, false, itemStatusID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====

 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into dbo.IAItemStatus.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="itemStatusID"></param>
		/// <param name="status"></param>
		/// <param name="description"></param>
		/// <returns>Object of type IAItemStatus.</returns>
		public IAItemStatus IAItemStatusInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int itemStatusID,
			string status,
			string description)
		{
			return IAItemStatusInsertAuto( sqlConnection, sqlTransaction, "BHLImport", itemStatusID, status, description );
		}
		
		/// <summary>
		/// Insert values into dbo.IAItemStatus.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="itemStatusID"></param>
		/// <param name="status"></param>
		/// <param name="description"></param>
		/// <returns>Object of type IAItemStatus.</returns>
		public IAItemStatus IAItemStatusInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int itemStatusID,
			string status,
			string description)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IAItemStatusInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("ItemStatusID", SqlDbType.Int, null, false, itemStatusID),
					CustomSqlHelper.CreateInputParameter("Status", SqlDbType.NVarChar, 30, false, status),
					CustomSqlHelper.CreateInputParameter("Description", SqlDbType.NVarChar, 4000, false, description), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<IAItemStatus> helper = new CustomSqlHelper<IAItemStatus>())
				{
					List<IAItemStatus> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						IAItemStatus o = list[0];
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
		/// Insert values into dbo.IAItemStatus. Returns an object of type IAItemStatus.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type IAItemStatus.</param>
		/// <returns>Object of type IAItemStatus.</returns>
		public IAItemStatus IAItemStatusInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			IAItemStatus value)
		{
			return IAItemStatusInsertAuto(sqlConnection, sqlTransaction, "BHLImport", value);
		}
		
		/// <summary>
		/// Insert values into dbo.IAItemStatus. Returns an object of type IAItemStatus.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type IAItemStatus.</param>
		/// <returns>Object of type IAItemStatus.</returns>
		public IAItemStatus IAItemStatusInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			IAItemStatus value)
		{
			return IAItemStatusInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.ItemStatusID,
				value.Status,
				value.Description);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from dbo.IAItemStatus by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="itemStatusID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool IAItemStatusDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int itemStatusID)
		{
			return IAItemStatusDeleteAuto( sqlConnection, sqlTransaction, "BHLImport", itemStatusID );
		}
		
		/// <summary>
		/// Delete values from dbo.IAItemStatus by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="itemStatusID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool IAItemStatusDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int itemStatusID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IAItemStatusDeleteAuto", connection, transaction, 
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
		/// Update values in dbo.IAItemStatus. Returns an object of type IAItemStatus.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="itemStatusID"></param>
		/// <param name="status"></param>
		/// <param name="description"></param>
		/// <returns>Object of type IAItemStatus.</returns>
		public IAItemStatus IAItemStatusUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int itemStatusID,
			string status,
			string description)
		{
			return IAItemStatusUpdateAuto( sqlConnection, sqlTransaction, "BHLImport", itemStatusID, status, description);
		}
		
		/// <summary>
		/// Update values in dbo.IAItemStatus. Returns an object of type IAItemStatus.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="itemStatusID"></param>
		/// <param name="status"></param>
		/// <param name="description"></param>
		/// <returns>Object of type IAItemStatus.</returns>
		public IAItemStatus IAItemStatusUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int itemStatusID,
			string status,
			string description)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IAItemStatusUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("ItemStatusID", SqlDbType.Int, null, false, itemStatusID),
					CustomSqlHelper.CreateInputParameter("Status", SqlDbType.NVarChar, 30, false, status),
					CustomSqlHelper.CreateInputParameter("Description", SqlDbType.NVarChar, 4000, false, description), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<IAItemStatus> helper = new CustomSqlHelper<IAItemStatus>())
				{
					List<IAItemStatus> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						IAItemStatus o = list[0];
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
		/// Update values in dbo.IAItemStatus. Returns an object of type IAItemStatus.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type IAItemStatus.</param>
		/// <returns>Object of type IAItemStatus.</returns>
		public IAItemStatus IAItemStatusUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			IAItemStatus value)
		{
			return IAItemStatusUpdateAuto(sqlConnection, sqlTransaction, "BHLImport", value );
		}
		
		/// <summary>
		/// Update values in dbo.IAItemStatus. Returns an object of type IAItemStatus.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type IAItemStatus.</param>
		/// <returns>Object of type IAItemStatus.</returns>
		public IAItemStatus IAItemStatusUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			IAItemStatus value)
		{
			return IAItemStatusUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.ItemStatusID,
				value.Status,
				value.Description);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage dbo.IAItemStatus object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.IAItemStatus.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type IAItemStatus.</param>
		/// <returns>Object of type CustomDataAccessStatus<IAItemStatus>.</returns>
		public CustomDataAccessStatus<IAItemStatus> IAItemStatusManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			IAItemStatus value  )
		{
			return IAItemStatusManageAuto( sqlConnection, sqlTransaction, "BHLImport", value  );
		}
		
		/// <summary>
		/// Manage dbo.IAItemStatus object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.IAItemStatus.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type IAItemStatus.</param>
		/// <returns>Object of type CustomDataAccessStatus<IAItemStatus>.</returns>
		public CustomDataAccessStatus<IAItemStatus> IAItemStatusManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			IAItemStatus value  )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				
				
				IAItemStatus returnValue = IAItemStatusInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.ItemStatusID,
						value.Status,
						value.Description);
				
				return new CustomDataAccessStatus<IAItemStatus>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (IAItemStatusDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.ItemStatusID))
				{
				return new CustomDataAccessStatus<IAItemStatus>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<IAItemStatus>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				
				IAItemStatus returnValue = IAItemStatusUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.ItemStatusID,
						value.Status,
						value.Description);
					
				return new CustomDataAccessStatus<IAItemStatus>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<IAItemStatus>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}

