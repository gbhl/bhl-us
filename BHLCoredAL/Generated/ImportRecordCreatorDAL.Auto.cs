
// Generated 8/27/2019 10:08:10 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class ImportRecordCreatorDAL is based upon import.ImportRecordCreator.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DAL
// {
// 		public partial class ImportRecordCreatorDAL
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
	partial class ImportRecordCreatorDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from import.ImportRecordCreator by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="importRecordCreatorID"></param>
		/// <returns>Object of type ImportRecordCreator.</returns>
		public ImportRecordCreator ImportRecordCreatorSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int importRecordCreatorID)
		{
			return ImportRecordCreatorSelectAuto(	sqlConnection, sqlTransaction, "BHL",	importRecordCreatorID );
		}
			
		/// <summary>
		/// Select values from import.ImportRecordCreator by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="importRecordCreatorID"></param>
		/// <returns>Object of type ImportRecordCreator.</returns>
		public ImportRecordCreator ImportRecordCreatorSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int importRecordCreatorID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("import.ImportRecordCreatorSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("ImportRecordCreatorID", SqlDbType.Int, null, false, importRecordCreatorID)))
			{
				using (CustomSqlHelper<ImportRecordCreator> helper = new CustomSqlHelper<ImportRecordCreator>())
				{
					CustomGenericList<ImportRecordCreator> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						ImportRecordCreator o = list[0];
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
		/// Select values from import.ImportRecordCreator by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="importRecordCreatorID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> ImportRecordCreatorSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int importRecordCreatorID)
		{
			return ImportRecordCreatorSelectAutoRaw( sqlConnection, sqlTransaction, "BHL", importRecordCreatorID );
		}
		
		/// <summary>
		/// Select values from import.ImportRecordCreator by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="importRecordCreatorID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> ImportRecordCreatorSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int importRecordCreatorID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("import.ImportRecordCreatorSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("ImportRecordCreatorID", SqlDbType.Int, null, false, importRecordCreatorID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====

 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into import.ImportRecordCreator.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="importRecordID"></param>
		/// <param name="fullName"></param>
		/// <param name="firstName"></param>
		/// <param name="lastName"></param>
		/// <param name="startYear"></param>
		/// <param name="endYear"></param>
		/// <param name="authorType"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <param name="productionAuthorID"></param>
		/// <param name="importedAuthorID"></param>
		/// <returns>Object of type ImportRecordCreator.</returns>
		public ImportRecordCreator ImportRecordCreatorInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int importRecordID,
			string fullName,
			string firstName,
			string lastName,
			string startYear,
			string endYear,
			string authorType,
			int creationUserID,
			int lastModifiedUserID,
			int? productionAuthorID,
			int? importedAuthorID)
		{
			return ImportRecordCreatorInsertAuto( sqlConnection, sqlTransaction, "BHL", importRecordID, fullName, firstName, lastName, startYear, endYear, authorType, creationUserID, lastModifiedUserID, productionAuthorID, importedAuthorID );
		}
		
		/// <summary>
		/// Insert values into import.ImportRecordCreator.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="importRecordID"></param>
		/// <param name="fullName"></param>
		/// <param name="firstName"></param>
		/// <param name="lastName"></param>
		/// <param name="startYear"></param>
		/// <param name="endYear"></param>
		/// <param name="authorType"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <param name="productionAuthorID"></param>
		/// <param name="importedAuthorID"></param>
		/// <returns>Object of type ImportRecordCreator.</returns>
		public ImportRecordCreator ImportRecordCreatorInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int importRecordID,
			string fullName,
			string firstName,
			string lastName,
			string startYear,
			string endYear,
			string authorType,
			int creationUserID,
			int lastModifiedUserID,
			int? productionAuthorID,
			int? importedAuthorID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("import.ImportRecordCreatorInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("ImportRecordCreatorID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("ImportRecordID", SqlDbType.Int, null, false, importRecordID),
					CustomSqlHelper.CreateInputParameter("FullName", SqlDbType.NVarChar, 300, false, fullName),
					CustomSqlHelper.CreateInputParameter("FirstName", SqlDbType.NVarChar, 150, false, firstName),
					CustomSqlHelper.CreateInputParameter("LastName", SqlDbType.NVarChar, 150, false, lastName),
					CustomSqlHelper.CreateInputParameter("StartYear", SqlDbType.NVarChar, 25, false, startYear),
					CustomSqlHelper.CreateInputParameter("EndYear", SqlDbType.NVarChar, 25, false, endYear),
					CustomSqlHelper.CreateInputParameter("AuthorType", SqlDbType.NVarChar, 50, false, authorType),
					CustomSqlHelper.CreateInputParameter("CreationUserID", SqlDbType.Int, null, false, creationUserID),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, false, lastModifiedUserID),
					CustomSqlHelper.CreateInputParameter("ProductionAuthorID", SqlDbType.Int, null, true, productionAuthorID),
					CustomSqlHelper.CreateInputParameter("ImportedAuthorID", SqlDbType.Int, null, true, importedAuthorID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<ImportRecordCreator> helper = new CustomSqlHelper<ImportRecordCreator>())
				{
					CustomGenericList<ImportRecordCreator> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						ImportRecordCreator o = list[0];
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
		/// Insert values into import.ImportRecordCreator. Returns an object of type ImportRecordCreator.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type ImportRecordCreator.</param>
		/// <returns>Object of type ImportRecordCreator.</returns>
		public ImportRecordCreator ImportRecordCreatorInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			ImportRecordCreator value)
		{
			return ImportRecordCreatorInsertAuto(sqlConnection, sqlTransaction, "BHL", value);
		}
		
		/// <summary>
		/// Insert values into import.ImportRecordCreator. Returns an object of type ImportRecordCreator.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type ImportRecordCreator.</param>
		/// <returns>Object of type ImportRecordCreator.</returns>
		public ImportRecordCreator ImportRecordCreatorInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			ImportRecordCreator value)
		{
			return ImportRecordCreatorInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.ImportRecordID,
				value.FullName,
				value.FirstName,
				value.LastName,
				value.StartYear,
				value.EndYear,
				value.AuthorType,
				value.CreationUserID,
				value.LastModifiedUserID,
				value.ProductionAuthorID,
				value.ImportedAuthorID);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from import.ImportRecordCreator by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="importRecordCreatorID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool ImportRecordCreatorDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int importRecordCreatorID)
		{
			return ImportRecordCreatorDeleteAuto( sqlConnection, sqlTransaction, "BHL", importRecordCreatorID );
		}
		
		/// <summary>
		/// Delete values from import.ImportRecordCreator by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="importRecordCreatorID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool ImportRecordCreatorDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int importRecordCreatorID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("import.ImportRecordCreatorDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("ImportRecordCreatorID", SqlDbType.Int, null, false, importRecordCreatorID), 
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
		/// Update values in import.ImportRecordCreator. Returns an object of type ImportRecordCreator.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="importRecordCreatorID"></param>
		/// <param name="importRecordID"></param>
		/// <param name="fullName"></param>
		/// <param name="firstName"></param>
		/// <param name="lastName"></param>
		/// <param name="startYear"></param>
		/// <param name="endYear"></param>
		/// <param name="authorType"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <param name="productionAuthorID"></param>
		/// <param name="importedAuthorID"></param>
		/// <returns>Object of type ImportRecordCreator.</returns>
		public ImportRecordCreator ImportRecordCreatorUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int importRecordCreatorID,
			int importRecordID,
			string fullName,
			string firstName,
			string lastName,
			string startYear,
			string endYear,
			string authorType,
			int lastModifiedUserID,
			int? productionAuthorID,
			int? importedAuthorID)
		{
			return ImportRecordCreatorUpdateAuto( sqlConnection, sqlTransaction, "BHL", importRecordCreatorID, importRecordID, fullName, firstName, lastName, startYear, endYear, authorType, lastModifiedUserID, productionAuthorID, importedAuthorID);
		}
		
		/// <summary>
		/// Update values in import.ImportRecordCreator. Returns an object of type ImportRecordCreator.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="importRecordCreatorID"></param>
		/// <param name="importRecordID"></param>
		/// <param name="fullName"></param>
		/// <param name="firstName"></param>
		/// <param name="lastName"></param>
		/// <param name="startYear"></param>
		/// <param name="endYear"></param>
		/// <param name="authorType"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <param name="productionAuthorID"></param>
		/// <param name="importedAuthorID"></param>
		/// <returns>Object of type ImportRecordCreator.</returns>
		public ImportRecordCreator ImportRecordCreatorUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int importRecordCreatorID,
			int importRecordID,
			string fullName,
			string firstName,
			string lastName,
			string startYear,
			string endYear,
			string authorType,
			int lastModifiedUserID,
			int? productionAuthorID,
			int? importedAuthorID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("import.ImportRecordCreatorUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("ImportRecordCreatorID", SqlDbType.Int, null, false, importRecordCreatorID),
					CustomSqlHelper.CreateInputParameter("ImportRecordID", SqlDbType.Int, null, false, importRecordID),
					CustomSqlHelper.CreateInputParameter("FullName", SqlDbType.NVarChar, 300, false, fullName),
					CustomSqlHelper.CreateInputParameter("FirstName", SqlDbType.NVarChar, 150, false, firstName),
					CustomSqlHelper.CreateInputParameter("LastName", SqlDbType.NVarChar, 150, false, lastName),
					CustomSqlHelper.CreateInputParameter("StartYear", SqlDbType.NVarChar, 25, false, startYear),
					CustomSqlHelper.CreateInputParameter("EndYear", SqlDbType.NVarChar, 25, false, endYear),
					CustomSqlHelper.CreateInputParameter("AuthorType", SqlDbType.NVarChar, 50, false, authorType),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, false, lastModifiedUserID),
					CustomSqlHelper.CreateInputParameter("ProductionAuthorID", SqlDbType.Int, null, true, productionAuthorID),
					CustomSqlHelper.CreateInputParameter("ImportedAuthorID", SqlDbType.Int, null, true, importedAuthorID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<ImportRecordCreator> helper = new CustomSqlHelper<ImportRecordCreator>())
				{
					CustomGenericList<ImportRecordCreator> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						ImportRecordCreator o = list[0];
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
		/// Update values in import.ImportRecordCreator. Returns an object of type ImportRecordCreator.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type ImportRecordCreator.</param>
		/// <returns>Object of type ImportRecordCreator.</returns>
		public ImportRecordCreator ImportRecordCreatorUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			ImportRecordCreator value)
		{
			return ImportRecordCreatorUpdateAuto(sqlConnection, sqlTransaction, "BHL", value );
		}
		
		/// <summary>
		/// Update values in import.ImportRecordCreator. Returns an object of type ImportRecordCreator.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type ImportRecordCreator.</param>
		/// <returns>Object of type ImportRecordCreator.</returns>
		public ImportRecordCreator ImportRecordCreatorUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			ImportRecordCreator value)
		{
			return ImportRecordCreatorUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.ImportRecordCreatorID,
				value.ImportRecordID,
				value.FullName,
				value.FirstName,
				value.LastName,
				value.StartYear,
				value.EndYear,
				value.AuthorType,
				value.LastModifiedUserID,
				value.ProductionAuthorID,
				value.ImportedAuthorID);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage import.ImportRecordCreator object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in import.ImportRecordCreator.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type ImportRecordCreator.</param>
		/// <returns>Object of type CustomDataAccessStatus<ImportRecordCreator>.</returns>
		public CustomDataAccessStatus<ImportRecordCreator> ImportRecordCreatorManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			ImportRecordCreator value , int userId )
		{
			return ImportRecordCreatorManageAuto( sqlConnection, sqlTransaction, "BHL", value , userId );
		}
		
		/// <summary>
		/// Manage import.ImportRecordCreator object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in import.ImportRecordCreator.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type ImportRecordCreator.</param>
		/// <returns>Object of type CustomDataAccessStatus<ImportRecordCreator>.</returns>
		public CustomDataAccessStatus<ImportRecordCreator> ImportRecordCreatorManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			ImportRecordCreator value , int userId )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				value.CreationUserID = userId;
				value.LastModifiedUserID = userId;
				ImportRecordCreator returnValue = ImportRecordCreatorInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.ImportRecordID,
						value.FullName,
						value.FirstName,
						value.LastName,
						value.StartYear,
						value.EndYear,
						value.AuthorType,
						value.CreationUserID,
						value.LastModifiedUserID,
						value.ProductionAuthorID,
						value.ImportedAuthorID);
				
				return new CustomDataAccessStatus<ImportRecordCreator>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (ImportRecordCreatorDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.ImportRecordCreatorID))
				{
				return new CustomDataAccessStatus<ImportRecordCreator>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<ImportRecordCreator>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				value.LastModifiedUserID = userId;
				ImportRecordCreator returnValue = ImportRecordCreatorUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.ImportRecordCreatorID,
						value.ImportRecordID,
						value.FullName,
						value.FirstName,
						value.LastName,
						value.StartYear,
						value.EndYear,
						value.AuthorType,
						value.LastModifiedUserID,
						value.ProductionAuthorID,
						value.ImportedAuthorID);
					
				return new CustomDataAccessStatus<ImportRecordCreator>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<ImportRecordCreator>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}

