using System;
using System.Data;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHL.DataObjects;

namespace MOBOT.BHL.DAL
{
	public partial class PageFlickrDAL
	{
        public void PageFlickrSave(SqlConnection sqlConnection, SqlTransaction sqlTransaction, PageFlickr pageFlickr, int userId)
        {
            SqlConnection connection = sqlConnection;
            SqlTransaction transaction = sqlTransaction;

            if (connection == null)
            {
                connection =
                  CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"));
            }

            bool isTransactionCoordinator = CustomSqlHelper.IsTransactionCoordinator(transaction);

            try
            {
                transaction = CustomSqlHelper.BeginTransaction(connection, transaction, isTransactionCoordinator);

                new PageFlickrDAL().PageFlickrManageAuto(connection, transaction, pageFlickr, userId);

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

        public PageFlickr PageFlickrSelectByPage(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            int pageId)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
              CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("PageFlickrSelectByPage", connection, transaction,
                CustomSqlHelper.CreateInputParameter("PageID", SqlDbType.Int, null, false, pageId)))
            {
                using (CustomSqlHelper<PageFlickr> helper = new CustomSqlHelper<PageFlickr>())
                {
                    CustomGenericList<PageFlickr> list = helper.ExecuteReader(command);
                    if (list != null && list.Count > 0)
                        return list[0];
                    else
                        return null;
                }
            }
        }

        public CustomGenericList<PageFlickr> PageFlickrSelectRandom(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            int numberToReturn)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;
            using (SqlCommand command = CustomSqlHelper.CreateCommand("PageFlickrSelectRandom", connection, transaction,
                    CustomSqlHelper.CreateInputParameter("NumToReturn", SqlDbType.Int, null, false, numberToReturn)))
            {
                using (CustomSqlHelper<PageFlickr> helper = new CustomSqlHelper<PageFlickr>())
                {
                    CustomGenericList<PageFlickr> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }
	}
}
