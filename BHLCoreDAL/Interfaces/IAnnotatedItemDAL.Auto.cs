
// Generated 5/9/2016 1:51:09 PM
// Do not modify the contents of this code file.
// Interface IAnnotatedItemDAL based upon annotation.AnnotatedItem.

#region using

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHL.DataObjects;

#endregion using

namespace MOBOT.BHL.DAL
{
	public interface IAnnotatedItemDAL
	{
		AnnotatedItem AnnotatedItemSelectAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			int annotatedItemID);

		AnnotatedItem AnnotatedItemSelectAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName,
			int annotatedItemID);

		List<CustomDataRow> AnnotatedItemSelectAutoRaw(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			int annotatedItemID);

		List<CustomDataRow> AnnotatedItemSelectAutoRaw(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName,
			int annotatedItemID);

		AnnotatedItem AnnotatedItemInsertAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			int annotatedTitleID,
			int? itemID,
			string externalIdentifier,
			string volume);

		AnnotatedItem AnnotatedItemInsertAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName,
			int annotatedTitleID,
			int? itemID,
			string externalIdentifier,
			string volume);

		AnnotatedItem AnnotatedItemInsertAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, AnnotatedItem value);

		AnnotatedItem AnnotatedItemInsertAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName, AnnotatedItem value);

		bool AnnotatedItemDeleteAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			int annotatedItemID);

		bool AnnotatedItemDeleteAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName,
			int annotatedItemID);

		AnnotatedItem AnnotatedItemUpdateAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			int annotatedItemID,
			int annotatedTitleID,
			int? itemID,
			string externalIdentifier,
			string volume);

		AnnotatedItem AnnotatedItemUpdateAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName,
			int annotatedItemID,
			int annotatedTitleID,
			int? itemID,
			string externalIdentifier,
			string volume);

		AnnotatedItem AnnotatedItemUpdateAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, AnnotatedItem value);

		AnnotatedItem AnnotatedItemUpdateAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName, AnnotatedItem value);

		CustomDataAccessStatus<AnnotatedItem> AnnotatedItemManageAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, AnnotatedItem value);

		CustomDataAccessStatus<AnnotatedItem> AnnotatedItemManageAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName, AnnotatedItem value);


	}
}

