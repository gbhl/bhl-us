
// Generated 1/20/2021 12:05:35 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class DOIDAL is based upon dbo.DOI.

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
using System.Collections.Generic;
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
		/// Select values from dbo.DOI by primary key(s).
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
		/// Select values from dbo.DOI by primary key(s).
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
					List<DOI> list = helper.ExecuteReader(command);
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
		/// Select values from dbo.DOI by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="dOIID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> DOISelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int dOIID)
		{
			return DOISelectAutoRaw( sqlConnection, sqlTransaction, "BHL", dOIID );
		}
		
		/// <summary>
		/// Select values from dbo.DOI by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="dOIID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> DOISelectAutoRaw(
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
		/// Insert values into dbo.DOI.
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
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
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
			short isValid,
			int creationUserID,
			int lastModifiedUserID)
		{
			return DOIInsertAuto( sqlConnection, sqlTransaction, "BHL", dOIEntityTypeID, entityID, dOIStatusID, dOIBatchID, dOIName, statusDate, statusMessage, isValid, creationUserID, lastModifiedUserID );
		}
		
		/// <summary>
		/// Insert values into dbo.DOI.
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
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
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
			short isValid,
			int creationUserID,
			int lastModifiedUserID)
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
					CustomSqlHelper.CreateInputParameter("CreationUserID", SqlDbType.Int, null, false, creationUserID),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, false, lastModifiedUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<DOI> helper = new CustomSqlHelper<DOI>())
				{
					List<DOI> list = helper.ExecuteReader(command);
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
		/// Insert values into dbo.DOI. Returns an object of type DOI.
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
		/// Insert values into dbo.DOI. Returns an object of type DOI.
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
				value.IsValid,
				value.CreationUserID,
				value.LastModifiedUserID);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from dbo.DOI by primary key(s).
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
		/// Delete values from dbo.DOI by primary key(s).
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
		/// Update values in dbo.DOI. Returns an object of type DOI.
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
		/// <param name="lastModifiedUserID"></param>
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
			short isValid,
			int lastModifiedUserID)
		{
			return DOIUpdateAuto( sqlConnection, sqlTransaction, "BHL", dOIID, dOIEntityTypeID, entityID, dOIStatusID, dOIBatchID, dOIName, statusDate, statusMessage, isValid, lastModifiedUserID);
		}
		
		/// <summary>
		/// Update values in dbo.DOI. Returns an object of type DOI.
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
		/// <param name="lastModifiedUserID"></param>
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
			short isValid,
			int lastModifiedUserID)
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
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, false, lastModifiedUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<DOI> helper = new CustomSqlHelper<DOI>())
				{
					List<DOI> list = helper.ExecuteReader(command);
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
		/// Update values in dbo.DOI. Returns an object of type DOI.
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
		/// Update values in dbo.DOI. Returns an object of type DOI.
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
				value.IsValid,
				value.LastModifiedUserID);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage dbo.DOI object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.DOI.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type DOI.</param>
		/// <returns>Object of type CustomDataAccessStatus<DOI>.</returns>
		public CustomDataAccessStatus<DOI> DOIManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			DOI value , int userId )
		{
			return DOIManageAuto( sqlConnection, sqlTransaction, "BHL", value , userId );
		}
		
		/// <summary>
		/// Manage dbo.DOI object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.DOI.
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
			DOI value , int userId )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				value.CreationUserID = userId;
				value.LastModifiedUserID = userId;
				DOI returnValue = DOIInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.DOIEntityTypeID,
						value.EntityID,
						value.DOIStatusID,
						value.DOIBatchID,
						value.DOIName,
						value.StatusDate,
						value.StatusMessage,
						value.IsValid,
						value.CreationUserID,
						value.LastModifiedUserID);
				
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
				value.LastModifiedUserID = userId;
				DOI returnValue = DOIUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.DOIID,
						value.DOIEntityTypeID,
						value.EntityID,
						value.DOIStatusID,
						value.DOIBatchID,
						value.DOIName,
						value.StatusDate,
						value.StatusMessage,
						value.IsValid,
						value.LastModifiedUserID);
					
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

