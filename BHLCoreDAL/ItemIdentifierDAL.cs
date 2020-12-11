
#region Using

using CustomDataAccess;
using MOBOT.BHL.DataObjects;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

#endregion Using

namespace MOBOT.BHL.DAL
{
    public partial class ItemIdentifierDAL
	{
        public List<ItemIdentifier> ItemIdentifierSelectBySegmentID(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int segmentID, int? display)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("SegmentIdentifierSelectBySegmentID", connection, transaction,
                CustomSqlHelper.CreateInputParameter("SegmentID", SqlDbType.Int, null, false, segmentID),
                CustomSqlHelper.CreateInputParameter("Display", SqlDbType.Int, null, true, display)))
            {
                using (CustomSqlHelper<ItemIdentifier> helper = new CustomSqlHelper<ItemIdentifier>())
                {
                    List<ItemIdentifier> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }
    }
}

