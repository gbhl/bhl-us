
#region Using

using System;
using System.Data;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHL.DataObjects;

#endregion Using

namespace MOBOT.BHL.DAL
{
	public partial class SegmentStatusDAL
	{
        public CustomGenericList<ItemStatus> SegmentStatusSelectAll(SqlConnection sqlConnection, SqlTransaction sqlTransaction)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("SegmentStatusSelectAll", connection, transaction))
            {
                using (CustomSqlHelper<ItemStatus> helper = new CustomSqlHelper<ItemStatus>())
                {
                    CustomGenericList<ItemStatus> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }

    }
}
