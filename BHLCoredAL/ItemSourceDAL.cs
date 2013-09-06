
#region Using

using System.Data;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHL.DataObjects;

#endregion Using

namespace MOBOT.BHL.DAL
{
	public partial class ItemSourceDAL
	{
        public CustomGenericList<ItemSource> ItemSourceSelectAll(SqlConnection sqlConnection, SqlTransaction sqlTransaction)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
              CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("ItemSourceSelectAll", connection, transaction))
            {
                using (CustomSqlHelper<ItemSource> helper = new CustomSqlHelper<ItemSource>())
                {
                    CustomGenericList<ItemSource> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }

    }
}
