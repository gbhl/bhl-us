
// Generated 9/17/2018 2:47:55 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class TextImportBatchStatusDAL is based upon txtimport.TextImportBatchStatus.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DAL
// {
// 		public partial class TextImportBatchStatusDAL
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
	partial class TextImportBatchStatusDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from txtimport.TextImportBatchStatus by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="textImportBatchStatusID"></param>
		/// <returns>Object of type TextImportBatchStatus.</returns>
		public TextImportBatchStatus TextImportBatchStatusSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int textImportBatchStatusID)
		{
			return TextImportBatchStatusSelectAuto(	sqlConnection, sqlTransaction, "BHL",	textImportBatchStatusID );
		}
			
		/// <summary>
		/// Select values from txtimport.TextImportBatchStatus by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="textImportBatchStatusID"></param>
		/// <returns>Object of type TextImportBatchStatus.</returns>
		public TextImportBatchStatus TextImportBatchStatusSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int textImportBatchStatusID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("txtimport.TextImportBatchStatusSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("TextImportBatchStatusID", SqlDbType.Int, null, false, textImportBatchStatusID)))
			{
				using (CustomSqlHelper<TextImportBatchStatus> helper = new CustomSqlHelper<TextImportBatchStatus>())
				{
					List<TextImportBatchStatus> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						TextImportBatchStatus o = list[0];
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
		/// Select values from txtimport.TextImportBatchStatus by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="textImportBatchStatusID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> TextImportBatchStatusSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int textImportBatchStatusID)
		{
			return TextImportBatchStatusSelectAutoRaw( sqlConnection, sqlTransaction, "BHL", textImportBatchStatusID );
		}
		
		/// <summary>
		/// Select values from txtimport.TextImportBatchStatus by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="textImportBatchStatusID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> TextImportBatchStatusSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int textImportBatchStatusID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("txtimport.TextImportBatchStatusSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("TextImportBatchStatusID", SqlDbType.Int, null, false, textImportBatchStatusID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====

 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into txtimport.TextImportBatchStatus.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="textImportBatchStatusID"></param>
		/// <param name="statusName"></param>
		/// <param name="statusDescription"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type TextImportBatchStatus.</returns>
		public TextImportBatchStatus TextImportBatchStatusInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int textImportBatchStatusID,
			string statusName,
			string statusDescription,
			int creationUserID,
			int lastModifiedUserID)
		{
			return TextImportBatchStatusInsertAuto( sqlConnection, sqlTransaction, "BHL", textImportBatchStatusID, statusName, statusDescription, creationUserID, lastModifiedUserID );
		}
		
		/// <summary>
		/// Insert values into txtimport.TextImportBatchStatus.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="textImportBatchStatusID"></param>
		/// <param name="statusName"></param>
		/// <param name="statusDescription"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type TextImportBatchStatus.</returns>
		public TextImportBatchStatus TextImportBatchStatusInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int textImportBatchStatusID,
			string statusName,
			string statusDescription,
			int creationUserID,
			int lastModifiedUserID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("txtimport.TextImportBatchStatusInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("TextImportBatchStatusID", SqlDbType.Int, null, false, textImportBatchStatusID),
					CustomSqlHelper.CreateInputParameter("StatusName", SqlDbType.NVarChar, 50, false, statusName),
					CustomSqlHelper.CreateInputParameter("StatusDescription", SqlDbType.NVarChar, 500, false, statusDescription),
					CustomSqlHelper.CreateInputParameter("CreationUserID", SqlDbType.Int, null, false, creationUserID),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, false, lastModifiedUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<TextImportBatchStatus> helper = new CustomSqlHelper<TextImportBatchStatus>())
				{
					List<TextImportBatchStatus> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						TextImportBatchStatus o = list[0];
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
		/// Insert values into txtimport.TextImportBatchStatus. Returns an object of type TextImportBatchStatus.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type TextImportBatchStatus.</param>
		/// <returns>Object of type TextImportBatchStatus.</returns>
		public TextImportBatchStatus TextImportBatchStatusInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			TextImportBatchStatus value)
		{
			return TextImportBatchStatusInsertAuto(sqlConnection, sqlTransaction, "BHL", value);
		}
		
		/// <summary>
		/// Insert values into txtimport.TextImportBatchStatus. Returns an object of type TextImportBatchStatus.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type TextImportBatchStatus.</param>
		/// <returns>Object of type TextImportBatchStatus.</returns>
		public TextImportBatchStatus TextImportBatchStatusInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			TextImportBatchStatus value)
		{
			return TextImportBatchStatusInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.TextImportBatchStatusID,
				value.StatusName,
				value.StatusDescription,
				value.CreationUserID,
				value.LastModifiedUserID);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from txtimport.TextImportBatchStatus by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="textImportBatchStatusID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool TextImportBatchStatusDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int textImportBatchStatusID)
		{
			return TextImportBatchStatusDeleteAuto( sqlConnection, sqlTransaction, "BHL", textImportBatchStatusID );
		}
		
		/// <summary>
		/// Delete values from txtimport.TextImportBatchStatus by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="textImportBatchStatusID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool TextImportBatchStatusDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int textImportBatchStatusID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("txtimport.TextImportBatchStatusDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("TextImportBatchStatusID", SqlDbType.Int, null, false, textImportBatchStatusID), 
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
		/// Update values in txtimport.TextImportBatchStatus. Returns an object of type TextImportBatchStatus.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="textImportBatchStatusID"></param>
		/// <param name="statusName"></param>
		/// <param name="statusDescription"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type TextImportBatchStatus.</returns>
		public TextImportBatchStatus TextImportBatchStatusUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int textImportBatchStatusID,
			string statusName,
			string statusDescription,
			int lastModifiedUserID)
		{
			return TextImportBatchStatusUpdateAuto( sqlConnection, sqlTransaction, "BHL", textImportBatchStatusID, statusName, statusDescription, lastModifiedUserID);
		}
		
		/// <summary>
		/// Update values in txtimport.TextImportBatchStatus. Returns an object of type TextImportBatchStatus.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="textImportBatchStatusID"></param>
		/// <param name="statusName"></param>
		/// <param name="statusDescription"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type TextImportBatchStatus.</returns>
		public TextImportBatchStatus TextImportBatchStatusUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int textImportBatchStatusID,
			string statusName,
			string statusDescription,
			int lastModifiedUserID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("txtimport.TextImportBatchStatusUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("TextImportBatchStatusID", SqlDbType.Int, null, false, textImportBatchStatusID),
					CustomSqlHelper.CreateInputParameter("StatusName", SqlDbType.NVarChar, 50, false, statusName),
					CustomSqlHelper.CreateInputParameter("StatusDescription", SqlDbType.NVarChar, 500, false, statusDescription),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, false, lastModifiedUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<TextImportBatchStatus> helper = new CustomSqlHelper<TextImportBatchStatus>())
				{
					List<TextImportBatchStatus> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						TextImportBatchStatus o = list[0];
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
		/// Update values in txtimport.TextImportBatchStatus. Returns an object of type TextImportBatchStatus.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type TextImportBatchStatus.</param>
		/// <returns>Object of type TextImportBatchStatus.</returns>
		public TextImportBatchStatus TextImportBatchStatusUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			TextImportBatchStatus value)
		{
			return TextImportBatchStatusUpdateAuto(sqlConnection, sqlTransaction, "BHL", value );
		}
		
		/// <summary>
		/// Update values in txtimport.TextImportBatchStatus. Returns an object of type TextImportBatchStatus.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type TextImportBatchStatus.</param>
		/// <returns>Object of type TextImportBatchStatus.</returns>
		public TextImportBatchStatus TextImportBatchStatusUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			TextImportBatchStatus value)
		{
			return TextImportBatchStatusUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.TextImportBatchStatusID,
				value.StatusName,
				value.StatusDescription,
				value.LastModifiedUserID);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage txtimport.TextImportBatchStatus object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in txtimport.TextImportBatchStatus.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type TextImportBatchStatus.</param>
		/// <returns>Object of type CustomDataAccessStatus<TextImportBatchStatus>.</returns>
		public CustomDataAccessStatus<TextImportBatchStatus> TextImportBatchStatusManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			TextImportBatchStatus value , int userId )
		{
			return TextImportBatchStatusManageAuto( sqlConnection, sqlTransaction, "BHL", value , userId );
		}
		
		/// <summary>
		/// Manage txtimport.TextImportBatchStatus object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in txtimport.TextImportBatchStatus.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type TextImportBatchStatus.</param>
		/// <returns>Object of type CustomDataAccessStatus<TextImportBatchStatus>.</returns>
		public CustomDataAccessStatus<TextImportBatchStatus> TextImportBatchStatusManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			TextImportBatchStatus value , int userId )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				value.CreationUserID = userId;
				value.LastModifiedUserID = userId;
				TextImportBatchStatus returnValue = TextImportBatchStatusInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.TextImportBatchStatusID,
						value.StatusName,
						value.StatusDescription,
						value.CreationUserID,
						value.LastModifiedUserID);
				
				return new CustomDataAccessStatus<TextImportBatchStatus>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (TextImportBatchStatusDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.TextImportBatchStatusID))
				{
				return new CustomDataAccessStatus<TextImportBatchStatus>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<TextImportBatchStatus>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				value.LastModifiedUserID = userId;
				TextImportBatchStatus returnValue = TextImportBatchStatusUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.TextImportBatchStatusID,
						value.StatusName,
						value.StatusDescription,
						value.LastModifiedUserID);
					
				return new CustomDataAccessStatus<TextImportBatchStatus>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<TextImportBatchStatus>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}

