
// Generated 5/9/2016 1:53:07 PM
// Do not modify the contents of this code file.
// Interface ISegmentDAL based upon dbo.Segment.

#region using

using System;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHL.DataObjects;

#endregion using

namespace MOBOT.BHL.DAL
{
	public interface ISegmentDAL
	{
		Segment SegmentSelectAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			int segmentID);

		Segment SegmentSelectAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName,
			int segmentID);

		CustomGenericList<CustomDataRow> SegmentSelectAutoRaw(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			int segmentID);

		CustomGenericList<CustomDataRow> SegmentSelectAutoRaw(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName,
			int segmentID);

		Segment SegmentInsertAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			int? itemID,
			int segmentStatusID,
			string contributorCode,
			string contributorSegmentID,
			short sequenceOrder,
			int segmentGenreID,
			string title,
			string translatedTitle,
			string containerTitle,
			string publicationDetails,
			string publisherName,
			string publisherPlace,
			string notes,
			string summary,
			string volume,
			string series,
			string issue,
			string edition,
			string date,
			string pageRange,
			string startPageNumber,
			string endPageNumber,
			int? startPageID,
			string languageCode,
			string url,
			string downloadUrl,
			string rightsStatus,
			string rightsStatement,
			string licenseName,
			string licenseUrl,
			DateTime? contributorCreationDate,
			DateTime? contributorLastModifiedDate,
			int? creationUserID,
			int? lastModifiedUserID,
			string sortTitle,
			int? redirectSegmentID);

		Segment SegmentInsertAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName,
			int? itemID,
			int segmentStatusID,
			string contributorCode,
			string contributorSegmentID,
			short sequenceOrder,
			int segmentGenreID,
			string title,
			string translatedTitle,
			string containerTitle,
			string publicationDetails,
			string publisherName,
			string publisherPlace,
			string notes,
			string summary,
			string volume,
			string series,
			string issue,
			string edition,
			string date,
			string pageRange,
			string startPageNumber,
			string endPageNumber,
			int? startPageID,
			string languageCode,
			string url,
			string downloadUrl,
			string rightsStatus,
			string rightsStatement,
			string licenseName,
			string licenseUrl,
			DateTime? contributorCreationDate,
			DateTime? contributorLastModifiedDate,
			int? creationUserID,
			int? lastModifiedUserID,
			string sortTitle,
			int? redirectSegmentID);

		Segment SegmentInsertAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, Segment value);

		Segment SegmentInsertAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName, Segment value);

		bool SegmentDeleteAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			int segmentID);

		bool SegmentDeleteAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName,
			int segmentID);

		Segment SegmentUpdateAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			int segmentID,
			int? itemID,
			int segmentStatusID,
			string contributorCode,
			string contributorSegmentID,
			short sequenceOrder,
			int segmentGenreID,
			string title,
			string translatedTitle,
			string containerTitle,
			string publicationDetails,
			string publisherName,
			string publisherPlace,
			string notes,
			string summary,
			string volume,
			string series,
			string issue,
			string edition,
			string date,
			string pageRange,
			string startPageNumber,
			string endPageNumber,
			int? startPageID,
			string languageCode,
			string url,
			string downloadUrl,
			string rightsStatus,
			string rightsStatement,
			string licenseName,
			string licenseUrl,
			DateTime? contributorCreationDate,
			DateTime? contributorLastModifiedDate,
			int? lastModifiedUserID,
			string sortTitle,
			int? redirectSegmentID);

		Segment SegmentUpdateAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName,
			int segmentID,
			int? itemID,
			int segmentStatusID,
			string contributorCode,
			string contributorSegmentID,
			short sequenceOrder,
			int segmentGenreID,
			string title,
			string translatedTitle,
			string containerTitle,
			string publicationDetails,
			string publisherName,
			string publisherPlace,
			string notes,
			string summary,
			string volume,
			string series,
			string issue,
			string edition,
			string date,
			string pageRange,
			string startPageNumber,
			string endPageNumber,
			int? startPageID,
			string languageCode,
			string url,
			string downloadUrl,
			string rightsStatus,
			string rightsStatement,
			string licenseName,
			string licenseUrl,
			DateTime? contributorCreationDate,
			DateTime? contributorLastModifiedDate,
			int? lastModifiedUserID,
			string sortTitle,
			int? redirectSegmentID);

		Segment SegmentUpdateAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, Segment value);

		Segment SegmentUpdateAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName, Segment value);

		CustomDataAccessStatus<Segment> SegmentManageAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, Segment value, int userId);

		CustomDataAccessStatus<Segment> SegmentManageAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName, Segment value, int userId);


	}
}

