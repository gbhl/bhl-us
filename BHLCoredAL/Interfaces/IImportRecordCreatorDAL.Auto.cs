	
// Generated 1/10/2014 11:05:49 AM
// Do not modify the contents of this code file.
// Interface IImportRecordCreatorDAL based upon ImportRecordCreator.

#region using

using System;
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

		CustomGenericList<CustomDataRow> ImportRecordCreatorSelectAutoRaw(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
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
			int lastModifiedUserID);

		ImportRecordCreator ImportRecordCreatorInsertAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, ImportRecordCreator value);

		bool ImportRecordCreatorDeleteAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
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
			int lastModifiedUserID);

		ImportRecordCreator ImportRecordCreatorUpdateAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, ImportRecordCreator value);
	}
}
// end of source generation
