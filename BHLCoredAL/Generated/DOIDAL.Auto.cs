
// Generated 11/11/2011 1:11:27 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class DOIDAL is based upon DOI.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DAL
// {
// 		public partial class DOIDAL
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
	partial class DOIDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from DOI by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="dOIID"></param>
		/// <returns>Object of type DOI.</returns>
		public DOI DOISelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int dOIID)
		{
			return DOISelectAuto(	sqlConnection, sqlTransaction, "BHL",	dOIID );
		}
			
		/// <summary>
		/// Select values from DOI by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="dOIID"></param>
		/// <returns>Object of type DOI.</returns>
		public DOI DOISelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int dOIID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("DOISelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("DOIID", SqlDbType.Int, null, false, dOIID)))
			{
				using (CustomSqlHelper<DOI> helper = new CustomSqlHelper<DOI>())
				{
					CustomGenericList<DOI> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						DOI o = list[0];
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
		/// Select values from DOI by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="dOIID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> DOISelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int dOIID)
		{
			return DOISelectAutoRaw( sqlConnection, sqlTransaction, "BHL", dOIID );
		}
		
		/// <summary>
		/// Select values from DOI by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="dOIID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> DOISelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int dOIID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("DOISelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("DOIID", SqlDbType.Int, null, false, dOIID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====
	
 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into DOI.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="dOIEntityTypeID"></param>
		/// <param name="entityID"></param>
		/// <param name="dOIStatusID"></param>
		/// <param name="dOIBatchID"></param>
		/// <param name="dOIName"></param>
		/// <param name="statusDate"></param>
		/// <param name="statusMessage"></param>
		/// <param name="isValid"></param>
		/// <returns>Object of type DOI.</returns>
		public DOI DOIInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int dOIEntityTypeID,
			int entityID,
			int dOIStatusID,
			string dOIBatchID,
			string dOIName,
			DateTime statusDate,
			string statusMessage,
			short isValid)
		{
			return DOIInsertAuto( sqlConnection, sqlTransaction, "BHL", dOIEntityTypeID, entityID, dOIStatusID, dOIBatchID, dOIName, statusDate, statusMessage, isValid );
		}
		
		/// <summary>
		/// Insert values into DOI.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="dOIEntityTypeID"></param>
		/// <param name="entityID"></param>
		/// <param name="dOIStatusID"></param>
		/// <param name="dOIBatchID"></param>
		/// <param name="dOIName"></param>
		/// <param name="statusDate"></param>
		/// <param name="statusMessage"></param>
		/// <param name="isValid"></param>
		/// <returns>Object of type DOI.</returns>
		public DOI DOIInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int dOIEntityTypeID,
			int entityID,
			int dOIStatusID,
			string dOIBatchID,
			string dOIName,
			DateTime statusDate,
			string statusMessage,
			short isValid)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("DOIInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("DOIID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("DOIEntityTypeID", SqlDbType.Int, null, false, dOIEntityTypeID),
					CustomSqlHelper.CreateInputParameter("EntityID", SqlDbType.Int, null, false, entityID),
					CustomSqlHelper.CreateInputParameter("DOIStatusID", SqlDbType.Int, null, false, dOIStatusID),
					CustomSqlHelper.CreateInputParameter("DOIBatchID", SqlDbType.NVarChar, 50, false, dOIBatchID),
					CustomSqlHelper.CreateInputParameter("DOIName", SqlDbType.NVarChar, 50, false, dOIName),
					CustomSqlHelper.CreateInputParameter("StatusDate", SqlDbType.DateTime, null, false, statusDate),
					CustomSqlHelper.CreateInputParameter("StatusMessage", SqlDbType.NVarChar, 1000, false, statusMessage),
					CustomSqlHelper.CreateInputParameter("IsValid", SqlDbType.SmallInt, null, false, isValid), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<DOI> helper = new CustomSqlHelper<DOI>())
				{
					CustomGenericList<DOI> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						DOI o = list[0];
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
		/// Insert values into DOI. Returns an object of type DOI.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type DOI.</param>
		/// <returns>Object of type DOI.</returns>
		public DOI DOIInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			DOI value)
		{
			return DOIInsertAuto(sqlConnection, sqlTransaction, "BHL", value);
		}
		
		/// <summary>
		/// Insert values into DOI. Returns an object of type DOI.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type DOI.</param>
		/// <returns>Object of type DOI.</returns>
		public DOI DOIInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			DOI value)
		{
			return DOIInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.DOIEntityTypeID,
				value.EntityID,
				value.DOIStatusID,
				value.DOIBatchID,
				value.DOIName,
				value.StatusDate,
				value.StatusMessage,
				value.IsValid);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from DOI by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="dOIID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool DOIDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int dOIID)
		{
			return DOIDeleteAuto( sqlConnection, sqlTransaction, "BHL", dOIID );
		}
		
		/// <summary>
		/// Delete values from DOI by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="dOIID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool DOIDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int dOIID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("DOIDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("DOIID", SqlDbType.Int, null, false, dOIID), 
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
		/// Update values in DOI. Returns an object of type DOI.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="dOIID"></param>
		/// <param name="dOIEntityTypeID"></param>
		/// <param name="entityID"></param>
		/// <param name="dOIStatusID"></param>
		/// <param name="dOIBatchID"></param>
		/// <param name="dOIName"></param>
		/// <param name="statusDate"></param>
		/// <param name="statusMessage"></param>
		/// <param name="isValid"></param>
		/// <returns>Object of type DOI.</returns>
		public DOI DOIUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int dOIID,
			int dOIEntityTypeID,
			int entityID,
			int dOIStatusID,
			string dOIBatchID,
			string dOIName,
			DateTime statusDate,
			string statusMessage,
			short isValid)
		{
			return DOIUpdateAuto( sqlConnection, sqlTransaction, "BHL", dOIID, dOIEntityTypeID, entityID, dOIStatusID, dOIBatchID, dOIName, statusDate, statusMessage, isValid);
		}
		
		/// <summary>
		/// Update values in DOI. Returns an object of type DOI.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="dOIID"></param>
		/// <param name="dOIEntityTypeID"></param>
		/// <param name="entityID"></param>
		/// <param name="dOIStatusID"></param>
		/// <param name="dOIBatchID"></param>
		/// <param name="dOIName"></param>
		/// <param name="statusDate"></param>
		/// <param name="statusMessage"></param>
		/// <param name="isValid"></param>
		/// <returns>Object of type DOI.</returns>
		public DOI DOIUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int dOIID,
			int dOIEntityTypeID,
			int entityID,
			int dOIStatusID,
			string dOIBatchID,
			string dOIName,
			DateTime statusDate,
			string statusMessage,
			short isValid)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("DOIUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("DOIID", SqlDbType.Int, null, false, dOIID),
					CustomSqlHelper.CreateInputParameter("DOIEntityTypeID", SqlDbType.Int, null, false, dOIEntityTypeID),
					CustomSqlHelper.CreateInputParameter("EntityID", SqlDbType.Int, null, false, entityID),
					CustomSqlHelper.CreateInputParameter("DOIStatusID", SqlDbType.Int, null, false, dOIStatusID),
					CustomSqlHelper.CreateInputParameter("DOIBatchID", SqlDbType.NVarChar, 50, false, dOIBatchID),
					CustomSqlHelper.CreateInputParameter("DOIName", SqlDbType.NVarChar, 50, false, dOIName),
					CustomSqlHelper.CreateInputParameter("StatusDate", SqlDbType.DateTime, null, false, statusDate),
					CustomSqlHelper.CreateInputParameter("StatusMessage", SqlDbType.NVarChar, 1000, false, statusMessage),
					CustomSqlHelper.CreateInputParameter("IsValid", SqlDbType.SmallInt, null, false, isValid), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<DOI> helper = new CustomSqlHelper<DOI>())
				{
					CustomGenericList<DOI> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						DOI o = list[0];
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
		/// Update values in DOI. Returns an object of type DOI.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type DOI.</param>
		/// <returns>Object of type DOI.</returns>
		public DOI DOIUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			DOI value)
		{
			return DOIUpdateAuto(sqlConnection, sqlTransaction, "BHL", value );
		}
		
		/// <summary>
		/// Update values in DOI. Returns an object of type DOI.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type DOI.</param>
		/// <returns>Object of type DOI.</returns>
		public DOI DOIUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			DOI value)
		{
			return DOIUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.DOIID,
				value.DOIEntityTypeID,
				value.EntityID,
				value.DOIStatusID,
				value.DOIBatchID,
				value.DOIName,
				value.StatusDate,
				value.StatusMessage,
				value.IsValid);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage DOI object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in DOI.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type DOI.</param>
		/// <returns>Object of type CustomDataAccessStatus<DOI>.</returns>
		public CustomDataAccessStatus<DOI> DOIManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			DOI value  )
		{
			return DOIManageAuto( sqlConnection, sqlTransaction, "BHL", value  );
		}
		
		/// <summary>
		/// Manage DOI object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in DOI.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type DOI.</param>
		/// <returns>Object of type CustomDataAccessStatus<DOI>.</returns>
		public CustomDataAccessStatus<DOI> DOIManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			DOI value  )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				
				
				DOI returnValue = DOIInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.DOIEntityTypeID,
						value.EntityID,
						value.DOIStatusID,
						value.DOIBatchID,
						value.DOIName,
						value.StatusDate,
						value.StatusMessage,
						value.IsValid);
				
				return new CustomDataAccessStatus<DOI>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (DOIDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.DOIID))
				{
				return new CustomDataAccessStatus<DOI>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<DOI>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				
				DOI returnValue = DOIUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.DOIID,
						value.DOIEntityTypeID,
						value.EntityID,
						value.DOIStatusID,
						value.DOIBatchID,
						value.DOIName,
						value.StatusDate,
						value.StatusMessage,
						value.IsValid);
					
				return new CustomDataAccessStatus<DOI>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<DOI>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}
// end of source generation
