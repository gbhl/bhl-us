
// Generated 11/20/2013 3:49:07 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class OAIRecordCreatorDAL is based upon OAIRecordCreator.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHLImport.DAL
// {
// 		public partial class OAIRecordCreatorDAL
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
	partial class OAIRecordCreatorDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from OAIRecordCreator by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="oAIRecordCreatorID"></param>
		/// <returns>Object of type OAIRecordCreator.</returns>
		public OAIRecordCreator OAIRecordCreatorSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int oAIRecordCreatorID)
		{
			return OAIRecordCreatorSelectAuto(	sqlConnection, sqlTransaction, "BHLImport",	oAIRecordCreatorID );
		}
			
		/// <summary>
		/// Select values from OAIRecordCreator by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="oAIRecordCreatorID"></param>
		/// <returns>Object of type OAIRecordCreator.</returns>
		public OAIRecordCreator OAIRecordCreatorSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int oAIRecordCreatorID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("OAIRecordCreatorSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("OAIRecordCreatorID", SqlDbType.Int, null, false, oAIRecordCreatorID)))
			{
				using (CustomSqlHelper<OAIRecordCreator> helper = new CustomSqlHelper<OAIRecordCreator>())
				{
					CustomGenericList<OAIRecordCreator> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						OAIRecordCreator o = list[0];
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
		/// Select values from OAIRecordCreator by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="oAIRecordCreatorID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> OAIRecordCreatorSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int oAIRecordCreatorID)
		{
			return OAIRecordCreatorSelectAutoRaw( sqlConnection, sqlTransaction, "BHLImport", oAIRecordCreatorID );
		}
		
		/// <summary>
		/// Select values from OAIRecordCreator by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="oAIRecordCreatorID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> OAIRecordCreatorSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int oAIRecordCreatorID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("OAIRecordCreatorSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("OAIRecordCreatorID", SqlDbType.Int, null, false, oAIRecordCreatorID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====
	
 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into OAIRecordCreator.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="oAIRecordID"></param>
		/// <param name="creatorType"></param>
		/// <param name="fullName"></param>
		/// <param name="dates"></param>
		/// <param name="startDate"></param>
		/// <param name="endDate"></param>
		/// <param name="productionAuthorID"></param>
		/// <returns>Object of type OAIRecordCreator.</returns>
		public OAIRecordCreator OAIRecordCreatorInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int oAIRecordID,
			string creatorType,
			string fullName,
			string dates,
			string startDate,
			string endDate,
			int? productionAuthorID)
		{
			return OAIRecordCreatorInsertAuto( sqlConnection, sqlTransaction, "BHLImport", oAIRecordID, creatorType, fullName, dates, startDate, endDate, productionAuthorID );
		}
		
		/// <summary>
		/// Insert values into OAIRecordCreator.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="oAIRecordID"></param>
		/// <param name="creatorType"></param>
		/// <param name="fullName"></param>
		/// <param name="dates"></param>
		/// <param name="startDate"></param>
		/// <param name="endDate"></param>
		/// <param name="productionAuthorID"></param>
		/// <returns>Object of type OAIRecordCreator.</returns>
		public OAIRecordCreator OAIRecordCreatorInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int oAIRecordID,
			string creatorType,
			string fullName,
			string dates,
			string startDate,
			string endDate,
			int? productionAuthorID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("OAIRecordCreatorInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("OAIRecordCreatorID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("OAIRecordID", SqlDbType.Int, null, false, oAIRecordID),
					CustomSqlHelper.CreateInputParameter("CreatorType", SqlDbType.NVarChar, 50, false, creatorType),
					CustomSqlHelper.CreateInputParameter("FullName", SqlDbType.NVarChar, 300, false, fullName),
					CustomSqlHelper.CreateInputParameter("Dates", SqlDbType.NVarChar, 50, false, dates),
					CustomSqlHelper.CreateInputParameter("StartDate", SqlDbType.NVarChar, 25, false, startDate),
					CustomSqlHelper.CreateInputParameter("EndDate", SqlDbType.NVarChar, 25, false, endDate),
					CustomSqlHelper.CreateInputParameter("ProductionAuthorID", SqlDbType.Int, null, true, productionAuthorID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<OAIRecordCreator> helper = new CustomSqlHelper<OAIRecordCreator>())
				{
					CustomGenericList<OAIRecordCreator> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						OAIRecordCreator o = list[0];
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
		/// Insert values into OAIRecordCreator. Returns an object of type OAIRecordCreator.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type OAIRecordCreator.</param>
		/// <returns>Object of type OAIRecordCreator.</returns>
		public OAIRecordCreator OAIRecordCreatorInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			OAIRecordCreator value)
		{
			return OAIRecordCreatorInsertAuto(sqlConnection, sqlTransaction, "BHLImport", value);
		}
		
		/// <summary>
		/// Insert values into OAIRecordCreator. Returns an object of type OAIRecordCreator.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type OAIRecordCreator.</param>
		/// <returns>Object of type OAIRecordCreator.</returns>
		public OAIRecordCreator OAIRecordCreatorInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			OAIRecordCreator value)
		{
			return OAIRecordCreatorInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.OAIRecordID,
				value.CreatorType,
				value.FullName,
				value.Dates,
				value.StartDate,
				value.EndDate,
				value.ProductionAuthorID);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from OAIRecordCreator by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="oAIRecordCreatorID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool OAIRecordCreatorDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int oAIRecordCreatorID)
		{
			return OAIRecordCreatorDeleteAuto( sqlConnection, sqlTransaction, "BHLImport", oAIRecordCreatorID );
		}
		
		/// <summary>
		/// Delete values from OAIRecordCreator by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="oAIRecordCreatorID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool OAIRecordCreatorDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int oAIRecordCreatorID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("OAIRecordCreatorDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("OAIRecordCreatorID", SqlDbType.Int, null, false, oAIRecordCreatorID), 
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
		/// Update values in OAIRecordCreator. Returns an object of type OAIRecordCreator.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="oAIRecordCreatorID"></param>
		/// <param name="oAIRecordID"></param>
		/// <param name="creatorType"></param>
		/// <param name="fullName"></param>
		/// <param name="dates"></param>
		/// <param name="startDate"></param>
		/// <param name="endDate"></param>
		/// <param name="productionAuthorID"></param>
		/// <returns>Object of type OAIRecordCreator.</returns>
		public OAIRecordCreator OAIRecordCreatorUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int oAIRecordCreatorID,
			int oAIRecordID,
			string creatorType,
			string fullName,
			string dates,
			string startDate,
			string endDate,
			int? productionAuthorID)
		{
			return OAIRecordCreatorUpdateAuto( sqlConnection, sqlTransaction, "BHLImport", oAIRecordCreatorID, oAIRecordID, creatorType, fullName, dates, startDate, endDate, productionAuthorID);
		}
		
		/// <summary>
		/// Update values in OAIRecordCreator. Returns an object of type OAIRecordCreator.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="oAIRecordCreatorID"></param>
		/// <param name="oAIRecordID"></param>
		/// <param name="creatorType"></param>
		/// <param name="fullName"></param>
		/// <param name="dates"></param>
		/// <param name="startDate"></param>
		/// <param name="endDate"></param>
		/// <param name="productionAuthorID"></param>
		/// <returns>Object of type OAIRecordCreator.</returns>
		public OAIRecordCreator OAIRecordCreatorUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int oAIRecordCreatorID,
			int oAIRecordID,
			string creatorType,
			string fullName,
			string dates,
			string startDate,
			string endDate,
			int? productionAuthorID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("OAIRecordCreatorUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("OAIRecordCreatorID", SqlDbType.Int, null, false, oAIRecordCreatorID),
					CustomSqlHelper.CreateInputParameter("OAIRecordID", SqlDbType.Int, null, false, oAIRecordID),
					CustomSqlHelper.CreateInputParameter("CreatorType", SqlDbType.NVarChar, 50, false, creatorType),
					CustomSqlHelper.CreateInputParameter("FullName", SqlDbType.NVarChar, 300, false, fullName),
					CustomSqlHelper.CreateInputParameter("Dates", SqlDbType.NVarChar, 50, false, dates),
					CustomSqlHelper.CreateInputParameter("StartDate", SqlDbType.NVarChar, 25, false, startDate),
					CustomSqlHelper.CreateInputParameter("EndDate", SqlDbType.NVarChar, 25, false, endDate),
					CustomSqlHelper.CreateInputParameter("ProductionAuthorID", SqlDbType.Int, null, true, productionAuthorID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<OAIRecordCreator> helper = new CustomSqlHelper<OAIRecordCreator>())
				{
					CustomGenericList<OAIRecordCreator> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						OAIRecordCreator o = list[0];
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
		/// Update values in OAIRecordCreator. Returns an object of type OAIRecordCreator.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type OAIRecordCreator.</param>
		/// <returns>Object of type OAIRecordCreator.</returns>
		public OAIRecordCreator OAIRecordCreatorUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			OAIRecordCreator value)
		{
			return OAIRecordCreatorUpdateAuto(sqlConnection, sqlTransaction, "BHLImport", value );
		}
		
		/// <summary>
		/// Update values in OAIRecordCreator. Returns an object of type OAIRecordCreator.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type OAIRecordCreator.</param>
		/// <returns>Object of type OAIRecordCreator.</returns>
		public OAIRecordCreator OAIRecordCreatorUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			OAIRecordCreator value)
		{
			return OAIRecordCreatorUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.OAIRecordCreatorID,
				value.OAIRecordID,
				value.CreatorType,
				value.FullName,
				value.Dates,
				value.StartDate,
				value.EndDate,
				value.ProductionAuthorID);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage OAIRecordCreator object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in OAIRecordCreator.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type OAIRecordCreator.</param>
		/// <returns>Object of type CustomDataAccessStatus<OAIRecordCreator>.</returns>
		public CustomDataAccessStatus<OAIRecordCreator> OAIRecordCreatorManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			OAIRecordCreator value  )
		{
			return OAIRecordCreatorManageAuto( sqlConnection, sqlTransaction, "BHLImport", value  );
		}
		
		/// <summary>
		/// Manage OAIRecordCreator object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in OAIRecordCreator.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type OAIRecordCreator.</param>
		/// <returns>Object of type CustomDataAccessStatus<OAIRecordCreator>.</returns>
		public CustomDataAccessStatus<OAIRecordCreator> OAIRecordCreatorManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			OAIRecordCreator value  )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				
				
				OAIRecordCreator returnValue = OAIRecordCreatorInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.OAIRecordID,
						value.CreatorType,
						value.FullName,
						value.Dates,
						value.StartDate,
						value.EndDate,
						value.ProductionAuthorID);
				
				return new CustomDataAccessStatus<OAIRecordCreator>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (OAIRecordCreatorDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.OAIRecordCreatorID))
				{
				return new CustomDataAccessStatus<OAIRecordCreator>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<OAIRecordCreator>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				
				OAIRecordCreator returnValue = OAIRecordCreatorUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.OAIRecordCreatorID,
						value.OAIRecordID,
						value.CreatorType,
						value.FullName,
						value.Dates,
						value.StartDate,
						value.EndDate,
						value.ProductionAuthorID);
					
				return new CustomDataAccessStatus<OAIRecordCreator>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<OAIRecordCreator>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}
// end of source generation
