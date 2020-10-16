
#region Using

using CustomDataAccess;
using MOBOT.BHL.DataObjects;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

#endregion Using

namespace MOBOT.BHL.DAL
{
    public partial class ItemAuthorDAL
	{
        public List<ItemAuthor> ItemAuthorSelectBySegmentID(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int segmentID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("SegmentAuthorSelectBySegmentID", connection, transaction,
                CustomSqlHelper.CreateInputParameter("SegmentID", SqlDbType.Int, null, false, segmentID)))
            {
                using (CustomSqlHelper<ItemAuthor> helper = new CustomSqlHelper<ItemAuthor>())
                {
                    List<ItemAuthor> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }

        /// <summary>
        /// Update ItemAuthor records with a specified Author ID to a new Author ID
        /// </summary>
        /// <param name="sqlConnection"></param>
        /// <param name="sqlTransaction"></param>
        /// <param name="fromAuthorId"></param>
        /// <param name="toAuthorId"></param>
        /// <returns></returns>
        public void ItemAuthorUpdateAuthorID(SqlConnection sqlConnection,
                SqlTransaction sqlTransaction, int fromAuthorId, int toAuthorId, int userId)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("SegmentAuthorUpdateAuthorID", connection, transaction,
                    CustomSqlHelper.CreateInputParameter("FromAuthorID", SqlDbType.Int, null, false, fromAuthorId),
                    CustomSqlHelper.CreateInputParameter("ToAuthorID", SqlDbType.Int, null, false, toAuthorId),
                    CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, false, userId)))
            {
                CustomSqlHelper.ExecuteNonQuery(command);
            }
        }
    }
}

