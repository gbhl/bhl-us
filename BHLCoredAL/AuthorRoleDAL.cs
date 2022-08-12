using CustomDataAccess;
using MOBOT.BHL.DataObjects;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace MOBOT.BHL.DAL
{
    public partial class AuthorRoleDAL
	{
        public List<AuthorRole> AuthorRoleSelectAll(SqlConnection sqlConnection,SqlTransaction sqlTransaction)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("AuthorRoleSelectAll", connection, transaction))
            {
                using (CustomSqlHelper<AuthorRole> helper = new CustomSqlHelper<AuthorRole>())
                {
                    List<AuthorRole> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }
    }
}
