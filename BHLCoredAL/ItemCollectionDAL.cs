using System.Data;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHL.DataObjects;

namespace MOBOT.BHL.DAL
{
	public partial class ItemCollectionDAL
	{
        public CustomGenericList<ItemCollection> SelectByItem(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int itemId)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("ItemCollectionSelectByItem", connection, transaction,
                            CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemId)))
            {
                using (CustomSqlHelper<ItemCollection> helper = new CustomSqlHelper<ItemCollection>())
                {
                    CustomGenericList<ItemCollection> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }

        public int ItemCountByCollection(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int collectionId)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("ItemCountByCollection", connection, transaction,
                            CustomSqlHelper.CreateInputParameter("CollectionID", SqlDbType.Int, null, false, collectionId)))
            {
                return (int)CustomSqlHelper.ExecuteScalar(command);
            }
        }


        public bool ItemCollectionDeleteForCollection(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int collectionID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command =
                CustomSqlHelper.CreateCommand("ItemCollectionDeleteForCollection", connection, sqlTransaction,
                CustomSqlHelper.CreateInputParameter("CollectionID", SqlDbType.Int, null, false, collectionID),
                CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
            {
                int returnCode = CustomSqlHelper.ExecuteNonQuery(command, "ReturnCode");

                if (transaction == null)
                {
                    CustomSqlHelper.CloseConnection(connection);
                }

                if (returnCode == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool ItemCollectionInsertItemsForCollection(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int collectionID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command =
                CustomSqlHelper.CreateCommand("ItemCollectionInsertItemsForCollection", connection, sqlTransaction,
                CustomSqlHelper.CreateInputParameter("CollectionID", SqlDbType.Int, null, false, collectionID),
                CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
            {
                int returnCode = CustomSqlHelper.ExecuteNonQuery(command, "ReturnCode");
                if (transaction == null) CustomSqlHelper.CloseConnection(connection);
                return (returnCode == 0);
            }
        }

        public CustomGenericList<ItemCollection> ItemCollectionSelectByItemAndCollection(
            SqlConnection sqlConnection, SqlTransaction sqlTransaction, int itemId, int collectionID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("ItemCollectionSelectByItemAndCollection", 
                            connection, transaction,
                            CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemId),
                            CustomSqlHelper.CreateInputParameter("CollectionID", SqlDbType.Int, null, false, collectionID)))
            {
                using (CustomSqlHelper<ItemCollection> helper = new CustomSqlHelper<ItemCollection>())
                {
                    CustomGenericList<ItemCollection> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }
    }
}
