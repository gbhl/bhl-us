
// Generated 1/5/2021 3:27:22 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class TitleLanguageDAL is based upon dbo.TitleLanguage.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DAL
// {
// 		public partial class TitleLanguageDAL
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
	partial class TitleLanguageDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from dbo.TitleLanguage by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="titleLanguageID"></param>
		/// <returns>Object of type TitleLanguage.</returns>
		public TitleLanguage TitleLanguageSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int titleLanguageID)
		{
			return TitleLanguageSelectAuto(	sqlConnection, sqlTransaction, "BHL",	titleLanguageID );
		}
			
		/// <summary>
		/// Select values from dbo.TitleLanguage by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="titleLanguageID"></param>
		/// <returns>Object of type TitleLanguage.</returns>
		public TitleLanguage TitleLanguageSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int titleLanguageID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleLanguageSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("TitleLanguageID", SqlDbType.Int, null, false, titleLanguageID)))
			{
				using (CustomSqlHelper<TitleLanguage> helper = new CustomSqlHelper<TitleLanguage>())
				{
					List<TitleLanguage> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						TitleLanguage o = list[0];
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
		/// Select values from dbo.TitleLanguage by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="titleLanguageID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> TitleLanguageSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int titleLanguageID)
		{
			return TitleLanguageSelectAutoRaw( sqlConnection, sqlTransaction, "BHL", titleLanguageID );
		}
		
		/// <summary>
		/// Select values from dbo.TitleLanguage by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="titleLanguageID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> TitleLanguageSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int titleLanguageID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleLanguageSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("TitleLanguageID", SqlDbType.Int, null, false, titleLanguageID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====

 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into dbo.TitleLanguage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="titleID"></param>
		/// <param name="languageCode"></param>
		/// <param name="creationUserID"></param>
		/// <returns>Object of type TitleLanguage.</returns>
		public TitleLanguage TitleLanguageInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int titleID,
			string languageCode,
			int? creationUserID)
		{
			return TitleLanguageInsertAuto( sqlConnection, sqlTransaction, "BHL", titleID, languageCode, creationUserID );
		}
		
		/// <summary>
		/// Insert values into dbo.TitleLanguage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="titleID"></param>
		/// <param name="languageCode"></param>
		/// <param name="creationUserID"></param>
		/// <returns>Object of type TitleLanguage.</returns>
		public TitleLanguage TitleLanguageInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int titleID,
			string languageCode,
			int? creationUserID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleLanguageInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("TitleLanguageID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("TitleID", SqlDbType.Int, null, false, titleID),
					CustomSqlHelper.CreateInputParameter("LanguageCode", SqlDbType.NVarChar, 10, false, languageCode),
					CustomSqlHelper.CreateInputParameter("CreationUserID", SqlDbType.Int, null, true, creationUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<TitleLanguage> helper = new CustomSqlHelper<TitleLanguage>())
				{
					List<TitleLanguage> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						TitleLanguage o = list[0];
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
		/// Insert values into dbo.TitleLanguage. Returns an object of type TitleLanguage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type TitleLanguage.</param>
		/// <returns>Object of type TitleLanguage.</returns>
		public TitleLanguage TitleLanguageInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			TitleLanguage value)
		{
			return TitleLanguageInsertAuto(sqlConnection, sqlTransaction, "BHL", value);
		}
		
		/// <summary>
		/// Insert values into dbo.TitleLanguage. Returns an object of type TitleLanguage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type TitleLanguage.</param>
		/// <returns>Object of type TitleLanguage.</returns>
		public TitleLanguage TitleLanguageInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			TitleLanguage value)
		{
			return TitleLanguageInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.TitleID,
				value.LanguageCode,
				value.CreationUserID);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from dbo.TitleLanguage by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="titleLanguageID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool TitleLanguageDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int titleLanguageID)
		{
			return TitleLanguageDeleteAuto( sqlConnection, sqlTransaction, "BHL", titleLanguageID );
		}
		
		/// <summary>
		/// Delete values from dbo.TitleLanguage by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="titleLanguageID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool TitleLanguageDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int titleLanguageID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleLanguageDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("TitleLanguageID", SqlDbType.Int, null, false, titleLanguageID), 
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
		/// Update values in dbo.TitleLanguage. Returns an object of type TitleLanguage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="titleLanguageID"></param>
		/// <param name="titleID"></param>
		/// <param name="languageCode"></param>
		/// <returns>Object of type TitleLanguage.</returns>
		public TitleLanguage TitleLanguageUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int titleLanguageID,
			int titleID,
			string languageCode)
		{
			return TitleLanguageUpdateAuto( sqlConnection, sqlTransaction, "BHL", titleLanguageID, titleID, languageCode);
		}
		
		/// <summary>
		/// Update values in dbo.TitleLanguage. Returns an object of type TitleLanguage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="titleLanguageID"></param>
		/// <param name="titleID"></param>
		/// <param name="languageCode"></param>
		/// <returns>Object of type TitleLanguage.</returns>
		public TitleLanguage TitleLanguageUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int titleLanguageID,
			int titleID,
			string languageCode)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleLanguageUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("TitleLanguageID", SqlDbType.Int, null, false, titleLanguageID),
					CustomSqlHelper.CreateInputParameter("TitleID", SqlDbType.Int, null, false, titleID),
					CustomSqlHelper.CreateInputParameter("LanguageCode", SqlDbType.NVarChar, 10, false, languageCode), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<TitleLanguage> helper = new CustomSqlHelper<TitleLanguage>())
				{
					List<TitleLanguage> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						TitleLanguage o = list[0];
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
		/// Update values in dbo.TitleLanguage. Returns an object of type TitleLanguage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type TitleLanguage.</param>
		/// <returns>Object of type TitleLanguage.</returns>
		public TitleLanguage TitleLanguageUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			TitleLanguage value)
		{
			return TitleLanguageUpdateAuto(sqlConnection, sqlTransaction, "BHL", value );
		}
		
		/// <summary>
		/// Update values in dbo.TitleLanguage. Returns an object of type TitleLanguage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type TitleLanguage.</param>
		/// <returns>Object of type TitleLanguage.</returns>
		public TitleLanguage TitleLanguageUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			TitleLanguage value)
		{
			return TitleLanguageUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.TitleLanguageID,
				value.TitleID,
				value.LanguageCode);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage dbo.TitleLanguage object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.TitleLanguage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type TitleLanguage.</param>
		/// <returns>Object of type CustomDataAccessStatus<TitleLanguage>.</returns>
		public CustomDataAccessStatus<TitleLanguage> TitleLanguageManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			TitleLanguage value , int userId )
		{
			return TitleLanguageManageAuto( sqlConnection, sqlTransaction, "BHL", value , userId );
		}
		
		/// <summary>
		/// Manage dbo.TitleLanguage object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.TitleLanguage.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type TitleLanguage.</param>
		/// <returns>Object of type CustomDataAccessStatus<TitleLanguage>.</returns>
		public CustomDataAccessStatus<TitleLanguage> TitleLanguageManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			TitleLanguage value , int userId )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				value.CreationUserID = userId;
				
				TitleLanguage returnValue = TitleLanguageInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.TitleID,
						value.LanguageCode,
						value.CreationUserID);
				
				return new CustomDataAccessStatus<TitleLanguage>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (TitleLanguageDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.TitleLanguageID))
				{
				return new CustomDataAccessStatus<TitleLanguage>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<TitleLanguage>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				
				TitleLanguage returnValue = TitleLanguageUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.TitleLanguageID,
						value.TitleID,
						value.LanguageCode);
					
				return new CustomDataAccessStatus<TitleLanguage>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<TitleLanguage>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}

