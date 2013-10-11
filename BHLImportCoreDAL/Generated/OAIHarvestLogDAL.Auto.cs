
// Generated 10/11/2013 1:38:31 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class OAIHarvestLogDAL is based upon OAIHarvestLog.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHLImport.DAL
// {
// 		public partial class OAIHarvestLogDAL
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
	partial class OAIHarvestLogDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from OAIHarvestLog by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="harvestLogID"></param>
		/// <returns>Object of type OAIHarvestLog.</returns>
		public OAIHarvestLog OAIHarvestLogSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int harvestLogID)
		{
			return OAIHarvestLogSelectAuto(	sqlConnection, sqlTransaction, "BHLImport",	harvestLogID );
		}
			
		/// <summary>
		/// Select values from OAIHarvestLog by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="harvestLogID"></param>
		/// <returns>Object of type OAIHarvestLog.</returns>
		public OAIHarvestLog OAIHarvestLogSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int harvestLogID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("OAIHarvestLogSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("HarvestLogID", SqlDbType.Int, null, false, harvestLogID)))
			{
				using (CustomSqlHelper<OAIHarvestLog> helper = new CustomSqlHelper<OAIHarvestLog>())
				{
					CustomGenericList<OAIHarvestLog> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						OAIHarvestLog o = list[0];
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
		/// Select values from OAIHarvestLog by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="harvestLogID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> OAIHarvestLogSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int harvestLogID)
		{
			return OAIHarvestLogSelectAutoRaw( sqlConnection, sqlTransaction, "BHLImport", harvestLogID );
		}
		
		/// <summary>
		/// Select values from OAIHarvestLog by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="harvestLogID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> OAIHarvestLogSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int harvestLogID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("OAIHarvestLogSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("HarvestLogID", SqlDbType.Int, null, false, harvestLogID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====
	
 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into OAIHarvestLog.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="harvestSetID"></param>
		/// <param name="harvestStartDateTime"></param>
		/// <param name="fromDateTime"></param>
		/// <param name="untilDateTime"></param>
		/// <param name="responseDateTime"></param>
		/// <param name="result"></param>
		/// <returns>Object of type OAIHarvestLog.</returns>
		public OAIHarvestLog OAIHarvestLogInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int harvestSetID,
			DateTime? harvestStartDateTime,
			DateTime? fromDateTime,
			DateTime? untilDateTime,
			DateTime? responseDateTime,
			string result)
		{
			return OAIHarvestLogInsertAuto( sqlConnection, sqlTransaction, "BHLImport", harvestSetID, harvestStartDateTime, fromDateTime, untilDateTime, responseDateTime, result );
		}
		
		/// <summary>
		/// Insert values into OAIHarvestLog.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="harvestSetID"></param>
		/// <param name="harvestStartDateTime"></param>
		/// <param name="fromDateTime"></param>
		/// <param name="untilDateTime"></param>
		/// <param name="responseDateTime"></param>
		/// <param name="result"></param>
		/// <returns>Object of type OAIHarvestLog.</returns>
		public OAIHarvestLog OAIHarvestLogInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int harvestSetID,
			DateTime? harvestStartDateTime,
			DateTime? fromDateTime,
			DateTime? untilDateTime,
			DateTime? responseDateTime,
			string result)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("OAIHarvestLogInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("HarvestLogID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("HarvestSetID", SqlDbType.Int, null, false, harvestSetID),
					CustomSqlHelper.CreateInputParameter("HarvestStartDateTime", SqlDbType.DateTime, null, true, harvestStartDateTime),
					CustomSqlHelper.CreateInputParameter("FromDateTime", SqlDbType.DateTime, null, true, fromDateTime),
					CustomSqlHelper.CreateInputParameter("UntilDateTime", SqlDbType.DateTime, null, true, untilDateTime),
					CustomSqlHelper.CreateInputParameter("ResponseDateTime", SqlDbType.DateTime, null, true, responseDateTime),
					CustomSqlHelper.CreateInputParameter("Result", SqlDbType.NVarChar, 200, false, result), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<OAIHarvestLog> helper = new CustomSqlHelper<OAIHarvestLog>())
				{
					CustomGenericList<OAIHarvestLog> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						OAIHarvestLog o = list[0];
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
		/// Insert values into OAIHarvestLog. Returns an object of type OAIHarvestLog.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type OAIHarvestLog.</param>
		/// <returns>Object of type OAIHarvestLog.</returns>
		public OAIHarvestLog OAIHarvestLogInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			OAIHarvestLog value)
		{
			return OAIHarvestLogInsertAuto(sqlConnection, sqlTransaction, "BHLImport", value);
		}
		
		/// <summary>
		/// Insert values into OAIHarvestLog. Returns an object of type OAIHarvestLog.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type OAIHarvestLog.</param>
		/// <returns>Object of type OAIHarvestLog.</returns>
		public OAIHarvestLog OAIHarvestLogInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			OAIHarvestLog value)
		{
			return OAIHarvestLogInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.HarvestSetID,
				value.HarvestStartDateTime,
				value.FromDateTime,
				value.UntilDateTime,
				value.ResponseDateTime,
				value.Result);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from OAIHarvestLog by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="harvestLogID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool OAIHarvestLogDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int harvestLogID)
		{
			return OAIHarvestLogDeleteAuto( sqlConnection, sqlTransaction, "BHLImport", harvestLogID );
		}
		
		/// <summary>
		/// Delete values from OAIHarvestLog by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="harvestLogID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool OAIHarvestLogDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int harvestLogID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("OAIHarvestLogDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("HarvestLogID", SqlDbType.Int, null, false, harvestLogID), 
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
		/// Update values in OAIHarvestLog. Returns an object of type OAIHarvestLog.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="harvestLogID"></param>
		/// <param name="harvestSetID"></param>
		/// <param name="harvestStartDateTime"></param>
		/// <param name="fromDateTime"></param>
		/// <param name="untilDateTime"></param>
		/// <param name="responseDateTime"></param>
		/// <param name="result"></param>
		/// <returns>Object of type OAIHarvestLog.</returns>
		public OAIHarvestLog OAIHarvestLogUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int harvestLogID,
			int harvestSetID,
			DateTime? harvestStartDateTime,
			DateTime? fromDateTime,
			DateTime? untilDateTime,
			DateTime? responseDateTime,
			string result)
		{
			return OAIHarvestLogUpdateAuto( sqlConnection, sqlTransaction, "BHLImport", harvestLogID, harvestSetID, harvestStartDateTime, fromDateTime, untilDateTime, responseDateTime, result);
		}
		
		/// <summary>
		/// Update values in OAIHarvestLog. Returns an object of type OAIHarvestLog.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="harvestLogID"></param>
		/// <param name="harvestSetID"></param>
		/// <param name="harvestStartDateTime"></param>
		/// <param name="fromDateTime"></param>
		/// <param name="untilDateTime"></param>
		/// <param name="responseDateTime"></param>
		/// <param name="result"></param>
		/// <returns>Object of type OAIHarvestLog.</returns>
		public OAIHarvestLog OAIHarvestLogUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int harvestLogID,
			int harvestSetID,
			DateTime? harvestStartDateTime,
			DateTime? fromDateTime,
			DateTime? untilDateTime,
			DateTime? responseDateTime,
			string result)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("OAIHarvestLogUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("HarvestLogID", SqlDbType.Int, null, false, harvestLogID),
					CustomSqlHelper.CreateInputParameter("HarvestSetID", SqlDbType.Int, null, false, harvestSetID),
					CustomSqlHelper.CreateInputParameter("HarvestStartDateTime", SqlDbType.DateTime, null, true, harvestStartDateTime),
					CustomSqlHelper.CreateInputParameter("FromDateTime", SqlDbType.DateTime, null, true, fromDateTime),
					CustomSqlHelper.CreateInputParameter("UntilDateTime", SqlDbType.DateTime, null, true, untilDateTime),
					CustomSqlHelper.CreateInputParameter("ResponseDateTime", SqlDbType.DateTime, null, true, responseDateTime),
					CustomSqlHelper.CreateInputParameter("Result", SqlDbType.NVarChar, 200, false, result), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<OAIHarvestLog> helper = new CustomSqlHelper<OAIHarvestLog>())
				{
					CustomGenericList<OAIHarvestLog> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						OAIHarvestLog o = list[0];
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
		/// Update values in OAIHarvestLog. Returns an object of type OAIHarvestLog.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type OAIHarvestLog.</param>
		/// <returns>Object of type OAIHarvestLog.</returns>
		public OAIHarvestLog OAIHarvestLogUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			OAIHarvestLog value)
		{
			return OAIHarvestLogUpdateAuto(sqlConnection, sqlTransaction, "BHLImport", value );
		}
		
		/// <summary>
		/// Update values in OAIHarvestLog. Returns an object of type OAIHarvestLog.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type OAIHarvestLog.</param>
		/// <returns>Object of type OAIHarvestLog.</returns>
		public OAIHarvestLog OAIHarvestLogUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			OAIHarvestLog value)
		{
			return OAIHarvestLogUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.HarvestLogID,
				value.HarvestSetID,
				value.HarvestStartDateTime,
				value.FromDateTime,
				value.UntilDateTime,
				value.ResponseDateTime,
				value.Result);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage OAIHarvestLog object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in OAIHarvestLog.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type OAIHarvestLog.</param>
		/// <returns>Object of type CustomDataAccessStatus<OAIHarvestLog>.</returns>
		public CustomDataAccessStatus<OAIHarvestLog> OAIHarvestLogManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			OAIHarvestLog value  )
		{
			return OAIHarvestLogManageAuto( sqlConnection, sqlTransaction, "BHLImport", value  );
		}
		
		/// <summary>
		/// Manage OAIHarvestLog object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in OAIHarvestLog.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type OAIHarvestLog.</param>
		/// <returns>Object of type CustomDataAccessStatus<OAIHarvestLog>.</returns>
		public CustomDataAccessStatus<OAIHarvestLog> OAIHarvestLogManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			OAIHarvestLog value  )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				
				
				OAIHarvestLog returnValue = OAIHarvestLogInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.HarvestSetID,
						value.HarvestStartDateTime,
						value.FromDateTime,
						value.UntilDateTime,
						value.ResponseDateTime,
						value.Result);
				
				return new CustomDataAccessStatus<OAIHarvestLog>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (OAIHarvestLogDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.HarvestLogID))
				{
				return new CustomDataAccessStatus<OAIHarvestLog>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<OAIHarvestLog>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				
				OAIHarvestLog returnValue = OAIHarvestLogUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.HarvestLogID,
						value.HarvestSetID,
						value.HarvestStartDateTime,
						value.FromDateTime,
						value.UntilDateTime,
						value.ResponseDateTime,
						value.Result);
					
				return new CustomDataAccessStatus<OAIHarvestLog>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<OAIHarvestLog>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}
// end of source generation
