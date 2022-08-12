
#region Using

using CustomDataAccess;
using MOBOT.BHL.DataObjects;
using System.Collections.Generic;
using System.Data.SqlClient;

#endregion Using

namespace MOBOT.BHL.DAL
{
    public partial class SegmentStatusDAL
	{
        public List<ItemStatus> SegmentStatusSelectAll(SqlConnection sqlConnection, SqlTransaction sqlTransaction)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("SegmentStatusSelectAll", connection, transaction))
            {
                using (CustomSqlHelper<ItemStatus> helper = new CustomSqlHelper<ItemStatus>())
                {
                    List<ItemStatus> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }

    }
}
