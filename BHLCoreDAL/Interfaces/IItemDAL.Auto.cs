
// Generated 6/2/2016 9:32:20 AM
// Do not modify the contents of this code file.
// Interface IItemDAL based upon dbo.Item.

#region using

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHL.DataObjects;

#endregion using

namespace MOBOT.BHL.DAL
{
	public interface IItemDAL
	{
		Item ItemSelectAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			int itemID);

		Item ItemSelectAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName,
			int itemID);

		List<CustomDataRow> ItemSelectAutoRaw(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			int itemID);

		List<CustomDataRow> ItemSelectAutoRaw(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName,
			int itemID);

		Item ItemInsertAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			int primaryTitleID,
			string barCode,
			string mARCItemID,
			string callNumber,
			string volume,
			string languageCode,
			string itemDescription,
			int? scannedBy,
			int? pDFSize,
			int? vaultID,
			string note,
			int? creationUserID,
			int? lastModifiedUserID,
			int itemStatusID,
			string scanningUser,
			DateTime? scanningDate,
			int? paginationCompleteUserID,
			DateTime? paginationCompleteDate,
			int? paginationStatusID,
			int? paginationStatusUserID,
			DateTime? paginationStatusDate,
			DateTime? lastPageNameLookupDate,
			int? itemSourceID,
			string year,
			string identifierBib,
			string fileRootFolder,
			string zQuery,
			string sponsor,
			string licenseUrl,
			string rights,
			string dueDiligence,
			string copyrightStatus,
			string copyrightRegion,
			string copyrightComment,
			string copyrightEvidence,
			string copyrightEvidenceOperator,
			string copyrightEvidenceDate,
			int? thumbnailPageID,
			int? redirectItemID,
			string externalUrl,
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
			byte volumeReviewed,
			DateTime? volumeReviewedDate,
			int? volumeReviewedUserID);

		Item ItemInsertAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName,
			int primaryTitleID,
			string barCode,
			string mARCItemID,
			string callNumber,
			string volume,
			string languageCode,
			string itemDescription,
			int? scannedBy,
			int? pDFSize,
			int? vaultID,
			string note,
			int? creationUserID,
			int? lastModifiedUserID,
			int itemStatusID,
			string scanningUser,
			DateTime? scanningDate,
			int? paginationCompleteUserID,
			DateTime? paginationCompleteDate,
			int? paginationStatusID,
			int? paginationStatusUserID,
			DateTime? paginationStatusDate,
			DateTime? lastPageNameLookupDate,
			int? itemSourceID,
			string year,
			string identifierBib,
			string fileRootFolder,
			string zQuery,
			string sponsor,
			string licenseUrl,
			string rights,
			string dueDiligence,
			string copyrightStatus,
			string copyrightRegion,
			string copyrightComment,
			string copyrightEvidence,
			string copyrightEvidenceOperator,
			string copyrightEvidenceDate,
			int? thumbnailPageID,
			int? redirectItemID,
			string externalUrl,
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
			byte volumeReviewed,
			DateTime? volumeReviewedDate,
			int? volumeReviewedUserID);

		Item ItemInsertAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, Item value);

		Item ItemInsertAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName, Item value);

		bool ItemDeleteAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			int itemID);

		bool ItemDeleteAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName,
			int itemID);

		Item ItemUpdateAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			int itemID,
			int primaryTitleID,
			string barCode,
			string mARCItemID,
			string callNumber,
			string volume,
			string languageCode,
			string itemDescription,
			int? scannedBy,
			int? pDFSize,
			int? vaultID,
			string note,
			int? lastModifiedUserID,
			int itemStatusID,
			string scanningUser,
			DateTime? scanningDate,
			int? paginationCompleteUserID,
			DateTime? paginationCompleteDate,
			int? paginationStatusID,
			int? paginationStatusUserID,
			DateTime? paginationStatusDate,
			DateTime? lastPageNameLookupDate,
			int? itemSourceID,
			string year,
			string identifierBib,
			string fileRootFolder,
			string zQuery,
			string sponsor,
			string licenseUrl,
			string rights,
			string dueDiligence,
			string copyrightStatus,
			string copyrightRegion,
			string copyrightComment,
			string copyrightEvidence,
			string copyrightEvidenceOperator,
			string copyrightEvidenceDate,
			int? thumbnailPageID,
			int? redirectItemID,
			string externalUrl,
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
			byte volumeReviewed,
			DateTime? volumeReviewedDate,
			int? volumeReviewedUserID);

		Item ItemUpdateAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName,
			int itemID,
			int primaryTitleID,
			string barCode,
			string mARCItemID,
			string callNumber,
			string volume,
			string languageCode,
			string itemDescription,
			int? scannedBy,
			int? pDFSize,
			int? vaultID,
			string note,
			int? lastModifiedUserID,
			int itemStatusID,
			string scanningUser,
			DateTime? scanningDate,
			int? paginationCompleteUserID,
			DateTime? paginationCompleteDate,
			int? paginationStatusID,
			int? paginationStatusUserID,
			DateTime? paginationStatusDate,
			DateTime? lastPageNameLookupDate,
			int? itemSourceID,
			string year,
			string identifierBib,
			string fileRootFolder,
			string zQuery,
			string sponsor,
			string licenseUrl,
			string rights,
			string dueDiligence,
			string copyrightStatus,
			string copyrightRegion,
			string copyrightComment,
			string copyrightEvidence,
			string copyrightEvidenceOperator,
			string copyrightEvidenceDate,
			int? thumbnailPageID,
			int? redirectItemID,
			string externalUrl,
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
			byte volumeReviewed,
			DateTime? volumeReviewedDate,
			int? volumeReviewedUserID);

		Item ItemUpdateAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, Item value);

		Item ItemUpdateAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName, Item value);

		CustomDataAccessStatus<Item> ItemManageAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, Item value, int userId);

		CustomDataAccessStatus<Item> ItemManageAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName, Item value, int userId);


	}
}

