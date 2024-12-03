
// Generated 12/2/2024 6:16:29 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class DocumentTypeDAL is based upon dbo.DocumentType.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DAL
// {
// 		public partial class DocumentTypeDAL
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
	partial class DocumentTypeDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from dbo.DocumentType by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="documentTypeID"></param>
		/// <returns>Object of type DocumentType.</returns>
		public DocumentType DocumentTypeSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int documentTypeID)
		{
			return DocumentTypeSelectAuto(	sqlConnection, sqlTransaction, "BHL",	documentTypeID );
		}
			
		/// <summary>
		/// Select values from dbo.DocumentType by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="documentTypeID"></param>
		/// <returns>Object of type DocumentType.</returns>
		public DocumentType DocumentTypeSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int documentTypeID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("DocumentTypeSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("DocumentTypeID", SqlDbType.Int, null, false, documentTypeID)))
			{
				using (CustomSqlHelper<DocumentType> helper = new CustomSqlHelper<DocumentType>())
				{
					List<DocumentType> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						DocumentType o = list[0];
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
		/// Select values from dbo.DocumentType by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="documentTypeID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> DocumentTypeSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int documentTypeID)
		{
			return DocumentTypeSelectAutoRaw( sqlConnection, sqlTransaction, "BHL", documentTypeID );
		}
		
		/// <summary>
		/// Select values from dbo.DocumentType by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="documentTypeID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> DocumentTypeSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int documentTypeID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("DocumentTypeSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("DocumentTypeID", SqlDbType.Int, null, false, documentTypeID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====

 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into dbo.DocumentType.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="name"></param>
		/// <param name="label"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type DocumentType.</returns>
		public DocumentType DocumentTypeInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string name,
			string label,
			int? creationUserID,
			int? lastModifiedUserID)
		{
			return DocumentTypeInsertAuto( sqlConnection, sqlTransaction, "BHL", name, label, creationUserID, lastModifiedUserID );
		}
		
		/// <summary>
		/// Insert values into dbo.DocumentType.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="name"></param>
		/// <param name="label"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type DocumentType.</returns>
		public DocumentType DocumentTypeInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			string name,
			string label,
			int? creationUserID,
			int? lastModifiedUserID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("DocumentTypeInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("DocumentTypeID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("Name", SqlDbType.NVarChar, 40, false, name),
					CustomSqlHelper.CreateInputParameter("Label", SqlDbType.NVarChar, 50, false, label),
					CustomSqlHelper.CreateInputParameter("CreationUserID", SqlDbType.Int, null, true, creationUserID),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, true, lastModifiedUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<DocumentType> helper = new CustomSqlHelper<DocumentType>())
				{
					List<DocumentType> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						DocumentType o = list[0];
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
		/// Insert values into dbo.DocumentType. Returns an object of type DocumentType.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type DocumentType.</param>
		/// <returns>Object of type DocumentType.</returns>
		public DocumentType DocumentTypeInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			DocumentType value)
		{
			return DocumentTypeInsertAuto(sqlConnection, sqlTransaction, "BHL", value);
		}
		
		/// <summary>
		/// Insert values into dbo.DocumentType. Returns an object of type DocumentType.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type DocumentType.</param>
		/// <returns>Object of type DocumentType.</returns>
		public DocumentType DocumentTypeInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			DocumentType value)
		{
			return DocumentTypeInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.Name,
				value.Label,
				value.CreationUserID,
				value.LastModifiedUserID);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from dbo.DocumentType by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="documentTypeID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool DocumentTypeDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int documentTypeID)
		{
			return DocumentTypeDeleteAuto( sqlConnection, sqlTransaction, "BHL", documentTypeID );
		}
		
		/// <summary>
		/// Delete values from dbo.DocumentType by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="documentTypeID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool DocumentTypeDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int documentTypeID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("DocumentTypeDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("DocumentTypeID", SqlDbType.Int, null, false, documentTypeID), 
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
		/// Update values in dbo.DocumentType. Returns an object of type DocumentType.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="documentTypeID"></param>
		/// <param name="name"></param>
		/// <param name="label"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type DocumentType.</returns>
		public DocumentType DocumentTypeUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int documentTypeID,
			string name,
			string label,
			int? lastModifiedUserID)
		{
			return DocumentTypeUpdateAuto( sqlConnection, sqlTransaction, "BHL", documentTypeID, name, label, lastModifiedUserID);
		}
		
		/// <summary>
		/// Update values in dbo.DocumentType. Returns an object of type DocumentType.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="documentTypeID"></param>
		/// <param name="name"></param>
		/// <param name="label"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type DocumentType.</returns>
		public DocumentType DocumentTypeUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int documentTypeID,
			string name,
			string label,
			int? lastModifiedUserID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("DocumentTypeUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("DocumentTypeID", SqlDbType.Int, null, false, documentTypeID),
					CustomSqlHelper.CreateInputParameter("Name", SqlDbType.NVarChar, 40, false, name),
					CustomSqlHelper.CreateInputParameter("Label", SqlDbType.NVarChar, 50, false, label),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, true, lastModifiedUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<DocumentType> helper = new CustomSqlHelper<DocumentType>())
				{
					List<DocumentType> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						DocumentType o = list[0];
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
		/// Update values in dbo.DocumentType. Returns an object of type DocumentType.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type DocumentType.</param>
		/// <returns>Object of type DocumentType.</returns>
		public DocumentType DocumentTypeUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			DocumentType value)
		{
			return DocumentTypeUpdateAuto(sqlConnection, sqlTransaction, "BHL", value );
		}
		
		/// <summary>
		/// Update values in dbo.DocumentType. Returns an object of type DocumentType.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type DocumentType.</param>
		/// <returns>Object of type DocumentType.</returns>
		public DocumentType DocumentTypeUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			DocumentType value)
		{
			return DocumentTypeUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.DocumentTypeID,
				value.Name,
				value.Label,
				value.LastModifiedUserID);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage dbo.DocumentType object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.DocumentType.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type DocumentType.</param>
		/// <returns>Object of type CustomDataAccessStatus<DocumentType>.</returns>
		public CustomDataAccessStatus<DocumentType> DocumentTypeManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			DocumentType value , int userId )
		{
			return DocumentTypeManageAuto( sqlConnection, sqlTransaction, "BHL", value , userId );
		}
		
		/// <summary>
		/// Manage dbo.DocumentType object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.DocumentType.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type DocumentType.</param>
		/// <returns>Object of type CustomDataAccessStatus<DocumentType>.</returns>
		public CustomDataAccessStatus<DocumentType> DocumentTypeManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			DocumentType value , int userId )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				value.CreationUserID = userId;
				value.LastModifiedUserID = userId;
				DocumentType returnValue = DocumentTypeInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.Name,
						value.Label,
						value.CreationUserID,
						value.LastModifiedUserID);
				
				return new CustomDataAccessStatus<DocumentType>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (DocumentTypeDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.DocumentTypeID))
				{
				return new CustomDataAccessStatus<DocumentType>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<DocumentType>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				value.LastModifiedUserID = userId;
				DocumentType returnValue = DocumentTypeUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.DocumentTypeID,
						value.Name,
						value.Label,
						value.LastModifiedUserID);
					
				return new CustomDataAccessStatus<DocumentType>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<DocumentType>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}

