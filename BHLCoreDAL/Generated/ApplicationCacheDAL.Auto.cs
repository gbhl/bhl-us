
// Generated 1/5/2021 3:24:49 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class ApplicationCacheDAL is based upon dbo.ApplicationCache.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DAL
// {
// 		public partial class ApplicationCacheDAL
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
using MOBOT.BHL.DataObjects;

#endregion using

namespace MOBOT.BHL.DAL
{
	partial class ApplicationCacheDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from dbo.ApplicationCache by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="cacheKey"></param>
		/// <returns>Object of type ApplicationCache.</returns>
		public ApplicationCache ApplicationCacheSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string cacheKey)
		{
			return ApplicationCacheSelectAuto(	sqlConnection, sqlTransaction, "BHL",	cacheKey );
		}
			
		/// <summary>
		/// Select values from dbo.ApplicationCache by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="cacheKey"></param>
		/// <returns>Object of type ApplicationCache.</returns>
		public ApplicationCache ApplicationCacheSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			string cacheKey )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("ApplicationCacheSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("CacheKey", SqlDbType.NVarChar, 100, false, cacheKey)))
			{
				using (CustomSqlHelper<ApplicationCache> helper = new CustomSqlHelper<ApplicationCache>())
				{
					List<ApplicationCache> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						ApplicationCache o = list[0];
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
		/// Select values from dbo.ApplicationCache by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="cacheKey"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> ApplicationCacheSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string cacheKey)
		{
			return ApplicationCacheSelectAutoRaw( sqlConnection, sqlTransaction, "BHL", cacheKey );
		}
		
		/// <summary>
		/// Select values from dbo.ApplicationCache by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="cacheKey"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> ApplicationCacheSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			string cacheKey)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("ApplicationCacheSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("CacheKey", SqlDbType.NVarChar, 100, false, cacheKey)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====

 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into dbo.ApplicationCache.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="cacheKey"></param>
		/// <param name="cacheData"></param>
		/// <param name="absoluteExpirationDate"></param>
		/// <param name="slidingExpirationDuration"></param>
		/// <param name="lastAccessDate"></param>
		/// <returns>Object of type ApplicationCache.</returns>
		public ApplicationCache ApplicationCacheInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string cacheKey,
			string cacheData,
			DateTime? absoluteExpirationDate,
			int? slidingExpirationDuration,
			DateTime lastAccessDate)
		{
			return ApplicationCacheInsertAuto( sqlConnection, sqlTransaction, "BHL", cacheKey, cacheData, absoluteExpirationDate, slidingExpirationDuration, lastAccessDate );
		}
		
		/// <summary>
		/// Insert values into dbo.ApplicationCache.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="cacheKey"></param>
		/// <param name="cacheData"></param>
		/// <param name="absoluteExpirationDate"></param>
		/// <param name="slidingExpirationDuration"></param>
		/// <param name="lastAccessDate"></param>
		/// <returns>Object of type ApplicationCache.</returns>
		public ApplicationCache ApplicationCacheInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			string cacheKey,
			string cacheData,
			DateTime? absoluteExpirationDate,
			int? slidingExpirationDuration,
			DateTime lastAccessDate)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("ApplicationCacheInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("CacheKey", SqlDbType.NVarChar, 100, false, cacheKey),
					CustomSqlHelper.CreateInputParameter("CacheData", SqlDbType.NVarChar, 1073741823, false, cacheData),
					CustomSqlHelper.CreateInputParameter("AbsoluteExpirationDate", SqlDbType.DateTime, null, true, absoluteExpirationDate),
					CustomSqlHelper.CreateInputParameter("SlidingExpirationDuration", SqlDbType.Int, null, true, slidingExpirationDuration),
					CustomSqlHelper.CreateInputParameter("LastAccessDate", SqlDbType.DateTime, null, false, lastAccessDate), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<ApplicationCache> helper = new CustomSqlHelper<ApplicationCache>())
				{
					List<ApplicationCache> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						ApplicationCache o = list[0];
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
		/// Insert values into dbo.ApplicationCache. Returns an object of type ApplicationCache.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type ApplicationCache.</param>
		/// <returns>Object of type ApplicationCache.</returns>
		public ApplicationCache ApplicationCacheInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			ApplicationCache value)
		{
			return ApplicationCacheInsertAuto(sqlConnection, sqlTransaction, "BHL", value);
		}
		
		/// <summary>
		/// Insert values into dbo.ApplicationCache. Returns an object of type ApplicationCache.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type ApplicationCache.</param>
		/// <returns>Object of type ApplicationCache.</returns>
		public ApplicationCache ApplicationCacheInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			ApplicationCache value)
		{
			return ApplicationCacheInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.CacheKey,
				value.CacheData,
				value.AbsoluteExpirationDate,
				value.SlidingExpirationDuration,
				value.LastAccessDate);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from dbo.ApplicationCache by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="cacheKey"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool ApplicationCacheDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string cacheKey)
		{
			return ApplicationCacheDeleteAuto( sqlConnection, sqlTransaction, "BHL", cacheKey );
		}
		
		/// <summary>
		/// Delete values from dbo.ApplicationCache by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="cacheKey"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool ApplicationCacheDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			string cacheKey)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("ApplicationCacheDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("CacheKey", SqlDbType.NVarChar, 100, false, cacheKey), 
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
		/// Update values in dbo.ApplicationCache. Returns an object of type ApplicationCache.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="cacheKey"></param>
		/// <param name="cacheData"></param>
		/// <param name="absoluteExpirationDate"></param>
		/// <param name="slidingExpirationDuration"></param>
		/// <param name="lastAccessDate"></param>
		/// <returns>Object of type ApplicationCache.</returns>
		public ApplicationCache ApplicationCacheUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string cacheKey,
			string cacheData,
			DateTime? absoluteExpirationDate,
			int? slidingExpirationDuration,
			DateTime lastAccessDate)
		{
			return ApplicationCacheUpdateAuto( sqlConnection, sqlTransaction, "BHL", cacheKey, cacheData, absoluteExpirationDate, slidingExpirationDuration, lastAccessDate);
		}
		
		/// <summary>
		/// Update values in dbo.ApplicationCache. Returns an object of type ApplicationCache.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="cacheKey"></param>
		/// <param name="cacheData"></param>
		/// <param name="absoluteExpirationDate"></param>
		/// <param name="slidingExpirationDuration"></param>
		/// <param name="lastAccessDate"></param>
		/// <returns>Object of type ApplicationCache.</returns>
		public ApplicationCache ApplicationCacheUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			string cacheKey,
			string cacheData,
			DateTime? absoluteExpirationDate,
			int? slidingExpirationDuration,
			DateTime lastAccessDate)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("ApplicationCacheUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("CacheKey", SqlDbType.NVarChar, 100, false, cacheKey),
					CustomSqlHelper.CreateInputParameter("CacheData", SqlDbType.NVarChar, 1073741823, false, cacheData),
					CustomSqlHelper.CreateInputParameter("AbsoluteExpirationDate", SqlDbType.DateTime, null, true, absoluteExpirationDate),
					CustomSqlHelper.CreateInputParameter("SlidingExpirationDuration", SqlDbType.Int, null, true, slidingExpirationDuration),
					CustomSqlHelper.CreateInputParameter("LastAccessDate", SqlDbType.DateTime, null, false, lastAccessDate), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<ApplicationCache> helper = new CustomSqlHelper<ApplicationCache>())
				{
					List<ApplicationCache> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						ApplicationCache o = list[0];
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
		/// Update values in dbo.ApplicationCache. Returns an object of type ApplicationCache.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type ApplicationCache.</param>
		/// <returns>Object of type ApplicationCache.</returns>
		public ApplicationCache ApplicationCacheUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			ApplicationCache value)
		{
			return ApplicationCacheUpdateAuto(sqlConnection, sqlTransaction, "BHL", value );
		}
		
		/// <summary>
		/// Update values in dbo.ApplicationCache. Returns an object of type ApplicationCache.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type ApplicationCache.</param>
		/// <returns>Object of type ApplicationCache.</returns>
		public ApplicationCache ApplicationCacheUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			ApplicationCache value)
		{
			return ApplicationCacheUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.CacheKey,
				value.CacheData,
				value.AbsoluteExpirationDate,
				value.SlidingExpirationDuration,
				value.LastAccessDate);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage dbo.ApplicationCache object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.ApplicationCache.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type ApplicationCache.</param>
		/// <returns>Object of type CustomDataAccessStatus<ApplicationCache>.</returns>
		public CustomDataAccessStatus<ApplicationCache> ApplicationCacheManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			ApplicationCache value  )
		{
			return ApplicationCacheManageAuto( sqlConnection, sqlTransaction, "BHL", value  );
		}
		
		/// <summary>
		/// Manage dbo.ApplicationCache object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.ApplicationCache.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type ApplicationCache.</param>
		/// <returns>Object of type CustomDataAccessStatus<ApplicationCache>.</returns>
		public CustomDataAccessStatus<ApplicationCache> ApplicationCacheManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			ApplicationCache value  )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				
				
				ApplicationCache returnValue = ApplicationCacheInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.CacheKey,
						value.CacheData,
						value.AbsoluteExpirationDate,
						value.SlidingExpirationDuration,
						value.LastAccessDate);
				
				return new CustomDataAccessStatus<ApplicationCache>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (ApplicationCacheDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.CacheKey))
				{
				return new CustomDataAccessStatus<ApplicationCache>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<ApplicationCache>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				
				ApplicationCache returnValue = ApplicationCacheUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.CacheKey,
						value.CacheData,
						value.AbsoluteExpirationDate,
						value.SlidingExpirationDuration,
						value.LastAccessDate);
					
				return new CustomDataAccessStatus<ApplicationCache>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<ApplicationCache>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}

