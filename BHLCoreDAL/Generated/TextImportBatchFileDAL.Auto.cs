
// Generated 9/14/2018 11:09:07 AM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class TextImportBatchFileDAL is based upon txtimport.TextImportBatchFile.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DAL
// {
// 		public partial class TextImportBatchFileDAL
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
	partial class TextImportBatchFileDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from txtimport.TextImportBatchFile by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="textImportBatchFileID"></param>
		/// <returns>Object of type TextImportBatchFile.</returns>
		public TextImportBatchFile TextImportBatchFileSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int textImportBatchFileID)
		{
			return TextImportBatchFileSelectAuto(	sqlConnection, sqlTransaction, "BHL",	textImportBatchFileID );
		}
			
		/// <summary>
		/// Select values from txtimport.TextImportBatchFile by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="textImportBatchFileID"></param>
		/// <returns>Object of type TextImportBatchFile.</returns>
		public TextImportBatchFile TextImportBatchFileSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int textImportBatchFileID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("txtimport.TextImportBatchFileSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("TextImportBatchFileID", SqlDbType.Int, null, false, textImportBatchFileID)))
			{
				using (CustomSqlHelper<TextImportBatchFile> helper = new CustomSqlHelper<TextImportBatchFile>())
				{
					CustomGenericList<TextImportBatchFile> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						TextImportBatchFile o = list[0];
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
		/// Select values from txtimport.TextImportBatchFile by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="textImportBatchFileID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> TextImportBatchFileSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int textImportBatchFileID)
		{
			return TextImportBatchFileSelectAutoRaw( sqlConnection, sqlTransaction, "BHL", textImportBatchFileID );
		}
		
		/// <summary>
		/// Select values from txtimport.TextImportBatchFile by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="textImportBatchFileID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> TextImportBatchFileSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int textImportBatchFileID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("txtimport.TextImportBatchFileSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("TextImportBatchFileID", SqlDbType.Int, null, false, textImportBatchFileID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====

 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into txtimport.TextImportBatchFile.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="textImportBatchID"></param>
		/// <param name="textImportBatchFileStatusID"></param>
		/// <param name="itemID"></param>
		/// <param name="filename"></param>
		/// <param name="fileFormat"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type TextImportBatchFile.</returns>
		public TextImportBatchFile TextImportBatchFileInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int textImportBatchID,
			int textImportBatchFileStatusID,
			int? itemID,
			string filename,
			string fileFormat,
			int creationUserID,
			int lastModifiedUserID)
		{
			return TextImportBatchFileInsertAuto( sqlConnection, sqlTransaction, "BHL", textImportBatchID, textImportBatchFileStatusID, itemID, filename, fileFormat, creationUserID, lastModifiedUserID );
		}
		
		/// <summary>
		/// Insert values into txtimport.TextImportBatchFile.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="textImportBatchID"></param>
		/// <param name="textImportBatchFileStatusID"></param>
		/// <param name="itemID"></param>
		/// <param name="filename"></param>
		/// <param name="fileFormat"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type TextImportBatchFile.</returns>
		public TextImportBatchFile TextImportBatchFileInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int textImportBatchID,
			int textImportBatchFileStatusID,
			int? itemID,
			string filename,
			string fileFormat,
			int creationUserID,
			int lastModifiedUserID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("txtimport.TextImportBatchFileInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("TextImportBatchFileID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("TextImportBatchID", SqlDbType.Int, null, false, textImportBatchID),
					CustomSqlHelper.CreateInputParameter("TextImportBatchFileStatusID", SqlDbType.Int, null, false, textImportBatchFileStatusID),
					CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, true, itemID),
					CustomSqlHelper.CreateInputParameter("Filename", SqlDbType.NVarChar, 500, false, filename),
					CustomSqlHelper.CreateInputParameter("FileFormat", SqlDbType.NVarChar, 100, false, fileFormat),
					CustomSqlHelper.CreateInputParameter("CreationUserID", SqlDbType.Int, null, false, creationUserID),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, false, lastModifiedUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<TextImportBatchFile> helper = new CustomSqlHelper<TextImportBatchFile>())
				{
					CustomGenericList<TextImportBatchFile> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						TextImportBatchFile o = list[0];
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
		/// Insert values into txtimport.TextImportBatchFile. Returns an object of type TextImportBatchFile.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type TextImportBatchFile.</param>
		/// <returns>Object of type TextImportBatchFile.</returns>
		public TextImportBatchFile TextImportBatchFileInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			TextImportBatchFile value)
		{
			return TextImportBatchFileInsertAuto(sqlConnection, sqlTransaction, "BHL", value);
		}
		
		/// <summary>
		/// Insert values into txtimport.TextImportBatchFile. Returns an object of type TextImportBatchFile.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type TextImportBatchFile.</param>
		/// <returns>Object of type TextImportBatchFile.</returns>
		public TextImportBatchFile TextImportBatchFileInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			TextImportBatchFile value)
		{
			return TextImportBatchFileInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.TextImportBatchID,
				value.TextImportBatchFileStatusID,
				value.ItemID,
				value.Filename,
				value.FileFormat,
				value.CreationUserID,
				value.LastModifiedUserID);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from txtimport.TextImportBatchFile by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="textImportBatchFileID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool TextImportBatchFileDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int textImportBatchFileID)
		{
			return TextImportBatchFileDeleteAuto( sqlConnection, sqlTransaction, "BHL", textImportBatchFileID );
		}
		
		/// <summary>
		/// Delete values from txtimport.TextImportBatchFile by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="textImportBatchFileID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool TextImportBatchFileDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int textImportBatchFileID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("txtimport.TextImportBatchFileDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("TextImportBatchFileID", SqlDbType.Int, null, false, textImportBatchFileID), 
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
		/// Update values in txtimport.TextImportBatchFile. Returns an object of type TextImportBatchFile.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="textImportBatchFileID"></param>
		/// <param name="textImportBatchID"></param>
		/// <param name="textImportBatchFileStatusID"></param>
		/// <param name="itemID"></param>
		/// <param name="filename"></param>
		/// <param name="fileFormat"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type TextImportBatchFile.</returns>
		public TextImportBatchFile TextImportBatchFileUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int textImportBatchFileID,
			int textImportBatchID,
			int textImportBatchFileStatusID,
			int? itemID,
			string filename,
			string fileFormat,
			int lastModifiedUserID)
		{
			return TextImportBatchFileUpdateAuto( sqlConnection, sqlTransaction, "BHL", textImportBatchFileID, textImportBatchID, textImportBatchFileStatusID, itemID, filename, fileFormat, lastModifiedUserID);
		}
		
		/// <summary>
		/// Update values in txtimport.TextImportBatchFile. Returns an object of type TextImportBatchFile.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="textImportBatchFileID"></param>
		/// <param name="textImportBatchID"></param>
		/// <param name="textImportBatchFileStatusID"></param>
		/// <param name="itemID"></param>
		/// <param name="filename"></param>
		/// <param name="fileFormat"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type TextImportBatchFile.</returns>
		public TextImportBatchFile TextImportBatchFileUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int textImportBatchFileID,
			int textImportBatchID,
			int textImportBatchFileStatusID,
			int? itemID,
			string filename,
			string fileFormat,
			int lastModifiedUserID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("txtimport.TextImportBatchFileUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("TextImportBatchFileID", SqlDbType.Int, null, false, textImportBatchFileID),
					CustomSqlHelper.CreateInputParameter("TextImportBatchID", SqlDbType.Int, null, false, textImportBatchID),
					CustomSqlHelper.CreateInputParameter("TextImportBatchFileStatusID", SqlDbType.Int, null, false, textImportBatchFileStatusID),
					CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, true, itemID),
					CustomSqlHelper.CreateInputParameter("Filename", SqlDbType.NVarChar, 500, false, filename),
					CustomSqlHelper.CreateInputParameter("FileFormat", SqlDbType.NVarChar, 100, false, fileFormat),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, false, lastModifiedUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<TextImportBatchFile> helper = new CustomSqlHelper<TextImportBatchFile>())
				{
					CustomGenericList<TextImportBatchFile> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						TextImportBatchFile o = list[0];
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
		/// Update values in txtimport.TextImportBatchFile. Returns an object of type TextImportBatchFile.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type TextImportBatchFile.</param>
		/// <returns>Object of type TextImportBatchFile.</returns>
		public TextImportBatchFile TextImportBatchFileUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			TextImportBatchFile value)
		{
			return TextImportBatchFileUpdateAuto(sqlConnection, sqlTransaction, "BHL", value );
		}
		
		/// <summary>
		/// Update values in txtimport.TextImportBatchFile. Returns an object of type TextImportBatchFile.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type TextImportBatchFile.</param>
		/// <returns>Object of type TextImportBatchFile.</returns>
		public TextImportBatchFile TextImportBatchFileUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			TextImportBatchFile value)
		{
			return TextImportBatchFileUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.TextImportBatchFileID,
				value.TextImportBatchID,
				value.TextImportBatchFileStatusID,
				value.ItemID,
				value.Filename,
				value.FileFormat,
				value.LastModifiedUserID);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage txtimport.TextImportBatchFile object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in txtimport.TextImportBatchFile.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type TextImportBatchFile.</param>
		/// <returns>Object of type CustomDataAccessStatus<TextImportBatchFile>.</returns>
		public CustomDataAccessStatus<TextImportBatchFile> TextImportBatchFileManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			TextImportBatchFile value , int userId )
		{
			return TextImportBatchFileManageAuto( sqlConnection, sqlTransaction, "BHL", value , userId );
		}
		
		/// <summary>
		/// Manage txtimport.TextImportBatchFile object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in txtimport.TextImportBatchFile.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type TextImportBatchFile.</param>
		/// <returns>Object of type CustomDataAccessStatus<TextImportBatchFile>.</returns>
		public CustomDataAccessStatus<TextImportBatchFile> TextImportBatchFileManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			TextImportBatchFile value , int userId )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				value.CreationUserID = userId;
				value.LastModifiedUserID = userId;
				TextImportBatchFile returnValue = TextImportBatchFileInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.TextImportBatchID,
						value.TextImportBatchFileStatusID,
						value.ItemID,
						value.Filename,
						value.FileFormat,
						value.CreationUserID,
						value.LastModifiedUserID);
				
				return new CustomDataAccessStatus<TextImportBatchFile>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (TextImportBatchFileDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.TextImportBatchFileID))
				{
				return new CustomDataAccessStatus<TextImportBatchFile>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<TextImportBatchFile>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				value.LastModifiedUserID = userId;
				TextImportBatchFile returnValue = TextImportBatchFileUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.TextImportBatchFileID,
						value.TextImportBatchID,
						value.TextImportBatchFileStatusID,
						value.ItemID,
						value.Filename,
						value.FileFormat,
						value.LastModifiedUserID);
					
				return new CustomDataAccessStatus<TextImportBatchFile>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<TextImportBatchFile>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}

