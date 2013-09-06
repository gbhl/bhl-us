
// Generated 9/18/2012 12:12:30 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class SegmentAuthorDAL is based upon SegmentAuthor.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DAL
// {
// 		public partial class SegmentAuthorDAL
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
	partial class SegmentAuthorDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from SegmentAuthor by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="segmentAuthorID"></param>
		/// <returns>Object of type SegmentAuthor.</returns>
		public SegmentAuthor SegmentAuthorSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int segmentAuthorID)
		{
			return SegmentAuthorSelectAuto(	sqlConnection, sqlTransaction, "BHL",	segmentAuthorID );
		}
			
		/// <summary>
		/// Select values from SegmentAuthor by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="segmentAuthorID"></param>
		/// <returns>Object of type SegmentAuthor.</returns>
		public SegmentAuthor SegmentAuthorSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int segmentAuthorID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("SegmentAuthorSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("SegmentAuthorID", SqlDbType.Int, null, false, segmentAuthorID)))
			{
				using (CustomSqlHelper<SegmentAuthor> helper = new CustomSqlHelper<SegmentAuthor>())
				{
					CustomGenericList<SegmentAuthor> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						SegmentAuthor o = list[0];
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
		/// Select values from SegmentAuthor by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="segmentAuthorID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> SegmentAuthorSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int segmentAuthorID)
		{
			return SegmentAuthorSelectAutoRaw( sqlConnection, sqlTransaction, "BHL", segmentAuthorID );
		}
		
		/// <summary>
		/// Select values from SegmentAuthor by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="segmentAuthorID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> SegmentAuthorSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int segmentAuthorID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("SegmentAuthorSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("SegmentAuthorID", SqlDbType.Int, null, false, segmentAuthorID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====
	
 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into SegmentAuthor.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="segmentID"></param>
		/// <param name="authorID"></param>
		/// <param name="sequenceOrder"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type SegmentAuthor.</returns>
		public SegmentAuthor SegmentAuthorInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int segmentID,
			int authorID,
			short sequenceOrder,
			int? creationUserID,
			int? lastModifiedUserID)
		{
			return SegmentAuthorInsertAuto( sqlConnection, sqlTransaction, "BHL", segmentID, authorID, sequenceOrder, creationUserID, lastModifiedUserID );
		}
		
		/// <summary>
		/// Insert values into SegmentAuthor.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="segmentID"></param>
		/// <param name="authorID"></param>
		/// <param name="sequenceOrder"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type SegmentAuthor.</returns>
		public SegmentAuthor SegmentAuthorInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int segmentID,
			int authorID,
			short sequenceOrder,
			int? creationUserID,
			int? lastModifiedUserID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("SegmentAuthorInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("SegmentAuthorID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("SegmentID", SqlDbType.Int, null, false, segmentID),
					CustomSqlHelper.CreateInputParameter("AuthorID", SqlDbType.Int, null, false, authorID),
					CustomSqlHelper.CreateInputParameter("SequenceOrder", SqlDbType.SmallInt, null, false, sequenceOrder),
					CustomSqlHelper.CreateInputParameter("CreationUserID", SqlDbType.Int, null, true, creationUserID),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, true, lastModifiedUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<SegmentAuthor> helper = new CustomSqlHelper<SegmentAuthor>())
				{
					CustomGenericList<SegmentAuthor> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						SegmentAuthor o = list[0];
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
		/// Insert values into SegmentAuthor. Returns an object of type SegmentAuthor.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type SegmentAuthor.</param>
		/// <returns>Object of type SegmentAuthor.</returns>
		public SegmentAuthor SegmentAuthorInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			SegmentAuthor value)
		{
			return SegmentAuthorInsertAuto(sqlConnection, sqlTransaction, "BHL", value);
		}
		
		/// <summary>
		/// Insert values into SegmentAuthor. Returns an object of type SegmentAuthor.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type SegmentAuthor.</param>
		/// <returns>Object of type SegmentAuthor.</returns>
		public SegmentAuthor SegmentAuthorInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			SegmentAuthor value)
		{
			return SegmentAuthorInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.SegmentID,
				value.AuthorID,
				value.SequenceOrder,
				value.CreationUserID,
				value.LastModifiedUserID);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from SegmentAuthor by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="segmentAuthorID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool SegmentAuthorDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int segmentAuthorID)
		{
			return SegmentAuthorDeleteAuto( sqlConnection, sqlTransaction, "BHL", segmentAuthorID );
		}
		
		/// <summary>
		/// Delete values from SegmentAuthor by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="segmentAuthorID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool SegmentAuthorDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int segmentAuthorID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("SegmentAuthorDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("SegmentAuthorID", SqlDbType.Int, null, false, segmentAuthorID), 
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
		/// Update values in SegmentAuthor. Returns an object of type SegmentAuthor.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="segmentAuthorID"></param>
		/// <param name="segmentID"></param>
		/// <param name="authorID"></param>
		/// <param name="sequenceOrder"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type SegmentAuthor.</returns>
		public SegmentAuthor SegmentAuthorUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int segmentAuthorID,
			int segmentID,
			int authorID,
			short sequenceOrder,
			int? lastModifiedUserID)
		{
			return SegmentAuthorUpdateAuto( sqlConnection, sqlTransaction, "BHL", segmentAuthorID, segmentID, authorID, sequenceOrder, lastModifiedUserID);
		}
		
		/// <summary>
		/// Update values in SegmentAuthor. Returns an object of type SegmentAuthor.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="segmentAuthorID"></param>
		/// <param name="segmentID"></param>
		/// <param name="authorID"></param>
		/// <param name="sequenceOrder"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type SegmentAuthor.</returns>
		public SegmentAuthor SegmentAuthorUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int segmentAuthorID,
			int segmentID,
			int authorID,
			short sequenceOrder,
			int? lastModifiedUserID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("SegmentAuthorUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("SegmentAuthorID", SqlDbType.Int, null, false, segmentAuthorID),
					CustomSqlHelper.CreateInputParameter("SegmentID", SqlDbType.Int, null, false, segmentID),
					CustomSqlHelper.CreateInputParameter("AuthorID", SqlDbType.Int, null, false, authorID),
					CustomSqlHelper.CreateInputParameter("SequenceOrder", SqlDbType.SmallInt, null, false, sequenceOrder),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, true, lastModifiedUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<SegmentAuthor> helper = new CustomSqlHelper<SegmentAuthor>())
				{
					CustomGenericList<SegmentAuthor> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						SegmentAuthor o = list[0];
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
		/// Update values in SegmentAuthor. Returns an object of type SegmentAuthor.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type SegmentAuthor.</param>
		/// <returns>Object of type SegmentAuthor.</returns>
		public SegmentAuthor SegmentAuthorUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			SegmentAuthor value)
		{
			return SegmentAuthorUpdateAuto(sqlConnection, sqlTransaction, "BHL", value );
		}
		
		/// <summary>
		/// Update values in SegmentAuthor. Returns an object of type SegmentAuthor.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type SegmentAuthor.</param>
		/// <returns>Object of type SegmentAuthor.</returns>
		public SegmentAuthor SegmentAuthorUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			SegmentAuthor value)
		{
			return SegmentAuthorUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.SegmentAuthorID,
				value.SegmentID,
				value.AuthorID,
				value.SequenceOrder,
				value.LastModifiedUserID);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage SegmentAuthor object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in SegmentAuthor.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type SegmentAuthor.</param>
		/// <returns>Object of type CustomDataAccessStatus<SegmentAuthor>.</returns>
		public CustomDataAccessStatus<SegmentAuthor> SegmentAuthorManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			SegmentAuthor value , int userId )
		{
			return SegmentAuthorManageAuto( sqlConnection, sqlTransaction, "BHL", value , userId );
		}
		
		/// <summary>
		/// Manage SegmentAuthor object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in SegmentAuthor.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type SegmentAuthor.</param>
		/// <returns>Object of type CustomDataAccessStatus<SegmentAuthor>.</returns>
		public CustomDataAccessStatus<SegmentAuthor> SegmentAuthorManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			SegmentAuthor value , int userId )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				value.CreationUserID = userId;
				value.LastModifiedUserID = userId;
				SegmentAuthor returnValue = SegmentAuthorInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.SegmentID,
						value.AuthorID,
						value.SequenceOrder,
						value.CreationUserID,
						value.LastModifiedUserID);
				
				return new CustomDataAccessStatus<SegmentAuthor>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (SegmentAuthorDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.SegmentAuthorID))
				{
				return new CustomDataAccessStatus<SegmentAuthor>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<SegmentAuthor>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				value.LastModifiedUserID = userId;
				SegmentAuthor returnValue = SegmentAuthorUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.SegmentAuthorID,
						value.SegmentID,
						value.AuthorID,
						value.SequenceOrder,
						value.LastModifiedUserID);
					
				return new CustomDataAccessStatus<SegmentAuthor>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<SegmentAuthor>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}
// end of source generation
