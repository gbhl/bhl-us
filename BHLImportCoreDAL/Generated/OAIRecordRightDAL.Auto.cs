
// Generated 1/5/2021 2:17:23 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class OAIRecordRightDAL is based upon dbo.OAIRecordRight.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHLImport.DAL
// {
// 		public partial class OAIRecordRightDAL
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
	partial class OAIRecordRightDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from dbo.OAIRecordRight by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="oAIRecordRightID"></param>
		/// <returns>Object of type OAIRecordRight.</returns>
		public OAIRecordRight OAIRecordRightSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int oAIRecordRightID)
		{
			return OAIRecordRightSelectAuto(	sqlConnection, sqlTransaction, "BHLImport",	oAIRecordRightID );
		}
			
		/// <summary>
		/// Select values from dbo.OAIRecordRight by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="oAIRecordRightID"></param>
		/// <returns>Object of type OAIRecordRight.</returns>
		public OAIRecordRight OAIRecordRightSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int oAIRecordRightID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("OAIRecordRightSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("OAIRecordRightID", SqlDbType.Int, null, false, oAIRecordRightID)))
			{
				using (CustomSqlHelper<OAIRecordRight> helper = new CustomSqlHelper<OAIRecordRight>())
				{
					List<OAIRecordRight> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						OAIRecordRight o = list[0];
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
		/// Select values from dbo.OAIRecordRight by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="oAIRecordRightID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> OAIRecordRightSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int oAIRecordRightID)
		{
			return OAIRecordRightSelectAutoRaw( sqlConnection, sqlTransaction, "BHLImport", oAIRecordRightID );
		}
		
		/// <summary>
		/// Select values from dbo.OAIRecordRight by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="oAIRecordRightID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> OAIRecordRightSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int oAIRecordRightID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("OAIRecordRightSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("OAIRecordRightID", SqlDbType.Int, null, false, oAIRecordRightID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====

 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into dbo.OAIRecordRight.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="oAIRecordID"></param>
		/// <param name="right"></param>
		/// <returns>Object of type OAIRecordRight.</returns>
		public OAIRecordRight OAIRecordRightInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int oAIRecordID,
			string right)
		{
			return OAIRecordRightInsertAuto( sqlConnection, sqlTransaction, "BHLImport", oAIRecordID, right );
		}
		
		/// <summary>
		/// Insert values into dbo.OAIRecordRight.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="oAIRecordID"></param>
		/// <param name="right"></param>
		/// <returns>Object of type OAIRecordRight.</returns>
		public OAIRecordRight OAIRecordRightInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int oAIRecordID,
			string right)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("OAIRecordRightInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("OAIRecordRightID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("OAIRecordID", SqlDbType.Int, null, false, oAIRecordID),
					CustomSqlHelper.CreateInputParameter("Right", SqlDbType.NVarChar, 1073741823, false, right), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<OAIRecordRight> helper = new CustomSqlHelper<OAIRecordRight>())
				{
					List<OAIRecordRight> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						OAIRecordRight o = list[0];
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
		/// Insert values into dbo.OAIRecordRight. Returns an object of type OAIRecordRight.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type OAIRecordRight.</param>
		/// <returns>Object of type OAIRecordRight.</returns>
		public OAIRecordRight OAIRecordRightInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			OAIRecordRight value)
		{
			return OAIRecordRightInsertAuto(sqlConnection, sqlTransaction, "BHLImport", value);
		}
		
		/// <summary>
		/// Insert values into dbo.OAIRecordRight. Returns an object of type OAIRecordRight.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type OAIRecordRight.</param>
		/// <returns>Object of type OAIRecordRight.</returns>
		public OAIRecordRight OAIRecordRightInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			OAIRecordRight value)
		{
			return OAIRecordRightInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.OAIRecordID,
				value.Right);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from dbo.OAIRecordRight by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="oAIRecordRightID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool OAIRecordRightDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int oAIRecordRightID)
		{
			return OAIRecordRightDeleteAuto( sqlConnection, sqlTransaction, "BHLImport", oAIRecordRightID );
		}
		
		/// <summary>
		/// Delete values from dbo.OAIRecordRight by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="oAIRecordRightID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool OAIRecordRightDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int oAIRecordRightID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("OAIRecordRightDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("OAIRecordRightID", SqlDbType.Int, null, false, oAIRecordRightID), 
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
		/// Update values in dbo.OAIRecordRight. Returns an object of type OAIRecordRight.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="oAIRecordRightID"></param>
		/// <param name="oAIRecordID"></param>
		/// <param name="right"></param>
		/// <returns>Object of type OAIRecordRight.</returns>
		public OAIRecordRight OAIRecordRightUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int oAIRecordRightID,
			int oAIRecordID,
			string right)
		{
			return OAIRecordRightUpdateAuto( sqlConnection, sqlTransaction, "BHLImport", oAIRecordRightID, oAIRecordID, right);
		}
		
		/// <summary>
		/// Update values in dbo.OAIRecordRight. Returns an object of type OAIRecordRight.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="oAIRecordRightID"></param>
		/// <param name="oAIRecordID"></param>
		/// <param name="right"></param>
		/// <returns>Object of type OAIRecordRight.</returns>
		public OAIRecordRight OAIRecordRightUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int oAIRecordRightID,
			int oAIRecordID,
			string right)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("OAIRecordRightUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("OAIRecordRightID", SqlDbType.Int, null, false, oAIRecordRightID),
					CustomSqlHelper.CreateInputParameter("OAIRecordID", SqlDbType.Int, null, false, oAIRecordID),
					CustomSqlHelper.CreateInputParameter("Right", SqlDbType.NVarChar, 1073741823, false, right), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<OAIRecordRight> helper = new CustomSqlHelper<OAIRecordRight>())
				{
					List<OAIRecordRight> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						OAIRecordRight o = list[0];
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
		/// Update values in dbo.OAIRecordRight. Returns an object of type OAIRecordRight.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type OAIRecordRight.</param>
		/// <returns>Object of type OAIRecordRight.</returns>
		public OAIRecordRight OAIRecordRightUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			OAIRecordRight value)
		{
			return OAIRecordRightUpdateAuto(sqlConnection, sqlTransaction, "BHLImport", value );
		}
		
		/// <summary>
		/// Update values in dbo.OAIRecordRight. Returns an object of type OAIRecordRight.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type OAIRecordRight.</param>
		/// <returns>Object of type OAIRecordRight.</returns>
		public OAIRecordRight OAIRecordRightUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			OAIRecordRight value)
		{
			return OAIRecordRightUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.OAIRecordRightID,
				value.OAIRecordID,
				value.Right);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage dbo.OAIRecordRight object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.OAIRecordRight.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type OAIRecordRight.</param>
		/// <returns>Object of type CustomDataAccessStatus<OAIRecordRight>.</returns>
		public CustomDataAccessStatus<OAIRecordRight> OAIRecordRightManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			OAIRecordRight value  )
		{
			return OAIRecordRightManageAuto( sqlConnection, sqlTransaction, "BHLImport", value  );
		}
		
		/// <summary>
		/// Manage dbo.OAIRecordRight object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.OAIRecordRight.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type OAIRecordRight.</param>
		/// <returns>Object of type CustomDataAccessStatus<OAIRecordRight>.</returns>
		public CustomDataAccessStatus<OAIRecordRight> OAIRecordRightManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			OAIRecordRight value  )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				
				
				OAIRecordRight returnValue = OAIRecordRightInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.OAIRecordID,
						value.Right);
				
				return new CustomDataAccessStatus<OAIRecordRight>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (OAIRecordRightDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.OAIRecordRightID))
				{
				return new CustomDataAccessStatus<OAIRecordRight>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<OAIRecordRight>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				
				OAIRecordRight returnValue = OAIRecordRightUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.OAIRecordRightID,
						value.OAIRecordID,
						value.Right);
					
				return new CustomDataAccessStatus<OAIRecordRight>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<OAIRecordRight>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}

