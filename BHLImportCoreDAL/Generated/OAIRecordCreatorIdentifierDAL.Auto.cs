
// Generated 6/24/2022 2:30:17 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class OAIRecordCreatorIdentifierDAL is based upon dbo.OAIRecordCreatorIdentifier.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHLImport.DAL
// {
// 		public partial class OAIRecordCreatorIdentifierDAL
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
	partial class OAIRecordCreatorIdentifierDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from dbo.OAIRecordCreatorIdentifier by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="oAIRecordCreatorIdentifierID"></param>
		/// <returns>Object of type OAIRecordCreatorIdentifier.</returns>
		public OAIRecordCreatorIdentifier OAIRecordCreatorIdentifierSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int oAIRecordCreatorIdentifierID)
		{
			return OAIRecordCreatorIdentifierSelectAuto(	sqlConnection, sqlTransaction, "BHLImport",	oAIRecordCreatorIdentifierID );
		}
			
		/// <summary>
		/// Select values from dbo.OAIRecordCreatorIdentifier by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="oAIRecordCreatorIdentifierID"></param>
		/// <returns>Object of type OAIRecordCreatorIdentifier.</returns>
		public OAIRecordCreatorIdentifier OAIRecordCreatorIdentifierSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int oAIRecordCreatorIdentifierID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("OAIRecordCreatorIdentifierSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("OAIRecordCreatorIdentifierID", SqlDbType.Int, null, false, oAIRecordCreatorIdentifierID)))
			{
				using (CustomSqlHelper<OAIRecordCreatorIdentifier> helper = new CustomSqlHelper<OAIRecordCreatorIdentifier>())
				{
					List<OAIRecordCreatorIdentifier> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						OAIRecordCreatorIdentifier o = list[0];
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
		/// Select values from dbo.OAIRecordCreatorIdentifier by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="oAIRecordCreatorIdentifierID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> OAIRecordCreatorIdentifierSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int oAIRecordCreatorIdentifierID)
		{
			return OAIRecordCreatorIdentifierSelectAutoRaw( sqlConnection, sqlTransaction, "BHLImport", oAIRecordCreatorIdentifierID );
		}
		
		/// <summary>
		/// Select values from dbo.OAIRecordCreatorIdentifier by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="oAIRecordCreatorIdentifierID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> OAIRecordCreatorIdentifierSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int oAIRecordCreatorIdentifierID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("OAIRecordCreatorIdentifierSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("OAIRecordCreatorIdentifierID", SqlDbType.Int, null, false, oAIRecordCreatorIdentifierID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====

 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into dbo.OAIRecordCreatorIdentifier.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="oAIRecordCreatorID"></param>
		/// <param name="identifierType"></param>
		/// <param name="identifierValue"></param>
		/// <returns>Object of type OAIRecordCreatorIdentifier.</returns>
		public OAIRecordCreatorIdentifier OAIRecordCreatorIdentifierInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int oAIRecordCreatorID,
			string identifierType,
			string identifierValue)
		{
			return OAIRecordCreatorIdentifierInsertAuto( sqlConnection, sqlTransaction, "BHLImport", oAIRecordCreatorID, identifierType, identifierValue );
		}
		
		/// <summary>
		/// Insert values into dbo.OAIRecordCreatorIdentifier.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="oAIRecordCreatorID"></param>
		/// <param name="identifierType"></param>
		/// <param name="identifierValue"></param>
		/// <returns>Object of type OAIRecordCreatorIdentifier.</returns>
		public OAIRecordCreatorIdentifier OAIRecordCreatorIdentifierInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int oAIRecordCreatorID,
			string identifierType,
			string identifierValue)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("OAIRecordCreatorIdentifierInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("OAIRecordCreatorIdentifierID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("OAIRecordCreatorID", SqlDbType.Int, null, false, oAIRecordCreatorID),
					CustomSqlHelper.CreateInputParameter("IdentifierType", SqlDbType.NVarChar, 40, false, identifierType),
					CustomSqlHelper.CreateInputParameter("IdentifierValue", SqlDbType.NVarChar, 125, false, identifierValue), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<OAIRecordCreatorIdentifier> helper = new CustomSqlHelper<OAIRecordCreatorIdentifier>())
				{
					List<OAIRecordCreatorIdentifier> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						OAIRecordCreatorIdentifier o = list[0];
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
		/// Insert values into dbo.OAIRecordCreatorIdentifier. Returns an object of type OAIRecordCreatorIdentifier.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type OAIRecordCreatorIdentifier.</param>
		/// <returns>Object of type OAIRecordCreatorIdentifier.</returns>
		public OAIRecordCreatorIdentifier OAIRecordCreatorIdentifierInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			OAIRecordCreatorIdentifier value)
		{
			return OAIRecordCreatorIdentifierInsertAuto(sqlConnection, sqlTransaction, "BHLImport", value);
		}
		
		/// <summary>
		/// Insert values into dbo.OAIRecordCreatorIdentifier. Returns an object of type OAIRecordCreatorIdentifier.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type OAIRecordCreatorIdentifier.</param>
		/// <returns>Object of type OAIRecordCreatorIdentifier.</returns>
		public OAIRecordCreatorIdentifier OAIRecordCreatorIdentifierInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			OAIRecordCreatorIdentifier value)
		{
			return OAIRecordCreatorIdentifierInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.OAIRecordCreatorID,
				value.IdentifierType,
				value.IdentifierValue);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from dbo.OAIRecordCreatorIdentifier by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="oAIRecordCreatorIdentifierID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool OAIRecordCreatorIdentifierDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int oAIRecordCreatorIdentifierID)
		{
			return OAIRecordCreatorIdentifierDeleteAuto( sqlConnection, sqlTransaction, "BHLImport", oAIRecordCreatorIdentifierID );
		}
		
		/// <summary>
		/// Delete values from dbo.OAIRecordCreatorIdentifier by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="oAIRecordCreatorIdentifierID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool OAIRecordCreatorIdentifierDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int oAIRecordCreatorIdentifierID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("OAIRecordCreatorIdentifierDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("OAIRecordCreatorIdentifierID", SqlDbType.Int, null, false, oAIRecordCreatorIdentifierID), 
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
		/// Update values in dbo.OAIRecordCreatorIdentifier. Returns an object of type OAIRecordCreatorIdentifier.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="oAIRecordCreatorIdentifierID"></param>
		/// <param name="oAIRecordCreatorID"></param>
		/// <param name="identifierType"></param>
		/// <param name="identifierValue"></param>
		/// <returns>Object of type OAIRecordCreatorIdentifier.</returns>
		public OAIRecordCreatorIdentifier OAIRecordCreatorIdentifierUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int oAIRecordCreatorIdentifierID,
			int oAIRecordCreatorID,
			string identifierType,
			string identifierValue)
		{
			return OAIRecordCreatorIdentifierUpdateAuto( sqlConnection, sqlTransaction, "BHLImport", oAIRecordCreatorIdentifierID, oAIRecordCreatorID, identifierType, identifierValue);
		}
		
		/// <summary>
		/// Update values in dbo.OAIRecordCreatorIdentifier. Returns an object of type OAIRecordCreatorIdentifier.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="oAIRecordCreatorIdentifierID"></param>
		/// <param name="oAIRecordCreatorID"></param>
		/// <param name="identifierType"></param>
		/// <param name="identifierValue"></param>
		/// <returns>Object of type OAIRecordCreatorIdentifier.</returns>
		public OAIRecordCreatorIdentifier OAIRecordCreatorIdentifierUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int oAIRecordCreatorIdentifierID,
			int oAIRecordCreatorID,
			string identifierType,
			string identifierValue)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("OAIRecordCreatorIdentifierUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("OAIRecordCreatorIdentifierID", SqlDbType.Int, null, false, oAIRecordCreatorIdentifierID),
					CustomSqlHelper.CreateInputParameter("OAIRecordCreatorID", SqlDbType.Int, null, false, oAIRecordCreatorID),
					CustomSqlHelper.CreateInputParameter("IdentifierType", SqlDbType.NVarChar, 40, false, identifierType),
					CustomSqlHelper.CreateInputParameter("IdentifierValue", SqlDbType.NVarChar, 125, false, identifierValue), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<OAIRecordCreatorIdentifier> helper = new CustomSqlHelper<OAIRecordCreatorIdentifier>())
				{
					List<OAIRecordCreatorIdentifier> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						OAIRecordCreatorIdentifier o = list[0];
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
		/// Update values in dbo.OAIRecordCreatorIdentifier. Returns an object of type OAIRecordCreatorIdentifier.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type OAIRecordCreatorIdentifier.</param>
		/// <returns>Object of type OAIRecordCreatorIdentifier.</returns>
		public OAIRecordCreatorIdentifier OAIRecordCreatorIdentifierUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			OAIRecordCreatorIdentifier value)
		{
			return OAIRecordCreatorIdentifierUpdateAuto(sqlConnection, sqlTransaction, "BHLImport", value );
		}
		
		/// <summary>
		/// Update values in dbo.OAIRecordCreatorIdentifier. Returns an object of type OAIRecordCreatorIdentifier.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type OAIRecordCreatorIdentifier.</param>
		/// <returns>Object of type OAIRecordCreatorIdentifier.</returns>
		public OAIRecordCreatorIdentifier OAIRecordCreatorIdentifierUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			OAIRecordCreatorIdentifier value)
		{
			return OAIRecordCreatorIdentifierUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.OAIRecordCreatorIdentifierID,
				value.OAIRecordCreatorID,
				value.IdentifierType,
				value.IdentifierValue);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage dbo.OAIRecordCreatorIdentifier object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.OAIRecordCreatorIdentifier.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type OAIRecordCreatorIdentifier.</param>
		/// <returns>Object of type CustomDataAccessStatus<OAIRecordCreatorIdentifier>.</returns>
		public CustomDataAccessStatus<OAIRecordCreatorIdentifier> OAIRecordCreatorIdentifierManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			OAIRecordCreatorIdentifier value  )
		{
			return OAIRecordCreatorIdentifierManageAuto( sqlConnection, sqlTransaction, "BHLImport", value  );
		}
		
		/// <summary>
		/// Manage dbo.OAIRecordCreatorIdentifier object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.OAIRecordCreatorIdentifier.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type OAIRecordCreatorIdentifier.</param>
		/// <returns>Object of type CustomDataAccessStatus<OAIRecordCreatorIdentifier>.</returns>
		public CustomDataAccessStatus<OAIRecordCreatorIdentifier> OAIRecordCreatorIdentifierManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			OAIRecordCreatorIdentifier value  )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				
				
				OAIRecordCreatorIdentifier returnValue = OAIRecordCreatorIdentifierInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.OAIRecordCreatorID,
						value.IdentifierType,
						value.IdentifierValue);
				
				return new CustomDataAccessStatus<OAIRecordCreatorIdentifier>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (OAIRecordCreatorIdentifierDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.OAIRecordCreatorIdentifierID))
				{
				return new CustomDataAccessStatus<OAIRecordCreatorIdentifier>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<OAIRecordCreatorIdentifier>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				
				OAIRecordCreatorIdentifier returnValue = OAIRecordCreatorIdentifierUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.OAIRecordCreatorIdentifierID,
						value.OAIRecordCreatorID,
						value.IdentifierType,
						value.IdentifierValue);
					
				return new CustomDataAccessStatus<OAIRecordCreatorIdentifier>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<OAIRecordCreatorIdentifier>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}

