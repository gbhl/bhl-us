
// Generated 1/5/2021 3:26:58 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class SegmentClusterSegmentDAL is based upon dbo.SegmentClusterSegment.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DAL
// {
// 		public partial class SegmentClusterSegmentDAL
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
	partial class SegmentClusterSegmentDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from dbo.SegmentClusterSegment by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="segmentID"></param>
		/// <param name="segmentClusterID"></param>
		/// <returns>Object of type SegmentClusterSegment.</returns>
		public SegmentClusterSegment SegmentClusterSegmentSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int segmentID,
			int segmentClusterID)
		{
			return SegmentClusterSegmentSelectAuto(	sqlConnection, sqlTransaction, "BHL",	segmentID, segmentClusterID );
		}
			
		/// <summary>
		/// Select values from dbo.SegmentClusterSegment by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="segmentID"></param>
		/// <param name="segmentClusterID"></param>
		/// <returns>Object of type SegmentClusterSegment.</returns>
		public SegmentClusterSegment SegmentClusterSegmentSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int segmentID,
			int segmentClusterID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("SegmentClusterSegmentSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("SegmentID", SqlDbType.Int, null, false, segmentID),
					CustomSqlHelper.CreateInputParameter("SegmentClusterID", SqlDbType.Int, null, false, segmentClusterID)))
			{
				using (CustomSqlHelper<SegmentClusterSegment> helper = new CustomSqlHelper<SegmentClusterSegment>())
				{
					List<SegmentClusterSegment> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						SegmentClusterSegment o = list[0];
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
		/// Select values from dbo.SegmentClusterSegment by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="segmentID"></param>
		/// <param name="segmentClusterID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> SegmentClusterSegmentSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int segmentID,
			int segmentClusterID)
		{
			return SegmentClusterSegmentSelectAutoRaw( sqlConnection, sqlTransaction, "BHL", segmentID, segmentClusterID );
		}
		
		/// <summary>
		/// Select values from dbo.SegmentClusterSegment by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="segmentID"></param>
		/// <param name="segmentClusterID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> SegmentClusterSegmentSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int segmentID,
			int segmentClusterID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("SegmentClusterSegmentSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("SegmentID", SqlDbType.Int, null, false, segmentID),
					CustomSqlHelper.CreateInputParameter("SegmentClusterID", SqlDbType.Int, null, false, segmentClusterID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====

 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into dbo.SegmentClusterSegment.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="segmentID"></param>
		/// <param name="segmentClusterID"></param>
		/// <param name="isPrimary"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type SegmentClusterSegment.</returns>
		public SegmentClusterSegment SegmentClusterSegmentInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int segmentID,
			int segmentClusterID,
			short isPrimary,
			int creationUserID,
			int lastModifiedUserID)
		{
			return SegmentClusterSegmentInsertAuto( sqlConnection, sqlTransaction, "BHL", segmentID, segmentClusterID, isPrimary, creationUserID, lastModifiedUserID );
		}
		
		/// <summary>
		/// Insert values into dbo.SegmentClusterSegment.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="segmentID"></param>
		/// <param name="segmentClusterID"></param>
		/// <param name="isPrimary"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type SegmentClusterSegment.</returns>
		public SegmentClusterSegment SegmentClusterSegmentInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int segmentID,
			int segmentClusterID,
			short isPrimary,
			int creationUserID,
			int lastModifiedUserID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("SegmentClusterSegmentInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("SegmentID", SqlDbType.Int, null, false, segmentID),
					CustomSqlHelper.CreateInputParameter("SegmentClusterID", SqlDbType.Int, null, false, segmentClusterID),
					CustomSqlHelper.CreateInputParameter("IsPrimary", SqlDbType.SmallInt, null, false, isPrimary),
					CustomSqlHelper.CreateInputParameter("CreationUserID", SqlDbType.Int, null, false, creationUserID),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, false, lastModifiedUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<SegmentClusterSegment> helper = new CustomSqlHelper<SegmentClusterSegment>())
				{
					List<SegmentClusterSegment> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						SegmentClusterSegment o = list[0];
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
		/// Insert values into dbo.SegmentClusterSegment. Returns an object of type SegmentClusterSegment.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type SegmentClusterSegment.</param>
		/// <returns>Object of type SegmentClusterSegment.</returns>
		public SegmentClusterSegment SegmentClusterSegmentInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			SegmentClusterSegment value)
		{
			return SegmentClusterSegmentInsertAuto(sqlConnection, sqlTransaction, "BHL", value);
		}
		
		/// <summary>
		/// Insert values into dbo.SegmentClusterSegment. Returns an object of type SegmentClusterSegment.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type SegmentClusterSegment.</param>
		/// <returns>Object of type SegmentClusterSegment.</returns>
		public SegmentClusterSegment SegmentClusterSegmentInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			SegmentClusterSegment value)
		{
			return SegmentClusterSegmentInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.SegmentID,
				value.SegmentClusterID,
				value.IsPrimary,
				value.CreationUserID,
				value.LastModifiedUserID);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from dbo.SegmentClusterSegment by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="segmentID"></param>
		/// <param name="segmentClusterID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool SegmentClusterSegmentDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int segmentID,
			int segmentClusterID)
		{
			return SegmentClusterSegmentDeleteAuto( sqlConnection, sqlTransaction, "BHL", segmentID, segmentClusterID );
		}
		
		/// <summary>
		/// Delete values from dbo.SegmentClusterSegment by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="segmentID"></param>
		/// <param name="segmentClusterID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool SegmentClusterSegmentDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int segmentID,
			int segmentClusterID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("SegmentClusterSegmentDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("SegmentID", SqlDbType.Int, null, false, segmentID),
					CustomSqlHelper.CreateInputParameter("SegmentClusterID", SqlDbType.Int, null, false, segmentClusterID), 
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
		/// Update values in dbo.SegmentClusterSegment. Returns an object of type SegmentClusterSegment.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="segmentID"></param>
		/// <param name="segmentClusterID"></param>
		/// <param name="isPrimary"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type SegmentClusterSegment.</returns>
		public SegmentClusterSegment SegmentClusterSegmentUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int segmentID,
			int segmentClusterID,
			short isPrimary,
			int lastModifiedUserID)
		{
			return SegmentClusterSegmentUpdateAuto( sqlConnection, sqlTransaction, "BHL", segmentID, segmentClusterID, isPrimary, lastModifiedUserID);
		}
		
		/// <summary>
		/// Update values in dbo.SegmentClusterSegment. Returns an object of type SegmentClusterSegment.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="segmentID"></param>
		/// <param name="segmentClusterID"></param>
		/// <param name="isPrimary"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type SegmentClusterSegment.</returns>
		public SegmentClusterSegment SegmentClusterSegmentUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int segmentID,
			int segmentClusterID,
			short isPrimary,
			int lastModifiedUserID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("SegmentClusterSegmentUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("SegmentID", SqlDbType.Int, null, false, segmentID),
					CustomSqlHelper.CreateInputParameter("SegmentClusterID", SqlDbType.Int, null, false, segmentClusterID),
					CustomSqlHelper.CreateInputParameter("IsPrimary", SqlDbType.SmallInt, null, false, isPrimary),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, false, lastModifiedUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<SegmentClusterSegment> helper = new CustomSqlHelper<SegmentClusterSegment>())
				{
					List<SegmentClusterSegment> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						SegmentClusterSegment o = list[0];
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
		/// Update values in dbo.SegmentClusterSegment. Returns an object of type SegmentClusterSegment.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type SegmentClusterSegment.</param>
		/// <returns>Object of type SegmentClusterSegment.</returns>
		public SegmentClusterSegment SegmentClusterSegmentUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			SegmentClusterSegment value)
		{
			return SegmentClusterSegmentUpdateAuto(sqlConnection, sqlTransaction, "BHL", value );
		}
		
		/// <summary>
		/// Update values in dbo.SegmentClusterSegment. Returns an object of type SegmentClusterSegment.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type SegmentClusterSegment.</param>
		/// <returns>Object of type SegmentClusterSegment.</returns>
		public SegmentClusterSegment SegmentClusterSegmentUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			SegmentClusterSegment value)
		{
			return SegmentClusterSegmentUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.SegmentID,
				value.SegmentClusterID,
				value.IsPrimary,
				value.LastModifiedUserID);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage dbo.SegmentClusterSegment object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.SegmentClusterSegment.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type SegmentClusterSegment.</param>
		/// <returns>Object of type CustomDataAccessStatus<SegmentClusterSegment>.</returns>
		public CustomDataAccessStatus<SegmentClusterSegment> SegmentClusterSegmentManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			SegmentClusterSegment value , int userId )
		{
			return SegmentClusterSegmentManageAuto( sqlConnection, sqlTransaction, "BHL", value , userId );
		}
		
		/// <summary>
		/// Manage dbo.SegmentClusterSegment object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.SegmentClusterSegment.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type SegmentClusterSegment.</param>
		/// <returns>Object of type CustomDataAccessStatus<SegmentClusterSegment>.</returns>
		public CustomDataAccessStatus<SegmentClusterSegment> SegmentClusterSegmentManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			SegmentClusterSegment value , int userId )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				value.CreationUserID = userId;
				value.LastModifiedUserID = userId;
				SegmentClusterSegment returnValue = SegmentClusterSegmentInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.SegmentID,
						value.SegmentClusterID,
						value.IsPrimary,
						value.CreationUserID,
						value.LastModifiedUserID);
				
				return new CustomDataAccessStatus<SegmentClusterSegment>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (SegmentClusterSegmentDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.SegmentID,
						value.SegmentClusterID))
				{
				return new CustomDataAccessStatus<SegmentClusterSegment>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<SegmentClusterSegment>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				value.LastModifiedUserID = userId;
				SegmentClusterSegment returnValue = SegmentClusterSegmentUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.SegmentID,
						value.SegmentClusterID,
						value.IsPrimary,
						value.LastModifiedUserID);
					
				return new CustomDataAccessStatus<SegmentClusterSegment>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<SegmentClusterSegment>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}

