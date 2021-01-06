
// Generated 1/5/2021 3:26:52 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class ScanningRequestDAL is based upon dbo.ScanningRequest.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DAL
// {
// 		public partial class ScanningRequestDAL
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
	partial class ScanningRequestDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from dbo.ScanningRequest by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="scanningRequestID"></param>
		/// <returns>Object of type ScanningRequest.</returns>
		public ScanningRequest ScanningRequestSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int scanningRequestID)
		{
			return ScanningRequestSelectAuto(	sqlConnection, sqlTransaction, "BHL",	scanningRequestID );
		}
			
		/// <summary>
		/// Select values from dbo.ScanningRequest by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="scanningRequestID"></param>
		/// <returns>Object of type ScanningRequest.</returns>
		public ScanningRequest ScanningRequestSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int scanningRequestID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("ScanningRequestSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("ScanningRequestID", SqlDbType.Int, null, false, scanningRequestID)))
			{
				using (CustomSqlHelper<ScanningRequest> helper = new CustomSqlHelper<ScanningRequest>())
				{
					List<ScanningRequest> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						ScanningRequest o = list[0];
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
		/// Select values from dbo.ScanningRequest by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="scanningRequestID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> ScanningRequestSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int scanningRequestID)
		{
			return ScanningRequestSelectAutoRaw( sqlConnection, sqlTransaction, "BHL", scanningRequestID );
		}
		
		/// <summary>
		/// Select values from dbo.ScanningRequest by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="scanningRequestID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> ScanningRequestSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int scanningRequestID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("ScanningRequestSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("ScanningRequestID", SqlDbType.Int, null, false, scanningRequestID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====

 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into dbo.ScanningRequest.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="geminiIssueID"></param>
		/// <param name="title"></param>
		/// <param name="year"></param>
		/// <param name="type"></param>
		/// <param name="volume"></param>
		/// <param name="edition"></param>
		/// <param name="oCLC"></param>
		/// <param name="iSBN"></param>
		/// <param name="iSSN"></param>
		/// <param name="author"></param>
		/// <param name="publisher"></param>
		/// <param name="language"></param>
		/// <param name="note"></param>
		/// <returns>Object of type ScanningRequest.</returns>
		public ScanningRequest ScanningRequestInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int? geminiIssueID,
			string title,
			string year,
			string type,
			string volume,
			string edition,
			string oCLC,
			string iSBN,
			string iSSN,
			string author,
			string publisher,
			string language,
			string note)
		{
			return ScanningRequestInsertAuto( sqlConnection, sqlTransaction, "BHL", geminiIssueID, title, year, type, volume, edition, oCLC, iSBN, iSSN, author, publisher, language, note );
		}
		
		/// <summary>
		/// Insert values into dbo.ScanningRequest.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="geminiIssueID"></param>
		/// <param name="title"></param>
		/// <param name="year"></param>
		/// <param name="type"></param>
		/// <param name="volume"></param>
		/// <param name="edition"></param>
		/// <param name="oCLC"></param>
		/// <param name="iSBN"></param>
		/// <param name="iSSN"></param>
		/// <param name="author"></param>
		/// <param name="publisher"></param>
		/// <param name="language"></param>
		/// <param name="note"></param>
		/// <returns>Object of type ScanningRequest.</returns>
		public ScanningRequest ScanningRequestInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int? geminiIssueID,
			string title,
			string year,
			string type,
			string volume,
			string edition,
			string oCLC,
			string iSBN,
			string iSSN,
			string author,
			string publisher,
			string language,
			string note)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("ScanningRequestInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("ScanningRequestID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("GeminiIssueID", SqlDbType.Int, null, true, geminiIssueID),
					CustomSqlHelper.CreateInputParameter("Title", SqlDbType.NVarChar, 500, false, title),
					CustomSqlHelper.CreateInputParameter("Year", SqlDbType.NVarChar, 20, false, year),
					CustomSqlHelper.CreateInputParameter("Type", SqlDbType.NVarChar, 20, false, type),
					CustomSqlHelper.CreateInputParameter("Volume", SqlDbType.NVarChar, 100, false, volume),
					CustomSqlHelper.CreateInputParameter("Edition", SqlDbType.NVarChar, 100, false, edition),
					CustomSqlHelper.CreateInputParameter("OCLC", SqlDbType.NVarChar, 30, false, oCLC),
					CustomSqlHelper.CreateInputParameter("ISBN", SqlDbType.NVarChar, 30, false, iSBN),
					CustomSqlHelper.CreateInputParameter("ISSN", SqlDbType.NVarChar, 30, false, iSSN),
					CustomSqlHelper.CreateInputParameter("Author", SqlDbType.NVarChar, 200, false, author),
					CustomSqlHelper.CreateInputParameter("Publisher", SqlDbType.NVarChar, 200, false, publisher),
					CustomSqlHelper.CreateInputParameter("Language", SqlDbType.NVarChar, 20, false, language),
					CustomSqlHelper.CreateInputParameter("Note", SqlDbType.NVarChar, 1073741823, false, note), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<ScanningRequest> helper = new CustomSqlHelper<ScanningRequest>())
				{
					List<ScanningRequest> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						ScanningRequest o = list[0];
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
		/// Insert values into dbo.ScanningRequest. Returns an object of type ScanningRequest.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type ScanningRequest.</param>
		/// <returns>Object of type ScanningRequest.</returns>
		public ScanningRequest ScanningRequestInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			ScanningRequest value)
		{
			return ScanningRequestInsertAuto(sqlConnection, sqlTransaction, "BHL", value);
		}
		
		/// <summary>
		/// Insert values into dbo.ScanningRequest. Returns an object of type ScanningRequest.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type ScanningRequest.</param>
		/// <returns>Object of type ScanningRequest.</returns>
		public ScanningRequest ScanningRequestInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			ScanningRequest value)
		{
			return ScanningRequestInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.GeminiIssueID,
				value.Title,
				value.Year,
				value.Type,
				value.Volume,
				value.Edition,
				value.OCLC,
				value.ISBN,
				value.ISSN,
				value.Author,
				value.Publisher,
				value.Language,
				value.Note);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from dbo.ScanningRequest by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="scanningRequestID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool ScanningRequestDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int scanningRequestID)
		{
			return ScanningRequestDeleteAuto( sqlConnection, sqlTransaction, "BHL", scanningRequestID );
		}
		
		/// <summary>
		/// Delete values from dbo.ScanningRequest by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="scanningRequestID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool ScanningRequestDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int scanningRequestID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("ScanningRequestDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("ScanningRequestID", SqlDbType.Int, null, false, scanningRequestID), 
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
		/// Update values in dbo.ScanningRequest. Returns an object of type ScanningRequest.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="scanningRequestID"></param>
		/// <param name="geminiIssueID"></param>
		/// <param name="title"></param>
		/// <param name="year"></param>
		/// <param name="type"></param>
		/// <param name="volume"></param>
		/// <param name="edition"></param>
		/// <param name="oCLC"></param>
		/// <param name="iSBN"></param>
		/// <param name="iSSN"></param>
		/// <param name="author"></param>
		/// <param name="publisher"></param>
		/// <param name="language"></param>
		/// <param name="note"></param>
		/// <returns>Object of type ScanningRequest.</returns>
		public ScanningRequest ScanningRequestUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int scanningRequestID,
			int? geminiIssueID,
			string title,
			string year,
			string type,
			string volume,
			string edition,
			string oCLC,
			string iSBN,
			string iSSN,
			string author,
			string publisher,
			string language,
			string note)
		{
			return ScanningRequestUpdateAuto( sqlConnection, sqlTransaction, "BHL", scanningRequestID, geminiIssueID, title, year, type, volume, edition, oCLC, iSBN, iSSN, author, publisher, language, note);
		}
		
		/// <summary>
		/// Update values in dbo.ScanningRequest. Returns an object of type ScanningRequest.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="scanningRequestID"></param>
		/// <param name="geminiIssueID"></param>
		/// <param name="title"></param>
		/// <param name="year"></param>
		/// <param name="type"></param>
		/// <param name="volume"></param>
		/// <param name="edition"></param>
		/// <param name="oCLC"></param>
		/// <param name="iSBN"></param>
		/// <param name="iSSN"></param>
		/// <param name="author"></param>
		/// <param name="publisher"></param>
		/// <param name="language"></param>
		/// <param name="note"></param>
		/// <returns>Object of type ScanningRequest.</returns>
		public ScanningRequest ScanningRequestUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int scanningRequestID,
			int? geminiIssueID,
			string title,
			string year,
			string type,
			string volume,
			string edition,
			string oCLC,
			string iSBN,
			string iSSN,
			string author,
			string publisher,
			string language,
			string note)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("ScanningRequestUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("ScanningRequestID", SqlDbType.Int, null, false, scanningRequestID),
					CustomSqlHelper.CreateInputParameter("GeminiIssueID", SqlDbType.Int, null, true, geminiIssueID),
					CustomSqlHelper.CreateInputParameter("Title", SqlDbType.NVarChar, 500, false, title),
					CustomSqlHelper.CreateInputParameter("Year", SqlDbType.NVarChar, 20, false, year),
					CustomSqlHelper.CreateInputParameter("Type", SqlDbType.NVarChar, 20, false, type),
					CustomSqlHelper.CreateInputParameter("Volume", SqlDbType.NVarChar, 100, false, volume),
					CustomSqlHelper.CreateInputParameter("Edition", SqlDbType.NVarChar, 100, false, edition),
					CustomSqlHelper.CreateInputParameter("OCLC", SqlDbType.NVarChar, 30, false, oCLC),
					CustomSqlHelper.CreateInputParameter("ISBN", SqlDbType.NVarChar, 30, false, iSBN),
					CustomSqlHelper.CreateInputParameter("ISSN", SqlDbType.NVarChar, 30, false, iSSN),
					CustomSqlHelper.CreateInputParameter("Author", SqlDbType.NVarChar, 200, false, author),
					CustomSqlHelper.CreateInputParameter("Publisher", SqlDbType.NVarChar, 200, false, publisher),
					CustomSqlHelper.CreateInputParameter("Language", SqlDbType.NVarChar, 20, false, language),
					CustomSqlHelper.CreateInputParameter("Note", SqlDbType.NVarChar, 1073741823, false, note), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<ScanningRequest> helper = new CustomSqlHelper<ScanningRequest>())
				{
					List<ScanningRequest> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						ScanningRequest o = list[0];
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
		/// Update values in dbo.ScanningRequest. Returns an object of type ScanningRequest.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type ScanningRequest.</param>
		/// <returns>Object of type ScanningRequest.</returns>
		public ScanningRequest ScanningRequestUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			ScanningRequest value)
		{
			return ScanningRequestUpdateAuto(sqlConnection, sqlTransaction, "BHL", value );
		}
		
		/// <summary>
		/// Update values in dbo.ScanningRequest. Returns an object of type ScanningRequest.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type ScanningRequest.</param>
		/// <returns>Object of type ScanningRequest.</returns>
		public ScanningRequest ScanningRequestUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			ScanningRequest value)
		{
			return ScanningRequestUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.ScanningRequestID,
				value.GeminiIssueID,
				value.Title,
				value.Year,
				value.Type,
				value.Volume,
				value.Edition,
				value.OCLC,
				value.ISBN,
				value.ISSN,
				value.Author,
				value.Publisher,
				value.Language,
				value.Note);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage dbo.ScanningRequest object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.ScanningRequest.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type ScanningRequest.</param>
		/// <returns>Object of type CustomDataAccessStatus<ScanningRequest>.</returns>
		public CustomDataAccessStatus<ScanningRequest> ScanningRequestManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			ScanningRequest value  )
		{
			return ScanningRequestManageAuto( sqlConnection, sqlTransaction, "BHL", value  );
		}
		
		/// <summary>
		/// Manage dbo.ScanningRequest object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.ScanningRequest.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type ScanningRequest.</param>
		/// <returns>Object of type CustomDataAccessStatus<ScanningRequest>.</returns>
		public CustomDataAccessStatus<ScanningRequest> ScanningRequestManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			ScanningRequest value  )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				
				
				ScanningRequest returnValue = ScanningRequestInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.GeminiIssueID,
						value.Title,
						value.Year,
						value.Type,
						value.Volume,
						value.Edition,
						value.OCLC,
						value.ISBN,
						value.ISSN,
						value.Author,
						value.Publisher,
						value.Language,
						value.Note);
				
				return new CustomDataAccessStatus<ScanningRequest>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (ScanningRequestDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.ScanningRequestID))
				{
				return new CustomDataAccessStatus<ScanningRequest>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<ScanningRequest>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				
				ScanningRequest returnValue = ScanningRequestUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.ScanningRequestID,
						value.GeminiIssueID,
						value.Title,
						value.Year,
						value.Type,
						value.Volume,
						value.Edition,
						value.OCLC,
						value.ISBN,
						value.ISSN,
						value.Author,
						value.Publisher,
						value.Language,
						value.Note);
					
				return new CustomDataAccessStatus<ScanningRequest>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<ScanningRequest>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}

