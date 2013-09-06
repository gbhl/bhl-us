
// Generated 9/18/2012 12:12:30 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class SegmentKeywordDAL is based upon SegmentKeyword.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DAL
// {
// 		public partial class SegmentKeywordDAL
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
	partial class SegmentKeywordDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from SegmentKeyword by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="segmentKeywordID"></param>
		/// <returns>Object of type SegmentKeyword.</returns>
		public SegmentKeyword SegmentKeywordSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int segmentKeywordID)
		{
			return SegmentKeywordSelectAuto(	sqlConnection, sqlTransaction, "BHL",	segmentKeywordID );
		}
			
		/// <summary>
		/// Select values from SegmentKeyword by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="segmentKeywordID"></param>
		/// <returns>Object of type SegmentKeyword.</returns>
		public SegmentKeyword SegmentKeywordSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int segmentKeywordID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("SegmentKeywordSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("SegmentKeywordID", SqlDbType.Int, null, false, segmentKeywordID)))
			{
				using (CustomSqlHelper<SegmentKeyword> helper = new CustomSqlHelper<SegmentKeyword>())
				{
					CustomGenericList<SegmentKeyword> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						SegmentKeyword o = list[0];
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
		/// Select values from SegmentKeyword by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="segmentKeywordID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> SegmentKeywordSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int segmentKeywordID)
		{
			return SegmentKeywordSelectAutoRaw( sqlConnection, sqlTransaction, "BHL", segmentKeywordID );
		}
		
		/// <summary>
		/// Select values from SegmentKeyword by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="segmentKeywordID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> SegmentKeywordSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int segmentKeywordID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("SegmentKeywordSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("SegmentKeywordID", SqlDbType.Int, null, false, segmentKeywordID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====
	
 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into SegmentKeyword.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="segmentID"></param>
		/// <param name="keywordID"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type SegmentKeyword.</returns>
		public SegmentKeyword SegmentKeywordInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int segmentID,
			int keywordID,
			int? creationUserID,
			int? lastModifiedUserID)
		{
			return SegmentKeywordInsertAuto( sqlConnection, sqlTransaction, "BHL", segmentID, keywordID, creationUserID, lastModifiedUserID );
		}
		
		/// <summary>
		/// Insert values into SegmentKeyword.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="segmentID"></param>
		/// <param name="keywordID"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type SegmentKeyword.</returns>
		public SegmentKeyword SegmentKeywordInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int segmentID,
			int keywordID,
			int? creationUserID,
			int? lastModifiedUserID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("SegmentKeywordInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("SegmentKeywordID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("SegmentID", SqlDbType.Int, null, false, segmentID),
					CustomSqlHelper.CreateInputParameter("KeywordID", SqlDbType.Int, null, false, keywordID),
					CustomSqlHelper.CreateInputParameter("CreationUserID", SqlDbType.Int, null, true, creationUserID),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, true, lastModifiedUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<SegmentKeyword> helper = new CustomSqlHelper<SegmentKeyword>())
				{
					CustomGenericList<SegmentKeyword> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						SegmentKeyword o = list[0];
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
		/// Insert values into SegmentKeyword. Returns an object of type SegmentKeyword.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type SegmentKeyword.</param>
		/// <returns>Object of type SegmentKeyword.</returns>
		public SegmentKeyword SegmentKeywordInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			SegmentKeyword value)
		{
			return SegmentKeywordInsertAuto(sqlConnection, sqlTransaction, "BHL", value);
		}
		
		/// <summary>
		/// Insert values into SegmentKeyword. Returns an object of type SegmentKeyword.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type SegmentKeyword.</param>
		/// <returns>Object of type SegmentKeyword.</returns>
		public SegmentKeyword SegmentKeywordInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			SegmentKeyword value)
		{
			return SegmentKeywordInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.SegmentID,
				value.KeywordID,
				value.CreationUserID,
				value.LastModifiedUserID);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from SegmentKeyword by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="segmentKeywordID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool SegmentKeywordDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int segmentKeywordID)
		{
			return SegmentKeywordDeleteAuto( sqlConnection, sqlTransaction, "BHL", segmentKeywordID );
		}
		
		/// <summary>
		/// Delete values from SegmentKeyword by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="segmentKeywordID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool SegmentKeywordDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int segmentKeywordID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("SegmentKeywordDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("SegmentKeywordID", SqlDbType.Int, null, false, segmentKeywordID), 
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
		/// Update values in SegmentKeyword. Returns an object of type SegmentKeyword.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="segmentKeywordID"></param>
		/// <param name="segmentID"></param>
		/// <param name="keywordID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type SegmentKeyword.</returns>
		public SegmentKeyword SegmentKeywordUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int segmentKeywordID,
			int segmentID,
			int keywordID,
			int? lastModifiedUserID)
		{
			return SegmentKeywordUpdateAuto( sqlConnection, sqlTransaction, "BHL", segmentKeywordID, segmentID, keywordID, lastModifiedUserID);
		}
		
		/// <summary>
		/// Update values in SegmentKeyword. Returns an object of type SegmentKeyword.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="segmentKeywordID"></param>
		/// <param name="segmentID"></param>
		/// <param name="keywordID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type SegmentKeyword.</returns>
		public SegmentKeyword SegmentKeywordUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int segmentKeywordID,
			int segmentID,
			int keywordID,
			int? lastModifiedUserID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("SegmentKeywordUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("SegmentKeywordID", SqlDbType.Int, null, false, segmentKeywordID),
					CustomSqlHelper.CreateInputParameter("SegmentID", SqlDbType.Int, null, false, segmentID),
					CustomSqlHelper.CreateInputParameter("KeywordID", SqlDbType.Int, null, false, keywordID),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, true, lastModifiedUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<SegmentKeyword> helper = new CustomSqlHelper<SegmentKeyword>())
				{
					CustomGenericList<SegmentKeyword> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						SegmentKeyword o = list[0];
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
		/// Update values in SegmentKeyword. Returns an object of type SegmentKeyword.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type SegmentKeyword.</param>
		/// <returns>Object of type SegmentKeyword.</returns>
		public SegmentKeyword SegmentKeywordUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			SegmentKeyword value)
		{
			return SegmentKeywordUpdateAuto(sqlConnection, sqlTransaction, "BHL", value );
		}
		
		/// <summary>
		/// Update values in SegmentKeyword. Returns an object of type SegmentKeyword.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type SegmentKeyword.</param>
		/// <returns>Object of type SegmentKeyword.</returns>
		public SegmentKeyword SegmentKeywordUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			SegmentKeyword value)
		{
			return SegmentKeywordUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.SegmentKeywordID,
				value.SegmentID,
				value.KeywordID,
				value.LastModifiedUserID);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage SegmentKeyword object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in SegmentKeyword.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type SegmentKeyword.</param>
		/// <returns>Object of type CustomDataAccessStatus<SegmentKeyword>.</returns>
		public CustomDataAccessStatus<SegmentKeyword> SegmentKeywordManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			SegmentKeyword value , int userId )
		{
			return SegmentKeywordManageAuto( sqlConnection, sqlTransaction, "BHL", value , userId );
		}
		
		/// <summary>
		/// Manage SegmentKeyword object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in SegmentKeyword.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type SegmentKeyword.</param>
		/// <returns>Object of type CustomDataAccessStatus<SegmentKeyword>.</returns>
		public CustomDataAccessStatus<SegmentKeyword> SegmentKeywordManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			SegmentKeyword value , int userId )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				value.CreationUserID = userId;
				value.LastModifiedUserID = userId;
				SegmentKeyword returnValue = SegmentKeywordInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.SegmentID,
						value.KeywordID,
						value.CreationUserID,
						value.LastModifiedUserID);
				
				return new CustomDataAccessStatus<SegmentKeyword>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (SegmentKeywordDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.SegmentKeywordID))
				{
				return new CustomDataAccessStatus<SegmentKeyword>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<SegmentKeyword>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				value.LastModifiedUserID = userId;
				SegmentKeyword returnValue = SegmentKeywordUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.SegmentKeywordID,
						value.SegmentID,
						value.KeywordID,
						value.LastModifiedUserID);
					
				return new CustomDataAccessStatus<SegmentKeyword>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<SegmentKeyword>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}
// end of source generation
