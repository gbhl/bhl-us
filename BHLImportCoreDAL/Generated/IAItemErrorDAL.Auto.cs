
// Generated 11/18/2009 1:43:59 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class IAItemErrorDAL is based upon IAItemError.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHLImport.DAL
// {
// 		public partial class IAItemErrorDAL
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
	partial class IAItemErrorDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from IAItemError by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="itemErrorID"></param>
		/// <returns>Object of type IAItemError.</returns>
		public IAItemError IAItemErrorSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int itemErrorID)
		{
			return IAItemErrorSelectAuto(	sqlConnection, sqlTransaction, "BHLImport",	itemErrorID );
		}
			
		/// <summary>
		/// Select values from IAItemError by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="itemErrorID"></param>
		/// <returns>Object of type IAItemError.</returns>
		public IAItemError IAItemErrorSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int itemErrorID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IAItemErrorSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("ItemErrorID", SqlDbType.Int, null, false, itemErrorID)))
			{
				using (CustomSqlHelper<IAItemError> helper = new CustomSqlHelper<IAItemError>())
				{
					CustomGenericList<IAItemError> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						IAItemError o = list[0];
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
		/// Select values from IAItemError by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="itemErrorID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> IAItemErrorSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int itemErrorID)
		{
			return IAItemErrorSelectAutoRaw( sqlConnection, sqlTransaction, "BHLImport", itemErrorID );
		}
		
		/// <summary>
		/// Select values from IAItemError by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="itemErrorID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> IAItemErrorSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int itemErrorID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IAItemErrorSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("ItemErrorID", SqlDbType.Int, null, false, itemErrorID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====
	
 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into IAItemError.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="itemID"></param>
		/// <param name="errorDate"></param>
		/// <param name="number"></param>
		/// <param name="severity"></param>
		/// <param name="state"></param>
		/// <param name="procedure"></param>
		/// <param name="line"></param>
		/// <param name="message"></param>
		/// <returns>Object of type IAItemError.</returns>
		public IAItemError IAItemErrorInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int? itemID,
			DateTime errorDate,
			int? number,
			int? severity,
			int? state,
			string procedure,
			int? line,
			string message)
		{
			return IAItemErrorInsertAuto( sqlConnection, sqlTransaction, "BHLImport", itemID, errorDate, number, severity, state, procedure, line, message );
		}
		
		/// <summary>
		/// Insert values into IAItemError.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="itemID"></param>
		/// <param name="errorDate"></param>
		/// <param name="number"></param>
		/// <param name="severity"></param>
		/// <param name="state"></param>
		/// <param name="procedure"></param>
		/// <param name="line"></param>
		/// <param name="message"></param>
		/// <returns>Object of type IAItemError.</returns>
		public IAItemError IAItemErrorInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int? itemID,
			DateTime errorDate,
			int? number,
			int? severity,
			int? state,
			string procedure,
			int? line,
			string message)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IAItemErrorInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("ItemErrorID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, true, itemID),
					CustomSqlHelper.CreateInputParameter("ErrorDate", SqlDbType.DateTime, null, false, errorDate),
					CustomSqlHelper.CreateInputParameter("Number", SqlDbType.Int, null, true, number),
					CustomSqlHelper.CreateInputParameter("Severity", SqlDbType.Int, null, true, severity),
					CustomSqlHelper.CreateInputParameter("State", SqlDbType.Int, null, true, state),
					CustomSqlHelper.CreateInputParameter("Procedure", SqlDbType.NVarChar, 126, true, procedure),
					CustomSqlHelper.CreateInputParameter("Line", SqlDbType.Int, null, true, line),
					CustomSqlHelper.CreateInputParameter("Message", SqlDbType.NVarChar, 4000, true, message), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<IAItemError> helper = new CustomSqlHelper<IAItemError>())
				{
					CustomGenericList<IAItemError> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						IAItemError o = list[0];
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
		/// Insert values into IAItemError. Returns an object of type IAItemError.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type IAItemError.</param>
		/// <returns>Object of type IAItemError.</returns>
		public IAItemError IAItemErrorInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			IAItemError value)
		{
			return IAItemErrorInsertAuto(sqlConnection, sqlTransaction, "BHLImport", value);
		}
		
		/// <summary>
		/// Insert values into IAItemError. Returns an object of type IAItemError.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type IAItemError.</param>
		/// <returns>Object of type IAItemError.</returns>
		public IAItemError IAItemErrorInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			IAItemError value)
		{
			return IAItemErrorInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.ItemID,
				value.ErrorDate,
				value.Number,
				value.Severity,
				value.State,
				value.Procedure,
				value.Line,
				value.Message);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from IAItemError by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="itemErrorID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool IAItemErrorDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int itemErrorID)
		{
			return IAItemErrorDeleteAuto( sqlConnection, sqlTransaction, "BHLImport", itemErrorID );
		}
		
		/// <summary>
		/// Delete values from IAItemError by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="itemErrorID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool IAItemErrorDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int itemErrorID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IAItemErrorDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("ItemErrorID", SqlDbType.Int, null, false, itemErrorID), 
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
		/// Update values in IAItemError. Returns an object of type IAItemError.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="itemErrorID"></param>
		/// <param name="itemID"></param>
		/// <param name="errorDate"></param>
		/// <param name="number"></param>
		/// <param name="severity"></param>
		/// <param name="state"></param>
		/// <param name="procedure"></param>
		/// <param name="line"></param>
		/// <param name="message"></param>
		/// <returns>Object of type IAItemError.</returns>
		public IAItemError IAItemErrorUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int itemErrorID,
			int? itemID,
			DateTime errorDate,
			int? number,
			int? severity,
			int? state,
			string procedure,
			int? line,
			string message)
		{
			return IAItemErrorUpdateAuto( sqlConnection, sqlTransaction, "BHLImport", itemErrorID, itemID, errorDate, number, severity, state, procedure, line, message);
		}
		
		/// <summary>
		/// Update values in IAItemError. Returns an object of type IAItemError.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="itemErrorID"></param>
		/// <param name="itemID"></param>
		/// <param name="errorDate"></param>
		/// <param name="number"></param>
		/// <param name="severity"></param>
		/// <param name="state"></param>
		/// <param name="procedure"></param>
		/// <param name="line"></param>
		/// <param name="message"></param>
		/// <returns>Object of type IAItemError.</returns>
		public IAItemError IAItemErrorUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int itemErrorID,
			int? itemID,
			DateTime errorDate,
			int? number,
			int? severity,
			int? state,
			string procedure,
			int? line,
			string message)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IAItemErrorUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("ItemErrorID", SqlDbType.Int, null, false, itemErrorID),
					CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, true, itemID),
					CustomSqlHelper.CreateInputParameter("ErrorDate", SqlDbType.DateTime, null, false, errorDate),
					CustomSqlHelper.CreateInputParameter("Number", SqlDbType.Int, null, true, number),
					CustomSqlHelper.CreateInputParameter("Severity", SqlDbType.Int, null, true, severity),
					CustomSqlHelper.CreateInputParameter("State", SqlDbType.Int, null, true, state),
					CustomSqlHelper.CreateInputParameter("Procedure", SqlDbType.NVarChar, 126, true, procedure),
					CustomSqlHelper.CreateInputParameter("Line", SqlDbType.Int, null, true, line),
					CustomSqlHelper.CreateInputParameter("Message", SqlDbType.NVarChar, 4000, true, message), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<IAItemError> helper = new CustomSqlHelper<IAItemError>())
				{
					CustomGenericList<IAItemError> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						IAItemError o = list[0];
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
		/// Update values in IAItemError. Returns an object of type IAItemError.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type IAItemError.</param>
		/// <returns>Object of type IAItemError.</returns>
		public IAItemError IAItemErrorUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			IAItemError value)
		{
			return IAItemErrorUpdateAuto(sqlConnection, sqlTransaction, "BHLImport", value );
		}
		
		/// <summary>
		/// Update values in IAItemError. Returns an object of type IAItemError.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type IAItemError.</param>
		/// <returns>Object of type IAItemError.</returns>
		public IAItemError IAItemErrorUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			IAItemError value)
		{
			return IAItemErrorUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.ItemErrorID,
				value.ItemID,
				value.ErrorDate,
				value.Number,
				value.Severity,
				value.State,
				value.Procedure,
				value.Line,
				value.Message);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage IAItemError object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in IAItemError.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type IAItemError.</param>
		/// <returns>Object of type CustomDataAccessStatus<IAItemError>.</returns>
		public CustomDataAccessStatus<IAItemError> IAItemErrorManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			IAItemError value  )
		{
			return IAItemErrorManageAuto( sqlConnection, sqlTransaction, "BHLImport", value  );
		}
		
		/// <summary>
		/// Manage IAItemError object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in IAItemError.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type IAItemError.</param>
		/// <returns>Object of type CustomDataAccessStatus<IAItemError>.</returns>
		public CustomDataAccessStatus<IAItemError> IAItemErrorManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			IAItemError value  )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				
				
				IAItemError returnValue = IAItemErrorInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.ItemID,
						value.ErrorDate,
						value.Number,
						value.Severity,
						value.State,
						value.Procedure,
						value.Line,
						value.Message);
				
				return new CustomDataAccessStatus<IAItemError>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (IAItemErrorDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.ItemErrorID))
				{
				return new CustomDataAccessStatus<IAItemError>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<IAItemError>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				
				IAItemError returnValue = IAItemErrorUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.ItemErrorID,
						value.ItemID,
						value.ErrorDate,
						value.Number,
						value.Severity,
						value.State,
						value.Procedure,
						value.Line,
						value.Message);
					
				return new CustomDataAccessStatus<IAItemError>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<IAItemError>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}
// end of source generation
