using CustomDataAccess;
using MOBOT.BHL.DataObjects;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace MOBOT.BHL.DAL
{
    public partial class TitleAuthorDAL
    {
        public List<TitleAuthor> TitleAuthorSelectByTitle(SqlConnection sqlConnection,
                SqlTransaction sqlTransaction, int titleID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleAuthorSelectByTitle", connection, transaction,
                    CustomSqlHelper.CreateInputParameter("TitleID", SqlDbType.Int, null, false, titleID)))
            {
                using (CustomSqlHelper<TitleAuthor> helper = new CustomSqlHelper<TitleAuthor>())
                {
                    List<TitleAuthor> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }

        /// <summary>
        /// Update TitleAuthor records with a specified Author ID to a new Author ID
        /// </summary>
        /// <param name="sqlConnection"></param>
        /// <param name="sqlTransaction"></param>
        /// <param name="fromAuthorId"></param>
        /// <param name="toAuthorId"></param>
        /// <returns></returns>
        public void TitleAuthorUpdateAuthorID(SqlConnection sqlConnection,
                SqlTransaction sqlTransaction, int fromAuthorId, int toAuthorId, int userId)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleAuthorUpdateAuthorID", connection, transaction,
                    CustomSqlHelper.CreateInputParameter("FromAuthorID", SqlDbType.Int, null, false, fromAuthorId),
                    CustomSqlHelper.CreateInputParameter("ToAuthorID", SqlDbType.Int, null, false, toAuthorId),
                    CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, false, userId)))
            {
                CustomSqlHelper.ExecuteNonQuery(command);
            }
        }
    }
}
