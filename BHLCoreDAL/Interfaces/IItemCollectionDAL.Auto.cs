
// Generated 5/9/2016 1:52:19 PM
// Do not modify the contents of this code file.
// Interface IItemCollectionDAL based upon dbo.ItemCollection.

#region using

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHL.DataObjects;

#endregion using

namespace MOBOT.BHL.DAL
{
	public interface IItemCollectionDAL
	{
		ItemCollection ItemCollectionSelectAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			int itemCollectionID);

		ItemCollection ItemCollectionSelectAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName,
			int itemCollectionID);

		List<CustomDataRow> ItemCollectionSelectAutoRaw(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			int itemCollectionID);

		List<CustomDataRow> ItemCollectionSelectAutoRaw(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName,
			int itemCollectionID);

		ItemCollection ItemCollectionInsertAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			int itemID,
			int collectionID);

		ItemCollection ItemCollectionInsertAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName,
			int itemID,
			int collectionID);

		ItemCollection ItemCollectionInsertAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, ItemCollection value);

		ItemCollection ItemCollectionInsertAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName, ItemCollection value);

		bool ItemCollectionDeleteAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			int itemCollectionID);

		bool ItemCollectionDeleteAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName,
			int itemCollectionID);

		ItemCollection ItemCollectionUpdateAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			int itemCollectionID,
			int itemID,
			int collectionID);

		ItemCollection ItemCollectionUpdateAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName,
			int itemCollectionID,
			int itemID,
			int collectionID);

		ItemCollection ItemCollectionUpdateAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, ItemCollection value);

		ItemCollection ItemCollectionUpdateAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName, ItemCollection value);

		CustomDataAccessStatus<ItemCollection> ItemCollectionManageAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, ItemCollection value);

		CustomDataAccessStatus<ItemCollection> ItemCollectionManageAuto(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string connectionKeyName, ItemCollection value);


	}
}

