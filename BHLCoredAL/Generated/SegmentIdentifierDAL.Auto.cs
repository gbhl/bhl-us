
// Generated 9/18/2012 12:12:30 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class SegmentIdentifierDAL is based upon SegmentIdentifier.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DAL
// {
// 		public partial class SegmentIdentifierDAL
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
	partial class SegmentIdentifierDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from SegmentIdentifier by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="segmentIdentifierID"></param>
		/// <returns>Object of type SegmentIdentifier.</returns>
		public SegmentIdentifier SegmentIdentifierSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int segmentIdentifierID)
		{
			return SegmentIdentifierSelectAuto(	sqlConnection, sqlTransaction, "BHL",	segmentIdentifierID );
		}
			
		/// <summary>
		/// Select values from SegmentIdentifier by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="segmentIdentifierID"></param>
		/// <returns>Object of type SegmentIdentifier.</returns>
		public SegmentIdentifier SegmentIdentifierSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int segmentIdentifierID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("SegmentIdentifierSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("SegmentIdentifierID", SqlDbType.Int, null, false, segmentIdentifierID)))
			{
				using (CustomSqlHelper<SegmentIdentifier> helper = new CustomSqlHelper<SegmentIdentifier>())
				{
					CustomGenericList<SegmentIdentifier> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						SegmentIdentifier o = list[0];
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
		/// Select values from SegmentIdentifier by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="segmentIdentifierID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> SegmentIdentifierSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int segmentIdentifierID)
		{
			return SegmentIdentifierSelectAutoRaw( sqlConnection, sqlTransaction, "BHL", segmentIdentifierID );
		}
		
		/// <summary>
		/// Select values from SegmentIdentifier by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="segmentIdentifierID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> SegmentIdentifierSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int segmentIdentifierID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("SegmentIdentifierSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("SegmentIdentifierID", SqlDbType.Int, null, false, segmentIdentifierID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====
	
 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into SegmentIdentifier.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="segmentID"></param>
		/// <param name="identifierID"></param>
		/// <param name="identifierValue"></param>
		/// <param name="isContainerIdentifier"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type SegmentIdentifier.</returns>
		public SegmentIdentifier SegmentIdentifierInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int segmentID,
			int identifierID,
			string identifierValue,
			short? isContainerIdentifier,
			int? creationUserID,
			int? lastModifiedUserID)
		{
			return SegmentIdentifierInsertAuto( sqlConnection, sqlTransaction, "BHL", segmentID, identifierID, identifierValue, isContainerIdentifier, creationUserID, lastModifiedUserID );
		}
		
		/// <summary>
		/// Insert values into SegmentIdentifier.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="segmentID"></param>
		/// <param name="identifierID"></param>
		/// <param name="identifierValue"></param>
		/// <param name="isContainerIdentifier"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type SegmentIdentifier.</returns>
		public SegmentIdentifier SegmentIdentifierInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int segmentID,
			int identifierID,
			string identifierValue,
			short? isContainerIdentifier,
			int? creationUserID,
			int? lastModifiedUserID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("SegmentIdentifierInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("SegmentIdentifierID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("SegmentID", SqlDbType.Int, null, false, segmentID),
					CustomSqlHelper.CreateInputParameter("IdentifierID", SqlDbType.Int, null, false, identifierID),
					CustomSqlHelper.CreateInputParameter("IdentifierValue", SqlDbType.NVarChar, 125, false, identifierValue),
					CustomSqlHelper.CreateInputParameter("IsContainerIdentifier", SqlDbType.SmallInt, null, true, isContainerIdentifier),
					CustomSqlHelper.CreateInputParameter("CreationUserID", SqlDbType.Int, null, true, creationUserID),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, true, lastModifiedUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<SegmentIdentifier> helper = new CustomSqlHelper<SegmentIdentifier>())
				{
					CustomGenericList<SegmentIdentifier> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						SegmentIdentifier o = list[0];
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
		/// Insert values into SegmentIdentifier. Returns an object of type SegmentIdentifier.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type SegmentIdentifier.</param>
		/// <returns>Object of type SegmentIdentifier.</returns>
		public SegmentIdentifier SegmentIdentifierInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			SegmentIdentifier value)
		{
			return SegmentIdentifierInsertAuto(sqlConnection, sqlTransaction, "BHL", value);
		}
		
		/// <summary>
		/// Insert values into SegmentIdentifier. Returns an object of type SegmentIdentifier.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type SegmentIdentifier.</param>
		/// <returns>Object of type SegmentIdentifier.</returns>
		public SegmentIdentifier SegmentIdentifierInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			SegmentIdentifier value)
		{
			return SegmentIdentifierInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.SegmentID,
				value.IdentifierID,
				value.IdentifierValue,
				value.IsContainerIdentifier,
				value.CreationUserID,
				value.LastModifiedUserID);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from SegmentIdentifier by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="segmentIdentifierID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool SegmentIdentifierDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int segmentIdentifierID)
		{
			return SegmentIdentifierDeleteAuto( sqlConnection, sqlTransaction, "BHL", segmentIdentifierID );
		}
		
		/// <summary>
		/// Delete values from SegmentIdentifier by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="segmentIdentifierID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool SegmentIdentifierDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int segmentIdentifierID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("SegmentIdentifierDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("SegmentIdentifierID", SqlDbType.Int, null, false, segmentIdentifierID), 
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
		/// Update values in SegmentIdentifier. Returns an object of type SegmentIdentifier.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="segmentIdentifierID"></param>
		/// <param name="segmentID"></param>
		/// <param name="identifierID"></param>
		/// <param name="identifierValue"></param>
		/// <param name="isContainerIdentifier"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type SegmentIdentifier.</returns>
		public SegmentIdentifier SegmentIdentifierUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int segmentIdentifierID,
			int segmentID,
			int identifierID,
			string identifierValue,
			short? isContainerIdentifier,
			int? lastModifiedUserID)
		{
			return SegmentIdentifierUpdateAuto( sqlConnection, sqlTransaction, "BHL", segmentIdentifierID, segmentID, identifierID, identifierValue, isContainerIdentifier, lastModifiedUserID);
		}
		
		/// <summary>
		/// Update values in SegmentIdentifier. Returns an object of type SegmentIdentifier.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="segmentIdentifierID"></param>
		/// <param name="segmentID"></param>
		/// <param name="identifierID"></param>
		/// <param name="identifierValue"></param>
		/// <param name="isContainerIdentifier"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type SegmentIdentifier.</returns>
		public SegmentIdentifier SegmentIdentifierUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int segmentIdentifierID,
			int segmentID,
			int identifierID,
			string identifierValue,
			short? isContainerIdentifier,
			int? lastModifiedUserID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("SegmentIdentifierUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("SegmentIdentifierID", SqlDbType.Int, null, false, segmentIdentifierID),
					CustomSqlHelper.CreateInputParameter("SegmentID", SqlDbType.Int, null, false, segmentID),
					CustomSqlHelper.CreateInputParameter("IdentifierID", SqlDbType.Int, null, false, identifierID),
					CustomSqlHelper.CreateInputParameter("IdentifierValue", SqlDbType.NVarChar, 125, false, identifierValue),
					CustomSqlHelper.CreateInputParameter("IsContainerIdentifier", SqlDbType.SmallInt, null, true, isContainerIdentifier),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, true, lastModifiedUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<SegmentIdentifier> helper = new CustomSqlHelper<SegmentIdentifier>())
				{
					CustomGenericList<SegmentIdentifier> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						SegmentIdentifier o = list[0];
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
		/// Update values in SegmentIdentifier. Returns an object of type SegmentIdentifier.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type SegmentIdentifier.</param>
		/// <returns>Object of type SegmentIdentifier.</returns>
		public SegmentIdentifier SegmentIdentifierUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			SegmentIdentifier value)
		{
			return SegmentIdentifierUpdateAuto(sqlConnection, sqlTransaction, "BHL", value );
		}
		
		/// <summary>
		/// Update values in SegmentIdentifier. Returns an object of type SegmentIdentifier.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type SegmentIdentifier.</param>
		/// <returns>Object of type SegmentIdentifier.</returns>
		public SegmentIdentifier SegmentIdentifierUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			SegmentIdentifier value)
		{
			return SegmentIdentifierUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.SegmentIdentifierID,
				value.SegmentID,
				value.IdentifierID,
				value.IdentifierValue,
				value.IsContainerIdentifier,
				value.LastModifiedUserID);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage SegmentIdentifier object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in SegmentIdentifier.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type SegmentIdentifier.</param>
		/// <returns>Object of type CustomDataAccessStatus<SegmentIdentifier>.</returns>
		public CustomDataAccessStatus<SegmentIdentifier> SegmentIdentifierManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			SegmentIdentifier value , int userId )
		{
			return SegmentIdentifierManageAuto( sqlConnection, sqlTransaction, "BHL", value , userId );
		}
		
		/// <summary>
		/// Manage SegmentIdentifier object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in SegmentIdentifier.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type SegmentIdentifier.</param>
		/// <returns>Object of type CustomDataAccessStatus<SegmentIdentifier>.</returns>
		public CustomDataAccessStatus<SegmentIdentifier> SegmentIdentifierManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			SegmentIdentifier value , int userId )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				value.CreationUserID = userId;
				value.LastModifiedUserID = userId;
				SegmentIdentifier returnValue = SegmentIdentifierInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.SegmentID,
						value.IdentifierID,
						value.IdentifierValue,
						value.IsContainerIdentifier,
						value.CreationUserID,
						value.LastModifiedUserID);
				
				return new CustomDataAccessStatus<SegmentIdentifier>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (SegmentIdentifierDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.SegmentIdentifierID))
				{
				return new CustomDataAccessStatus<SegmentIdentifier>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<SegmentIdentifier>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				value.LastModifiedUserID = userId;
				SegmentIdentifier returnValue = SegmentIdentifierUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.SegmentIdentifierID,
						value.SegmentID,
						value.IdentifierID,
						value.IdentifierValue,
						value.IsContainerIdentifier,
						value.LastModifiedUserID);
					
				return new CustomDataAccessStatus<SegmentIdentifier>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<SegmentIdentifier>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}
// end of source generation
