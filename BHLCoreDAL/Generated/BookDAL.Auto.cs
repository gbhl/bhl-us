
// Generated 1/5/2021 3:25:05 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class BookDAL is based upon dbo.Book.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DAL
// {
// 		public partial class BookDAL
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
	partial class BookDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from dbo.Book by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="bookID"></param>
		/// <returns>Object of type Book.</returns>
		public Book BookSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int bookID)
		{
			return BookSelectAuto(	sqlConnection, sqlTransaction, "BHL",	bookID );
		}
			
		/// <summary>
		/// Select values from dbo.Book by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="bookID"></param>
		/// <returns>Object of type Book.</returns>
		public Book BookSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int bookID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("BookSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("BookID", SqlDbType.Int, null, false, bookID)))
			{
				using (CustomSqlHelper<Book> helper = new CustomSqlHelper<Book>())
				{
					List<Book> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						Book o = list[0];
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
		/// Select values from dbo.Book by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="bookID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> BookSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int bookID)
		{
			return BookSelectAutoRaw( sqlConnection, sqlTransaction, "BHL", bookID );
		}
		
		/// <summary>
		/// Select values from dbo.Book by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="bookID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> BookSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int bookID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("BookSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("BookID", SqlDbType.Int, null, false, bookID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====

 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into dbo.Book.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="itemID"></param>
		/// <param name="redirectBookID"></param>
		/// <param name="thumbnailPageID"></param>
		/// <param name="languageCode"></param>
		/// <param name="barCode"></param>
		/// <param name="mARCItemID"></param>
		/// <param name="callNumber"></param>
		/// <param name="volume"></param>
		/// <param name="startYear"></param>
		/// <param name="endYear"></param>
		/// <param name="startVolume"></param>
		/// <param name="endVolume"></param>
		/// <param name="startIssue"></param>
		/// <param name="endIssue"></param>
		/// <param name="startNumber"></param>
		/// <param name="endNumber"></param>
		/// <param name="startSeries"></param>
		/// <param name="endSeries"></param>
		/// <param name="startPart"></param>
		/// <param name="endPart"></param>
		/// <param name="identifierBib"></param>
		/// <param name="zQuery"></param>
		/// <param name="sponsor"></param>
		/// <param name="externalUrl"></param>
		/// <param name="licenseUrl"></param>
		/// <param name="rights"></param>
		/// <param name="dueDiligence"></param>
		/// <param name="copyrightStatus"></param>
		/// <param name="copyrightRegion"></param>
		/// <param name="copyrightComment"></param>
		/// <param name="copyrightEvidence"></param>
		/// <param name="scanningUser"></param>
		/// <param name="scanningDate"></param>
		/// <param name="paginationStatusID"></param>
		/// <param name="paginationStatusDate"></param>
		/// <param name="paginationStatusUserID"></param>
		/// <param name="paginationCompleteDate"></param>
		/// <param name="paginationCompleteUserID"></param>
		/// <param name="lastPageNameLookupDate"></param>
		/// <param name="isVirtual"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type Book.</returns>
		public Book BookInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int itemID,
			int? redirectBookID,
			int? thumbnailPageID,
			string languageCode,
			string barCode,
			string mARCItemID,
			string callNumber,
			string volume,
			string startYear,
			string endYear,
			string startVolume,
			string endVolume,
			string startIssue,
			string endIssue,
			string startNumber,
			string endNumber,
			string startSeries,
			string endSeries,
			string startPart,
			string endPart,
			string identifierBib,
			string zQuery,
			string sponsor,
			string externalUrl,
			string licenseUrl,
			string rights,
			string dueDiligence,
			string copyrightStatus,
			string copyrightRegion,
			string copyrightComment,
			string copyrightEvidence,
			string scanningUser,
			DateTime? scanningDate,
			int? paginationStatusID,
			DateTime? paginationStatusDate,
			int? paginationStatusUserID,
			DateTime? paginationCompleteDate,
			int? paginationCompleteUserID,
			DateTime? lastPageNameLookupDate,
			byte isVirtual,
			int? creationUserID,
			int? lastModifiedUserID)
		{
			return BookInsertAuto( sqlConnection, sqlTransaction, "BHL", itemID, redirectBookID, thumbnailPageID, languageCode, barCode, mARCItemID, callNumber, volume, startYear, endYear, startVolume, endVolume, startIssue, endIssue, startNumber, endNumber, startSeries, endSeries, startPart, endPart, identifierBib, zQuery, sponsor, externalUrl, licenseUrl, rights, dueDiligence, copyrightStatus, copyrightRegion, copyrightComment, copyrightEvidence, scanningUser, scanningDate, paginationStatusID, paginationStatusDate, paginationStatusUserID, paginationCompleteDate, paginationCompleteUserID, lastPageNameLookupDate, isVirtual, creationUserID, lastModifiedUserID );
		}
		
		/// <summary>
		/// Insert values into dbo.Book.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="itemID"></param>
		/// <param name="redirectBookID"></param>
		/// <param name="thumbnailPageID"></param>
		/// <param name="languageCode"></param>
		/// <param name="barCode"></param>
		/// <param name="mARCItemID"></param>
		/// <param name="callNumber"></param>
		/// <param name="volume"></param>
		/// <param name="startYear"></param>
		/// <param name="endYear"></param>
		/// <param name="startVolume"></param>
		/// <param name="endVolume"></param>
		/// <param name="startIssue"></param>
		/// <param name="endIssue"></param>
		/// <param name="startNumber"></param>
		/// <param name="endNumber"></param>
		/// <param name="startSeries"></param>
		/// <param name="endSeries"></param>
		/// <param name="startPart"></param>
		/// <param name="endPart"></param>
		/// <param name="identifierBib"></param>
		/// <param name="zQuery"></param>
		/// <param name="sponsor"></param>
		/// <param name="externalUrl"></param>
		/// <param name="licenseUrl"></param>
		/// <param name="rights"></param>
		/// <param name="dueDiligence"></param>
		/// <param name="copyrightStatus"></param>
		/// <param name="copyrightRegion"></param>
		/// <param name="copyrightComment"></param>
		/// <param name="copyrightEvidence"></param>
		/// <param name="scanningUser"></param>
		/// <param name="scanningDate"></param>
		/// <param name="paginationStatusID"></param>
		/// <param name="paginationStatusDate"></param>
		/// <param name="paginationStatusUserID"></param>
		/// <param name="paginationCompleteDate"></param>
		/// <param name="paginationCompleteUserID"></param>
		/// <param name="lastPageNameLookupDate"></param>
		/// <param name="isVirtual"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type Book.</returns>
		public Book BookInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int itemID,
			int? redirectBookID,
			int? thumbnailPageID,
			string languageCode,
			string barCode,
			string mARCItemID,
			string callNumber,
			string volume,
			string startYear,
			string endYear,
			string startVolume,
			string endVolume,
			string startIssue,
			string endIssue,
			string startNumber,
			string endNumber,
			string startSeries,
			string endSeries,
			string startPart,
			string endPart,
			string identifierBib,
			string zQuery,
			string sponsor,
			string externalUrl,
			string licenseUrl,
			string rights,
			string dueDiligence,
			string copyrightStatus,
			string copyrightRegion,
			string copyrightComment,
			string copyrightEvidence,
			string scanningUser,
			DateTime? scanningDate,
			int? paginationStatusID,
			DateTime? paginationStatusDate,
			int? paginationStatusUserID,
			DateTime? paginationCompleteDate,
			int? paginationCompleteUserID,
			DateTime? lastPageNameLookupDate,
			byte isVirtual,
			int? creationUserID,
			int? lastModifiedUserID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("BookInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("BookID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemID),
					CustomSqlHelper.CreateInputParameter("RedirectBookID", SqlDbType.Int, null, true, redirectBookID),
					CustomSqlHelper.CreateInputParameter("ThumbnailPageID", SqlDbType.Int, null, true, thumbnailPageID),
					CustomSqlHelper.CreateInputParameter("LanguageCode", SqlDbType.NVarChar, 10, true, languageCode),
					CustomSqlHelper.CreateInputParameter("BarCode", SqlDbType.NVarChar, 200, false, barCode),
					CustomSqlHelper.CreateInputParameter("MARCItemID", SqlDbType.NVarChar, 50, true, mARCItemID),
					CustomSqlHelper.CreateInputParameter("CallNumber", SqlDbType.NVarChar, 100, true, callNumber),
					CustomSqlHelper.CreateInputParameter("Volume", SqlDbType.NVarChar, 100, true, volume),
					CustomSqlHelper.CreateInputParameter("StartYear", SqlDbType.NVarChar, 20, true, startYear),
					CustomSqlHelper.CreateInputParameter("EndYear", SqlDbType.NVarChar, 20, false, endYear),
					CustomSqlHelper.CreateInputParameter("StartVolume", SqlDbType.NVarChar, 10, false, startVolume),
					CustomSqlHelper.CreateInputParameter("EndVolume", SqlDbType.NVarChar, 10, false, endVolume),
					CustomSqlHelper.CreateInputParameter("StartIssue", SqlDbType.NVarChar, 10, false, startIssue),
					CustomSqlHelper.CreateInputParameter("EndIssue", SqlDbType.NVarChar, 10, false, endIssue),
					CustomSqlHelper.CreateInputParameter("StartNumber", SqlDbType.NVarChar, 10, false, startNumber),
					CustomSqlHelper.CreateInputParameter("EndNumber", SqlDbType.NVarChar, 10, false, endNumber),
					CustomSqlHelper.CreateInputParameter("StartSeries", SqlDbType.NVarChar, 10, false, startSeries),
					CustomSqlHelper.CreateInputParameter("EndSeries", SqlDbType.NVarChar, 10, false, endSeries),
					CustomSqlHelper.CreateInputParameter("StartPart", SqlDbType.NVarChar, 10, false, startPart),
					CustomSqlHelper.CreateInputParameter("EndPart", SqlDbType.NVarChar, 10, false, endPart),
					CustomSqlHelper.CreateInputParameter("IdentifierBib", SqlDbType.NVarChar, 50, true, identifierBib),
					CustomSqlHelper.CreateInputParameter("ZQuery", SqlDbType.NVarChar, 200, true, zQuery),
					CustomSqlHelper.CreateInputParameter("Sponsor", SqlDbType.NVarChar, 100, true, sponsor),
					CustomSqlHelper.CreateInputParameter("ExternalUrl", SqlDbType.NVarChar, 500, true, externalUrl),
					CustomSqlHelper.CreateInputParameter("LicenseUrl", SqlDbType.NVarChar, 1073741823, true, licenseUrl),
					CustomSqlHelper.CreateInputParameter("Rights", SqlDbType.NVarChar, 1073741823, true, rights),
					CustomSqlHelper.CreateInputParameter("DueDiligence", SqlDbType.NVarChar, 1073741823, true, dueDiligence),
					CustomSqlHelper.CreateInputParameter("CopyrightStatus", SqlDbType.NVarChar, 1073741823, true, copyrightStatus),
					CustomSqlHelper.CreateInputParameter("CopyrightRegion", SqlDbType.NVarChar, 50, true, copyrightRegion),
					CustomSqlHelper.CreateInputParameter("CopyrightComment", SqlDbType.NVarChar, 1073741823, true, copyrightComment),
					CustomSqlHelper.CreateInputParameter("CopyrightEvidence", SqlDbType.NVarChar, 1073741823, true, copyrightEvidence),
					CustomSqlHelper.CreateInputParameter("ScanningUser", SqlDbType.NVarChar, 100, true, scanningUser),
					CustomSqlHelper.CreateInputParameter("ScanningDate", SqlDbType.DateTime, null, true, scanningDate),
					CustomSqlHelper.CreateInputParameter("PaginationStatusID", SqlDbType.Int, null, true, paginationStatusID),
					CustomSqlHelper.CreateInputParameter("PaginationStatusDate", SqlDbType.DateTime, null, true, paginationStatusDate),
					CustomSqlHelper.CreateInputParameter("PaginationStatusUserID", SqlDbType.Int, null, true, paginationStatusUserID),
					CustomSqlHelper.CreateInputParameter("PaginationCompleteDate", SqlDbType.DateTime, null, true, paginationCompleteDate),
					CustomSqlHelper.CreateInputParameter("PaginationCompleteUserID", SqlDbType.Int, null, true, paginationCompleteUserID),
					CustomSqlHelper.CreateInputParameter("LastPageNameLookupDate", SqlDbType.DateTime, null, true, lastPageNameLookupDate),
					CustomSqlHelper.CreateInputParameter("IsVirtual", SqlDbType.TinyInt, null, false, isVirtual),
					CustomSqlHelper.CreateInputParameter("CreationUserID", SqlDbType.Int, null, true, creationUserID),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, true, lastModifiedUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<Book> helper = new CustomSqlHelper<Book>())
				{
					List<Book> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						Book o = list[0];
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
		/// Insert values into dbo.Book. Returns an object of type Book.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type Book.</param>
		/// <returns>Object of type Book.</returns>
		public Book BookInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			Book value)
		{
			return BookInsertAuto(sqlConnection, sqlTransaction, "BHL", value);
		}
		
		/// <summary>
		/// Insert values into dbo.Book. Returns an object of type Book.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type Book.</param>
		/// <returns>Object of type Book.</returns>
		public Book BookInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			Book value)
		{
			return BookInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.ItemID,
				value.RedirectBookID,
				value.ThumbnailPageID,
				value.LanguageCode,
				value.BarCode,
				value.MARCItemID,
				value.CallNumber,
				value.Volume,
				value.StartYear,
				value.EndYear,
				value.StartVolume,
				value.EndVolume,
				value.StartIssue,
				value.EndIssue,
				value.StartNumber,
				value.EndNumber,
				value.StartSeries,
				value.EndSeries,
				value.StartPart,
				value.EndPart,
				value.IdentifierBib,
				value.ZQuery,
				value.Sponsor,
				value.ExternalUrl,
				value.LicenseUrl,
				value.Rights,
				value.DueDiligence,
				value.CopyrightStatus,
				value.CopyrightRegion,
				value.CopyrightComment,
				value.CopyrightEvidence,
				value.ScanningUser,
				value.ScanningDate,
				value.PaginationStatusID,
				value.PaginationStatusDate,
				value.PaginationStatusUserID,
				value.PaginationCompleteDate,
				value.PaginationCompleteUserID,
				value.LastPageNameLookupDate,
				value.IsVirtual,
				value.CreationUserID,
				value.LastModifiedUserID);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from dbo.Book by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="bookID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool BookDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int bookID)
		{
			return BookDeleteAuto( sqlConnection, sqlTransaction, "BHL", bookID );
		}
		
		/// <summary>
		/// Delete values from dbo.Book by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="bookID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool BookDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int bookID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("BookDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("BookID", SqlDbType.Int, null, false, bookID), 
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
		/// Update values in dbo.Book. Returns an object of type Book.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="bookID"></param>
		/// <param name="itemID"></param>
		/// <param name="redirectBookID"></param>
		/// <param name="thumbnailPageID"></param>
		/// <param name="languageCode"></param>
		/// <param name="barCode"></param>
		/// <param name="mARCItemID"></param>
		/// <param name="callNumber"></param>
		/// <param name="volume"></param>
		/// <param name="startYear"></param>
		/// <param name="endYear"></param>
		/// <param name="startVolume"></param>
		/// <param name="endVolume"></param>
		/// <param name="startIssue"></param>
		/// <param name="endIssue"></param>
		/// <param name="startNumber"></param>
		/// <param name="endNumber"></param>
		/// <param name="startSeries"></param>
		/// <param name="endSeries"></param>
		/// <param name="startPart"></param>
		/// <param name="endPart"></param>
		/// <param name="identifierBib"></param>
		/// <param name="zQuery"></param>
		/// <param name="sponsor"></param>
		/// <param name="externalUrl"></param>
		/// <param name="licenseUrl"></param>
		/// <param name="rights"></param>
		/// <param name="dueDiligence"></param>
		/// <param name="copyrightStatus"></param>
		/// <param name="copyrightRegion"></param>
		/// <param name="copyrightComment"></param>
		/// <param name="copyrightEvidence"></param>
		/// <param name="scanningUser"></param>
		/// <param name="scanningDate"></param>
		/// <param name="paginationStatusID"></param>
		/// <param name="paginationStatusDate"></param>
		/// <param name="paginationStatusUserID"></param>
		/// <param name="paginationCompleteDate"></param>
		/// <param name="paginationCompleteUserID"></param>
		/// <param name="lastPageNameLookupDate"></param>
		/// <param name="isVirtual"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type Book.</returns>
		public Book BookUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int bookID,
			int itemID,
			int? redirectBookID,
			int? thumbnailPageID,
			string languageCode,
			string barCode,
			string mARCItemID,
			string callNumber,
			string volume,
			string startYear,
			string endYear,
			string startVolume,
			string endVolume,
			string startIssue,
			string endIssue,
			string startNumber,
			string endNumber,
			string startSeries,
			string endSeries,
			string startPart,
			string endPart,
			string identifierBib,
			string zQuery,
			string sponsor,
			string externalUrl,
			string licenseUrl,
			string rights,
			string dueDiligence,
			string copyrightStatus,
			string copyrightRegion,
			string copyrightComment,
			string copyrightEvidence,
			string scanningUser,
			DateTime? scanningDate,
			int? paginationStatusID,
			DateTime? paginationStatusDate,
			int? paginationStatusUserID,
			DateTime? paginationCompleteDate,
			int? paginationCompleteUserID,
			DateTime? lastPageNameLookupDate,
			byte isVirtual,
			int? lastModifiedUserID)
		{
			return BookUpdateAuto( sqlConnection, sqlTransaction, "BHL", bookID, itemID, redirectBookID, thumbnailPageID, languageCode, barCode, mARCItemID, callNumber, volume, startYear, endYear, startVolume, endVolume, startIssue, endIssue, startNumber, endNumber, startSeries, endSeries, startPart, endPart, identifierBib, zQuery, sponsor, externalUrl, licenseUrl, rights, dueDiligence, copyrightStatus, copyrightRegion, copyrightComment, copyrightEvidence, scanningUser, scanningDate, paginationStatusID, paginationStatusDate, paginationStatusUserID, paginationCompleteDate, paginationCompleteUserID, lastPageNameLookupDate, isVirtual, lastModifiedUserID);
		}
		
		/// <summary>
		/// Update values in dbo.Book. Returns an object of type Book.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="bookID"></param>
		/// <param name="itemID"></param>
		/// <param name="redirectBookID"></param>
		/// <param name="thumbnailPageID"></param>
		/// <param name="languageCode"></param>
		/// <param name="barCode"></param>
		/// <param name="mARCItemID"></param>
		/// <param name="callNumber"></param>
		/// <param name="volume"></param>
		/// <param name="startYear"></param>
		/// <param name="endYear"></param>
		/// <param name="startVolume"></param>
		/// <param name="endVolume"></param>
		/// <param name="startIssue"></param>
		/// <param name="endIssue"></param>
		/// <param name="startNumber"></param>
		/// <param name="endNumber"></param>
		/// <param name="startSeries"></param>
		/// <param name="endSeries"></param>
		/// <param name="startPart"></param>
		/// <param name="endPart"></param>
		/// <param name="identifierBib"></param>
		/// <param name="zQuery"></param>
		/// <param name="sponsor"></param>
		/// <param name="externalUrl"></param>
		/// <param name="licenseUrl"></param>
		/// <param name="rights"></param>
		/// <param name="dueDiligence"></param>
		/// <param name="copyrightStatus"></param>
		/// <param name="copyrightRegion"></param>
		/// <param name="copyrightComment"></param>
		/// <param name="copyrightEvidence"></param>
		/// <param name="scanningUser"></param>
		/// <param name="scanningDate"></param>
		/// <param name="paginationStatusID"></param>
		/// <param name="paginationStatusDate"></param>
		/// <param name="paginationStatusUserID"></param>
		/// <param name="paginationCompleteDate"></param>
		/// <param name="paginationCompleteUserID"></param>
		/// <param name="lastPageNameLookupDate"></param>
		/// <param name="isVirtual"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <returns>Object of type Book.</returns>
		public Book BookUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int bookID,
			int itemID,
			int? redirectBookID,
			int? thumbnailPageID,
			string languageCode,
			string barCode,
			string mARCItemID,
			string callNumber,
			string volume,
			string startYear,
			string endYear,
			string startVolume,
			string endVolume,
			string startIssue,
			string endIssue,
			string startNumber,
			string endNumber,
			string startSeries,
			string endSeries,
			string startPart,
			string endPart,
			string identifierBib,
			string zQuery,
			string sponsor,
			string externalUrl,
			string licenseUrl,
			string rights,
			string dueDiligence,
			string copyrightStatus,
			string copyrightRegion,
			string copyrightComment,
			string copyrightEvidence,
			string scanningUser,
			DateTime? scanningDate,
			int? paginationStatusID,
			DateTime? paginationStatusDate,
			int? paginationStatusUserID,
			DateTime? paginationCompleteDate,
			int? paginationCompleteUserID,
			DateTime? lastPageNameLookupDate,
			byte isVirtual,
			int? lastModifiedUserID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("BookUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("BookID", SqlDbType.Int, null, false, bookID),
					CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemID),
					CustomSqlHelper.CreateInputParameter("RedirectBookID", SqlDbType.Int, null, true, redirectBookID),
					CustomSqlHelper.CreateInputParameter("ThumbnailPageID", SqlDbType.Int, null, true, thumbnailPageID),
					CustomSqlHelper.CreateInputParameter("LanguageCode", SqlDbType.NVarChar, 10, true, languageCode),
					CustomSqlHelper.CreateInputParameter("BarCode", SqlDbType.NVarChar, 200, false, barCode),
					CustomSqlHelper.CreateInputParameter("MARCItemID", SqlDbType.NVarChar, 50, true, mARCItemID),
					CustomSqlHelper.CreateInputParameter("CallNumber", SqlDbType.NVarChar, 100, true, callNumber),
					CustomSqlHelper.CreateInputParameter("Volume", SqlDbType.NVarChar, 100, true, volume),
					CustomSqlHelper.CreateInputParameter("StartYear", SqlDbType.NVarChar, 20, true, startYear),
					CustomSqlHelper.CreateInputParameter("EndYear", SqlDbType.NVarChar, 20, false, endYear),
					CustomSqlHelper.CreateInputParameter("StartVolume", SqlDbType.NVarChar, 10, false, startVolume),
					CustomSqlHelper.CreateInputParameter("EndVolume", SqlDbType.NVarChar, 10, false, endVolume),
					CustomSqlHelper.CreateInputParameter("StartIssue", SqlDbType.NVarChar, 10, false, startIssue),
					CustomSqlHelper.CreateInputParameter("EndIssue", SqlDbType.NVarChar, 10, false, endIssue),
					CustomSqlHelper.CreateInputParameter("StartNumber", SqlDbType.NVarChar, 10, false, startNumber),
					CustomSqlHelper.CreateInputParameter("EndNumber", SqlDbType.NVarChar, 10, false, endNumber),
					CustomSqlHelper.CreateInputParameter("StartSeries", SqlDbType.NVarChar, 10, false, startSeries),
					CustomSqlHelper.CreateInputParameter("EndSeries", SqlDbType.NVarChar, 10, false, endSeries),
					CustomSqlHelper.CreateInputParameter("StartPart", SqlDbType.NVarChar, 10, false, startPart),
					CustomSqlHelper.CreateInputParameter("EndPart", SqlDbType.NVarChar, 10, false, endPart),
					CustomSqlHelper.CreateInputParameter("IdentifierBib", SqlDbType.NVarChar, 50, true, identifierBib),
					CustomSqlHelper.CreateInputParameter("ZQuery", SqlDbType.NVarChar, 200, true, zQuery),
					CustomSqlHelper.CreateInputParameter("Sponsor", SqlDbType.NVarChar, 100, true, sponsor),
					CustomSqlHelper.CreateInputParameter("ExternalUrl", SqlDbType.NVarChar, 500, true, externalUrl),
					CustomSqlHelper.CreateInputParameter("LicenseUrl", SqlDbType.NVarChar, 1073741823, true, licenseUrl),
					CustomSqlHelper.CreateInputParameter("Rights", SqlDbType.NVarChar, 1073741823, true, rights),
					CustomSqlHelper.CreateInputParameter("DueDiligence", SqlDbType.NVarChar, 1073741823, true, dueDiligence),
					CustomSqlHelper.CreateInputParameter("CopyrightStatus", SqlDbType.NVarChar, 1073741823, true, copyrightStatus),
					CustomSqlHelper.CreateInputParameter("CopyrightRegion", SqlDbType.NVarChar, 50, true, copyrightRegion),
					CustomSqlHelper.CreateInputParameter("CopyrightComment", SqlDbType.NVarChar, 1073741823, true, copyrightComment),
					CustomSqlHelper.CreateInputParameter("CopyrightEvidence", SqlDbType.NVarChar, 1073741823, true, copyrightEvidence),
					CustomSqlHelper.CreateInputParameter("ScanningUser", SqlDbType.NVarChar, 100, true, scanningUser),
					CustomSqlHelper.CreateInputParameter("ScanningDate", SqlDbType.DateTime, null, true, scanningDate),
					CustomSqlHelper.CreateInputParameter("PaginationStatusID", SqlDbType.Int, null, true, paginationStatusID),
					CustomSqlHelper.CreateInputParameter("PaginationStatusDate", SqlDbType.DateTime, null, true, paginationStatusDate),
					CustomSqlHelper.CreateInputParameter("PaginationStatusUserID", SqlDbType.Int, null, true, paginationStatusUserID),
					CustomSqlHelper.CreateInputParameter("PaginationCompleteDate", SqlDbType.DateTime, null, true, paginationCompleteDate),
					CustomSqlHelper.CreateInputParameter("PaginationCompleteUserID", SqlDbType.Int, null, true, paginationCompleteUserID),
					CustomSqlHelper.CreateInputParameter("LastPageNameLookupDate", SqlDbType.DateTime, null, true, lastPageNameLookupDate),
					CustomSqlHelper.CreateInputParameter("IsVirtual", SqlDbType.TinyInt, null, false, isVirtual),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, true, lastModifiedUserID), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<Book> helper = new CustomSqlHelper<Book>())
				{
					List<Book> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						Book o = list[0];
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
		/// Update values in dbo.Book. Returns an object of type Book.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type Book.</param>
		/// <returns>Object of type Book.</returns>
		public Book BookUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			Book value)
		{
			return BookUpdateAuto(sqlConnection, sqlTransaction, "BHL", value );
		}
		
		/// <summary>
		/// Update values in dbo.Book. Returns an object of type Book.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type Book.</param>
		/// <returns>Object of type Book.</returns>
		public Book BookUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			Book value)
		{
			return BookUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.BookID,
				value.ItemID,
				value.RedirectBookID,
				value.ThumbnailPageID,
				value.LanguageCode,
				value.BarCode,
				value.MARCItemID,
				value.CallNumber,
				value.Volume,
				value.StartYear,
				value.EndYear,
				value.StartVolume,
				value.EndVolume,
				value.StartIssue,
				value.EndIssue,
				value.StartNumber,
				value.EndNumber,
				value.StartSeries,
				value.EndSeries,
				value.StartPart,
				value.EndPart,
				value.IdentifierBib,
				value.ZQuery,
				value.Sponsor,
				value.ExternalUrl,
				value.LicenseUrl,
				value.Rights,
				value.DueDiligence,
				value.CopyrightStatus,
				value.CopyrightRegion,
				value.CopyrightComment,
				value.CopyrightEvidence,
				value.ScanningUser,
				value.ScanningDate,
				value.PaginationStatusID,
				value.PaginationStatusDate,
				value.PaginationStatusUserID,
				value.PaginationCompleteDate,
				value.PaginationCompleteUserID,
				value.LastPageNameLookupDate,
				value.IsVirtual,
				value.LastModifiedUserID);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage dbo.Book object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.Book.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type Book.</param>
		/// <returns>Object of type CustomDataAccessStatus<Book>.</returns>
		public CustomDataAccessStatus<Book> BookManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			Book value , int userId )
		{
			return BookManageAuto( sqlConnection, sqlTransaction, "BHL", value , userId );
		}
		
		/// <summary>
		/// Manage dbo.Book object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.Book.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type Book.</param>
		/// <returns>Object of type CustomDataAccessStatus<Book>.</returns>
		public CustomDataAccessStatus<Book> BookManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			Book value , int userId )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				value.CreationUserID = userId;
				value.LastModifiedUserID = userId;
				Book returnValue = BookInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.ItemID,
						value.RedirectBookID,
						value.ThumbnailPageID,
						value.LanguageCode,
						value.BarCode,
						value.MARCItemID,
						value.CallNumber,
						value.Volume,
						value.StartYear,
						value.EndYear,
						value.StartVolume,
						value.EndVolume,
						value.StartIssue,
						value.EndIssue,
						value.StartNumber,
						value.EndNumber,
						value.StartSeries,
						value.EndSeries,
						value.StartPart,
						value.EndPart,
						value.IdentifierBib,
						value.ZQuery,
						value.Sponsor,
						value.ExternalUrl,
						value.LicenseUrl,
						value.Rights,
						value.DueDiligence,
						value.CopyrightStatus,
						value.CopyrightRegion,
						value.CopyrightComment,
						value.CopyrightEvidence,
						value.ScanningUser,
						value.ScanningDate,
						value.PaginationStatusID,
						value.PaginationStatusDate,
						value.PaginationStatusUserID,
						value.PaginationCompleteDate,
						value.PaginationCompleteUserID,
						value.LastPageNameLookupDate,
						value.IsVirtual,
						value.CreationUserID,
						value.LastModifiedUserID);
				
				return new CustomDataAccessStatus<Book>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (BookDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.BookID))
				{
				return new CustomDataAccessStatus<Book>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<Book>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				value.LastModifiedUserID = userId;
				Book returnValue = BookUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.BookID,
						value.ItemID,
						value.RedirectBookID,
						value.ThumbnailPageID,
						value.LanguageCode,
						value.BarCode,
						value.MARCItemID,
						value.CallNumber,
						value.Volume,
						value.StartYear,
						value.EndYear,
						value.StartVolume,
						value.EndVolume,
						value.StartIssue,
						value.EndIssue,
						value.StartNumber,
						value.EndNumber,
						value.StartSeries,
						value.EndSeries,
						value.StartPart,
						value.EndPart,
						value.IdentifierBib,
						value.ZQuery,
						value.Sponsor,
						value.ExternalUrl,
						value.LicenseUrl,
						value.Rights,
						value.DueDiligence,
						value.CopyrightStatus,
						value.CopyrightRegion,
						value.CopyrightComment,
						value.CopyrightEvidence,
						value.ScanningUser,
						value.ScanningDate,
						value.PaginationStatusID,
						value.PaginationStatusDate,
						value.PaginationStatusUserID,
						value.PaginationCompleteDate,
						value.PaginationCompleteUserID,
						value.LastPageNameLookupDate,
						value.IsVirtual,
						value.LastModifiedUserID);
					
				return new CustomDataAccessStatus<Book>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<Book>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}

