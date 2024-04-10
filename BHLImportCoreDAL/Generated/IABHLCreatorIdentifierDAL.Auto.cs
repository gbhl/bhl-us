
// Generated 3/4/2024 11:54:37 AM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class IABHLCreatorIdentifierDAL is based upon dbo.IABHLCreatorIdentifier.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHLImport.DAL
// {
// 		public partial class IABHLCreatorIdentifierDAL
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
	partial class IABHLCreatorIdentifierDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from dbo.IABHLCreatorIdentifier by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="bHLCreatorIdentifierID"></param>
		/// <returns>Object of type IABHLCreatorIdentifier.</returns>
		public IABHLCreatorIdentifier IABHLCreatorIdentifierSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int bHLCreatorIdentifierID)
		{
			return IABHLCreatorIdentifierSelectAuto(	sqlConnection, sqlTransaction, "BHLImport",	bHLCreatorIdentifierID );
		}
			
		/// <summary>
		/// Select values from dbo.IABHLCreatorIdentifier by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="bHLCreatorIdentifierID"></param>
		/// <returns>Object of type IABHLCreatorIdentifier.</returns>
		public IABHLCreatorIdentifier IABHLCreatorIdentifierSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int bHLCreatorIdentifierID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IABHLCreatorIdentifierSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("BHLCreatorIdentifierID", SqlDbType.Int, null, false, bHLCreatorIdentifierID)))
			{
				using (CustomSqlHelper<IABHLCreatorIdentifier> helper = new CustomSqlHelper<IABHLCreatorIdentifier>())
				{
					List<IABHLCreatorIdentifier> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						IABHLCreatorIdentifier o = list[0];
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
		/// Select values from dbo.IABHLCreatorIdentifier by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="bHLCreatorIdentifierID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> IABHLCreatorIdentifierSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int bHLCreatorIdentifierID)
		{
			return IABHLCreatorIdentifierSelectAutoRaw( sqlConnection, sqlTransaction, "BHLImport", bHLCreatorIdentifierID );
		}
		
		/// <summary>
		/// Select values from dbo.IABHLCreatorIdentifier by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="bHLCreatorIdentifierID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> IABHLCreatorIdentifierSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int bHLCreatorIdentifierID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IABHLCreatorIdentifierSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("BHLCreatorIdentifierID", SqlDbType.Int, null, false, bHLCreatorIdentifierID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====

 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into dbo.IABHLCreatorIdentifier.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="bHLCreatorID"></param>
		/// <param name="type"></param>
		/// <param name="value"></param>
		/// <returns>Object of type IABHLCreatorIdentifier.</returns>
		public IABHLCreatorIdentifier IABHLCreatorIdentifierInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int bHLCreatorID,
			string type,
			string value)
		{
			return IABHLCreatorIdentifierInsertAuto( sqlConnection, sqlTransaction, "BHLImport", bHLCreatorID, type, value );
		}
		
		/// <summary>
		/// Insert values into dbo.IABHLCreatorIdentifier.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="bHLCreatorID"></param>
		/// <param name="type"></param>
		/// <param name="value"></param>
		/// <returns>Object of type IABHLCreatorIdentifier.</returns>
		public IABHLCreatorIdentifier IABHLCreatorIdentifierInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int bHLCreatorID,
			string type,
			string value)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IABHLCreatorIdentifierInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("BHLCreatorIdentifierID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("BHLCreatorID", SqlDbType.Int, null, false, bHLCreatorID),
					CustomSqlHelper.CreateInputParameter("Type", SqlDbType.NVarChar, 40, false, type),
					CustomSqlHelper.CreateInputParameter("Value", SqlDbType.NVarChar, 125, false, value), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<IABHLCreatorIdentifier> helper = new CustomSqlHelper<IABHLCreatorIdentifier>())
				{
					List<IABHLCreatorIdentifier> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						IABHLCreatorIdentifier o = list[0];
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
		/// Insert values into dbo.IABHLCreatorIdentifier. Returns an object of type IABHLCreatorIdentifier.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type IABHLCreatorIdentifier.</param>
		/// <returns>Object of type IABHLCreatorIdentifier.</returns>
		public IABHLCreatorIdentifier IABHLCreatorIdentifierInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			IABHLCreatorIdentifier value)
		{
			return IABHLCreatorIdentifierInsertAuto(sqlConnection, sqlTransaction, "BHLImport", value);
		}
		
		/// <summary>
		/// Insert values into dbo.IABHLCreatorIdentifier. Returns an object of type IABHLCreatorIdentifier.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type IABHLCreatorIdentifier.</param>
		/// <returns>Object of type IABHLCreatorIdentifier.</returns>
		public IABHLCreatorIdentifier IABHLCreatorIdentifierInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			IABHLCreatorIdentifier value)
		{
			return IABHLCreatorIdentifierInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.BHLCreatorID,
				value.Type,
				value.Value);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from dbo.IABHLCreatorIdentifier by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="bHLCreatorIdentifierID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool IABHLCreatorIdentifierDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int bHLCreatorIdentifierID)
		{
			return IABHLCreatorIdentifierDeleteAuto( sqlConnection, sqlTransaction, "BHLImport", bHLCreatorIdentifierID );
		}
		
		/// <summary>
		/// Delete values from dbo.IABHLCreatorIdentifier by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="bHLCreatorIdentifierID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool IABHLCreatorIdentifierDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int bHLCreatorIdentifierID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IABHLCreatorIdentifierDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("BHLCreatorIdentifierID", SqlDbType.Int, null, false, bHLCreatorIdentifierID), 
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
		/// Update values in dbo.IABHLCreatorIdentifier. Returns an object of type IABHLCreatorIdentifier.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="bHLCreatorIdentifierID"></param>
		/// <param name="bHLCreatorID"></param>
		/// <param name="type"></param>
		/// <param name="value"></param>
		/// <returns>Object of type IABHLCreatorIdentifier.</returns>
		public IABHLCreatorIdentifier IABHLCreatorIdentifierUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int bHLCreatorIdentifierID,
			int bHLCreatorID,
			string type,
			string value)
		{
			return IABHLCreatorIdentifierUpdateAuto( sqlConnection, sqlTransaction, "BHLImport", bHLCreatorIdentifierID, bHLCreatorID, type, value);
		}
		
		/// <summary>
		/// Update values in dbo.IABHLCreatorIdentifier. Returns an object of type IABHLCreatorIdentifier.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="bHLCreatorIdentifierID"></param>
		/// <param name="bHLCreatorID"></param>
		/// <param name="type"></param>
		/// <param name="value"></param>
		/// <returns>Object of type IABHLCreatorIdentifier.</returns>
		public IABHLCreatorIdentifier IABHLCreatorIdentifierUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int bHLCreatorIdentifierID,
			int bHLCreatorID,
			string type,
			string value)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IABHLCreatorIdentifierUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("BHLCreatorIdentifierID", SqlDbType.Int, null, false, bHLCreatorIdentifierID),
					CustomSqlHelper.CreateInputParameter("BHLCreatorID", SqlDbType.Int, null, false, bHLCreatorID),
					CustomSqlHelper.CreateInputParameter("Type", SqlDbType.NVarChar, 40, false, type),
					CustomSqlHelper.CreateInputParameter("Value", SqlDbType.NVarChar, 125, false, value), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<IABHLCreatorIdentifier> helper = new CustomSqlHelper<IABHLCreatorIdentifier>())
				{
					List<IABHLCreatorIdentifier> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						IABHLCreatorIdentifier o = list[0];
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
		/// Update values in dbo.IABHLCreatorIdentifier. Returns an object of type IABHLCreatorIdentifier.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type IABHLCreatorIdentifier.</param>
		/// <returns>Object of type IABHLCreatorIdentifier.</returns>
		public IABHLCreatorIdentifier IABHLCreatorIdentifierUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			IABHLCreatorIdentifier value)
		{
			return IABHLCreatorIdentifierUpdateAuto(sqlConnection, sqlTransaction, "BHLImport", value );
		}
		
		/// <summary>
		/// Update values in dbo.IABHLCreatorIdentifier. Returns an object of type IABHLCreatorIdentifier.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type IABHLCreatorIdentifier.</param>
		/// <returns>Object of type IABHLCreatorIdentifier.</returns>
		public IABHLCreatorIdentifier IABHLCreatorIdentifierUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			IABHLCreatorIdentifier value)
		{
			return IABHLCreatorIdentifierUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.BHLCreatorIdentifierID,
				value.BHLCreatorID,
				value.Type,
				value.Value);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage dbo.IABHLCreatorIdentifier object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.IABHLCreatorIdentifier.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type IABHLCreatorIdentifier.</param>
		/// <returns>Object of type CustomDataAccessStatus<IABHLCreatorIdentifier>.</returns>
		public CustomDataAccessStatus<IABHLCreatorIdentifier> IABHLCreatorIdentifierManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			IABHLCreatorIdentifier value  )
		{
			return IABHLCreatorIdentifierManageAuto( sqlConnection, sqlTransaction, "BHLImport", value  );
		}
		
		/// <summary>
		/// Manage dbo.IABHLCreatorIdentifier object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.IABHLCreatorIdentifier.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type IABHLCreatorIdentifier.</param>
		/// <returns>Object of type CustomDataAccessStatus<IABHLCreatorIdentifier>.</returns>
		public CustomDataAccessStatus<IABHLCreatorIdentifier> IABHLCreatorIdentifierManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			IABHLCreatorIdentifier value  )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				
				
				IABHLCreatorIdentifier returnValue = IABHLCreatorIdentifierInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.BHLCreatorID,
						value.Type,
						value.Value);
				
				return new CustomDataAccessStatus<IABHLCreatorIdentifier>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (IABHLCreatorIdentifierDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.BHLCreatorIdentifierID))
				{
				return new CustomDataAccessStatus<IABHLCreatorIdentifier>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<IABHLCreatorIdentifier>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				
				IABHLCreatorIdentifier returnValue = IABHLCreatorIdentifierUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.BHLCreatorIdentifierID,
						value.BHLCreatorID,
						value.Type,
						value.Value);
					
				return new CustomDataAccessStatus<IABHLCreatorIdentifier>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<IABHLCreatorIdentifier>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}

