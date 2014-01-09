	
// Generated 1/15/2014 9:26:48 AM
// Do not modify the contents of this code file.
// Interface IImportRecordDAL based upon ImportRecord.

#region using

using System;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHL.DataObjects;

#endregion using

namespace MOBOT.BHL.DAL
{
	public interface IImportRecordDAL
	{
		ImportRecord ImportRecordSelectAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			int importRecordID);

		CustomGenericList<CustomDataRow> ImportRecordSelectAutoRaw(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			int importRecordID);

		ImportRecord ImportRecordInsertAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			int importFileID,
			int importRecordStatusID,
			string genre,
			string title,
			string translatedTitle,
			string journalTitle,
			string volume,
			string series,
			string issue,
			string publicationDetails,
			string publisherName,
			string publisherPlace,
			string year,
			short? startYear,
			short? endYear,
			string language,
			string rights,
			string dueDiligence,
			string copyrightStatus,
			string license,
			string licenseUrl,
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
			int lastModifiedUserID);

		ImportRecord ImportRecordInsertAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, ImportRecord value);

		bool ImportRecordDeleteAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			int importRecordID);

		ImportRecord ImportRecordUpdateAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
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
			string publicationDetails,
			string publisherName,
			string publisherPlace,
			string year,
			short? startYear,
			short? endYear,
			string language,
			string rights,
			string dueDiligence,
			string copyrightStatus,
			string license,
			string licenseUrl,
			string startPage,
			string endPage,
			string url,
			string downloadUrl,
			string dOI,
			string iSSN,
			string iSBN,
			string oCLC,
			string lCCN,
			int lastModifiedUserID);

		ImportRecord ImportRecordUpdateAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, ImportRecord value);

        void ImportRecordSave(SqlConnection sqlConnection, SqlTransaction sqlTransaction, ImportRecord citation, int userID);
	}
}
// end of source generation
