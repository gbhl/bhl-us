
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
        public CustomGenericList<SegmentStatus> SegmentStatusSelectAll(SqlConnection sqlConnection, SqlTransaction sqlTransaction)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("SegmentStatusSelectAll", connection, transaction))
            {
                using (CustomSqlHelper<SegmentStatus> helper = new CustomSqlHelper<SegmentStatus>())
                {
                    CustomGenericList<SegmentStatus> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }

    }
}
