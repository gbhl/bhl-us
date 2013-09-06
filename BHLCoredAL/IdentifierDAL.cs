using System;
using System.Data;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHL.DataObjects;

namespace MOBOT.BHL.DAL
{
	public partial class IdentifierDAL
	{
        public CustomGenericList<Identifier> IdentifierSelectAll(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("IdentifierSelectAll", connection, transaction))
            {
                using (CustomSqlHelper<Identifier> helper = new CustomSqlHelper<Identifier>())
                {
                    CustomGenericList<Identifier> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }
    }
}
