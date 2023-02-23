
// Generated 2/23/2023 1:49:30 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class TitleExternalResourceDAL is based upon dbo.TitleExternalResource.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DAL
// {
// 		public partial class TitleExternalResourceDAL
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
	partial class TitleExternalResourceDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from dbo.TitleExternalResource by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="titleExternalResourceID"></param>
		/// <returns>Object of type TitleExternalResource.</returns>
		public TitleExternalResource TitleExternalResourceSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int titleExternalResourceID)
		{
			return TitleExternalResourceSelectAuto(	sqlConnection, sqlTransaction, "BHL",	titleExternalResourceID );
		}
			
		/// <summary>
		/// Select values from dbo.TitleExternalResource by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="titleExternalResourceID"></param>
		/// <returns>Object of type TitleExternalResource.</returns>
		public TitleExternalResource TitleExternalResourceSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int titleExternalResourceID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleExternalResourceSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("TitleExternalResourceID", SqlDbType.Int, null, false, titleExternalResourceID)))
			{
				using (CustomSqlHelper<TitleExternalResource> helper = new CustomSqlHelper<TitleExternalResource>())
				{
					List<TitleExternalResource> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						TitleExternalResource o = list[0];
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
		/// Select values from dbo.TitleExternalResource by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="titleExternalResourceID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> TitleExternalResourceSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int titleExternalResourceID)
		{
			return TitleExternalResourceSelectAutoRaw( sqlConnection, sqlTransaction, "BHL", titleExternalResourceID );
		}
		
		/// <summary>
		/// Select values from dbo.TitleExternalResource by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="titleExternalResourceID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> TitleExternalResourceSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int titleExternalResourceID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleExternalResourceSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("TitleExternalResourceID", SqlDbType.Int, null, false, titleExternalResourceID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====

 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into dbo.TitleExternalResource.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="titleID"></param>
		/// <param name="titleExternalResourceTypeID"></param>
		/// <param name="urlText"></param>
		/// <param name="url"></param>
		/// <param name="sequenceOrder"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type TitleExternalResource.</returns>
		public TitleExternalResource TitleExternalResourceInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int titleID,
			int titleExternalResourceTypeID,
			string urlText,
			string url,
			short sequenceOrder,
			int creationUserID,
			int lastModifiedUserID)
		{
			return TitleExternalResourceInsertAuto( sqlConnection, sqlTransaction, "BHL", titleID, titleExternalResourceTypeID, urlText, url, sequenceOrder, creationUserID, lastModifiedUserID );
		}
		
		/// <summary>
		/// Insert values into dbo.TitleExternalResource.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="titleID"></param>
		/// <param name="titleExternalResourceTypeID"></param>
		/// <param name="urlText"></param>
		/// <param name="url"></param>
		/// <param name="sequenceOrder"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type TitleExternalResource.</returns>
		public TitleExternalResource TitleExternalResourceInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int titleID,
			int titleExternalResourceTypeID,
			string urlText,
			string url,
			short sequenceOrder,
			int creationUserID,
			int lastModifiedUserID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleExternalResourceInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("TitleExternalResourceID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("TitleID", SqlDbType.Int, null, false, titleID),
					CustomSqlHelper.CreateInputParameter("TitleExternalResourceTypeID", SqlDbType.Int, null, false, titleExternalResourceTypeID),
					CustomSqlHelper.CreateInputParameter("UrlText", SqlDbType.NVarChar, 100, false, urlText),
					CustomSqlHelper.CreateInputParameter("Url", SqlDbType.NVarChar, 200, false, url),
					CustomSqlHelper.CreateInputParameter("SequenceOrder", SqlDbType.SmallInt, null, false, sequenceOrder),
					CustomSqlHelper.CreateInputParameter("CreationUserID", SqlDbType.Int, null, false, creationUserID),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, false, lastModifiedUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<TitleExternalResource> helper = new CustomSqlHelper<TitleExternalResource>())
				{
					List<TitleExternalResource> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						TitleExternalResource o = list[0];
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
		/// Insert values into dbo.TitleExternalResource. Returns an object of type TitleExternalResource.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type TitleExternalResource.</param>
		/// <returns>Object of type TitleExternalResource.</returns>
		public TitleExternalResource TitleExternalResourceInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			TitleExternalResource value)
		{
			return TitleExternalResourceInsertAuto(sqlConnection, sqlTransaction, "BHL", value);
		}
		
		/// <summary>
		/// Insert values into dbo.TitleExternalResource. Returns an object of type TitleExternalResource.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type TitleExternalResource.</param>
		/// <returns>Object of type TitleExternalResource.</returns>
		public TitleExternalResource TitleExternalResourceInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			TitleExternalResource value)
		{
			return TitleExternalResourceInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.TitleID,
				value.TitleExternalResourceTypeID,
				value.UrlText,
				value.Url,
				value.SequenceOrder,
				value.CreationUserID,
				value.LastModifiedUserID);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from dbo.TitleExternalResource by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="titleExternalResourceID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool TitleExternalResourceDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int titleExternalResourceID)
		{
			return TitleExternalResourceDeleteAuto( sqlConnection, sqlTransaction, "BHL", titleExternalResourceID );
		}
		
		/// <summary>
		/// Delete values from dbo.TitleExternalResource by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="titleExternalResourceID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool TitleExternalResourceDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int titleExternalResourceID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleExternalResourceDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("TitleExternalResourceID", SqlDbType.Int, null, false, titleExternalResourceID), 
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
		/// Update values in dbo.TitleExternalResource. Returns an object of type TitleExternalResource.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="titleExternalResourceID"></param>
		/// <param name="titleID"></param>
		/// <param name="titleExternalResourceTypeID"></param>
		/// <param name="urlText"></param>
		/// <param name="url"></param>
		/// <param name="sequenceOrder"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type TitleExternalResource.</returns>
		public TitleExternalResource TitleExternalResourceUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int titleExternalResourceID,
			int titleID,
			int titleExternalResourceTypeID,
			string urlText,
			string url,
			short sequenceOrder,
			int lastModifiedUserID)
		{
			return TitleExternalResourceUpdateAuto( sqlConnection, sqlTransaction, "BHL", titleExternalResourceID, titleID, titleExternalResourceTypeID, urlText, url, sequenceOrder, lastModifiedUserID);
		}
		
		/// <summary>
		/// Update values in dbo.TitleExternalResource. Returns an object of type TitleExternalResource.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="titleExternalResourceID"></param>
		/// <param name="titleID"></param>
		/// <param name="titleExternalResourceTypeID"></param>
		/// <param name="urlText"></param>
		/// <param name="url"></param>
		/// <param name="sequenceOrder"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type TitleExternalResource.</returns>
		public TitleExternalResource TitleExternalResourceUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int titleExternalResourceID,
			int titleID,
			int titleExternalResourceTypeID,
			string urlText,
			string url,
			short sequenceOrder,
			int lastModifiedUserID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleExternalResourceUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("TitleExternalResourceID", SqlDbType.Int, null, false, titleExternalResourceID),
					CustomSqlHelper.CreateInputParameter("TitleID", SqlDbType.Int, null, false, titleID),
					CustomSqlHelper.CreateInputParameter("TitleExternalResourceTypeID", SqlDbType.Int, null, false, titleExternalResourceTypeID),
					CustomSqlHelper.CreateInputParameter("UrlText", SqlDbType.NVarChar, 100, false, urlText),
					CustomSqlHelper.CreateInputParameter("Url", SqlDbType.NVarChar, 200, false, url),
					CustomSqlHelper.CreateInputParameter("SequenceOrder", SqlDbType.SmallInt, null, false, sequenceOrder),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, false, lastModifiedUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<TitleExternalResource> helper = new CustomSqlHelper<TitleExternalResource>())
				{
					List<TitleExternalResource> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						TitleExternalResource o = list[0];
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
		/// Update values in dbo.TitleExternalResource. Returns an object of type TitleExternalResource.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type TitleExternalResource.</param>
		/// <returns>Object of type TitleExternalResource.</returns>
		public TitleExternalResource TitleExternalResourceUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			TitleExternalResource value)
		{
			return TitleExternalResourceUpdateAuto(sqlConnection, sqlTransaction, "BHL", value );
		}
		
		/// <summary>
		/// Update values in dbo.TitleExternalResource. Returns an object of type TitleExternalResource.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type TitleExternalResource.</param>
		/// <returns>Object of type TitleExternalResource.</returns>
		public TitleExternalResource TitleExternalResourceUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			TitleExternalResource value)
		{
			return TitleExternalResourceUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.TitleExternalResourceID,
				value.TitleID,
				value.TitleExternalResourceTypeID,
				value.UrlText,
				value.Url,
				value.SequenceOrder,
				value.LastModifiedUserID);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage dbo.TitleExternalResource object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.TitleExternalResource.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type TitleExternalResource.</param>
		/// <returns>Object of type CustomDataAccessStatus<TitleExternalResource>.</returns>
		public CustomDataAccessStatus<TitleExternalResource> TitleExternalResourceManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			TitleExternalResource value , int userId )
		{
			return TitleExternalResourceManageAuto( sqlConnection, sqlTransaction, "BHL", value , userId );
		}
		
		/// <summary>
		/// Manage dbo.TitleExternalResource object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.TitleExternalResource.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type TitleExternalResource.</param>
		/// <returns>Object of type CustomDataAccessStatus<TitleExternalResource>.</returns>
		public CustomDataAccessStatus<TitleExternalResource> TitleExternalResourceManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			TitleExternalResource value , int userId )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				value.CreationUserID = userId;
				value.LastModifiedUserID = userId;
				TitleExternalResource returnValue = TitleExternalResourceInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.TitleID,
						value.TitleExternalResourceTypeID,
						value.UrlText,
						value.Url,
						value.SequenceOrder,
						value.CreationUserID,
						value.LastModifiedUserID);
				
				return new CustomDataAccessStatus<TitleExternalResource>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (TitleExternalResourceDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.TitleExternalResourceID))
				{
				return new CustomDataAccessStatus<TitleExternalResource>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<TitleExternalResource>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				value.LastModifiedUserID = userId;
				TitleExternalResource returnValue = TitleExternalResourceUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.TitleExternalResourceID,
						value.TitleID,
						value.TitleExternalResourceTypeID,
						value.UrlText,
						value.Url,
						value.SequenceOrder,
						value.LastModifiedUserID);
					
				return new CustomDataAccessStatus<TitleExternalResource>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<TitleExternalResource>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}

