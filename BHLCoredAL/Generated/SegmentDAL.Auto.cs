
// Generated 4/27/2021 1:21:19 PM
// Do not modify the contents of this code file.
// This is part of a data access layer. 
// This partial class SegmentDAL is based upon dbo.Segment.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// The code file you create must be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DAL
// {
// 		public partial class SegmentDAL
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
	partial class SegmentDAL 
	{
 		#region ===== SELECT =====

		/// <summary>
		/// Select values from dbo.Segment by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="segmentID"></param>
		/// <returns>Object of type Segment.</returns>
		public Segment SegmentSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int segmentID)
		{
			return SegmentSelectAuto(	sqlConnection, sqlTransaction, "BHL",	segmentID );
		}
			
		/// <summary>
		/// Select values from dbo.Segment by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="segmentID"></param>
		/// <returns>Object of type Segment.</returns>
		public Segment SegmentSelectAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int segmentID )
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings( connectionKeyName ), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("SegmentSelectAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("SegmentID", SqlDbType.Int, null, false, segmentID)))
			{
				using (CustomSqlHelper<Segment> helper = new CustomSqlHelper<Segment>())
				{
					List<Segment> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						Segment o = list[0];
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
		/// Select values from dbo.Segment by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="segmentID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> SegmentSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int segmentID)
		{
			return SegmentSelectAutoRaw( sqlConnection, sqlTransaction, "BHL", segmentID );
		}
		
		/// <summary>
		/// Select values from dbo.Segment by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="segmentID"></param>
		/// <returns>List&lt;CustomDataRow&gt;</returns>
		public List<CustomDataRow> SegmentSelectAutoRaw(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int segmentID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("SegmentSelectAuto", connection, transaction,
				CustomSqlHelper.CreateInputParameter("SegmentID", SqlDbType.Int, null, false, segmentID)))
			{
				return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
			}
		}
		
		#endregion ===== SELECT =====

 		#region ===== INSERT =====

		/// <summary>
		/// Insert values into dbo.Segment.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="itemID"></param>
		/// <param name="redirectSegmentID"></param>
		/// <param name="segmentGenreID"></param>
		/// <param name="startPageID"></param>
		/// <param name="thumbnailPageID"></param>
		/// <param name="languageCode"></param>
		/// <param name="barCode"></param>
		/// <param name="mARCItemID"></param>
		/// <param name="title"></param>
		/// <param name="sortTitle"></param>
		/// <param name="translatedTitle"></param>
		/// <param name="containerTitle"></param>
		/// <param name="publicationDetails"></param>
		/// <param name="publisherName"></param>
		/// <param name="publisherPlace"></param>
		/// <param name="summary"></param>
		/// <param name="volume"></param>
		/// <param name="series"></param>
		/// <param name="issue"></param>
		/// <param name="edition"></param>
		/// <param name="date"></param>
		/// <param name="pageRange"></param>
		/// <param name="startPageNumber"></param>
		/// <param name="endPageNumber"></param>
		/// <param name="url"></param>
		/// <param name="downloadUrl"></param>
		/// <param name="licenseName"></param>
		/// <param name="licenseUrl"></param>
		/// <param name="rightsStatus"></param>
		/// <param name="rightsStatement"></param>
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
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <param name="pageProgression"></param>
		/// <returns>Object of type Segment.</returns>
		public Segment SegmentInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int itemID,
			int? redirectSegmentID,
			int segmentGenreID,
			int? startPageID,
			int? thumbnailPageID,
			string languageCode,
			string barCode,
			string mARCItemID,
			string title,
			string sortTitle,
			string translatedTitle,
			string containerTitle,
			string publicationDetails,
			string publisherName,
			string publisherPlace,
			string summary,
			string volume,
			string series,
			string issue,
			string edition,
			string date,
			string pageRange,
			string startPageNumber,
			string endPageNumber,
			string url,
			string downloadUrl,
			string licenseName,
			string licenseUrl,
			string rightsStatus,
			string rightsStatement,
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
			int? creationUserID,
			int? lastModifiedUserID,
			string pageProgression)
		{
			return SegmentInsertAuto( sqlConnection, sqlTransaction, "BHL", itemID, redirectSegmentID, segmentGenreID, startPageID, thumbnailPageID, languageCode, barCode, mARCItemID, title, sortTitle, translatedTitle, containerTitle, publicationDetails, publisherName, publisherPlace, summary, volume, series, issue, edition, date, pageRange, startPageNumber, endPageNumber, url, downloadUrl, licenseName, licenseUrl, rightsStatus, rightsStatement, copyrightStatus, copyrightRegion, copyrightComment, copyrightEvidence, scanningUser, scanningDate, paginationStatusID, paginationStatusDate, paginationStatusUserID, paginationCompleteDate, paginationCompleteUserID, lastPageNameLookupDate, creationUserID, lastModifiedUserID, pageProgression );
		}
		
		/// <summary>
		/// Insert values into dbo.Segment.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="itemID"></param>
		/// <param name="redirectSegmentID"></param>
		/// <param name="segmentGenreID"></param>
		/// <param name="startPageID"></param>
		/// <param name="thumbnailPageID"></param>
		/// <param name="languageCode"></param>
		/// <param name="barCode"></param>
		/// <param name="mARCItemID"></param>
		/// <param name="title"></param>
		/// <param name="sortTitle"></param>
		/// <param name="translatedTitle"></param>
		/// <param name="containerTitle"></param>
		/// <param name="publicationDetails"></param>
		/// <param name="publisherName"></param>
		/// <param name="publisherPlace"></param>
		/// <param name="summary"></param>
		/// <param name="volume"></param>
		/// <param name="series"></param>
		/// <param name="issue"></param>
		/// <param name="edition"></param>
		/// <param name="date"></param>
		/// <param name="pageRange"></param>
		/// <param name="startPageNumber"></param>
		/// <param name="endPageNumber"></param>
		/// <param name="url"></param>
		/// <param name="downloadUrl"></param>
		/// <param name="licenseName"></param>
		/// <param name="licenseUrl"></param>
		/// <param name="rightsStatus"></param>
		/// <param name="rightsStatement"></param>
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
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <param name="pageProgression"></param>
		/// <returns>Object of type Segment.</returns>
		public Segment SegmentInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int itemID,
			int? redirectSegmentID,
			int segmentGenreID,
			int? startPageID,
			int? thumbnailPageID,
			string languageCode,
			string barCode,
			string mARCItemID,
			string title,
			string sortTitle,
			string translatedTitle,
			string containerTitle,
			string publicationDetails,
			string publisherName,
			string publisherPlace,
			string summary,
			string volume,
			string series,
			string issue,
			string edition,
			string date,
			string pageRange,
			string startPageNumber,
			string endPageNumber,
			string url,
			string downloadUrl,
			string licenseName,
			string licenseUrl,
			string rightsStatus,
			string rightsStatement,
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
			int? creationUserID,
			int? lastModifiedUserID,
			string pageProgression)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("SegmentInsertAuto", connection, transaction, 
				CustomSqlHelper.CreateOutputParameter("SegmentID", SqlDbType.Int, null, false),
					CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemID),
					CustomSqlHelper.CreateInputParameter("RedirectSegmentID", SqlDbType.Int, null, true, redirectSegmentID),
					CustomSqlHelper.CreateInputParameter("SegmentGenreID", SqlDbType.Int, null, false, segmentGenreID),
					CustomSqlHelper.CreateInputParameter("StartPageID", SqlDbType.Int, null, true, startPageID),
					CustomSqlHelper.CreateInputParameter("ThumbnailPageID", SqlDbType.Int, null, true, thumbnailPageID),
					CustomSqlHelper.CreateInputParameter("LanguageCode", SqlDbType.NVarChar, 10, true, languageCode),
					CustomSqlHelper.CreateInputParameter("BarCode", SqlDbType.NVarChar, 200, true, barCode),
					CustomSqlHelper.CreateInputParameter("MARCItemID", SqlDbType.NVarChar, 200, true, mARCItemID),
					CustomSqlHelper.CreateInputParameter("Title", SqlDbType.NVarChar, 2000, false, title),
					CustomSqlHelper.CreateInputParameter("SortTitle", SqlDbType.NVarChar, 2000, false, sortTitle),
					CustomSqlHelper.CreateInputParameter("TranslatedTitle", SqlDbType.NVarChar, 2000, false, translatedTitle),
					CustomSqlHelper.CreateInputParameter("ContainerTitle", SqlDbType.NVarChar, 2000, false, containerTitle),
					CustomSqlHelper.CreateInputParameter("PublicationDetails", SqlDbType.NVarChar, 400, false, publicationDetails),
					CustomSqlHelper.CreateInputParameter("PublisherName", SqlDbType.NVarChar, 250, false, publisherName),
					CustomSqlHelper.CreateInputParameter("PublisherPlace", SqlDbType.NVarChar, 150, false, publisherPlace),
					CustomSqlHelper.CreateInputParameter("Summary", SqlDbType.NVarChar, 1073741823, false, summary),
					CustomSqlHelper.CreateInputParameter("Volume", SqlDbType.NVarChar, 100, false, volume),
					CustomSqlHelper.CreateInputParameter("Series", SqlDbType.NVarChar, 100, false, series),
					CustomSqlHelper.CreateInputParameter("Issue", SqlDbType.NVarChar, 100, false, issue),
					CustomSqlHelper.CreateInputParameter("Edition", SqlDbType.NVarChar, 400, false, edition),
					CustomSqlHelper.CreateInputParameter("Date", SqlDbType.NVarChar, 20, false, date),
					CustomSqlHelper.CreateInputParameter("PageRange", SqlDbType.NVarChar, 50, false, pageRange),
					CustomSqlHelper.CreateInputParameter("StartPageNumber", SqlDbType.NVarChar, 20, false, startPageNumber),
					CustomSqlHelper.CreateInputParameter("EndPageNumber", SqlDbType.NVarChar, 20, false, endPageNumber),
					CustomSqlHelper.CreateInputParameter("Url", SqlDbType.NVarChar, 200, false, url),
					CustomSqlHelper.CreateInputParameter("DownloadUrl", SqlDbType.NVarChar, 200, false, downloadUrl),
					CustomSqlHelper.CreateInputParameter("LicenseName", SqlDbType.NVarChar, 200, false, licenseName),
					CustomSqlHelper.CreateInputParameter("LicenseUrl", SqlDbType.NVarChar, 200, false, licenseUrl),
					CustomSqlHelper.CreateInputParameter("RightsStatus", SqlDbType.NVarChar, 500, false, rightsStatus),
					CustomSqlHelper.CreateInputParameter("RightsStatement", SqlDbType.NVarChar, 1073741823, false, rightsStatement),
					CustomSqlHelper.CreateInputParameter("CopyrightStatus", SqlDbType.NVarChar, 1073741823, false, copyrightStatus),
					CustomSqlHelper.CreateInputParameter("CopyrightRegion", SqlDbType.NVarChar, 50, false, copyrightRegion),
					CustomSqlHelper.CreateInputParameter("CopyrightComment", SqlDbType.NVarChar, 1073741823, false, copyrightComment),
					CustomSqlHelper.CreateInputParameter("CopyrightEvidence", SqlDbType.NVarChar, 1073741823, false, copyrightEvidence),
					CustomSqlHelper.CreateInputParameter("ScanningUser", SqlDbType.NVarChar, 100, true, scanningUser),
					CustomSqlHelper.CreateInputParameter("ScanningDate", SqlDbType.DateTime, null, true, scanningDate),
					CustomSqlHelper.CreateInputParameter("PaginationStatusID", SqlDbType.Int, null, true, paginationStatusID),
					CustomSqlHelper.CreateInputParameter("PaginationStatusDate", SqlDbType.DateTime, null, true, paginationStatusDate),
					CustomSqlHelper.CreateInputParameter("PaginationStatusUserID", SqlDbType.Int, null, true, paginationStatusUserID),
					CustomSqlHelper.CreateInputParameter("PaginationCompleteDate", SqlDbType.DateTime, null, true, paginationCompleteDate),
					CustomSqlHelper.CreateInputParameter("PaginationCompleteUserID", SqlDbType.Int, null, true, paginationCompleteUserID),
					CustomSqlHelper.CreateInputParameter("LastPageNameLookupDate", SqlDbType.DateTime, null, true, lastPageNameLookupDate),
					CustomSqlHelper.CreateInputParameter("CreationUserID", SqlDbType.Int, null, true, creationUserID),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, true, lastModifiedUserID),
					CustomSqlHelper.CreateInputParameter("PageProgression", SqlDbType.NVarChar, 10, false, pageProgression), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<Segment> helper = new CustomSqlHelper<Segment>())
				{
					List<Segment> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						Segment o = list[0];
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
		/// Insert values into dbo.Segment. Returns an object of type Segment.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type Segment.</param>
		/// <returns>Object of type Segment.</returns>
		public Segment SegmentInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			Segment value)
		{
			return SegmentInsertAuto(sqlConnection, sqlTransaction, "BHL", value);
		}
		
		/// <summary>
		/// Insert values into dbo.Segment. Returns an object of type Segment.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type Segment.</param>
		/// <returns>Object of type Segment.</returns>
		public Segment SegmentInsertAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			Segment value)
		{
			return SegmentInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.ItemID,
				value.RedirectSegmentID,
				value.SegmentGenreID,
				value.StartPageID,
				value.ThumbnailPageID,
				value.LanguageCode,
				value.BarCode,
				value.MARCItemID,
				value.Title,
				value.SortTitle,
				value.TranslatedTitle,
				value.ContainerTitle,
				value.PublicationDetails,
				value.PublisherName,
				value.PublisherPlace,
				value.Summary,
				value.Volume,
				value.Series,
				value.Issue,
				value.Edition,
				value.Date,
				value.PageRange,
				value.StartPageNumber,
				value.EndPageNumber,
				value.Url,
				value.DownloadUrl,
				value.LicenseName,
				value.LicenseUrl,
				value.RightsStatus,
				value.RightsStatement,
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
				value.CreationUserID,
				value.LastModifiedUserID,
				value.PageProgression);
		}
		
		#endregion ===== INSERT =====

		#region ===== DELETE =====

		/// <summary>
		/// Delete values from dbo.Segment by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="segmentID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool SegmentDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int segmentID)
		{
			return SegmentDeleteAuto( sqlConnection, sqlTransaction, "BHL", segmentID );
		}
		
		/// <summary>
		/// Delete values from dbo.Segment by primary key(s).
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="segmentID"></param>
		/// <returns>true if successful otherwise false.</returns>
		public bool SegmentDeleteAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int segmentID)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("SegmentDeleteAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("SegmentID", SqlDbType.Int, null, false, segmentID), 
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
		/// Update values in dbo.Segment. Returns an object of type Segment.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="segmentID"></param>
		/// <param name="itemID"></param>
		/// <param name="redirectSegmentID"></param>
		/// <param name="segmentGenreID"></param>
		/// <param name="startPageID"></param>
		/// <param name="thumbnailPageID"></param>
		/// <param name="languageCode"></param>
		/// <param name="barCode"></param>
		/// <param name="mARCItemID"></param>
		/// <param name="title"></param>
		/// <param name="sortTitle"></param>
		/// <param name="translatedTitle"></param>
		/// <param name="containerTitle"></param>
		/// <param name="publicationDetails"></param>
		/// <param name="publisherName"></param>
		/// <param name="publisherPlace"></param>
		/// <param name="summary"></param>
		/// <param name="volume"></param>
		/// <param name="series"></param>
		/// <param name="issue"></param>
		/// <param name="edition"></param>
		/// <param name="date"></param>
		/// <param name="pageRange"></param>
		/// <param name="startPageNumber"></param>
		/// <param name="endPageNumber"></param>
		/// <param name="url"></param>
		/// <param name="downloadUrl"></param>
		/// <param name="licenseName"></param>
		/// <param name="licenseUrl"></param>
		/// <param name="rightsStatus"></param>
		/// <param name="rightsStatement"></param>
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
		/// <param name="lastModifiedUserID"></param>
		/// <param name="pageProgression"></param>
		/// <returns>Object of type Segment.</returns>
		public Segment SegmentUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			int segmentID,
			int itemID,
			int? redirectSegmentID,
			int segmentGenreID,
			int? startPageID,
			int? thumbnailPageID,
			string languageCode,
			string barCode,
			string mARCItemID,
			string title,
			string sortTitle,
			string translatedTitle,
			string containerTitle,
			string publicationDetails,
			string publisherName,
			string publisherPlace,
			string summary,
			string volume,
			string series,
			string issue,
			string edition,
			string date,
			string pageRange,
			string startPageNumber,
			string endPageNumber,
			string url,
			string downloadUrl,
			string licenseName,
			string licenseUrl,
			string rightsStatus,
			string rightsStatement,
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
			int? lastModifiedUserID,
			string pageProgression)
		{
			return SegmentUpdateAuto( sqlConnection, sqlTransaction, "BHL", segmentID, itemID, redirectSegmentID, segmentGenreID, startPageID, thumbnailPageID, languageCode, barCode, mARCItemID, title, sortTitle, translatedTitle, containerTitle, publicationDetails, publisherName, publisherPlace, summary, volume, series, issue, edition, date, pageRange, startPageNumber, endPageNumber, url, downloadUrl, licenseName, licenseUrl, rightsStatus, rightsStatement, copyrightStatus, copyrightRegion, copyrightComment, copyrightEvidence, scanningUser, scanningDate, paginationStatusID, paginationStatusDate, paginationStatusUserID, paginationCompleteDate, paginationCompleteUserID, lastPageNameLookupDate, lastModifiedUserID, pageProgression);
		}
		
		/// <summary>
		/// Update values in dbo.Segment. Returns an object of type Segment.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="segmentID"></param>
		/// <param name="itemID"></param>
		/// <param name="redirectSegmentID"></param>
		/// <param name="segmentGenreID"></param>
		/// <param name="startPageID"></param>
		/// <param name="thumbnailPageID"></param>
		/// <param name="languageCode"></param>
		/// <param name="barCode"></param>
		/// <param name="mARCItemID"></param>
		/// <param name="title"></param>
		/// <param name="sortTitle"></param>
		/// <param name="translatedTitle"></param>
		/// <param name="containerTitle"></param>
		/// <param name="publicationDetails"></param>
		/// <param name="publisherName"></param>
		/// <param name="publisherPlace"></param>
		/// <param name="summary"></param>
		/// <param name="volume"></param>
		/// <param name="series"></param>
		/// <param name="issue"></param>
		/// <param name="edition"></param>
		/// <param name="date"></param>
		/// <param name="pageRange"></param>
		/// <param name="startPageNumber"></param>
		/// <param name="endPageNumber"></param>
		/// <param name="url"></param>
		/// <param name="downloadUrl"></param>
		/// <param name="licenseName"></param>
		/// <param name="licenseUrl"></param>
		/// <param name="rightsStatus"></param>
		/// <param name="rightsStatement"></param>
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
		/// <param name="lastModifiedUserID"></param>
		/// <param name="pageProgression"></param>
		/// <returns>Object of type Segment.</returns>
		public Segment SegmentUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			int segmentID,
			int itemID,
			int? redirectSegmentID,
			int segmentGenreID,
			int? startPageID,
			int? thumbnailPageID,
			string languageCode,
			string barCode,
			string mARCItemID,
			string title,
			string sortTitle,
			string translatedTitle,
			string containerTitle,
			string publicationDetails,
			string publisherName,
			string publisherPlace,
			string summary,
			string volume,
			string series,
			string issue,
			string edition,
			string date,
			string pageRange,
			string startPageNumber,
			string endPageNumber,
			string url,
			string downloadUrl,
			string licenseName,
			string licenseUrl,
			string rightsStatus,
			string rightsStatement,
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
			int? lastModifiedUserID,
			string pageProgression)
		{
			SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
			SqlTransaction transaction = sqlTransaction;
			
			using (SqlCommand command = CustomSqlHelper.CreateCommand("SegmentUpdateAuto", connection, transaction, 
				CustomSqlHelper.CreateInputParameter("SegmentID", SqlDbType.Int, null, false, segmentID),
					CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemID),
					CustomSqlHelper.CreateInputParameter("RedirectSegmentID", SqlDbType.Int, null, true, redirectSegmentID),
					CustomSqlHelper.CreateInputParameter("SegmentGenreID", SqlDbType.Int, null, false, segmentGenreID),
					CustomSqlHelper.CreateInputParameter("StartPageID", SqlDbType.Int, null, true, startPageID),
					CustomSqlHelper.CreateInputParameter("ThumbnailPageID", SqlDbType.Int, null, true, thumbnailPageID),
					CustomSqlHelper.CreateInputParameter("LanguageCode", SqlDbType.NVarChar, 10, true, languageCode),
					CustomSqlHelper.CreateInputParameter("BarCode", SqlDbType.NVarChar, 200, true, barCode),
					CustomSqlHelper.CreateInputParameter("MARCItemID", SqlDbType.NVarChar, 200, true, mARCItemID),
					CustomSqlHelper.CreateInputParameter("Title", SqlDbType.NVarChar, 2000, false, title),
					CustomSqlHelper.CreateInputParameter("SortTitle", SqlDbType.NVarChar, 2000, false, sortTitle),
					CustomSqlHelper.CreateInputParameter("TranslatedTitle", SqlDbType.NVarChar, 2000, false, translatedTitle),
					CustomSqlHelper.CreateInputParameter("ContainerTitle", SqlDbType.NVarChar, 2000, false, containerTitle),
					CustomSqlHelper.CreateInputParameter("PublicationDetails", SqlDbType.NVarChar, 400, false, publicationDetails),
					CustomSqlHelper.CreateInputParameter("PublisherName", SqlDbType.NVarChar, 250, false, publisherName),
					CustomSqlHelper.CreateInputParameter("PublisherPlace", SqlDbType.NVarChar, 150, false, publisherPlace),
					CustomSqlHelper.CreateInputParameter("Summary", SqlDbType.NVarChar, 1073741823, false, summary),
					CustomSqlHelper.CreateInputParameter("Volume", SqlDbType.NVarChar, 100, false, volume),
					CustomSqlHelper.CreateInputParameter("Series", SqlDbType.NVarChar, 100, false, series),
					CustomSqlHelper.CreateInputParameter("Issue", SqlDbType.NVarChar, 100, false, issue),
					CustomSqlHelper.CreateInputParameter("Edition", SqlDbType.NVarChar, 400, false, edition),
					CustomSqlHelper.CreateInputParameter("Date", SqlDbType.NVarChar, 20, false, date),
					CustomSqlHelper.CreateInputParameter("PageRange", SqlDbType.NVarChar, 50, false, pageRange),
					CustomSqlHelper.CreateInputParameter("StartPageNumber", SqlDbType.NVarChar, 20, false, startPageNumber),
					CustomSqlHelper.CreateInputParameter("EndPageNumber", SqlDbType.NVarChar, 20, false, endPageNumber),
					CustomSqlHelper.CreateInputParameter("Url", SqlDbType.NVarChar, 200, false, url),
					CustomSqlHelper.CreateInputParameter("DownloadUrl", SqlDbType.NVarChar, 200, false, downloadUrl),
					CustomSqlHelper.CreateInputParameter("LicenseName", SqlDbType.NVarChar, 200, false, licenseName),
					CustomSqlHelper.CreateInputParameter("LicenseUrl", SqlDbType.NVarChar, 200, false, licenseUrl),
					CustomSqlHelper.CreateInputParameter("RightsStatus", SqlDbType.NVarChar, 500, false, rightsStatus),
					CustomSqlHelper.CreateInputParameter("RightsStatement", SqlDbType.NVarChar, 1073741823, false, rightsStatement),
					CustomSqlHelper.CreateInputParameter("CopyrightStatus", SqlDbType.NVarChar, 1073741823, false, copyrightStatus),
					CustomSqlHelper.CreateInputParameter("CopyrightRegion", SqlDbType.NVarChar, 50, false, copyrightRegion),
					CustomSqlHelper.CreateInputParameter("CopyrightComment", SqlDbType.NVarChar, 1073741823, false, copyrightComment),
					CustomSqlHelper.CreateInputParameter("CopyrightEvidence", SqlDbType.NVarChar, 1073741823, false, copyrightEvidence),
					CustomSqlHelper.CreateInputParameter("ScanningUser", SqlDbType.NVarChar, 100, true, scanningUser),
					CustomSqlHelper.CreateInputParameter("ScanningDate", SqlDbType.DateTime, null, true, scanningDate),
					CustomSqlHelper.CreateInputParameter("PaginationStatusID", SqlDbType.Int, null, true, paginationStatusID),
					CustomSqlHelper.CreateInputParameter("PaginationStatusDate", SqlDbType.DateTime, null, true, paginationStatusDate),
					CustomSqlHelper.CreateInputParameter("PaginationStatusUserID", SqlDbType.Int, null, true, paginationStatusUserID),
					CustomSqlHelper.CreateInputParameter("PaginationCompleteDate", SqlDbType.DateTime, null, true, paginationCompleteDate),
					CustomSqlHelper.CreateInputParameter("PaginationCompleteUserID", SqlDbType.Int, null, true, paginationCompleteUserID),
					CustomSqlHelper.CreateInputParameter("LastPageNameLookupDate", SqlDbType.DateTime, null, true, lastPageNameLookupDate),
					CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, true, lastModifiedUserID),
					CustomSqlHelper.CreateInputParameter("PageProgression", SqlDbType.NVarChar, 10, false, pageProgression), 
					CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
			{
				using (CustomSqlHelper<Segment> helper = new CustomSqlHelper<Segment>())
				{
					List<Segment> list = helper.ExecuteReader(command);
					if (list.Count > 0)
					{
						Segment o = list[0];
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
		/// Update values in dbo.Segment. Returns an object of type Segment.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type Segment.</param>
		/// <returns>Object of type Segment.</returns>
		public Segment SegmentUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			Segment value)
		{
			return SegmentUpdateAuto(sqlConnection, sqlTransaction, "BHL", value );
		}
		
		/// <summary>
		/// Update values in dbo.Segment. Returns an object of type Segment.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type Segment.</param>
		/// <returns>Object of type Segment.</returns>
		public Segment SegmentUpdateAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			Segment value)
		{
			return SegmentUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
				value.SegmentID,
				value.ItemID,
				value.RedirectSegmentID,
				value.SegmentGenreID,
				value.StartPageID,
				value.ThumbnailPageID,
				value.LanguageCode,
				value.BarCode,
				value.MARCItemID,
				value.Title,
				value.SortTitle,
				value.TranslatedTitle,
				value.ContainerTitle,
				value.PublicationDetails,
				value.PublisherName,
				value.PublisherPlace,
				value.Summary,
				value.Volume,
				value.Series,
				value.Issue,
				value.Edition,
				value.Date,
				value.PageRange,
				value.StartPageNumber,
				value.EndPageNumber,
				value.Url,
				value.DownloadUrl,
				value.LicenseName,
				value.LicenseUrl,
				value.RightsStatus,
				value.RightsStatement,
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
				value.LastModifiedUserID,
				value.PageProgression);
		}
		
		#endregion ===== UPDATE =====

		#region ===== MANAGE =====
		
		/// <summary>
		/// Manage dbo.Segment object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.Segment.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="value">Object of type Segment.</param>
		/// <returns>Object of type CustomDataAccessStatus<Segment>.</returns>
		public CustomDataAccessStatus<Segment> SegmentManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			Segment value , int userId )
		{
			return SegmentManageAuto( sqlConnection, sqlTransaction, "BHL", value , userId );
		}
		
		/// <summary>
		/// Manage dbo.Segment object.
		/// If the object is of type CustomObjectBase, 
		/// then either insert values into, delete values from, or update values in dbo.Segment.
		/// </summary>
		/// <param name="sqlConnection">Sql connection or null.</param>
		/// <param name="sqlTransaction">Sql transaction or null.</param>
		/// <param name="connectionKeyName">Connection key name located in config file.</param>
		/// <param name="value">Object of type Segment.</param>
		/// <returns>Object of type CustomDataAccessStatus<Segment>.</returns>
		public CustomDataAccessStatus<Segment> SegmentManageAuto(
			SqlConnection sqlConnection, 
			SqlTransaction sqlTransaction, 
			string connectionKeyName,
			Segment value , int userId )
		{
			if (value.IsNew && !value.IsDeleted)
			{
				value.CreationUserID = userId;
				value.LastModifiedUserID = userId;
				Segment returnValue = SegmentInsertAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.ItemID,
						value.RedirectSegmentID,
						value.SegmentGenreID,
						value.StartPageID,
						value.ThumbnailPageID,
						value.LanguageCode,
						value.BarCode,
						value.MARCItemID,
						value.Title,
						value.SortTitle,
						value.TranslatedTitle,
						value.ContainerTitle,
						value.PublicationDetails,
						value.PublisherName,
						value.PublisherPlace,
						value.Summary,
						value.Volume,
						value.Series,
						value.Issue,
						value.Edition,
						value.Date,
						value.PageRange,
						value.StartPageNumber,
						value.EndPageNumber,
						value.Url,
						value.DownloadUrl,
						value.LicenseName,
						value.LicenseUrl,
						value.RightsStatus,
						value.RightsStatement,
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
						value.CreationUserID,
						value.LastModifiedUserID,
						value.PageProgression);
				
				return new CustomDataAccessStatus<Segment>(
					CustomDataAccessContext.Insert, 
					true, returnValue);
			}
			else if (!value.IsNew && value.IsDeleted)
			{
				if (SegmentDeleteAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.SegmentID))
				{
				return new CustomDataAccessStatus<Segment>(
					CustomDataAccessContext.Delete, 
					true, value);
				}
				else
				{
				return new CustomDataAccessStatus<Segment>(
					CustomDataAccessContext.Delete, 
					false, value);
				}
			}
			else if (value.IsDirty && !value.IsDeleted)
			{
				value.LastModifiedUserID = userId;
				Segment returnValue = SegmentUpdateAuto(sqlConnection, sqlTransaction, connectionKeyName,
					value.SegmentID,
						value.ItemID,
						value.RedirectSegmentID,
						value.SegmentGenreID,
						value.StartPageID,
						value.ThumbnailPageID,
						value.LanguageCode,
						value.BarCode,
						value.MARCItemID,
						value.Title,
						value.SortTitle,
						value.TranslatedTitle,
						value.ContainerTitle,
						value.PublicationDetails,
						value.PublisherName,
						value.PublisherPlace,
						value.Summary,
						value.Volume,
						value.Series,
						value.Issue,
						value.Edition,
						value.Date,
						value.PageRange,
						value.StartPageNumber,
						value.EndPageNumber,
						value.Url,
						value.DownloadUrl,
						value.LicenseName,
						value.LicenseUrl,
						value.RightsStatus,
						value.RightsStatement,
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
						value.LastModifiedUserID,
						value.PageProgression);
					
				return new CustomDataAccessStatus<Segment>(
					CustomDataAccessContext.Update, 
					true, returnValue);
			}
			else
			{
				return new CustomDataAccessStatus<Segment>(
					CustomDataAccessContext.NA, 
					false, value);
			}
		}
		
		#endregion ===== MANAGE =====

	}	
}

