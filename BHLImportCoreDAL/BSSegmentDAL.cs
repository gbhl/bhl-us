
#region Using

using CustomDataAccess;
using MOBOT.BHLImport.DataObjects;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

#endregion Using

namespace MOBOT.BHLImport.DAL
{
    public partial class BSSegmentDAL
    {
        public List<BSSegment> BSSegmentSelectByItem(SqlConnection sqlConnection, SqlTransaction sqlTransaction, int itemId)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHLImport"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("dbo.BSSegmentSelectByItem", connection, transaction,
                CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemId)))
            {
                using (CustomSqlHelper<BSSegment> helper = new CustomSqlHelper<BSSegment>())
                {
                    List<BSSegment> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }

        public List<BSSegment> BSSegmentSelectHarvestedByItem(SqlConnection sqlConnection, SqlTransaction sqlTransaction, int itemId)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHLImport"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;
            using (SqlCommand command = CustomSqlHelper.CreateCommand("dbo.BSSegmentSelectHarvestedByItem", connection, transaction,
                CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemId)))
            {
                using (CustomSqlHelper<BSSegment> helper = new CustomSqlHelper<BSSegment>())
                {
                    List<BSSegment> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }

        public void BSSegmentResolveAuthors(SqlConnection sqlConnection, SqlTransaction sqlTransaction, int segmentID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHLImport"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;
            using (SqlCommand command = CustomSqlHelper.CreateCommand("dbo.BSSegmentResolveAuthors", connection, transaction,
                CustomSqlHelper.CreateInputParameter("SegmentID", SqlDbType.Int, null, false, segmentID)))
            {
                command.ExecuteNonQuery();
            }
        }

        public void BSSegmentPublishToProduction(SqlConnection sqlConnection, SqlTransaction sqlTransaction, int itemID, int segmentID, int statusID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHLImport"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;
            using (SqlCommand command = CustomSqlHelper.CreateCommand("dbo.BSSegmentPublishToProduction", connection, transaction,
                CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemID),
                CustomSqlHelper.CreateInputParameter("SegmentID", SqlDbType.Int, null, false, segmentID),
                CustomSqlHelper.CreateInputParameter("@SegmentStatusID", SqlDbType.Int, null, false, statusID)))
            {
                command.ExecuteNonQuery();
            }
        }
    }
}

