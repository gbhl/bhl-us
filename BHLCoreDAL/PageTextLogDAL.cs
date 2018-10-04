
using CustomDataAccess;
using MOBOT.BHL.DataObjects;
using System.Data;
using System.Data.SqlClient;

namespace MOBOT.BHL.DAL
{
    public partial class PageTextLogDAL
	{
        public CustomGenericList<PageTextLog> PageTextLogSelectForItem(
                SqlConnection sqlConnection,
                SqlTransaction sqlTransaction,
                int itemID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("PageTextLogSelectForItem", connection, transaction,
                    CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemID)))
            {
                using (CustomSqlHelper<PageTextLog> helper = new CustomSqlHelper<PageTextLog>())
                {
                    CustomGenericList<PageTextLog> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }

        public void PageTextLogInsertForItem(
                SqlConnection sqlConnection,
                SqlTransaction sqlTransaction,
                int itemID,
                string textSource,
                int userID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("PageTextLogInsertForItem", connection, transaction,
                    CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemID),
                    CustomSqlHelper.CreateInputParameter("TextSource", SqlDbType.NVarChar, 50, false, textSource),
                    CustomSqlHelper.CreateInputParameter("UserID", SqlDbType.Int, null, false, userID)))
            {
                command.ExecuteNonQuery();
            }
        }

        public CustomGenericList<PageTextLog> PageTextLogSelectNonOCRForItem(
                SqlConnection sqlConnection,
                SqlTransaction sqlTransaction,
                int itemID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("PageTextLogSelectNonOCRForItem", connection, transaction,
                    CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemID)))
            {
                using (CustomSqlHelper<PageTextLog> helper = new CustomSqlHelper<PageTextLog>())
                {
                    CustomGenericList<PageTextLog> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }
    }
}

