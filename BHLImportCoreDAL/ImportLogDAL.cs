#region Using

using System;
using System.Data;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHLImport.DataObjects;

#endregion Using

namespace MOBOT.BHLImport.DAL
{
    public partial class ImportLogDAL
    {
        public CustomGenericList<ImportLog> ImportLogSelectRecent(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            int numLogs)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHLImport"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("ImportLogSelectRecent", connection, transaction,
                CustomSqlHelper.CreateInputParameter("NumLogs", SqlDbType.Int, null, false, numLogs)))
            {
                using (CustomSqlHelper<ImportLog> helper = new CustomSqlHelper<ImportLog>())
                {
                    return helper.ExecuteReader(command);
                }
            }
        }
    }
}
