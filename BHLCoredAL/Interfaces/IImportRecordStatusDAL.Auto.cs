	
// Generated 1/10/2014 11:05:49 AM
// Do not modify the contents of this code file.
// Interface IImportRecordStatusDAL based upon ImportRecordStatus.

#region using

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHL.DataObjects;

#endregion using

namespace MOBOT.BHL.DAL
{
	public interface IImportRecordStatusDAL
	{
		ImportRecordStatus ImportRecordStatusSelectAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			int importRecordStatusID);

		List<CustomDataRow> ImportRecordStatusSelectAutoRaw(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			int importRecordStatusID);

		ImportRecordStatus ImportRecordStatusInsertAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			int importRecordStatusID,
			string statusName,
			string statusDescription,
			int creationUserID,
			int lastModifiedUserID);

		ImportRecordStatus ImportRecordStatusInsertAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, ImportRecordStatus value);

		bool ImportRecordStatusDeleteAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			int importRecordStatusID);

		ImportRecordStatus ImportRecordStatusUpdateAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			int importRecordStatusID,
			string statusName,
			string statusDescription,
			int lastModifiedUserID);

		ImportRecordStatus ImportRecordStatusUpdateAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, ImportRecordStatus value);
	}
}
// end of source generation
