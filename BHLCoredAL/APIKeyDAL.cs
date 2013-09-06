#region Using

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHL.DataObjects;

#endregion Using

namespace MOBOT.BHL.DAL
{
	public partial class APIKeyDAL
	{
        public APIKey ApiKeyInsert(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            String contactName,
            String emailAddress)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("ApiKeyInsert", connection, transaction,
                CustomSqlHelper.CreateInputParameter("ContactName", SqlDbType.NVarChar, 200, false, contactName),
                CustomSqlHelper.CreateInputParameter("EmailAddress", SqlDbType.NVarChar, 200, false, emailAddress)))
            {
                using (CustomSqlHelper<APIKey> helper = new CustomSqlHelper<APIKey>())
                {
                    CustomGenericList<APIKey> list = helper.ExecuteReader(command);
                    if (list.Count > 0)
                        return list[0];
                    else
                        return null;
                }
            }
        }
        
        public APIKey ApiKeySelectByEmail(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            String emailAddress)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("ApiKeySelectByEmail", connection, transaction,
                CustomSqlHelper.CreateInputParameter("EmailAddress", SqlDbType.NVarChar, 200, false, emailAddress)))
            {
                using (CustomSqlHelper<APIKey> helper = new CustomSqlHelper<APIKey>())
                {
                    CustomGenericList<APIKey> list = helper.ExecuteReader(command);
                    if (list.Count > 0)
                        return list[0];
                    else
                        return null;
                }
            }
        }

        public CustomGenericList<APIKey> ApiKeySelectAll(SqlConnection sqlConnection, SqlTransaction sqlTransaction)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("ApiKeySelectAll", connection, transaction))
            {
                using (CustomSqlHelper<APIKey> helper = new CustomSqlHelper<APIKey>())
                {
                    return helper.ExecuteReader(command);
                }
            }
        }
    }
}
