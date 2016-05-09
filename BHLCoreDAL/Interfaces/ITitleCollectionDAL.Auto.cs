
// Generated 5/9/2016 1:54:26 PM
// Do not modify the contents of this code file.
// Interface ITitleCollectionDAL based upon dbo.TitleCollection.

#region using

using System;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHL.DataObjects;

#endregion using

namespace MOBOT.BHL.DAL
{
	public interface ITitleCollectionDAL
	{
		TitleCollection TitleCollectionSelectAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			int titleCollectionID);

		TitleCollection TitleCollectionSelectAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName,
			int titleCollectionID);

		CustomGenericList<CustomDataRow> TitleCollectionSelectAutoRaw(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			int titleCollectionID);

		CustomGenericList<CustomDataRow> TitleCollectionSelectAutoRaw(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName,
			int titleCollectionID);

		TitleCollection TitleCollectionInsertAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			int titleID,
			int collectionID);

		TitleCollection TitleCollectionInsertAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName,
			int titleID,
			int collectionID);

		TitleCollection TitleCollectionInsertAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, TitleCollection value);

		TitleCollection TitleCollectionInsertAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName, TitleCollection value);

		bool TitleCollectionDeleteAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			int titleCollectionID);

		bool TitleCollectionDeleteAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName,
			int titleCollectionID);

		TitleCollection TitleCollectionUpdateAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			int titleCollectionID,
			int titleID,
			int collectionID);

		TitleCollection TitleCollectionUpdateAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName,
			int titleCollectionID,
			int titleID,
			int collectionID);

		TitleCollection TitleCollectionUpdateAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, TitleCollection value);

		TitleCollection TitleCollectionUpdateAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName, TitleCollection value);

		CustomDataAccessStatus<TitleCollection> TitleCollectionManageAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, TitleCollection value);

		CustomDataAccessStatus<TitleCollection> TitleCollectionManageAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName, TitleCollection value);


	}
}

