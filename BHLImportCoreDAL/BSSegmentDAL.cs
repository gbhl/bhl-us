
#region Using

using System;
using System.Data;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHLImport.DataObjects;

#endregion Using

namespace MOBOT.BHLImport.DAL
{
	public partial class BSSegmentDAL
	{
        public CustomGenericList<BSSegment> BSSegmentSelectByItem(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int itemId)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHLImport"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("BSSegmentSelectByItem", connection, transaction,
                CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemId)))
            {
                using (CustomSqlHelper<BSSegment> helper = new CustomSqlHelper<BSSegment>())
                {
                    CustomGenericList<BSSegment> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }
    }
}

