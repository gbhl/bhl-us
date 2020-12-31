
#region Using

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHLImport.DataObjects;

#endregion Using

namespace MOBOT.BHLImport.DAL
{
	public partial class BotanicusHarvestLogDAL
	{
        /// <summary>
        /// Select the most recent end date for automated Botanicus harvests.
        /// </summary>
        /// <param name="sqlConnection">Sql connection or null.</param>
        /// <param name="sqlTransaction">Sql transaction or null.</param>
        /// <param name="marcBibID"></param>
        /// <param name="importSourceID"></param>
        /// <returns>Object of type Title.</returns>
        public DateTime BotanicusHarvestLogSelectLatestEndDate(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHLImport"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("BotanicusHarvestLogSelectLatestEndDate", connection, transaction))
            {
                DateTime result = Convert.ToDateTime(CustomSqlHelper.ExecuteScalar(command));
                return result;
            }
        }

        public List<BotanicusHarvestLog> BotanicusHarvestLogSelectRecent(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            int numLogs)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHLImport"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("BotanicusHarvestLogSelectRecent", connection, transaction,
                CustomSqlHelper.CreateInputParameter("NumLogs", SqlDbType.Int, null, false, numLogs)))
            {
                using (CustomSqlHelper<BotanicusHarvestLog> helper = new CustomSqlHelper<BotanicusHarvestLog>())
                {
                    return helper.ExecuteReader(command);
                }
            }
        }
    }
}
