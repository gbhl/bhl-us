
#region Using

using CustomDataAccess;
using MOBOT.BHL.DataObjects;
using System.Collections.Generic;
using System.Data.SqlClient;

#endregion Using

namespace MOBOT.BHL.DAL
{
    public partial class ItemSourceDAL
	{
        public List<ItemSource> ItemSourceSelectAll(SqlConnection sqlConnection, SqlTransaction sqlTransaction)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
              CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("ItemSourceSelectAll", connection, transaction))
            {
                using (CustomSqlHelper<ItemSource> helper = new CustomSqlHelper<ItemSource>())
                {
                    List<ItemSource> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }

    }
}
