
// Generated 6/2/2016 9:32:28 AM
// Do not modify the contents of this code file.
// Interface ITitleDAL based upon dbo.Title.

#region using

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHL.DataObjects;

#endregion using

namespace MOBOT.BHL.DAL
{
	public interface ITitleDAL
	{
		Title TitleSelectAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			int titleID);

		Title TitleSelectAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName,
			int titleID);

		List<CustomDataRow> TitleSelectAutoRaw(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			int titleID);

		List<CustomDataRow> TitleSelectAutoRaw(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName,
			int titleID);

		Title TitleInsertAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			string mARCBibID,
			string mARCLeader,
			int? tropicosTitleID,
			int? redirectTitleID,
			string fullTitle,
			string shortTitle,
			string uniformTitle,
			string sortTitle,
			string callNumber,
			string publicationDetails,
			short? startYear,
			short? endYear,
			string datafield_260_a,
			string datafield_260_b,
			string datafield_260_c,
			string languageCode,
			string titleDescription,
			string tL2Author,
			bool publishReady,
			bool rareBooks,
			string note,
			int? creationUserID,
			int? lastModifiedUserID,
			string originalCatalogingSource,
			string editionStatement,
			string currentPublicationFrequency,
			string partNumber,
			string partName,
			int? bibliographicLevelID);

		Title TitleInsertAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName,
			string mARCBibID,
			string mARCLeader,
			int? tropicosTitleID,
			int? redirectTitleID,
			string fullTitle,
			string shortTitle,
			string uniformTitle,
			string sortTitle,
			string callNumber,
			string publicationDetails,
			short? startYear,
			short? endYear,
			string datafield_260_a,
			string datafield_260_b,
			string datafield_260_c,
			string languageCode,
			string titleDescription,
			string tL2Author,
			bool publishReady,
			bool rareBooks,
			string note,
			int? creationUserID,
			int? lastModifiedUserID,
			string originalCatalogingSource,
			string editionStatement,
			string currentPublicationFrequency,
			string partNumber,
			string partName,
			int? bibliographicLevelID);

		Title TitleInsertAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, Title value);

		Title TitleInsertAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName, Title value);

		bool TitleDeleteAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			int titleID);

		bool TitleDeleteAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName,
			int titleID);

		Title TitleUpdateAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			int titleID,
			string mARCBibID,
			string mARCLeader,
			int? tropicosTitleID,
			int? redirectTitleID,
			string fullTitle,
			string shortTitle,
			string uniformTitle,
			string sortTitle,
			string callNumber,
			string publicationDetails,
			short? startYear,
			short? endYear,
			string datafield_260_a,
			string datafield_260_b,
			string datafield_260_c,
			string languageCode,
			string titleDescription,
			string tL2Author,
			bool publishReady,
			bool rareBooks,
			string note,
			int? lastModifiedUserID,
			string originalCatalogingSource,
			string editionStatement,
			string currentPublicationFrequency,
			string partNumber,
			string partName,
			int? bibliographicLevelID);

		Title TitleUpdateAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName,
			int titleID,
			string mARCBibID,
			string mARCLeader,
			int? tropicosTitleID,
			int? redirectTitleID,
			string fullTitle,
			string shortTitle,
			string uniformTitle,
			string sortTitle,
			string callNumber,
			string publicationDetails,
			short? startYear,
			short? endYear,
			string datafield_260_a,
			string datafield_260_b,
			string datafield_260_c,
			string languageCode,
			string titleDescription,
			string tL2Author,
			bool publishReady,
			bool rareBooks,
			string note,
			int? lastModifiedUserID,
			string originalCatalogingSource,
			string editionStatement,
			string currentPublicationFrequency,
			string partNumber,
			string partName,
			int? bibliographicLevelID);

		Title TitleUpdateAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, Title value);

		Title TitleUpdateAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName, Title value);

		CustomDataAccessStatus<Title> TitleManageAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, Title value, int userId);

		CustomDataAccessStatus<Title> TitleManageAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName, Title value, int userId);


	}
}

