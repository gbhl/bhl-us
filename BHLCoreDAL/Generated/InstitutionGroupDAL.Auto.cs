
// Generated 1/5/2021 3:25:22 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class InstitutionGroupDAL is based upon dbo.InstitutionGroup.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DAL
// {
// 		public partial class InstitutionGroupDAL
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
	partial class InstitutionGroupDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from dbo.InstitutionGroup by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="institutionGroupID"></param>
		/// <returns>Object of type InstitutionGroup.</returns>
		public InstitutionGroup InstitutionGroupSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int institutionGroupID)
		{
			return InstitutionGroupSelectAuto(	sqlConnection, sqlTransaction, "BHL",	institutionGroupID );
		}
			
		/// <summary>
		/// Select values from dbo.InstitutionGroup by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="institutionGroupID"></param>
		/// <returns>Object of type InstitutionGroup.</returns>
		public InstitutionGroup InstitutionGroupSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int institutionGroupID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("InstitutionGroupSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("InstitutionGroupID", SqlDbType.Int, null, false, institutionGroupID)))
			{
				using (CustomSqlHelper<InstitutionGroup> helper = new CustomSqlHelper<InstitutionGroup>())
				{
					List<InstitutionGroup> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						InstitutionGroup o = list[0];
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
		/// Select values from dbo.InstitutionGroup by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="institutionGroupID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> InstitutionGroupSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int institutionGroupID)
		{
			return InstitutionGroupSelectAutoRaw( sqlConnection, sqlTransaction, "BHL", institutionGroupID );
		}
		
		/// <summary>
		/// Select values from dbo.InstitutionGroup by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="institutionGroupID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> InstitutionGroupSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int institutionGroupID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("InstitutionGroupSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("InstitutionGroupID", SqlDbType.Int, null, false, institutionGroupID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====

 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into dbo.InstitutionGroup.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="institutionGroupName"></param>
		/// <param name="institutionGroupDescription"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type InstitutionGroup.</returns>
		public InstitutionGroup InstitutionGroupInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string institutionGroupName,
			string institutionGroupDescription,
			int? creationUserID,
			int? lastModifiedUserID)
		{
			return InstitutionGroupInsertAuto( sqlConnection, sqlTransaction, "BHL", institutionGroupName, institutionGroupDescription, creationUserID, lastModifiedUserID );
		}
		
		/// <summary>
		/// Insert values into dbo.InstitutionGroup.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="institutionGroupName"></param>
		/// <param name="institutionGroupDescription"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type InstitutionGroup.</returns>
		public InstitutionGroup InstitutionGroupInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			string institutionGroupName,
			string institutionGroupDescription,
			int? creationUserID,
			int? lastModifiedUserID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("InstitutionGroupInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("InstitutionGroupID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("InstitutionGroupName", SqlDbType.NVarChar, 300, false, institutionGroupName),
					CustomSqlHelper.CreateInputParameter("InstitutionGroupDescription", SqlDbType.NVarChar, 1073741823, false, institutionGroupDescription),
					CustomSqlHelper.CreateInputParameter("CreationUserID", SqlDbType.Int, null, true, creationUserID),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, true, lastModifiedUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<InstitutionGroup> helper = new CustomSqlHelper<InstitutionGroup>())
				{
					List<InstitutionGroup> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						InstitutionGroup o = list[0];
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
		/// Insert values into dbo.InstitutionGroup. Returns an object of type InstitutionGroup.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type InstitutionGroup.</param>
		/// <returns>Object of type InstitutionGroup.</returns>
		public InstitutionGroup InstitutionGroupInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			InstitutionGroup value)
		{
			return InstitutionGroupInsertAuto(sqlConnection, sqlTransaction, "BHL", value);
		}
		
		/// <summary>
		/// Insert values into dbo.InstitutionGroup. Returns an object of type InstitutionGroup.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type InstitutionGroup.</param>
		/// <returns>Object of type InstitutionGroup.</returns>
		public InstitutionGroup InstitutionGroupInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			InstitutionGroup value)
		{
			return InstitutionGroupInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.InstitutionGroupName,
				value.InstitutionGroupDescription,
				value.CreationUserID,
				value.LastModifiedUserID);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from dbo.InstitutionGroup by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="institutionGroupID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool InstitutionGroupDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int institutionGroupID)
		{
			return InstitutionGroupDeleteAuto( sqlConnection, sqlTransaction, "BHL", institutionGroupID );
		}
		
		/// <summary>
		/// Delete values from dbo.InstitutionGroup by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="institutionGroupID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool InstitutionGroupDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int institutionGroupID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("InstitutionGroupDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("InstitutionGroupID", SqlDbType.Int, null, false, institutionGroupID), 
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
		/// Update values in dbo.InstitutionGroup. Returns an object of type InstitutionGroup.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="institutionGroupID"></param>
		/// <param name="institutionGroupName"></param>
		/// <param name="institutionGroupDescription"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type InstitutionGroup.</returns>
		public InstitutionGroup InstitutionGroupUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int institutionGroupID,
			string institutionGroupName,
			string institutionGroupDescription,
			int? lastModifiedUserID)
		{
			return InstitutionGroupUpdateAuto( sqlConnection, sqlTransaction, "BHL", institutionGroupID, institutionGroupName, institutionGroupDescription, lastModifiedUserID);
		}
		
		/// <summary>
		/// Update values in dbo.InstitutionGroup. Returns an object of type InstitutionGroup.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="institutionGroupID"></param>
		/// <param name="institutionGroupName"></param>
		/// <param name="institutionGroupDescription"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type InstitutionGroup.</returns>
		public InstitutionGroup InstitutionGroupUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int institutionGroupID,
			string institutionGroupName,
			string institutionGroupDescription,
			int? lastModifiedUserID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("InstitutionGroupUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("InstitutionGroupID", SqlDbType.Int, null, false, institutionGroupID),
					CustomSqlHelper.CreateInputParameter("InstitutionGroupName", SqlDbType.NVarChar, 300, false, institutionGroupName),
					CustomSqlHelper.CreateInputParameter("InstitutionGroupDescription", SqlDbType.NVarChar, 1073741823, false, institutionGroupDescription),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, true, lastModifiedUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<InstitutionGroup> helper = new CustomSqlHelper<InstitutionGroup>())
				{
					List<InstitutionGroup> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						InstitutionGroup o = list[0];
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
		/// Update values in dbo.InstitutionGroup. Returns an object of type InstitutionGroup.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type InstitutionGroup.</param>
		/// <returns>Object of type InstitutionGroup.</returns>
		public InstitutionGroup InstitutionGroupUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			InstitutionGroup value)
		{
			return InstitutionGroupUpdateAuto(sqlConnection, sqlTransaction, "BHL", value );
		}
		
		/// <summary>
		/// Update values in dbo.InstitutionGroup. Returns an object of type InstitutionGroup.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type InstitutionGroup.</param>
		/// <returns>Object of type InstitutionGroup.</returns>
		public InstitutionGroup InstitutionGroupUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			InstitutionGroup value)
		{
			return InstitutionGroupUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.InstitutionGroupID,
				value.InstitutionGroupName,
				value.InstitutionGroupDescription,
				value.LastModifiedUserID);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage dbo.InstitutionGroup object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.InstitutionGroup.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type InstitutionGroup.</param>
		/// <returns>Object of type CustomDataAccessStatus<InstitutionGroup>.</returns>
		public CustomDataAccessStatus<InstitutionGroup> InstitutionGroupManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			InstitutionGroup value , int userId )
		{
			return InstitutionGroupManageAuto( sqlConnection, sqlTransaction, "BHL", value , userId );
		}
		
		/// <summary>
		/// Manage dbo.InstitutionGroup object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.InstitutionGroup.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type InstitutionGroup.</param>
		/// <returns>Object of type CustomDataAccessStatus<InstitutionGroup>.</returns>
		public CustomDataAccessStatus<InstitutionGroup> InstitutionGroupManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			InstitutionGroup value , int userId )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				value.CreationUserID = userId;
				value.LastModifiedUserID = userId;
				InstitutionGroup returnValue = InstitutionGroupInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.InstitutionGroupName,
						value.InstitutionGroupDescription,
						value.CreationUserID,
						value.LastModifiedUserID);
				
				return new CustomDataAccessStatus<InstitutionGroup>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (InstitutionGroupDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.InstitutionGroupID))
				{
				return new CustomDataAccessStatus<InstitutionGroup>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<InstitutionGroup>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				value.LastModifiedUserID = userId;
				InstitutionGroup returnValue = InstitutionGroupUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.InstitutionGroupID,
						value.InstitutionGroupName,
						value.InstitutionGroupDescription,
						value.LastModifiedUserID);
					
				return new CustomDataAccessStatus<InstitutionGroup>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<InstitutionGroup>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}

