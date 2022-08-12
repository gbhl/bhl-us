
// Generated 1/5/2021 3:25:27 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class InstitutionRoleDAL is based upon dbo.InstitutionRole.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DAL
// {
// 		public partial class InstitutionRoleDAL
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
	partial class InstitutionRoleDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from dbo.InstitutionRole by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="institutionRoleID"></param>
		/// <returns>Object of type InstitutionRole.</returns>
		public InstitutionRole InstitutionRoleSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int institutionRoleID)
		{
			return InstitutionRoleSelectAuto(	sqlConnection, sqlTransaction, "BHL",	institutionRoleID );
		}
			
		/// <summary>
		/// Select values from dbo.InstitutionRole by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="institutionRoleID"></param>
		/// <returns>Object of type InstitutionRole.</returns>
		public InstitutionRole InstitutionRoleSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int institutionRoleID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("InstitutionRoleSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("InstitutionRoleID", SqlDbType.Int, null, false, institutionRoleID)))
			{
				using (CustomSqlHelper<InstitutionRole> helper = new CustomSqlHelper<InstitutionRole>())
				{
					List<InstitutionRole> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						InstitutionRole o = list[0];
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
		/// Select values from dbo.InstitutionRole by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="institutionRoleID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> InstitutionRoleSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int institutionRoleID)
		{
			return InstitutionRoleSelectAutoRaw( sqlConnection, sqlTransaction, "BHL", institutionRoleID );
		}
		
		/// <summary>
		/// Select values from dbo.InstitutionRole by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="institutionRoleID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> InstitutionRoleSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int institutionRoleID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("InstitutionRoleSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("InstitutionRoleID", SqlDbType.Int, null, false, institutionRoleID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====

 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into dbo.InstitutionRole.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="institutionRoleName"></param>
		/// <param name="institutionRoleLabel"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type InstitutionRole.</returns>
		public InstitutionRole InstitutionRoleInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string institutionRoleName,
			string institutionRoleLabel,
			int creationUserID,
			int lastModifiedUserID)
		{
			return InstitutionRoleInsertAuto( sqlConnection, sqlTransaction, "BHL", institutionRoleName, institutionRoleLabel, creationUserID, lastModifiedUserID );
		}
		
		/// <summary>
		/// Insert values into dbo.InstitutionRole.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="institutionRoleName"></param>
		/// <param name="institutionRoleLabel"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type InstitutionRole.</returns>
		public InstitutionRole InstitutionRoleInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			string institutionRoleName,
			string institutionRoleLabel,
			int creationUserID,
			int lastModifiedUserID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("InstitutionRoleInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("InstitutionRoleID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("InstitutionRoleName", SqlDbType.NVarChar, 100, false, institutionRoleName),
					CustomSqlHelper.CreateInputParameter("InstitutionRoleLabel", SqlDbType.NVarChar, 100, false, institutionRoleLabel),
					CustomSqlHelper.CreateInputParameter("CreationUserID", SqlDbType.Int, null, false, creationUserID),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, false, lastModifiedUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<InstitutionRole> helper = new CustomSqlHelper<InstitutionRole>())
				{
					List<InstitutionRole> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						InstitutionRole o = list[0];
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
		/// Insert values into dbo.InstitutionRole. Returns an object of type InstitutionRole.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type InstitutionRole.</param>
		/// <returns>Object of type InstitutionRole.</returns>
		public InstitutionRole InstitutionRoleInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			InstitutionRole value)
		{
			return InstitutionRoleInsertAuto(sqlConnection, sqlTransaction, "BHL", value);
		}
		
		/// <summary>
		/// Insert values into dbo.InstitutionRole. Returns an object of type InstitutionRole.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type InstitutionRole.</param>
		/// <returns>Object of type InstitutionRole.</returns>
		public InstitutionRole InstitutionRoleInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			InstitutionRole value)
		{
			return InstitutionRoleInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.InstitutionRoleName,
				value.InstitutionRoleLabel,
				value.CreationUserID,
				value.LastModifiedUserID);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from dbo.InstitutionRole by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="institutionRoleID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool InstitutionRoleDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int institutionRoleID)
		{
			return InstitutionRoleDeleteAuto( sqlConnection, sqlTransaction, "BHL", institutionRoleID );
		}
		
		/// <summary>
		/// Delete values from dbo.InstitutionRole by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="institutionRoleID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool InstitutionRoleDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int institutionRoleID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("InstitutionRoleDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("InstitutionRoleID", SqlDbType.Int, null, false, institutionRoleID), 
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
		/// Update values in dbo.InstitutionRole. Returns an object of type InstitutionRole.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="institutionRoleID"></param>
		/// <param name="institutionRoleName"></param>
		/// <param name="institutionRoleLabel"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type InstitutionRole.</returns>
		public InstitutionRole InstitutionRoleUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int institutionRoleID,
			string institutionRoleName,
			string institutionRoleLabel,
			int lastModifiedUserID)
		{
			return InstitutionRoleUpdateAuto( sqlConnection, sqlTransaction, "BHL", institutionRoleID, institutionRoleName, institutionRoleLabel, lastModifiedUserID);
		}
		
		/// <summary>
		/// Update values in dbo.InstitutionRole. Returns an object of type InstitutionRole.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="institutionRoleID"></param>
		/// <param name="institutionRoleName"></param>
		/// <param name="institutionRoleLabel"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type InstitutionRole.</returns>
		public InstitutionRole InstitutionRoleUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int institutionRoleID,
			string institutionRoleName,
			string institutionRoleLabel,
			int lastModifiedUserID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("InstitutionRoleUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("InstitutionRoleID", SqlDbType.Int, null, false, institutionRoleID),
					CustomSqlHelper.CreateInputParameter("InstitutionRoleName", SqlDbType.NVarChar, 100, false, institutionRoleName),
					CustomSqlHelper.CreateInputParameter("InstitutionRoleLabel", SqlDbType.NVarChar, 100, false, institutionRoleLabel),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, false, lastModifiedUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<InstitutionRole> helper = new CustomSqlHelper<InstitutionRole>())
				{
					List<InstitutionRole> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						InstitutionRole o = list[0];
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
		/// Update values in dbo.InstitutionRole. Returns an object of type InstitutionRole.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type InstitutionRole.</param>
		/// <returns>Object of type InstitutionRole.</returns>
		public InstitutionRole InstitutionRoleUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			InstitutionRole value)
		{
			return InstitutionRoleUpdateAuto(sqlConnection, sqlTransaction, "BHL", value );
		}
		
		/// <summary>
		/// Update values in dbo.InstitutionRole. Returns an object of type InstitutionRole.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type InstitutionRole.</param>
		/// <returns>Object of type InstitutionRole.</returns>
		public InstitutionRole InstitutionRoleUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			InstitutionRole value)
		{
			return InstitutionRoleUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.InstitutionRoleID,
				value.InstitutionRoleName,
				value.InstitutionRoleLabel,
				value.LastModifiedUserID);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage dbo.InstitutionRole object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.InstitutionRole.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type InstitutionRole.</param>
		/// <returns>Object of type CustomDataAccessStatus<InstitutionRole>.</returns>
		public CustomDataAccessStatus<InstitutionRole> InstitutionRoleManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			InstitutionRole value , int userId )
		{
			return InstitutionRoleManageAuto( sqlConnection, sqlTransaction, "BHL", value , userId );
		}
		
		/// <summary>
		/// Manage dbo.InstitutionRole object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.InstitutionRole.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type InstitutionRole.</param>
		/// <returns>Object of type CustomDataAccessStatus<InstitutionRole>.</returns>
		public CustomDataAccessStatus<InstitutionRole> InstitutionRoleManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			InstitutionRole value , int userId )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				value.CreationUserID = userId;
				value.LastModifiedUserID = userId;
				InstitutionRole returnValue = InstitutionRoleInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.InstitutionRoleName,
						value.InstitutionRoleLabel,
						value.CreationUserID,
						value.LastModifiedUserID);
				
				return new CustomDataAccessStatus<InstitutionRole>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (InstitutionRoleDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.InstitutionRoleID))
				{
				return new CustomDataAccessStatus<InstitutionRole>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<InstitutionRole>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				value.LastModifiedUserID = userId;
				InstitutionRole returnValue = InstitutionRoleUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.InstitutionRoleID,
						value.InstitutionRoleName,
						value.InstitutionRoleLabel,
						value.LastModifiedUserID);
					
				return new CustomDataAccessStatus<InstitutionRole>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<InstitutionRole>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}

