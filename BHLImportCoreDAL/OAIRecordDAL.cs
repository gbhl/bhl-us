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

            using (SqlCommand command =
                CustomSqlHelper.CreateCommand("OAIRecordDeleteForOAIRecordID", connection, sqlTransaction,
                CustomSqlHelper.CreateInputParameter("OAIRecordID", SqlDbType.Int, null, false, oaiRecordID)))
            {
                CustomSqlHelper.ExecuteNonQuery(command);
            }
        }

        public void OAIRecordDeleteForHarvestLogID(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int HarvestLogID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHLImport"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command =
                CustomSqlHelper.CreateCommand("OAIRecordDeleteForHarvestLogID", connection, sqlTransaction,
                CustomSqlHelper.CreateInputParameter("HarvestLogID", SqlDbType.Int, null, false, HarvestLogID)))
            {
                CustomSqlHelper.ExecuteNonQuery(command);
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

        public void Save(SqlConnection sqlConnection, SqlTransaction sqlTransaction, OAIRecord oaiRecord)
        {
            SqlConnection connection = sqlConnection;
            SqlTransaction transaction = sqlTransaction;

            // Only proceed with the save if we don't already have a record for this exact identifier and datestamp
            if (OAIRecordSelectForOAIIdentifierAndDateStamp(connection, transaction, oaiRecord.OAIIdentifier, oaiRecord.OAIDateStamp) == null)
            {
                if (connection == null)
                {
                    connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHLImport"));
                }

                bool isTransactionCoordinator = CustomSqlHelper.IsTransactionCoordinator(transaction);

                try
                {
                    transaction = CustomSqlHelper.BeginTransaction(connection, transaction, isTransactionCoordinator);

                    // Delete any existing record for this identifier that has a "New" status
                    OAIRecord existingRecord = OAIRecordSelectForOAIIdentifierAndStatus(connection, transaction, oaiRecord.OAIIdentifier, 10);
                    if (existingRecord != null) OAIRecordDeleteForOAIRecordID(connection, transaction, existingRecord.OAIRecordID);

                    oaiRecord.OAIRecordStatusID = 10;   // Set record status to "New"
                    OAIRecord newOaiRecord = OAIRecordInsertAuto(connection, transaction, oaiRecord);

                    if (oaiRecord.Creators.Count > 0)
                    {
                        OAIRecordCreatorDAL creatorDAL = new OAIRecordCreatorDAL();
                        foreach (OAIRecordCreator creator in oaiRecord.Creators)
                        {
                            creator.OAIRecordID = newOaiRecord.OAIRecordID;
                            creatorDAL.OAIRecordCreatorInsertAuto(connection, transaction, creator);
                        }
                    }

                    if (oaiRecord.DcTypes.Count > 0)
                    {
                        OAIRecordDCTypeDAL typeDAL = new OAIRecordDCTypeDAL();
                        foreach (OAIRecordDCType dcType in oaiRecord.DcTypes)
                        {
                            dcType.OAIRecordID = newOaiRecord.OAIRecordID;
                            typeDAL.OAIRecordDCTypeInsertAuto(connection, transaction, dcType);
                        }
                    }

                    if (oaiRecord.Rights.Count > 0)
                    {
                        OAIRecordRightDAL rightDAL = new OAIRecordRightDAL();
                        foreach (OAIRecordRight right in oaiRecord.Rights)
                        {
                            right.OAIRecordID = newOaiRecord.OAIRecordID;
                            rightDAL.OAIRecordRightInsertAuto(connection, transaction, right);
                        }
                    }

                    if (oaiRecord.Subjects.Count > 0)
                    {
                        OAIRecordSubjectDAL subjectDAL = new OAIRecordSubjectDAL();
                        foreach (OAIRecordSubject subject in oaiRecord.Subjects)
                        {
                            subject.OAIRecordID = newOaiRecord.OAIRecordID;
                            subjectDAL.OAIRecordSubjectInsertAuto(connection, transaction, subject);
                        }
                    }

                    CustomSqlHelper.CommitTransaction(transaction, isTransactionCoordinator);
                }
                catch
                {
                    CustomSqlHelper.RollbackTransaction(transaction, isTransactionCoordinator);

                    throw;
                }
                finally
                {
                    CustomSqlHelper.CloseConnection(connection, isTransactionCoordinator);
                }
            }
        }
	}
}
