using System;
using System.Data;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHL.DataObjects;

namespace MOBOT.BHL.DAL
{
	public partial class SegmentClusterSegmentDAL
	{
        public SegmentClusterSegment SegmentClusterSegmentUpdate(SqlConnection sqlConnection, SqlTransaction sqlTransaction, 
            int segmentID, int? segmentClusterID, int isPrimary, int? userID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("SegmentClusterSegmentUpdate", connection, transaction,
                CustomSqlHelper.CreateInputParameter("SegmentID", SqlDbType.Int, null, false, segmentID),
                CustomSqlHelper.CreateInputParameter("SegmentClusterID", SqlDbType.Int, null, false, segmentClusterID),
                CustomSqlHelper.CreateInputParameter("IsPrimary", SqlDbType.SmallInt, null, false, isPrimary),
                CustomSqlHelper.CreateInputParameter("UserID", SqlDbType.Int, null, false, userID)))
            {
                using (CustomSqlHelper<SegmentClusterSegment> helper = new CustomSqlHelper<SegmentClusterSegment>())
                {
                    CustomGenericList<SegmentClusterSegment> list = helper.ExecuteReader(command);
                    if (list.Count > 0)
                        return list[0];
                    else
                        return null;
                }
            }
        }
    }
}
