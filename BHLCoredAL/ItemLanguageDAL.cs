using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHL.DataObjects;

namespace MOBOT.BHL.DAL
{
	public partial class ItemLanguageDAL
	{
        public List<ItemLanguage> ItemLanguageSelectByItemID(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int itemId)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("ItemLanguageSelectByItemID", connection, transaction,
                            CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemId)))
            {
                using (CustomSqlHelper<ItemLanguage> helper = new CustomSqlHelper<ItemLanguage>())
                {
                    List<ItemLanguage> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }
    }
}
