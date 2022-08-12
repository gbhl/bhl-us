
// Generated 1/5/2021 3:26:06 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class MarcDataFieldDAL is based upon dbo.MarcDataField.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DAL
// {
// 		public partial class MarcDataFieldDAL
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
	partial class MarcDataFieldDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from dbo.MarcDataField by primary key(s).
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
			return MarcDataFieldSelectAuto(	sqlConnection, sqlTransaction, "BHL",	marcDataFieldID );
		}
			
		/// <summary>
		/// Select values from dbo.MarcDataField by primary key(s).
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
					List<MarcDataField> list = helper.ExecuteReader(command);
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
		/// Select values from dbo.MarcDataField by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="marcDataFieldID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> MarcDataFieldSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int marcDataFieldID)
		{
			return MarcDataFieldSelectAutoRaw( sqlConnection, sqlTransaction, "BHL", marcDataFieldID );
		}
		
		/// <summary>
		/// Select values from dbo.MarcDataField by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="marcDataFieldID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> MarcDataFieldSelectAutoRaw(
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
		/// Insert values into dbo.MarcDataField.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="marcID"></param>
		/// <param name="tag"></param>
		/// <param name="indicator1"></param>
		/// <param name="indicator2"></param>
		/// <returns>Object of type MarcDataField.</returns>
		public MarcDataField MarcDataFieldInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int marcID,
			string tag,
			string indicator1,
			string indicator2)
		{
			return MarcDataFieldInsertAuto( sqlConnection, sqlTransaction, "BHL", marcID, tag, indicator1, indicator2 );
		}
		
		/// <summary>
		/// Insert values into dbo.MarcDataField.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="marcID"></param>
		/// <param name="tag"></param>
		/// <param name="indicator1"></param>
		/// <param name="indicator2"></param>
		/// <returns>Object of type MarcDataField.</returns>
		public MarcDataField MarcDataFieldInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int marcID,
			string tag,
			string indicator1,
			string indicator2)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("MarcDataFieldInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("MarcDataFieldID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("MarcID", SqlDbType.Int, null, false, marcID),
					CustomSqlHelper.CreateInputParameter("Tag", SqlDbType.NChar, 3, false, tag),
					CustomSqlHelper.CreateInputParameter("Indicator1", SqlDbType.NChar, 1, false, indicator1),
					CustomSqlHelper.CreateInputParameter("Indicator2", SqlDbType.NChar, 1, false, indicator2), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<MarcDataField> helper = new CustomSqlHelper<MarcDataField>())
				{
					List<MarcDataField> list = helper.ExecuteReader(command);
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
		/// Insert values into dbo.MarcDataField. Returns an object of type MarcDataField.
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
			return MarcDataFieldInsertAuto(sqlConnection, sqlTransaction, "BHL", value);
		}
		
		/// <summary>
		/// Insert values into dbo.MarcDataField. Returns an object of type MarcDataField.
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
				value.MarcID,
				value.Tag,
				value.Indicator1,
				value.Indicator2);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from dbo.MarcDataField by primary key(s).
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
			return MarcDataFieldDeleteAuto( sqlConnection, sqlTransaction, "BHL", marcDataFieldID );
		}
		
		/// <summary>
		/// Delete values from dbo.MarcDataField by primary key(s).
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
		/// Update values in dbo.MarcDataField. Returns an object of type MarcDataField.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="marcDataFieldID"></param>
		/// <param name="marcID"></param>
		/// <param name="tag"></param>
		/// <param name="indicator1"></param>
		/// <param name="indicator2"></param>
		/// <returns>Object of type MarcDataField.</returns>
		public MarcDataField MarcDataFieldUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int marcDataFieldID,
			int marcID,
			string tag,
			string indicator1,
			string indicator2)
		{
			return MarcDataFieldUpdateAuto( sqlConnection, sqlTransaction, "BHL", marcDataFieldID, marcID, tag, indicator1, indicator2);
		}
		
		/// <summary>
		/// Update values in dbo.MarcDataField. Returns an object of type MarcDataField.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="marcDataFieldID"></param>
		/// <param name="marcID"></param>
		/// <param name="tag"></param>
		/// <param name="indicator1"></param>
		/// <param name="indicator2"></param>
		/// <returns>Object of type MarcDataField.</returns>
		public MarcDataField MarcDataFieldUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int marcDataFieldID,
			int marcID,
			string tag,
			string indicator1,
			string indicator2)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("MarcDataFieldUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("MarcDataFieldID", SqlDbType.Int, null, false, marcDataFieldID),
					CustomSqlHelper.CreateInputParameter("MarcID", SqlDbType.Int, null, false, marcID),
					CustomSqlHelper.CreateInputParameter("Tag", SqlDbType.NChar, 3, false, tag),
					CustomSqlHelper.CreateInputParameter("Indicator1", SqlDbType.NChar, 1, false, indicator1),
					CustomSqlHelper.CreateInputParameter("Indicator2", SqlDbType.NChar, 1, false, indicator2), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<MarcDataField> helper = new CustomSqlHelper<MarcDataField>())
				{
					List<MarcDataField> list = helper.ExecuteReader(command);
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
		/// Update values in dbo.MarcDataField. Returns an object of type MarcDataField.
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
			return MarcDataFieldUpdateAuto(sqlConnection, sqlTransaction, "BHL", value );
		}
		
		/// <summary>
		/// Update values in dbo.MarcDataField. Returns an object of type MarcDataField.
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
				value.MarcID,
				value.Tag,
				value.Indicator1,
				value.Indicator2);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage dbo.MarcDataField object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.MarcDataField.
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
			return MarcDataFieldManageAuto( sqlConnection, sqlTransaction, "BHL", value  );
		}
		
		/// <summary>
		/// Manage dbo.MarcDataField object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.MarcDataField.
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
					value.MarcID,
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
						value.MarcID,
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

