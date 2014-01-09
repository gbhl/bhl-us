	
// Generated 1/10/2014 11:05:49 AM
// Do not modify the contents of this code file.
// Interface IImportRecordErrorLogDAL based upon ImportRecordErrorLog.

#region using

using System;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHL.DataObjects;

#endregion using

namespace MOBOT.BHL.DAL
{
	public interface IImportRecordErrorLogDAL
	{
		ImportRecordErrorLog ImportRecordErrorLogSelectAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			int importRecordErrorLogID);

		CustomGenericList<CustomDataRow> ImportRecordErrorLogSelectAutoRaw(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			int importRecordErrorLogID);

		ImportRecordErrorLog ImportRecordErrorLogInsertAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			int importRecordID,
			DateTime errorDate,
			string errorMessage,
			int creationUserID,
			int lastModifiedUserID);

		ImportRecordErrorLog ImportRecordErrorLogInsertAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, ImportRecordErrorLog value);

		bool ImportRecordErrorLogDeleteAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			int importRecordErrorLogID);

		ImportRecordErrorLog ImportRecordErrorLogUpdateAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			int importRecordErrorLogID,
			int importRecordID,
			DateTime errorDate,
			string errorMessage,
			int lastModifiedUserID);

		ImportRecordErrorLog ImportRecordErrorLogUpdateAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, ImportRecordErrorLog value);
	}
}
// end of source generation
