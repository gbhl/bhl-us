using CustomDataAccess;
using MOBOT.BHL.DataObjects;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace MOBOT.BHL.DAL
{
    public partial class AuthorTypeDAL
	{
        public List<AuthorType> AuthorTypeSelectAll(SqlConnection sqlConnection, SqlTransaction sqlTransaction)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("AuthorTypeSelectAll", connection, transaction))
            {
                using (CustomSqlHelper<AuthorType> helper = new CustomSqlHelper<AuthorType>())
                {
                    List<AuthorType> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }
    }
}
