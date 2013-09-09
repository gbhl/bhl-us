
// Generated 11/12/2008 3:12:29 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class MarcDataFieldDAL is based upon MarcDataField.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.IAAnalysis.DAL
// {
// 		public partial class MarcDataFieldDAL
//		{
//		}
// }

#endregion How To Implement

#region using

using System;
using System.Data;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.IAAnalysis.DataObjects;

#endregion using

namespace MOBOT.IAAnalysis.DAL
{
	partial class MarcDataFieldDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from MarcDataField by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="marcDataFieldID"></param>
		/// <returns>Object of type MarcDataField.</returns>
		public MarcDataField MarcDataFieldSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int marcDataFieldID)
		{
			return MarcDataFieldSelectAuto(	sqlConnection, sqlTransaction, "IAAnalysis",	marcDataFieldID );
		}
			
		/// <summary>
		/// Select values from MarcDataField by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="marcDataFieldID"></param>
		/// <returns>Object of type MarcDataField.</returns>
		public MarcDataField MarcDataFieldSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int marcDataFieldID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("MarcDataFieldSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("MarcDataFieldID", SqlDbType.Int, null, false, marcDataFieldID)))
			{
				using (CustomSqlHelper<MarcDataField> helper = new CustomSqlHelper<MarcDataField>())
				{
					CustomGenericList<MarcDataField> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						MarcDataField o = list[0];
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
		/// Select values from MarcDataField by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="marcDataFieldID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> MarcDataFieldSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int marcDataFieldID)
		{
			return MarcDataFieldSelectAutoRaw( sqlConnection, sqlTransaction, "IAAnalysis", marcDataFieldID );
		}
		
		/// <summary>
		/// Select values from MarcDataField by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="marcDataFieldID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> MarcDataFieldSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int marcDataFieldID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("MarcDataFieldSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("MarcDataFieldID", SqlDbType.Int, null, false, marcDataFieldID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====
	
 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into MarcDataField.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="itemID"></param>
		/// <param name="tag"></param>
		/// <param name="indicator1"></param>
		/// <param name="indicator2"></param>
		/// <returns>Object of type MarcDataField.</returns>
		public MarcDataField MarcDataFieldInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int itemID,
			string tag,
			string indicator1,
			string indicator2)
		{
			return MarcDataFieldInsertAuto( sqlConnection, sqlTransaction, "IAAnalysis", itemID, tag, indicator1, indicator2 );
		}
		
		/// <summary>
		/// Insert values into MarcDataField.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="itemID"></param>
		/// <param name="tag"></param>
		/// <param name="indicator1"></param>
		/// <param name="indicator2"></param>
		/// <returns>Object of type MarcDataField.</returns>
		public MarcDataField MarcDataFieldInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int itemID,
			string tag,
			string indicator1,
			string indicator2)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("MarcDataFieldInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("MarcDataFieldID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemID),
					CustomSqlHelper.CreateInputParameter("Tag", SqlDbType.NChar, 3, false, tag),
					CustomSqlHelper.CreateInputParameter("Indicator1", SqlDbType.NChar, 1, false, indicator1),
					CustomSqlHelper.CreateInputParameter("Indicator2", SqlDbType.NChar, 1, false, indicator2), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<MarcDataField> helper = new CustomSqlHelper<MarcDataField>())
				{
					CustomGenericList<MarcDataField> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						MarcDataField o = list[0];
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
		/// Insert values into MarcDataField. Returns an object of type MarcDataField.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type MarcDataField.</param>
		/// <returns>Object of type MarcDataField.</returns>
		public MarcDataField MarcDataFieldInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			MarcDataField value)
		{
			return MarcDataFieldInsertAuto(sqlConnection, sqlTransaction, "IAAnalysis", value);
		}
		
		/// <summary>
		/// Insert values into MarcDataField. Returns an object of type MarcDataField.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type MarcDataField.</param>
		/// <returns>Object of type MarcDataField.</returns>
		public MarcDataField MarcDataFieldInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			MarcDataField value)
		{
			return MarcDataFieldInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.ItemID,
				value.Tag,
				value.Indicator1,
				value.Indicator2);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from MarcDataField by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="marcDataFieldID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool MarcDataFieldDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int marcDataFieldID)
		{
			return MarcDataFieldDeleteAuto( sqlConnection, sqlTransaction, "IAAnalysis", marcDataFieldID );
		}
		
		/// <summary>
		/// Delete values from MarcDataField by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="marcDataFieldID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool MarcDataFieldDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int marcDataFieldID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("MarcDataFieldDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("MarcDataFieldID", SqlDbType.Int, null, false, marcDataFieldID), 
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
		/// Update values in MarcDataField. Returns an object of type MarcDataField.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="marcDataFieldID"></param>
		/// <param name="itemID"></param>
		/// <param name="tag"></param>
		/// <param name="indicator1"></param>
		/// <param name="indicator2"></param>
		/// <returns>Object of type MarcDataField.</returns>
		public MarcDataField MarcDataFieldUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int marcDataFieldID,
			int itemID,
			string tag,
			string indicator1,
			string indicator2)
		{
			return MarcDataFieldUpdateAuto( sqlConnection, sqlTransaction, "IAAnalysis", marcDataFieldID, itemID, tag, indicator1, indicator2);
		}
		
		/// <summary>
		/// Update values in MarcDataField. Returns an object of type MarcDataField.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="marcDataFieldID"></param>
		/// <param name="itemID"></param>
		/// <param name="tag"></param>
		/// <param name="indicator1"></param>
		/// <param name="indicator2"></param>
		/// <returns>Object of type MarcDataField.</returns>
		public MarcDataField MarcDataFieldUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int marcDataFieldID,
			int itemID,
			string tag,
			string indicator1,
			string indicator2)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("MarcDataFieldUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("MarcDataFieldID", SqlDbType.Int, null, false, marcDataFieldID),
					CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemID),
					CustomSqlHelper.CreateInputParameter("Tag", SqlDbType.NChar, 3, false, tag),
					CustomSqlHelper.CreateInputParameter("Indicator1", SqlDbType.NChar, 1, false, indicator1),
					CustomSqlHelper.CreateInputParameter("Indicator2", SqlDbType.NChar, 1, false, indicator2), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<MarcDataField> helper = new CustomSqlHelper<MarcDataField>())
				{
					CustomGenericList<MarcDataField> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						MarcDataField o = list[0];
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
		/// Update values in MarcDataField. Returns an object of type MarcDataField.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type MarcDataField.</param>
		/// <returns>Object of type MarcDataField.</returns>
		public MarcDataField MarcDataFieldUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			MarcDataField value)
		{
			return MarcDataFieldUpdateAuto(sqlConnection, sqlTransaction, "IAAnalysis", value );
		}
		
		/// <summary>
		/// Update values in MarcDataField. Returns an object of type MarcDataField.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type MarcDataField.</param>
		/// <returns>Object of type MarcDataField.</returns>
		public MarcDataField MarcDataFieldUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			MarcDataField value)
		{
			return MarcDataFieldUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.MarcDataFieldID,
				value.ItemID,
				value.Tag,
				value.Indicator1,
				value.Indicator2);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage MarcDataField object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in MarcDataField.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type MarcDataField.</param>
		/// <returns>Object of type CustomDataAccessStatus<MarcDataField>.</returns>
		public CustomDataAccessStatus<MarcDataField> MarcDataFieldManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			MarcDataField value  )
		{
			return MarcDataFieldManageAuto( sqlConnection, sqlTransaction, "IAAnalysis", value  );
		}
		
		/// <summary>
		/// Manage MarcDataField object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in MarcDataField.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type MarcDataField.</param>
		/// <returns>Object of type CustomDataAccessStatus<MarcDataField>.</returns>
		public CustomDataAccessStatus<MarcDataField> MarcDataFieldManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			MarcDataField value  )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				
				
				MarcDataField returnValue = MarcDataFieldInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.ItemID,
						value.Tag,
						value.Indicator1,
						value.Indicator2);
				
				return new CustomDataAccessStatus<MarcDataField>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (MarcDataFieldDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.MarcDataFieldID))
				{
				return new CustomDataAccessStatus<MarcDataField>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<MarcDataField>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				
				MarcDataField returnValue = MarcDataFieldUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.MarcDataFieldID,
						value.ItemID,
						value.Tag,
						value.Indicator1,
						value.Indicator2);
					
				return new CustomDataAccessStatus<MarcDataField>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<MarcDataField>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}
// end of source generation
