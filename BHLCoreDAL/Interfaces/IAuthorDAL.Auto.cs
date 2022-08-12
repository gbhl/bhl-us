
// Generated 5/9/2016 2:24:52 PM
// Do not modify the contents of this code file.
// Interface IAuthorDAL based upon dbo.Author.

#region using

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHL.DataObjects;

#endregion using

namespace MOBOT.BHL.DAL
{
	public interface IAuthorDAL
	{
		Author AuthorSelectAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			int authorID);

		Author AuthorSelectAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName,
			int authorID);

		List<CustomDataRow> AuthorSelectAutoRaw(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			int authorID);

		List<CustomDataRow> AuthorSelectAutoRaw(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName,
			int authorID);

		Author AuthorInsertAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			int? authorTypeID,
			string startDate,
			string endDate,
			string numeration,
			string title,
			string unit,
			string location,
			short isActive,
			int? redirectAuthorID,
			int? creationUserID,
			int? lastModifiedUserID);

		Author AuthorInsertAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName,
			int? authorTypeID,
			string startDate,
			string endDate,
			string numeration,
			string title,
			string unit,
			string location,
			short isActive,
			int? redirectAuthorID,
			int? creationUserID,
			int? lastModifiedUserID);

		Author AuthorInsertAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, Author value);

		Author AuthorInsertAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName, Author value);

		bool AuthorDeleteAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			int authorID);

		bool AuthorDeleteAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName,
			int authorID);

		Author AuthorUpdateAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			int authorID,
			int? authorTypeID,
			string startDate,
			string endDate,
			string numeration,
			string title,
			string unit,
			string location,
			short isActive,
			int? redirectAuthorID,
			int? lastModifiedUserID);

		Author AuthorUpdateAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName,
			int authorID,
			int? authorTypeID,
			string startDate,
			string endDate,
			string numeration,
			string title,
			string unit,
			string location,
			short isActive,
			int? redirectAuthorID,
			int? lastModifiedUserID);

		Author AuthorUpdateAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, Author value);

		Author AuthorUpdateAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName, Author value);

		CustomDataAccessStatus<Author> AuthorManageAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, Author value, int userId);

		CustomDataAccessStatus<Author> AuthorManageAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName, Author value, int userId);


	}
}

