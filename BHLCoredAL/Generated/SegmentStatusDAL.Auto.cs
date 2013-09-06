
// Generated 9/18/2012 12:12:30 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class SegmentStatusDAL is based upon SegmentStatus.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DAL
// {
// 		public partial class SegmentStatusDAL
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
	partial class SegmentStatusDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from SegmentStatus by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="segmentStatusID"></param>
		/// <returns>Object of type SegmentStatus.</returns>
		public SegmentStatus SegmentStatusSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int segmentStatusID)
		{
			return SegmentStatusSelectAuto(	sqlConnection, sqlTransaction, "BHL",	segmentStatusID );
		}
			
		/// <summary>
		/// Select values from SegmentStatus by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="segmentStatusID"></param>
		/// <returns>Object of type SegmentStatus.</returns>
		public SegmentStatus SegmentStatusSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int segmentStatusID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("SegmentStatusSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("SegmentStatusID", SqlDbType.Int, null, false, segmentStatusID)))
			{
				using (CustomSqlHelper<SegmentStatus> helper = new CustomSqlHelper<SegmentStatus>())
				{
					CustomGenericList<SegmentStatus> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						SegmentStatus o = list[0];
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
		/// Select values from SegmentStatus by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="segmentStatusID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> SegmentStatusSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int segmentStatusID)
		{
			return SegmentStatusSelectAutoRaw( sqlConnection, sqlTransaction, "BHL", segmentStatusID );
		}
		
		/// <summary>
		/// Select values from SegmentStatus by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="segmentStatusID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> SegmentStatusSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int segmentStatusID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("SegmentStatusSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("SegmentStatusID", SqlDbType.Int, null, false, segmentStatusID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====
	
 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into SegmentStatus.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="segmentStatusID"></param>
		/// <param name="statusName"></param>
		/// <param name="statusDescription"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type SegmentStatus.</returns>
		public SegmentStatus SegmentStatusInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int segmentStatusID,
			string statusName,
			string statusDescription,
			int creationUserID,
			int lastModifiedUserID)
		{
			return SegmentStatusInsertAuto( sqlConnection, sqlTransaction, "BHL", segmentStatusID, statusName, statusDescription, creationUserID, lastModifiedUserID );
		}
		
		/// <summary>
		/// Insert values into SegmentStatus.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="segmentStatusID"></param>
		/// <param name="statusName"></param>
		/// <param name="statusDescription"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type SegmentStatus.</returns>
		public SegmentStatus SegmentStatusInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int segmentStatusID,
			string statusName,
			string statusDescription,
			int creationUserID,
			int lastModifiedUserID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("SegmentStatusInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("SegmentStatusID", SqlDbType.Int, null, false, segmentStatusID),
					CustomSqlHelper.CreateInputParameter("StatusName", SqlDbType.NVarChar, 50, false, statusName),
					CustomSqlHelper.CreateInputParameter("StatusDescription", SqlDbType.NVarChar, 500, false, statusDescription),
					CustomSqlHelper.CreateInputParameter("CreationUserID", SqlDbType.Int, null, false, creationUserID),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, false, lastModifiedUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<SegmentStatus> helper = new CustomSqlHelper<SegmentStatus>())
				{
					CustomGenericList<SegmentStatus> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						SegmentStatus o = list[0];
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
		/// Insert values into SegmentStatus. Returns an object of type SegmentStatus.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type SegmentStatus.</param>
		/// <returns>Object of type SegmentStatus.</returns>
		public SegmentStatus SegmentStatusInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			SegmentStatus value)
		{
			return SegmentStatusInsertAuto(sqlConnection, sqlTransaction, "BHL", value);
		}
		
		/// <summary>
		/// Insert values into SegmentStatus. Returns an object of type SegmentStatus.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type SegmentStatus.</param>
		/// <returns>Object of type SegmentStatus.</returns>
		public SegmentStatus SegmentStatusInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			SegmentStatus value)
		{
			return SegmentStatusInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.SegmentStatusID,
				value.StatusName,
				value.StatusDescription,
				value.CreationUserID,
				value.LastModifiedUserID);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from SegmentStatus by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="segmentStatusID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool SegmentStatusDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int segmentStatusID)
		{
			return SegmentStatusDeleteAuto( sqlConnection, sqlTransaction, "BHL", segmentStatusID );
		}
		
		/// <summary>
		/// Delete values from SegmentStatus by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="segmentStatusID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool SegmentStatusDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int segmentStatusID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("SegmentStatusDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("SegmentStatusID", SqlDbType.Int, null, false, segmentStatusID), 
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
		/// Update values in SegmentStatus. Returns an object of type SegmentStatus.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="segmentStatusID"></param>
		/// <param name="statusName"></param>
		/// <param name="statusDescription"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type SegmentStatus.</returns>
		public SegmentStatus SegmentStatusUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int segmentStatusID,
			string statusName,
			string statusDescription,
			int lastModifiedUserID)
		{
			return SegmentStatusUpdateAuto( sqlConnection, sqlTransaction, "BHL", segmentStatusID, statusName, statusDescription, lastModifiedUserID);
		}
		
		/// <summary>
		/// Update values in SegmentStatus. Returns an object of type SegmentStatus.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="segmentStatusID"></param>
		/// <param name="statusName"></param>
		/// <param name="statusDescription"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type SegmentStatus.</returns>
		public SegmentStatus SegmentStatusUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int segmentStatusID,
			string statusName,
			string statusDescription,
			int lastModifiedUserID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("SegmentStatusUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("SegmentStatusID", SqlDbType.Int, null, false, segmentStatusID),
					CustomSqlHelper.CreateInputParameter("StatusName", SqlDbType.NVarChar, 50, false, statusName),
					CustomSqlHelper.CreateInputParameter("StatusDescription", SqlDbType.NVarChar, 500, false, statusDescription),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, false, lastModifiedUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<SegmentStatus> helper = new CustomSqlHelper<SegmentStatus>())
				{
					CustomGenericList<SegmentStatus> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						SegmentStatus o = list[0];
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
		/// Update values in SegmentStatus. Returns an object of type SegmentStatus.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type SegmentStatus.</param>
		/// <returns>Object of type SegmentStatus.</returns>
		public SegmentStatus SegmentStatusUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			SegmentStatus value)
		{
			return SegmentStatusUpdateAuto(sqlConnection, sqlTransaction, "BHL", value );
		}
		
		/// <summary>
		/// Update values in SegmentStatus. Returns an object of type SegmentStatus.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type SegmentStatus.</param>
		/// <returns>Object of type SegmentStatus.</returns>
		public SegmentStatus SegmentStatusUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			SegmentStatus value)
		{
			return SegmentStatusUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.SegmentStatusID,
				value.StatusName,
				value.StatusDescription,
				value.LastModifiedUserID);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage SegmentStatus object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in SegmentStatus.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type SegmentStatus.</param>
		/// <returns>Object of type CustomDataAccessStatus<SegmentStatus>.</returns>
		public CustomDataAccessStatus<SegmentStatus> SegmentStatusManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			SegmentStatus value , int userId )
		{
			return SegmentStatusManageAuto( sqlConnection, sqlTransaction, "BHL", value , userId );
		}
		
		/// <summary>
		/// Manage SegmentStatus object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in SegmentStatus.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type SegmentStatus.</param>
		/// <returns>Object of type CustomDataAccessStatus<SegmentStatus>.</returns>
		public CustomDataAccessStatus<SegmentStatus> SegmentStatusManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			SegmentStatus value , int userId )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				value.CreationUserID = userId;
				value.LastModifiedUserID = userId;
				SegmentStatus returnValue = SegmentStatusInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.SegmentStatusID,
						value.StatusName,
						value.StatusDescription,
						value.CreationUserID,
						value.LastModifiedUserID);
				
				return new CustomDataAccessStatus<SegmentStatus>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (SegmentStatusDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.SegmentStatusID))
				{
				return new CustomDataAccessStatus<SegmentStatus>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<SegmentStatus>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				value.LastModifiedUserID = userId;
				SegmentStatus returnValue = SegmentStatusUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.SegmentStatusID,
						value.StatusName,
						value.StatusDescription,
						value.LastModifiedUserID);
					
				return new CustomDataAccessStatus<SegmentStatus>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<SegmentStatus>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}
// end of source generation
