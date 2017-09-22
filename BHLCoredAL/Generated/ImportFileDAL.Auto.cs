
// Generated 9/20/2017 11:00:01 AM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class ImportFileDAL is based upon import.ImportFile.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DAL
// {
// 		public partial class ImportFileDAL
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
	partial class ImportFileDAL : IImportFileDAL
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from import.ImportFile by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="importFileID"></param>
		/// <returns>Object of type ImportFile.</returns>
		public ImportFile ImportFileSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int importFileID)
		{
			return ImportFileSelectAuto(	sqlConnection, sqlTransaction, "BHL",	importFileID );
		}
			
		/// <summary>
		/// Select values from import.ImportFile by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="importFileID"></param>
		/// <returns>Object of type ImportFile.</returns>
		public ImportFile ImportFileSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int importFileID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("import.ImportFileSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("ImportFileID", SqlDbType.Int, null, false, importFileID)))
			{
				using (CustomSqlHelper<ImportFile> helper = new CustomSqlHelper<ImportFile>())
				{
					CustomGenericList<ImportFile> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						ImportFile o = list[0];
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
		/// Select values from import.ImportFile by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="importFileID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> ImportFileSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int importFileID)
		{
			return ImportFileSelectAutoRaw( sqlConnection, sqlTransaction, "BHL", importFileID );
		}
		
		/// <summary>
		/// Select values from import.ImportFile by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="importFileID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> ImportFileSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int importFileID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("import.ImportFileSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("ImportFileID", SqlDbType.Int, null, false, importFileID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====

 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into import.ImportFile.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="importFileStatusID"></param>
		/// <param name="importFileName"></param>
		/// <param name="contributorCode"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <param name="segmentGenreID"></param>
		/// <returns>Object of type ImportFile.</returns>
		public ImportFile ImportFileInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int importFileStatusID,
			string importFileName,
			string contributorCode,
			int creationUserID,
			int lastModifiedUserID,
			int? segmentGenreID)
		{
			return ImportFileInsertAuto( sqlConnection, sqlTransaction, "BHL", importFileStatusID, importFileName, contributorCode, creationUserID, lastModifiedUserID, segmentGenreID );
		}
		
		/// <summary>
		/// Insert values into import.ImportFile.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="importFileStatusID"></param>
		/// <param name="importFileName"></param>
		/// <param name="contributorCode"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <param name="segmentGenreID"></param>
		/// <returns>Object of type ImportFile.</returns>
		public ImportFile ImportFileInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int importFileStatusID,
			string importFileName,
			string contributorCode,
			int creationUserID,
			int lastModifiedUserID,
			int? segmentGenreID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("import.ImportFileInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("ImportFileID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("ImportFileStatusID", SqlDbType.Int, null, false, importFileStatusID),
					CustomSqlHelper.CreateInputParameter("ImportFileName", SqlDbType.NVarChar, 200, false, importFileName),
					CustomSqlHelper.CreateInputParameter("ContributorCode", SqlDbType.NVarChar, 10, false, contributorCode),
					CustomSqlHelper.CreateInputParameter("CreationUserID", SqlDbType.Int, null, false, creationUserID),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, false, lastModifiedUserID),
					CustomSqlHelper.CreateInputParameter("SegmentGenreID", SqlDbType.Int, null, true, segmentGenreID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<ImportFile> helper = new CustomSqlHelper<ImportFile>())
				{
					CustomGenericList<ImportFile> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						ImportFile o = list[0];
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
		/// Insert values into import.ImportFile. Returns an object of type ImportFile.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type ImportFile.</param>
		/// <returns>Object of type ImportFile.</returns>
		public ImportFile ImportFileInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			ImportFile value)
		{
			return ImportFileInsertAuto(sqlConnection, sqlTransaction, "BHL", value);
		}
		
		/// <summary>
		/// Insert values into import.ImportFile. Returns an object of type ImportFile.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type ImportFile.</param>
		/// <returns>Object of type ImportFile.</returns>
		public ImportFile ImportFileInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			ImportFile value)
		{
			return ImportFileInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.ImportFileStatusID,
				value.ImportFileName,
				value.ContributorCode,
				value.CreationUserID,
				value.LastModifiedUserID,
				value.SegmentGenreID);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from import.ImportFile by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="importFileID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool ImportFileDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int importFileID)
		{
			return ImportFileDeleteAuto( sqlConnection, sqlTransaction, "BHL", importFileID );
		}
		
		/// <summary>
		/// Delete values from import.ImportFile by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="importFileID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool ImportFileDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int importFileID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("import.ImportFileDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("ImportFileID", SqlDbType.Int, null, false, importFileID), 
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
		/// Update values in import.ImportFile. Returns an object of type ImportFile.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="importFileID"></param>
		/// <param name="importFileStatusID"></param>
		/// <param name="importFileName"></param>
		/// <param name="contributorCode"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <param name="segmentGenreID"></param>
		/// <returns>Object of type ImportFile.</returns>
		public ImportFile ImportFileUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int importFileID,
			int importFileStatusID,
			string importFileName,
			string contributorCode,
			int lastModifiedUserID,
			int? segmentGenreID)
		{
			return ImportFileUpdateAuto( sqlConnection, sqlTransaction, "BHL", importFileID, importFileStatusID, importFileName, contributorCode, lastModifiedUserID, segmentGenreID);
		}
		
		/// <summary>
		/// Update values in import.ImportFile. Returns an object of type ImportFile.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="importFileID"></param>
		/// <param name="importFileStatusID"></param>
		/// <param name="importFileName"></param>
		/// <param name="contributorCode"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <param name="segmentGenreID"></param>
		/// <returns>Object of type ImportFile.</returns>
		public ImportFile ImportFileUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int importFileID,
			int importFileStatusID,
			string importFileName,
			string contributorCode,
			int lastModifiedUserID,
			int? segmentGenreID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("import.ImportFileUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("ImportFileID", SqlDbType.Int, null, false, importFileID),
					CustomSqlHelper.CreateInputParameter("ImportFileStatusID", SqlDbType.Int, null, false, importFileStatusID),
					CustomSqlHelper.CreateInputParameter("ImportFileName", SqlDbType.NVarChar, 200, false, importFileName),
					CustomSqlHelper.CreateInputParameter("ContributorCode", SqlDbType.NVarChar, 10, false, contributorCode),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, false, lastModifiedUserID),
					CustomSqlHelper.CreateInputParameter("SegmentGenreID", SqlDbType.Int, null, true, segmentGenreID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<ImportFile> helper = new CustomSqlHelper<ImportFile>())
				{
					CustomGenericList<ImportFile> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						ImportFile o = list[0];
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
		/// Update values in import.ImportFile. Returns an object of type ImportFile.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type ImportFile.</param>
		/// <returns>Object of type ImportFile.</returns>
		public ImportFile ImportFileUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			ImportFile value)
		{
			return ImportFileUpdateAuto(sqlConnection, sqlTransaction, "BHL", value );
		}
		
		/// <summary>
		/// Update values in import.ImportFile. Returns an object of type ImportFile.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type ImportFile.</param>
		/// <returns>Object of type ImportFile.</returns>
		public ImportFile ImportFileUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			ImportFile value)
		{
			return ImportFileUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.ImportFileID,
				value.ImportFileStatusID,
				value.ImportFileName,
				value.ContributorCode,
				value.LastModifiedUserID,
				value.SegmentGenreID);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage import.ImportFile object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in import.ImportFile.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type ImportFile.</param>
		/// <returns>Object of type CustomDataAccessStatus<ImportFile>.</returns>
		public CustomDataAccessStatus<ImportFile> ImportFileManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			ImportFile value , int userId )
		{
			return ImportFileManageAuto( sqlConnection, sqlTransaction, "BHL", value , userId );
		}
		
		/// <summary>
		/// Manage import.ImportFile object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in import.ImportFile.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type ImportFile.</param>
		/// <returns>Object of type CustomDataAccessStatus<ImportFile>.</returns>
		public CustomDataAccessStatus<ImportFile> ImportFileManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			ImportFile value , int userId )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				value.CreationUserID = userId;
				value.LastModifiedUserID = userId;
				ImportFile returnValue = ImportFileInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.ImportFileStatusID,
						value.ImportFileName,
						value.ContributorCode,
						value.CreationUserID,
						value.LastModifiedUserID,
						value.SegmentGenreID);
				
				return new CustomDataAccessStatus<ImportFile>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (ImportFileDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.ImportFileID))
				{
				return new CustomDataAccessStatus<ImportFile>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<ImportFile>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				value.LastModifiedUserID = userId;
				ImportFile returnValue = ImportFileUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.ImportFileID,
						value.ImportFileStatusID,
						value.ImportFileName,
						value.ContributorCode,
						value.LastModifiedUserID,
						value.SegmentGenreID);
					
				return new CustomDataAccessStatus<ImportFile>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<ImportFile>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}

