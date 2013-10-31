
// Generated 10/31/2013 4:01:46 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class OAIRecordStatusDAL is based upon OAIRecordStatus.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHLImport.DAL
// {
// 		public partial class OAIRecordStatusDAL
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
	partial class OAIRecordStatusDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from OAIRecordStatus by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="oAIRecordStatusID"></param>
		/// <returns>Object of type OAIRecordStatus.</returns>
		public OAIRecordStatus OAIRecordStatusSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int oAIRecordStatusID)
		{
			return OAIRecordStatusSelectAuto(	sqlConnection, sqlTransaction, "BHLImport",	oAIRecordStatusID );
		}
			
		/// <summary>
		/// Select values from OAIRecordStatus by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="oAIRecordStatusID"></param>
		/// <returns>Object of type OAIRecordStatus.</returns>
		public OAIRecordStatus OAIRecordStatusSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int oAIRecordStatusID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("OAIRecordStatusSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("OAIRecordStatusID", SqlDbType.Int, null, false, oAIRecordStatusID)))
			{
				using (CustomSqlHelper<OAIRecordStatus> helper = new CustomSqlHelper<OAIRecordStatus>())
				{
					CustomGenericList<OAIRecordStatus> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						OAIRecordStatus o = list[0];
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
		/// Select values from OAIRecordStatus by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="oAIRecordStatusID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> OAIRecordStatusSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int oAIRecordStatusID)
		{
			return OAIRecordStatusSelectAutoRaw( sqlConnection, sqlTransaction, "BHLImport", oAIRecordStatusID );
		}
		
		/// <summary>
		/// Select values from OAIRecordStatus by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="oAIRecordStatusID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> OAIRecordStatusSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int oAIRecordStatusID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("OAIRecordStatusSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("OAIRecordStatusID", SqlDbType.Int, null, false, oAIRecordStatusID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====
	
 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into OAIRecordStatus.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="recordStatus"></param>
		/// <param name="statusDescription"></param>
		/// <returns>Object of type OAIRecordStatus.</returns>
		public OAIRecordStatus OAIRecordStatusInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string recordStatus,
			string statusDescription)
		{
			return OAIRecordStatusInsertAuto( sqlConnection, sqlTransaction, "BHLImport", recordStatus, statusDescription );
		}
		
		/// <summary>
		/// Insert values into OAIRecordStatus.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="recordStatus"></param>
		/// <param name="statusDescription"></param>
		/// <returns>Object of type OAIRecordStatus.</returns>
		public OAIRecordStatus OAIRecordStatusInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			string recordStatus,
			string statusDescription)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("OAIRecordStatusInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("OAIRecordStatusID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("RecordStatus", SqlDbType.NVarChar, 30, false, recordStatus),
					CustomSqlHelper.CreateInputParameter("StatusDescription", SqlDbType.NVarChar, 400, false, statusDescription), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<OAIRecordStatus> helper = new CustomSqlHelper<OAIRecordStatus>())
				{
					CustomGenericList<OAIRecordStatus> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						OAIRecordStatus o = list[0];
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
		/// Insert values into OAIRecordStatus. Returns an object of type OAIRecordStatus.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type OAIRecordStatus.</param>
		/// <returns>Object of type OAIRecordStatus.</returns>
		public OAIRecordStatus OAIRecordStatusInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			OAIRecordStatus value)
		{
			return OAIRecordStatusInsertAuto(sqlConnection, sqlTransaction, "BHLImport", value);
		}
		
		/// <summary>
		/// Insert values into OAIRecordStatus. Returns an object of type OAIRecordStatus.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type OAIRecordStatus.</param>
		/// <returns>Object of type OAIRecordStatus.</returns>
		public OAIRecordStatus OAIRecordStatusInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			OAIRecordStatus value)
		{
			return OAIRecordStatusInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.RecordStatus,
				value.StatusDescription);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from OAIRecordStatus by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="oAIRecordStatusID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool OAIRecordStatusDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int oAIRecordStatusID)
		{
			return OAIRecordStatusDeleteAuto( sqlConnection, sqlTransaction, "BHLImport", oAIRecordStatusID );
		}
		
		/// <summary>
		/// Delete values from OAIRecordStatus by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="oAIRecordStatusID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool OAIRecordStatusDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int oAIRecordStatusID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("OAIRecordStatusDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("OAIRecordStatusID", SqlDbType.Int, null, false, oAIRecordStatusID), 
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
		/// Update values in OAIRecordStatus. Returns an object of type OAIRecordStatus.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="oAIRecordStatusID"></param>
		/// <param name="recordStatus"></param>
		/// <param name="statusDescription"></param>
		/// <returns>Object of type OAIRecordStatus.</returns>
		public OAIRecordStatus OAIRecordStatusUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int oAIRecordStatusID,
			string recordStatus,
			string statusDescription)
		{
			return OAIRecordStatusUpdateAuto( sqlConnection, sqlTransaction, "BHLImport", oAIRecordStatusID, recordStatus, statusDescription);
		}
		
		/// <summary>
		/// Update values in OAIRecordStatus. Returns an object of type OAIRecordStatus.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="oAIRecordStatusID"></param>
		/// <param name="recordStatus"></param>
		/// <param name="statusDescription"></param>
		/// <returns>Object of type OAIRecordStatus.</returns>
		public OAIRecordStatus OAIRecordStatusUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int oAIRecordStatusID,
			string recordStatus,
			string statusDescription)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("OAIRecordStatusUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("OAIRecordStatusID", SqlDbType.Int, null, false, oAIRecordStatusID),
					CustomSqlHelper.CreateInputParameter("RecordStatus", SqlDbType.NVarChar, 30, false, recordStatus),
					CustomSqlHelper.CreateInputParameter("StatusDescription", SqlDbType.NVarChar, 400, false, statusDescription), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<OAIRecordStatus> helper = new CustomSqlHelper<OAIRecordStatus>())
				{
					CustomGenericList<OAIRecordStatus> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						OAIRecordStatus o = list[0];
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
		/// Update values in OAIRecordStatus. Returns an object of type OAIRecordStatus.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type OAIRecordStatus.</param>
		/// <returns>Object of type OAIRecordStatus.</returns>
		public OAIRecordStatus OAIRecordStatusUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			OAIRecordStatus value)
		{
			return OAIRecordStatusUpdateAuto(sqlConnection, sqlTransaction, "BHLImport", value );
		}
		
		/// <summary>
		/// Update values in OAIRecordStatus. Returns an object of type OAIRecordStatus.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type OAIRecordStatus.</param>
		/// <returns>Object of type OAIRecordStatus.</returns>
		public OAIRecordStatus OAIRecordStatusUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			OAIRecordStatus value)
		{
			return OAIRecordStatusUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.OAIRecordStatusID,
				value.RecordStatus,
				value.StatusDescription);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage OAIRecordStatus object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in OAIRecordStatus.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type OAIRecordStatus.</param>
		/// <returns>Object of type CustomDataAccessStatus<OAIRecordStatus>.</returns>
		public CustomDataAccessStatus<OAIRecordStatus> OAIRecordStatusManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			OAIRecordStatus value  )
		{
			return OAIRecordStatusManageAuto( sqlConnection, sqlTransaction, "BHLImport", value  );
		}
		
		/// <summary>
		/// Manage OAIRecordStatus object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in OAIRecordStatus.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type OAIRecordStatus.</param>
		/// <returns>Object of type CustomDataAccessStatus<OAIRecordStatus>.</returns>
		public CustomDataAccessStatus<OAIRecordStatus> OAIRecordStatusManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			OAIRecordStatus value  )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				
				
				OAIRecordStatus returnValue = OAIRecordStatusInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.RecordStatus,
						value.StatusDescription);
				
				return new CustomDataAccessStatus<OAIRecordStatus>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (OAIRecordStatusDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.OAIRecordStatusID))
				{
				return new CustomDataAccessStatus<OAIRecordStatus>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<OAIRecordStatus>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				
				OAIRecordStatus returnValue = OAIRecordStatusUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.OAIRecordStatusID,
						value.RecordStatus,
						value.StatusDescription);
					
				return new CustomDataAccessStatus<OAIRecordStatus>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<OAIRecordStatus>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}
// end of source generation
