
// Generated 9/20/2017 11:00:01 AM
// Do not modify the contents of this code file.
// Interface IImportFileDAL based upon import.ImportFile.

#region using

using System;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHL.DataObjects;

#endregion using

namespace MOBOT.BHL.DAL
{
	public interface IImportFileDAL
	{
		ImportFile ImportFileSelectAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			int importFileID);

		ImportFile ImportFileSelectAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName,
			int importFileID);

		CustomGenericList<CustomDataRow> ImportFileSelectAutoRaw(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			int importFileID);

		CustomGenericList<CustomDataRow> ImportFileSelectAutoRaw(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName,
			int importFileID);

		ImportFile ImportFileInsertAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			int importFileStatusID,
			string importFileName,
			string contributorCode,
			int creationUserID,
			int lastModifiedUserID,
			int? segmentGenreID);

		ImportFile ImportFileInsertAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName,
			int importFileStatusID,
			string importFileName,
			string contributorCode,
			int creationUserID,
			int lastModifiedUserID,
			int? segmentGenreID);

		ImportFile ImportFileInsertAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, ImportFile value);

		ImportFile ImportFileInsertAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName, ImportFile value);

		bool ImportFileDeleteAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			int importFileID);

		bool ImportFileDeleteAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName,
			int importFileID);

		ImportFile ImportFileUpdateAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			int importFileID,
			int importFileStatusID,
			string importFileName,
			string contributorCode,
			int lastModifiedUserID,
			int? segmentGenreID);

		ImportFile ImportFileUpdateAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName,
			int importFileID,
			int importFileStatusID,
			string importFileName,
			string contributorCode,
			int lastModifiedUserID,
			int? segmentGenreID);

		ImportFile ImportFileUpdateAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, ImportFile value);

		ImportFile ImportFileUpdateAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName, ImportFile value);

		CustomDataAccessStatus<ImportFile> ImportFileManageAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, ImportFile value, int userId);

		CustomDataAccessStatus<ImportFile> ImportFileManageAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName, ImportFile value, int userId);


	}
}

