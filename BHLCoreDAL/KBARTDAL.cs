using CustomDataAccess;
using MOBOT.BHL.DataObjects;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace MOBOT.BHL.DAL
{
    public partial class KBARTDAL
    {
        public List<KBART> Export(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string urlRoot)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("ExportKBART", connection, transaction,
                CustomSqlHelper.CreateInputParameter("UrlRoot", SqlDbType.NVarChar, 200, false, urlRoot)))
            {
                using (CustomSqlHelper<KBART> helper = new CustomSqlHelper<KBART>())
                {
                    return helper.ExecuteReader(command);
                }
            }
        }
    }
}
