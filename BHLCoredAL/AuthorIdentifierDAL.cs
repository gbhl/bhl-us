using System;
using System.Data;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHL.DataObjects;

namespace MOBOT.BHL.DAL
{
	public partial class AuthorIdentifierDAL
	{
        public CustomGenericList<AuthorIdentifier> AuthorIdentifierSelectByAuthorID(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
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
                    CustomGenericList<AuthorIdentifier> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }
    }
}
