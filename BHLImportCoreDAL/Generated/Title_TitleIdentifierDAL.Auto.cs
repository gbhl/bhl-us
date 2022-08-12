
// Generated 1/5/2021 2:18:33 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class Title_TitleIdentifierDAL is based upon dbo.Title_TitleIdentifier.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHLImport.DAL
// {
// 		public partial class Title_TitleIdentifierDAL
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
using MOBOT.BHLImport.DataObjects;

#endregion using

namespace MOBOT.BHLImport.DAL
{
	partial class Title_TitleIdentifierDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from dbo.Title_TitleIdentifier by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="title_TitleIdentifierID"></param>
		/// <returns>Object of type Title_TitleIdentifier.</returns>
		public Title_TitleIdentifier Title_TitleIdentifierSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int title_TitleIdentifierID)
		{
			return Title_TitleIdentifierSelectAuto(	sqlConnection, sqlTransaction, "BHLImport",	title_TitleIdentifierID );
		}
			
		/// <summary>
		/// Select values from dbo.Title_TitleIdentifier by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="title_TitleIdentifierID"></param>
		/// <returns>Object of type Title_TitleIdentifier.</returns>
		public Title_TitleIdentifier Title_TitleIdentifierSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int title_TitleIdentifierID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("Title_TitleIdentifierSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("Title_TitleIdentifierID", SqlDbType.Int, null, false, title_TitleIdentifierID)))
			{
				using (CustomSqlHelper<Title_TitleIdentifier> helper = new CustomSqlHelper<Title_TitleIdentifier>())
				{
					List<Title_TitleIdentifier> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						Title_TitleIdentifier o = list[0];
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
		/// Select values from dbo.Title_TitleIdentifier by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="title_TitleIdentifierID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> Title_TitleIdentifierSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int title_TitleIdentifierID)
		{
			return Title_TitleIdentifierSelectAutoRaw( sqlConnection, sqlTransaction, "BHLImport", title_TitleIdentifierID );
		}
		
		/// <summary>
		/// Select values from dbo.Title_TitleIdentifier by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="title_TitleIdentifierID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> Title_TitleIdentifierSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int title_TitleIdentifierID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("Title_TitleIdentifierSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("Title_TitleIdentifierID", SqlDbType.Int, null, false, title_TitleIdentifierID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====

 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into dbo.Title_TitleIdentifier.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="importKey"></param>
		/// <param name="importStatusID"></param>
		/// <param name="importSourceID"></param>
		/// <param name="identifierName"></param>
		/// <param name="identifierValue"></param>
		/// <param name="externalCreationDate"></param>
		/// <param name="externalLastModifiedDate"></param>
		/// <param name="productionDate"></param>
		/// <returns>Object of type Title_TitleIdentifier.</returns>
		public Title_TitleIdentifier Title_TitleIdentifierInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string importKey,
			int importStatusID,
			int? importSourceID,
			string identifierName,
			string identifierValue,
			DateTime? externalCreationDate,
			DateTime? externalLastModifiedDate,
			DateTime? productionDate)
		{
			return Title_TitleIdentifierInsertAuto( sqlConnection, sqlTransaction, "BHLImport", importKey, importStatusID, importSourceID, identifierName, identifierValue, externalCreationDate, externalLastModifiedDate, productionDate );
		}
		
		/// <summary>
		/// Insert values into dbo.Title_TitleIdentifier.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="importKey"></param>
		/// <param name="importStatusID"></param>
		/// <param name="importSourceID"></param>
		/// <param name="identifierName"></param>
		/// <param name="identifierValue"></param>
		/// <param name="externalCreationDate"></param>
		/// <param name="externalLastModifiedDate"></param>
		/// <param name="productionDate"></param>
		/// <returns>Object of type Title_TitleIdentifier.</returns>
		public Title_TitleIdentifier Title_TitleIdentifierInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			string importKey,
			int importStatusID,
			int? importSourceID,
			string identifierName,
			string identifierValue,
			DateTime? externalCreationDate,
			DateTime? externalLastModifiedDate,
			DateTime? productionDate)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("Title_TitleIdentifierInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("Title_TitleIdentifierID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("ImportKey", SqlDbType.NVarChar, 50, false, importKey),
					CustomSqlHelper.CreateInputParameter("ImportStatusID", SqlDbType.Int, null, false, importStatusID),
					CustomSqlHelper.CreateInputParameter("ImportSourceID", SqlDbType.Int, null, true, importSourceID),
					CustomSqlHelper.CreateInputParameter("IdentifierName", SqlDbType.NVarChar, 40, false, identifierName),
					CustomSqlHelper.CreateInputParameter("IdentifierValue", SqlDbType.NVarChar, 125, false, identifierValue),
					CustomSqlHelper.CreateInputParameter("ExternalCreationDate", SqlDbType.DateTime, null, true, externalCreationDate),
					CustomSqlHelper.CreateInputParameter("ExternalLastModifiedDate", SqlDbType.DateTime, null, true, externalLastModifiedDate),
					CustomSqlHelper.CreateInputParameter("ProductionDate", SqlDbType.DateTime, null, true, productionDate), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<Title_TitleIdentifier> helper = new CustomSqlHelper<Title_TitleIdentifier>())
				{
					List<Title_TitleIdentifier> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						Title_TitleIdentifier o = list[0];
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
		/// Insert values into dbo.Title_TitleIdentifier. Returns an object of type Title_TitleIdentifier.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type Title_TitleIdentifier.</param>
		/// <returns>Object of type Title_TitleIdentifier.</returns>
		public Title_TitleIdentifier Title_TitleIdentifierInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			Title_TitleIdentifier value)
		{
			return Title_TitleIdentifierInsertAuto(sqlConnection, sqlTransaction, "BHLImport", value);
		}
		
		/// <summary>
		/// Insert values into dbo.Title_TitleIdentifier. Returns an object of type Title_TitleIdentifier.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type Title_TitleIdentifier.</param>
		/// <returns>Object of type Title_TitleIdentifier.</returns>
		public Title_TitleIdentifier Title_TitleIdentifierInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			Title_TitleIdentifier value)
		{
			return Title_TitleIdentifierInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.ImportKey,
				value.ImportStatusID,
				value.ImportSourceID,
				value.IdentifierName,
				value.IdentifierValue,
				value.ExternalCreationDate,
				value.ExternalLastModifiedDate,
				value.ProductionDate);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from dbo.Title_TitleIdentifier by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="title_TitleIdentifierID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool Title_TitleIdentifierDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int title_TitleIdentifierID)
		{
			return Title_TitleIdentifierDeleteAuto( sqlConnection, sqlTransaction, "BHLImport", title_TitleIdentifierID );
		}
		
		/// <summary>
		/// Delete values from dbo.Title_TitleIdentifier by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="title_TitleIdentifierID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool Title_TitleIdentifierDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int title_TitleIdentifierID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("Title_TitleIdentifierDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("Title_TitleIdentifierID", SqlDbType.Int, null, false, title_TitleIdentifierID), 
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
		/// Update values in dbo.Title_TitleIdentifier. Returns an object of type Title_TitleIdentifier.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="title_TitleIdentifierID"></param>
		/// <param name="importKey"></param>
		/// <param name="importStatusID"></param>
		/// <param name="importSourceID"></param>
		/// <param name="identifierName"></param>
		/// <param name="identifierValue"></param>
		/// <param name="externalCreationDate"></param>
		/// <param name="externalLastModifiedDate"></param>
		/// <param name="productionDate"></param>
		/// <returns>Object of type Title_TitleIdentifier.</returns>
		public Title_TitleIdentifier Title_TitleIdentifierUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int title_TitleIdentifierID,
			string importKey,
			int importStatusID,
			int? importSourceID,
			string identifierName,
			string identifierValue,
			DateTime? externalCreationDate,
			DateTime? externalLastModifiedDate,
			DateTime? productionDate)
		{
			return Title_TitleIdentifierUpdateAuto( sqlConnection, sqlTransaction, "BHLImport", title_TitleIdentifierID, importKey, importStatusID, importSourceID, identifierName, identifierValue, externalCreationDate, externalLastModifiedDate, productionDate);
		}
		
		/// <summary>
		/// Update values in dbo.Title_TitleIdentifier. Returns an object of type Title_TitleIdentifier.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="title_TitleIdentifierID"></param>
		/// <param name="importKey"></param>
		/// <param name="importStatusID"></param>
		/// <param name="importSourceID"></param>
		/// <param name="identifierName"></param>
		/// <param name="identifierValue"></param>
		/// <param name="externalCreationDate"></param>
		/// <param name="externalLastModifiedDate"></param>
		/// <param name="productionDate"></param>
		/// <returns>Object of type Title_TitleIdentifier.</returns>
		public Title_TitleIdentifier Title_TitleIdentifierUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int title_TitleIdentifierID,
			string importKey,
			int importStatusID,
			int? importSourceID,
			string identifierName,
			string identifierValue,
			DateTime? externalCreationDate,
			DateTime? externalLastModifiedDate,
			DateTime? productionDate)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("Title_TitleIdentifierUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("Title_TitleIdentifierID", SqlDbType.Int, null, false, title_TitleIdentifierID),
					CustomSqlHelper.CreateInputParameter("ImportKey", SqlDbType.NVarChar, 50, false, importKey),
					CustomSqlHelper.CreateInputParameter("ImportStatusID", SqlDbType.Int, null, false, importStatusID),
					CustomSqlHelper.CreateInputParameter("ImportSourceID", SqlDbType.Int, null, true, importSourceID),
					CustomSqlHelper.CreateInputParameter("IdentifierName", SqlDbType.NVarChar, 40, false, identifierName),
					CustomSqlHelper.CreateInputParameter("IdentifierValue", SqlDbType.NVarChar, 125, false, identifierValue),
					CustomSqlHelper.CreateInputParameter("ExternalCreationDate", SqlDbType.DateTime, null, true, externalCreationDate),
					CustomSqlHelper.CreateInputParameter("ExternalLastModifiedDate", SqlDbType.DateTime, null, true, externalLastModifiedDate),
					CustomSqlHelper.CreateInputParameter("ProductionDate", SqlDbType.DateTime, null, true, productionDate), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<Title_TitleIdentifier> helper = new CustomSqlHelper<Title_TitleIdentifier>())
				{
					List<Title_TitleIdentifier> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						Title_TitleIdentifier o = list[0];
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
		/// Update values in dbo.Title_TitleIdentifier. Returns an object of type Title_TitleIdentifier.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type Title_TitleIdentifier.</param>
		/// <returns>Object of type Title_TitleIdentifier.</returns>
		public Title_TitleIdentifier Title_TitleIdentifierUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			Title_TitleIdentifier value)
		{
			return Title_TitleIdentifierUpdateAuto(sqlConnection, sqlTransaction, "BHLImport", value );
		}
		
		/// <summary>
		/// Update values in dbo.Title_TitleIdentifier. Returns an object of type Title_TitleIdentifier.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type Title_TitleIdentifier.</param>
		/// <returns>Object of type Title_TitleIdentifier.</returns>
		public Title_TitleIdentifier Title_TitleIdentifierUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			Title_TitleIdentifier value)
		{
			return Title_TitleIdentifierUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.Title_TitleIdentifierID,
				value.ImportKey,
				value.ImportStatusID,
				value.ImportSourceID,
				value.IdentifierName,
				value.IdentifierValue,
				value.ExternalCreationDate,
				value.ExternalLastModifiedDate,
				value.ProductionDate);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage dbo.Title_TitleIdentifier object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.Title_TitleIdentifier.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type Title_TitleIdentifier.</param>
		/// <returns>Object of type CustomDataAccessStatus<Title_TitleIdentifier>.</returns>
		public CustomDataAccessStatus<Title_TitleIdentifier> Title_TitleIdentifierManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			Title_TitleIdentifier value  )
		{
			return Title_TitleIdentifierManageAuto( sqlConnection, sqlTransaction, "BHLImport", value  );
		}
		
		/// <summary>
		/// Manage dbo.Title_TitleIdentifier object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.Title_TitleIdentifier.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type Title_TitleIdentifier.</param>
		/// <returns>Object of type CustomDataAccessStatus<Title_TitleIdentifier>.</returns>
		public CustomDataAccessStatus<Title_TitleIdentifier> Title_TitleIdentifierManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			Title_TitleIdentifier value  )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				
				
				Title_TitleIdentifier returnValue = Title_TitleIdentifierInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.ImportKey,
						value.ImportStatusID,
						value.ImportSourceID,
						value.IdentifierName,
						value.IdentifierValue,
						value.ExternalCreationDate,
						value.ExternalLastModifiedDate,
						value.ProductionDate);
				
				return new CustomDataAccessStatus<Title_TitleIdentifier>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (Title_TitleIdentifierDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.Title_TitleIdentifierID))
				{
				return new CustomDataAccessStatus<Title_TitleIdentifier>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<Title_TitleIdentifier>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				
				Title_TitleIdentifier returnValue = Title_TitleIdentifierUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.Title_TitleIdentifierID,
						value.ImportKey,
						value.ImportStatusID,
						value.ImportSourceID,
						value.IdentifierName,
						value.IdentifierValue,
						value.ExternalCreationDate,
						value.ExternalLastModifiedDate,
						value.ProductionDate);
					
				return new CustomDataAccessStatus<Title_TitleIdentifier>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<Title_TitleIdentifier>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}

