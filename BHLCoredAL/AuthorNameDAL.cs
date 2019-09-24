using System;
using System.Data;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHL.DataObjects;

namespace MOBOT.BHL.DAL
{
	public partial class AuthorNameDAL
	{
        public CustomGenericList<AuthorName> AuthorNameSelectByAuthorID(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            int authorId)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("AuthorNameSelectByAuthorID", connection, transaction,
                CustomSqlHelper.CreateInputParameter("AuthorID", SqlDbType.Int, null, false, authorId)))
            {
                using (CustomSqlHelper<AuthorName> helper = new CustomSqlHelper<AuthorName>())
                {
                    CustomGenericList<AuthorName> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }

        public void AuthorNameInsertFromAuthorID(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            int fromAuthorId, int toAuthorId, int userId)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("AuthorNameInsertFromAuthorID", connection, transaction,
                CustomSqlHelper.CreateInputParameter("FromAuthorID", SqlDbType.Int, null, false, fromAuthorId),
                CustomSqlHelper.CreateInputParameter("ToAuthorID", SqlDbType.Int, null, false, toAuthorId),
                CustomSqlHelper.CreateInputParameter("UserID", SqlDbType.Int, null, false, userId)))
            {
                CustomSqlHelper.ExecuteNonQuery(command);
            }
        }
    }
}
