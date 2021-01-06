
// Generated 1/5/2021 3:25:59 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class LanguageDAL is based upon dbo.Language.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DAL
// {
// 		public partial class LanguageDAL
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
	partial class LanguageDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from dbo.Language by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="languageCode"></param>
		/// <returns>Object of type Language.</returns>
		public Language LanguageSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string languageCode)
		{
			return LanguageSelectAuto(	sqlConnection, sqlTransaction, "BHL",	languageCode );
		}
			
		/// <summary>
		/// Select values from dbo.Language by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="languageCode"></param>
		/// <returns>Object of type Language.</returns>
		public Language LanguageSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			string languageCode )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("LanguageSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("LanguageCode", SqlDbType.NVarChar, 10, false, languageCode)))
			{
				using (CustomSqlHelper<Language> helper = new CustomSqlHelper<Language>())
				{
					List<Language> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						Language o = list[0];
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
		/// Select values from dbo.Language by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="languageCode"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> LanguageSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string languageCode)
		{
			return LanguageSelectAutoRaw( sqlConnection, sqlTransaction, "BHL", languageCode );
		}
		
		/// <summary>
		/// Select values from dbo.Language by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="languageCode"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> LanguageSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			string languageCode)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("LanguageSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("LanguageCode", SqlDbType.NVarChar, 10, false, languageCode)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====

 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into dbo.Language.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="languageCode"></param>
		/// <param name="languageName"></param>
		/// <param name="note"></param>
		/// <returns>Object of type Language.</returns>
		public Language LanguageInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string languageCode,
			string languageName,
			string note)
		{
			return LanguageInsertAuto( sqlConnection, sqlTransaction, "BHL", languageCode, languageName, note );
		}
		
		/// <summary>
		/// Insert values into dbo.Language.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="languageCode"></param>
		/// <param name="languageName"></param>
		/// <param name="note"></param>
		/// <returns>Object of type Language.</returns>
		public Language LanguageInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			string languageCode,
			string languageName,
			string note)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("LanguageInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("LanguageCode", SqlDbType.NVarChar, 10, false, languageCode),
					CustomSqlHelper.CreateInputParameter("LanguageName", SqlDbType.NVarChar, 20, false, languageName),
					CustomSqlHelper.CreateInputParameter("Note", SqlDbType.NVarChar, 255, true, note), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<Language> helper = new CustomSqlHelper<Language>())
				{
					List<Language> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						Language o = list[0];
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
		/// Insert values into dbo.Language. Returns an object of type Language.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type Language.</param>
		/// <returns>Object of type Language.</returns>
		public Language LanguageInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			Language value)
		{
			return LanguageInsertAuto(sqlConnection, sqlTransaction, "BHL", value);
		}
		
		/// <summary>
		/// Insert values into dbo.Language. Returns an object of type Language.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type Language.</param>
		/// <returns>Object of type Language.</returns>
		public Language LanguageInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			Language value)
		{
			return LanguageInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.LanguageCode,
				value.LanguageName,
				value.Note);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from dbo.Language by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="languageCode"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool LanguageDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string languageCode)
		{
			return LanguageDeleteAuto( sqlConnection, sqlTransaction, "BHL", languageCode );
		}
		
		/// <summary>
		/// Delete values from dbo.Language by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="languageCode"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool LanguageDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			string languageCode)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("LanguageDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("LanguageCode", SqlDbType.NVarChar, 10, false, languageCode), 
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
		/// Update values in dbo.Language. Returns an object of type Language.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="languageCode"></param>
		/// <param name="languageName"></param>
		/// <param name="note"></param>
		/// <returns>Object of type Language.</returns>
		public Language LanguageUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string languageCode,
			string languageName,
			string note)
		{
			return LanguageUpdateAuto( sqlConnection, sqlTransaction, "BHL", languageCode, languageName, note);
		}
		
		/// <summary>
		/// Update values in dbo.Language. Returns an object of type Language.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="languageCode"></param>
		/// <param name="languageName"></param>
		/// <param name="note"></param>
		/// <returns>Object of type Language.</returns>
		public Language LanguageUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			string languageCode,
			string languageName,
			string note)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("LanguageUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("LanguageCode", SqlDbType.NVarChar, 10, false, languageCode),
					CustomSqlHelper.CreateInputParameter("LanguageName", SqlDbType.NVarChar, 20, false, languageName),
					CustomSqlHelper.CreateInputParameter("Note", SqlDbType.NVarChar, 255, true, note), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<Language> helper = new CustomSqlHelper<Language>())
				{
					List<Language> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						Language o = list[0];
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
		/// Update values in dbo.Language. Returns an object of type Language.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type Language.</param>
		/// <returns>Object of type Language.</returns>
		public Language LanguageUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			Language value)
		{
			return LanguageUpdateAuto(sqlConnection, sqlTransaction, "BHL", value );
		}
		
		/// <summary>
		/// Update values in dbo.Language. Returns an object of type Language.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type Language.</param>
		/// <returns>Object of type Language.</returns>
		public Language LanguageUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			Language value)
		{
			return LanguageUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.LanguageCode,
				value.LanguageName,
				value.Note);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage dbo.Language object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.Language.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type Language.</param>
		/// <returns>Object of type CustomDataAccessStatus<Language>.</returns>
		public CustomDataAccessStatus<Language> LanguageManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			Language value  )
		{
			return LanguageManageAuto( sqlConnection, sqlTransaction, "BHL", value  );
		}
		
		/// <summary>
		/// Manage dbo.Language object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.Language.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type Language.</param>
		/// <returns>Object of type CustomDataAccessStatus<Language>.</returns>
		public CustomDataAccessStatus<Language> LanguageManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			Language value  )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				
				
				Language returnValue = LanguageInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.LanguageCode,
						value.LanguageName,
						value.Note);
				
				return new CustomDataAccessStatus<Language>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (LanguageDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.LanguageCode))
				{
				return new CustomDataAccessStatus<Language>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<Language>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				
				Language returnValue = LanguageUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.LanguageCode,
						value.LanguageName,
						value.Note);
					
				return new CustomDataAccessStatus<Language>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<Language>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}

