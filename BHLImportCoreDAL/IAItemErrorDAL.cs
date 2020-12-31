#region Using

using CustomDataAccess;
using MOBOT.BHLImport.DataObjects;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

#endregion Using

namespace MOBOT.BHLImport.DAL
{
    public partial class IAItemErrorDAL
    {
        public List<IAItemError> IAItemErrorSelectRecent(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            int numErrors)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHLImport"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("IAItemErrorSelectRecent", connection, transaction,
                CustomSqlHelper.CreateInputParameter("NumErrors", SqlDbType.Int, null, false, numErrors)))
            {
                using (CustomSqlHelper<IAItemError> helper = new CustomSqlHelper<IAItemError>())
                {
                    return helper.ExecuteReader(command);
                }
            }
        }
    }
}
