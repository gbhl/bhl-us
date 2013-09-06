
// Generated 11/2/2012 3:47:04 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class NameSegmentDAL is based upon NameSegment.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DAL
// {
// 		public partial class NameSegmentDAL
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
	partial class NameSegmentDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from NameSegment by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="nameSegmentID"></param>
		/// <returns>Object of type NameSegment.</returns>
		public NameSegment NameSegmentSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int nameSegmentID)
		{
			return NameSegmentSelectAuto(	sqlConnection, sqlTransaction, "BHL",	nameSegmentID );
		}
			
		/// <summary>
		/// Select values from NameSegment by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="nameSegmentID"></param>
		/// <returns>Object of type NameSegment.</returns>
		public NameSegment NameSegmentSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int nameSegmentID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("NameSegmentSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("NameSegmentID", SqlDbType.Int, null, false, nameSegmentID)))
			{
				using (CustomSqlHelper<NameSegment> helper = new CustomSqlHelper<NameSegment>())
				{
					CustomGenericList<NameSegment> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						NameSegment o = list[0];
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
		/// Select values from NameSegment by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="nameSegmentID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> NameSegmentSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int nameSegmentID)
		{
			return NameSegmentSelectAutoRaw( sqlConnection, sqlTransaction, "BHL", nameSegmentID );
		}
		
		/// <summary>
		/// Select values from NameSegment by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="nameSegmentID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> NameSegmentSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int nameSegmentID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("NameSegmentSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("NameSegmentID", SqlDbType.Int, null, false, nameSegmentID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====
	
 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into NameSegment.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="nameID"></param>
		/// <param name="segmentID"></param>
		/// <param name="nameSourceID"></param>
		/// <param name="isFirstOccurrence"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type NameSegment.</returns>
		public NameSegment NameSegmentInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int nameID,
			int segmentID,
			int nameSourceID,
			short isFirstOccurrence,
			int? creationUserID,
			int? lastModifiedUserID)
		{
			return NameSegmentInsertAuto( sqlConnection, sqlTransaction, "BHL", nameID, segmentID, nameSourceID, isFirstOccurrence, creationUserID, lastModifiedUserID );
		}
		
		/// <summary>
		/// Insert values into NameSegment.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="nameID"></param>
		/// <param name="segmentID"></param>
		/// <param name="nameSourceID"></param>
		/// <param name="isFirstOccurrence"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type NameSegment.</returns>
		public NameSegment NameSegmentInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int nameID,
			int segmentID,
			int nameSourceID,
			short isFirstOccurrence,
			int? creationUserID,
			int? lastModifiedUserID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("NameSegmentInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("NameSegmentID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("NameID", SqlDbType.Int, null, false, nameID),
					CustomSqlHelper.CreateInputParameter("SegmentID", SqlDbType.Int, null, false, segmentID),
					CustomSqlHelper.CreateInputParameter("NameSourceID", SqlDbType.Int, null, false, nameSourceID),
					CustomSqlHelper.CreateInputParameter("IsFirstOccurrence", SqlDbType.SmallInt, null, false, isFirstOccurrence),
					CustomSqlHelper.CreateInputParameter("CreationUserID", SqlDbType.Int, null, true, creationUserID),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, true, lastModifiedUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<NameSegment> helper = new CustomSqlHelper<NameSegment>())
				{
					CustomGenericList<NameSegment> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						NameSegment o = list[0];
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
		/// Insert values into NameSegment. Returns an object of type NameSegment.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type NameSegment.</param>
		/// <returns>Object of type NameSegment.</returns>
		public NameSegment NameSegmentInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			NameSegment value)
		{
			return NameSegmentInsertAuto(sqlConnection, sqlTransaction, "BHL", value);
		}
		
		/// <summary>
		/// Insert values into NameSegment. Returns an object of type NameSegment.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type NameSegment.</param>
		/// <returns>Object of type NameSegment.</returns>
		public NameSegment NameSegmentInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			NameSegment value)
		{
			return NameSegmentInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.NameID,
				value.SegmentID,
				value.NameSourceID,
				value.IsFirstOccurrence,
				value.CreationUserID,
				value.LastModifiedUserID);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from NameSegment by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="nameSegmentID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool NameSegmentDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int nameSegmentID)
		{
			return NameSegmentDeleteAuto( sqlConnection, sqlTransaction, "BHL", nameSegmentID );
		}
		
		/// <summary>
		/// Delete values from NameSegment by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="nameSegmentID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool NameSegmentDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int nameSegmentID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("NameSegmentDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("NameSegmentID", SqlDbType.Int, null, false, nameSegmentID), 
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
		/// Update values in NameSegment. Returns an object of type NameSegment.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="nameSegmentID"></param>
		/// <param name="nameID"></param>
		/// <param name="segmentID"></param>
		/// <param name="nameSourceID"></param>
		/// <param name="isFirstOccurrence"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type NameSegment.</returns>
		public NameSegment NameSegmentUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int nameSegmentID,
			int nameID,
			int segmentID,
			int nameSourceID,
			short isFirstOccurrence,
			int? lastModifiedUserID)
		{
			return NameSegmentUpdateAuto( sqlConnection, sqlTransaction, "BHL", nameSegmentID, nameID, segmentID, nameSourceID, isFirstOccurrence, lastModifiedUserID);
		}
		
		/// <summary>
		/// Update values in NameSegment. Returns an object of type NameSegment.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="nameSegmentID"></param>
		/// <param name="nameID"></param>
		/// <param name="segmentID"></param>
		/// <param name="nameSourceID"></param>
		/// <param name="isFirstOccurrence"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type NameSegment.</returns>
		public NameSegment NameSegmentUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int nameSegmentID,
			int nameID,
			int segmentID,
			int nameSourceID,
			short isFirstOccurrence,
			int? lastModifiedUserID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("NameSegmentUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("NameSegmentID", SqlDbType.Int, null, false, nameSegmentID),
					CustomSqlHelper.CreateInputParameter("NameID", SqlDbType.Int, null, false, nameID),
					CustomSqlHelper.CreateInputParameter("SegmentID", SqlDbType.Int, null, false, segmentID),
					CustomSqlHelper.CreateInputParameter("NameSourceID", SqlDbType.Int, null, false, nameSourceID),
					CustomSqlHelper.CreateInputParameter("IsFirstOccurrence", SqlDbType.SmallInt, null, false, isFirstOccurrence),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, true, lastModifiedUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<NameSegment> helper = new CustomSqlHelper<NameSegment>())
				{
					CustomGenericList<NameSegment> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						NameSegment o = list[0];
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
		/// Update values in NameSegment. Returns an object of type NameSegment.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type NameSegment.</param>
		/// <returns>Object of type NameSegment.</returns>
		public NameSegment NameSegmentUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			NameSegment value)
		{
			return NameSegmentUpdateAuto(sqlConnection, sqlTransaction, "BHL", value );
		}
		
		/// <summary>
		/// Update values in NameSegment. Returns an object of type NameSegment.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type NameSegment.</param>
		/// <returns>Object of type NameSegment.</returns>
		public NameSegment NameSegmentUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			NameSegment value)
		{
			return NameSegmentUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.NameSegmentID,
				value.NameID,
				value.SegmentID,
				value.NameSourceID,
				value.IsFirstOccurrence,
				value.LastModifiedUserID);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage NameSegment object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in NameSegment.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type NameSegment.</param>
		/// <returns>Object of type CustomDataAccessStatus<NameSegment>.</returns>
		public CustomDataAccessStatus<NameSegment> NameSegmentManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			NameSegment value , int userId )
		{
			return NameSegmentManageAuto( sqlConnection, sqlTransaction, "BHL", value , userId );
		}
		
		/// <summary>
		/// Manage NameSegment object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in NameSegment.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type NameSegment.</param>
		/// <returns>Object of type CustomDataAccessStatus<NameSegment>.</returns>
		public CustomDataAccessStatus<NameSegment> NameSegmentManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			NameSegment value , int userId )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				value.CreationUserID = userId;
				value.LastModifiedUserID = userId;
				NameSegment returnValue = NameSegmentInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.NameID,
						value.SegmentID,
						value.NameSourceID,
						value.IsFirstOccurrence,
						value.CreationUserID,
						value.LastModifiedUserID);
				
				return new CustomDataAccessStatus<NameSegment>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (NameSegmentDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.NameSegmentID))
				{
				return new CustomDataAccessStatus<NameSegment>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<NameSegment>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				value.LastModifiedUserID = userId;
				NameSegment returnValue = NameSegmentUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.NameSegmentID,
						value.NameID,
						value.SegmentID,
						value.NameSourceID,
						value.IsFirstOccurrence,
						value.LastModifiedUserID);
					
				return new CustomDataAccessStatus<NameSegment>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<NameSegment>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}
// end of source generation
