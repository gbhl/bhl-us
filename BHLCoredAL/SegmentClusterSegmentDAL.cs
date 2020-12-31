using CustomDataAccess;
using MOBOT.BHL.DataObjects;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace MOBOT.BHL.DAL
{
    public partial class SegmentClusterSegmentDAL
	{
        public SegmentClusterSegment SegmentClusterSegmentInsert(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            int segmentID, int? segmentClusterID, int isPrimary, int? segmentClusterTypeID, int? userID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("SegmentClusterSegmentInsert", connection, transaction,
                CustomSqlHelper.CreateInputParameter("SegmentID", SqlDbType.Int, null, false, segmentID),
                CustomSqlHelper.CreateInputParameter("SegmentClusterID", SqlDbType.Int, null, true, segmentClusterID),
                CustomSqlHelper.CreateInputParameter("IsPrimary", SqlDbType.SmallInt, null, false, isPrimary),
                CustomSqlHelper.CreateInputParameter("SegmentClusterTypeID", SqlDbType.Int, null, false, segmentClusterTypeID),
                CustomSqlHelper.CreateInputParameter("UserID", SqlDbType.Int, null, false, userID)))
            {
                using (CustomSqlHelper<SegmentClusterSegment> helper = new CustomSqlHelper<SegmentClusterSegment>())
                {
                    List<SegmentClusterSegment> list = helper.ExecuteReader(command);
                    if (list.Count > 0)
                        return list[0];
                    else
                        return null;
                }
            }
        }

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
                    List<SegmentClusterSegment> list = helper.ExecuteReader(command);
                    if (list.Count > 0)
                        return list[0];
                    else
                        return null;
                }
            }
        }

        public void SegmentClusterSegmentDeleteForSegment(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            int segmentID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("SegmentClusterSegmentDeleteForSegment", connection, transaction,
                CustomSqlHelper.CreateInputParameter("SegmentID", SqlDbType.Int, null, false, segmentID)))
            {
                command.ExecuteNonQuery();
            }
        }
    }
}
