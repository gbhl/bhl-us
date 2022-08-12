
// Generated 7/13/2021 2:35:01 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class ImportRecordContributorDAL is based upon import.ImportRecordContributor.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DAL
// {
// 		public partial class ImportRecordContributorDAL
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
	partial class ImportRecordContributorDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from import.ImportRecordContributor by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="importRecordContributorID"></param>
		/// <returns>Object of type ImportRecordContributor.</returns>
		public ImportRecordContributor ImportRecordContributorSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int importRecordContributorID)
		{
			return ImportRecordContributorSelectAuto(	sqlConnection, sqlTransaction, "BHL",	importRecordContributorID );
		}
			
		/// <summary>
		/// Select values from import.ImportRecordContributor by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="importRecordContributorID"></param>
		/// <returns>Object of type ImportRecordContributor.</returns>
		public ImportRecordContributor ImportRecordContributorSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int importRecordContributorID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("import.ImportRecordContributorSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("ImportRecordContributorID", SqlDbType.Int, null, false, importRecordContributorID)))
			{
				using (CustomSqlHelper<ImportRecordContributor> helper = new CustomSqlHelper<ImportRecordContributor>())
				{
					List<ImportRecordContributor> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						ImportRecordContributor o = list[0];
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
		/// Select values from import.ImportRecordContributor by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="importRecordContributorID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> ImportRecordContributorSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int importRecordContributorID)
		{
			return ImportRecordContributorSelectAutoRaw( sqlConnection, sqlTransaction, "BHL", importRecordContributorID );
		}
		
		/// <summary>
		/// Select values from import.ImportRecordContributor by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="importRecordContributorID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> ImportRecordContributorSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int importRecordContributorID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("import.ImportRecordContributorSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("ImportRecordContributorID", SqlDbType.Int, null, false, importRecordContributorID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====

 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into import.ImportRecordContributor.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="importRecordID"></param>
		/// <param name="institutionCode"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type ImportRecordContributor.</returns>
		public ImportRecordContributor ImportRecordContributorInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int importRecordID,
			string institutionCode,
			int creationUserID,
			int lastModifiedUserID)
		{
			return ImportRecordContributorInsertAuto( sqlConnection, sqlTransaction, "BHL", importRecordID, institutionCode, creationUserID, lastModifiedUserID );
		}
		
		/// <summary>
		/// Insert values into import.ImportRecordContributor.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="importRecordID"></param>
		/// <param name="institutionCode"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type ImportRecordContributor.</returns>
		public ImportRecordContributor ImportRecordContributorInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int importRecordID,
			string institutionCode,
			int creationUserID,
			int lastModifiedUserID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("import.ImportRecordContributorInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("ImportRecordContributorID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("ImportRecordID", SqlDbType.Int, null, false, importRecordID),
					CustomSqlHelper.CreateInputParameter("InstitutionCode", SqlDbType.NVarChar, 10, false, institutionCode),
					CustomSqlHelper.CreateInputParameter("CreationUserID", SqlDbType.Int, null, false, creationUserID),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, false, lastModifiedUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<ImportRecordContributor> helper = new CustomSqlHelper<ImportRecordContributor>())
				{
					List<ImportRecordContributor> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						ImportRecordContributor o = list[0];
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
		/// Insert values into import.ImportRecordContributor. Returns an object of type ImportRecordContributor.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type ImportRecordContributor.</param>
		/// <returns>Object of type ImportRecordContributor.</returns>
		public ImportRecordContributor ImportRecordContributorInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			ImportRecordContributor value)
		{
			return ImportRecordContributorInsertAuto(sqlConnection, sqlTransaction, "BHL", value);
		}
		
		/// <summary>
		/// Insert values into import.ImportRecordContributor. Returns an object of type ImportRecordContributor.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type ImportRecordContributor.</param>
		/// <returns>Object of type ImportRecordContributor.</returns>
		public ImportRecordContributor ImportRecordContributorInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			ImportRecordContributor value)
		{
			return ImportRecordContributorInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.ImportRecordID,
				value.InstitutionCode,
				value.CreationUserID,
				value.LastModifiedUserID);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from import.ImportRecordContributor by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="importRecordContributorID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool ImportRecordContributorDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int importRecordContributorID)
		{
			return ImportRecordContributorDeleteAuto( sqlConnection, sqlTransaction, "BHL", importRecordContributorID );
		}
		
		/// <summary>
		/// Delete values from import.ImportRecordContributor by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="importRecordContributorID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool ImportRecordContributorDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int importRecordContributorID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("import.ImportRecordContributorDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("ImportRecordContributorID", SqlDbType.Int, null, false, importRecordContributorID), 
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
		/// Update values in import.ImportRecordContributor. Returns an object of type ImportRecordContributor.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="importRecordContributorID"></param>
		/// <param name="importRecordID"></param>
		/// <param name="institutionCode"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type ImportRecordContributor.</returns>
		public ImportRecordContributor ImportRecordContributorUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int importRecordContributorID,
			int importRecordID,
			string institutionCode,
			int lastModifiedUserID)
		{
			return ImportRecordContributorUpdateAuto( sqlConnection, sqlTransaction, "BHL", importRecordContributorID, importRecordID, institutionCode, lastModifiedUserID);
		}
		
		/// <summary>
		/// Update values in import.ImportRecordContributor. Returns an object of type ImportRecordContributor.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="importRecordContributorID"></param>
		/// <param name="importRecordID"></param>
		/// <param name="institutionCode"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type ImportRecordContributor.</returns>
		public ImportRecordContributor ImportRecordContributorUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int importRecordContributorID,
			int importRecordID,
			string institutionCode,
			int lastModifiedUserID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("import.ImportRecordContributorUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("ImportRecordContributorID", SqlDbType.Int, null, false, importRecordContributorID),
					CustomSqlHelper.CreateInputParameter("ImportRecordID", SqlDbType.Int, null, false, importRecordID),
					CustomSqlHelper.CreateInputParameter("InstitutionCode", SqlDbType.NVarChar, 10, false, institutionCode),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, false, lastModifiedUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<ImportRecordContributor> helper = new CustomSqlHelper<ImportRecordContributor>())
				{
					List<ImportRecordContributor> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						ImportRecordContributor o = list[0];
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
		/// Update values in import.ImportRecordContributor. Returns an object of type ImportRecordContributor.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type ImportRecordContributor.</param>
		/// <returns>Object of type ImportRecordContributor.</returns>
		public ImportRecordContributor ImportRecordContributorUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			ImportRecordContributor value)
		{
			return ImportRecordContributorUpdateAuto(sqlConnection, sqlTransaction, "BHL", value );
		}
		
		/// <summary>
		/// Update values in import.ImportRecordContributor. Returns an object of type ImportRecordContributor.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type ImportRecordContributor.</param>
		/// <returns>Object of type ImportRecordContributor.</returns>
		public ImportRecordContributor ImportRecordContributorUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			ImportRecordContributor value)
		{
			return ImportRecordContributorUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.ImportRecordContributorID,
				value.ImportRecordID,
				value.InstitutionCode,
				value.LastModifiedUserID);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage import.ImportRecordContributor object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in import.ImportRecordContributor.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type ImportRecordContributor.</param>
		/// <returns>Object of type CustomDataAccessStatus<ImportRecordContributor>.</returns>
		public CustomDataAccessStatus<ImportRecordContributor> ImportRecordContributorManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			ImportRecordContributor value , int userId )
		{
			return ImportRecordContributorManageAuto( sqlConnection, sqlTransaction, "BHL", value , userId );
		}
		
		/// <summary>
		/// Manage import.ImportRecordContributor object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in import.ImportRecordContributor.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type ImportRecordContributor.</param>
		/// <returns>Object of type CustomDataAccessStatus<ImportRecordContributor>.</returns>
		public CustomDataAccessStatus<ImportRecordContributor> ImportRecordContributorManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			ImportRecordContributor value , int userId )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				value.CreationUserID = userId;
				value.LastModifiedUserID = userId;
				ImportRecordContributor returnValue = ImportRecordContributorInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.ImportRecordID,
						value.InstitutionCode,
						value.CreationUserID,
						value.LastModifiedUserID);
				
				return new CustomDataAccessStatus<ImportRecordContributor>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (ImportRecordContributorDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.ImportRecordContributorID))
				{
				return new CustomDataAccessStatus<ImportRecordContributor>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<ImportRecordContributor>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				value.LastModifiedUserID = userId;
				ImportRecordContributor returnValue = ImportRecordContributorUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.ImportRecordContributorID,
						value.ImportRecordID,
						value.InstitutionCode,
						value.LastModifiedUserID);
					
				return new CustomDataAccessStatus<ImportRecordContributor>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<ImportRecordContributor>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}

