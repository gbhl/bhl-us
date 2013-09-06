using System;
using System.Data;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHL.DataObjects;

namespace MOBOT.BHL.DAL
{
	public partial class CollectionDAL
	{
        public CustomGenericList<Collection> SelectAll(SqlConnection sqlConnection, SqlTransaction sqlTransaction)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("CollectionSelectAll", connection, transaction))
            {
                using (CustomSqlHelper<Collection> helper = new CustomSqlHelper<Collection>())
                {
                    CustomGenericList<Collection> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }

        public CustomGenericList<Collection> SelectActive(SqlConnection sqlConnection, SqlTransaction sqlTransaction)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("CollectionSelectActive", connection, transaction))
            {
                using (CustomSqlHelper<Collection> helper = new CustomSqlHelper<Collection>())
                {
                    CustomGenericList<Collection> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }

        public Collection SelectFeatured(SqlConnection sqlConnection, SqlTransaction sqlTransaction)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("CollectionSelectFeatured", connection, transaction))
            {
                using (CustomSqlHelper<Collection> helper = new CustomSqlHelper<Collection>())
                {
                    CustomGenericList<Collection> list = helper.ExecuteReader(command);
                    return (list.Count > 0 ? list[0] : null);
                }
            }
        }

        public CustomGenericList<Collection> SelectByContents(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int canContainTitles, int canContainItems)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("CollectionSelectByContents", connection, transaction,
                CustomSqlHelper.CreateInputParameter("CanContainTitles", SqlDbType.SmallInt, null, false, canContainTitles),
                CustomSqlHelper.CreateInputParameter("CanContainItems", SqlDbType.SmallInt, null, false, canContainItems)))
            {
                using (CustomSqlHelper<Collection> helper = new CustomSqlHelper<Collection>())
                {
                    CustomGenericList<Collection> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }

        public CustomGenericList<Collection> CollectionSelectByNameAndAllowedContent(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, string collectionName, short canContainTitles, short canContainItems)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("CollectionSelectByNameAndAllowedContent", 
                connection, transaction,
                CustomSqlHelper.CreateInputParameter("CollectionName", SqlDbType.NVarChar, 50, false, collectionName),
                CustomSqlHelper.CreateInputParameter("CanContainTitles", SqlDbType.SmallInt, null, false, canContainTitles),
                CustomSqlHelper.CreateInputParameter("CanContainItems", SqlDbType.SmallInt, null, false, canContainItems)))
            {
                using (CustomSqlHelper<Collection> helper = new CustomSqlHelper<Collection>())
                {
                    CustomGenericList<Collection> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }

        public CustomGenericList<Collection> CollectionSelectByUrl(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, string collectionUrl)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("CollectionSelectByUrl",
                connection, transaction,
                CustomSqlHelper.CreateInputParameter("CollectionURL", SqlDbType.NVarChar, 50, false, collectionUrl)))
            {
                using (CustomSqlHelper<Collection> helper = new CustomSqlHelper<Collection>())
                {
                    CustomGenericList<Collection> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }

        public CustomGenericList<Collection> CollectionSelectByNameLike(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string collectionName)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("CollectionSelectByNameLike", connection, transaction,
                            CustomSqlHelper.CreateInputParameter("CollectionName", SqlDbType.VarChar, 50, false, collectionName)))
            {
                using (CustomSqlHelper<Collection> helper = new CustomSqlHelper<Collection>())
                {
                    CustomGenericList<Collection> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }

        /// <summary>
        /// Select all collections related to the specified title or its related items
        /// </summary>
        /// <param name="sqlConnection"></param>
        /// <param name="sqlTransaction"></param>
        /// <param name="titleID"></param>
        /// <returns>List of Collection</returns>
        public CustomGenericList<Collection> CollectionSelectAllForTitle(SqlConnection sqlConnection, SqlTransaction sqlTransaction, 
            int titleID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("CollectionSelectAllForTitle", connection, transaction,
                            CustomSqlHelper.CreateInputParameter("TitleID", SqlDbType.Int, null, false, titleID)))
            {
                using (CustomSqlHelper<Collection> helper = new CustomSqlHelper<Collection>())
                {
                    CustomGenericList<Collection> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }

        private void CollectionClearOtherFeatured(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            int collectionID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("CollectionClearOtherFeatured", connection, transaction,
                            CustomSqlHelper.CreateInputParameter("CollectionID", SqlDbType.Int, null, false, collectionID)))
            {
                command.ExecuteNonQuery();
            }
        }

        public void Save(SqlConnection sqlConnection, SqlTransaction sqlTransaction, Collection collection)
        {
            SqlConnection connection = sqlConnection;
            SqlTransaction transaction = sqlTransaction;

            if (connection == null)
            {
                connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"));
            }
            bool isTransactionCoordinator = CustomSqlHelper.IsTransactionCoordinator(transaction);

            try
            {
                transaction = CustomSqlHelper.BeginTransaction(connection, transaction, isTransactionCoordinator);
                CustomDataAccessStatus<Collection> collectionStatus = new CollectionDAL().CollectionManageAuto(connection, transaction, collection);
                if (collectionStatus.IsInserted)
                {
                    if (collectionStatus.ReturnObject.CanContainTitles == 1)
                    {
                        // New Title collection, so add titles to the collection (if institution or language were specified)
                        new TitleCollectionDAL().TitleCollectionInsertTitlesForCollection(
                            connection, transaction, collectionStatus.ReturnObject.CollectionID);
                    }
                    if (collectionStatus.ReturnObject.CanContainItems == 1)
                    {
                        // New Item collection, so add items to the collection (if institution or language were specified)
                        new ItemCollectionDAL().ItemCollectionInsertItemsForCollection(
                            connection, transaction, collectionStatus.ReturnObject.CollectionID);
                    }
                }
                // Make sure this is the only featured collection
                if (collection.Featured == 1)
                {
                    this.CollectionClearOtherFeatured(connection, transaction, 
                        (collectionStatus.IsInserted ? collectionStatus.ReturnObject.CollectionID : collection.CollectionID));
                }

                CustomSqlHelper.CommitTransaction(transaction, isTransactionCoordinator);
            }
            catch (Exception ex)
            {
                CustomSqlHelper.RollbackTransaction(transaction, isTransactionCoordinator);
                throw;
            }
            finally
            {
                CustomSqlHelper.CloseConnection(connection, isTransactionCoordinator);
            }

        }
    }
}
