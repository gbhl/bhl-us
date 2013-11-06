
// Generated 10/31/2013 4:01:46 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class OAIRecordSubjectDAL is based upon OAIRecordSubject.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHLImport.DAL
// {
// 		public partial class OAIRecordSubjectDAL
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
	partial class OAIRecordSubjectDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from OAIRecordSubject by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="oAIRecordSubjectID"></param>
		/// <returns>Object of type OAIRecordSubject.</returns>
		public OAIRecordSubject OAIRecordSubjectSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int oAIRecordSubjectID)
		{
			return OAIRecordSubjectSelectAuto(	sqlConnection, sqlTransaction, "BHLImport",	oAIRecordSubjectID );
		}
			
		/// <summary>
		/// Select values from OAIRecordSubject by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="oAIRecordSubjectID"></param>
		/// <returns>Object of type OAIRecordSubject.</returns>
		public OAIRecordSubject OAIRecordSubjectSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int oAIRecordSubjectID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("OAIRecordSubjectSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("OAIRecordSubjectID", SqlDbType.Int, null, false, oAIRecordSubjectID)))
			{
				using (CustomSqlHelper<OAIRecordSubject> helper = new CustomSqlHelper<OAIRecordSubject>())
				{
					CustomGenericList<OAIRecordSubject> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						OAIRecordSubject o = list[0];
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
		/// Select values from OAIRecordSubject by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="oAIRecordSubjectID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> OAIRecordSubjectSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int oAIRecordSubjectID)
		{
			return OAIRecordSubjectSelectAutoRaw( sqlConnection, sqlTransaction, "BHLImport", oAIRecordSubjectID );
		}
		
		/// <summary>
		/// Select values from OAIRecordSubject by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="oAIRecordSubjectID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> OAIRecordSubjectSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int oAIRecordSubjectID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("OAIRecordSubjectSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("OAIRecordSubjectID", SqlDbType.Int, null, false, oAIRecordSubjectID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====
	
 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into OAIRecordSubject.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="oAIRecordID"></param>
		/// <param name="keyword"></param>
		/// <param name="productionKeywordID"></param>
		/// <returns>Object of type OAIRecordSubject.</returns>
		public OAIRecordSubject OAIRecordSubjectInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int oAIRecordID,
			string keyword,
			int? productionKeywordID)
		{
			return OAIRecordSubjectInsertAuto( sqlConnection, sqlTransaction, "BHLImport", oAIRecordID, keyword, productionKeywordID );
		}
		
		/// <summary>
		/// Insert values into OAIRecordSubject.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="oAIRecordID"></param>
		/// <param name="keyword"></param>
		/// <param name="productionKeywordID"></param>
		/// <returns>Object of type OAIRecordSubject.</returns>
		public OAIRecordSubject OAIRecordSubjectInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int oAIRecordID,
			string keyword,
			int? productionKeywordID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("OAIRecordSubjectInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("OAIRecordSubjectID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("OAIRecordID", SqlDbType.Int, null, false, oAIRecordID),
					CustomSqlHelper.CreateInputParameter("Keyword", SqlDbType.NVarChar, 50, false, keyword),
					CustomSqlHelper.CreateInputParameter("ProductionKeywordID", SqlDbType.Int, null, true, productionKeywordID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<OAIRecordSubject> helper = new CustomSqlHelper<OAIRecordSubject>())
				{
					CustomGenericList<OAIRecordSubject> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						OAIRecordSubject o = list[0];
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
		/// Insert values into OAIRecordSubject. Returns an object of type OAIRecordSubject.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type OAIRecordSubject.</param>
		/// <returns>Object of type OAIRecordSubject.</returns>
		public OAIRecordSubject OAIRecordSubjectInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			OAIRecordSubject value)
		{
			return OAIRecordSubjectInsertAuto(sqlConnection, sqlTransaction, "BHLImport", value);
		}
		
		/// <summary>
		/// Insert values into OAIRecordSubject. Returns an object of type OAIRecordSubject.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type OAIRecordSubject.</param>
		/// <returns>Object of type OAIRecordSubject.</returns>
		public OAIRecordSubject OAIRecordSubjectInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			OAIRecordSubject value)
		{
			return OAIRecordSubjectInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.OAIRecordID,
				value.Keyword,
				value.ProductionKeywordID);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from OAIRecordSubject by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="oAIRecordSubjectID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool OAIRecordSubjectDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int oAIRecordSubjectID)
		{
			return OAIRecordSubjectDeleteAuto( sqlConnection, sqlTransaction, "BHLImport", oAIRecordSubjectID );
		}
		
		/// <summary>
		/// Delete values from OAIRecordSubject by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="oAIRecordSubjectID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool OAIRecordSubjectDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int oAIRecordSubjectID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("OAIRecordSubjectDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("OAIRecordSubjectID", SqlDbType.Int, null, false, oAIRecordSubjectID), 
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
		/// Update values in OAIRecordSubject. Returns an object of type OAIRecordSubject.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="oAIRecordSubjectID"></param>
		/// <param name="oAIRecordID"></param>
		/// <param name="keyword"></param>
		/// <param name="productionKeywordID"></param>
		/// <returns>Object of type OAIRecordSubject.</returns>
		public OAIRecordSubject OAIRecordSubjectUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int oAIRecordSubjectID,
			int oAIRecordID,
			string keyword,
			int? productionKeywordID)
		{
			return OAIRecordSubjectUpdateAuto( sqlConnection, sqlTransaction, "BHLImport", oAIRecordSubjectID, oAIRecordID, keyword, productionKeywordID);
		}
		
		/// <summary>
		/// Update values in OAIRecordSubject. Returns an object of type OAIRecordSubject.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="oAIRecordSubjectID"></param>
		/// <param name="oAIRecordID"></param>
		/// <param name="keyword"></param>
		/// <param name="productionKeywordID"></param>
		/// <returns>Object of type OAIRecordSubject.</returns>
		public OAIRecordSubject OAIRecordSubjectUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int oAIRecordSubjectID,
			int oAIRecordID,
			string keyword,
			int? productionKeywordID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("OAIRecordSubjectUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("OAIRecordSubjectID", SqlDbType.Int, null, false, oAIRecordSubjectID),
					CustomSqlHelper.CreateInputParameter("OAIRecordID", SqlDbType.Int, null, false, oAIRecordID),
					CustomSqlHelper.CreateInputParameter("Keyword", SqlDbType.NVarChar, 50, false, keyword),
					CustomSqlHelper.CreateInputParameter("ProductionKeywordID", SqlDbType.Int, null, true, productionKeywordID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<OAIRecordSubject> helper = new CustomSqlHelper<OAIRecordSubject>())
				{
					CustomGenericList<OAIRecordSubject> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						OAIRecordSubject o = list[0];
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
		/// Update values in OAIRecordSubject. Returns an object of type OAIRecordSubject.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type OAIRecordSubject.</param>
		/// <returns>Object of type OAIRecordSubject.</returns>
		public OAIRecordSubject OAIRecordSubjectUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			OAIRecordSubject value)
		{
			return OAIRecordSubjectUpdateAuto(sqlConnection, sqlTransaction, "BHLImport", value );
		}
		
		/// <summary>
		/// Update values in OAIRecordSubject. Returns an object of type OAIRecordSubject.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type OAIRecordSubject.</param>
		/// <returns>Object of type OAIRecordSubject.</returns>
		public OAIRecordSubject OAIRecordSubjectUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			OAIRecordSubject value)
		{
			return OAIRecordSubjectUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.OAIRecordSubjectID,
				value.OAIRecordID,
				value.Keyword,
				value.ProductionKeywordID);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage OAIRecordSubject object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in OAIRecordSubject.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type OAIRecordSubject.</param>
		/// <returns>Object of type CustomDataAccessStatus<OAIRecordSubject>.</returns>
		public CustomDataAccessStatus<OAIRecordSubject> OAIRecordSubjectManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			OAIRecordSubject value  )
		{
			return OAIRecordSubjectManageAuto( sqlConnection, sqlTransaction, "BHLImport", value  );
		}
		
		/// <summary>
		/// Manage OAIRecordSubject object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in OAIRecordSubject.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type OAIRecordSubject.</param>
		/// <returns>Object of type CustomDataAccessStatus<OAIRecordSubject>.</returns>
		public CustomDataAccessStatus<OAIRecordSubject> OAIRecordSubjectManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			OAIRecordSubject value  )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				
				
				OAIRecordSubject returnValue = OAIRecordSubjectInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.OAIRecordID,
						value.Keyword,
						value.ProductionKeywordID);
				
				return new CustomDataAccessStatus<OAIRecordSubject>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (OAIRecordSubjectDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.OAIRecordSubjectID))
				{
				return new CustomDataAccessStatus<OAIRecordSubject>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<OAIRecordSubject>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				
				OAIRecordSubject returnValue = OAIRecordSubjectUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.OAIRecordSubjectID,
						value.OAIRecordID,
						value.Keyword,
						value.ProductionKeywordID);
					
				return new CustomDataAccessStatus<OAIRecordSubject>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<OAIRecordSubject>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}
// end of source generation
