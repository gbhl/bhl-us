
// Generated 1/5/2021 2:14:52 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class IAMarcDataFieldDAL is based upon dbo.IAMarcDataField.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHLImport.DAL
// {
// 		public partial class IAMarcDataFieldDAL
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
	partial class IAMarcDataFieldDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from dbo.IAMarcDataField by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="marcDataFieldID"></param>
		/// <returns>Object of type IAMarcDataField.</returns>
		public IAMarcDataField IAMarcDataFieldSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int marcDataFieldID)
		{
			return IAMarcDataFieldSelectAuto(	sqlConnection, sqlTransaction, "BHLImport",	marcDataFieldID );
		}
			
		/// <summary>
		/// Select values from dbo.IAMarcDataField by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="marcDataFieldID"></param>
		/// <returns>Object of type IAMarcDataField.</returns>
		public IAMarcDataField IAMarcDataFieldSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int marcDataFieldID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IAMarcDataFieldSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("MarcDataFieldID", SqlDbType.Int, null, false, marcDataFieldID)))
			{
				using (CustomSqlHelper<IAMarcDataField> helper = new CustomSqlHelper<IAMarcDataField>())
				{
					List<IAMarcDataField> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						IAMarcDataField o = list[0];
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
		/// Select values from dbo.IAMarcDataField by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="marcDataFieldID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> IAMarcDataFieldSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int marcDataFieldID)
		{
			return IAMarcDataFieldSelectAutoRaw( sqlConnection, sqlTransaction, "BHLImport", marcDataFieldID );
		}
		
		/// <summary>
		/// Select values from dbo.IAMarcDataField by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="marcDataFieldID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> IAMarcDataFieldSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int marcDataFieldID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IAMarcDataFieldSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("MarcDataFieldID", SqlDbType.Int, null, false, marcDataFieldID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====

 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into dbo.IAMarcDataField.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="marcID"></param>
		/// <param name="tag"></param>
		/// <param name="indicator1"></param>
		/// <param name="indicator2"></param>
		/// <returns>Object of type IAMarcDataField.</returns>
		public IAMarcDataField IAMarcDataFieldInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int marcID,
			string tag,
			string indicator1,
			string indicator2)
		{
			return IAMarcDataFieldInsertAuto( sqlConnection, sqlTransaction, "BHLImport", marcID, tag, indicator1, indicator2 );
		}
		
		/// <summary>
		/// Insert values into dbo.IAMarcDataField.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="marcID"></param>
		/// <param name="tag"></param>
		/// <param name="indicator1"></param>
		/// <param name="indicator2"></param>
		/// <returns>Object of type IAMarcDataField.</returns>
		public IAMarcDataField IAMarcDataFieldInsertAuto(
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
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IAMarcDataFieldInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("MarcDataFieldID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("MarcID", SqlDbType.Int, null, false, marcID),
					CustomSqlHelper.CreateInputParameter("Tag", SqlDbType.NChar, 3, false, tag),
					CustomSqlHelper.CreateInputParameter("Indicator1", SqlDbType.NChar, 1, false, indicator1),
					CustomSqlHelper.CreateInputParameter("Indicator2", SqlDbType.NChar, 1, false, indicator2), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<IAMarcDataField> helper = new CustomSqlHelper<IAMarcDataField>())
				{
					List<IAMarcDataField> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						IAMarcDataField o = list[0];
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
		/// Insert values into dbo.IAMarcDataField. Returns an object of type IAMarcDataField.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type IAMarcDataField.</param>
		/// <returns>Object of type IAMarcDataField.</returns>
		public IAMarcDataField IAMarcDataFieldInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			IAMarcDataField value)
		{
			return IAMarcDataFieldInsertAuto(sqlConnection, sqlTransaction, "BHLImport", value);
		}
		
		/// <summary>
		/// Insert values into dbo.IAMarcDataField. Returns an object of type IAMarcDataField.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type IAMarcDataField.</param>
		/// <returns>Object of type IAMarcDataField.</returns>
		public IAMarcDataField IAMarcDataFieldInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			IAMarcDataField value)
		{
			return IAMarcDataFieldInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.MarcID,
				value.Tag,
				value.Indicator1,
				value.Indicator2);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from dbo.IAMarcDataField by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="marcDataFieldID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool IAMarcDataFieldDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int marcDataFieldID)
		{
			return IAMarcDataFieldDeleteAuto( sqlConnection, sqlTransaction, "BHLImport", marcDataFieldID );
		}
		
		/// <summary>
		/// Delete values from dbo.IAMarcDataField by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="marcDataFieldID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool IAMarcDataFieldDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int marcDataFieldID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IAMarcDataFieldDeleteAuto", connection, transaction, 
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
		/// Update values in dbo.IAMarcDataField. Returns an object of type IAMarcDataField.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="marcDataFieldID"></param>
		/// <param name="marcID"></param>
		/// <param name="tag"></param>
		/// <param name="indicator1"></param>
		/// <param name="indicator2"></param>
		/// <returns>Object of type IAMarcDataField.</returns>
		public IAMarcDataField IAMarcDataFieldUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int marcDataFieldID,
			int marcID,
			string tag,
			string indicator1,
			string indicator2)
		{
			return IAMarcDataFieldUpdateAuto( sqlConnection, sqlTransaction, "BHLImport", marcDataFieldID, marcID, tag, indicator1, indicator2);
		}
		
		/// <summary>
		/// Update values in dbo.IAMarcDataField. Returns an object of type IAMarcDataField.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="marcDataFieldID"></param>
		/// <param name="marcID"></param>
		/// <param name="tag"></param>
		/// <param name="indicator1"></param>
		/// <param name="indicator2"></param>
		/// <returns>Object of type IAMarcDataField.</returns>
		public IAMarcDataField IAMarcDataFieldUpdateAuto(
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
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IAMarcDataFieldUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("MarcDataFieldID", SqlDbType.Int, null, false, marcDataFieldID),
					CustomSqlHelper.CreateInputParameter("MarcID", SqlDbType.Int, null, false, marcID),
					CustomSqlHelper.CreateInputParameter("Tag", SqlDbType.NChar, 3, false, tag),
					CustomSqlHelper.CreateInputParameter("Indicator1", SqlDbType.NChar, 1, false, indicator1),
					CustomSqlHelper.CreateInputParameter("Indicator2", SqlDbType.NChar, 1, false, indicator2), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<IAMarcDataField> helper = new CustomSqlHelper<IAMarcDataField>())
				{
					List<IAMarcDataField> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						IAMarcDataField o = list[0];
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
		/// Update values in dbo.IAMarcDataField. Returns an object of type IAMarcDataField.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type IAMarcDataField.</param>
		/// <returns>Object of type IAMarcDataField.</returns>
		public IAMarcDataField IAMarcDataFieldUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			IAMarcDataField value)
		{
			return IAMarcDataFieldUpdateAuto(sqlConnection, sqlTransaction, "BHLImport", value );
		}
		
		/// <summary>
		/// Update values in dbo.IAMarcDataField. Returns an object of type IAMarcDataField.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type IAMarcDataField.</param>
		/// <returns>Object of type IAMarcDataField.</returns>
		public IAMarcDataField IAMarcDataFieldUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			IAMarcDataField value)
		{
			return IAMarcDataFieldUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.MarcDataFieldID,
				value.MarcID,
				value.Tag,
				value.Indicator1,
				value.Indicator2);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage dbo.IAMarcDataField object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.IAMarcDataField.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type IAMarcDataField.</param>
		/// <returns>Object of type CustomDataAccessStatus<IAMarcDataField>.</returns>
		public CustomDataAccessStatus<IAMarcDataField> IAMarcDataFieldManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			IAMarcDataField value  )
		{
			return IAMarcDataFieldManageAuto( sqlConnection, sqlTransaction, "BHLImport", value  );
		}
		
		/// <summary>
		/// Manage dbo.IAMarcDataField object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.IAMarcDataField.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type IAMarcDataField.</param>
		/// <returns>Object of type CustomDataAccessStatus<IAMarcDataField>.</returns>
		public CustomDataAccessStatus<IAMarcDataField> IAMarcDataFieldManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			IAMarcDataField value  )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				
				
				IAMarcDataField returnValue = IAMarcDataFieldInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.MarcID,
						value.Tag,
						value.Indicator1,
						value.Indicator2);
				
				return new CustomDataAccessStatus<IAMarcDataField>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (IAMarcDataFieldDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.MarcDataFieldID))
				{
				return new CustomDataAccessStatus<IAMarcDataField>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<IAMarcDataField>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				
				IAMarcDataField returnValue = IAMarcDataFieldUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.MarcDataFieldID,
						value.MarcID,
						value.Tag,
						value.Indicator1,
						value.Indicator2);
					
				return new CustomDataAccessStatus<IAMarcDataField>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<IAMarcDataField>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}

