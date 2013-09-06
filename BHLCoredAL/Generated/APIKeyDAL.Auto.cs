
// Generated 2/4/2010 3:26:40 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class APIKeyDAL is based upon APIKey.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DAL
// {
// 		public partial class APIKeyDAL
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
	partial class APIKeyDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from APIKey by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="apiKeyID"></param>
		/// <returns>Object of type APIKey.</returns>
		public APIKey APIKeySelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int apiKeyID)
		{
			return APIKeySelectAuto(	sqlConnection, sqlTransaction, "BHL",	apiKeyID );
		}
			
		/// <summary>
		/// Select values from APIKey by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="apiKeyID"></param>
		/// <returns>Object of type APIKey.</returns>
		public APIKey APIKeySelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int apiKeyID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("APIKeySelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("ApiKeyID", SqlDbType.Int, null, false, apiKeyID)))
			{
				using (CustomSqlHelper<APIKey> helper = new CustomSqlHelper<APIKey>())
				{
					CustomGenericList<APIKey> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						APIKey o = list[0];
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
		/// Select values from APIKey by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="apiKeyID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> APIKeySelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int apiKeyID)
		{
			return APIKeySelectAutoRaw( sqlConnection, sqlTransaction, "BHL", apiKeyID );
		}
		
		/// <summary>
		/// Select values from APIKey by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="apiKeyID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> APIKeySelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int apiKeyID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("APIKeySelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("ApiKeyID", SqlDbType.Int, null, false, apiKeyID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====
	
 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into APIKey.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="contactName"></param>
		/// <param name="emailAddress"></param>
		/// <param name="apiKeyValue"></param>
		/// <param name="isActive"></param>
		/// <returns>Object of type APIKey.</returns>
		public APIKey APIKeyInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string contactName,
			string emailAddress,
			Guid apiKeyValue,
			byte isActive)
		{
			return APIKeyInsertAuto( sqlConnection, sqlTransaction, "BHL", contactName, emailAddress, apiKeyValue, isActive );
		}
		
		/// <summary>
		/// Insert values into APIKey.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="contactName"></param>
		/// <param name="emailAddress"></param>
		/// <param name="apiKeyValue"></param>
		/// <param name="isActive"></param>
		/// <returns>Object of type APIKey.</returns>
		public APIKey APIKeyInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			string contactName,
			string emailAddress,
			Guid apiKeyValue,
			byte isActive)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("APIKeyInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("ApiKeyID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("ContactName", SqlDbType.NVarChar, 200, false, contactName),
					CustomSqlHelper.CreateInputParameter("EmailAddress", SqlDbType.NVarChar, 200, false, emailAddress),
					CustomSqlHelper.CreateInputParameter("ApiKeyValue", SqlDbType.UniqueIdentifier, null, false, apiKeyValue),
					CustomSqlHelper.CreateInputParameter("IsActive", SqlDbType.TinyInt, null, false, isActive), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<APIKey> helper = new CustomSqlHelper<APIKey>())
				{
					CustomGenericList<APIKey> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						APIKey o = list[0];
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
		/// Insert values into APIKey. Returns an object of type APIKey.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type APIKey.</param>
		/// <returns>Object of type APIKey.</returns>
		public APIKey APIKeyInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			APIKey value)
		{
			return APIKeyInsertAuto(sqlConnection, sqlTransaction, "BHL", value);
		}
		
		/// <summary>
		/// Insert values into APIKey. Returns an object of type APIKey.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type APIKey.</param>
		/// <returns>Object of type APIKey.</returns>
		public APIKey APIKeyInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			APIKey value)
		{
			return APIKeyInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.ContactName,
				value.EmailAddress,
				value.ApiKeyValue,
				value.IsActive);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from APIKey by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="apiKeyID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool APIKeyDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int apiKeyID)
		{
			return APIKeyDeleteAuto( sqlConnection, sqlTransaction, "BHL", apiKeyID );
		}
		
		/// <summary>
		/// Delete values from APIKey by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="apiKeyID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool APIKeyDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int apiKeyID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("APIKeyDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("ApiKeyID", SqlDbType.Int, null, false, apiKeyID), 
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
		/// Update values in APIKey. Returns an object of type APIKey.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="apiKeyID"></param>
		/// <param name="contactName"></param>
		/// <param name="emailAddress"></param>
		/// <param name="apiKeyValue"></param>
		/// <param name="isActive"></param>
		/// <returns>Object of type APIKey.</returns>
		public APIKey APIKeyUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int apiKeyID,
			string contactName,
			string emailAddress,
			Guid apiKeyValue,
			byte isActive)
		{
			return APIKeyUpdateAuto( sqlConnection, sqlTransaction, "BHL", apiKeyID, contactName, emailAddress, apiKeyValue, isActive);
		}
		
		/// <summary>
		/// Update values in APIKey. Returns an object of type APIKey.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="apiKeyID"></param>
		/// <param name="contactName"></param>
		/// <param name="emailAddress"></param>
		/// <param name="apiKeyValue"></param>
		/// <param name="isActive"></param>
		/// <returns>Object of type APIKey.</returns>
		public APIKey APIKeyUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int apiKeyID,
			string contactName,
			string emailAddress,
			Guid apiKeyValue,
			byte isActive)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("APIKeyUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("ApiKeyID", SqlDbType.Int, null, false, apiKeyID),
					CustomSqlHelper.CreateInputParameter("ContactName", SqlDbType.NVarChar, 200, false, contactName),
					CustomSqlHelper.CreateInputParameter("EmailAddress", SqlDbType.NVarChar, 200, false, emailAddress),
					CustomSqlHelper.CreateInputParameter("ApiKeyValue", SqlDbType.UniqueIdentifier, null, false, apiKeyValue),
					CustomSqlHelper.CreateInputParameter("IsActive", SqlDbType.TinyInt, null, false, isActive), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<APIKey> helper = new CustomSqlHelper<APIKey>())
				{
					CustomGenericList<APIKey> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						APIKey o = list[0];
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
		/// Update values in APIKey. Returns an object of type APIKey.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type APIKey.</param>
		/// <returns>Object of type APIKey.</returns>
		public APIKey APIKeyUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			APIKey value)
		{
			return APIKeyUpdateAuto(sqlConnection, sqlTransaction, "BHL", value );
		}
		
		/// <summary>
		/// Update values in APIKey. Returns an object of type APIKey.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type APIKey.</param>
		/// <returns>Object of type APIKey.</returns>
		public APIKey APIKeyUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			APIKey value)
		{
			return APIKeyUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.ApiKeyID,
				value.ContactName,
				value.EmailAddress,
				value.ApiKeyValue,
				value.IsActive);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage APIKey object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in APIKey.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type APIKey.</param>
		/// <returns>Object of type CustomDataAccessStatus<APIKey>.</returns>
		public CustomDataAccessStatus<APIKey> APIKeyManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			APIKey value  )
		{
			return APIKeyManageAuto( sqlConnection, sqlTransaction, "BHL", value  );
		}
		
		/// <summary>
		/// Manage APIKey object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in APIKey.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type APIKey.</param>
		/// <returns>Object of type CustomDataAccessStatus<APIKey>.</returns>
		public CustomDataAccessStatus<APIKey> APIKeyManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			APIKey value  )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				
				
				APIKey returnValue = APIKeyInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.ContactName,
						value.EmailAddress,
						value.ApiKeyValue,
						value.IsActive);
				
				return new CustomDataAccessStatus<APIKey>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (APIKeyDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.ApiKeyID))
				{
				return new CustomDataAccessStatus<APIKey>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<APIKey>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				
				APIKey returnValue = APIKeyUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.ApiKeyID,
						value.ContactName,
						value.EmailAddress,
						value.ApiKeyValue,
						value.IsActive);
					
				return new CustomDataAccessStatus<APIKey>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<APIKey>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}
// end of source generation
