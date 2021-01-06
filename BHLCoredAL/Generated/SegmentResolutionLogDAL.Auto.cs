
// Generated 1/5/2021 3:27:03 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class SegmentResolutionLogDAL is based upon dbo.SegmentResolutionLog.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DAL
// {
// 		public partial class SegmentResolutionLogDAL
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
	partial class SegmentResolutionLogDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from dbo.SegmentResolutionLog by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="segmentResolutionLogID"></param>
		/// <returns>Object of type SegmentResolutionLog.</returns>
		public SegmentResolutionLog SegmentResolutionLogSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int segmentResolutionLogID)
		{
			return SegmentResolutionLogSelectAuto(	sqlConnection, sqlTransaction, "BHL",	segmentResolutionLogID );
		}
			
		/// <summary>
		/// Select values from dbo.SegmentResolutionLog by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="segmentResolutionLogID"></param>
		/// <returns>Object of type SegmentResolutionLog.</returns>
		public SegmentResolutionLog SegmentResolutionLogSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int segmentResolutionLogID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("SegmentResolutionLogSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("SegmentResolutionLogID", SqlDbType.Int, null, false, segmentResolutionLogID)))
			{
				using (CustomSqlHelper<SegmentResolutionLog> helper = new CustomSqlHelper<SegmentResolutionLog>())
				{
					List<SegmentResolutionLog> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						SegmentResolutionLog o = list[0];
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
		/// Select values from dbo.SegmentResolutionLog by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="segmentResolutionLogID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> SegmentResolutionLogSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int segmentResolutionLogID)
		{
			return SegmentResolutionLogSelectAutoRaw( sqlConnection, sqlTransaction, "BHL", segmentResolutionLogID );
		}
		
		/// <summary>
		/// Select values from dbo.SegmentResolutionLog by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="segmentResolutionLogID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> SegmentResolutionLogSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int segmentResolutionLogID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("SegmentResolutionLogSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("SegmentResolutionLogID", SqlDbType.Int, null, false, segmentResolutionLogID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====

 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into dbo.SegmentResolutionLog.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="segmentID"></param>
		/// <param name="matchingSegmentID"></param>
		/// <param name="score"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type SegmentResolutionLog.</returns>
		public SegmentResolutionLog SegmentResolutionLogInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int segmentID,
			int matchingSegmentID,
			decimal? score,
			int creationUserID,
			int lastModifiedUserID)
		{
			return SegmentResolutionLogInsertAuto( sqlConnection, sqlTransaction, "BHL", segmentID, matchingSegmentID, score, creationUserID, lastModifiedUserID );
		}
		
		/// <summary>
		/// Insert values into dbo.SegmentResolutionLog.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="segmentID"></param>
		/// <param name="matchingSegmentID"></param>
		/// <param name="score"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type SegmentResolutionLog.</returns>
		public SegmentResolutionLog SegmentResolutionLogInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int segmentID,
			int matchingSegmentID,
			decimal? score,
			int creationUserID,
			int lastModifiedUserID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("SegmentResolutionLogInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("SegmentResolutionLogID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("SegmentID", SqlDbType.Int, null, false, segmentID),
					CustomSqlHelper.CreateInputParameter("MatchingSegmentID", SqlDbType.Int, null, false, matchingSegmentID),
					CustomSqlHelper.CreateInputParameter("Score", SqlDbType.Decimal, null, true, score),
					CustomSqlHelper.CreateInputParameter("CreationUserID", SqlDbType.Int, null, false, creationUserID),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, false, lastModifiedUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<SegmentResolutionLog> helper = new CustomSqlHelper<SegmentResolutionLog>())
				{
					List<SegmentResolutionLog> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						SegmentResolutionLog o = list[0];
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
		/// Insert values into dbo.SegmentResolutionLog. Returns an object of type SegmentResolutionLog.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type SegmentResolutionLog.</param>
		/// <returns>Object of type SegmentResolutionLog.</returns>
		public SegmentResolutionLog SegmentResolutionLogInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			SegmentResolutionLog value)
		{
			return SegmentResolutionLogInsertAuto(sqlConnection, sqlTransaction, "BHL", value);
		}
		
		/// <summary>
		/// Insert values into dbo.SegmentResolutionLog. Returns an object of type SegmentResolutionLog.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type SegmentResolutionLog.</param>
		/// <returns>Object of type SegmentResolutionLog.</returns>
		public SegmentResolutionLog SegmentResolutionLogInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			SegmentResolutionLog value)
		{
			return SegmentResolutionLogInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.SegmentID,
				value.MatchingSegmentID,
				value.Score,
				value.CreationUserID,
				value.LastModifiedUserID);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from dbo.SegmentResolutionLog by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="segmentResolutionLogID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool SegmentResolutionLogDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int segmentResolutionLogID)
		{
			return SegmentResolutionLogDeleteAuto( sqlConnection, sqlTransaction, "BHL", segmentResolutionLogID );
		}
		
		/// <summary>
		/// Delete values from dbo.SegmentResolutionLog by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="segmentResolutionLogID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool SegmentResolutionLogDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int segmentResolutionLogID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("SegmentResolutionLogDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("SegmentResolutionLogID", SqlDbType.Int, null, false, segmentResolutionLogID), 
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
		/// Update values in dbo.SegmentResolutionLog. Returns an object of type SegmentResolutionLog.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="segmentResolutionLogID"></param>
		/// <param name="segmentID"></param>
		/// <param name="matchingSegmentID"></param>
		/// <param name="score"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type SegmentResolutionLog.</returns>
		public SegmentResolutionLog SegmentResolutionLogUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int segmentResolutionLogID,
			int segmentID,
			int matchingSegmentID,
			decimal? score,
			int lastModifiedUserID)
		{
			return SegmentResolutionLogUpdateAuto( sqlConnection, sqlTransaction, "BHL", segmentResolutionLogID, segmentID, matchingSegmentID, score, lastModifiedUserID);
		}
		
		/// <summary>
		/// Update values in dbo.SegmentResolutionLog. Returns an object of type SegmentResolutionLog.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="segmentResolutionLogID"></param>
		/// <param name="segmentID"></param>
		/// <param name="matchingSegmentID"></param>
		/// <param name="score"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type SegmentResolutionLog.</returns>
		public SegmentResolutionLog SegmentResolutionLogUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int segmentResolutionLogID,
			int segmentID,
			int matchingSegmentID,
			decimal? score,
			int lastModifiedUserID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("SegmentResolutionLogUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("SegmentResolutionLogID", SqlDbType.Int, null, false, segmentResolutionLogID),
					CustomSqlHelper.CreateInputParameter("SegmentID", SqlDbType.Int, null, false, segmentID),
					CustomSqlHelper.CreateInputParameter("MatchingSegmentID", SqlDbType.Int, null, false, matchingSegmentID),
					CustomSqlHelper.CreateInputParameter("Score", SqlDbType.Decimal, null, true, score),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, false, lastModifiedUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<SegmentResolutionLog> helper = new CustomSqlHelper<SegmentResolutionLog>())
				{
					List<SegmentResolutionLog> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						SegmentResolutionLog o = list[0];
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
		/// Update values in dbo.SegmentResolutionLog. Returns an object of type SegmentResolutionLog.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type SegmentResolutionLog.</param>
		/// <returns>Object of type SegmentResolutionLog.</returns>
		public SegmentResolutionLog SegmentResolutionLogUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			SegmentResolutionLog value)
		{
			return SegmentResolutionLogUpdateAuto(sqlConnection, sqlTransaction, "BHL", value );
		}
		
		/// <summary>
		/// Update values in dbo.SegmentResolutionLog. Returns an object of type SegmentResolutionLog.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type SegmentResolutionLog.</param>
		/// <returns>Object of type SegmentResolutionLog.</returns>
		public SegmentResolutionLog SegmentResolutionLogUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			SegmentResolutionLog value)
		{
			return SegmentResolutionLogUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.SegmentResolutionLogID,
				value.SegmentID,
				value.MatchingSegmentID,
				value.Score,
				value.LastModifiedUserID);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage dbo.SegmentResolutionLog object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.SegmentResolutionLog.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type SegmentResolutionLog.</param>
		/// <returns>Object of type CustomDataAccessStatus<SegmentResolutionLog>.</returns>
		public CustomDataAccessStatus<SegmentResolutionLog> SegmentResolutionLogManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			SegmentResolutionLog value , int userId )
		{
			return SegmentResolutionLogManageAuto( sqlConnection, sqlTransaction, "BHL", value , userId );
		}
		
		/// <summary>
		/// Manage dbo.SegmentResolutionLog object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.SegmentResolutionLog.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type SegmentResolutionLog.</param>
		/// <returns>Object of type CustomDataAccessStatus<SegmentResolutionLog>.</returns>
		public CustomDataAccessStatus<SegmentResolutionLog> SegmentResolutionLogManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			SegmentResolutionLog value , int userId )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				value.CreationUserID = userId;
				value.LastModifiedUserID = userId;
				SegmentResolutionLog returnValue = SegmentResolutionLogInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.SegmentID,
						value.MatchingSegmentID,
						value.Score,
						value.CreationUserID,
						value.LastModifiedUserID);
				
				return new CustomDataAccessStatus<SegmentResolutionLog>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (SegmentResolutionLogDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.SegmentResolutionLogID))
				{
				return new CustomDataAccessStatus<SegmentResolutionLog>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<SegmentResolutionLog>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				value.LastModifiedUserID = userId;
				SegmentResolutionLog returnValue = SegmentResolutionLogUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.SegmentResolutionLogID,
						value.SegmentID,
						value.MatchingSegmentID,
						value.Score,
						value.LastModifiedUserID);
					
				return new CustomDataAccessStatus<SegmentResolutionLog>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<SegmentResolutionLog>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}

