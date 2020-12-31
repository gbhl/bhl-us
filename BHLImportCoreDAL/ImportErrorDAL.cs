#region Using

using CustomDataAccess;
using MOBOT.BHLImport.DataObjects;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

#endregion Using

namespace MOBOT.BHLImport.DAL
{
    public partial class ImportErrorDAL
    {
        public List<ImportError> ImportErrorSelectRecent(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            int numErrors)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHLImport"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("ImportErrorSelectRecent", connection, transaction,
                CustomSqlHelper.CreateInputParameter("NumErrors", SqlDbType.Int, null, false, numErrors)))
            {
                using (CustomSqlHelper<ImportError> helper = new CustomSqlHelper<ImportError>())
                {
                    return helper.ExecuteReader(command);
                }
            }
        }
    }
}
