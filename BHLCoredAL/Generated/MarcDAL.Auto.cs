
// Generated 5/9/2016 1:52:44 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class MarcDAL is based upon dbo.Marc.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DAL
// {
// 		public partial class MarcDAL
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
	partial class MarcDAL : IMarcDAL
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from dbo.Marc by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="marcID"></param>
		/// <returns>Object of type Marc.</returns>
		public Marc MarcSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int marcID)
		{
			return MarcSelectAuto(	sqlConnection, sqlTransaction, "BHL",	marcID );
		}
			
		/// <summary>
		/// Select values from dbo.Marc by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="marcID"></param>
		/// <returns>Object of type Marc.</returns>
		public Marc MarcSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int marcID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("MarcSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("MarcID", SqlDbType.Int, null, false, marcID)))
			{
				using (CustomSqlHelper<Marc> helper = new CustomSqlHelper<Marc>())
				{
					CustomGenericList<Marc> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						Marc o = list[0];
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
		/// Select values from dbo.Marc by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="marcID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> MarcSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int marcID)
		{
			return MarcSelectAutoRaw( sqlConnection, sqlTransaction, "BHL", marcID );
		}
		
		/// <summary>
		/// Select values from dbo.Marc by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="marcID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> MarcSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int marcID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("MarcSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("MarcID", SqlDbType.Int, null, false, marcID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====

 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into dbo.Marc.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="marcImportStatusID"></param>
		/// <param name="marcImportBatchID"></param>
		/// <param name="marcFileLocation"></param>
		/// <param name="institutionCode"></param>
		/// <param name="leader"></param>
		/// <param name="titleID"></param>
		/// <returns>Object of type Marc.</returns>
		public Marc MarcInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int marcImportStatusID,
			int marcImportBatchID,
			string marcFileLocation,
			string institutionCode,
			string leader,
			int? titleID)
		{
			return MarcInsertAuto( sqlConnection, sqlTransaction, "BHL", marcImportStatusID, marcImportBatchID, marcFileLocation, institutionCode, leader, titleID );
		}
		
		/// <summary>
		/// Insert values into dbo.Marc.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="marcImportStatusID"></param>
		/// <param name="marcImportBatchID"></param>
		/// <param name="marcFileLocation"></param>
		/// <param name="institutionCode"></param>
		/// <param name="leader"></param>
		/// <param name="titleID"></param>
		/// <returns>Object of type Marc.</returns>
		public Marc MarcInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int marcImportStatusID,
			int marcImportBatchID,
			string marcFileLocation,
			string institutionCode,
			string leader,
			int? titleID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("MarcInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("MarcID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("MarcImportStatusID", SqlDbType.Int, null, false, marcImportStatusID),
					CustomSqlHelper.CreateInputParameter("MarcImportBatchID", SqlDbType.Int, null, false, marcImportBatchID),
					CustomSqlHelper.CreateInputParameter("MarcFileLocation", SqlDbType.NVarChar, 500, false, marcFileLocation),
					CustomSqlHelper.CreateInputParameter("InstitutionCode", SqlDbType.NVarChar, 10, true, institutionCode),
					CustomSqlHelper.CreateInputParameter("Leader", SqlDbType.NVarChar, 200, false, leader),
					CustomSqlHelper.CreateInputParameter("TitleID", SqlDbType.Int, null, true, titleID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<Marc> helper = new CustomSqlHelper<Marc>())
				{
					CustomGenericList<Marc> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						Marc o = list[0];
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
		/// Insert values into dbo.Marc. Returns an object of type Marc.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type Marc.</param>
		/// <returns>Object of type Marc.</returns>
		public Marc MarcInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			Marc value)
		{
			return MarcInsertAuto(sqlConnection, sqlTransaction, "BHL", value);
		}
		
		/// <summary>
		/// Insert values into dbo.Marc. Returns an object of type Marc.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type Marc.</param>
		/// <returns>Object of type Marc.</returns>
		public Marc MarcInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			Marc value)
		{
			return MarcInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.MarcImportStatusID,
				value.MarcImportBatchID,
				value.MarcFileLocation,
				value.InstitutionCode,
				value.Leader,
				value.TitleID);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from dbo.Marc by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="marcID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool MarcDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int marcID)
		{
			return MarcDeleteAuto( sqlConnection, sqlTransaction, "BHL", marcID );
		}
		
		/// <summary>
		/// Delete values from dbo.Marc by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="marcID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool MarcDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int marcID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("MarcDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("MarcID", SqlDbType.Int, null, false, marcID), 
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
		/// Update values in dbo.Marc. Returns an object of type Marc.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="marcID"></param>
		/// <param name="marcImportStatusID"></param>
		/// <param name="marcImportBatchID"></param>
		/// <param name="marcFileLocation"></param>
		/// <param name="institutionCode"></param>
		/// <param name="leader"></param>
		/// <param name="titleID"></param>
		/// <returns>Object of type Marc.</returns>
		public Marc MarcUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int marcID,
			int marcImportStatusID,
			int marcImportBatchID,
			string marcFileLocation,
			string institutionCode,
			string leader,
			int? titleID)
		{
			return MarcUpdateAuto( sqlConnection, sqlTransaction, "BHL", marcID, marcImportStatusID, marcImportBatchID, marcFileLocation, institutionCode, leader, titleID);
		}
		
		/// <summary>
		/// Update values in dbo.Marc. Returns an object of type Marc.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="marcID"></param>
		/// <param name="marcImportStatusID"></param>
		/// <param name="marcImportBatchID"></param>
		/// <param name="marcFileLocation"></param>
		/// <param name="institutionCode"></param>
		/// <param name="leader"></param>
		/// <param name="titleID"></param>
		/// <returns>Object of type Marc.</returns>
		public Marc MarcUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int marcID,
			int marcImportStatusID,
			int marcImportBatchID,
			string marcFileLocation,
			string institutionCode,
			string leader,
			int? titleID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("MarcUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("MarcID", SqlDbType.Int, null, false, marcID),
					CustomSqlHelper.CreateInputParameter("MarcImportStatusID", SqlDbType.Int, null, false, marcImportStatusID),
					CustomSqlHelper.CreateInputParameter("MarcImportBatchID", SqlDbType.Int, null, false, marcImportBatchID),
					CustomSqlHelper.CreateInputParameter("MarcFileLocation", SqlDbType.NVarChar, 500, false, marcFileLocation),
					CustomSqlHelper.CreateInputParameter("InstitutionCode", SqlDbType.NVarChar, 10, true, institutionCode),
					CustomSqlHelper.CreateInputParameter("Leader", SqlDbType.NVarChar, 200, false, leader),
					CustomSqlHelper.CreateInputParameter("TitleID", SqlDbType.Int, null, true, titleID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<Marc> helper = new CustomSqlHelper<Marc>())
				{
					CustomGenericList<Marc> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						Marc o = list[0];
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
		/// Update values in dbo.Marc. Returns an object of type Marc.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type Marc.</param>
		/// <returns>Object of type Marc.</returns>
		public Marc MarcUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			Marc value)
		{
			return MarcUpdateAuto(sqlConnection, sqlTransaction, "BHL", value );
		}
		
		/// <summary>
		/// Update values in dbo.Marc. Returns an object of type Marc.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type Marc.</param>
		/// <returns>Object of type Marc.</returns>
		public Marc MarcUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			Marc value)
		{
			return MarcUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.MarcID,
				value.MarcImportStatusID,
				value.MarcImportBatchID,
				value.MarcFileLocation,
				value.InstitutionCode,
				value.Leader,
				value.TitleID);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage dbo.Marc object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.Marc.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type Marc.</param>
		/// <returns>Object of type CustomDataAccessStatus<Marc>.</returns>
		public CustomDataAccessStatus<Marc> MarcManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			Marc value  )
		{
			return MarcManageAuto( sqlConnection, sqlTransaction, "BHL", value  );
		}
		
		/// <summary>
		/// Manage dbo.Marc object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.Marc.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type Marc.</param>
		/// <returns>Object of type CustomDataAccessStatus<Marc>.</returns>
		public CustomDataAccessStatus<Marc> MarcManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			Marc value  )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				
				
				Marc returnValue = MarcInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.MarcImportStatusID,
						value.MarcImportBatchID,
						value.MarcFileLocation,
						value.InstitutionCode,
						value.Leader,
						value.TitleID);
				
				return new CustomDataAccessStatus<Marc>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (MarcDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.MarcID))
				{
				return new CustomDataAccessStatus<Marc>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<Marc>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				
				Marc returnValue = MarcUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.MarcID,
						value.MarcImportStatusID,
						value.MarcImportBatchID,
						value.MarcFileLocation,
						value.InstitutionCode,
						value.Leader,
						value.TitleID);
					
				return new CustomDataAccessStatus<Marc>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<Marc>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}

