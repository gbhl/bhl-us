
// Generated 11/12/2008 3:38:13 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class CollectionDAL is based upon Collection.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.IAAnalysis.DAL
// {
// 		public partial class CollectionDAL
//		{
//		}
// }

#endregion How To Implement

#region using

using System;
using System.Data;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.IAAnalysis.DataObjects;

#endregion using

namespace MOBOT.IAAnalysis.DAL
{
	partial class CollectionDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from Collection by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="collectionID"></param>
		/// <returns>Object of type Collection.</returns>
		public Collection CollectionSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int collectionID)
		{
			return CollectionSelectAuto(	sqlConnection, sqlTransaction, "IAAnalysis",	collectionID );
		}
			
		/// <summary>
		/// Select values from Collection by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="collectionID"></param>
		/// <returns>Object of type Collection.</returns>
		public Collection CollectionSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int collectionID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("CollectionSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("CollectionID", SqlDbType.Int, null, false, collectionID)))
			{
				using (CustomSqlHelper<Collection> helper = new CustomSqlHelper<Collection>())
				{
					CustomGenericList<Collection> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						Collection o = list[0];
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
		/// Select values from Collection by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="collectionID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> CollectionSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int collectionID)
		{
			return CollectionSelectAutoRaw( sqlConnection, sqlTransaction, "IAAnalysis", collectionID );
		}
		
		/// <summary>
		/// Select values from Collection by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="collectionID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> CollectionSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int collectionID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("CollectionSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("CollectionID", SqlDbType.Int, null, false, collectionID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====
	
 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into Collection.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="collectionName"></param>
		/// <returns>Object of type Collection.</returns>
		public Collection CollectionInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string collectionName)
		{
			return CollectionInsertAuto( sqlConnection, sqlTransaction, "IAAnalysis", collectionName );
		}
		
		/// <summary>
		/// Insert values into Collection.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="collectionName"></param>
		/// <returns>Object of type Collection.</returns>
		public Collection CollectionInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			string collectionName)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("CollectionInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("CollectionID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("CollectionName", SqlDbType.NVarChar, 200, false, collectionName), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<Collection> helper = new CustomSqlHelper<Collection>())
				{
					CustomGenericList<Collection> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						Collection o = list[0];
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
		/// Insert values into Collection. Returns an object of type Collection.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type Collection.</param>
		/// <returns>Object of type Collection.</returns>
		public Collection CollectionInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			Collection value)
		{
			return CollectionInsertAuto(sqlConnection, sqlTransaction, "IAAnalysis", value);
		}
		
		/// <summary>
		/// Insert values into Collection. Returns an object of type Collection.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type Collection.</param>
		/// <returns>Object of type Collection.</returns>
		public Collection CollectionInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			Collection value)
		{
			return CollectionInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.CollectionName);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from Collection by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="collectionID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool CollectionDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int collectionID)
		{
			return CollectionDeleteAuto( sqlConnection, sqlTransaction, "IAAnalysis", collectionID );
		}
		
		/// <summary>
		/// Delete values from Collection by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="collectionID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool CollectionDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int collectionID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("CollectionDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("CollectionID", SqlDbType.Int, null, false, collectionID), 
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
		/// Update values in Collection. Returns an object of type Collection.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="collectionID"></param>
		/// <param name="collectionName"></param>
		/// <returns>Object of type Collection.</returns>
		public Collection CollectionUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int collectionID,
			string collectionName)
		{
			return CollectionUpdateAuto( sqlConnection, sqlTransaction, "IAAnalysis", collectionID, collectionName);
		}
		
		/// <summary>
		/// Update values in Collection. Returns an object of type Collection.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="collectionID"></param>
		/// <param name="collectionName"></param>
		/// <returns>Object of type Collection.</returns>
		public Collection CollectionUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int collectionID,
			string collectionName)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("CollectionUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("CollectionID", SqlDbType.Int, null, false, collectionID),
					CustomSqlHelper.CreateInputParameter("CollectionName", SqlDbType.NVarChar, 200, false, collectionName), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<Collection> helper = new CustomSqlHelper<Collection>())
				{
					CustomGenericList<Collection> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						Collection o = list[0];
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
		/// Update values in Collection. Returns an object of type Collection.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type Collection.</param>
		/// <returns>Object of type Collection.</returns>
		public Collection CollectionUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			Collection value)
		{
			return CollectionUpdateAuto(sqlConnection, sqlTransaction, "IAAnalysis", value );
		}
		
		/// <summary>
		/// Update values in Collection. Returns an object of type Collection.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type Collection.</param>
		/// <returns>Object of type Collection.</returns>
		public Collection CollectionUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			Collection value)
		{
			return CollectionUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.CollectionID,
				value.CollectionName);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage Collection object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in Collection.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type Collection.</param>
		/// <returns>Object of type CustomDataAccessStatus<Collection>.</returns>
		public CustomDataAccessStatus<Collection> CollectionManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			Collection value  )
		{
			return CollectionManageAuto( sqlConnection, sqlTransaction, "IAAnalysis", value  );
		}
		
		/// <summary>
		/// Manage Collection object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in Collection.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type Collection.</param>
		/// <returns>Object of type CustomDataAccessStatus<Collection>.</returns>
		public CustomDataAccessStatus<Collection> CollectionManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			Collection value  )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				
				
				Collection returnValue = CollectionInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.CollectionName);
				
				return new CustomDataAccessStatus<Collection>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (CollectionDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.CollectionID))
				{
				return new CustomDataAccessStatus<Collection>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<Collection>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				
				Collection returnValue = CollectionUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.CollectionID,
						value.CollectionName);
					
				return new CustomDataAccessStatus<Collection>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<Collection>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}
// end of source generation
