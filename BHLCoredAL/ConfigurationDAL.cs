
#region Using

using CustomDataAccess;
using MOBOT.BHL.DataObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

#endregion Using

namespace MOBOT.BHL.DAL
{
    public partial class ConfigurationDAL
	{
        public Configuration ConfigurationSelectByName(
            SqlConnection sqlConnection, 
            SqlTransaction sqlTransaction,
            String configurationName)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
              CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("ConfigurationSelectByName", connection, transaction,
                CustomSqlHelper.CreateInputParameter("ConfigurationName", SqlDbType.NVarChar, 50, false, configurationName)))
            {
                using (CustomSqlHelper<Configuration> helper = new CustomSqlHelper<Configuration>())
                {
                    List<Configuration> list = helper.ExecuteReader(command);
                    if (list.Count > 0)
                        return list[0];
                    else
                        return null;
                }
            }
        }

        public bool SearchCatalogOnline(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
              CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("SearchCatalogCheckStatus", connection, transaction,
                CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
            {
                return (CustomSqlHelper.ExecuteNonQuery(command, "ReturnCode") == 1);
            }
        }
    }
}
