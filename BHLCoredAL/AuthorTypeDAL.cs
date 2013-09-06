using System;
using System.Data;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHL.DataObjects;

namespace MOBOT.BHL.DAL
{
	public partial class AuthorTypeDAL
	{
        public CustomGenericList<AuthorType> AuthorTypeSelectAll(SqlConnection sqlConnection, SqlTransaction sqlTransaction)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("AuthorTypeSelectAll", connection, transaction))
            {
                using (CustomSqlHelper<AuthorType> helper = new CustomSqlHelper<AuthorType>())
                {
                    CustomGenericList<AuthorType> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }
    }
}
