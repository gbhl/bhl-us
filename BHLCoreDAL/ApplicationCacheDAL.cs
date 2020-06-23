using CustomDataAccess;
using MOBOT.BHL.DataObjects;
using System.Data;
using System.Data.SqlClient;

namespace MOBOT.BHL.DAL
{
    public partial class ApplicationCacheDAL
	{
		public ApplicationCache ApplicationCacheSelectByKey (
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            string key)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("ApplicationCacheSelectByKey", connection, transaction,
                CustomSqlHelper.CreateInputParameter("CacheKey", SqlDbType.NVarChar, 100, false, key)))
            {
                using (CustomSqlHelper<ApplicationCache> helper = new CustomSqlHelper<ApplicationCache>())
                {
                    CustomGenericList<ApplicationCache> list = helper.ExecuteReader(command);
                    if (list.Count > 0)
                        return list[0];
                    else
                        return null;
                }
            }
        }
    }
}

