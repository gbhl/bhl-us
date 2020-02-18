#region Using

using CustomDataAccess;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

#endregion Using

namespace MOBOT.BHLImport.DAL
{
    public partial class ImportLogDAL
    {
        public List<CustomDataRow> ImportLogSelectRecent(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            int numLogs)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHLImport"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("ImportLogSelectRecent", connection, transaction,
                CustomSqlHelper.CreateInputParameter("NumLogs", SqlDbType.Int, null, false, numLogs)))
            {
                return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
/*
                using (CustomSqlHelper<CustomDataRow> helper = new CustomSqlHelper<CustomDataRow>())
                {
                    return helper.ExecuteReader(command);
                }
                */
            }
        }
    }
}
