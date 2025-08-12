using CustomDataAccess;
using System;
using System.Data;
using System.Data.SqlClient;

namespace MOBOT.BHLImport.DAL
{
    public partial class WDEntityIdentifierDAL
    {
        public void WDEntityIdentifierDeleteByEntityType(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            string entityType)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHLImport"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("dbo.WDEntityIdentifierDeleteByEntityType", connection, transaction,
                CustomSqlHelper.CreateInputParameter("EntityType", SqlDbType.NVarChar, 20, false, entityType)))
            {
                CustomSqlHelper.ExecuteNonQuery(command);
            }
        }

        public void WDEntityIdentifierInsert(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            string entityType,
            int entityId,
            string identifierType,
            string identifierValue,
            DateTime harvestDate)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHLImport"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("dbo.WDEntityIdentifierInsert", connection, transaction,
                CustomSqlHelper.CreateInputParameter("BHLEntityType", SqlDbType.NVarChar, 20, false, entityType),
                CustomSqlHelper.CreateInputParameter("BHLEntityID", SqlDbType.Int, null, false, entityId),
                CustomSqlHelper.CreateInputParameter("IdentifierType", SqlDbType.NVarChar, 40, false, identifierType),
                CustomSqlHelper.CreateInputParameter("IdentifierValue", SqlDbType.NVarChar, 125, false, identifierValue),
                CustomSqlHelper.CreateInputParameter("HarvestDate", SqlDbType.DateTime, null, false, harvestDate)))
            {
                CustomSqlHelper.ExecuteNonQuery(command);
            }
        }

        public void WDEntityIdentifierPublishAuthorIDs(SqlConnection sqlConnection, SqlTransaction sqlTransaction)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHLImport"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("dbo.WDEntityIdentifierPublishAuthorIDs", connection, transaction))
            {
                CustomSqlHelper.ExecuteNonQuery(command);
            }
        }

        public void WDEntityIdentifierPublishTitleIDs(SqlConnection sqlConnection, SqlTransaction sqlTransaction)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHLImport"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("dbo.WDEntityIdentifierPublishTitleIDs", connection, transaction))
            {
                CustomSqlHelper.ExecuteNonQuery(command);
            }
        }
    }
}
