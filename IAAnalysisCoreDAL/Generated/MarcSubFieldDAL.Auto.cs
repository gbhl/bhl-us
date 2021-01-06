
// Generated 1/5/2021 12:30:06 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class MarcSubFieldDAL is based upon dbo.MarcSubField.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.IAAnalysis.DAL
// {
// 		public partial class MarcSubFieldDAL
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
using MOBOT.IAAnalysis.DataObjects;

#endregion using

namespace MOBOT.IAAnalysis.DAL
{
	partial class MarcSubFieldDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from dbo.MarcSubField by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="marcSubFieldID"></param>
		/// <returns>Object of type MarcSubField.</returns>
		public MarcSubField MarcSubFieldSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int marcSubFieldID)
		{
			return MarcSubFieldSelectAuto(	sqlConnection, sqlTransaction, "IAAnalysis",	marcSubFieldID );
		}
			
		/// <summary>
		/// Select values from dbo.MarcSubField by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="marcSubFieldID"></param>
		/// <returns>Object of type MarcSubField.</returns>
		public MarcSubField MarcSubFieldSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int marcSubFieldID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("MarcSubFieldSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("MarcSubFieldID", SqlDbType.Int, null, false, marcSubFieldID)))
			{
				using (CustomSqlHelper<MarcSubField> helper = new CustomSqlHelper<MarcSubField>())
				{
					List<MarcSubField> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						MarcSubField o = list[0];
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
		/// Select values from dbo.MarcSubField by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="marcSubFieldID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> MarcSubFieldSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int marcSubFieldID)
		{
			return MarcSubFieldSelectAutoRaw( sqlConnection, sqlTransaction, "IAAnalysis", marcSubFieldID );
		}
		
		/// <summary>
		/// Select values from dbo.MarcSubField by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="marcSubFieldID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> MarcSubFieldSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int marcSubFieldID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("MarcSubFieldSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("MarcSubFieldID", SqlDbType.Int, null, false, marcSubFieldID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====

 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into dbo.MarcSubField.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="marcDataFieldID"></param>
		/// <param name="code"></param>
		/// <param name="value"></param>
		/// <returns>Object of type MarcSubField.</returns>
		public MarcSubField MarcSubFieldInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int marcDataFieldID,
			string code,
			string value)
		{
			return MarcSubFieldInsertAuto( sqlConnection, sqlTransaction, "IAAnalysis", marcDataFieldID, code, value );
		}
		
		/// <summary>
		/// Insert values into dbo.MarcSubField.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="marcDataFieldID"></param>
		/// <param name="code"></param>
		/// <param name="value"></param>
		/// <returns>Object of type MarcSubField.</returns>
		public MarcSubField MarcSubFieldInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int marcDataFieldID,
			string code,
			string value)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("MarcSubFieldInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("MarcSubFieldID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("MarcDataFieldID", SqlDbType.Int, null, false, marcDataFieldID),
					CustomSqlHelper.CreateInputParameter("Code", SqlDbType.NChar, 1, false, code),
					CustomSqlHelper.CreateInputParameter("Value", SqlDbType.NVarChar, 200, false, value), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<MarcSubField> helper = new CustomSqlHelper<MarcSubField>())
				{
					List<MarcSubField> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						MarcSubField o = list[0];
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
		/// Insert values into dbo.MarcSubField. Returns an object of type MarcSubField.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type MarcSubField.</param>
		/// <returns>Object of type MarcSubField.</returns>
		public MarcSubField MarcSubFieldInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			MarcSubField value)
		{
			return MarcSubFieldInsertAuto(sqlConnection, sqlTransaction, "IAAnalysis", value);
		}
		
		/// <summary>
		/// Insert values into dbo.MarcSubField. Returns an object of type MarcSubField.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type MarcSubField.</param>
		/// <returns>Object of type MarcSubField.</returns>
		public MarcSubField MarcSubFieldInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			MarcSubField value)
		{
			return MarcSubFieldInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.MarcDataFieldID,
				value.Code,
				value.Value);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from dbo.MarcSubField by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="marcSubFieldID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool MarcSubFieldDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int marcSubFieldID)
		{
			return MarcSubFieldDeleteAuto( sqlConnection, sqlTransaction, "IAAnalysis", marcSubFieldID );
		}
		
		/// <summary>
		/// Delete values from dbo.MarcSubField by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="marcSubFieldID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool MarcSubFieldDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int marcSubFieldID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("MarcSubFieldDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("MarcSubFieldID", SqlDbType.Int, null, false, marcSubFieldID), 
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
		/// Update values in dbo.MarcSubField. Returns an object of type MarcSubField.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="marcSubFieldID"></param>
		/// <param name="marcDataFieldID"></param>
		/// <param name="code"></param>
		/// <param name="value"></param>
		/// <returns>Object of type MarcSubField.</returns>
		public MarcSubField MarcSubFieldUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int marcSubFieldID,
			int marcDataFieldID,
			string code,
			string value)
		{
			return MarcSubFieldUpdateAuto( sqlConnection, sqlTransaction, "IAAnalysis", marcSubFieldID, marcDataFieldID, code, value);
		}
		
		/// <summary>
		/// Update values in dbo.MarcSubField. Returns an object of type MarcSubField.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="marcSubFieldID"></param>
		/// <param name="marcDataFieldID"></param>
		/// <param name="code"></param>
		/// <param name="value"></param>
		/// <returns>Object of type MarcSubField.</returns>
		public MarcSubField MarcSubFieldUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int marcSubFieldID,
			int marcDataFieldID,
			string code,
			string value)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("MarcSubFieldUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("MarcSubFieldID", SqlDbType.Int, null, false, marcSubFieldID),
					CustomSqlHelper.CreateInputParameter("MarcDataFieldID", SqlDbType.Int, null, false, marcDataFieldID),
					CustomSqlHelper.CreateInputParameter("Code", SqlDbType.NChar, 1, false, code),
					CustomSqlHelper.CreateInputParameter("Value", SqlDbType.NVarChar, 200, false, value), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<MarcSubField> helper = new CustomSqlHelper<MarcSubField>())
				{
					List<MarcSubField> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						MarcSubField o = list[0];
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
		/// Update values in dbo.MarcSubField. Returns an object of type MarcSubField.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type MarcSubField.</param>
		/// <returns>Object of type MarcSubField.</returns>
		public MarcSubField MarcSubFieldUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			MarcSubField value)
		{
			return MarcSubFieldUpdateAuto(sqlConnection, sqlTransaction, "IAAnalysis", value );
		}
		
		/// <summary>
		/// Update values in dbo.MarcSubField. Returns an object of type MarcSubField.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type MarcSubField.</param>
		/// <returns>Object of type MarcSubField.</returns>
		public MarcSubField MarcSubFieldUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			MarcSubField value)
		{
			return MarcSubFieldUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.MarcSubFieldID,
				value.MarcDataFieldID,
				value.Code,
				value.Value);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage dbo.MarcSubField object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.MarcSubField.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type MarcSubField.</param>
		/// <returns>Object of type CustomDataAccessStatus<MarcSubField>.</returns>
		public CustomDataAccessStatus<MarcSubField> MarcSubFieldManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			MarcSubField value  )
		{
			return MarcSubFieldManageAuto( sqlConnection, sqlTransaction, "IAAnalysis", value  );
		}
		
		/// <summary>
		/// Manage dbo.MarcSubField object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.MarcSubField.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type MarcSubField.</param>
		/// <returns>Object of type CustomDataAccessStatus<MarcSubField>.</returns>
		public CustomDataAccessStatus<MarcSubField> MarcSubFieldManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			MarcSubField value  )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				
				
				MarcSubField returnValue = MarcSubFieldInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.MarcDataFieldID,
						value.Code,
						value.Value);
				
				return new CustomDataAccessStatus<MarcSubField>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (MarcSubFieldDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.MarcSubFieldID))
				{
				return new CustomDataAccessStatus<MarcSubField>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<MarcSubField>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				
				MarcSubField returnValue = MarcSubFieldUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.MarcSubFieldID,
						value.MarcDataFieldID,
						value.Code,
						value.Value);
					
				return new CustomDataAccessStatus<MarcSubField>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<MarcSubField>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}

