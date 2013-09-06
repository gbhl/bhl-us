#region using

using System.Data;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHL.DataObjects;

#endregion using

namespace MOBOT.BHL.DAL
{
    public partial class ItemCOinSDAL
    {
        public ItemCOinSView ItemCOinSSelectByTitleId(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
         int titleId)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
              CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("ItemCOinSSelectByTitleId", connection, transaction,
                CustomSqlHelper.CreateInputParameter("TitleID", SqlDbType.Int, null, false, titleId)))
            {
                using (CustomSqlHelper<ItemCOinSView> helper = new CustomSqlHelper<ItemCOinSView>())
                {
                    CustomGenericList<ItemCOinSView> list = helper.ExecuteReader(command);
                    if (list.Count > 0)
                    {
                        return list[0];
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        public ItemCOinSView ItemCOinSSelectByItemId(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
         int itemId)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
              CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("ItemCOinSSelectByItemId", connection, transaction,
                CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemId)))
            {
                using (CustomSqlHelper<ItemCOinSView> helper = new CustomSqlHelper<ItemCOinSView>())
                {
                    CustomGenericList<ItemCOinSView> list = helper.ExecuteReader(command);
                    if (list.Count > 0)
                    {
                        return list[0];
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
    }
}
