
// Generated 2/28/2019 2:07:31 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class IAItemIdentifierDAL is based upon dbo.IAItemIdentifier.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHLImport.DAL
// {
// 		public partial class IAItemIdentifierDAL
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
	partial class IAItemIdentifierDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from dbo.IAItemIdentifier by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="itemIdentifierID"></param>
		/// <returns>Object of type IAItemIdentifier.</returns>
		public IAItemIdentifier IAItemIdentifierSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int itemIdentifierID)
		{
			return IAItemIdentifierSelectAuto(	sqlConnection, sqlTransaction, "BHLImport",	itemIdentifierID );
		}
			
		/// <summary>
		/// Select values from dbo.IAItemIdentifier by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="itemIdentifierID"></param>
		/// <returns>Object of type IAItemIdentifier.</returns>
		public IAItemIdentifier IAItemIdentifierSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int itemIdentifierID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IAItemIdentifierSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("ItemIdentifierID", SqlDbType.Int, null, false, itemIdentifierID)))
			{
				using (CustomSqlHelper<IAItemIdentifier> helper = new CustomSqlHelper<IAItemIdentifier>())
				{
					CustomGenericList<IAItemIdentifier> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						IAItemIdentifier o = list[0];
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
		/// Select values from dbo.IAItemIdentifier by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="itemIdentifierID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> IAItemIdentifierSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int itemIdentifierID)
		{
			return IAItemIdentifierSelectAutoRaw( sqlConnection, sqlTransaction, "BHLImport", itemIdentifierID );
		}
		
		/// <summary>
		/// Select values from dbo.IAItemIdentifier by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="itemIdentifierID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> IAItemIdentifierSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int itemIdentifierID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IAItemIdentifierSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("ItemIdentifierID", SqlDbType.Int, null, false, itemIdentifierID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====

 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into dbo.IAItemIdentifier.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="itemID"></param>
		/// <param name="identifierDescription"></param>
		/// <param name="identifierValue"></param>
		/// <returns>Object of type IAItemIdentifier.</returns>
		public IAItemIdentifier IAItemIdentifierInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int itemID,
			string identifierDescription,
			string identifierValue)
		{
			return IAItemIdentifierInsertAuto( sqlConnection, sqlTransaction, "BHLImport", itemID, identifierDescription, identifierValue );
		}
		
		/// <summary>
		/// Insert values into dbo.IAItemIdentifier.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="itemID"></param>
		/// <param name="identifierDescription"></param>
		/// <param name="identifierValue"></param>
		/// <returns>Object of type IAItemIdentifier.</returns>
		public IAItemIdentifier IAItemIdentifierInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int itemID,
			string identifierDescription,
			string identifierValue)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IAItemIdentifierInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("ItemIdentifierID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemID),
					CustomSqlHelper.CreateInputParameter("IdentifierDescription", SqlDbType.NVarChar, 100, false, identifierDescription),
					CustomSqlHelper.CreateInputParameter("IdentifierValue", SqlDbType.NVarChar, 125, false, identifierValue), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<IAItemIdentifier> helper = new CustomSqlHelper<IAItemIdentifier>())
				{
					CustomGenericList<IAItemIdentifier> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						IAItemIdentifier o = list[0];
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
		/// Insert values into dbo.IAItemIdentifier. Returns an object of type IAItemIdentifier.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type IAItemIdentifier.</param>
		/// <returns>Object of type IAItemIdentifier.</returns>
		public IAItemIdentifier IAItemIdentifierInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			IAItemIdentifier value)
		{
			return IAItemIdentifierInsertAuto(sqlConnection, sqlTransaction, "BHLImport", value);
		}
		
		/// <summary>
		/// Insert values into dbo.IAItemIdentifier. Returns an object of type IAItemIdentifier.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type IAItemIdentifier.</param>
		/// <returns>Object of type IAItemIdentifier.</returns>
		public IAItemIdentifier IAItemIdentifierInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			IAItemIdentifier value)
		{
			return IAItemIdentifierInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.ItemID,
				value.IdentifierDescription,
				value.IdentifierValue);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from dbo.IAItemIdentifier by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="itemIdentifierID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool IAItemIdentifierDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int itemIdentifierID)
		{
			return IAItemIdentifierDeleteAuto( sqlConnection, sqlTransaction, "BHLImport", itemIdentifierID );
		}
		
		/// <summary>
		/// Delete values from dbo.IAItemIdentifier by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="itemIdentifierID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool IAItemIdentifierDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int itemIdentifierID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IAItemIdentifierDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("ItemIdentifierID", SqlDbType.Int, null, false, itemIdentifierID), 
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
		/// Update values in dbo.IAItemIdentifier. Returns an object of type IAItemIdentifier.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="itemIdentifierID"></param>
		/// <param name="itemID"></param>
		/// <param name="identifierDescription"></param>
		/// <param name="identifierValue"></param>
		/// <returns>Object of type IAItemIdentifier.</returns>
		public IAItemIdentifier IAItemIdentifierUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int itemIdentifierID,
			int itemID,
			string identifierDescription,
			string identifierValue)
		{
			return IAItemIdentifierUpdateAuto( sqlConnection, sqlTransaction, "BHLImport", itemIdentifierID, itemID, identifierDescription, identifierValue);
		}
		
		/// <summary>
		/// Update values in dbo.IAItemIdentifier. Returns an object of type IAItemIdentifier.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="itemIdentifierID"></param>
		/// <param name="itemID"></param>
		/// <param name="identifierDescription"></param>
		/// <param name="identifierValue"></param>
		/// <returns>Object of type IAItemIdentifier.</returns>
		public IAItemIdentifier IAItemIdentifierUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int itemIdentifierID,
			int itemID,
			string identifierDescription,
			string identifierValue)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IAItemIdentifierUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("ItemIdentifierID", SqlDbType.Int, null, false, itemIdentifierID),
					CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemID),
					CustomSqlHelper.CreateInputParameter("IdentifierDescription", SqlDbType.NVarChar, 100, false, identifierDescription),
					CustomSqlHelper.CreateInputParameter("IdentifierValue", SqlDbType.NVarChar, 125, false, identifierValue), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<IAItemIdentifier> helper = new CustomSqlHelper<IAItemIdentifier>())
				{
					CustomGenericList<IAItemIdentifier> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						IAItemIdentifier o = list[0];
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
		/// Update values in dbo.IAItemIdentifier. Returns an object of type IAItemIdentifier.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type IAItemIdentifier.</param>
		/// <returns>Object of type IAItemIdentifier.</returns>
		public IAItemIdentifier IAItemIdentifierUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			IAItemIdentifier value)
		{
			return IAItemIdentifierUpdateAuto(sqlConnection, sqlTransaction, "BHLImport", value );
		}
		
		/// <summary>
		/// Update values in dbo.IAItemIdentifier. Returns an object of type IAItemIdentifier.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type IAItemIdentifier.</param>
		/// <returns>Object of type IAItemIdentifier.</returns>
		public IAItemIdentifier IAItemIdentifierUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			IAItemIdentifier value)
		{
			return IAItemIdentifierUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.ItemIdentifierID,
				value.ItemID,
				value.IdentifierDescription,
				value.IdentifierValue);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage dbo.IAItemIdentifier object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.IAItemIdentifier.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type IAItemIdentifier.</param>
		/// <returns>Object of type CustomDataAccessStatus<IAItemIdentifier>.</returns>
		public CustomDataAccessStatus<IAItemIdentifier> IAItemIdentifierManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			IAItemIdentifier value  )
		{
			return IAItemIdentifierManageAuto( sqlConnection, sqlTransaction, "BHLImport", value  );
		}
		
		/// <summary>
		/// Manage dbo.IAItemIdentifier object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.IAItemIdentifier.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type IAItemIdentifier.</param>
		/// <returns>Object of type CustomDataAccessStatus<IAItemIdentifier>.</returns>
		public CustomDataAccessStatus<IAItemIdentifier> IAItemIdentifierManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			IAItemIdentifier value  )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				
				
				IAItemIdentifier returnValue = IAItemIdentifierInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.ItemID,
						value.IdentifierDescription,
						value.IdentifierValue);
				
				return new CustomDataAccessStatus<IAItemIdentifier>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (IAItemIdentifierDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.ItemIdentifierID))
				{
				return new CustomDataAccessStatus<IAItemIdentifier>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<IAItemIdentifier>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				
				IAItemIdentifier returnValue = IAItemIdentifierUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.ItemIdentifierID,
						value.ItemID,
						value.IdentifierDescription,
						value.IdentifierValue);
					
				return new CustomDataAccessStatus<IAItemIdentifier>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<IAItemIdentifier>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}

