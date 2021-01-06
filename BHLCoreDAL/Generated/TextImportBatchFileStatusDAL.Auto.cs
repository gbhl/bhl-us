
// Generated 9/17/2018 2:48:01 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class TextImportBatchFileStatusDAL is based upon txtimport.TextImportBatchFileStatus.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DAL
// {
// 		public partial class TextImportBatchFileStatusDAL
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
	partial class TextImportBatchFileStatusDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from txtimport.TextImportBatchFileStatus by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="textImportBatchFileStatusID"></param>
		/// <returns>Object of type TextImportBatchFileStatus.</returns>
		public TextImportBatchFileStatus TextImportBatchFileStatusSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int textImportBatchFileStatusID)
		{
			return TextImportBatchFileStatusSelectAuto(	sqlConnection, sqlTransaction, "BHL",	textImportBatchFileStatusID );
		}
			
		/// <summary>
		/// Select values from txtimport.TextImportBatchFileStatus by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="textImportBatchFileStatusID"></param>
		/// <returns>Object of type TextImportBatchFileStatus.</returns>
		public TextImportBatchFileStatus TextImportBatchFileStatusSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int textImportBatchFileStatusID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("txtimport.TextImportBatchFileStatusSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("TextImportBatchFileStatusID", SqlDbType.Int, null, false, textImportBatchFileStatusID)))
			{
				using (CustomSqlHelper<TextImportBatchFileStatus> helper = new CustomSqlHelper<TextImportBatchFileStatus>())
				{
					List<TextImportBatchFileStatus> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						TextImportBatchFileStatus o = list[0];
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
		/// Select values from txtimport.TextImportBatchFileStatus by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="textImportBatchFileStatusID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> TextImportBatchFileStatusSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int textImportBatchFileStatusID)
		{
			return TextImportBatchFileStatusSelectAutoRaw( sqlConnection, sqlTransaction, "BHL", textImportBatchFileStatusID );
		}
		
		/// <summary>
		/// Select values from txtimport.TextImportBatchFileStatus by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="textImportBatchFileStatusID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> TextImportBatchFileStatusSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int textImportBatchFileStatusID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("txtimport.TextImportBatchFileStatusSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("TextImportBatchFileStatusID", SqlDbType.Int, null, false, textImportBatchFileStatusID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====

 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into txtimport.TextImportBatchFileStatus.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="textImportBatchFileStatusID"></param>
		/// <param name="statusName"></param>
		/// <param name="statusDescription"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type TextImportBatchFileStatus.</returns>
		public TextImportBatchFileStatus TextImportBatchFileStatusInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int textImportBatchFileStatusID,
			string statusName,
			string statusDescription,
			int creationUserID,
			int lastModifiedUserID)
		{
			return TextImportBatchFileStatusInsertAuto( sqlConnection, sqlTransaction, "BHL", textImportBatchFileStatusID, statusName, statusDescription, creationUserID, lastModifiedUserID );
		}
		
		/// <summary>
		/// Insert values into txtimport.TextImportBatchFileStatus.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="textImportBatchFileStatusID"></param>
		/// <param name="statusName"></param>
		/// <param name="statusDescription"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type TextImportBatchFileStatus.</returns>
		public TextImportBatchFileStatus TextImportBatchFileStatusInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int textImportBatchFileStatusID,
			string statusName,
			string statusDescription,
			int creationUserID,
			int lastModifiedUserID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("txtimport.TextImportBatchFileStatusInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("TextImportBatchFileStatusID", SqlDbType.Int, null, false, textImportBatchFileStatusID),
					CustomSqlHelper.CreateInputParameter("StatusName", SqlDbType.NVarChar, 50, false, statusName),
					CustomSqlHelper.CreateInputParameter("StatusDescription", SqlDbType.NVarChar, 500, false, statusDescription),
					CustomSqlHelper.CreateInputParameter("CreationUserID", SqlDbType.Int, null, false, creationUserID),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, false, lastModifiedUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<TextImportBatchFileStatus> helper = new CustomSqlHelper<TextImportBatchFileStatus>())
				{
					List<TextImportBatchFileStatus> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						TextImportBatchFileStatus o = list[0];
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
		/// Insert values into txtimport.TextImportBatchFileStatus. Returns an object of type TextImportBatchFileStatus.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type TextImportBatchFileStatus.</param>
		/// <returns>Object of type TextImportBatchFileStatus.</returns>
		public TextImportBatchFileStatus TextImportBatchFileStatusInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			TextImportBatchFileStatus value)
		{
			return TextImportBatchFileStatusInsertAuto(sqlConnection, sqlTransaction, "BHL", value);
		}
		
		/// <summary>
		/// Insert values into txtimport.TextImportBatchFileStatus. Returns an object of type TextImportBatchFileStatus.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type TextImportBatchFileStatus.</param>
		/// <returns>Object of type TextImportBatchFileStatus.</returns>
		public TextImportBatchFileStatus TextImportBatchFileStatusInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			TextImportBatchFileStatus value)
		{
			return TextImportBatchFileStatusInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.TextImportBatchFileStatusID,
				value.StatusName,
				value.StatusDescription,
				value.CreationUserID,
				value.LastModifiedUserID);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from txtimport.TextImportBatchFileStatus by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="textImportBatchFileStatusID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool TextImportBatchFileStatusDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int textImportBatchFileStatusID)
		{
			return TextImportBatchFileStatusDeleteAuto( sqlConnection, sqlTransaction, "BHL", textImportBatchFileStatusID );
		}
		
		/// <summary>
		/// Delete values from txtimport.TextImportBatchFileStatus by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="textImportBatchFileStatusID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool TextImportBatchFileStatusDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int textImportBatchFileStatusID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("txtimport.TextImportBatchFileStatusDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("TextImportBatchFileStatusID", SqlDbType.Int, null, false, textImportBatchFileStatusID), 
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
		/// Update values in txtimport.TextImportBatchFileStatus. Returns an object of type TextImportBatchFileStatus.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="textImportBatchFileStatusID"></param>
		/// <param name="statusName"></param>
		/// <param name="statusDescription"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type TextImportBatchFileStatus.</returns>
		public TextImportBatchFileStatus TextImportBatchFileStatusUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int textImportBatchFileStatusID,
			string statusName,
			string statusDescription,
			int lastModifiedUserID)
		{
			return TextImportBatchFileStatusUpdateAuto( sqlConnection, sqlTransaction, "BHL", textImportBatchFileStatusID, statusName, statusDescription, lastModifiedUserID);
		}
		
		/// <summary>
		/// Update values in txtimport.TextImportBatchFileStatus. Returns an object of type TextImportBatchFileStatus.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="textImportBatchFileStatusID"></param>
		/// <param name="statusName"></param>
		/// <param name="statusDescription"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type TextImportBatchFileStatus.</returns>
		public TextImportBatchFileStatus TextImportBatchFileStatusUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int textImportBatchFileStatusID,
			string statusName,
			string statusDescription,
			int lastModifiedUserID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("txtimport.TextImportBatchFileStatusUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("TextImportBatchFileStatusID", SqlDbType.Int, null, false, textImportBatchFileStatusID),
					CustomSqlHelper.CreateInputParameter("StatusName", SqlDbType.NVarChar, 50, false, statusName),
					CustomSqlHelper.CreateInputParameter("StatusDescription", SqlDbType.NVarChar, 500, false, statusDescription),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, false, lastModifiedUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<TextImportBatchFileStatus> helper = new CustomSqlHelper<TextImportBatchFileStatus>())
				{
					List<TextImportBatchFileStatus> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						TextImportBatchFileStatus o = list[0];
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
		/// Update values in txtimport.TextImportBatchFileStatus. Returns an object of type TextImportBatchFileStatus.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type TextImportBatchFileStatus.</param>
		/// <returns>Object of type TextImportBatchFileStatus.</returns>
		public TextImportBatchFileStatus TextImportBatchFileStatusUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			TextImportBatchFileStatus value)
		{
			return TextImportBatchFileStatusUpdateAuto(sqlConnection, sqlTransaction, "BHL", value );
		}
		
		/// <summary>
		/// Update values in txtimport.TextImportBatchFileStatus. Returns an object of type TextImportBatchFileStatus.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type TextImportBatchFileStatus.</param>
		/// <returns>Object of type TextImportBatchFileStatus.</returns>
		public TextImportBatchFileStatus TextImportBatchFileStatusUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			TextImportBatchFileStatus value)
		{
			return TextImportBatchFileStatusUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.TextImportBatchFileStatusID,
				value.StatusName,
				value.StatusDescription,
				value.LastModifiedUserID);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage txtimport.TextImportBatchFileStatus object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in txtimport.TextImportBatchFileStatus.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type TextImportBatchFileStatus.</param>
		/// <returns>Object of type CustomDataAccessStatus<TextImportBatchFileStatus>.</returns>
		public CustomDataAccessStatus<TextImportBatchFileStatus> TextImportBatchFileStatusManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			TextImportBatchFileStatus value , int userId )
		{
			return TextImportBatchFileStatusManageAuto( sqlConnection, sqlTransaction, "BHL", value , userId );
		}
		
		/// <summary>
		/// Manage txtimport.TextImportBatchFileStatus object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in txtimport.TextImportBatchFileStatus.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type TextImportBatchFileStatus.</param>
		/// <returns>Object of type CustomDataAccessStatus<TextImportBatchFileStatus>.</returns>
		public CustomDataAccessStatus<TextImportBatchFileStatus> TextImportBatchFileStatusManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			TextImportBatchFileStatus value , int userId )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				value.CreationUserID = userId;
				value.LastModifiedUserID = userId;
				TextImportBatchFileStatus returnValue = TextImportBatchFileStatusInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.TextImportBatchFileStatusID,
						value.StatusName,
						value.StatusDescription,
						value.CreationUserID,
						value.LastModifiedUserID);
				
				return new CustomDataAccessStatus<TextImportBatchFileStatus>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (TextImportBatchFileStatusDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.TextImportBatchFileStatusID))
				{
				return new CustomDataAccessStatus<TextImportBatchFileStatus>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<TextImportBatchFileStatus>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				value.LastModifiedUserID = userId;
				TextImportBatchFileStatus returnValue = TextImportBatchFileStatusUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.TextImportBatchFileStatusID,
						value.StatusName,
						value.StatusDescription,
						value.LastModifiedUserID);
					
				return new CustomDataAccessStatus<TextImportBatchFileStatus>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<TextImportBatchFileStatus>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}

