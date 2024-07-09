
// Generated 7/9/2024 11:03:14 AM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class SegmentExternalResourceDAL is based upon dbo.SegmentExternalResource.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DAL
// {
// 		public partial class SegmentExternalResourceDAL
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
	partial class SegmentExternalResourceDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from dbo.SegmentExternalResource by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="segmentExternalResourceID"></param>
		/// <returns>Object of type SegmentExternalResource.</returns>
		public SegmentExternalResource SegmentExternalResourceSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int segmentExternalResourceID)
		{
			return SegmentExternalResourceSelectAuto(	sqlConnection, sqlTransaction, "BHL",	segmentExternalResourceID );
		}
			
		/// <summary>
		/// Select values from dbo.SegmentExternalResource by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="segmentExternalResourceID"></param>
		/// <returns>Object of type SegmentExternalResource.</returns>
		public SegmentExternalResource SegmentExternalResourceSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int segmentExternalResourceID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("SegmentExternalResourceSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("SegmentExternalResourceID", SqlDbType.Int, null, false, segmentExternalResourceID)))
			{
				using (CustomSqlHelper<SegmentExternalResource> helper = new CustomSqlHelper<SegmentExternalResource>())
				{
					List<SegmentExternalResource> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						SegmentExternalResource o = list[0];
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
		/// Select values from dbo.SegmentExternalResource by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="segmentExternalResourceID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> SegmentExternalResourceSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int segmentExternalResourceID)
		{
			return SegmentExternalResourceSelectAutoRaw( sqlConnection, sqlTransaction, "BHL", segmentExternalResourceID );
		}
		
		/// <summary>
		/// Select values from dbo.SegmentExternalResource by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="segmentExternalResourceID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> SegmentExternalResourceSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int segmentExternalResourceID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("SegmentExternalResourceSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("SegmentExternalResourceID", SqlDbType.Int, null, false, segmentExternalResourceID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====

 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into dbo.SegmentExternalResource.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="segmentID"></param>
		/// <param name="externalResourceTypeID"></param>
		/// <param name="urlText"></param>
		/// <param name="url"></param>
		/// <param name="sequenceOrder"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type SegmentExternalResource.</returns>
		public SegmentExternalResource SegmentExternalResourceInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int segmentID,
			int externalResourceTypeID,
			string urlText,
			string url,
			short sequenceOrder,
			int creationUserID,
			int lastModifiedUserID)
		{
			return SegmentExternalResourceInsertAuto( sqlConnection, sqlTransaction, "BHL", segmentID, externalResourceTypeID, urlText, url, sequenceOrder, creationUserID, lastModifiedUserID );
		}
		
		/// <summary>
		/// Insert values into dbo.SegmentExternalResource.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="segmentID"></param>
		/// <param name="externalResourceTypeID"></param>
		/// <param name="urlText"></param>
		/// <param name="url"></param>
		/// <param name="sequenceOrder"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type SegmentExternalResource.</returns>
		public SegmentExternalResource SegmentExternalResourceInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int segmentID,
			int externalResourceTypeID,
			string urlText,
			string url,
			short sequenceOrder,
			int creationUserID,
			int lastModifiedUserID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("SegmentExternalResourceInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("SegmentExternalResourceID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("SegmentID", SqlDbType.Int, null, false, segmentID),
					CustomSqlHelper.CreateInputParameter("ExternalResourceTypeID", SqlDbType.Int, null, false, externalResourceTypeID),
					CustomSqlHelper.CreateInputParameter("UrlText", SqlDbType.NVarChar, 100, false, urlText),
					CustomSqlHelper.CreateInputParameter("Url", SqlDbType.NVarChar, 200, false, url),
					CustomSqlHelper.CreateInputParameter("SequenceOrder", SqlDbType.SmallInt, null, false, sequenceOrder),
					CustomSqlHelper.CreateInputParameter("CreationUserID", SqlDbType.Int, null, false, creationUserID),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, false, lastModifiedUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<SegmentExternalResource> helper = new CustomSqlHelper<SegmentExternalResource>())
				{
					List<SegmentExternalResource> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						SegmentExternalResource o = list[0];
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
		/// Insert values into dbo.SegmentExternalResource. Returns an object of type SegmentExternalResource.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type SegmentExternalResource.</param>
		/// <returns>Object of type SegmentExternalResource.</returns>
		public SegmentExternalResource SegmentExternalResourceInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			SegmentExternalResource value)
		{
			return SegmentExternalResourceInsertAuto(sqlConnection, sqlTransaction, "BHL", value);
		}
		
		/// <summary>
		/// Insert values into dbo.SegmentExternalResource. Returns an object of type SegmentExternalResource.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type SegmentExternalResource.</param>
		/// <returns>Object of type SegmentExternalResource.</returns>
		public SegmentExternalResource SegmentExternalResourceInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			SegmentExternalResource value)
		{
			return SegmentExternalResourceInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.SegmentID,
				value.ExternalResourceTypeID,
				value.UrlText,
				value.Url,
				value.SequenceOrder,
				value.CreationUserID,
				value.LastModifiedUserID);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from dbo.SegmentExternalResource by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="segmentExternalResourceID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool SegmentExternalResourceDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int segmentExternalResourceID)
		{
			return SegmentExternalResourceDeleteAuto( sqlConnection, sqlTransaction, "BHL", segmentExternalResourceID );
		}
		
		/// <summary>
		/// Delete values from dbo.SegmentExternalResource by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="segmentExternalResourceID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool SegmentExternalResourceDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int segmentExternalResourceID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("SegmentExternalResourceDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("SegmentExternalResourceID", SqlDbType.Int, null, false, segmentExternalResourceID), 
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
		/// Update values in dbo.SegmentExternalResource. Returns an object of type SegmentExternalResource.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="segmentExternalResourceID"></param>
		/// <param name="segmentID"></param>
		/// <param name="externalResourceTypeID"></param>
		/// <param name="urlText"></param>
		/// <param name="url"></param>
		/// <param name="sequenceOrder"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type SegmentExternalResource.</returns>
		public SegmentExternalResource SegmentExternalResourceUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int segmentExternalResourceID,
			int segmentID,
			int externalResourceTypeID,
			string urlText,
			string url,
			short sequenceOrder,
			int lastModifiedUserID)
		{
			return SegmentExternalResourceUpdateAuto( sqlConnection, sqlTransaction, "BHL", segmentExternalResourceID, segmentID, externalResourceTypeID, urlText, url, sequenceOrder, lastModifiedUserID);
		}
		
		/// <summary>
		/// Update values in dbo.SegmentExternalResource. Returns an object of type SegmentExternalResource.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="segmentExternalResourceID"></param>
		/// <param name="segmentID"></param>
		/// <param name="externalResourceTypeID"></param>
		/// <param name="urlText"></param>
		/// <param name="url"></param>
		/// <param name="sequenceOrder"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type SegmentExternalResource.</returns>
		public SegmentExternalResource SegmentExternalResourceUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int segmentExternalResourceID,
			int segmentID,
			int externalResourceTypeID,
			string urlText,
			string url,
			short sequenceOrder,
			int lastModifiedUserID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("SegmentExternalResourceUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("SegmentExternalResourceID", SqlDbType.Int, null, false, segmentExternalResourceID),
					CustomSqlHelper.CreateInputParameter("SegmentID", SqlDbType.Int, null, false, segmentID),
					CustomSqlHelper.CreateInputParameter("ExternalResourceTypeID", SqlDbType.Int, null, false, externalResourceTypeID),
					CustomSqlHelper.CreateInputParameter("UrlText", SqlDbType.NVarChar, 100, false, urlText),
					CustomSqlHelper.CreateInputParameter("Url", SqlDbType.NVarChar, 200, false, url),
					CustomSqlHelper.CreateInputParameter("SequenceOrder", SqlDbType.SmallInt, null, false, sequenceOrder),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, false, lastModifiedUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<SegmentExternalResource> helper = new CustomSqlHelper<SegmentExternalResource>())
				{
					List<SegmentExternalResource> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						SegmentExternalResource o = list[0];
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
		/// Update values in dbo.SegmentExternalResource. Returns an object of type SegmentExternalResource.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type SegmentExternalResource.</param>
		/// <returns>Object of type SegmentExternalResource.</returns>
		public SegmentExternalResource SegmentExternalResourceUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			SegmentExternalResource value)
		{
			return SegmentExternalResourceUpdateAuto(sqlConnection, sqlTransaction, "BHL", value );
		}
		
		/// <summary>
		/// Update values in dbo.SegmentExternalResource. Returns an object of type SegmentExternalResource.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type SegmentExternalResource.</param>
		/// <returns>Object of type SegmentExternalResource.</returns>
		public SegmentExternalResource SegmentExternalResourceUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			SegmentExternalResource value)
		{
			return SegmentExternalResourceUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.SegmentExternalResourceID,
				value.SegmentID,
				value.ExternalResourceTypeID,
				value.UrlText,
				value.Url,
				value.SequenceOrder,
				value.LastModifiedUserID);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage dbo.SegmentExternalResource object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.SegmentExternalResource.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type SegmentExternalResource.</param>
		/// <returns>Object of type CustomDataAccessStatus<SegmentExternalResource>.</returns>
		public CustomDataAccessStatus<SegmentExternalResource> SegmentExternalResourceManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			SegmentExternalResource value , int userId )
		{
			return SegmentExternalResourceManageAuto( sqlConnection, sqlTransaction, "BHL", value , userId );
		}
		
		/// <summary>
		/// Manage dbo.SegmentExternalResource object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.SegmentExternalResource.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type SegmentExternalResource.</param>
		/// <returns>Object of type CustomDataAccessStatus<SegmentExternalResource>.</returns>
		public CustomDataAccessStatus<SegmentExternalResource> SegmentExternalResourceManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			SegmentExternalResource value , int userId )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				value.CreationUserID = userId;
				value.LastModifiedUserID = userId;
				SegmentExternalResource returnValue = SegmentExternalResourceInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.SegmentID,
						value.ExternalResourceTypeID,
						value.UrlText,
						value.Url,
						value.SequenceOrder,
						value.CreationUserID,
						value.LastModifiedUserID);
				
				return new CustomDataAccessStatus<SegmentExternalResource>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (SegmentExternalResourceDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.SegmentExternalResourceID))
				{
				return new CustomDataAccessStatus<SegmentExternalResource>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<SegmentExternalResource>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				value.LastModifiedUserID = userId;
				SegmentExternalResource returnValue = SegmentExternalResourceUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.SegmentExternalResourceID,
						value.SegmentID,
						value.ExternalResourceTypeID,
						value.UrlText,
						value.Url,
						value.SequenceOrder,
						value.LastModifiedUserID);
					
				return new CustomDataAccessStatus<SegmentExternalResource>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<SegmentExternalResource>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}

