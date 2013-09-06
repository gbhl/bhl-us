
// Generated 7/30/2010 2:09:29 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class TitleCollectionDAL is based upon TitleCollection.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DAL
// {
// 		public partial class TitleCollectionDAL
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
	partial class TitleCollectionDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from TitleCollection by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="titleCollectionID"></param>
		/// <returns>Object of type TitleCollection.</returns>
		public TitleCollection TitleCollectionSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int titleCollectionID)
		{
			return TitleCollectionSelectAuto(	sqlConnection, sqlTransaction, "BHL",	titleCollectionID );
		}
			
		/// <summary>
		/// Select values from TitleCollection by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="titleCollectionID"></param>
		/// <returns>Object of type TitleCollection.</returns>
		public TitleCollection TitleCollectionSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int titleCollectionID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleCollectionSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("TitleCollectionID", SqlDbType.Int, null, false, titleCollectionID)))
			{
				using (CustomSqlHelper<TitleCollection> helper = new CustomSqlHelper<TitleCollection>())
				{
					CustomGenericList<TitleCollection> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						TitleCollection o = list[0];
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
		/// Select values from TitleCollection by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="titleCollectionID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> TitleCollectionSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int titleCollectionID)
		{
			return TitleCollectionSelectAutoRaw( sqlConnection, sqlTransaction, "BHL", titleCollectionID );
		}
		
		/// <summary>
		/// Select values from TitleCollection by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="titleCollectionID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> TitleCollectionSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int titleCollectionID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleCollectionSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("TitleCollectionID", SqlDbType.Int, null, false, titleCollectionID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====
	
 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into TitleCollection.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="titleID"></param>
		/// <param name="collectionID"></param>
		/// <returns>Object of type TitleCollection.</returns>
		public TitleCollection TitleCollectionInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int titleID,
			int collectionID)
		{
			return TitleCollectionInsertAuto( sqlConnection, sqlTransaction, "BHL", titleID, collectionID );
		}
		
		/// <summary>
		/// Insert values into TitleCollection.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="titleID"></param>
		/// <param name="collectionID"></param>
		/// <returns>Object of type TitleCollection.</returns>
		public TitleCollection TitleCollectionInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int titleID,
			int collectionID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleCollectionInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("TitleCollectionID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("TitleID", SqlDbType.Int, null, false, titleID),
					CustomSqlHelper.CreateInputParameter("CollectionID", SqlDbType.Int, null, false, collectionID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<TitleCollection> helper = new CustomSqlHelper<TitleCollection>())
				{
					CustomGenericList<TitleCollection> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						TitleCollection o = list[0];
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
		/// Insert values into TitleCollection. Returns an object of type TitleCollection.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type TitleCollection.</param>
		/// <returns>Object of type TitleCollection.</returns>
		public TitleCollection TitleCollectionInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			TitleCollection value)
		{
			return TitleCollectionInsertAuto(sqlConnection, sqlTransaction, "BHL", value);
		}
		
		/// <summary>
		/// Insert values into TitleCollection. Returns an object of type TitleCollection.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type TitleCollection.</param>
		/// <returns>Object of type TitleCollection.</returns>
		public TitleCollection TitleCollectionInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			TitleCollection value)
		{
			return TitleCollectionInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.TitleID,
				value.CollectionID);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from TitleCollection by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="titleCollectionID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool TitleCollectionDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int titleCollectionID)
		{
			return TitleCollectionDeleteAuto( sqlConnection, sqlTransaction, "BHL", titleCollectionID );
		}
		
		/// <summary>
		/// Delete values from TitleCollection by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="titleCollectionID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool TitleCollectionDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int titleCollectionID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleCollectionDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("TitleCollectionID", SqlDbType.Int, null, false, titleCollectionID), 
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
		/// Update values in TitleCollection. Returns an object of type TitleCollection.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="titleCollectionID"></param>
		/// <param name="titleID"></param>
		/// <param name="collectionID"></param>
		/// <returns>Object of type TitleCollection.</returns>
		public TitleCollection TitleCollectionUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int titleCollectionID,
			int titleID,
			int collectionID)
		{
			return TitleCollectionUpdateAuto( sqlConnection, sqlTransaction, "BHL", titleCollectionID, titleID, collectionID);
		}
		
		/// <summary>
		/// Update values in TitleCollection. Returns an object of type TitleCollection.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="titleCollectionID"></param>
		/// <param name="titleID"></param>
		/// <param name="collectionID"></param>
		/// <returns>Object of type TitleCollection.</returns>
		public TitleCollection TitleCollectionUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int titleCollectionID,
			int titleID,
			int collectionID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleCollectionUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("TitleCollectionID", SqlDbType.Int, null, false, titleCollectionID),
					CustomSqlHelper.CreateInputParameter("TitleID", SqlDbType.Int, null, false, titleID),
					CustomSqlHelper.CreateInputParameter("CollectionID", SqlDbType.Int, null, false, collectionID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<TitleCollection> helper = new CustomSqlHelper<TitleCollection>())
				{
					CustomGenericList<TitleCollection> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						TitleCollection o = list[0];
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
		/// Update values in TitleCollection. Returns an object of type TitleCollection.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type TitleCollection.</param>
		/// <returns>Object of type TitleCollection.</returns>
		public TitleCollection TitleCollectionUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			TitleCollection value)
		{
			return TitleCollectionUpdateAuto(sqlConnection, sqlTransaction, "BHL", value );
		}
		
		/// <summary>
		/// Update values in TitleCollection. Returns an object of type TitleCollection.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type TitleCollection.</param>
		/// <returns>Object of type TitleCollection.</returns>
		public TitleCollection TitleCollectionUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			TitleCollection value)
		{
			return TitleCollectionUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.TitleCollectionID,
				value.TitleID,
				value.CollectionID);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage TitleCollection object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in TitleCollection.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type TitleCollection.</param>
		/// <returns>Object of type CustomDataAccessStatus<TitleCollection>.</returns>
		public CustomDataAccessStatus<TitleCollection> TitleCollectionManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			TitleCollection value  )
		{
			return TitleCollectionManageAuto( sqlConnection, sqlTransaction, "BHL", value  );
		}
		
		/// <summary>
		/// Manage TitleCollection object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in TitleCollection.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type TitleCollection.</param>
		/// <returns>Object of type CustomDataAccessStatus<TitleCollection>.</returns>
		public CustomDataAccessStatus<TitleCollection> TitleCollectionManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			TitleCollection value  )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				
				
				TitleCollection returnValue = TitleCollectionInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.TitleID,
						value.CollectionID);
				
				return new CustomDataAccessStatus<TitleCollection>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (TitleCollectionDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.TitleCollectionID))
				{
				return new CustomDataAccessStatus<TitleCollection>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<TitleCollection>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				
				TitleCollection returnValue = TitleCollectionUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.TitleCollectionID,
						value.TitleID,
						value.CollectionID);
					
				return new CustomDataAccessStatus<TitleCollection>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<TitleCollection>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}
// end of source generation
