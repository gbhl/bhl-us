using System;
using System.Data;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHLImport.DataObjects;

namespace MOBOT.BHLImport.DAL
{
	public partial class OAIRecordDAL
	{
        public void OAIRecordDeleteForOAIRecordID(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int oaiRecordID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHLImport"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            try
            {
                using (SqlCommand command =
                    CustomSqlHelper.CreateCommand("OAIRecordDeleteForOAIRecordID", connection, sqlTransaction,
                    CustomSqlHelper.CreateInputParameter("OAIRecordID", SqlDbType.Int, null, false, oaiRecordID)))
                {
                    CustomSqlHelper.ExecuteNonQuery(command);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void OAIRecordDeleteForHarvestLogID(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int HarvestLogID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHLImport"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            try
            {
                using (SqlCommand command =
                    CustomSqlHelper.CreateCommand("OAIRecordDeleteForHarvestLogID", connection, sqlTransaction,
                    CustomSqlHelper.CreateInputParameter("HarvestLogID", SqlDbType.Int, null, false, HarvestLogID)))
                {
                    CustomSqlHelper.ExecuteNonQuery(command);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public OAIRecord OAIRecordSelectForOAIIdentifierAndStatus(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, string oaiIdentifier, int oaiRecordStatusID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHLImport"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("OAIRecordSelectForOAIIdentifierAndStatus", connection, transaction,
                CustomSqlHelper.CreateInputParameter("OAIIdentifier", SqlDbType.NVarChar, 100, false, oaiIdentifier),
                CustomSqlHelper.CreateInputParameter("OAIRecordStatusID", SqlDbType.Int, null, false, oaiRecordStatusID)))
            {
                using (CustomSqlHelper<OAIRecord> helper = new CustomSqlHelper<OAIRecord>())
                {
                    CustomGenericList<OAIRecord> list = helper.ExecuteReader(command);

                    if (list.Count > 0)
                    {
                        return list[0];
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        public OAIRecord OAIRecordSelectForOAIIdentifierAndDateStamp(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, string oaiIdentifier, string oaiDateStamp)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHLImport"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("OAIRecordSelectForOAIIdentifierAndDateStamp", connection, transaction,
                CustomSqlHelper.CreateInputParameter("OAIIdentifier", SqlDbType.NVarChar, 100, false, oaiIdentifier),
                CustomSqlHelper.CreateInputParameter("OAIDateStamp", SqlDbType.NVarChar, 30, false, oaiDateStamp)))
            {
                using (CustomSqlHelper<OAIRecord> helper = new CustomSqlHelper<OAIRecord>())
                {
                    CustomGenericList<OAIRecord> list = helper.ExecuteReader(command);

                    if (list.Count > 0)
                    {
                        return list[0];
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
	}
}
