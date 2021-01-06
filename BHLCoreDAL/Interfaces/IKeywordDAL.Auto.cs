
// Generated 5/9/2016 1:52:35 PM
// Do not modify the contents of this code file.
// Interface IKeywordDAL based upon dbo.Keyword.

#region using

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHL.DataObjects;

#endregion using

namespace MOBOT.BHL.DAL
{
	public interface IKeywordDAL
	{
		Keyword KeywordSelectAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			int keywordID);

		Keyword KeywordSelectAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName,
			int keywordID);

		List<CustomDataRow> KeywordSelectAutoRaw(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			int keywordID);

		List<CustomDataRow> KeywordSelectAutoRaw(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName,
			int keywordID);

		Keyword KeywordInsertAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			string keyword,
			int? creationUserID,
			int? lastModifiedUserID);

		Keyword KeywordInsertAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName,
			string keyword,
			int? creationUserID,
			int? lastModifiedUserID);

		Keyword KeywordInsertAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, Keyword value);

		Keyword KeywordInsertAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName, Keyword value);

		bool KeywordDeleteAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			int keywordID);

		bool KeywordDeleteAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName,
			int keywordID);

		Keyword KeywordUpdateAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			int keywordID,
			string keyword,
			int? lastModifiedUserID);

		Keyword KeywordUpdateAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName,
			int keywordID,
			string keyword,
			int? lastModifiedUserID);

		Keyword KeywordUpdateAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, Keyword value);

		Keyword KeywordUpdateAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName, Keyword value);

		CustomDataAccessStatus<Keyword> KeywordManageAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, Keyword value, int userId);

		CustomDataAccessStatus<Keyword> KeywordManageAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName, Keyword value, int userId);


	}
}

