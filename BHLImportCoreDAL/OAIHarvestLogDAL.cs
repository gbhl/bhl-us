
#region Using

using System;
using System.Data.SqlClient;
using CustomDataAccess;
using System.Data;

#endregion Using

namespace MOBOT.BHLImport.DAL
{
	public partial class OAIHarvestLogDAL
	{
        public DateTime OAIHarvestLogSelectLastDateForHarvestSet(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            int harvestSetID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHLImport"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("OAIHarvestLogSelectLastDateForHarvestSet", connection, transaction,
                CustomSqlHelper.CreateInputParameter("HarvestSetID", SqlDbType.Int, null, true, harvestSetID)))
            {
                using (CustomSqlHelper<DateTime> helper = new CustomSqlHelper<DateTime>())
                {
                    CustomGenericList<DateTime> k = helper.ExecuteReader(command);

                    return k[0];
                }
            }
        }

	}
}
