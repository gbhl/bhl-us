
// Generated 9/20/2017 1:00:17 PM
// Do not modify the contents of this code file.
// Interface IImportRecordCreatorDAL based upon import.ImportRecordCreator.

#region using

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHL.DataObjects;

#endregion using

namespace MOBOT.BHL.DAL
{
	public interface IImportRecordCreatorDAL
	{
		ImportRecordCreator ImportRecordCreatorSelectAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			int importRecordCreatorID);

		ImportRecordCreator ImportRecordCreatorSelectAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName,
			int importRecordCreatorID);

		List<CustomDataRow> ImportRecordCreatorSelectAutoRaw(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			int importRecordCreatorID);

		List<CustomDataRow> ImportRecordCreatorSelectAutoRaw(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName,
			int importRecordCreatorID);

		ImportRecordCreator ImportRecordCreatorInsertAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			int importRecordID,
			string fullName,
			string firstName,
			string lastName,
			string startYear,
			string endYear,
			string authorType,
			int creationUserID,
			int lastModifiedUserID,
			int? authorID);

		ImportRecordCreator ImportRecordCreatorInsertAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName,
			int importRecordID,
			string fullName,
			string firstName,
			string lastName,
			string startYear,
			string endYear,
			string authorType,
			int creationUserID,
			int lastModifiedUserID,
			int? authorID);

		ImportRecordCreator ImportRecordCreatorInsertAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, ImportRecordCreator value);

		ImportRecordCreator ImportRecordCreatorInsertAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName, ImportRecordCreator value);

		bool ImportRecordCreatorDeleteAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			int importRecordCreatorID);

		bool ImportRecordCreatorDeleteAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName,
			int importRecordCreatorID);

		ImportRecordCreator ImportRecordCreatorUpdateAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			int importRecordCreatorID,
			int importRecordID,
			string fullName,
			string firstName,
			string lastName,
			string startYear,
			string endYear,
			string authorType,
			int lastModifiedUserID,
			int? authorID);

		ImportRecordCreator ImportRecordCreatorUpdateAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName,
			int importRecordCreatorID,
			int importRecordID,
			string fullName,
			string firstName,
			string lastName,
			string startYear,
			string endYear,
			string authorType,
			int lastModifiedUserID,
			int? authorID);

		ImportRecordCreator ImportRecordCreatorUpdateAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, ImportRecordCreator value);

		ImportRecordCreator ImportRecordCreatorUpdateAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName, ImportRecordCreator value);

		CustomDataAccessStatus<ImportRecordCreator> ImportRecordCreatorManageAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, ImportRecordCreator value, int userId);

		CustomDataAccessStatus<ImportRecordCreator> ImportRecordCreatorManageAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName, ImportRecordCreator value, int userId);


	}
}

