
// Generated 1/5/2021 2:17:11 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class OAIRecordDCTypeDAL is based upon dbo.OAIRecordDCType.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHLImport.DAL
// {
// 		public partial class OAIRecordDCTypeDAL
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
	partial class OAIRecordDCTypeDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from dbo.OAIRecordDCType by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="oAIRecordDCTypeID"></param>
		/// <returns>Object of type OAIRecordDCType.</returns>
		public OAIRecordDCType OAIRecordDCTypeSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int oAIRecordDCTypeID)
		{
			return OAIRecordDCTypeSelectAuto(	sqlConnection, sqlTransaction, "BHLImport",	oAIRecordDCTypeID );
		}
			
		/// <summary>
		/// Select values from dbo.OAIRecordDCType by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="oAIRecordDCTypeID"></param>
		/// <returns>Object of type OAIRecordDCType.</returns>
		public OAIRecordDCType OAIRecordDCTypeSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int oAIRecordDCTypeID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("OAIRecordDCTypeSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("OAIRecordDCTypeID", SqlDbType.Int, null, false, oAIRecordDCTypeID)))
			{
				using (CustomSqlHelper<OAIRecordDCType> helper = new CustomSqlHelper<OAIRecordDCType>())
				{
					List<OAIRecordDCType> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						OAIRecordDCType o = list[0];
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
		/// Select values from dbo.OAIRecordDCType by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="oAIRecordDCTypeID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> OAIRecordDCTypeSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int oAIRecordDCTypeID)
		{
			return OAIRecordDCTypeSelectAutoRaw( sqlConnection, sqlTransaction, "BHLImport", oAIRecordDCTypeID );
		}
		
		/// <summary>
		/// Select values from dbo.OAIRecordDCType by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="oAIRecordDCTypeID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> OAIRecordDCTypeSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int oAIRecordDCTypeID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("OAIRecordDCTypeSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("OAIRecordDCTypeID", SqlDbType.Int, null, false, oAIRecordDCTypeID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====

 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into dbo.OAIRecordDCType.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="oAIRecordID"></param>
		/// <param name="dCType"></param>
		/// <returns>Object of type OAIRecordDCType.</returns>
		public OAIRecordDCType OAIRecordDCTypeInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int oAIRecordID,
			string dCType)
		{
			return OAIRecordDCTypeInsertAuto( sqlConnection, sqlTransaction, "BHLImport", oAIRecordID, dCType );
		}
		
		/// <summary>
		/// Insert values into dbo.OAIRecordDCType.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="oAIRecordID"></param>
		/// <param name="dCType"></param>
		/// <returns>Object of type OAIRecordDCType.</returns>
		public OAIRecordDCType OAIRecordDCTypeInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int oAIRecordID,
			string dCType)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("OAIRecordDCTypeInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("OAIRecordDCTypeID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("OAIRecordID", SqlDbType.Int, null, false, oAIRecordID),
					CustomSqlHelper.CreateInputParameter("DCType", SqlDbType.NVarChar, 300, false, dCType), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<OAIRecordDCType> helper = new CustomSqlHelper<OAIRecordDCType>())
				{
					List<OAIRecordDCType> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						OAIRecordDCType o = list[0];
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
		/// Insert values into dbo.OAIRecordDCType. Returns an object of type OAIRecordDCType.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type OAIRecordDCType.</param>
		/// <returns>Object of type OAIRecordDCType.</returns>
		public OAIRecordDCType OAIRecordDCTypeInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			OAIRecordDCType value)
		{
			return OAIRecordDCTypeInsertAuto(sqlConnection, sqlTransaction, "BHLImport", value);
		}
		
		/// <summary>
		/// Insert values into dbo.OAIRecordDCType. Returns an object of type OAIRecordDCType.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type OAIRecordDCType.</param>
		/// <returns>Object of type OAIRecordDCType.</returns>
		public OAIRecordDCType OAIRecordDCTypeInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			OAIRecordDCType value)
		{
			return OAIRecordDCTypeInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.OAIRecordID,
				value.DCType);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from dbo.OAIRecordDCType by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="oAIRecordDCTypeID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool OAIRecordDCTypeDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int oAIRecordDCTypeID)
		{
			return OAIRecordDCTypeDeleteAuto( sqlConnection, sqlTransaction, "BHLImport", oAIRecordDCTypeID );
		}
		
		/// <summary>
		/// Delete values from dbo.OAIRecordDCType by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="oAIRecordDCTypeID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool OAIRecordDCTypeDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int oAIRecordDCTypeID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("OAIRecordDCTypeDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("OAIRecordDCTypeID", SqlDbType.Int, null, false, oAIRecordDCTypeID), 
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
		/// Update values in dbo.OAIRecordDCType. Returns an object of type OAIRecordDCType.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="oAIRecordDCTypeID"></param>
		/// <param name="oAIRecordID"></param>
		/// <param name="dCType"></param>
		/// <returns>Object of type OAIRecordDCType.</returns>
		public OAIRecordDCType OAIRecordDCTypeUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int oAIRecordDCTypeID,
			int oAIRecordID,
			string dCType)
		{
			return OAIRecordDCTypeUpdateAuto( sqlConnection, sqlTransaction, "BHLImport", oAIRecordDCTypeID, oAIRecordID, dCType);
		}
		
		/// <summary>
		/// Update values in dbo.OAIRecordDCType. Returns an object of type OAIRecordDCType.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="oAIRecordDCTypeID"></param>
		/// <param name="oAIRecordID"></param>
		/// <param name="dCType"></param>
		/// <returns>Object of type OAIRecordDCType.</returns>
		public OAIRecordDCType OAIRecordDCTypeUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int oAIRecordDCTypeID,
			int oAIRecordID,
			string dCType)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("OAIRecordDCTypeUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("OAIRecordDCTypeID", SqlDbType.Int, null, false, oAIRecordDCTypeID),
					CustomSqlHelper.CreateInputParameter("OAIRecordID", SqlDbType.Int, null, false, oAIRecordID),
					CustomSqlHelper.CreateInputParameter("DCType", SqlDbType.NVarChar, 300, false, dCType), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<OAIRecordDCType> helper = new CustomSqlHelper<OAIRecordDCType>())
				{
					List<OAIRecordDCType> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						OAIRecordDCType o = list[0];
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
		/// Update values in dbo.OAIRecordDCType. Returns an object of type OAIRecordDCType.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type OAIRecordDCType.</param>
		/// <returns>Object of type OAIRecordDCType.</returns>
		public OAIRecordDCType OAIRecordDCTypeUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			OAIRecordDCType value)
		{
			return OAIRecordDCTypeUpdateAuto(sqlConnection, sqlTransaction, "BHLImport", value );
		}
		
		/// <summary>
		/// Update values in dbo.OAIRecordDCType. Returns an object of type OAIRecordDCType.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type OAIRecordDCType.</param>
		/// <returns>Object of type OAIRecordDCType.</returns>
		public OAIRecordDCType OAIRecordDCTypeUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			OAIRecordDCType value)
		{
			return OAIRecordDCTypeUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.OAIRecordDCTypeID,
				value.OAIRecordID,
				value.DCType);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage dbo.OAIRecordDCType object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.OAIRecordDCType.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type OAIRecordDCType.</param>
		/// <returns>Object of type CustomDataAccessStatus<OAIRecordDCType>.</returns>
		public CustomDataAccessStatus<OAIRecordDCType> OAIRecordDCTypeManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			OAIRecordDCType value  )
		{
			return OAIRecordDCTypeManageAuto( sqlConnection, sqlTransaction, "BHLImport", value  );
		}
		
		/// <summary>
		/// Manage dbo.OAIRecordDCType object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.OAIRecordDCType.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type OAIRecordDCType.</param>
		/// <returns>Object of type CustomDataAccessStatus<OAIRecordDCType>.</returns>
		public CustomDataAccessStatus<OAIRecordDCType> OAIRecordDCTypeManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			OAIRecordDCType value  )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				
				
				OAIRecordDCType returnValue = OAIRecordDCTypeInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.OAIRecordID,
						value.DCType);
				
				return new CustomDataAccessStatus<OAIRecordDCType>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (OAIRecordDCTypeDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.OAIRecordDCTypeID))
				{
				return new CustomDataAccessStatus<OAIRecordDCType>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<OAIRecordDCType>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				
				OAIRecordDCType returnValue = OAIRecordDCTypeUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.OAIRecordDCTypeID,
						value.OAIRecordID,
						value.DCType);
					
				return new CustomDataAccessStatus<OAIRecordDCType>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<OAIRecordDCType>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}

