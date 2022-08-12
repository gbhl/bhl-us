
// Generated 1/5/2021 3:27:18 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class TitleInstitutionDAL is based upon dbo.TitleInstitution.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DAL
// {
// 		public partial class TitleInstitutionDAL
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
	partial class TitleInstitutionDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from dbo.TitleInstitution by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="titleInstitutionID"></param>
		/// <returns>Object of type TitleInstitution.</returns>
		public TitleInstitution TitleInstitutionSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int titleInstitutionID)
		{
			return TitleInstitutionSelectAuto(	sqlConnection, sqlTransaction, "BHL",	titleInstitutionID );
		}
			
		/// <summary>
		/// Select values from dbo.TitleInstitution by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="titleInstitutionID"></param>
		/// <returns>Object of type TitleInstitution.</returns>
		public TitleInstitution TitleInstitutionSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int titleInstitutionID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleInstitutionSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("TitleInstitutionID", SqlDbType.Int, null, false, titleInstitutionID)))
			{
				using (CustomSqlHelper<TitleInstitution> helper = new CustomSqlHelper<TitleInstitution>())
				{
					List<TitleInstitution> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						TitleInstitution o = list[0];
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
		/// Select values from dbo.TitleInstitution by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="titleInstitutionID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> TitleInstitutionSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int titleInstitutionID)
		{
			return TitleInstitutionSelectAutoRaw( sqlConnection, sqlTransaction, "BHL", titleInstitutionID );
		}
		
		/// <summary>
		/// Select values from dbo.TitleInstitution by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="titleInstitutionID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> TitleInstitutionSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int titleInstitutionID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleInstitutionSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("TitleInstitutionID", SqlDbType.Int, null, false, titleInstitutionID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====

 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into dbo.TitleInstitution.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="titleID"></param>
		/// <param name="institutionCode"></param>
		/// <param name="institutionRoleID"></param>
		/// <param name="url"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type TitleInstitution.</returns>
		public TitleInstitution TitleInstitutionInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int titleID,
			string institutionCode,
			int institutionRoleID,
			string url,
			int creationUserID,
			int lastModifiedUserID)
		{
			return TitleInstitutionInsertAuto( sqlConnection, sqlTransaction, "BHL", titleID, institutionCode, institutionRoleID, url, creationUserID, lastModifiedUserID );
		}
		
		/// <summary>
		/// Insert values into dbo.TitleInstitution.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="titleID"></param>
		/// <param name="institutionCode"></param>
		/// <param name="institutionRoleID"></param>
		/// <param name="url"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type TitleInstitution.</returns>
		public TitleInstitution TitleInstitutionInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int titleID,
			string institutionCode,
			int institutionRoleID,
			string url,
			int creationUserID,
			int lastModifiedUserID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleInstitutionInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("TitleInstitutionID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("TitleID", SqlDbType.Int, null, false, titleID),
					CustomSqlHelper.CreateInputParameter("InstitutionCode", SqlDbType.NVarChar, 10, false, institutionCode),
					CustomSqlHelper.CreateInputParameter("InstitutionRoleID", SqlDbType.Int, null, false, institutionRoleID),
					CustomSqlHelper.CreateInputParameter("Url", SqlDbType.NVarChar, 500, false, url),
					CustomSqlHelper.CreateInputParameter("CreationUserID", SqlDbType.Int, null, false, creationUserID),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, false, lastModifiedUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<TitleInstitution> helper = new CustomSqlHelper<TitleInstitution>())
				{
					List<TitleInstitution> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						TitleInstitution o = list[0];
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
		/// Insert values into dbo.TitleInstitution. Returns an object of type TitleInstitution.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type TitleInstitution.</param>
		/// <returns>Object of type TitleInstitution.</returns>
		public TitleInstitution TitleInstitutionInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			TitleInstitution value)
		{
			return TitleInstitutionInsertAuto(sqlConnection, sqlTransaction, "BHL", value);
		}
		
		/// <summary>
		/// Insert values into dbo.TitleInstitution. Returns an object of type TitleInstitution.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type TitleInstitution.</param>
		/// <returns>Object of type TitleInstitution.</returns>
		public TitleInstitution TitleInstitutionInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			TitleInstitution value)
		{
			return TitleInstitutionInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.TitleID,
				value.InstitutionCode,
				value.InstitutionRoleID,
				value.Url,
				value.CreationUserID,
				value.LastModifiedUserID);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from dbo.TitleInstitution by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="titleInstitutionID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool TitleInstitutionDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int titleInstitutionID)
		{
			return TitleInstitutionDeleteAuto( sqlConnection, sqlTransaction, "BHL", titleInstitutionID );
		}
		
		/// <summary>
		/// Delete values from dbo.TitleInstitution by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="titleInstitutionID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool TitleInstitutionDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int titleInstitutionID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleInstitutionDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("TitleInstitutionID", SqlDbType.Int, null, false, titleInstitutionID), 
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
		/// Update values in dbo.TitleInstitution. Returns an object of type TitleInstitution.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="titleInstitutionID"></param>
		/// <param name="titleID"></param>
		/// <param name="institutionCode"></param>
		/// <param name="institutionRoleID"></param>
		/// <param name="url"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type TitleInstitution.</returns>
		public TitleInstitution TitleInstitutionUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int titleInstitutionID,
			int titleID,
			string institutionCode,
			int institutionRoleID,
			string url,
			int lastModifiedUserID)
		{
			return TitleInstitutionUpdateAuto( sqlConnection, sqlTransaction, "BHL", titleInstitutionID, titleID, institutionCode, institutionRoleID, url, lastModifiedUserID);
		}
		
		/// <summary>
		/// Update values in dbo.TitleInstitution. Returns an object of type TitleInstitution.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="titleInstitutionID"></param>
		/// <param name="titleID"></param>
		/// <param name="institutionCode"></param>
		/// <param name="institutionRoleID"></param>
		/// <param name="url"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type TitleInstitution.</returns>
		public TitleInstitution TitleInstitutionUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int titleInstitutionID,
			int titleID,
			string institutionCode,
			int institutionRoleID,
			string url,
			int lastModifiedUserID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleInstitutionUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("TitleInstitutionID", SqlDbType.Int, null, false, titleInstitutionID),
					CustomSqlHelper.CreateInputParameter("TitleID", SqlDbType.Int, null, false, titleID),
					CustomSqlHelper.CreateInputParameter("InstitutionCode", SqlDbType.NVarChar, 10, false, institutionCode),
					CustomSqlHelper.CreateInputParameter("InstitutionRoleID", SqlDbType.Int, null, false, institutionRoleID),
					CustomSqlHelper.CreateInputParameter("Url", SqlDbType.NVarChar, 500, false, url),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, false, lastModifiedUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<TitleInstitution> helper = new CustomSqlHelper<TitleInstitution>())
				{
					List<TitleInstitution> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						TitleInstitution o = list[0];
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
		/// Update values in dbo.TitleInstitution. Returns an object of type TitleInstitution.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type TitleInstitution.</param>
		/// <returns>Object of type TitleInstitution.</returns>
		public TitleInstitution TitleInstitutionUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			TitleInstitution value)
		{
			return TitleInstitutionUpdateAuto(sqlConnection, sqlTransaction, "BHL", value );
		}
		
		/// <summary>
		/// Update values in dbo.TitleInstitution. Returns an object of type TitleInstitution.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type TitleInstitution.</param>
		/// <returns>Object of type TitleInstitution.</returns>
		public TitleInstitution TitleInstitutionUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			TitleInstitution value)
		{
			return TitleInstitutionUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.TitleInstitutionID,
				value.TitleID,
				value.InstitutionCode,
				value.InstitutionRoleID,
				value.Url,
				value.LastModifiedUserID);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage dbo.TitleInstitution object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.TitleInstitution.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type TitleInstitution.</param>
		/// <returns>Object of type CustomDataAccessStatus<TitleInstitution>.</returns>
		public CustomDataAccessStatus<TitleInstitution> TitleInstitutionManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			TitleInstitution value , int userId )
		{
			return TitleInstitutionManageAuto( sqlConnection, sqlTransaction, "BHL", value , userId );
		}
		
		/// <summary>
		/// Manage dbo.TitleInstitution object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.TitleInstitution.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type TitleInstitution.</param>
		/// <returns>Object of type CustomDataAccessStatus<TitleInstitution>.</returns>
		public CustomDataAccessStatus<TitleInstitution> TitleInstitutionManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			TitleInstitution value , int userId )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				value.CreationUserID = userId;
				value.LastModifiedUserID = userId;
				TitleInstitution returnValue = TitleInstitutionInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.TitleID,
						value.InstitutionCode,
						value.InstitutionRoleID,
						value.Url,
						value.CreationUserID,
						value.LastModifiedUserID);
				
				return new CustomDataAccessStatus<TitleInstitution>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (TitleInstitutionDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.TitleInstitutionID))
				{
				return new CustomDataAccessStatus<TitleInstitution>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<TitleInstitution>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				value.LastModifiedUserID = userId;
				TitleInstitution returnValue = TitleInstitutionUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.TitleInstitutionID,
						value.TitleID,
						value.InstitutionCode,
						value.InstitutionRoleID,
						value.Url,
						value.LastModifiedUserID);
					
				return new CustomDataAccessStatus<TitleInstitution>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<TitleInstitution>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}

