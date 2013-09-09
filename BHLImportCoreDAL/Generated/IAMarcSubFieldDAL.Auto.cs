
// Generated 7/8/2013 2:53:08 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class IAMarcSubFieldDAL is based upon IAMarcSubField.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHLImport.DAL
// {
// 		public partial class IAMarcSubFieldDAL
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
	partial class IAMarcSubFieldDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from IAMarcSubField by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="marcSubFieldID"></param>
		/// <returns>Object of type IAMarcSubField.</returns>
		public IAMarcSubField IAMarcSubFieldSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int marcSubFieldID)
		{
			return IAMarcSubFieldSelectAuto(	sqlConnection, sqlTransaction, "BHLImport",	marcSubFieldID );
		}
			
		/// <summary>
		/// Select values from IAMarcSubField by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="marcSubFieldID"></param>
		/// <returns>Object of type IAMarcSubField.</returns>
		public IAMarcSubField IAMarcSubFieldSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int marcSubFieldID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IAMarcSubFieldSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("MarcSubFieldID", SqlDbType.Int, null, false, marcSubFieldID)))
			{
				using (CustomSqlHelper<IAMarcSubField> helper = new CustomSqlHelper<IAMarcSubField>())
				{
					CustomGenericList<IAMarcSubField> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						IAMarcSubField o = list[0];
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
		/// Select values from IAMarcSubField by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="marcSubFieldID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> IAMarcSubFieldSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int marcSubFieldID)
		{
			return IAMarcSubFieldSelectAutoRaw( sqlConnection, sqlTransaction, "BHLImport", marcSubFieldID );
		}
		
		/// <summary>
		/// Select values from IAMarcSubField by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="marcSubFieldID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> IAMarcSubFieldSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int marcSubFieldID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IAMarcSubFieldSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("MarcSubFieldID", SqlDbType.Int, null, false, marcSubFieldID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====
	
 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into IAMarcSubField.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="marcDataFieldID"></param>
		/// <param name="code"></param>
		/// <param name="value"></param>
		/// <returns>Object of type IAMarcSubField.</returns>
		public IAMarcSubField IAMarcSubFieldInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int marcDataFieldID,
			string code,
			string value)
		{
			return IAMarcSubFieldInsertAuto( sqlConnection, sqlTransaction, "BHLImport", marcDataFieldID, code, value );
		}
		
		/// <summary>
		/// Insert values into IAMarcSubField.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="marcDataFieldID"></param>
		/// <param name="code"></param>
		/// <param name="value"></param>
		/// <returns>Object of type IAMarcSubField.</returns>
		public IAMarcSubField IAMarcSubFieldInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int marcDataFieldID,
			string code,
			string value)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IAMarcSubFieldInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("MarcSubFieldID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("MarcDataFieldID", SqlDbType.Int, null, false, marcDataFieldID),
					CustomSqlHelper.CreateInputParameter("Code", SqlDbType.NChar, 1, false, code),
					CustomSqlHelper.CreateInputParameter("Value", SqlDbType.NVarChar, 2000, false, value), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<IAMarcSubField> helper = new CustomSqlHelper<IAMarcSubField>())
				{
					CustomGenericList<IAMarcSubField> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						IAMarcSubField o = list[0];
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
		/// Insert values into IAMarcSubField. Returns an object of type IAMarcSubField.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type IAMarcSubField.</param>
		/// <returns>Object of type IAMarcSubField.</returns>
		public IAMarcSubField IAMarcSubFieldInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			IAMarcSubField value)
		{
			return IAMarcSubFieldInsertAuto(sqlConnection, sqlTransaction, "BHLImport", value);
		}
		
		/// <summary>
		/// Insert values into IAMarcSubField. Returns an object of type IAMarcSubField.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type IAMarcSubField.</param>
		/// <returns>Object of type IAMarcSubField.</returns>
		public IAMarcSubField IAMarcSubFieldInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			IAMarcSubField value)
		{
			return IAMarcSubFieldInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.MarcDataFieldID,
				value.Code,
				value.Value);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from IAMarcSubField by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="marcSubFieldID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool IAMarcSubFieldDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int marcSubFieldID)
		{
			return IAMarcSubFieldDeleteAuto( sqlConnection, sqlTransaction, "BHLImport", marcSubFieldID );
		}
		
		/// <summary>
		/// Delete values from IAMarcSubField by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="marcSubFieldID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool IAMarcSubFieldDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int marcSubFieldID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IAMarcSubFieldDeleteAuto", connection, transaction, 
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
		/// Update values in IAMarcSubField. Returns an object of type IAMarcSubField.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="marcSubFieldID"></param>
		/// <param name="marcDataFieldID"></param>
		/// <param name="code"></param>
		/// <param name="value"></param>
		/// <returns>Object of type IAMarcSubField.</returns>
		public IAMarcSubField IAMarcSubFieldUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int marcSubFieldID,
			int marcDataFieldID,
			string code,
			string value)
		{
			return IAMarcSubFieldUpdateAuto( sqlConnection, sqlTransaction, "BHLImport", marcSubFieldID, marcDataFieldID, code, value);
		}
		
		/// <summary>
		/// Update values in IAMarcSubField. Returns an object of type IAMarcSubField.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="marcSubFieldID"></param>
		/// <param name="marcDataFieldID"></param>
		/// <param name="code"></param>
		/// <param name="value"></param>
		/// <returns>Object of type IAMarcSubField.</returns>
		public IAMarcSubField IAMarcSubFieldUpdateAuto(
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
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IAMarcSubFieldUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("MarcSubFieldID", SqlDbType.Int, null, false, marcSubFieldID),
					CustomSqlHelper.CreateInputParameter("MarcDataFieldID", SqlDbType.Int, null, false, marcDataFieldID),
					CustomSqlHelper.CreateInputParameter("Code", SqlDbType.NChar, 1, false, code),
					CustomSqlHelper.CreateInputParameter("Value", SqlDbType.NVarChar, 2000, false, value), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<IAMarcSubField> helper = new CustomSqlHelper<IAMarcSubField>())
				{
					CustomGenericList<IAMarcSubField> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						IAMarcSubField o = list[0];
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
		/// Update values in IAMarcSubField. Returns an object of type IAMarcSubField.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type IAMarcSubField.</param>
		/// <returns>Object of type IAMarcSubField.</returns>
		public IAMarcSubField IAMarcSubFieldUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			IAMarcSubField value)
		{
			return IAMarcSubFieldUpdateAuto(sqlConnection, sqlTransaction, "BHLImport", value );
		}
		
		/// <summary>
		/// Update values in IAMarcSubField. Returns an object of type IAMarcSubField.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type IAMarcSubField.</param>
		/// <returns>Object of type IAMarcSubField.</returns>
		public IAMarcSubField IAMarcSubFieldUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			IAMarcSubField value)
		{
			return IAMarcSubFieldUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.MarcSubFieldID,
				value.MarcDataFieldID,
				value.Code,
				value.Value);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage IAMarcSubField object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in IAMarcSubField.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type IAMarcSubField.</param>
		/// <returns>Object of type CustomDataAccessStatus<IAMarcSubField>.</returns>
		public CustomDataAccessStatus<IAMarcSubField> IAMarcSubFieldManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			IAMarcSubField value  )
		{
			return IAMarcSubFieldManageAuto( sqlConnection, sqlTransaction, "BHLImport", value  );
		}
		
		/// <summary>
		/// Manage IAMarcSubField object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in IAMarcSubField.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type IAMarcSubField.</param>
		/// <returns>Object of type CustomDataAccessStatus<IAMarcSubField>.</returns>
		public CustomDataAccessStatus<IAMarcSubField> IAMarcSubFieldManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			IAMarcSubField value  )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				
				
				IAMarcSubField returnValue = IAMarcSubFieldInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.MarcDataFieldID,
						value.Code,
						value.Value);
				
				return new CustomDataAccessStatus<IAMarcSubField>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (IAMarcSubFieldDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.MarcSubFieldID))
				{
				return new CustomDataAccessStatus<IAMarcSubField>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<IAMarcSubField>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				
				IAMarcSubField returnValue = IAMarcSubFieldUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.MarcSubFieldID,
						value.MarcDataFieldID,
						value.Code,
						value.Value);
					
				return new CustomDataAccessStatus<IAMarcSubField>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<IAMarcSubField>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}
// end of source generation
