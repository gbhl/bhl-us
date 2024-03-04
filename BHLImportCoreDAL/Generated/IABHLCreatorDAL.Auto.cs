
// Generated 3/4/2024 11:54:14 AM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class IABHLCreatorDAL is based upon dbo.IABHLCreator.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHLImport.DAL
// {
// 		public partial class IABHLCreatorDAL
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
	partial class IABHLCreatorDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from dbo.IABHLCreator by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="bHLCreatorID"></param>
		/// <returns>Object of type IABHLCreator.</returns>
		public IABHLCreator IABHLCreatorSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int bHLCreatorID)
		{
			return IABHLCreatorSelectAuto(	sqlConnection, sqlTransaction, "BHLImport",	bHLCreatorID );
		}
			
		/// <summary>
		/// Select values from dbo.IABHLCreator by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="bHLCreatorID"></param>
		/// <returns>Object of type IABHLCreator.</returns>
		public IABHLCreator IABHLCreatorSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int bHLCreatorID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IABHLCreatorSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("BHLCreatorID", SqlDbType.Int, null, false, bHLCreatorID)))
			{
				using (CustomSqlHelper<IABHLCreator> helper = new CustomSqlHelper<IABHLCreator>())
				{
					List<IABHLCreator> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						IABHLCreator o = list[0];
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
		/// Select values from dbo.IABHLCreator by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="bHLCreatorID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> IABHLCreatorSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int bHLCreatorID)
		{
			return IABHLCreatorSelectAutoRaw( sqlConnection, sqlTransaction, "BHLImport", bHLCreatorID );
		}
		
		/// <summary>
		/// Select values from dbo.IABHLCreator by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="bHLCreatorID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> IABHLCreatorSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int bHLCreatorID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IABHLCreatorSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("BHLCreatorID", SqlDbType.Int, null, false, bHLCreatorID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====

 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into dbo.IABHLCreator.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="itemID"></param>
		/// <param name="name"></param>
		/// <returns>Object of type IABHLCreator.</returns>
		public IABHLCreator IABHLCreatorInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int itemID,
			string name)
		{
			return IABHLCreatorInsertAuto( sqlConnection, sqlTransaction, "BHLImport", itemID, name );
		}
		
		/// <summary>
		/// Insert values into dbo.IABHLCreator.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="itemID"></param>
		/// <param name="name"></param>
		/// <returns>Object of type IABHLCreator.</returns>
		public IABHLCreator IABHLCreatorInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int itemID,
			string name)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IABHLCreatorInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("BHLCreatorID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemID),
					CustomSqlHelper.CreateInputParameter("Name", SqlDbType.NVarChar, 300, false, name), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<IABHLCreator> helper = new CustomSqlHelper<IABHLCreator>())
				{
					List<IABHLCreator> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						IABHLCreator o = list[0];
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
		/// Insert values into dbo.IABHLCreator. Returns an object of type IABHLCreator.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type IABHLCreator.</param>
		/// <returns>Object of type IABHLCreator.</returns>
		public IABHLCreator IABHLCreatorInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			IABHLCreator value)
		{
			return IABHLCreatorInsertAuto(sqlConnection, sqlTransaction, "BHLImport", value);
		}
		
		/// <summary>
		/// Insert values into dbo.IABHLCreator. Returns an object of type IABHLCreator.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type IABHLCreator.</param>
		/// <returns>Object of type IABHLCreator.</returns>
		public IABHLCreator IABHLCreatorInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			IABHLCreator value)
		{
			return IABHLCreatorInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.ItemID,
				value.Name);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from dbo.IABHLCreator by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="bHLCreatorID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool IABHLCreatorDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int bHLCreatorID)
		{
			return IABHLCreatorDeleteAuto( sqlConnection, sqlTransaction, "BHLImport", bHLCreatorID );
		}
		
		/// <summary>
		/// Delete values from dbo.IABHLCreator by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="bHLCreatorID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool IABHLCreatorDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int bHLCreatorID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IABHLCreatorDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("BHLCreatorID", SqlDbType.Int, null, false, bHLCreatorID), 
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
		/// Update values in dbo.IABHLCreator. Returns an object of type IABHLCreator.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="bHLCreatorID"></param>
		/// <param name="itemID"></param>
		/// <param name="name"></param>
		/// <returns>Object of type IABHLCreator.</returns>
		public IABHLCreator IABHLCreatorUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int bHLCreatorID,
			int itemID,
			string name)
		{
			return IABHLCreatorUpdateAuto( sqlConnection, sqlTransaction, "BHLImport", bHLCreatorID, itemID, name);
		}
		
		/// <summary>
		/// Update values in dbo.IABHLCreator. Returns an object of type IABHLCreator.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="bHLCreatorID"></param>
		/// <param name="itemID"></param>
		/// <param name="name"></param>
		/// <returns>Object of type IABHLCreator.</returns>
		public IABHLCreator IABHLCreatorUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int bHLCreatorID,
			int itemID,
			string name)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IABHLCreatorUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("BHLCreatorID", SqlDbType.Int, null, false, bHLCreatorID),
					CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemID),
					CustomSqlHelper.CreateInputParameter("Name", SqlDbType.NVarChar, 300, false, name), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<IABHLCreator> helper = new CustomSqlHelper<IABHLCreator>())
				{
					List<IABHLCreator> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						IABHLCreator o = list[0];
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
		/// Update values in dbo.IABHLCreator. Returns an object of type IABHLCreator.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type IABHLCreator.</param>
		/// <returns>Object of type IABHLCreator.</returns>
		public IABHLCreator IABHLCreatorUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			IABHLCreator value)
		{
			return IABHLCreatorUpdateAuto(sqlConnection, sqlTransaction, "BHLImport", value );
		}
		
		/// <summary>
		/// Update values in dbo.IABHLCreator. Returns an object of type IABHLCreator.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type IABHLCreator.</param>
		/// <returns>Object of type IABHLCreator.</returns>
		public IABHLCreator IABHLCreatorUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			IABHLCreator value)
		{
			return IABHLCreatorUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.BHLCreatorID,
				value.ItemID,
				value.Name);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage dbo.IABHLCreator object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.IABHLCreator.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type IABHLCreator.</param>
		/// <returns>Object of type CustomDataAccessStatus<IABHLCreator>.</returns>
		public CustomDataAccessStatus<IABHLCreator> IABHLCreatorManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			IABHLCreator value  )
		{
			return IABHLCreatorManageAuto( sqlConnection, sqlTransaction, "BHLImport", value  );
		}
		
		/// <summary>
		/// Manage dbo.IABHLCreator object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.IABHLCreator.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type IABHLCreator.</param>
		/// <returns>Object of type CustomDataAccessStatus<IABHLCreator>.</returns>
		public CustomDataAccessStatus<IABHLCreator> IABHLCreatorManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			IABHLCreator value  )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				
				
				IABHLCreator returnValue = IABHLCreatorInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.ItemID,
						value.Name);
				
				return new CustomDataAccessStatus<IABHLCreator>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (IABHLCreatorDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.BHLCreatorID))
				{
				return new CustomDataAccessStatus<IABHLCreator>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<IABHLCreator>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				
				IABHLCreator returnValue = IABHLCreatorUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.BHLCreatorID,
						value.ItemID,
						value.Name);
					
				return new CustomDataAccessStatus<IABHLCreator>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<IABHLCreator>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}

