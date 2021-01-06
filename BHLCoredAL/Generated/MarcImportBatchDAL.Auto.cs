
// Generated 1/5/2021 3:26:08 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class MarcImportBatchDAL is based upon dbo.MarcImportBatch.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DAL
// {
// 		public partial class MarcImportBatchDAL
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
	partial class MarcImportBatchDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from dbo.MarcImportBatch by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="marcImportBatchID"></param>
		/// <returns>Object of type MarcImportBatch.</returns>
		public MarcImportBatch MarcImportBatchSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int marcImportBatchID)
		{
			return MarcImportBatchSelectAuto(	sqlConnection, sqlTransaction, "BHL",	marcImportBatchID );
		}
			
		/// <summary>
		/// Select values from dbo.MarcImportBatch by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="marcImportBatchID"></param>
		/// <returns>Object of type MarcImportBatch.</returns>
		public MarcImportBatch MarcImportBatchSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int marcImportBatchID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("MarcImportBatchSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("MarcImportBatchID", SqlDbType.Int, null, false, marcImportBatchID)))
			{
				using (CustomSqlHelper<MarcImportBatch> helper = new CustomSqlHelper<MarcImportBatch>())
				{
					List<MarcImportBatch> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						MarcImportBatch o = list[0];
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
		/// Select values from dbo.MarcImportBatch by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="marcImportBatchID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> MarcImportBatchSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int marcImportBatchID)
		{
			return MarcImportBatchSelectAutoRaw( sqlConnection, sqlTransaction, "BHL", marcImportBatchID );
		}
		
		/// <summary>
		/// Select values from dbo.MarcImportBatch by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="marcImportBatchID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> MarcImportBatchSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int marcImportBatchID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("MarcImportBatchSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("MarcImportBatchID", SqlDbType.Int, null, false, marcImportBatchID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====

 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into dbo.MarcImportBatch.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="fileName"></param>
		/// <param name="institutionCode"></param>
		/// <returns>Object of type MarcImportBatch.</returns>
		public MarcImportBatch MarcImportBatchInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string fileName,
			string institutionCode)
		{
			return MarcImportBatchInsertAuto( sqlConnection, sqlTransaction, "BHL", fileName, institutionCode );
		}
		
		/// <summary>
		/// Insert values into dbo.MarcImportBatch.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="fileName"></param>
		/// <param name="institutionCode"></param>
		/// <returns>Object of type MarcImportBatch.</returns>
		public MarcImportBatch MarcImportBatchInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			string fileName,
			string institutionCode)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("MarcImportBatchInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("MarcImportBatchID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("FileName", SqlDbType.NVarChar, 500, false, fileName),
					CustomSqlHelper.CreateInputParameter("InstitutionCode", SqlDbType.NVarChar, 10, true, institutionCode), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<MarcImportBatch> helper = new CustomSqlHelper<MarcImportBatch>())
				{
					List<MarcImportBatch> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						MarcImportBatch o = list[0];
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
		/// Insert values into dbo.MarcImportBatch. Returns an object of type MarcImportBatch.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type MarcImportBatch.</param>
		/// <returns>Object of type MarcImportBatch.</returns>
		public MarcImportBatch MarcImportBatchInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			MarcImportBatch value)
		{
			return MarcImportBatchInsertAuto(sqlConnection, sqlTransaction, "BHL", value);
		}
		
		/// <summary>
		/// Insert values into dbo.MarcImportBatch. Returns an object of type MarcImportBatch.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type MarcImportBatch.</param>
		/// <returns>Object of type MarcImportBatch.</returns>
		public MarcImportBatch MarcImportBatchInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			MarcImportBatch value)
		{
			return MarcImportBatchInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.FileName,
				value.InstitutionCode);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from dbo.MarcImportBatch by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="marcImportBatchID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool MarcImportBatchDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int marcImportBatchID)
		{
			return MarcImportBatchDeleteAuto( sqlConnection, sqlTransaction, "BHL", marcImportBatchID );
		}
		
		/// <summary>
		/// Delete values from dbo.MarcImportBatch by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="marcImportBatchID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool MarcImportBatchDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int marcImportBatchID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("MarcImportBatchDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("MarcImportBatchID", SqlDbType.Int, null, false, marcImportBatchID), 
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
		/// Update values in dbo.MarcImportBatch. Returns an object of type MarcImportBatch.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="marcImportBatchID"></param>
		/// <param name="fileName"></param>
		/// <param name="institutionCode"></param>
		/// <returns>Object of type MarcImportBatch.</returns>
		public MarcImportBatch MarcImportBatchUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int marcImportBatchID,
			string fileName,
			string institutionCode)
		{
			return MarcImportBatchUpdateAuto( sqlConnection, sqlTransaction, "BHL", marcImportBatchID, fileName, institutionCode);
		}
		
		/// <summary>
		/// Update values in dbo.MarcImportBatch. Returns an object of type MarcImportBatch.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="marcImportBatchID"></param>
		/// <param name="fileName"></param>
		/// <param name="institutionCode"></param>
		/// <returns>Object of type MarcImportBatch.</returns>
		public MarcImportBatch MarcImportBatchUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int marcImportBatchID,
			string fileName,
			string institutionCode)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("MarcImportBatchUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("MarcImportBatchID", SqlDbType.Int, null, false, marcImportBatchID),
					CustomSqlHelper.CreateInputParameter("FileName", SqlDbType.NVarChar, 500, false, fileName),
					CustomSqlHelper.CreateInputParameter("InstitutionCode", SqlDbType.NVarChar, 10, true, institutionCode), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<MarcImportBatch> helper = new CustomSqlHelper<MarcImportBatch>())
				{
					List<MarcImportBatch> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						MarcImportBatch o = list[0];
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
		/// Update values in dbo.MarcImportBatch. Returns an object of type MarcImportBatch.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type MarcImportBatch.</param>
		/// <returns>Object of type MarcImportBatch.</returns>
		public MarcImportBatch MarcImportBatchUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			MarcImportBatch value)
		{
			return MarcImportBatchUpdateAuto(sqlConnection, sqlTransaction, "BHL", value );
		}
		
		/// <summary>
		/// Update values in dbo.MarcImportBatch. Returns an object of type MarcImportBatch.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type MarcImportBatch.</param>
		/// <returns>Object of type MarcImportBatch.</returns>
		public MarcImportBatch MarcImportBatchUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			MarcImportBatch value)
		{
			return MarcImportBatchUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.MarcImportBatchID,
				value.FileName,
				value.InstitutionCode);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage dbo.MarcImportBatch object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.MarcImportBatch.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type MarcImportBatch.</param>
		/// <returns>Object of type CustomDataAccessStatus<MarcImportBatch>.</returns>
		public CustomDataAccessStatus<MarcImportBatch> MarcImportBatchManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			MarcImportBatch value  )
		{
			return MarcImportBatchManageAuto( sqlConnection, sqlTransaction, "BHL", value  );
		}
		
		/// <summary>
		/// Manage dbo.MarcImportBatch object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.MarcImportBatch.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type MarcImportBatch.</param>
		/// <returns>Object of type CustomDataAccessStatus<MarcImportBatch>.</returns>
		public CustomDataAccessStatus<MarcImportBatch> MarcImportBatchManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			MarcImportBatch value  )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				
				
				MarcImportBatch returnValue = MarcImportBatchInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.FileName,
						value.InstitutionCode);
				
				return new CustomDataAccessStatus<MarcImportBatch>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (MarcImportBatchDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.MarcImportBatchID))
				{
				return new CustomDataAccessStatus<MarcImportBatch>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<MarcImportBatch>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				
				MarcImportBatch returnValue = MarcImportBatchUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.MarcImportBatchID,
						value.FileName,
						value.InstitutionCode);
					
				return new CustomDataAccessStatus<MarcImportBatch>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<MarcImportBatch>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}

