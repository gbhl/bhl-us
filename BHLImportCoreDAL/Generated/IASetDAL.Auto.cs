
// Generated 1/5/2021 2:16:04 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class IASetDAL is based upon dbo.IASet.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHLImport.DAL
// {
// 		public partial class IASetDAL
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
	partial class IASetDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from dbo.IASet by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="setID"></param>
		/// <returns>Object of type IASet.</returns>
		public IASet IASetSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int setID)
		{
			return IASetSelectAuto(	sqlConnection, sqlTransaction, "BHLImport",	setID );
		}
			
		/// <summary>
		/// Select values from dbo.IASet by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="setID"></param>
		/// <returns>Object of type IASet.</returns>
		public IASet IASetSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int setID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IASetSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("SetID", SqlDbType.Int, null, false, setID)))
			{
				using (CustomSqlHelper<IASet> helper = new CustomSqlHelper<IASet>())
				{
					List<IASet> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						IASet o = list[0];
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
		/// Select values from dbo.IASet by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="setID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> IASetSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int setID)
		{
			return IASetSelectAutoRaw( sqlConnection, sqlTransaction, "BHLImport", setID );
		}
		
		/// <summary>
		/// Select values from dbo.IASet by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="setID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> IASetSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int setID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IASetSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("SetID", SqlDbType.Int, null, false, setID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====

 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into dbo.IASet.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="setSpecification"></param>
		/// <param name="downloadAll"></param>
		/// <param name="lastDownloadDate"></param>
		/// <param name="lastFullDownloadDate"></param>
		/// <returns>Object of type IASet.</returns>
		public IASet IASetInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string setSpecification,
			bool downloadAll,
			DateTime? lastDownloadDate,
			DateTime? lastFullDownloadDate)
		{
			return IASetInsertAuto( sqlConnection, sqlTransaction, "BHLImport", setSpecification, downloadAll, lastDownloadDate, lastFullDownloadDate );
		}
		
		/// <summary>
		/// Insert values into dbo.IASet.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="setSpecification"></param>
		/// <param name="downloadAll"></param>
		/// <param name="lastDownloadDate"></param>
		/// <param name="lastFullDownloadDate"></param>
		/// <returns>Object of type IASet.</returns>
		public IASet IASetInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			string setSpecification,
			bool downloadAll,
			DateTime? lastDownloadDate,
			DateTime? lastFullDownloadDate)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IASetInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("SetID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("SetSpecification", SqlDbType.NVarChar, 200, false, setSpecification),
					CustomSqlHelper.CreateInputParameter("DownloadAll", SqlDbType.Bit, null, false, downloadAll),
					CustomSqlHelper.CreateInputParameter("LastDownloadDate", SqlDbType.DateTime, null, true, lastDownloadDate),
					CustomSqlHelper.CreateInputParameter("LastFullDownloadDate", SqlDbType.DateTime, null, true, lastFullDownloadDate), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<IASet> helper = new CustomSqlHelper<IASet>())
				{
					List<IASet> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						IASet o = list[0];
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
		/// Insert values into dbo.IASet. Returns an object of type IASet.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type IASet.</param>
		/// <returns>Object of type IASet.</returns>
		public IASet IASetInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			IASet value)
		{
			return IASetInsertAuto(sqlConnection, sqlTransaction, "BHLImport", value);
		}
		
		/// <summary>
		/// Insert values into dbo.IASet. Returns an object of type IASet.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type IASet.</param>
		/// <returns>Object of type IASet.</returns>
		public IASet IASetInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			IASet value)
		{
			return IASetInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.SetSpecification,
				value.DownloadAll,
				value.LastDownloadDate,
				value.LastFullDownloadDate);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from dbo.IASet by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="setID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool IASetDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int setID)
		{
			return IASetDeleteAuto( sqlConnection, sqlTransaction, "BHLImport", setID );
		}
		
		/// <summary>
		/// Delete values from dbo.IASet by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="setID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool IASetDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int setID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IASetDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("SetID", SqlDbType.Int, null, false, setID), 
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
		/// Update values in dbo.IASet. Returns an object of type IASet.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="setID"></param>
		/// <param name="setSpecification"></param>
		/// <param name="downloadAll"></param>
		/// <param name="lastDownloadDate"></param>
		/// <param name="lastFullDownloadDate"></param>
		/// <returns>Object of type IASet.</returns>
		public IASet IASetUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int setID,
			string setSpecification,
			bool downloadAll,
			DateTime? lastDownloadDate,
			DateTime? lastFullDownloadDate)
		{
			return IASetUpdateAuto( sqlConnection, sqlTransaction, "BHLImport", setID, setSpecification, downloadAll, lastDownloadDate, lastFullDownloadDate);
		}
		
		/// <summary>
		/// Update values in dbo.IASet. Returns an object of type IASet.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="setID"></param>
		/// <param name="setSpecification"></param>
		/// <param name="downloadAll"></param>
		/// <param name="lastDownloadDate"></param>
		/// <param name="lastFullDownloadDate"></param>
		/// <returns>Object of type IASet.</returns>
		public IASet IASetUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int setID,
			string setSpecification,
			bool downloadAll,
			DateTime? lastDownloadDate,
			DateTime? lastFullDownloadDate)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IASetUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("SetID", SqlDbType.Int, null, false, setID),
					CustomSqlHelper.CreateInputParameter("SetSpecification", SqlDbType.NVarChar, 200, false, setSpecification),
					CustomSqlHelper.CreateInputParameter("DownloadAll", SqlDbType.Bit, null, false, downloadAll),
					CustomSqlHelper.CreateInputParameter("LastDownloadDate", SqlDbType.DateTime, null, true, lastDownloadDate),
					CustomSqlHelper.CreateInputParameter("LastFullDownloadDate", SqlDbType.DateTime, null, true, lastFullDownloadDate), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<IASet> helper = new CustomSqlHelper<IASet>())
				{
					List<IASet> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						IASet o = list[0];
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
		/// Update values in dbo.IASet. Returns an object of type IASet.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type IASet.</param>
		/// <returns>Object of type IASet.</returns>
		public IASet IASetUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			IASet value)
		{
			return IASetUpdateAuto(sqlConnection, sqlTransaction, "BHLImport", value );
		}
		
		/// <summary>
		/// Update values in dbo.IASet. Returns an object of type IASet.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type IASet.</param>
		/// <returns>Object of type IASet.</returns>
		public IASet IASetUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			IASet value)
		{
			return IASetUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.SetID,
				value.SetSpecification,
				value.DownloadAll,
				value.LastDownloadDate,
				value.LastFullDownloadDate);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage dbo.IASet object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.IASet.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type IASet.</param>
		/// <returns>Object of type CustomDataAccessStatus<IASet>.</returns>
		public CustomDataAccessStatus<IASet> IASetManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			IASet value  )
		{
			return IASetManageAuto( sqlConnection, sqlTransaction, "BHLImport", value  );
		}
		
		/// <summary>
		/// Manage dbo.IASet object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.IASet.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type IASet.</param>
		/// <returns>Object of type CustomDataAccessStatus<IASet>.</returns>
		public CustomDataAccessStatus<IASet> IASetManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			IASet value  )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				
				
				IASet returnValue = IASetInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.SetSpecification,
						value.DownloadAll,
						value.LastDownloadDate,
						value.LastFullDownloadDate);
				
				return new CustomDataAccessStatus<IASet>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (IASetDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.SetID))
				{
				return new CustomDataAccessStatus<IASet>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<IASet>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				
				IASet returnValue = IASetUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.SetID,
						value.SetSpecification,
						value.DownloadAll,
						value.LastDownloadDate,
						value.LastFullDownloadDate);
					
				return new CustomDataAccessStatus<IASet>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<IASet>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}

