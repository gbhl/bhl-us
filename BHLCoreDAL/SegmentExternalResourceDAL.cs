
#region Using

using CustomDataAccess;
using MOBOT.BHL.DataObjects;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

#endregion Using

namespace MOBOT.BHL.DAL
{
	public partial class SegmentExternalResourceDAL
	{
        public List<SegmentExternalResource> SegmentExternalResourceSelectBySegmentID(SqlConnection sqlConnection,
         SqlTransaction sqlTransaction, int segmentID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("SegmentExternalResourceSelectBySegmentID", connection, transaction,
                CustomSqlHelper.CreateInputParameter("SegmentID", SqlDbType.Int, null, false, segmentID)))
            {
                using (CustomSqlHelper<SegmentExternalResource> helper = new CustomSqlHelper<SegmentExternalResource>())
                {
                    List<SegmentExternalResource> list = helper.ExecuteReader(command);

                    return list;
                }
            }
        }
    }
}

