
// Generated 12/15/2010 3:05:49 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class AnnotatedPageCharacteristicDAL is based upon AnnotatedPageCharacteristic.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DAL
// {
// 		public partial class AnnotatedPageCharacteristicDAL
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
	partial class AnnotatedPageCharacteristicDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from AnnotatedPageCharacteristic by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="annotatedPageCharacteristicID"></param>
		/// <returns>Object of type AnnotatedPageCharacteristic.</returns>
		public AnnotatedPageCharacteristic AnnotatedPageCharacteristicSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int annotatedPageCharacteristicID)
		{
			return AnnotatedPageCharacteristicSelectAuto(	sqlConnection, sqlTransaction, "BHL",	annotatedPageCharacteristicID );
		}
			
		/// <summary>
		/// Select values from AnnotatedPageCharacteristic by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="annotatedPageCharacteristicID"></param>
		/// <returns>Object of type AnnotatedPageCharacteristic.</returns>
		public AnnotatedPageCharacteristic AnnotatedPageCharacteristicSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int annotatedPageCharacteristicID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("annotation.AnnotatedPageCharacteristicSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("AnnotatedPageCharacteristicID", SqlDbType.Int, null, false, annotatedPageCharacteristicID)))
			{
				using (CustomSqlHelper<AnnotatedPageCharacteristic> helper = new CustomSqlHelper<AnnotatedPageCharacteristic>())
				{
					List<AnnotatedPageCharacteristic> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						AnnotatedPageCharacteristic o = list[0];
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
		/// Select values from AnnotatedPageCharacteristic by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="annotatedPageCharacteristicID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> AnnotatedPageCharacteristicSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int annotatedPageCharacteristicID)
		{
			return AnnotatedPageCharacteristicSelectAutoRaw( sqlConnection, sqlTransaction, "BHL", annotatedPageCharacteristicID );
		}
		
		/// <summary>
		/// Select values from AnnotatedPageCharacteristic by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="annotatedPageCharacteristicID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> AnnotatedPageCharacteristicSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int annotatedPageCharacteristicID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("annotation.AnnotatedPageCharacteristicSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("AnnotatedPageCharacteristicID", SqlDbType.Int, null, false, annotatedPageCharacteristicID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====
	
 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into AnnotatedPageCharacteristic.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="annotatedPageID"></param>
		/// <param name="characteristicDetail"></param>
		/// <param name="characteristicDetailClean"></param>
		/// <param name="characteristicDetailDisplay"></param>
		/// <returns>Object of type AnnotatedPageCharacteristic.</returns>
		public AnnotatedPageCharacteristic AnnotatedPageCharacteristicInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int? annotatedPageID,
			string characteristicDetail,
			string characteristicDetailClean,
			string characteristicDetailDisplay)
		{
			return AnnotatedPageCharacteristicInsertAuto( sqlConnection, sqlTransaction, "BHL", annotatedPageID, characteristicDetail, characteristicDetailClean, characteristicDetailDisplay );
		}
		
		/// <summary>
		/// Insert values into AnnotatedPageCharacteristic.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="annotatedPageID"></param>
		/// <param name="characteristicDetail"></param>
		/// <param name="characteristicDetailClean"></param>
		/// <param name="characteristicDetailDisplay"></param>
		/// <returns>Object of type AnnotatedPageCharacteristic.</returns>
		public AnnotatedPageCharacteristic AnnotatedPageCharacteristicInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int? annotatedPageID,
			string characteristicDetail,
			string characteristicDetailClean,
			string characteristicDetailDisplay)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("annotation.AnnotatedPageCharacteristicInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("AnnotatedPageCharacteristicID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("AnnotatedPageID", SqlDbType.Int, null, true, annotatedPageID),
					CustomSqlHelper.CreateInputParameter("CharacteristicDetail", SqlDbType.NVarChar, 1073741823, false, characteristicDetail),
					CustomSqlHelper.CreateInputParameter("CharacteristicDetailClean", SqlDbType.NVarChar, 1073741823, false, characteristicDetailClean),
					CustomSqlHelper.CreateInputParameter("CharacteristicDetailDisplay", SqlDbType.NVarChar, 1073741823, false, characteristicDetailDisplay), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<AnnotatedPageCharacteristic> helper = new CustomSqlHelper<AnnotatedPageCharacteristic>())
				{
					List<AnnotatedPageCharacteristic> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						AnnotatedPageCharacteristic o = list[0];
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
		/// Insert values into AnnotatedPageCharacteristic. Returns an object of type AnnotatedPageCharacteristic.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type AnnotatedPageCharacteristic.</param>
		/// <returns>Object of type AnnotatedPageCharacteristic.</returns>
		public AnnotatedPageCharacteristic AnnotatedPageCharacteristicInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			AnnotatedPageCharacteristic value)
		{
			return AnnotatedPageCharacteristicInsertAuto(sqlConnection, sqlTransaction, "BHL", value);
		}
		
		/// <summary>
		/// Insert values into AnnotatedPageCharacteristic. Returns an object of type AnnotatedPageCharacteristic.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type AnnotatedPageCharacteristic.</param>
		/// <returns>Object of type AnnotatedPageCharacteristic.</returns>
		public AnnotatedPageCharacteristic AnnotatedPageCharacteristicInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			AnnotatedPageCharacteristic value)
		{
			return AnnotatedPageCharacteristicInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.AnnotatedPageID,
				value.CharacteristicDetail,
				value.CharacteristicDetailClean,
				value.CharacteristicDetailDisplay);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from AnnotatedPageCharacteristic by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="annotatedPageCharacteristicID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool AnnotatedPageCharacteristicDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int annotatedPageCharacteristicID)
		{
			return AnnotatedPageCharacteristicDeleteAuto( sqlConnection, sqlTransaction, "BHL", annotatedPageCharacteristicID );
		}
		
		/// <summary>
		/// Delete values from AnnotatedPageCharacteristic by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="annotatedPageCharacteristicID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool AnnotatedPageCharacteristicDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int annotatedPageCharacteristicID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("annotation.AnnotatedPageCharacteristicDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("AnnotatedPageCharacteristicID", SqlDbType.Int, null, false, annotatedPageCharacteristicID), 
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
		/// Update values in AnnotatedPageCharacteristic. Returns an object of type AnnotatedPageCharacteristic.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="annotatedPageCharacteristicID"></param>
		/// <param name="annotatedPageID"></param>
		/// <param name="characteristicDetail"></param>
		/// <param name="characteristicDetailClean"></param>
		/// <param name="characteristicDetailDisplay"></param>
		/// <returns>Object of type AnnotatedPageCharacteristic.</returns>
		public AnnotatedPageCharacteristic AnnotatedPageCharacteristicUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int annotatedPageCharacteristicID,
			int? annotatedPageID,
			string characteristicDetail,
			string characteristicDetailClean,
			string characteristicDetailDisplay)
		{
			return AnnotatedPageCharacteristicUpdateAuto( sqlConnection, sqlTransaction, "BHL", annotatedPageCharacteristicID, annotatedPageID, characteristicDetail, characteristicDetailClean, characteristicDetailDisplay);
		}
		
		/// <summary>
		/// Update values in AnnotatedPageCharacteristic. Returns an object of type AnnotatedPageCharacteristic.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="annotatedPageCharacteristicID"></param>
		/// <param name="annotatedPageID"></param>
		/// <param name="characteristicDetail"></param>
		/// <param name="characteristicDetailClean"></param>
		/// <param name="characteristicDetailDisplay"></param>
		/// <returns>Object of type AnnotatedPageCharacteristic.</returns>
		public AnnotatedPageCharacteristic AnnotatedPageCharacteristicUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int annotatedPageCharacteristicID,
			int? annotatedPageID,
			string characteristicDetail,
			string characteristicDetailClean,
			string characteristicDetailDisplay)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("annotation.AnnotatedPageCharacteristicUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("AnnotatedPageCharacteristicID", SqlDbType.Int, null, false, annotatedPageCharacteristicID),
					CustomSqlHelper.CreateInputParameter("AnnotatedPageID", SqlDbType.Int, null, true, annotatedPageID),
					CustomSqlHelper.CreateInputParameter("CharacteristicDetail", SqlDbType.NVarChar, 1073741823, false, characteristicDetail),
					CustomSqlHelper.CreateInputParameter("CharacteristicDetailClean", SqlDbType.NVarChar, 1073741823, false, characteristicDetailClean),
					CustomSqlHelper.CreateInputParameter("CharacteristicDetailDisplay", SqlDbType.NVarChar, 1073741823, false, characteristicDetailDisplay), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<AnnotatedPageCharacteristic> helper = new CustomSqlHelper<AnnotatedPageCharacteristic>())
				{
					List<AnnotatedPageCharacteristic> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						AnnotatedPageCharacteristic o = list[0];
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
		/// Update values in AnnotatedPageCharacteristic. Returns an object of type AnnotatedPageCharacteristic.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type AnnotatedPageCharacteristic.</param>
		/// <returns>Object of type AnnotatedPageCharacteristic.</returns>
		public AnnotatedPageCharacteristic AnnotatedPageCharacteristicUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			AnnotatedPageCharacteristic value)
		{
			return AnnotatedPageCharacteristicUpdateAuto(sqlConnection, sqlTransaction, "BHL", value );
		}
		
		/// <summary>
		/// Update values in AnnotatedPageCharacteristic. Returns an object of type AnnotatedPageCharacteristic.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type AnnotatedPageCharacteristic.</param>
		/// <returns>Object of type AnnotatedPageCharacteristic.</returns>
		public AnnotatedPageCharacteristic AnnotatedPageCharacteristicUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			AnnotatedPageCharacteristic value)
		{
			return AnnotatedPageCharacteristicUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.AnnotatedPageCharacteristicID,
				value.AnnotatedPageID,
				value.CharacteristicDetail,
				value.CharacteristicDetailClean,
				value.CharacteristicDetailDisplay);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage AnnotatedPageCharacteristic object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in AnnotatedPageCharacteristic.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type AnnotatedPageCharacteristic.</param>
		/// <returns>Object of type CustomDataAccessStatus<AnnotatedPageCharacteristic>.</returns>
		public CustomDataAccessStatus<AnnotatedPageCharacteristic> AnnotatedPageCharacteristicManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			AnnotatedPageCharacteristic value  )
		{
			return AnnotatedPageCharacteristicManageAuto( sqlConnection, sqlTransaction, "BHL", value  );
		}
		
		/// <summary>
		/// Manage AnnotatedPageCharacteristic object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in AnnotatedPageCharacteristic.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type AnnotatedPageCharacteristic.</param>
		/// <returns>Object of type CustomDataAccessStatus<AnnotatedPageCharacteristic>.</returns>
		public CustomDataAccessStatus<AnnotatedPageCharacteristic> AnnotatedPageCharacteristicManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			AnnotatedPageCharacteristic value  )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				
				
				AnnotatedPageCharacteristic returnValue = AnnotatedPageCharacteristicInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.AnnotatedPageID,
						value.CharacteristicDetail,
						value.CharacteristicDetailClean,
						value.CharacteristicDetailDisplay);
				
				return new CustomDataAccessStatus<AnnotatedPageCharacteristic>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (AnnotatedPageCharacteristicDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.AnnotatedPageCharacteristicID))
				{
				return new CustomDataAccessStatus<AnnotatedPageCharacteristic>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<AnnotatedPageCharacteristic>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				
				AnnotatedPageCharacteristic returnValue = AnnotatedPageCharacteristicUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.AnnotatedPageCharacteristicID,
						value.AnnotatedPageID,
						value.CharacteristicDetail,
						value.CharacteristicDetailClean,
						value.CharacteristicDetailDisplay);
					
				return new CustomDataAccessStatus<AnnotatedPageCharacteristic>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<AnnotatedPageCharacteristic>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}
// end of source generation
