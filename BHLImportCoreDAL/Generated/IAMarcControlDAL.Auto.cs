
// Generated 1/5/2021 2:14:45 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class IAMarcControlDAL is based upon dbo.IAMarcControl.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHLImport.DAL
// {
// 		public partial class IAMarcControlDAL
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
	partial class IAMarcControlDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from dbo.IAMarcControl by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="marcControlID"></param>
		/// <returns>Object of type IAMarcControl.</returns>
		public IAMarcControl IAMarcControlSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int marcControlID)
		{
			return IAMarcControlSelectAuto(	sqlConnection, sqlTransaction, "BHLImport",	marcControlID );
		}
			
		/// <summary>
		/// Select values from dbo.IAMarcControl by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="marcControlID"></param>
		/// <returns>Object of type IAMarcControl.</returns>
		public IAMarcControl IAMarcControlSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int marcControlID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IAMarcControlSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("MarcControlID", SqlDbType.Int, null, false, marcControlID)))
			{
				using (CustomSqlHelper<IAMarcControl> helper = new CustomSqlHelper<IAMarcControl>())
				{
					List<IAMarcControl> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						IAMarcControl o = list[0];
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
		/// Select values from dbo.IAMarcControl by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="marcControlID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> IAMarcControlSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int marcControlID)
		{
			return IAMarcControlSelectAutoRaw( sqlConnection, sqlTransaction, "BHLImport", marcControlID );
		}
		
		/// <summary>
		/// Select values from dbo.IAMarcControl by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="marcControlID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> IAMarcControlSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int marcControlID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IAMarcControlSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("MarcControlID", SqlDbType.Int, null, false, marcControlID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====

 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into dbo.IAMarcControl.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="marcID"></param>
		/// <param name="tag"></param>
		/// <param name="value"></param>
		/// <returns>Object of type IAMarcControl.</returns>
		public IAMarcControl IAMarcControlInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int marcID,
			string tag,
			string value)
		{
			return IAMarcControlInsertAuto( sqlConnection, sqlTransaction, "BHLImport", marcID, tag, value );
		}
		
		/// <summary>
		/// Insert values into dbo.IAMarcControl.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="marcID"></param>
		/// <param name="tag"></param>
		/// <param name="value"></param>
		/// <returns>Object of type IAMarcControl.</returns>
		public IAMarcControl IAMarcControlInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int marcID,
			string tag,
			string value)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IAMarcControlInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("MarcControlID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("MarcID", SqlDbType.Int, null, false, marcID),
					CustomSqlHelper.CreateInputParameter("Tag", SqlDbType.NChar, 3, false, tag),
					CustomSqlHelper.CreateInputParameter("Value", SqlDbType.NVarChar, 2000, false, value), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<IAMarcControl> helper = new CustomSqlHelper<IAMarcControl>())
				{
					List<IAMarcControl> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						IAMarcControl o = list[0];
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
		/// Insert values into dbo.IAMarcControl. Returns an object of type IAMarcControl.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type IAMarcControl.</param>
		/// <returns>Object of type IAMarcControl.</returns>
		public IAMarcControl IAMarcControlInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			IAMarcControl value)
		{
			return IAMarcControlInsertAuto(sqlConnection, sqlTransaction, "BHLImport", value);
		}
		
		/// <summary>
		/// Insert values into dbo.IAMarcControl. Returns an object of type IAMarcControl.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type IAMarcControl.</param>
		/// <returns>Object of type IAMarcControl.</returns>
		public IAMarcControl IAMarcControlInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			IAMarcControl value)
		{
			return IAMarcControlInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.MarcID,
				value.Tag,
				value.Value);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from dbo.IAMarcControl by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="marcControlID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool IAMarcControlDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int marcControlID)
		{
			return IAMarcControlDeleteAuto( sqlConnection, sqlTransaction, "BHLImport", marcControlID );
		}
		
		/// <summary>
		/// Delete values from dbo.IAMarcControl by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="marcControlID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool IAMarcControlDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int marcControlID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IAMarcControlDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("MarcControlID", SqlDbType.Int, null, false, marcControlID), 
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
		/// Update values in dbo.IAMarcControl. Returns an object of type IAMarcControl.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="marcControlID"></param>
		/// <param name="marcID"></param>
		/// <param name="tag"></param>
		/// <param name="value"></param>
		/// <returns>Object of type IAMarcControl.</returns>
		public IAMarcControl IAMarcControlUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int marcControlID,
			int marcID,
			string tag,
			string value)
		{
			return IAMarcControlUpdateAuto( sqlConnection, sqlTransaction, "BHLImport", marcControlID, marcID, tag, value);
		}
		
		/// <summary>
		/// Update values in dbo.IAMarcControl. Returns an object of type IAMarcControl.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="marcControlID"></param>
		/// <param name="marcID"></param>
		/// <param name="tag"></param>
		/// <param name="value"></param>
		/// <returns>Object of type IAMarcControl.</returns>
		public IAMarcControl IAMarcControlUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int marcControlID,
			int marcID,
			string tag,
			string value)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("IAMarcControlUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("MarcControlID", SqlDbType.Int, null, false, marcControlID),
					CustomSqlHelper.CreateInputParameter("MarcID", SqlDbType.Int, null, false, marcID),
					CustomSqlHelper.CreateInputParameter("Tag", SqlDbType.NChar, 3, false, tag),
					CustomSqlHelper.CreateInputParameter("Value", SqlDbType.NVarChar, 2000, false, value), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<IAMarcControl> helper = new CustomSqlHelper<IAMarcControl>())
				{
					List<IAMarcControl> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						IAMarcControl o = list[0];
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
		/// Update values in dbo.IAMarcControl. Returns an object of type IAMarcControl.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type IAMarcControl.</param>
		/// <returns>Object of type IAMarcControl.</returns>
		public IAMarcControl IAMarcControlUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			IAMarcControl value)
		{
			return IAMarcControlUpdateAuto(sqlConnection, sqlTransaction, "BHLImport", value );
		}
		
		/// <summary>
		/// Update values in dbo.IAMarcControl. Returns an object of type IAMarcControl.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type IAMarcControl.</param>
		/// <returns>Object of type IAMarcControl.</returns>
		public IAMarcControl IAMarcControlUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			IAMarcControl value)
		{
			return IAMarcControlUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.MarcControlID,
				value.MarcID,
				value.Tag,
				value.Value);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage dbo.IAMarcControl object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.IAMarcControl.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type IAMarcControl.</param>
		/// <returns>Object of type CustomDataAccessStatus<IAMarcControl>.</returns>
		public CustomDataAccessStatus<IAMarcControl> IAMarcControlManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			IAMarcControl value  )
		{
			return IAMarcControlManageAuto( sqlConnection, sqlTransaction, "BHLImport", value  );
		}
		
		/// <summary>
		/// Manage dbo.IAMarcControl object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.IAMarcControl.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type IAMarcControl.</param>
		/// <returns>Object of type CustomDataAccessStatus<IAMarcControl>.</returns>
		public CustomDataAccessStatus<IAMarcControl> IAMarcControlManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			IAMarcControl value  )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				
				
				IAMarcControl returnValue = IAMarcControlInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.MarcID,
						value.Tag,
						value.Value);
				
				return new CustomDataAccessStatus<IAMarcControl>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (IAMarcControlDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.MarcControlID))
				{
				return new CustomDataAccessStatus<IAMarcControl>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<IAMarcControl>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				
				IAMarcControl returnValue = IAMarcControlUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.MarcControlID,
						value.MarcID,
						value.Tag,
						value.Value);
					
				return new CustomDataAccessStatus<IAMarcControl>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<IAMarcControl>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}

