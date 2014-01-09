	
// Generated 1/10/2014 11:05:49 AM
// Do not modify the contents of this code file.
// Interface IImportRecordKeywordDAL based upon ImportRecordKeyword.

#region using

using System;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHL.DataObjects;

#endregion using

namespace MOBOT.BHL.DAL
{
	public interface IImportRecordKeywordDAL
	{
		ImportRecordKeyword ImportRecordKeywordSelectAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			int importRecordKeywordID);

		CustomGenericList<CustomDataRow> ImportRecordKeywordSelectAutoRaw(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			int importRecordKeywordID);

		ImportRecordKeyword ImportRecordKeywordInsertAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			int importRecordID,
			string keyword,
			int creationUserID,
			int lastModifiedUserID);

		ImportRecordKeyword ImportRecordKeywordInsertAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, ImportRecordKeyword value);

		bool ImportRecordKeywordDeleteAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			int importRecordKeywordID);

		ImportRecordKeyword ImportRecordKeywordUpdateAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			int importRecordKeywordID,
			int importRecordID,
			string keyword,
			int lastModifiedUserID);

		ImportRecordKeyword ImportRecordKeywordUpdateAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, ImportRecordKeyword value);
	}
}
// end of source generation
