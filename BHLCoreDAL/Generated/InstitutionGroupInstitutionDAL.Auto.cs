
// Generated 3/31/2020 12:15:15 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class InstitutionGroupInstitutionDAL is based upon dbo.InstitutionGroupInstitution.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DAL
// {
// 		public partial class InstitutionGroupInstitutionDAL
//		{
//		}
// }

#endregion How To Implement

#region using

using System;
using System.Data;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHL.DataObjects;

#endregion using

namespace MOBOT.BHL.DAL
{
	partial class InstitutionGroupInstitutionDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from dbo.InstitutionGroupInstitution by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="institutionGroupInstitutionID"></param>
		/// <returns>Object of type InstitutionGroupInstitution.</returns>
		public InstitutionGroupInstitution InstitutionGroupInstitutionSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int institutionGroupInstitutionID)
		{
			return InstitutionGroupInstitutionSelectAuto(	sqlConnection, sqlTransaction, "BHL",	institutionGroupInstitutionID );
		}
			
		/// <summary>
		/// Select values from dbo.InstitutionGroupInstitution by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="institutionGroupInstitutionID"></param>
		/// <returns>Object of type InstitutionGroupInstitution.</returns>
		public InstitutionGroupInstitution InstitutionGroupInstitutionSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int institutionGroupInstitutionID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("InstitutionGroupInstitutionSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("InstitutionGroupInstitutionID", SqlDbType.Int, null, false, institutionGroupInstitutionID)))
			{
				using (CustomSqlHelper<InstitutionGroupInstitution> helper = new CustomSqlHelper<InstitutionGroupInstitution>())
				{
					CustomGenericList<InstitutionGroupInstitution> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						InstitutionGroupInstitution o = list[0];
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
		/// Select values from dbo.InstitutionGroupInstitution by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="institutionGroupInstitutionID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> InstitutionGroupInstitutionSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int institutionGroupInstitutionID)
		{
			return InstitutionGroupInstitutionSelectAutoRaw( sqlConnection, sqlTransaction, "BHL", institutionGroupInstitutionID );
		}
		
		/// <summary>
		/// Select values from dbo.InstitutionGroupInstitution by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="institutionGroupInstitutionID"></param>
		/// <returns>CustomGenericList&lt;CustomDataRow&gt;</returns>
		public CustomGenericList<CustomDataRow> InstitutionGroupInstitutionSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int institutionGroupInstitutionID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("InstitutionGroupInstitutionSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("InstitutionGroupInstitutionID", SqlDbType.Int, null, false, institutionGroupInstitutionID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====

 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into dbo.InstitutionGroupInstitution.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="institutionGroupID"></param>
		/// <param name="institutionCode"></param>
		/// <param name="creationUserID"></param>
		/// <returns>Object of type InstitutionGroupInstitution.</returns>
		public InstitutionGroupInstitution InstitutionGroupInstitutionInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int institutionGroupID,
			string institutionCode,
			int? creationUserID)
		{
			return InstitutionGroupInstitutionInsertAuto( sqlConnection, sqlTransaction, "BHL", institutionGroupID, institutionCode, creationUserID );
		}
		
		/// <summary>
		/// Insert values into dbo.InstitutionGroupInstitution.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="institutionGroupID"></param>
		/// <param name="institutionCode"></param>
		/// <param name="creationUserID"></param>
		/// <returns>Object of type InstitutionGroupInstitution.</returns>
		public InstitutionGroupInstitution InstitutionGroupInstitutionInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int institutionGroupID,
			string institutionCode,
			int? creationUserID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("InstitutionGroupInstitutionInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("InstitutionGroupInstitutionID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("InstitutionGroupID", SqlDbType.Int, null, false, institutionGroupID),
					CustomSqlHelper.CreateInputParameter("InstitutionCode", SqlDbType.NVarChar, 10, false, institutionCode),
					CustomSqlHelper.CreateInputParameter("CreationUserID", SqlDbType.Int, null, true, creationUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<InstitutionGroupInstitution> helper = new CustomSqlHelper<InstitutionGroupInstitution>())
				{
					CustomGenericList<InstitutionGroupInstitution> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						InstitutionGroupInstitution o = list[0];
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
		/// Insert values into dbo.InstitutionGroupInstitution. Returns an object of type InstitutionGroupInstitution.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type InstitutionGroupInstitution.</param>
		/// <returns>Object of type InstitutionGroupInstitution.</returns>
		public InstitutionGroupInstitution InstitutionGroupInstitutionInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			InstitutionGroupInstitution value)
		{
			return InstitutionGroupInstitutionInsertAuto(sqlConnection, sqlTransaction, "BHL", value);
		}
		
		/// <summary>
		/// Insert values into dbo.InstitutionGroupInstitution. Returns an object of type InstitutionGroupInstitution.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type InstitutionGroupInstitution.</param>
		/// <returns>Object of type InstitutionGroupInstitution.</returns>
		public InstitutionGroupInstitution InstitutionGroupInstitutionInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			InstitutionGroupInstitution value)
		{
			return InstitutionGroupInstitutionInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.InstitutionGroupID,
				value.InstitutionCode,
				value.CreationUserID);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from dbo.InstitutionGroupInstitution by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="institutionGroupInstitutionID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool InstitutionGroupInstitutionDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int institutionGroupInstitutionID)
		{
			return InstitutionGroupInstitutionDeleteAuto( sqlConnection, sqlTransaction, "BHL", institutionGroupInstitutionID );
		}
		
		/// <summary>
		/// Delete values from dbo.InstitutionGroupInstitution by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="institutionGroupInstitutionID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool InstitutionGroupInstitutionDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int institutionGroupInstitutionID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("InstitutionGroupInstitutionDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("InstitutionGroupInstitutionID", SqlDbType.Int, null, false, institutionGroupInstitutionID), 
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
		/// Update values in dbo.InstitutionGroupInstitution. Returns an object of type InstitutionGroupInstitution.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="institutionGroupInstitutionID"></param>
		/// <param name="institutionGroupID"></param>
		/// <param name="institutionCode"></param>
		/// <returns>Object of type InstitutionGroupInstitution.</returns>
		public InstitutionGroupInstitution InstitutionGroupInstitutionUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int institutionGroupInstitutionID,
			int institutionGroupID,
			string institutionCode)
		{
			return InstitutionGroupInstitutionUpdateAuto( sqlConnection, sqlTransaction, "BHL", institutionGroupInstitutionID, institutionGroupID, institutionCode);
		}
		
		/// <summary>
		/// Update values in dbo.InstitutionGroupInstitution. Returns an object of type InstitutionGroupInstitution.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="institutionGroupInstitutionID"></param>
		/// <param name="institutionGroupID"></param>
		/// <param name="institutionCode"></param>
		/// <returns>Object of type InstitutionGroupInstitution.</returns>
		public InstitutionGroupInstitution InstitutionGroupInstitutionUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int institutionGroupInstitutionID,
			int institutionGroupID,
			string institutionCode)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("InstitutionGroupInstitutionUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("InstitutionGroupInstitutionID", SqlDbType.Int, null, false, institutionGroupInstitutionID),
					CustomSqlHelper.CreateInputParameter("InstitutionGroupID", SqlDbType.Int, null, false, institutionGroupID),
					CustomSqlHelper.CreateInputParameter("InstitutionCode", SqlDbType.NVarChar, 10, false, institutionCode), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<InstitutionGroupInstitution> helper = new CustomSqlHelper<InstitutionGroupInstitution>())
				{
					CustomGenericList<InstitutionGroupInstitution> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						InstitutionGroupInstitution o = list[0];
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
		/// Update values in dbo.InstitutionGroupInstitution. Returns an object of type InstitutionGroupInstitution.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type InstitutionGroupInstitution.</param>
		/// <returns>Object of type InstitutionGroupInstitution.</returns>
		public InstitutionGroupInstitution InstitutionGroupInstitutionUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			InstitutionGroupInstitution value)
		{
			return InstitutionGroupInstitutionUpdateAuto(sqlConnection, sqlTransaction, "BHL", value );
		}
		
		/// <summary>
		/// Update values in dbo.InstitutionGroupInstitution. Returns an object of type InstitutionGroupInstitution.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type InstitutionGroupInstitution.</param>
		/// <returns>Object of type InstitutionGroupInstitution.</returns>
		public InstitutionGroupInstitution InstitutionGroupInstitutionUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			InstitutionGroupInstitution value)
		{
			return InstitutionGroupInstitutionUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.InstitutionGroupInstitutionID,
				value.InstitutionGroupID,
				value.InstitutionCode);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage dbo.InstitutionGroupInstitution object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.InstitutionGroupInstitution.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type InstitutionGroupInstitution.</param>
		/// <returns>Object of type CustomDataAccessStatus<InstitutionGroupInstitution>.</returns>
		public CustomDataAccessStatus<InstitutionGroupInstitution> InstitutionGroupInstitutionManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			InstitutionGroupInstitution value , int userId )
		{
			return InstitutionGroupInstitutionManageAuto( sqlConnection, sqlTransaction, "BHL", value , userId );
		}
		
		/// <summary>
		/// Manage dbo.InstitutionGroupInstitution object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.InstitutionGroupInstitution.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type InstitutionGroupInstitution.</param>
		/// <returns>Object of type CustomDataAccessStatus<InstitutionGroupInstitution>.</returns>
		public CustomDataAccessStatus<InstitutionGroupInstitution> InstitutionGroupInstitutionManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			InstitutionGroupInstitution value , int userId )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				value.CreationUserID = userId;
				
				InstitutionGroupInstitution returnValue = InstitutionGroupInstitutionInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.InstitutionGroupID,
						value.InstitutionCode,
						value.CreationUserID);
				
				return new CustomDataAccessStatus<InstitutionGroupInstitution>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (InstitutionGroupInstitutionDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.InstitutionGroupInstitutionID))
				{
				return new CustomDataAccessStatus<InstitutionGroupInstitution>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<InstitutionGroupInstitution>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				
				InstitutionGroupInstitution returnValue = InstitutionGroupInstitutionUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.InstitutionGroupInstitutionID,
						value.InstitutionGroupID,
						value.InstitutionCode);
					
				return new CustomDataAccessStatus<InstitutionGroupInstitution>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<InstitutionGroupInstitution>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}

