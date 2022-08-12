
// Generated 1/5/2021 2:17:18 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class OAIRecordRelatedTitleDAL is based upon dbo.OAIRecordRelatedTitle.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHLImport.DAL
// {
// 		public partial class OAIRecordRelatedTitleDAL
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
	partial class OAIRecordRelatedTitleDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from dbo.OAIRecordRelatedTitle by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="oAIRecordRelatedTitleID"></param>
		/// <returns>Object of type OAIRecordRelatedTitle.</returns>
		public OAIRecordRelatedTitle OAIRecordRelatedTitleSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int oAIRecordRelatedTitleID)
		{
			return OAIRecordRelatedTitleSelectAuto(	sqlConnection, sqlTransaction, "BHLImport",	oAIRecordRelatedTitleID );
		}
			
		/// <summary>
		/// Select values from dbo.OAIRecordRelatedTitle by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="oAIRecordRelatedTitleID"></param>
		/// <returns>Object of type OAIRecordRelatedTitle.</returns>
		public OAIRecordRelatedTitle OAIRecordRelatedTitleSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int oAIRecordRelatedTitleID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("OAIRecordRelatedTitleSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("OAIRecordRelatedTitleID", SqlDbType.Int, null, false, oAIRecordRelatedTitleID)))
			{
				using (CustomSqlHelper<OAIRecordRelatedTitle> helper = new CustomSqlHelper<OAIRecordRelatedTitle>())
				{
					List<OAIRecordRelatedTitle> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						OAIRecordRelatedTitle o = list[0];
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
		/// Select values from dbo.OAIRecordRelatedTitle by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="oAIRecordRelatedTitleID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> OAIRecordRelatedTitleSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int oAIRecordRelatedTitleID)
		{
			return OAIRecordRelatedTitleSelectAutoRaw( sqlConnection, sqlTransaction, "BHLImport", oAIRecordRelatedTitleID );
		}
		
		/// <summary>
		/// Select values from dbo.OAIRecordRelatedTitle by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="oAIRecordRelatedTitleID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> OAIRecordRelatedTitleSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int oAIRecordRelatedTitleID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("OAIRecordRelatedTitleSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("OAIRecordRelatedTitleID", SqlDbType.Int, null, false, oAIRecordRelatedTitleID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====

 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into dbo.OAIRecordRelatedTitle.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="oAIRecordID"></param>
		/// <param name="titleType"></param>
		/// <param name="title"></param>
		/// <param name="productionEntityType"></param>
		/// <param name="productionEntityID"></param>
		/// <returns>Object of type OAIRecordRelatedTitle.</returns>
		public OAIRecordRelatedTitle OAIRecordRelatedTitleInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int oAIRecordID,
			string titleType,
			string title,
			string productionEntityType,
			int? productionEntityID)
		{
			return OAIRecordRelatedTitleInsertAuto( sqlConnection, sqlTransaction, "BHLImport", oAIRecordID, titleType, title, productionEntityType, productionEntityID );
		}
		
		/// <summary>
		/// Insert values into dbo.OAIRecordRelatedTitle.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="oAIRecordID"></param>
		/// <param name="titleType"></param>
		/// <param name="title"></param>
		/// <param name="productionEntityType"></param>
		/// <param name="productionEntityID"></param>
		/// <returns>Object of type OAIRecordRelatedTitle.</returns>
		public OAIRecordRelatedTitle OAIRecordRelatedTitleInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int oAIRecordID,
			string titleType,
			string title,
			string productionEntityType,
			int? productionEntityID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("OAIRecordRelatedTitleInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("OAIRecordRelatedTitleID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("OAIRecordID", SqlDbType.Int, null, false, oAIRecordID),
					CustomSqlHelper.CreateInputParameter("TitleType", SqlDbType.NVarChar, 50, false, titleType),
					CustomSqlHelper.CreateInputParameter("Title", SqlDbType.NVarChar, 300, false, title),
					CustomSqlHelper.CreateInputParameter("ProductionEntityType", SqlDbType.NVarChar, 15, true, productionEntityType),
					CustomSqlHelper.CreateInputParameter("ProductionEntityID", SqlDbType.Int, null, true, productionEntityID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<OAIRecordRelatedTitle> helper = new CustomSqlHelper<OAIRecordRelatedTitle>())
				{
					List<OAIRecordRelatedTitle> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						OAIRecordRelatedTitle o = list[0];
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
		/// Insert values into dbo.OAIRecordRelatedTitle. Returns an object of type OAIRecordRelatedTitle.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type OAIRecordRelatedTitle.</param>
		/// <returns>Object of type OAIRecordRelatedTitle.</returns>
		public OAIRecordRelatedTitle OAIRecordRelatedTitleInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			OAIRecordRelatedTitle value)
		{
			return OAIRecordRelatedTitleInsertAuto(sqlConnection, sqlTransaction, "BHLImport", value);
		}
		
		/// <summary>
		/// Insert values into dbo.OAIRecordRelatedTitle. Returns an object of type OAIRecordRelatedTitle.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type OAIRecordRelatedTitle.</param>
		/// <returns>Object of type OAIRecordRelatedTitle.</returns>
		public OAIRecordRelatedTitle OAIRecordRelatedTitleInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			OAIRecordRelatedTitle value)
		{
			return OAIRecordRelatedTitleInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.OAIRecordID,
				value.TitleType,
				value.Title,
				value.ProductionEntityType,
				value.ProductionEntityID);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from dbo.OAIRecordRelatedTitle by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="oAIRecordRelatedTitleID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool OAIRecordRelatedTitleDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int oAIRecordRelatedTitleID)
		{
			return OAIRecordRelatedTitleDeleteAuto( sqlConnection, sqlTransaction, "BHLImport", oAIRecordRelatedTitleID );
		}
		
		/// <summary>
		/// Delete values from dbo.OAIRecordRelatedTitle by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="oAIRecordRelatedTitleID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool OAIRecordRelatedTitleDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int oAIRecordRelatedTitleID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("OAIRecordRelatedTitleDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("OAIRecordRelatedTitleID", SqlDbType.Int, null, false, oAIRecordRelatedTitleID), 
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
		/// Update values in dbo.OAIRecordRelatedTitle. Returns an object of type OAIRecordRelatedTitle.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="oAIRecordRelatedTitleID"></param>
		/// <param name="oAIRecordID"></param>
		/// <param name="titleType"></param>
		/// <param name="title"></param>
		/// <param name="productionEntityType"></param>
		/// <param name="productionEntityID"></param>
		/// <returns>Object of type OAIRecordRelatedTitle.</returns>
		public OAIRecordRelatedTitle OAIRecordRelatedTitleUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int oAIRecordRelatedTitleID,
			int oAIRecordID,
			string titleType,
			string title,
			string productionEntityType,
			int? productionEntityID)
		{
			return OAIRecordRelatedTitleUpdateAuto( sqlConnection, sqlTransaction, "BHLImport", oAIRecordRelatedTitleID, oAIRecordID, titleType, title, productionEntityType, productionEntityID);
		}
		
		/// <summary>
		/// Update values in dbo.OAIRecordRelatedTitle. Returns an object of type OAIRecordRelatedTitle.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="oAIRecordRelatedTitleID"></param>
		/// <param name="oAIRecordID"></param>
		/// <param name="titleType"></param>
		/// <param name="title"></param>
		/// <param name="productionEntityType"></param>
		/// <param name="productionEntityID"></param>
		/// <returns>Object of type OAIRecordRelatedTitle.</returns>
		public OAIRecordRelatedTitle OAIRecordRelatedTitleUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int oAIRecordRelatedTitleID,
			int oAIRecordID,
			string titleType,
			string title,
			string productionEntityType,
			int? productionEntityID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("OAIRecordRelatedTitleUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("OAIRecordRelatedTitleID", SqlDbType.Int, null, false, oAIRecordRelatedTitleID),
					CustomSqlHelper.CreateInputParameter("OAIRecordID", SqlDbType.Int, null, false, oAIRecordID),
					CustomSqlHelper.CreateInputParameter("TitleType", SqlDbType.NVarChar, 50, false, titleType),
					CustomSqlHelper.CreateInputParameter("Title", SqlDbType.NVarChar, 300, false, title),
					CustomSqlHelper.CreateInputParameter("ProductionEntityType", SqlDbType.NVarChar, 15, true, productionEntityType),
					CustomSqlHelper.CreateInputParameter("ProductionEntityID", SqlDbType.Int, null, true, productionEntityID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<OAIRecordRelatedTitle> helper = new CustomSqlHelper<OAIRecordRelatedTitle>())
				{
					List<OAIRecordRelatedTitle> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						OAIRecordRelatedTitle o = list[0];
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
		/// Update values in dbo.OAIRecordRelatedTitle. Returns an object of type OAIRecordRelatedTitle.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type OAIRecordRelatedTitle.</param>
		/// <returns>Object of type OAIRecordRelatedTitle.</returns>
		public OAIRecordRelatedTitle OAIRecordRelatedTitleUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			OAIRecordRelatedTitle value)
		{
			return OAIRecordRelatedTitleUpdateAuto(sqlConnection, sqlTransaction, "BHLImport", value );
		}
		
		/// <summary>
		/// Update values in dbo.OAIRecordRelatedTitle. Returns an object of type OAIRecordRelatedTitle.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type OAIRecordRelatedTitle.</param>
		/// <returns>Object of type OAIRecordRelatedTitle.</returns>
		public OAIRecordRelatedTitle OAIRecordRelatedTitleUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			OAIRecordRelatedTitle value)
		{
			return OAIRecordRelatedTitleUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.OAIRecordRelatedTitleID,
				value.OAIRecordID,
				value.TitleType,
				value.Title,
				value.ProductionEntityType,
				value.ProductionEntityID);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage dbo.OAIRecordRelatedTitle object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.OAIRecordRelatedTitle.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type OAIRecordRelatedTitle.</param>
		/// <returns>Object of type CustomDataAccessStatus<OAIRecordRelatedTitle>.</returns>
		public CustomDataAccessStatus<OAIRecordRelatedTitle> OAIRecordRelatedTitleManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			OAIRecordRelatedTitle value  )
		{
			return OAIRecordRelatedTitleManageAuto( sqlConnection, sqlTransaction, "BHLImport", value  );
		}
		
		/// <summary>
		/// Manage dbo.OAIRecordRelatedTitle object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.OAIRecordRelatedTitle.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type OAIRecordRelatedTitle.</param>
		/// <returns>Object of type CustomDataAccessStatus<OAIRecordRelatedTitle>.</returns>
		public CustomDataAccessStatus<OAIRecordRelatedTitle> OAIRecordRelatedTitleManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			OAIRecordRelatedTitle value  )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				
				
				OAIRecordRelatedTitle returnValue = OAIRecordRelatedTitleInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.OAIRecordID,
						value.TitleType,
						value.Title,
						value.ProductionEntityType,
						value.ProductionEntityID);
				
				return new CustomDataAccessStatus<OAIRecordRelatedTitle>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (OAIRecordRelatedTitleDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.OAIRecordRelatedTitleID))
				{
				return new CustomDataAccessStatus<OAIRecordRelatedTitle>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<OAIRecordRelatedTitle>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				
				OAIRecordRelatedTitle returnValue = OAIRecordRelatedTitleUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.OAIRecordRelatedTitleID,
						value.OAIRecordID,
						value.TitleType,
						value.Title,
						value.ProductionEntityType,
						value.ProductionEntityID);
					
				return new CustomDataAccessStatus<OAIRecordRelatedTitle>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<OAIRecordRelatedTitle>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}

