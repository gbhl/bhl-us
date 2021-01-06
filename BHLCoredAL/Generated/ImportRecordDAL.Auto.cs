
// Generated 11/7/2017 10:02:12 AM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class ImportRecordDAL is based upon import.ImportRecord.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DAL
// {
// 		public partial class ImportRecordDAL
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
	partial class ImportRecordDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from import.ImportRecord by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="importRecordID"></param>
		/// <returns>Object of type ImportRecord.</returns>
		public ImportRecord ImportRecordSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int importRecordID)
		{
			return ImportRecordSelectAuto(	sqlConnection, sqlTransaction, "BHL",	importRecordID );
		}
			
		/// <summary>
		/// Select values from import.ImportRecord by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="importRecordID"></param>
		/// <returns>Object of type ImportRecord.</returns>
		public ImportRecord ImportRecordSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int importRecordID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("import.ImportRecordSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("ImportRecordID", SqlDbType.Int, null, false, importRecordID)))
			{
				using (CustomSqlHelper<ImportRecord> helper = new CustomSqlHelper<ImportRecord>())
				{
					List<ImportRecord> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						ImportRecord o = list[0];
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
		/// Select values from import.ImportRecord by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="importRecordID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> ImportRecordSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int importRecordID)
		{
			return ImportRecordSelectAutoRaw( sqlConnection, sqlTransaction, "BHL", importRecordID );
		}
		
		/// <summary>
		/// Select values from import.ImportRecord by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="importRecordID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> ImportRecordSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int importRecordID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("import.ImportRecordSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("ImportRecordID", SqlDbType.Int, null, false, importRecordID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====

 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into import.ImportRecord.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="importFileID"></param>
		/// <param name="importRecordStatusID"></param>
		/// <param name="genre"></param>
		/// <param name="title"></param>
		/// <param name="translatedTitle"></param>
		/// <param name="journalTitle"></param>
		/// <param name="volume"></param>
		/// <param name="series"></param>
		/// <param name="issue"></param>
		/// <param name="edition"></param>
		/// <param name="publicationDetails"></param>
		/// <param name="publisherName"></param>
		/// <param name="publisherPlace"></param>
		/// <param name="year"></param>
		/// <param name="startYear"></param>
		/// <param name="endYear"></param>
		/// <param name="language"></param>
		/// <param name="summary"></param>
		/// <param name="notes"></param>
		/// <param name="rights"></param>
		/// <param name="dueDiligence"></param>
		/// <param name="copyrightStatus"></param>
		/// <param name="license"></param>
		/// <param name="licenseUrl"></param>
		/// <param name="pageRange"></param>
		/// <param name="startPage"></param>
		/// <param name="endPage"></param>
		/// <param name="url"></param>
		/// <param name="downloadUrl"></param>
		/// <param name="dOI"></param>
		/// <param name="iSSN"></param>
		/// <param name="iSBN"></param>
		/// <param name="oCLC"></param>
		/// <param name="lCCN"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <param name="titleID"></param>
		/// <param name="itemID"></param>
		/// <param name="startPageID"></param>
		/// <param name="endPageID"></param>
		/// <param name="segmentID"></param>
		/// <returns>Object of type ImportRecord.</returns>
		public ImportRecord ImportRecordInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int importFileID,
			int importRecordStatusID,
			string genre,
			string title,
			string translatedTitle,
			string journalTitle,
			string volume,
			string series,
			string issue,
			string edition,
			string publicationDetails,
			string publisherName,
			string publisherPlace,
			string year,
			short? startYear,
			short? endYear,
			string language,
			string summary,
			string notes,
			string rights,
			string dueDiligence,
			string copyrightStatus,
			string license,
			string licenseUrl,
			string pageRange,
			string startPage,
			string endPage,
			string url,
			string downloadUrl,
			string dOI,
			string iSSN,
			string iSBN,
			string oCLC,
			string lCCN,
			int creationUserID,
			int lastModifiedUserID,
			int? titleID,
			int? itemID,
			int? startPageID,
			int? endPageID,
			int? segmentID)
		{
			return ImportRecordInsertAuto( sqlConnection, sqlTransaction, "BHL", importFileID, importRecordStatusID, genre, title, translatedTitle, journalTitle, volume, series, issue, edition, publicationDetails, publisherName, publisherPlace, year, startYear, endYear, language, summary, notes, rights, dueDiligence, copyrightStatus, license, licenseUrl, pageRange, startPage, endPage, url, downloadUrl, dOI, iSSN, iSBN, oCLC, lCCN, creationUserID, lastModifiedUserID, titleID, itemID, startPageID, endPageID, segmentID );
		}
		
		/// <summary>
		/// Insert values into import.ImportRecord.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="importFileID"></param>
		/// <param name="importRecordStatusID"></param>
		/// <param name="genre"></param>
		/// <param name="title"></param>
		/// <param name="translatedTitle"></param>
		/// <param name="journalTitle"></param>
		/// <param name="volume"></param>
		/// <param name="series"></param>
		/// <param name="issue"></param>
		/// <param name="edition"></param>
		/// <param name="publicationDetails"></param>
		/// <param name="publisherName"></param>
		/// <param name="publisherPlace"></param>
		/// <param name="year"></param>
		/// <param name="startYear"></param>
		/// <param name="endYear"></param>
		/// <param name="language"></param>
		/// <param name="summary"></param>
		/// <param name="notes"></param>
		/// <param name="rights"></param>
		/// <param name="dueDiligence"></param>
		/// <param name="copyrightStatus"></param>
		/// <param name="license"></param>
		/// <param name="licenseUrl"></param>
		/// <param name="pageRange"></param>
		/// <param name="startPage"></param>
		/// <param name="endPage"></param>
		/// <param name="url"></param>
		/// <param name="downloadUrl"></param>
		/// <param name="dOI"></param>
		/// <param name="iSSN"></param>
		/// <param name="iSBN"></param>
		/// <param name="oCLC"></param>
		/// <param name="lCCN"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <param name="titleID"></param>
		/// <param name="itemID"></param>
		/// <param name="startPageID"></param>
		/// <param name="endPageID"></param>
		/// <param name="segmentID"></param>
		/// <returns>Object of type ImportRecord.</returns>
		public ImportRecord ImportRecordInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int importFileID,
			int importRecordStatusID,
			string genre,
			string title,
			string translatedTitle,
			string journalTitle,
			string volume,
			string series,
			string issue,
			string edition,
			string publicationDetails,
			string publisherName,
			string publisherPlace,
			string year,
			short? startYear,
			short? endYear,
			string language,
			string summary,
			string notes,
			string rights,
			string dueDiligence,
			string copyrightStatus,
			string license,
			string licenseUrl,
			string pageRange,
			string startPage,
			string endPage,
			string url,
			string downloadUrl,
			string dOI,
			string iSSN,
			string iSBN,
			string oCLC,
			string lCCN,
			int creationUserID,
			int lastModifiedUserID,
			int? titleID,
			int? itemID,
			int? startPageID,
			int? endPageID,
			int? segmentID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("import.ImportRecordInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("ImportRecordID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("ImportFileID", SqlDbType.Int, null, false, importFileID),
					CustomSqlHelper.CreateInputParameter("ImportRecordStatusID", SqlDbType.Int, null, false, importRecordStatusID),
					CustomSqlHelper.CreateInputParameter("Genre", SqlDbType.NVarChar, 50, false, genre),
					CustomSqlHelper.CreateInputParameter("Title", SqlDbType.NVarChar, 2000, false, title),
					CustomSqlHelper.CreateInputParameter("TranslatedTitle", SqlDbType.NVarChar, 2000, false, translatedTitle),
					CustomSqlHelper.CreateInputParameter("JournalTitle", SqlDbType.NVarChar, 2000, false, journalTitle),
					CustomSqlHelper.CreateInputParameter("Volume", SqlDbType.NVarChar, 100, false, volume),
					CustomSqlHelper.CreateInputParameter("Series", SqlDbType.NVarChar, 100, false, series),
					CustomSqlHelper.CreateInputParameter("Issue", SqlDbType.NVarChar, 100, false, issue),
					CustomSqlHelper.CreateInputParameter("Edition", SqlDbType.NVarChar, 400, false, edition),
					CustomSqlHelper.CreateInputParameter("PublicationDetails", SqlDbType.NVarChar, 400, false, publicationDetails),
					CustomSqlHelper.CreateInputParameter("PublisherName", SqlDbType.NVarChar, 250, false, publisherName),
					CustomSqlHelper.CreateInputParameter("PublisherPlace", SqlDbType.NVarChar, 150, false, publisherPlace),
					CustomSqlHelper.CreateInputParameter("Year", SqlDbType.NVarChar, 20, false, year),
					CustomSqlHelper.CreateInputParameter("StartYear", SqlDbType.SmallInt, null, true, startYear),
					CustomSqlHelper.CreateInputParameter("EndYear", SqlDbType.SmallInt, null, true, endYear),
					CustomSqlHelper.CreateInputParameter("Language", SqlDbType.NVarChar, 30, true, language),
					CustomSqlHelper.CreateInputParameter("Summary", SqlDbType.NVarChar, 1073741823, false, summary),
					CustomSqlHelper.CreateInputParameter("Notes", SqlDbType.NVarChar, 1073741823, false, notes),
					CustomSqlHelper.CreateInputParameter("Rights", SqlDbType.NVarChar, 1073741823, false, rights),
					CustomSqlHelper.CreateInputParameter("DueDiligence", SqlDbType.NVarChar, 1073741823, false, dueDiligence),
					CustomSqlHelper.CreateInputParameter("CopyrightStatus", SqlDbType.NVarChar, 1073741823, false, copyrightStatus),
					CustomSqlHelper.CreateInputParameter("License", SqlDbType.NVarChar, 1073741823, false, license),
					CustomSqlHelper.CreateInputParameter("LicenseUrl", SqlDbType.NVarChar, 200, false, licenseUrl),
					CustomSqlHelper.CreateInputParameter("PageRange", SqlDbType.NVarChar, 50, false, pageRange),
					CustomSqlHelper.CreateInputParameter("StartPage", SqlDbType.NVarChar, 20, false, startPage),
					CustomSqlHelper.CreateInputParameter("EndPage", SqlDbType.NVarChar, 20, false, endPage),
					CustomSqlHelper.CreateInputParameter("Url", SqlDbType.NVarChar, 200, false, url),
					CustomSqlHelper.CreateInputParameter("DownloadUrl", SqlDbType.NVarChar, 200, false, downloadUrl),
					CustomSqlHelper.CreateInputParameter("DOI", SqlDbType.NVarChar, 125, false, dOI),
					CustomSqlHelper.CreateInputParameter("ISSN", SqlDbType.NVarChar, 125, false, iSSN),
					CustomSqlHelper.CreateInputParameter("ISBN", SqlDbType.NVarChar, 125, false, iSBN),
					CustomSqlHelper.CreateInputParameter("OCLC", SqlDbType.NVarChar, 125, false, oCLC),
					CustomSqlHelper.CreateInputParameter("LCCN", SqlDbType.NVarChar, 125, false, lCCN),
					CustomSqlHelper.CreateInputParameter("CreationUserID", SqlDbType.Int, null, false, creationUserID),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, false, lastModifiedUserID),
					CustomSqlHelper.CreateInputParameter("TitleID", SqlDbType.Int, null, true, titleID),
					CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, true, itemID),
					CustomSqlHelper.CreateInputParameter("StartPageID", SqlDbType.Int, null, true, startPageID),
					CustomSqlHelper.CreateInputParameter("EndPageID", SqlDbType.Int, null, true, endPageID),
					CustomSqlHelper.CreateInputParameter("SegmentID", SqlDbType.Int, null, true, segmentID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<ImportRecord> helper = new CustomSqlHelper<ImportRecord>())
				{
					List<ImportRecord> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						ImportRecord o = list[0];
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
		/// Insert values into import.ImportRecord. Returns an object of type ImportRecord.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type ImportRecord.</param>
		/// <returns>Object of type ImportRecord.</returns>
		public ImportRecord ImportRecordInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			ImportRecord value)
		{
			return ImportRecordInsertAuto(sqlConnection, sqlTransaction, "BHL", value);
		}
		
		/// <summary>
		/// Insert values into import.ImportRecord. Returns an object of type ImportRecord.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type ImportRecord.</param>
		/// <returns>Object of type ImportRecord.</returns>
		public ImportRecord ImportRecordInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			ImportRecord value)
		{
			return ImportRecordInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.ImportFileID,
				value.ImportRecordStatusID,
				value.Genre,
				value.Title,
				value.TranslatedTitle,
				value.JournalTitle,
				value.Volume,
				value.Series,
				value.Issue,
				value.Edition,
				value.PublicationDetails,
				value.PublisherName,
				value.PublisherPlace,
				value.Year,
				value.StartYear,
				value.EndYear,
				value.Language,
				value.Summary,
				value.Notes,
				value.Rights,
				value.DueDiligence,
				value.CopyrightStatus,
				value.License,
				value.LicenseUrl,
				value.PageRange,
				value.StartPage,
				value.EndPage,
				value.Url,
				value.DownloadUrl,
				value.DOI,
				value.ISSN,
				value.ISBN,
				value.OCLC,
				value.LCCN,
				value.CreationUserID,
				value.LastModifiedUserID,
				value.TitleID,
				value.ItemID,
				value.StartPageID,
				value.EndPageID,
				value.SegmentID);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from import.ImportRecord by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="importRecordID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool ImportRecordDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int importRecordID)
		{
			return ImportRecordDeleteAuto( sqlConnection, sqlTransaction, "BHL", importRecordID );
		}
		
		/// <summary>
		/// Delete values from import.ImportRecord by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="importRecordID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool ImportRecordDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int importRecordID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("import.ImportRecordDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("ImportRecordID", SqlDbType.Int, null, false, importRecordID), 
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
		/// Update values in import.ImportRecord. Returns an object of type ImportRecord.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="importRecordID"></param>
		/// <param name="importFileID"></param>
		/// <param name="importRecordStatusID"></param>
		/// <param name="genre"></param>
		/// <param name="title"></param>
		/// <param name="translatedTitle"></param>
		/// <param name="journalTitle"></param>
		/// <param name="volume"></param>
		/// <param name="series"></param>
		/// <param name="issue"></param>
		/// <param name="edition"></param>
		/// <param name="publicationDetails"></param>
		/// <param name="publisherName"></param>
		/// <param name="publisherPlace"></param>
		/// <param name="year"></param>
		/// <param name="startYear"></param>
		/// <param name="endYear"></param>
		/// <param name="language"></param>
		/// <param name="summary"></param>
		/// <param name="notes"></param>
		/// <param name="rights"></param>
		/// <param name="dueDiligence"></param>
		/// <param name="copyrightStatus"></param>
		/// <param name="license"></param>
		/// <param name="licenseUrl"></param>
		/// <param name="pageRange"></param>
		/// <param name="startPage"></param>
		/// <param name="endPage"></param>
		/// <param name="url"></param>
		/// <param name="downloadUrl"></param>
		/// <param name="dOI"></param>
		/// <param name="iSSN"></param>
		/// <param name="iSBN"></param>
		/// <param name="oCLC"></param>
		/// <param name="lCCN"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <param name="titleID"></param>
		/// <param name="itemID"></param>
		/// <param name="startPageID"></param>
		/// <param name="endPageID"></param>
		/// <param name="segmentID"></param>
		/// <returns>Object of type ImportRecord.</returns>
		public ImportRecord ImportRecordUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int importRecordID,
			int importFileID,
			int importRecordStatusID,
			string genre,
			string title,
			string translatedTitle,
			string journalTitle,
			string volume,
			string series,
			string issue,
			string edition,
			string publicationDetails,
			string publisherName,
			string publisherPlace,
			string year,
			short? startYear,
			short? endYear,
			string language,
			string summary,
			string notes,
			string rights,
			string dueDiligence,
			string copyrightStatus,
			string license,
			string licenseUrl,
			string pageRange,
			string startPage,
			string endPage,
			string url,
			string downloadUrl,
			string dOI,
			string iSSN,
			string iSBN,
			string oCLC,
			string lCCN,
			int lastModifiedUserID,
			int? titleID,
			int? itemID,
			int? startPageID,
			int? endPageID,
			int? segmentID)
		{
			return ImportRecordUpdateAuto( sqlConnection, sqlTransaction, "BHL", importRecordID, importFileID, importRecordStatusID, genre, title, translatedTitle, journalTitle, volume, series, issue, edition, publicationDetails, publisherName, publisherPlace, year, startYear, endYear, language, summary, notes, rights, dueDiligence, copyrightStatus, license, licenseUrl, pageRange, startPage, endPage, url, downloadUrl, dOI, iSSN, iSBN, oCLC, lCCN, lastModifiedUserID, titleID, itemID, startPageID, endPageID, segmentID);
		}
		
		/// <summary>
		/// Update values in import.ImportRecord. Returns an object of type ImportRecord.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="importRecordID"></param>
		/// <param name="importFileID"></param>
		/// <param name="importRecordStatusID"></param>
		/// <param name="genre"></param>
		/// <param name="title"></param>
		/// <param name="translatedTitle"></param>
		/// <param name="journalTitle"></param>
		/// <param name="volume"></param>
		/// <param name="series"></param>
		/// <param name="issue"></param>
		/// <param name="edition"></param>
		/// <param name="publicationDetails"></param>
		/// <param name="publisherName"></param>
		/// <param name="publisherPlace"></param>
		/// <param name="year"></param>
		/// <param name="startYear"></param>
		/// <param name="endYear"></param>
		/// <param name="language"></param>
		/// <param name="summary"></param>
		/// <param name="notes"></param>
		/// <param name="rights"></param>
		/// <param name="dueDiligence"></param>
		/// <param name="copyrightStatus"></param>
		/// <param name="license"></param>
		/// <param name="licenseUrl"></param>
		/// <param name="pageRange"></param>
		/// <param name="startPage"></param>
		/// <param name="endPage"></param>
		/// <param name="url"></param>
		/// <param name="downloadUrl"></param>
		/// <param name="dOI"></param>
		/// <param name="iSSN"></param>
		/// <param name="iSBN"></param>
		/// <param name="oCLC"></param>
		/// <param name="lCCN"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <param name="titleID"></param>
		/// <param name="itemID"></param>
		/// <param name="startPageID"></param>
		/// <param name="endPageID"></param>
		/// <param name="segmentID"></param>
		/// <returns>Object of type ImportRecord.</returns>
		public ImportRecord ImportRecordUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int importRecordID,
			int importFileID,
			int importRecordStatusID,
			string genre,
			string title,
			string translatedTitle,
			string journalTitle,
			string volume,
			string series,
			string issue,
			string edition,
			string publicationDetails,
			string publisherName,
			string publisherPlace,
			string year,
			short? startYear,
			short? endYear,
			string language,
			string summary,
			string notes,
			string rights,
			string dueDiligence,
			string copyrightStatus,
			string license,
			string licenseUrl,
			string pageRange,
			string startPage,
			string endPage,
			string url,
			string downloadUrl,
			string dOI,
			string iSSN,
			string iSBN,
			string oCLC,
			string lCCN,
			int lastModifiedUserID,
			int? titleID,
			int? itemID,
			int? startPageID,
			int? endPageID,
			int? segmentID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("import.ImportRecordUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("ImportRecordID", SqlDbType.Int, null, false, importRecordID),
					CustomSqlHelper.CreateInputParameter("ImportFileID", SqlDbType.Int, null, false, importFileID),
					CustomSqlHelper.CreateInputParameter("ImportRecordStatusID", SqlDbType.Int, null, false, importRecordStatusID),
					CustomSqlHelper.CreateInputParameter("Genre", SqlDbType.NVarChar, 50, false, genre),
					CustomSqlHelper.CreateInputParameter("Title", SqlDbType.NVarChar, 2000, false, title),
					CustomSqlHelper.CreateInputParameter("TranslatedTitle", SqlDbType.NVarChar, 2000, false, translatedTitle),
					CustomSqlHelper.CreateInputParameter("JournalTitle", SqlDbType.NVarChar, 2000, false, journalTitle),
					CustomSqlHelper.CreateInputParameter("Volume", SqlDbType.NVarChar, 100, false, volume),
					CustomSqlHelper.CreateInputParameter("Series", SqlDbType.NVarChar, 100, false, series),
					CustomSqlHelper.CreateInputParameter("Issue", SqlDbType.NVarChar, 100, false, issue),
					CustomSqlHelper.CreateInputParameter("Edition", SqlDbType.NVarChar, 400, false, edition),
					CustomSqlHelper.CreateInputParameter("PublicationDetails", SqlDbType.NVarChar, 400, false, publicationDetails),
					CustomSqlHelper.CreateInputParameter("PublisherName", SqlDbType.NVarChar, 250, false, publisherName),
					CustomSqlHelper.CreateInputParameter("PublisherPlace", SqlDbType.NVarChar, 150, false, publisherPlace),
					CustomSqlHelper.CreateInputParameter("Year", SqlDbType.NVarChar, 20, false, year),
					CustomSqlHelper.CreateInputParameter("StartYear", SqlDbType.SmallInt, null, true, startYear),
					CustomSqlHelper.CreateInputParameter("EndYear", SqlDbType.SmallInt, null, true, endYear),
					CustomSqlHelper.CreateInputParameter("Language", SqlDbType.NVarChar, 30, true, language),
					CustomSqlHelper.CreateInputParameter("Summary", SqlDbType.NVarChar, 1073741823, false, summary),
					CustomSqlHelper.CreateInputParameter("Notes", SqlDbType.NVarChar, 1073741823, false, notes),
					CustomSqlHelper.CreateInputParameter("Rights", SqlDbType.NVarChar, 1073741823, false, rights),
					CustomSqlHelper.CreateInputParameter("DueDiligence", SqlDbType.NVarChar, 1073741823, false, dueDiligence),
					CustomSqlHelper.CreateInputParameter("CopyrightStatus", SqlDbType.NVarChar, 1073741823, false, copyrightStatus),
					CustomSqlHelper.CreateInputParameter("License", SqlDbType.NVarChar, 1073741823, false, license),
					CustomSqlHelper.CreateInputParameter("LicenseUrl", SqlDbType.NVarChar, 200, false, licenseUrl),
					CustomSqlHelper.CreateInputParameter("PageRange", SqlDbType.NVarChar, 50, false, pageRange),
					CustomSqlHelper.CreateInputParameter("StartPage", SqlDbType.NVarChar, 20, false, startPage),
					CustomSqlHelper.CreateInputParameter("EndPage", SqlDbType.NVarChar, 20, false, endPage),
					CustomSqlHelper.CreateInputParameter("Url", SqlDbType.NVarChar, 200, false, url),
					CustomSqlHelper.CreateInputParameter("DownloadUrl", SqlDbType.NVarChar, 200, false, downloadUrl),
					CustomSqlHelper.CreateInputParameter("DOI", SqlDbType.NVarChar, 125, false, dOI),
					CustomSqlHelper.CreateInputParameter("ISSN", SqlDbType.NVarChar, 125, false, iSSN),
					CustomSqlHelper.CreateInputParameter("ISBN", SqlDbType.NVarChar, 125, false, iSBN),
					CustomSqlHelper.CreateInputParameter("OCLC", SqlDbType.NVarChar, 125, false, oCLC),
					CustomSqlHelper.CreateInputParameter("LCCN", SqlDbType.NVarChar, 125, false, lCCN),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, false, lastModifiedUserID),
					CustomSqlHelper.CreateInputParameter("TitleID", SqlDbType.Int, null, true, titleID),
					CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, true, itemID),
					CustomSqlHelper.CreateInputParameter("StartPageID", SqlDbType.Int, null, true, startPageID),
					CustomSqlHelper.CreateInputParameter("EndPageID", SqlDbType.Int, null, true, endPageID),
					CustomSqlHelper.CreateInputParameter("SegmentID", SqlDbType.Int, null, true, segmentID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<ImportRecord> helper = new CustomSqlHelper<ImportRecord>())
				{
					List<ImportRecord> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						ImportRecord o = list[0];
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
		/// Update values in import.ImportRecord. Returns an object of type ImportRecord.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type ImportRecord.</param>
		/// <returns>Object of type ImportRecord.</returns>
		public ImportRecord ImportRecordUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			ImportRecord value)
		{
			return ImportRecordUpdateAuto(sqlConnection, sqlTransaction, "BHL", value );
		}
		
		/// <summary>
		/// Update values in import.ImportRecord. Returns an object of type ImportRecord.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type ImportRecord.</param>
		/// <returns>Object of type ImportRecord.</returns>
		public ImportRecord ImportRecordUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			ImportRecord value)
		{
			return ImportRecordUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.ImportRecordID,
				value.ImportFileID,
				value.ImportRecordStatusID,
				value.Genre,
				value.Title,
				value.TranslatedTitle,
				value.JournalTitle,
				value.Volume,
				value.Series,
				value.Issue,
				value.Edition,
				value.PublicationDetails,
				value.PublisherName,
				value.PublisherPlace,
				value.Year,
				value.StartYear,
				value.EndYear,
				value.Language,
				value.Summary,
				value.Notes,
				value.Rights,
				value.DueDiligence,
				value.CopyrightStatus,
				value.License,
				value.LicenseUrl,
				value.PageRange,
				value.StartPage,
				value.EndPage,
				value.Url,
				value.DownloadUrl,
				value.DOI,
				value.ISSN,
				value.ISBN,
				value.OCLC,
				value.LCCN,
				value.LastModifiedUserID,
				value.TitleID,
				value.ItemID,
				value.StartPageID,
				value.EndPageID,
				value.SegmentID);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage import.ImportRecord object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in import.ImportRecord.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type ImportRecord.</param>
		/// <returns>Object of type CustomDataAccessStatus<ImportRecord>.</returns>
		public CustomDataAccessStatus<ImportRecord> ImportRecordManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			ImportRecord value , int userId )
		{
			return ImportRecordManageAuto( sqlConnection, sqlTransaction, "BHL", value , userId );
		}
		
		/// <summary>
		/// Manage import.ImportRecord object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in import.ImportRecord.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type ImportRecord.</param>
		/// <returns>Object of type CustomDataAccessStatus<ImportRecord>.</returns>
		public CustomDataAccessStatus<ImportRecord> ImportRecordManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			ImportRecord value , int userId )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				value.CreationUserID = userId;
				value.LastModifiedUserID = userId;
				ImportRecord returnValue = ImportRecordInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.ImportFileID,
						value.ImportRecordStatusID,
						value.Genre,
						value.Title,
						value.TranslatedTitle,
						value.JournalTitle,
						value.Volume,
						value.Series,
						value.Issue,
						value.Edition,
						value.PublicationDetails,
						value.PublisherName,
						value.PublisherPlace,
						value.Year,
						value.StartYear,
						value.EndYear,
						value.Language,
						value.Summary,
						value.Notes,
						value.Rights,
						value.DueDiligence,
						value.CopyrightStatus,
						value.License,
						value.LicenseUrl,
						value.PageRange,
						value.StartPage,
						value.EndPage,
						value.Url,
						value.DownloadUrl,
						value.DOI,
						value.ISSN,
						value.ISBN,
						value.OCLC,
						value.LCCN,
						value.CreationUserID,
						value.LastModifiedUserID,
						value.TitleID,
						value.ItemID,
						value.StartPageID,
						value.EndPageID,
						value.SegmentID);
				
				return new CustomDataAccessStatus<ImportRecord>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (ImportRecordDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.ImportRecordID))
				{
				return new CustomDataAccessStatus<ImportRecord>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<ImportRecord>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				value.LastModifiedUserID = userId;
				ImportRecord returnValue = ImportRecordUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.ImportRecordID,
						value.ImportFileID,
						value.ImportRecordStatusID,
						value.Genre,
						value.Title,
						value.TranslatedTitle,
						value.JournalTitle,
						value.Volume,
						value.Series,
						value.Issue,
						value.Edition,
						value.PublicationDetails,
						value.PublisherName,
						value.PublisherPlace,
						value.Year,
						value.StartYear,
						value.EndYear,
						value.Language,
						value.Summary,
						value.Notes,
						value.Rights,
						value.DueDiligence,
						value.CopyrightStatus,
						value.License,
						value.LicenseUrl,
						value.PageRange,
						value.StartPage,
						value.EndPage,
						value.Url,
						value.DownloadUrl,
						value.DOI,
						value.ISSN,
						value.ISBN,
						value.OCLC,
						value.LCCN,
						value.LastModifiedUserID,
						value.TitleID,
						value.ItemID,
						value.StartPageID,
						value.EndPageID,
						value.SegmentID);
					
				return new CustomDataAccessStatus<ImportRecord>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<ImportRecord>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}

