
#region Using

using CustomDataAccess;
using MOBOT.BHL.DataObjects;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

#endregion Using

namespace MOBOT.BHL.DAL
{
    public partial class ImportRecordDAL
	{
        public List<ImportRecord> ImportRecordSelectByImportFileID(SqlConnection sqlConnection, 
            SqlTransaction sqlTransaction, int importFileID, int numRows, int startRow, string sortColumn, string sortDirection,
            int extended = 0)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("import.ImportRecordSelectByImportFileID", connection, transaction,
                    CustomSqlHelper.CreateInputParameter("ImportFileID", SqlDbType.Int, null, false, importFileID),
                    CustomSqlHelper.CreateInputParameter("NumRows", SqlDbType.Int, null, false, numRows),
                    CustomSqlHelper.CreateInputParameter("StartRow", SqlDbType.Int, null, false, startRow),
                    CustomSqlHelper.CreateInputParameter("SortColumn", SqlDbType.NVarChar, 150, false, sortColumn),
                    CustomSqlHelper.CreateInputParameter("SortDirection", SqlDbType.NVarChar, 4, false, sortDirection),
                    CustomSqlHelper.CreateInputParameter("Extended", SqlDbType.Int, null, false, extended)))
            {
                using (CustomSqlHelper<ImportRecord> helper = new CustomSqlHelper<ImportRecord>())
                {
                    List<ImportRecord> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }

        public List<ImportRecordReview> ImportRecordSelectForReviewByImportFileID(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int importFileID, int numRows, int startRow, string sortColumn, string sortDirection,
            int extended = 0)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("import.ImportRecordSelectForReviewByImportFileID", connection, transaction,
                    CustomSqlHelper.CreateInputParameter("ImportFileID", SqlDbType.Int, null, false, importFileID),
                    CustomSqlHelper.CreateInputParameter("NumRows", SqlDbType.Int, null, false, numRows),
                    CustomSqlHelper.CreateInputParameter("StartRow", SqlDbType.Int, null, false, startRow),
                    CustomSqlHelper.CreateInputParameter("SortColumn", SqlDbType.NVarChar, 150, false, sortColumn),
                    CustomSqlHelper.CreateInputParameter("SortDirection", SqlDbType.NVarChar, 4, false, sortDirection),
                    CustomSqlHelper.CreateInputParameter("Extended", SqlDbType.Int, null, false, extended)))
            {
                using (CustomSqlHelper<ImportRecordReview> helper = new CustomSqlHelper<ImportRecordReview>())
                {
                    List<ImportRecordReview> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }

        public List<ImportRecordReview> ImportRecordSelectForReviewByImportRecordID(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int importRecordID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("import.ImportRecordSelectForReviewByImportRecordID", connection, transaction,
                    CustomSqlHelper.CreateInputParameter("ImportRecordID", SqlDbType.Int, null, false, importRecordID)))
            {
                using (CustomSqlHelper<ImportRecordReview> helper = new CustomSqlHelper<ImportRecordReview>())
                {
                    List<ImportRecordReview> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }

        public void ImportRecordSave(SqlConnection sqlConnection, SqlTransaction sqlTransaction, ImportRecord citation, int userID)
        {
            if (sqlConnection == null)
            {
                sqlConnection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"));
            }

            bool isTransactionCoordinator = CustomSqlHelper.IsTransactionCoordinator(sqlTransaction);

            try
            {
                sqlTransaction = CustomSqlHelper.BeginTransaction(sqlConnection, sqlTransaction, isTransactionCoordinator);

                CustomDataAccessStatus<ImportRecord> updatedCitation =
                    new ImportRecordDAL().ImportRecordManageAuto(sqlConnection, sqlTransaction, citation, userID);

                if (citation.Authors.Count > 0)
                {
                    ImportRecordCreatorDAL importRecordCreatorDAL = new ImportRecordCreatorDAL();
                    foreach (ImportRecordCreator creator in citation.Authors)
                    {
                        if (creator.ImportRecordID == 0) creator.ImportRecordID = updatedCitation.ReturnObject.ImportRecordID;
                        importRecordCreatorDAL.ImportRecordCreatorManageAuto(sqlConnection, sqlTransaction, creator, userID);
                    }
                }

                if (citation.Keywords.Count > 0)
                {
                    ImportRecordKeywordDAL importRecordKeywordDAL = new ImportRecordKeywordDAL();
                    foreach (ImportRecordKeyword keyword in citation.Keywords)
                    {
                        if (keyword.ImportRecordID == 0) keyword.ImportRecordID = updatedCitation.ReturnObject.ImportRecordID;
                        importRecordKeywordDAL.ImportRecordKeywordManageAuto(sqlConnection, sqlTransaction, keyword, userID);
                    }
                }

                if (citation.Contributors.Count > 0)
                {
                    ImportRecordContributorDAL importRecordContributorDAL = new ImportRecordContributorDAL();
                    foreach(ImportRecordContributor contributor in citation.Contributors)
                    {
                        if (contributor.ImportRecordID == 0) contributor.ImportRecordID = updatedCitation.ReturnObject.ImportRecordID;
                        importRecordContributorDAL.ImportRecordContributorManageAuto(sqlConnection, sqlTransaction, contributor, userID);
                    }
                }

                if (citation.Pages.Count > 0)
                {
                    ImportRecordPageDAL importRecordPageDAL = new ImportRecordPageDAL();
                    foreach(ImportRecordPage page in citation.Pages)
                    {
                        if (page.ImportRecordID == 0) page.ImportRecordID = updatedCitation.ReturnObject.ImportRecordID;
                        importRecordPageDAL.ImportRecordPageManageAuto(sqlConnection, sqlTransaction, page, userID);
                    }
                }

                if (citation.Errors.Count > 0)
                {
                    ImportRecordErrorLogDAL importRecordErrorLogDAL = new ImportRecordErrorLogDAL();
                    foreach(ImportRecordErrorLog error in citation.Errors)
                    {
                        if (error.ImportRecordID == 0) error.ImportRecordID = updatedCitation.ReturnObject.ImportRecordID;
                        importRecordErrorLogDAL.ImportRecordErrorLogManageAuto(sqlConnection, sqlTransaction, error, userID);
                    }
                }

                if (citation.Warnings.Count > 0)
                {
                    ImportRecordErrorLogDAL importRecordErrorLogDAL = new ImportRecordErrorLogDAL();
                    foreach (ImportRecordErrorLog warning in citation.Warnings)
                    {
                        if (warning.ImportRecordID == 0) warning.ImportRecordID = updatedCitation.ReturnObject.ImportRecordID;
                        importRecordErrorLogDAL.ImportRecordErrorLogManageAuto(sqlConnection, sqlTransaction, warning, userID);
                    }
                }

                CustomSqlHelper.CommitTransaction(sqlTransaction, isTransactionCoordinator);
            }
            catch
            {
                CustomSqlHelper.RollbackTransaction(sqlTransaction, isTransactionCoordinator);

                throw;
            }
            finally
            {
                CustomSqlHelper.CloseConnection(sqlConnection, isTransactionCoordinator);
            }
        }
	}
}
