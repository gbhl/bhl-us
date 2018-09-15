
// Generated 9/14/2018 11:08:59 AM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class TextImportBatchDAL is based upon txtimport.TextImportBatch.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DAL
// {
// 		public partial class TextImportBatchDAL
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
	partial class TextImportBatchDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from txtimport.TextImportBatch by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="textImportBatchID"></param>
		/// <returns>Object of type TextImportBatch.</returns>
		public TextImportBatch TextImportBatchSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int textImportBatchID)
		{
			return TextImportBatchSelectAuto(	sqlConnection, sqlTransaction, "BHL",	textImportBatchID );
		}
			
		/// <summary>
		/// Select values from txtimport.TextImportBatch by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="textImportBatchID"></param>
		/// <returns>Object of type TextImportBatch.</returns>
		public TextImportBatch TextImportBatchSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int textImportBatchID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("txtimport.TextImportBatchSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("TextImportBatchID", SqlDbType.Int, null, false, textImportBatchID)))
			{
				using (CustomSqlHelper<TextImportBatch> helper = new CustomSqlHelper<TextImportBatch>())
				{
					CustomGenericList<TextImportBatch> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						TextImportBatch o = list[0];
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
		/// Select values from txtimport.TextImportBatch by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="textImportBatchID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> TextImportBatchSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int textImportBatchID)
		{
			return TextImportBatchSelectAutoRaw( sqlConnection, sqlTransaction, "BHL", textImportBatchID );
		}
		
		/// <summary>
		/// Select values from txtimport.TextImportBatch by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="textImportBatchID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> TextImportBatchSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int textImportBatchID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("txtimport.TextImportBatchSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("TextImportBatchID", SqlDbType.Int, null, false, textImportBatchID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====

 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into txtimport.TextImportBatch.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="textImportBatchStatusID"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type TextImportBatch.</returns>
		public TextImportBatch TextImportBatchInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int textImportBatchStatusID,
			int creationUserID,
			int lastModifiedUserID)
		{
			return TextImportBatchInsertAuto( sqlConnection, sqlTransaction, "BHL", textImportBatchStatusID, creationUserID, lastModifiedUserID );
		}
		
		/// <summary>
		/// Insert values into txtimport.TextImportBatch.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="textImportBatchStatusID"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type TextImportBatch.</returns>
		public TextImportBatch TextImportBatchInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int textImportBatchStatusID,
			int creationUserID,
			int lastModifiedUserID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("txtimport.TextImportBatchInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("TextImportBatchID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("TextImportBatchStatusID", SqlDbType.Int, null, false, textImportBatchStatusID),
					CustomSqlHelper.CreateInputParameter("CreationUserID", SqlDbType.Int, null, false, creationUserID),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, false, lastModifiedUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<TextImportBatch> helper = new CustomSqlHelper<TextImportBatch>())
				{
					CustomGenericList<TextImportBatch> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						TextImportBatch o = list[0];
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
		/// Insert values into txtimport.TextImportBatch. Returns an object of type TextImportBatch.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type TextImportBatch.</param>
		/// <returns>Object of type TextImportBatch.</returns>
		public TextImportBatch TextImportBatchInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			TextImportBatch value)
		{
			return TextImportBatchInsertAuto(sqlConnection, sqlTransaction, "BHL", value);
		}
		
		/// <summary>
		/// Insert values into txtimport.TextImportBatch. Returns an object of type TextImportBatch.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type TextImportBatch.</param>
		/// <returns>Object of type TextImportBatch.</returns>
		public TextImportBatch TextImportBatchInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			TextImportBatch value)
		{
			return TextImportBatchInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.TextImportBatchStatusID,
				value.CreationUserID,
				value.LastModifiedUserID);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from txtimport.TextImportBatch by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="textImportBatchID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool TextImportBatchDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int textImportBatchID)
		{
			return TextImportBatchDeleteAuto( sqlConnection, sqlTransaction, "BHL", textImportBatchID );
		}
		
		/// <summary>
		/// Delete values from txtimport.TextImportBatch by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="textImportBatchID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool TextImportBatchDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int textImportBatchID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("txtimport.TextImportBatchDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("TextImportBatchID", SqlDbType.Int, null, false, textImportBatchID), 
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
		/// Update values in txtimport.TextImportBatch. Returns an object of type TextImportBatch.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="textImportBatchID"></param>
		/// <param name="textImportBatchStatusID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type TextImportBatch.</returns>
		public TextImportBatch TextImportBatchUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int textImportBatchID,
			int textImportBatchStatusID,
			int lastModifiedUserID)
		{
			return TextImportBatchUpdateAuto( sqlConnection, sqlTransaction, "BHL", textImportBatchID, textImportBatchStatusID, lastModifiedUserID);
		}
		
		/// <summary>
		/// Update values in txtimport.TextImportBatch. Returns an object of type TextImportBatch.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="textImportBatchID"></param>
		/// <param name="textImportBatchStatusID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type TextImportBatch.</returns>
		public TextImportBatch TextImportBatchUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int textImportBatchID,
			int textImportBatchStatusID,
			int lastModifiedUserID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("txtimport.TextImportBatchUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("TextImportBatchID", SqlDbType.Int, null, false, textImportBatchID),
					CustomSqlHelper.CreateInputParameter("TextImportBatchStatusID", SqlDbType.Int, null, false, textImportBatchStatusID),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, false, lastModifiedUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<TextImportBatch> helper = new CustomSqlHelper<TextImportBatch>())
				{
					CustomGenericList<TextImportBatch> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						TextImportBatch o = list[0];
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
		/// Update values in txtimport.TextImportBatch. Returns an object of type TextImportBatch.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type TextImportBatch.</param>
		/// <returns>Object of type TextImportBatch.</returns>
		public TextImportBatch TextImportBatchUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			TextImportBatch value)
		{
			return TextImportBatchUpdateAuto(sqlConnection, sqlTransaction, "BHL", value );
		}
		
		/// <summary>
		/// Update values in txtimport.TextImportBatch. Returns an object of type TextImportBatch.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type TextImportBatch.</param>
		/// <returns>Object of type TextImportBatch.</returns>
		public TextImportBatch TextImportBatchUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			TextImportBatch value)
		{
			return TextImportBatchUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.TextImportBatchID,
				value.TextImportBatchStatusID,
				value.LastModifiedUserID);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage txtimport.TextImportBatch object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in txtimport.TextImportBatch.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type TextImportBatch.</param>
		/// <returns>Object of type CustomDataAccessStatus<TextImportBatch>.</returns>
		public CustomDataAccessStatus<TextImportBatch> TextImportBatchManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			TextImportBatch value , int userId )
		{
			return TextImportBatchManageAuto( sqlConnection, sqlTransaction, "BHL", value , userId );
		}
		
		/// <summary>
		/// Manage txtimport.TextImportBatch object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in txtimport.TextImportBatch.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type TextImportBatch.</param>
		/// <returns>Object of type CustomDataAccessStatus<TextImportBatch>.</returns>
		public CustomDataAccessStatus<TextImportBatch> TextImportBatchManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			TextImportBatch value , int userId )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				value.CreationUserID = userId;
				value.LastModifiedUserID = userId;
				TextImportBatch returnValue = TextImportBatchInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.TextImportBatchStatusID,
						value.CreationUserID,
						value.LastModifiedUserID);
				
				return new CustomDataAccessStatus<TextImportBatch>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (TextImportBatchDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.TextImportBatchID))
				{
				return new CustomDataAccessStatus<TextImportBatch>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<TextImportBatch>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				value.LastModifiedUserID = userId;
				TextImportBatch returnValue = TextImportBatchUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.TextImportBatchID,
						value.TextImportBatchStatusID,
						value.LastModifiedUserID);
					
				return new CustomDataAccessStatus<TextImportBatch>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<TextImportBatch>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}

