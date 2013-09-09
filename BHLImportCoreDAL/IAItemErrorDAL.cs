#region Using

using System;
using System.Data;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHLImport.DataObjects;

#endregion Using

namespace MOBOT.BHLImport.DAL
{
    public partial class IAItemErrorDAL
    {
        public CustomGenericList<IAItemError> IAItemErrorSelectRecent(
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
