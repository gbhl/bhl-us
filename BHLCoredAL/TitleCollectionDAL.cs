using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHL.DataObjects;

namespace MOBOT.BHL.DAL
{
	public partial class TitleCollectionDAL
	{
        public List<TitleCollection> SelectByTitle(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int titleId)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleCollectionSelectByTitle", connection, transaction,
                            CustomSqlHelper.CreateInputParameter("TitleID", SqlDbType.Int, null, false, titleId)))
            {
                using (CustomSqlHelper<TitleCollection> helper = new CustomSqlHelper<TitleCollection>())
                {
                    List<TitleCollection> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }

        public int TitleCountByCollection(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int collectionId)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleCountByCollection", connection, transaction,
                            CustomSqlHelper.CreateInputParameter("CollectionID", SqlDbType.Int, null, false, collectionId)))
            {
                return (int)CustomSqlHelper.ExecuteScalar(command);
            }
        }

        public bool TitleCollectionDeleteForCollection(SqlConnection sqlConnection, 
            SqlTransaction sqlTransaction, int collectionID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command =
                CustomSqlHelper.CreateCommand("TitleCollectionDeleteForCollection", connection, sqlTransaction,
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

        public bool TitleCollectionInsertTitlesForCollection(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int collectionID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command =
                CustomSqlHelper.CreateCommand("TitleCollectionInsertTitlesForCollection", connection, sqlTransaction,
                CustomSqlHelper.CreateInputParameter("CollectionID", SqlDbType.Int, null, false, collectionID),
                CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
            {
                int returnCode = CustomSqlHelper.ExecuteNonQuery(command, "ReturnCode");
                if (transaction == null) CustomSqlHelper.CloseConnection(connection);
                return (returnCode == 0);
            }
        }

        public List<TitleCollection> TitleCollectionSelectByTitleAndCollection(
            SqlConnection sqlConnection, SqlTransaction sqlTransaction, int titleId, int collectionID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleCollectionSelectByTitleAndCollection",
                            connection, transaction,
                            CustomSqlHelper.CreateInputParameter("TitleID", SqlDbType.Int, null, false, titleId),
                            CustomSqlHelper.CreateInputParameter("CollectionID", SqlDbType.Int, null, false, collectionID)))
            {
                using (CustomSqlHelper<TitleCollection> helper = new CustomSqlHelper<TitleCollection>())
                {
                    List<TitleCollection> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }
    }
}
