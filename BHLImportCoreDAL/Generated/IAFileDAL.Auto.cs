
// Generated 8/23/2010 3:08:23 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class IAFileDAL is based upon IAFile.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHLImport.DAL
// {
// 		public partial class IAFileDAL
//		{
//		}
// }

#endregion How To Implement

#region using

using System;
using System.Data;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHLImport.DataObjects;

#endregion using

namespace MOBOT.BHLImport.DAL
{
	partial class IAFileDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from IAFile by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="fileID"></param>
		/// <returns>Object of type IAFile.</returns>
		public IAFile IAFileSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int fileID)
		{
			return IAFileSelectAuto(	sqlConnection, sqlTransaction, "BHLImport",	fileID );
		}
			
		/// <summary>
		/// Select values from IAFile by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="fileID"></param>
		/// <returns>Object of type IAFile.</returns>
		public IAFile IAFileSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int fileID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IAFileSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("FileID", SqlDbType.Int, null, false, fileID)))
			{
				using (CustomSqlHelper<IAFile> helper = new CustomSqlHelper<IAFile>())
				{
					CustomGenericList<IAFile> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						IAFile o = list[0];
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
		/// Select values from IAFile by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="fileID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> IAFileSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int fileID)
		{
			return IAFileSelectAutoRaw( sqlConnection, sqlTransaction, "BHLImport", fileID );
		}
		
		/// <summary>
		/// Select values from IAFile by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="fileID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> IAFileSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int fileID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IAFileSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("FileID", SqlDbType.Int, null, false, fileID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====
	
 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into IAFile.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="itemID"></param>
		/// <param name="remoteFileName"></param>
		/// <param name="localFileName"></param>
		/// <param name="source"></param>
		/// <param name="format"></param>
		/// <param name="original"></param>
		/// <param name="remoteFileLastModifiedDate"></param>
		/// <returns>Object of type IAFile.</returns>
		public IAFile IAFileInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int itemID,
			string remoteFileName,
			string localFileName,
			string source,
			string format,
			string original,
			DateTime? remoteFileLastModifiedDate)
		{
			return IAFileInsertAuto( sqlConnection, sqlTransaction, "BHLImport", itemID, remoteFileName, localFileName, source, format, original, remoteFileLastModifiedDate );
		}
		
		/// <summary>
		/// Insert values into IAFile.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="itemID"></param>
		/// <param name="remoteFileName"></param>
		/// <param name="localFileName"></param>
		/// <param name="source"></param>
		/// <param name="format"></param>
		/// <param name="original"></param>
		/// <param name="remoteFileLastModifiedDate"></param>
		/// <returns>Object of type IAFile.</returns>
		public IAFile IAFileInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int itemID,
			string remoteFileName,
			string localFileName,
			string source,
			string format,
			string original,
			DateTime? remoteFileLastModifiedDate)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IAFileInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("FileID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemID),
					CustomSqlHelper.CreateInputParameter("RemoteFileName", SqlDbType.NVarChar, 100, false, remoteFileName),
					CustomSqlHelper.CreateInputParameter("LocalFileName", SqlDbType.NVarChar, 100, false, localFileName),
					CustomSqlHelper.CreateInputParameter("Source", SqlDbType.NVarChar, 20, false, source),
					CustomSqlHelper.CreateInputParameter("Format", SqlDbType.NVarChar, 50, false, format),
					CustomSqlHelper.CreateInputParameter("Original", SqlDbType.NVarChar, 50, false, original),
					CustomSqlHelper.CreateInputParameter("RemoteFileLastModifiedDate", SqlDbType.DateTime, null, true, remoteFileLastModifiedDate), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<IAFile> helper = new CustomSqlHelper<IAFile>())
				{
					CustomGenericList<IAFile> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						IAFile o = list[0];
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
		/// Insert values into IAFile. Returns an object of type IAFile.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type IAFile.</param>
		/// <returns>Object of type IAFile.</returns>
		public IAFile IAFileInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			IAFile value)
		{
			return IAFileInsertAuto(sqlConnection, sqlTransaction, "BHLImport", value);
		}
		
		/// <summary>
		/// Insert values into IAFile. Returns an object of type IAFile.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type IAFile.</param>
		/// <returns>Object of type IAFile.</returns>
		public IAFile IAFileInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			IAFile value)
		{
			return IAFileInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.ItemID,
				value.RemoteFileName,
				value.LocalFileName,
				value.Source,
				value.Format,
				value.Original,
				value.RemoteFileLastModifiedDate);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from IAFile by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="fileID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool IAFileDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int fileID)
		{
			return IAFileDeleteAuto( sqlConnection, sqlTransaction, "BHLImport", fileID );
		}
		
		/// <summary>
		/// Delete values from IAFile by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="fileID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool IAFileDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int fileID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IAFileDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("FileID", SqlDbType.Int, null, false, fileID), 
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
		/// Update values in IAFile. Returns an object of type IAFile.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="fileID"></param>
		/// <param name="itemID"></param>
		/// <param name="remoteFileName"></param>
		/// <param name="localFileName"></param>
		/// <param name="source"></param>
		/// <param name="format"></param>
		/// <param name="original"></param>
		/// <param name="remoteFileLastModifiedDate"></param>
		/// <returns>Object of type IAFile.</returns>
		public IAFile IAFileUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int fileID,
			int itemID,
			string remoteFileName,
			string localFileName,
			string source,
			string format,
			string original,
			DateTime? remoteFileLastModifiedDate)
		{
			return IAFileUpdateAuto( sqlConnection, sqlTransaction, "BHLImport", fileID, itemID, remoteFileName, localFileName, source, format, original, remoteFileLastModifiedDate);
		}
		
		/// <summary>
		/// Update values in IAFile. Returns an object of type IAFile.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="fileID"></param>
		/// <param name="itemID"></param>
		/// <param name="remoteFileName"></param>
		/// <param name="localFileName"></param>
		/// <param name="source"></param>
		/// <param name="format"></param>
		/// <param name="original"></param>
		/// <param name="remoteFileLastModifiedDate"></param>
		/// <returns>Object of type IAFile.</returns>
		public IAFile IAFileUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int fileID,
			int itemID,
			string remoteFileName,
			string localFileName,
			string source,
			string format,
			string original,
			DateTime? remoteFileLastModifiedDate)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IAFileUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("FileID", SqlDbType.Int, null, false, fileID),
					CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemID),
					CustomSqlHelper.CreateInputParameter("RemoteFileName", SqlDbType.NVarChar, 100, false, remoteFileName),
					CustomSqlHelper.CreateInputParameter("LocalFileName", SqlDbType.NVarChar, 100, false, localFileName),
					CustomSqlHelper.CreateInputParameter("Source", SqlDbType.NVarChar, 20, false, source),
					CustomSqlHelper.CreateInputParameter("Format", SqlDbType.NVarChar, 50, false, format),
					CustomSqlHelper.CreateInputParameter("Original", SqlDbType.NVarChar, 50, false, original),
					CustomSqlHelper.CreateInputParameter("RemoteFileLastModifiedDate", SqlDbType.DateTime, null, true, remoteFileLastModifiedDate), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<IAFile> helper = new CustomSqlHelper<IAFile>())
				{
					CustomGenericList<IAFile> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						IAFile o = list[0];
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
		/// Update values in IAFile. Returns an object of type IAFile.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type IAFile.</param>
		/// <returns>Object of type IAFile.</returns>
		public IAFile IAFileUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			IAFile value)
		{
			return IAFileUpdateAuto(sqlConnection, sqlTransaction, "BHLImport", value );
		}
		
		/// <summary>
		/// Update values in IAFile. Returns an object of type IAFile.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type IAFile.</param>
		/// <returns>Object of type IAFile.</returns>
		public IAFile IAFileUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			IAFile value)
		{
			return IAFileUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.FileID,
				value.ItemID,
				value.RemoteFileName,
				value.LocalFileName,
				value.Source,
				value.Format,
				value.Original,
				value.RemoteFileLastModifiedDate);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage IAFile object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in IAFile.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type IAFile.</param>
		/// <returns>Object of type CustomDataAccessStatus<IAFile>.</returns>
		public CustomDataAccessStatus<IAFile> IAFileManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			IAFile value  )
		{
			return IAFileManageAuto( sqlConnection, sqlTransaction, "BHLImport", value  );
		}
		
		/// <summary>
		/// Manage IAFile object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in IAFile.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type IAFile.</param>
		/// <returns>Object of type CustomDataAccessStatus<IAFile>.</returns>
		public CustomDataAccessStatus<IAFile> IAFileManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			IAFile value  )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				
				
				IAFile returnValue = IAFileInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.ItemID,
						value.RemoteFileName,
						value.LocalFileName,
						value.Source,
						value.Format,
						value.Original,
						value.RemoteFileLastModifiedDate);
				
				return new CustomDataAccessStatus<IAFile>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (IAFileDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.FileID))
				{
				return new CustomDataAccessStatus<IAFile>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<IAFile>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				
				IAFile returnValue = IAFileUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.FileID,
						value.ItemID,
						value.RemoteFileName,
						value.LocalFileName,
						value.Source,
						value.Format,
						value.Original,
						value.RemoteFileLastModifiedDate);
					
				return new CustomDataAccessStatus<IAFile>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<IAFile>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}
// end of source generation
