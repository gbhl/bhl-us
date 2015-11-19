
#region Using

using System;
using System.Data;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHL.DataObjects;

#endregion Using

namespace MOBOT.BHL.DAL
{
	public partial class DisqusCacheDAL
	{

        public CustomGenericList<DisqusCache> DisqusCacheSelectByItemID(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            string connectionKeyName,
            int itemID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings(connectionKeyName), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("DisqusCacheSelectByItemID", connection, transaction,
                CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemID)))
            {
                using (CustomSqlHelper<DisqusCache> helper = new CustomSqlHelper<DisqusCache>())
                {
                    CustomGenericList<DisqusCache> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }

        public bool DisqusCacheDeleteByItemID(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int itemID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("DisqusCacheDeleteByItemID", connection, transaction,
                CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemID),
                CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
            {
                int returnCode = CustomSqlHelper.ExecuteNonQuery(command, "ReturnCode");

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
    }
}
