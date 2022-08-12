
// Generated 1/5/2021 2:11:52 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class BSItemStatusDAL is based upon dbo.BSItemStatus.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHLImport.DAL
// {
// 		public partial class BSItemStatusDAL
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
	partial class BSItemStatusDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from dbo.BSItemStatus by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="itemStatusID"></param>
		/// <returns>Object of type BSItemStatus.</returns>
		public BSItemStatus BSItemStatusSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int itemStatusID)
		{
			return BSItemStatusSelectAuto(	sqlConnection, sqlTransaction, "BHLImport",	itemStatusID );
		}
			
		/// <summary>
		/// Select values from dbo.BSItemStatus by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="itemStatusID"></param>
		/// <returns>Object of type BSItemStatus.</returns>
		public BSItemStatus BSItemStatusSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int itemStatusID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("BSItemStatusSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("ItemStatusID", SqlDbType.Int, null, false, itemStatusID)))
			{
				using (CustomSqlHelper<BSItemStatus> helper = new CustomSqlHelper<BSItemStatus>())
				{
					List<BSItemStatus> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						BSItemStatus o = list[0];
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
		/// Select values from dbo.BSItemStatus by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="itemStatusID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> BSItemStatusSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int itemStatusID)
		{
			return BSItemStatusSelectAutoRaw( sqlConnection, sqlTransaction, "BHLImport", itemStatusID );
		}
		
		/// <summary>
		/// Select values from dbo.BSItemStatus by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="itemStatusID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> BSItemStatusSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int itemStatusID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("BSItemStatusSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("ItemStatusID", SqlDbType.Int, null, false, itemStatusID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====

 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into dbo.BSItemStatus.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="itemStatusID"></param>
		/// <param name="status"></param>
		/// <param name="description"></param>
		/// <returns>Object of type BSItemStatus.</returns>
		public BSItemStatus BSItemStatusInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int itemStatusID,
			string status,
			string description)
		{
			return BSItemStatusInsertAuto( sqlConnection, sqlTransaction, "BHLImport", itemStatusID, status, description );
		}
		
		/// <summary>
		/// Insert values into dbo.BSItemStatus.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="itemStatusID"></param>
		/// <param name="status"></param>
		/// <param name="description"></param>
		/// <returns>Object of type BSItemStatus.</returns>
		public BSItemStatus BSItemStatusInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int itemStatusID,
			string status,
			string description)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("BSItemStatusInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("ItemStatusID", SqlDbType.Int, null, false, itemStatusID),
					CustomSqlHelper.CreateInputParameter("Status", SqlDbType.NVarChar, 30, false, status),
					CustomSqlHelper.CreateInputParameter("Description", SqlDbType.NVarChar, 4000, false, description), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<BSItemStatus> helper = new CustomSqlHelper<BSItemStatus>())
				{
					List<BSItemStatus> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						BSItemStatus o = list[0];
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
		/// Insert values into dbo.BSItemStatus. Returns an object of type BSItemStatus.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type BSItemStatus.</param>
		/// <returns>Object of type BSItemStatus.</returns>
		public BSItemStatus BSItemStatusInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			BSItemStatus value)
		{
			return BSItemStatusInsertAuto(sqlConnection, sqlTransaction, "BHLImport", value);
		}
		
		/// <summary>
		/// Insert values into dbo.BSItemStatus. Returns an object of type BSItemStatus.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type BSItemStatus.</param>
		/// <returns>Object of type BSItemStatus.</returns>
		public BSItemStatus BSItemStatusInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			BSItemStatus value)
		{
			return BSItemStatusInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.ItemStatusID,
				value.Status,
				value.Description);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from dbo.BSItemStatus by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="itemStatusID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool BSItemStatusDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int itemStatusID)
		{
			return BSItemStatusDeleteAuto( sqlConnection, sqlTransaction, "BHLImport", itemStatusID );
		}
		
		/// <summary>
		/// Delete values from dbo.BSItemStatus by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="itemStatusID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool BSItemStatusDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int itemStatusID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("BSItemStatusDeleteAuto", connection, transaction, 
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
		/// Update values in dbo.BSItemStatus. Returns an object of type BSItemStatus.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="itemStatusID"></param>
		/// <param name="status"></param>
		/// <param name="description"></param>
		/// <returns>Object of type BSItemStatus.</returns>
		public BSItemStatus BSItemStatusUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int itemStatusID,
			string status,
			string description)
		{
			return BSItemStatusUpdateAuto( sqlConnection, sqlTransaction, "BHLImport", itemStatusID, status, description);
		}
		
		/// <summary>
		/// Update values in dbo.BSItemStatus. Returns an object of type BSItemStatus.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="itemStatusID"></param>
		/// <param name="status"></param>
		/// <param name="description"></param>
		/// <returns>Object of type BSItemStatus.</returns>
		public BSItemStatus BSItemStatusUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int itemStatusID,
			string status,
			string description)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("BSItemStatusUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("ItemStatusID", SqlDbType.Int, null, false, itemStatusID),
					CustomSqlHelper.CreateInputParameter("Status", SqlDbType.NVarChar, 30, false, status),
					CustomSqlHelper.CreateInputParameter("Description", SqlDbType.NVarChar, 4000, false, description), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<BSItemStatus> helper = new CustomSqlHelper<BSItemStatus>())
				{
					List<BSItemStatus> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						BSItemStatus o = list[0];
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
		/// Update values in dbo.BSItemStatus. Returns an object of type BSItemStatus.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type BSItemStatus.</param>
		/// <returns>Object of type BSItemStatus.</returns>
		public BSItemStatus BSItemStatusUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			BSItemStatus value)
		{
			return BSItemStatusUpdateAuto(sqlConnection, sqlTransaction, "BHLImport", value );
		}
		
		/// <summary>
		/// Update values in dbo.BSItemStatus. Returns an object of type BSItemStatus.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type BSItemStatus.</param>
		/// <returns>Object of type BSItemStatus.</returns>
		public BSItemStatus BSItemStatusUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			BSItemStatus value)
		{
			return BSItemStatusUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.ItemStatusID,
				value.Status,
				value.Description);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage dbo.BSItemStatus object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.BSItemStatus.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type BSItemStatus.</param>
		/// <returns>Object of type CustomDataAccessStatus<BSItemStatus>.</returns>
		public CustomDataAccessStatus<BSItemStatus> BSItemStatusManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			BSItemStatus value  )
		{
			return BSItemStatusManageAuto( sqlConnection, sqlTransaction, "BHLImport", value  );
		}
		
		/// <summary>
		/// Manage dbo.BSItemStatus object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.BSItemStatus.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type BSItemStatus.</param>
		/// <returns>Object of type CustomDataAccessStatus<BSItemStatus>.</returns>
		public CustomDataAccessStatus<BSItemStatus> BSItemStatusManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			BSItemStatus value  )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				
				
				BSItemStatus returnValue = BSItemStatusInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.ItemStatusID,
						value.Status,
						value.Description);
				
				return new CustomDataAccessStatus<BSItemStatus>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (BSItemStatusDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.ItemStatusID))
				{
				return new CustomDataAccessStatus<BSItemStatus>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<BSItemStatus>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				
				BSItemStatus returnValue = BSItemStatusUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.ItemStatusID,
						value.Status,
						value.Description);
					
				return new CustomDataAccessStatus<BSItemStatus>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<BSItemStatus>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}

