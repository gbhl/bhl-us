
// Generated 6/19/2020 3:21:49 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class NameSourceGNFinderDAL is based upon dbo.NameSourceGNFinder.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DAL
// {
// 		public partial class NameSourceGNFinderDAL
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
	partial class NameSourceGNFinderDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from dbo.NameSourceGNFinder by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="dataSourceID"></param>
		/// <returns>Object of type NameSourceGNFinder.</returns>
		public NameSourceGNFinder NameSourceGNFinderSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int dataSourceID)
		{
			return NameSourceGNFinderSelectAuto(	sqlConnection, sqlTransaction, "BHL",	dataSourceID );
		}
			
		/// <summary>
		/// Select values from dbo.NameSourceGNFinder by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="dataSourceID"></param>
		/// <returns>Object of type NameSourceGNFinder.</returns>
		public NameSourceGNFinder NameSourceGNFinderSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int dataSourceID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("NameSourceGNFinderSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("DataSourceID", SqlDbType.Int, null, false, dataSourceID)))
			{
				using (CustomSqlHelper<NameSourceGNFinder> helper = new CustomSqlHelper<NameSourceGNFinder>())
				{
					CustomGenericList<NameSourceGNFinder> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						NameSourceGNFinder o = list[0];
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
		/// Select values from dbo.NameSourceGNFinder by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="dataSourceID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> NameSourceGNFinderSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int dataSourceID)
		{
			return NameSourceGNFinderSelectAutoRaw( sqlConnection, sqlTransaction, "BHL", dataSourceID );
		}
		
		/// <summary>
		/// Select values from dbo.NameSourceGNFinder by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="dataSourceID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> NameSourceGNFinderSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int dataSourceID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("NameSourceGNFinderSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("DataSourceID", SqlDbType.Int, null, false, dataSourceID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====

 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into dbo.NameSourceGNFinder.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="dataSourceID"></param>
		/// <param name="gNDataSourceName"></param>
		/// <param name="bHLIdentifierID"></param>
		/// <param name="gNDataSourceLabel"></param>
		/// <param name="gNDataSourceIcon"></param>
		/// <param name="gNDataSourceURLFormat"></param>
		/// <returns>Object of type NameSourceGNFinder.</returns>
		public NameSourceGNFinder NameSourceGNFinderInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int dataSourceID,
			string gNDataSourceName,
			int bHLIdentifierID,
			string gNDataSourceLabel,
			string gNDataSourceIcon,
			string gNDataSourceURLFormat)
		{
			return NameSourceGNFinderInsertAuto( sqlConnection, sqlTransaction, "BHL", dataSourceID, gNDataSourceName, bHLIdentifierID, gNDataSourceLabel, gNDataSourceIcon, gNDataSourceURLFormat );
		}
		
		/// <summary>
		/// Insert values into dbo.NameSourceGNFinder.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="dataSourceID"></param>
		/// <param name="gNDataSourceName"></param>
		/// <param name="bHLIdentifierID"></param>
		/// <param name="gNDataSourceLabel"></param>
		/// <param name="gNDataSourceIcon"></param>
		/// <param name="gNDataSourceURLFormat"></param>
		/// <returns>Object of type NameSourceGNFinder.</returns>
		public NameSourceGNFinder NameSourceGNFinderInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int dataSourceID,
			string gNDataSourceName,
			int bHLIdentifierID,
			string gNDataSourceLabel,
			string gNDataSourceIcon,
			string gNDataSourceURLFormat)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("NameSourceGNFinderInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("DataSourceID", SqlDbType.Int, null, false, dataSourceID),
					CustomSqlHelper.CreateInputParameter("GNDataSourceName", SqlDbType.NVarChar, 200, false, gNDataSourceName),
					CustomSqlHelper.CreateInputParameter("BHLIdentifierID", SqlDbType.Int, null, false, bHLIdentifierID),
					CustomSqlHelper.CreateInputParameter("GNDataSourceLabel", SqlDbType.NVarChar, 200, false, gNDataSourceLabel),
					CustomSqlHelper.CreateInputParameter("GNDataSourceIcon", SqlDbType.NVarChar, 300, false, gNDataSourceIcon),
					CustomSqlHelper.CreateInputParameter("GNDataSourceURLFormat", SqlDbType.NVarChar, 300, true, gNDataSourceURLFormat), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<NameSourceGNFinder> helper = new CustomSqlHelper<NameSourceGNFinder>())
				{
					CustomGenericList<NameSourceGNFinder> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						NameSourceGNFinder o = list[0];
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
		/// Insert values into dbo.NameSourceGNFinder. Returns an object of type NameSourceGNFinder.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type NameSourceGNFinder.</param>
		/// <returns>Object of type NameSourceGNFinder.</returns>
		public NameSourceGNFinder NameSourceGNFinderInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			NameSourceGNFinder value)
		{
			return NameSourceGNFinderInsertAuto(sqlConnection, sqlTransaction, "BHL", value);
		}
		
		/// <summary>
		/// Insert values into dbo.NameSourceGNFinder. Returns an object of type NameSourceGNFinder.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type NameSourceGNFinder.</param>
		/// <returns>Object of type NameSourceGNFinder.</returns>
		public NameSourceGNFinder NameSourceGNFinderInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			NameSourceGNFinder value)
		{
			return NameSourceGNFinderInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.DataSourceID,
				value.GNDataSourceName,
				value.BHLIdentifierID,
				value.GNDataSourceLabel,
				value.GNDataSourceIcon,
				value.GNDataSourceURLFormat);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from dbo.NameSourceGNFinder by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="dataSourceID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool NameSourceGNFinderDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int dataSourceID)
		{
			return NameSourceGNFinderDeleteAuto( sqlConnection, sqlTransaction, "BHL", dataSourceID );
		}
		
		/// <summary>
		/// Delete values from dbo.NameSourceGNFinder by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="dataSourceID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool NameSourceGNFinderDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int dataSourceID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("NameSourceGNFinderDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("DataSourceID", SqlDbType.Int, null, false, dataSourceID), 
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
		/// Update values in dbo.NameSourceGNFinder. Returns an object of type NameSourceGNFinder.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="dataSourceID"></param>
		/// <param name="gNDataSourceName"></param>
		/// <param name="bHLIdentifierID"></param>
		/// <param name="gNDataSourceLabel"></param>
		/// <param name="gNDataSourceIcon"></param>
		/// <param name="gNDataSourceURLFormat"></param>
		/// <returns>Object of type NameSourceGNFinder.</returns>
		public NameSourceGNFinder NameSourceGNFinderUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int dataSourceID,
			string gNDataSourceName,
			int bHLIdentifierID,
			string gNDataSourceLabel,
			string gNDataSourceIcon,
			string gNDataSourceURLFormat)
		{
			return NameSourceGNFinderUpdateAuto( sqlConnection, sqlTransaction, "BHL", dataSourceID, gNDataSourceName, bHLIdentifierID, gNDataSourceLabel, gNDataSourceIcon, gNDataSourceURLFormat);
		}
		
		/// <summary>
		/// Update values in dbo.NameSourceGNFinder. Returns an object of type NameSourceGNFinder.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="dataSourceID"></param>
		/// <param name="gNDataSourceName"></param>
		/// <param name="bHLIdentifierID"></param>
		/// <param name="gNDataSourceLabel"></param>
		/// <param name="gNDataSourceIcon"></param>
		/// <param name="gNDataSourceURLFormat"></param>
		/// <returns>Object of type NameSourceGNFinder.</returns>
		public NameSourceGNFinder NameSourceGNFinderUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int dataSourceID,
			string gNDataSourceName,
			int bHLIdentifierID,
			string gNDataSourceLabel,
			string gNDataSourceIcon,
			string gNDataSourceURLFormat)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("NameSourceGNFinderUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("DataSourceID", SqlDbType.Int, null, false, dataSourceID),
					CustomSqlHelper.CreateInputParameter("GNDataSourceName", SqlDbType.NVarChar, 200, false, gNDataSourceName),
					CustomSqlHelper.CreateInputParameter("BHLIdentifierID", SqlDbType.Int, null, false, bHLIdentifierID),
					CustomSqlHelper.CreateInputParameter("GNDataSourceLabel", SqlDbType.NVarChar, 200, false, gNDataSourceLabel),
					CustomSqlHelper.CreateInputParameter("GNDataSourceIcon", SqlDbType.NVarChar, 300, false, gNDataSourceIcon),
					CustomSqlHelper.CreateInputParameter("GNDataSourceURLFormat", SqlDbType.NVarChar, 300, true, gNDataSourceURLFormat), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<NameSourceGNFinder> helper = new CustomSqlHelper<NameSourceGNFinder>())
				{
					CustomGenericList<NameSourceGNFinder> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						NameSourceGNFinder o = list[0];
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
		/// Update values in dbo.NameSourceGNFinder. Returns an object of type NameSourceGNFinder.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type NameSourceGNFinder.</param>
		/// <returns>Object of type NameSourceGNFinder.</returns>
		public NameSourceGNFinder NameSourceGNFinderUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			NameSourceGNFinder value)
		{
			return NameSourceGNFinderUpdateAuto(sqlConnection, sqlTransaction, "BHL", value );
		}
		
		/// <summary>
		/// Update values in dbo.NameSourceGNFinder. Returns an object of type NameSourceGNFinder.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type NameSourceGNFinder.</param>
		/// <returns>Object of type NameSourceGNFinder.</returns>
		public NameSourceGNFinder NameSourceGNFinderUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			NameSourceGNFinder value)
		{
			return NameSourceGNFinderUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.DataSourceID,
				value.GNDataSourceName,
				value.BHLIdentifierID,
				value.GNDataSourceLabel,
				value.GNDataSourceIcon,
				value.GNDataSourceURLFormat);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage dbo.NameSourceGNFinder object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.NameSourceGNFinder.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type NameSourceGNFinder.</param>
		/// <returns>Object of type CustomDataAccessStatus<NameSourceGNFinder>.</returns>
		public CustomDataAccessStatus<NameSourceGNFinder> NameSourceGNFinderManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			NameSourceGNFinder value  )
		{
			return NameSourceGNFinderManageAuto( sqlConnection, sqlTransaction, "BHL", value  );
		}
		
		/// <summary>
		/// Manage dbo.NameSourceGNFinder object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.NameSourceGNFinder.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type NameSourceGNFinder.</param>
		/// <returns>Object of type CustomDataAccessStatus<NameSourceGNFinder>.</returns>
		public CustomDataAccessStatus<NameSourceGNFinder> NameSourceGNFinderManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			NameSourceGNFinder value  )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				
				
				NameSourceGNFinder returnValue = NameSourceGNFinderInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.DataSourceID,
						value.GNDataSourceName,
						value.BHLIdentifierID,
						value.GNDataSourceLabel,
						value.GNDataSourceIcon,
						value.GNDataSourceURLFormat);
				
				return new CustomDataAccessStatus<NameSourceGNFinder>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (NameSourceGNFinderDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.DataSourceID))
				{
				return new CustomDataAccessStatus<NameSourceGNFinder>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<NameSourceGNFinder>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				
				NameSourceGNFinder returnValue = NameSourceGNFinderUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.DataSourceID,
						value.GNDataSourceName,
						value.BHLIdentifierID,
						value.GNDataSourceLabel,
						value.GNDataSourceIcon,
						value.GNDataSourceURLFormat);
					
				return new CustomDataAccessStatus<NameSourceGNFinder>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<NameSourceGNFinder>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}

