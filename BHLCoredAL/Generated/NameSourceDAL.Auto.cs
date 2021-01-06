
// Generated 1/5/2021 3:26:25 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class NameSourceDAL is based upon dbo.NameSource.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DAL
// {
// 		public partial class NameSourceDAL
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
	partial class NameSourceDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from dbo.NameSource by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="nameSourceID"></param>
		/// <returns>Object of type NameSource.</returns>
		public NameSource NameSourceSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int nameSourceID)
		{
			return NameSourceSelectAuto(	sqlConnection, sqlTransaction, "BHL",	nameSourceID );
		}
			
		/// <summary>
		/// Select values from dbo.NameSource by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="nameSourceID"></param>
		/// <returns>Object of type NameSource.</returns>
		public NameSource NameSourceSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int nameSourceID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("NameSourceSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("NameSourceID", SqlDbType.Int, null, false, nameSourceID)))
			{
				using (CustomSqlHelper<NameSource> helper = new CustomSqlHelper<NameSource>())
				{
					List<NameSource> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						NameSource o = list[0];
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
		/// Select values from dbo.NameSource by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="nameSourceID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> NameSourceSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int nameSourceID)
		{
			return NameSourceSelectAutoRaw( sqlConnection, sqlTransaction, "BHL", nameSourceID );
		}
		
		/// <summary>
		/// Select values from dbo.NameSource by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="nameSourceID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> NameSourceSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int nameSourceID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("NameSourceSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("NameSourceID", SqlDbType.Int, null, false, nameSourceID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====

 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into dbo.NameSource.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="sourceName"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type NameSource.</returns>
		public NameSource NameSourceInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string sourceName,
			int? creationUserID,
			int? lastModifiedUserID)
		{
			return NameSourceInsertAuto( sqlConnection, sqlTransaction, "BHL", sourceName, creationUserID, lastModifiedUserID );
		}
		
		/// <summary>
		/// Insert values into dbo.NameSource.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="sourceName"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type NameSource.</returns>
		public NameSource NameSourceInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			string sourceName,
			int? creationUserID,
			int? lastModifiedUserID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("NameSourceInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("NameSourceID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("SourceName", SqlDbType.NVarChar, 50, false, sourceName),
					CustomSqlHelper.CreateInputParameter("CreationUserID", SqlDbType.Int, null, true, creationUserID),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, true, lastModifiedUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<NameSource> helper = new CustomSqlHelper<NameSource>())
				{
					List<NameSource> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						NameSource o = list[0];
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
		/// Insert values into dbo.NameSource. Returns an object of type NameSource.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type NameSource.</param>
		/// <returns>Object of type NameSource.</returns>
		public NameSource NameSourceInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			NameSource value)
		{
			return NameSourceInsertAuto(sqlConnection, sqlTransaction, "BHL", value);
		}
		
		/// <summary>
		/// Insert values into dbo.NameSource. Returns an object of type NameSource.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type NameSource.</param>
		/// <returns>Object of type NameSource.</returns>
		public NameSource NameSourceInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			NameSource value)
		{
			return NameSourceInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.SourceName,
				value.CreationUserID,
				value.LastModifiedUserID);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from dbo.NameSource by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="nameSourceID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool NameSourceDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int nameSourceID)
		{
			return NameSourceDeleteAuto( sqlConnection, sqlTransaction, "BHL", nameSourceID );
		}
		
		/// <summary>
		/// Delete values from dbo.NameSource by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="nameSourceID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool NameSourceDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int nameSourceID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("NameSourceDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("NameSourceID", SqlDbType.Int, null, false, nameSourceID), 
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
		/// Update values in dbo.NameSource. Returns an object of type NameSource.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="nameSourceID"></param>
		/// <param name="sourceName"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type NameSource.</returns>
		public NameSource NameSourceUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int nameSourceID,
			string sourceName,
			int? lastModifiedUserID)
		{
			return NameSourceUpdateAuto( sqlConnection, sqlTransaction, "BHL", nameSourceID, sourceName, lastModifiedUserID);
		}
		
		/// <summary>
		/// Update values in dbo.NameSource. Returns an object of type NameSource.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="nameSourceID"></param>
		/// <param name="sourceName"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type NameSource.</returns>
		public NameSource NameSourceUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int nameSourceID,
			string sourceName,
			int? lastModifiedUserID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("NameSourceUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("NameSourceID", SqlDbType.Int, null, false, nameSourceID),
					CustomSqlHelper.CreateInputParameter("SourceName", SqlDbType.NVarChar, 50, false, sourceName),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, true, lastModifiedUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<NameSource> helper = new CustomSqlHelper<NameSource>())
				{
					List<NameSource> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						NameSource o = list[0];
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
		/// Update values in dbo.NameSource. Returns an object of type NameSource.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type NameSource.</param>
		/// <returns>Object of type NameSource.</returns>
		public NameSource NameSourceUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			NameSource value)
		{
			return NameSourceUpdateAuto(sqlConnection, sqlTransaction, "BHL", value );
		}
		
		/// <summary>
		/// Update values in dbo.NameSource. Returns an object of type NameSource.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type NameSource.</param>
		/// <returns>Object of type NameSource.</returns>
		public NameSource NameSourceUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			NameSource value)
		{
			return NameSourceUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.NameSourceID,
				value.SourceName,
				value.LastModifiedUserID);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage dbo.NameSource object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.NameSource.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type NameSource.</param>
		/// <returns>Object of type CustomDataAccessStatus<NameSource>.</returns>
		public CustomDataAccessStatus<NameSource> NameSourceManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			NameSource value , int userId )
		{
			return NameSourceManageAuto( sqlConnection, sqlTransaction, "BHL", value , userId );
		}
		
		/// <summary>
		/// Manage dbo.NameSource object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.NameSource.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type NameSource.</param>
		/// <returns>Object of type CustomDataAccessStatus<NameSource>.</returns>
		public CustomDataAccessStatus<NameSource> NameSourceManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			NameSource value , int userId )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				value.CreationUserID = userId;
				value.LastModifiedUserID = userId;
				NameSource returnValue = NameSourceInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.SourceName,
						value.CreationUserID,
						value.LastModifiedUserID);
				
				return new CustomDataAccessStatus<NameSource>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (NameSourceDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.NameSourceID))
				{
				return new CustomDataAccessStatus<NameSource>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<NameSource>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				value.LastModifiedUserID = userId;
				NameSource returnValue = NameSourceUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.NameSourceID,
						value.SourceName,
						value.LastModifiedUserID);
					
				return new CustomDataAccessStatus<NameSource>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<NameSource>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}

