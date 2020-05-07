using CustomDataAccess;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace MOBOT.BHL.DAL
{
    public partial class AspNetUserDAL
    {
        public List<CustomDataRow> AspNetUserSelectAll(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("AspNetUserSelectAll", connection, transaction))
            {
                return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
            }
        }
    }
}
