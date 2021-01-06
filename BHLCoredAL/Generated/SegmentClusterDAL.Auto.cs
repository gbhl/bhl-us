
// Generated 1/5/2021 3:26:56 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class SegmentClusterDAL is based upon dbo.SegmentCluster.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DAL
// {
// 		public partial class SegmentClusterDAL
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
	partial class SegmentClusterDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from dbo.SegmentCluster by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="segmentClusterID"></param>
		/// <returns>Object of type SegmentCluster.</returns>
		public SegmentCluster SegmentClusterSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int segmentClusterID)
		{
			return SegmentClusterSelectAuto(	sqlConnection, sqlTransaction, "BHL",	segmentClusterID );
		}
			
		/// <summary>
		/// Select values from dbo.SegmentCluster by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="segmentClusterID"></param>
		/// <returns>Object of type SegmentCluster.</returns>
		public SegmentCluster SegmentClusterSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int segmentClusterID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("SegmentClusterSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("SegmentClusterID", SqlDbType.Int, null, false, segmentClusterID)))
			{
				using (CustomSqlHelper<SegmentCluster> helper = new CustomSqlHelper<SegmentCluster>())
				{
					List<SegmentCluster> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						SegmentCluster o = list[0];
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
		/// Select values from dbo.SegmentCluster by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="segmentClusterID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> SegmentClusterSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int segmentClusterID)
		{
			return SegmentClusterSelectAutoRaw( sqlConnection, sqlTransaction, "BHL", segmentClusterID );
		}
		
		/// <summary>
		/// Select values from dbo.SegmentCluster by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="segmentClusterID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> SegmentClusterSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int segmentClusterID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("SegmentClusterSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("SegmentClusterID", SqlDbType.Int, null, false, segmentClusterID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====

 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into dbo.SegmentCluster.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <param name="segmentClusterTypeID"></param>
		/// <returns>Object of type SegmentCluster.</returns>
		public SegmentCluster SegmentClusterInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int creationUserID,
			int lastModifiedUserID,
			int segmentClusterTypeID)
		{
			return SegmentClusterInsertAuto( sqlConnection, sqlTransaction, "BHL", creationUserID, lastModifiedUserID, segmentClusterTypeID );
		}
		
		/// <summary>
		/// Insert values into dbo.SegmentCluster.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <param name="segmentClusterTypeID"></param>
		/// <returns>Object of type SegmentCluster.</returns>
		public SegmentCluster SegmentClusterInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int creationUserID,
			int lastModifiedUserID,
			int segmentClusterTypeID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("SegmentClusterInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("SegmentClusterID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("CreationUserID", SqlDbType.Int, null, false, creationUserID),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, false, lastModifiedUserID),
					CustomSqlHelper.CreateInputParameter("SegmentClusterTypeID", SqlDbType.Int, null, false, segmentClusterTypeID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<SegmentCluster> helper = new CustomSqlHelper<SegmentCluster>())
				{
					List<SegmentCluster> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						SegmentCluster o = list[0];
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
		/// Insert values into dbo.SegmentCluster. Returns an object of type SegmentCluster.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type SegmentCluster.</param>
		/// <returns>Object of type SegmentCluster.</returns>
		public SegmentCluster SegmentClusterInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			SegmentCluster value)
		{
			return SegmentClusterInsertAuto(sqlConnection, sqlTransaction, "BHL", value);
		}
		
		/// <summary>
		/// Insert values into dbo.SegmentCluster. Returns an object of type SegmentCluster.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type SegmentCluster.</param>
		/// <returns>Object of type SegmentCluster.</returns>
		public SegmentCluster SegmentClusterInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			SegmentCluster value)
		{
			return SegmentClusterInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.CreationUserID,
				value.LastModifiedUserID,
				value.SegmentClusterTypeID);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from dbo.SegmentCluster by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="segmentClusterID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool SegmentClusterDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int segmentClusterID)
		{
			return SegmentClusterDeleteAuto( sqlConnection, sqlTransaction, "BHL", segmentClusterID );
		}
		
		/// <summary>
		/// Delete values from dbo.SegmentCluster by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="segmentClusterID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool SegmentClusterDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int segmentClusterID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("SegmentClusterDeleteAuto", connection, transaction, 
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
		/// Update values in dbo.SegmentCluster. Returns an object of type SegmentCluster.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="segmentClusterID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <param name="segmentClusterTypeID"></param>
		/// <returns>Object of type SegmentCluster.</returns>
		public SegmentCluster SegmentClusterUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int segmentClusterID,
			int lastModifiedUserID,
			int segmentClusterTypeID)
		{
			return SegmentClusterUpdateAuto( sqlConnection, sqlTransaction, "BHL", segmentClusterID, lastModifiedUserID, segmentClusterTypeID);
		}
		
		/// <summary>
		/// Update values in dbo.SegmentCluster. Returns an object of type SegmentCluster.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="segmentClusterID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <param name="segmentClusterTypeID"></param>
		/// <returns>Object of type SegmentCluster.</returns>
		public SegmentCluster SegmentClusterUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int segmentClusterID,
			int lastModifiedUserID,
			int segmentClusterTypeID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("SegmentClusterUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("SegmentClusterID", SqlDbType.Int, null, false, segmentClusterID),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, false, lastModifiedUserID),
					CustomSqlHelper.CreateInputParameter("SegmentClusterTypeID", SqlDbType.Int, null, false, segmentClusterTypeID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<SegmentCluster> helper = new CustomSqlHelper<SegmentCluster>())
				{
					List<SegmentCluster> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						SegmentCluster o = list[0];
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
		/// Update values in dbo.SegmentCluster. Returns an object of type SegmentCluster.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type SegmentCluster.</param>
		/// <returns>Object of type SegmentCluster.</returns>
		public SegmentCluster SegmentClusterUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			SegmentCluster value)
		{
			return SegmentClusterUpdateAuto(sqlConnection, sqlTransaction, "BHL", value );
		}
		
		/// <summary>
		/// Update values in dbo.SegmentCluster. Returns an object of type SegmentCluster.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type SegmentCluster.</param>
		/// <returns>Object of type SegmentCluster.</returns>
		public SegmentCluster SegmentClusterUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			SegmentCluster value)
		{
			return SegmentClusterUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.SegmentClusterID,
				value.LastModifiedUserID,
				value.SegmentClusterTypeID);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage dbo.SegmentCluster object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.SegmentCluster.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type SegmentCluster.</param>
		/// <returns>Object of type CustomDataAccessStatus<SegmentCluster>.</returns>
		public CustomDataAccessStatus<SegmentCluster> SegmentClusterManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			SegmentCluster value , int userId )
		{
			return SegmentClusterManageAuto( sqlConnection, sqlTransaction, "BHL", value , userId );
		}
		
		/// <summary>
		/// Manage dbo.SegmentCluster object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.SegmentCluster.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type SegmentCluster.</param>
		/// <returns>Object of type CustomDataAccessStatus<SegmentCluster>.</returns>
		public CustomDataAccessStatus<SegmentCluster> SegmentClusterManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			SegmentCluster value , int userId )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				value.CreationUserID = userId;
				value.LastModifiedUserID = userId;
				SegmentCluster returnValue = SegmentClusterInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.CreationUserID,
						value.LastModifiedUserID,
						value.SegmentClusterTypeID);
				
				return new CustomDataAccessStatus<SegmentCluster>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (SegmentClusterDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.SegmentClusterID))
				{
				return new CustomDataAccessStatus<SegmentCluster>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<SegmentCluster>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				value.LastModifiedUserID = userId;
				SegmentCluster returnValue = SegmentClusterUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.SegmentClusterID,
						value.LastModifiedUserID,
						value.SegmentClusterTypeID);
					
				return new CustomDataAccessStatus<SegmentCluster>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<SegmentCluster>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}

