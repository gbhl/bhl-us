using System;
using System.Data;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHL.DataObjects;

namespace MOBOT.BHL.DAL
{
	public partial class SegmentPageDAL
	{
        public CustomGenericList<SegmentPage> SegmentPageSelectBySegmentID(SqlConnection sqlConnection, 
            SqlTransaction sqlTransaction, int segmentId)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("SegmentPageSelectBySegmentID", connection, transaction,
                CustomSqlHelper.CreateInputParameter("SegmentID", SqlDbType.Int, null, false, segmentId)))
            {
                using (CustomSqlHelper<SegmentPage> helper = new CustomSqlHelper<SegmentPage>())
                {
                    CustomGenericList<SegmentPage> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }
	}
}
