using CustomDataAccess;
using MOBOT.BHL.DataObjects;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace MOBOT.BHL.DAL
{
    public partial class AuthorIdentifierDAL
	{
        public List<AuthorIdentifier> AuthorIdentifierSelectByAuthorID(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            int authorId)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("AuthorIdentifierSelectByAuthorID", connection, transaction,
                CustomSqlHelper.CreateInputParameter("AuthorID", SqlDbType.Int, null, false, authorId)))
            {
                using (CustomSqlHelper<AuthorIdentifier> helper = new CustomSqlHelper<AuthorIdentifier>())
                {
                    List<AuthorIdentifier> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }

        public void AuthorIdentifierUpdateAuthorID(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            int fromAuthorId, int toAuthorId, int userId)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("AuthorIdentifierUpdateAuthorID", connection, transaction,
                CustomSqlHelper.CreateInputParameter("FromAuthorID", SqlDbType.Int, null, false, fromAuthorId),
                CustomSqlHelper.CreateInputParameter("ToAuthorID", SqlDbType.Int, null, false, toAuthorId),
                CustomSqlHelper.CreateInputParameter("UserID", SqlDbType.Int, null, false, userId)))
            {
                CustomSqlHelper.ExecuteNonQuery(command);
            }
        }
    }
}
