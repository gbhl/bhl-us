
// Generated 1/5/2021 2:14:38 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class IAMarcDAL is based upon dbo.IAMarc.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHLImport.DAL
// {
// 		public partial class IAMarcDAL
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
	partial class IAMarcDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from dbo.IAMarc by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="marcID"></param>
		/// <returns>Object of type IAMarc.</returns>
		public IAMarc IAMarcSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int marcID)
		{
			return IAMarcSelectAuto(	sqlConnection, sqlTransaction, "BHLImport",	marcID );
		}
			
		/// <summary>
		/// Select values from dbo.IAMarc by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="marcID"></param>
		/// <returns>Object of type IAMarc.</returns>
		public IAMarc IAMarcSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int marcID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IAMarcSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("MarcID", SqlDbType.Int, null, false, marcID)))
			{
				using (CustomSqlHelper<IAMarc> helper = new CustomSqlHelper<IAMarc>())
				{
					List<IAMarc> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						IAMarc o = list[0];
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
		/// Select values from dbo.IAMarc by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="marcID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> IAMarcSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int marcID)
		{
			return IAMarcSelectAutoRaw( sqlConnection, sqlTransaction, "BHLImport", marcID );
		}
		
		/// <summary>
		/// Select values from dbo.IAMarc by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="marcID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> IAMarcSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int marcID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IAMarcSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("MarcID", SqlDbType.Int, null, false, marcID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====

 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into dbo.IAMarc.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="itemID"></param>
		/// <param name="leader"></param>
		/// <returns>Object of type IAMarc.</returns>
		public IAMarc IAMarcInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int itemID,
			string leader)
		{
			return IAMarcInsertAuto( sqlConnection, sqlTransaction, "BHLImport", itemID, leader );
		}
		
		/// <summary>
		/// Insert values into dbo.IAMarc.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="itemID"></param>
		/// <param name="leader"></param>
		/// <returns>Object of type IAMarc.</returns>
		public IAMarc IAMarcInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int itemID,
			string leader)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IAMarcInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("MarcID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemID),
					CustomSqlHelper.CreateInputParameter("Leader", SqlDbType.VarChar, 200, false, leader), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<IAMarc> helper = new CustomSqlHelper<IAMarc>())
				{
					List<IAMarc> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						IAMarc o = list[0];
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
		/// Insert values into dbo.IAMarc. Returns an object of type IAMarc.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type IAMarc.</param>
		/// <returns>Object of type IAMarc.</returns>
		public IAMarc IAMarcInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			IAMarc value)
		{
			return IAMarcInsertAuto(sqlConnection, sqlTransaction, "BHLImport", value);
		}
		
		/// <summary>
		/// Insert values into dbo.IAMarc. Returns an object of type IAMarc.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type IAMarc.</param>
		/// <returns>Object of type IAMarc.</returns>
		public IAMarc IAMarcInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			IAMarc value)
		{
			return IAMarcInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.ItemID,
				value.Leader);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from dbo.IAMarc by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="marcID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool IAMarcDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int marcID)
		{
			return IAMarcDeleteAuto( sqlConnection, sqlTransaction, "BHLImport", marcID );
		}
		
		/// <summary>
		/// Delete values from dbo.IAMarc by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="marcID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool IAMarcDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int marcID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IAMarcDeleteAuto", connection, transaction, 
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
		/// Update values in dbo.IAMarc. Returns an object of type IAMarc.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="marcID"></param>
		/// <param name="itemID"></param>
		/// <param name="leader"></param>
		/// <returns>Object of type IAMarc.</returns>
		public IAMarc IAMarcUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int marcID,
			int itemID,
			string leader)
		{
			return IAMarcUpdateAuto( sqlConnection, sqlTransaction, "BHLImport", marcID, itemID, leader);
		}
		
		/// <summary>
		/// Update values in dbo.IAMarc. Returns an object of type IAMarc.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="marcID"></param>
		/// <param name="itemID"></param>
		/// <param name="leader"></param>
		/// <returns>Object of type IAMarc.</returns>
		public IAMarc IAMarcUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int marcID,
			int itemID,
			string leader)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IAMarcUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("MarcID", SqlDbType.Int, null, false, marcID),
					CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemID),
					CustomSqlHelper.CreateInputParameter("Leader", SqlDbType.VarChar, 200, false, leader), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<IAMarc> helper = new CustomSqlHelper<IAMarc>())
				{
					List<IAMarc> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						IAMarc o = list[0];
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
		/// Update values in dbo.IAMarc. Returns an object of type IAMarc.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type IAMarc.</param>
		/// <returns>Object of type IAMarc.</returns>
		public IAMarc IAMarcUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			IAMarc value)
		{
			return IAMarcUpdateAuto(sqlConnection, sqlTransaction, "BHLImport", value );
		}
		
		/// <summary>
		/// Update values in dbo.IAMarc. Returns an object of type IAMarc.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type IAMarc.</param>
		/// <returns>Object of type IAMarc.</returns>
		public IAMarc IAMarcUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			IAMarc value)
		{
			return IAMarcUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.MarcID,
				value.ItemID,
				value.Leader);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage dbo.IAMarc object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.IAMarc.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type IAMarc.</param>
		/// <returns>Object of type CustomDataAccessStatus<IAMarc>.</returns>
		public CustomDataAccessStatus<IAMarc> IAMarcManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			IAMarc value  )
		{
			return IAMarcManageAuto( sqlConnection, sqlTransaction, "BHLImport", value  );
		}
		
		/// <summary>
		/// Manage dbo.IAMarc object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.IAMarc.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type IAMarc.</param>
		/// <returns>Object of type CustomDataAccessStatus<IAMarc>.</returns>
		public CustomDataAccessStatus<IAMarc> IAMarcManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			IAMarc value  )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				
				
				IAMarc returnValue = IAMarcInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.ItemID,
						value.Leader);
				
				return new CustomDataAccessStatus<IAMarc>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (IAMarcDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.MarcID))
				{
				return new CustomDataAccessStatus<IAMarc>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<IAMarc>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				
				IAMarc returnValue = IAMarcUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.MarcID,
						value.ItemID,
						value.Leader);
					
				return new CustomDataAccessStatus<IAMarc>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<IAMarc>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}

