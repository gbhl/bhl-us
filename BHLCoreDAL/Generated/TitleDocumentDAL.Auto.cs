
// Generated 12/2/2024 5:43:10 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class TitleDocumentDAL is based upon dbo.TitleDocument.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DAL
// {
// 		public partial class TitleDocumentDAL
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
	partial class TitleDocumentDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from dbo.TitleDocument by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="titleDocumentID"></param>
		/// <returns>Object of type TitleDocument.</returns>
		public TitleDocument TitleDocumentSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int titleDocumentID)
		{
			return TitleDocumentSelectAuto(	sqlConnection, sqlTransaction, "BHL",	titleDocumentID );
		}
			
		/// <summary>
		/// Select values from dbo.TitleDocument by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="titleDocumentID"></param>
		/// <returns>Object of type TitleDocument.</returns>
		public TitleDocument TitleDocumentSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int titleDocumentID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleDocumentSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("TitleDocumentID", SqlDbType.Int, null, false, titleDocumentID)))
			{
				using (CustomSqlHelper<TitleDocument> helper = new CustomSqlHelper<TitleDocument>())
				{
					List<TitleDocument> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						TitleDocument o = list[0];
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
		/// Select values from dbo.TitleDocument by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="titleDocumentID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> TitleDocumentSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int titleDocumentID)
		{
			return TitleDocumentSelectAutoRaw( sqlConnection, sqlTransaction, "BHL", titleDocumentID );
		}
		
		/// <summary>
		/// Select values from dbo.TitleDocument by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="titleDocumentID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> TitleDocumentSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int titleDocumentID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleDocumentSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("TitleDocumentID", SqlDbType.Int, null, false, titleDocumentID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====

 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into dbo.TitleDocument.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="titleID"></param>
		/// <param name="documentTypeID"></param>
		/// <param name="name"></param>
		/// <param name="url"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type TitleDocument.</returns>
		public TitleDocument TitleDocumentInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int titleID,
			int documentTypeID,
			string name,
			string url,
			int? creationUserID,
			int? lastModifiedUserID)
		{
			return TitleDocumentInsertAuto( sqlConnection, sqlTransaction, "BHL", titleID, documentTypeID, name, url, creationUserID, lastModifiedUserID );
		}
		
		/// <summary>
		/// Insert values into dbo.TitleDocument.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="titleID"></param>
		/// <param name="documentTypeID"></param>
		/// <param name="name"></param>
		/// <param name="url"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type TitleDocument.</returns>
		public TitleDocument TitleDocumentInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int titleID,
			int documentTypeID,
			string name,
			string url,
			int? creationUserID,
			int? lastModifiedUserID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleDocumentInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("TitleDocumentID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("TitleID", SqlDbType.Int, null, false, titleID),
					CustomSqlHelper.CreateInputParameter("DocumentTypeID", SqlDbType.Int, null, false, documentTypeID),
					CustomSqlHelper.CreateInputParameter("Name", SqlDbType.NVarChar, 200, false, name),
					CustomSqlHelper.CreateInputParameter("Url", SqlDbType.NVarChar, 200, false, url),
					CustomSqlHelper.CreateInputParameter("CreationUserID", SqlDbType.Int, null, true, creationUserID),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, true, lastModifiedUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<TitleDocument> helper = new CustomSqlHelper<TitleDocument>())
				{
					List<TitleDocument> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						TitleDocument o = list[0];
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
		/// Insert values into dbo.TitleDocument. Returns an object of type TitleDocument.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type TitleDocument.</param>
		/// <returns>Object of type TitleDocument.</returns>
		public TitleDocument TitleDocumentInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			TitleDocument value)
		{
			return TitleDocumentInsertAuto(sqlConnection, sqlTransaction, "BHL", value);
		}
		
		/// <summary>
		/// Insert values into dbo.TitleDocument. Returns an object of type TitleDocument.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type TitleDocument.</param>
		/// <returns>Object of type TitleDocument.</returns>
		public TitleDocument TitleDocumentInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			TitleDocument value)
		{
			return TitleDocumentInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.TitleID,
				value.DocumentTypeID,
				value.Name,
				value.Url,
				value.CreationUserID,
				value.LastModifiedUserID);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from dbo.TitleDocument by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="titleDocumentID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool TitleDocumentDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int titleDocumentID)
		{
			return TitleDocumentDeleteAuto( sqlConnection, sqlTransaction, "BHL", titleDocumentID );
		}
		
		/// <summary>
		/// Delete values from dbo.TitleDocument by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="titleDocumentID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool TitleDocumentDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int titleDocumentID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleDocumentDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("TitleDocumentID", SqlDbType.Int, null, false, titleDocumentID), 
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
		/// Update values in dbo.TitleDocument. Returns an object of type TitleDocument.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="titleDocumentID"></param>
		/// <param name="titleID"></param>
		/// <param name="documentTypeID"></param>
		/// <param name="name"></param>
		/// <param name="url"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type TitleDocument.</returns>
		public TitleDocument TitleDocumentUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int titleDocumentID,
			int titleID,
			int documentTypeID,
			string name,
			string url,
			int? lastModifiedUserID)
		{
			return TitleDocumentUpdateAuto( sqlConnection, sqlTransaction, "BHL", titleDocumentID, titleID, documentTypeID, name, url, lastModifiedUserID);
		}
		
		/// <summary>
		/// Update values in dbo.TitleDocument. Returns an object of type TitleDocument.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="titleDocumentID"></param>
		/// <param name="titleID"></param>
		/// <param name="documentTypeID"></param>
		/// <param name="name"></param>
		/// <param name="url"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type TitleDocument.</returns>
		public TitleDocument TitleDocumentUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int titleDocumentID,
			int titleID,
			int documentTypeID,
			string name,
			string url,
			int? lastModifiedUserID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleDocumentUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("TitleDocumentID", SqlDbType.Int, null, false, titleDocumentID),
					CustomSqlHelper.CreateInputParameter("TitleID", SqlDbType.Int, null, false, titleID),
					CustomSqlHelper.CreateInputParameter("DocumentTypeID", SqlDbType.Int, null, false, documentTypeID),
					CustomSqlHelper.CreateInputParameter("Name", SqlDbType.NVarChar, 200, false, name),
					CustomSqlHelper.CreateInputParameter("Url", SqlDbType.NVarChar, 200, false, url),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, true, lastModifiedUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<TitleDocument> helper = new CustomSqlHelper<TitleDocument>())
				{
					List<TitleDocument> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						TitleDocument o = list[0];
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
		/// Update values in dbo.TitleDocument. Returns an object of type TitleDocument.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type TitleDocument.</param>
		/// <returns>Object of type TitleDocument.</returns>
		public TitleDocument TitleDocumentUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			TitleDocument value)
		{
			return TitleDocumentUpdateAuto(sqlConnection, sqlTransaction, "BHL", value );
		}
		
		/// <summary>
		/// Update values in dbo.TitleDocument. Returns an object of type TitleDocument.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type TitleDocument.</param>
		/// <returns>Object of type TitleDocument.</returns>
		public TitleDocument TitleDocumentUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			TitleDocument value)
		{
			return TitleDocumentUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.TitleDocumentID,
				value.TitleID,
				value.DocumentTypeID,
				value.Name,
				value.Url,
				value.LastModifiedUserID);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage dbo.TitleDocument object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.TitleDocument.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type TitleDocument.</param>
		/// <returns>Object of type CustomDataAccessStatus<TitleDocument>.</returns>
		public CustomDataAccessStatus<TitleDocument> TitleDocumentManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			TitleDocument value , int userId )
		{
			return TitleDocumentManageAuto( sqlConnection, sqlTransaction, "BHL", value , userId );
		}
		
		/// <summary>
		/// Manage dbo.TitleDocument object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.TitleDocument.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type TitleDocument.</param>
		/// <returns>Object of type CustomDataAccessStatus<TitleDocument>.</returns>
		public CustomDataAccessStatus<TitleDocument> TitleDocumentManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			TitleDocument value , int userId )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				value.CreationUserID = userId;
				value.LastModifiedUserID = userId;
				TitleDocument returnValue = TitleDocumentInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.TitleID,
						value.DocumentTypeID,
						value.Name,
						value.Url,
						value.CreationUserID,
						value.LastModifiedUserID);
				
				return new CustomDataAccessStatus<TitleDocument>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (TitleDocumentDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.TitleDocumentID))
				{
				return new CustomDataAccessStatus<TitleDocument>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<TitleDocument>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				value.LastModifiedUserID = userId;
				TitleDocument returnValue = TitleDocumentUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.TitleDocumentID,
						value.TitleID,
						value.DocumentTypeID,
						value.Name,
						value.Url,
						value.LastModifiedUserID);
					
				return new CustomDataAccessStatus<TitleDocument>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<TitleDocument>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}

