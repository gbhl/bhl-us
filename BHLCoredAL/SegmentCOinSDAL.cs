using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHL.DataObjects;

namespace MOBOT.BHL.DAL
{
    public partial class SegmentCOinSDAL
    {
        public SegmentCOinSView SegmentCOinSSelectBySegmentId(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
         int segmentId)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
              CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("SegmentCOinSSelectBySegmentID", connection, transaction,
                CustomSqlHelper.CreateInputParameter("SegmentID", SqlDbType.Int, null, false, segmentId)))
            {
                using (CustomSqlHelper<SegmentCOinSView> helper = new CustomSqlHelper<SegmentCOinSView>())
                {
                    List<SegmentCOinSView> list = helper.ExecuteReader(command);
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
