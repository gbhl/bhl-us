
// Generated 9/18/2012 12:12:30 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class SegmentPageDAL is based upon SegmentPage.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DAL
// {
// 		public partial class SegmentPageDAL
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
	partial class SegmentPageDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from SegmentPage by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="segmentPageID"></param>
		/// <returns>Object of type SegmentPage.</returns>
		public SegmentPage SegmentPageSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int segmentPageID)
		{
			return SegmentPageSelectAuto(	sqlConnection, sqlTransaction, "BHL",	segmentPageID );
		}
			
		/// <summary>
		/// Select values from SegmentPage by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="segmentPageID"></param>
		/// <returns>Object of type SegmentPage.</returns>
		public SegmentPage SegmentPageSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int segmentPageID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("SegmentPageSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("SegmentPageID", SqlDbType.Int, null, false, segmentPageID)))
			{
				using (CustomSqlHelper<SegmentPage> helper = new CustomSqlHelper<SegmentPage>())
				{
					CustomGenericList<SegmentPage> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						SegmentPage o = list[0];
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
		/// Select values from SegmentPage by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="segmentPageID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> SegmentPageSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int segmentPageID)
		{
			return SegmentPageSelectAutoRaw( sqlConnection, sqlTransaction, "BHL", segmentPageID );
		}
		
		/// <summary>
		/// Select values from SegmentPage by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="segmentPageID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> SegmentPageSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int segmentPageID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("SegmentPageSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("SegmentPageID", SqlDbType.Int, null, false, segmentPageID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====
	
 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into SegmentPage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="segmentID"></param>
		/// <param name="pageID"></param>
		/// <param name="sequenceOrder"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type SegmentPage.</returns>
		public SegmentPage SegmentPageInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int segmentID,
			int pageID,
			short sequenceOrder,
			int? creationUserID,
			int? lastModifiedUserID)
		{
			return SegmentPageInsertAuto( sqlConnection, sqlTransaction, "BHL", segmentID, pageID, sequenceOrder, creationUserID, lastModifiedUserID );
		}
		
		/// <summary>
		/// Insert values into SegmentPage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="segmentID"></param>
		/// <param name="pageID"></param>
		/// <param name="sequenceOrder"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type SegmentPage.</returns>
		public SegmentPage SegmentPageInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int segmentID,
			int pageID,
			short sequenceOrder,
			int? creationUserID,
			int? lastModifiedUserID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("SegmentPageInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("SegmentPageID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("SegmentID", SqlDbType.Int, null, false, segmentID),
					CustomSqlHelper.CreateInputParameter("PageID", SqlDbType.Int, null, false, pageID),
					CustomSqlHelper.CreateInputParameter("SequenceOrder", SqlDbType.SmallInt, null, false, sequenceOrder),
					CustomSqlHelper.CreateInputParameter("CreationUserID", SqlDbType.Int, null, true, creationUserID),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, true, lastModifiedUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<SegmentPage> helper = new CustomSqlHelper<SegmentPage>())
				{
					CustomGenericList<SegmentPage> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						SegmentPage o = list[0];
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
		/// Insert values into SegmentPage. Returns an object of type SegmentPage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type SegmentPage.</param>
		/// <returns>Object of type SegmentPage.</returns>
		public SegmentPage SegmentPageInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			SegmentPage value)
		{
			return SegmentPageInsertAuto(sqlConnection, sqlTransaction, "BHL", value);
		}
		
		/// <summary>
		/// Insert values into SegmentPage. Returns an object of type SegmentPage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type SegmentPage.</param>
		/// <returns>Object of type SegmentPage.</returns>
		public SegmentPage SegmentPageInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			SegmentPage value)
		{
			return SegmentPageInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.SegmentID,
				value.PageID,
				value.SequenceOrder,
				value.CreationUserID,
				value.LastModifiedUserID);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from SegmentPage by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="segmentPageID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool SegmentPageDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int segmentPageID)
		{
			return SegmentPageDeleteAuto( sqlConnection, sqlTransaction, "BHL", segmentPageID );
		}
		
		/// <summary>
		/// Delete values from SegmentPage by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="segmentPageID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool SegmentPageDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int segmentPageID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("SegmentPageDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("SegmentPageID", SqlDbType.Int, null, false, segmentPageID), 
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
		/// Update values in SegmentPage. Returns an object of type SegmentPage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="segmentPageID"></param>
		/// <param name="segmentID"></param>
		/// <param name="pageID"></param>
		/// <param name="sequenceOrder"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type SegmentPage.</returns>
		public SegmentPage SegmentPageUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int segmentPageID,
			int segmentID,
			int pageID,
			short sequenceOrder,
			int? lastModifiedUserID)
		{
			return SegmentPageUpdateAuto( sqlConnection, sqlTransaction, "BHL", segmentPageID, segmentID, pageID, sequenceOrder, lastModifiedUserID);
		}
		
		/// <summary>
		/// Update values in SegmentPage. Returns an object of type SegmentPage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="segmentPageID"></param>
		/// <param name="segmentID"></param>
		/// <param name="pageID"></param>
		/// <param name="sequenceOrder"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type SegmentPage.</returns>
		public SegmentPage SegmentPageUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int segmentPageID,
			int segmentID,
			int pageID,
			short sequenceOrder,
			int? lastModifiedUserID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("SegmentPageUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("SegmentPageID", SqlDbType.Int, null, false, segmentPageID),
					CustomSqlHelper.CreateInputParameter("SegmentID", SqlDbType.Int, null, false, segmentID),
					CustomSqlHelper.CreateInputParameter("PageID", SqlDbType.Int, null, false, pageID),
					CustomSqlHelper.CreateInputParameter("SequenceOrder", SqlDbType.SmallInt, null, false, sequenceOrder),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, true, lastModifiedUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<SegmentPage> helper = new CustomSqlHelper<SegmentPage>())
				{
					CustomGenericList<SegmentPage> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						SegmentPage o = list[0];
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
		/// Update values in SegmentPage. Returns an object of type SegmentPage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type SegmentPage.</param>
		/// <returns>Object of type SegmentPage.</returns>
		public SegmentPage SegmentPageUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			SegmentPage value)
		{
			return SegmentPageUpdateAuto(sqlConnection, sqlTransaction, "BHL", value );
		}
		
		/// <summary>
		/// Update values in SegmentPage. Returns an object of type SegmentPage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type SegmentPage.</param>
		/// <returns>Object of type SegmentPage.</returns>
		public SegmentPage SegmentPageUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			SegmentPage value)
		{
			return SegmentPageUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.SegmentPageID,
				value.SegmentID,
				value.PageID,
				value.SequenceOrder,
				value.LastModifiedUserID);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage SegmentPage object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in SegmentPage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type SegmentPage.</param>
		/// <returns>Object of type CustomDataAccessStatus<SegmentPage>.</returns>
		public CustomDataAccessStatus<SegmentPage> SegmentPageManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			SegmentPage value , int userId )
		{
			return SegmentPageManageAuto( sqlConnection, sqlTransaction, "BHL", value , userId );
		}
		
		/// <summary>
		/// Manage SegmentPage object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in SegmentPage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type SegmentPage.</param>
		/// <returns>Object of type CustomDataAccessStatus<SegmentPage>.</returns>
		public CustomDataAccessStatus<SegmentPage> SegmentPageManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			SegmentPage value , int userId )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				value.CreationUserID = userId;
				value.LastModifiedUserID = userId;
				SegmentPage returnValue = SegmentPageInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.SegmentID,
						value.PageID,
						value.SequenceOrder,
						value.CreationUserID,
						value.LastModifiedUserID);
				
				return new CustomDataAccessStatus<SegmentPage>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (SegmentPageDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.SegmentPageID))
				{
				return new CustomDataAccessStatus<SegmentPage>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<SegmentPage>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				value.LastModifiedUserID = userId;
				SegmentPage returnValue = SegmentPageUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.SegmentPageID,
						value.SegmentID,
						value.PageID,
						value.SequenceOrder,
						value.LastModifiedUserID);
					
				return new CustomDataAccessStatus<SegmentPage>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<SegmentPage>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}
// end of source generation
