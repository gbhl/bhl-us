using System;
using System.Data;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHL.DataObjects;

namespace MOBOT.BHL.DAL
{
	public partial class AuthorRoleDAL
	{
        public CustomGenericList<AuthorRole> AuthorRoleSelectAll(SqlConnection sqlConnection,SqlTransaction sqlTransaction)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("AuthorRoleSelectAll", connection, transaction))
            {
                using (CustomSqlHelper<AuthorRole> helper = new CustomSqlHelper<AuthorRole>())
                {
                    CustomGenericList<AuthorRole> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }
    }
}
