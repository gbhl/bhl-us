
#region Using

using System;
using System.Data;
using System.Data.SqlClient;
using CustomDataAccess;
using System.Collections.Generic;
using MOBOT.BHL.DataObjects;

#endregion Using

namespace MOBOT.BHL.DAL
{
	public partial class PDFDAL
	{
        public PDF AddNewPdf(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            int itemID, String emailAddress, String shareWith, bool imagesOnly, 
            String articleTitle, String articleCreators, String articleTags, List<int> pageIDs)
        {
            SqlConnection connection = sqlConnection;
            SqlTransaction transaction = sqlTransaction;

            if (connection == null)
            {
                connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"));
            }

            bool isTransactionCoordinator = CustomSqlHelper.IsTransactionCoordinator(transaction);

            try
            {
                transaction = CustomSqlHelper.BeginTransaction(connection, transaction, isTransactionCoordinator);

                // Add the new pdf record
                PDF newPdf = new PDF();
                newPdf.ItemID = itemID;
                newPdf.EmailAddress = emailAddress;
                newPdf.ShareWithEmailAddresses = shareWith;
                newPdf.ImagesOnly = imagesOnly;
                newPdf.ArticleTitle = articleTitle;
                newPdf.ArticleCreators = articleCreators;
                newPdf.ArticleTags = articleTags;
                newPdf.PdfStatusID = 10;
                PDF savedpdf = new PDFDAL().PDFInsertAuto(connection, transaction, newPdf);

                // Add records for the pdf pages
                PDFPageDAL pdfPageDal = new PDFPageDAL();
                foreach (int pageID in pageIDs)
                {
                    PDFPage newPdfPage = new PDFPage();
                    newPdfPage.PdfID = savedpdf.PdfID;
                    newPdfPage.PageID = pageID;
                    pdfPageDal.PDFPageInsertAuto(connection, transaction, newPdfPage);
                }

                CustomSqlHelper.CommitTransaction(transaction, isTransactionCoordinator);

                return savedpdf;
            }
            catch (Exception ex)
            {
                CustomSqlHelper.RollbackTransaction(transaction, isTransactionCoordinator);
                throw;
            }
            finally
            {
                CustomSqlHelper.CloseConnection(connection, isTransactionCoordinator);
            }
        }

        public List<PDF> PDFSelectForFileCreation(SqlConnection sqlConnection, SqlTransaction sqlTransaction)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("PDFSelectForFileCreation", connection, transaction))
            {
                using (CustomSqlHelper<PDF> helper = new CustomSqlHelper<PDF>())
                {
                    List<PDF> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }

        public List<PDF> PDFSelectForDeletion(SqlConnection sqlConnection, SqlTransaction sqlTransaction)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("PDFSelectForDeletion", connection, transaction))
            {
                using (CustomSqlHelper<PDF> helper = new CustomSqlHelper<PDF>())
                {
                    List<PDF> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }

        public List<PDF> PDFSelectDuplicateForPdfID(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            int PdfId)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("PDFSelectDuplicateForPdfID", connection, transaction,
                CustomSqlHelper.CreateInputParameter("PdfID", SqlDbType.Int, null, false, PdfId)))
            {
                using (CustomSqlHelper<PDF> helper = new CustomSqlHelper<PDF>())
                {
                    List<PDF> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }

        public List<PDF> PDFSelectForWeekAndStatus(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            int year,
            int week,
            int pdfStatusId)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("PDFSelectForWeekAndStatus", connection, transaction,
                CustomSqlHelper.CreateInputParameter("Year", SqlDbType.Int, null, false, year),
                CustomSqlHelper.CreateInputParameter("Week", SqlDbType.Int, null, false, week),
                CustomSqlHelper.CreateInputParameter("PdfStatusID", SqlDbType.Int, null, false, pdfStatusId)))
            {
                using (CustomSqlHelper<PDF> helper = new CustomSqlHelper<PDF>())
                {
                    List<PDF> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }

        public bool PDFUpdatePdfStatus(SqlConnection sqlConnection, SqlTransaction sqlTransaction, 
            int PdfId,
            int PdfStatusId
            )
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("PDFUpdatePdfStatus", connection, transaction,
                CustomSqlHelper.CreateInputParameter("PdfID", SqlDbType.Int, null, false, PdfId),
                CustomSqlHelper.CreateInputParameter("PdfStatusID", SqlDbType.Int, null, false, PdfStatusId),
                CustomSqlHelper.CreateOutputParameter("RowsUpdated", SqlDbType.Int, null, false)))
            {
                command.ExecuteNonQuery();
                // If RowsUpdated > 0, then a row was updated (return true)
                return ((int)command.Parameters[2].Value > 0);
            }
        }

        public PDF PDFUpdateGenerationInfo(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            PDF pdf)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"));
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("PDFUpdateGenerationInfo", connection, transaction,
                    CustomSqlHelper.CreateInputParameter("PdfID", SqlDbType.Int, null, false, pdf.PdfID),
                    CustomSqlHelper.CreateInputParameter("FileLocation", SqlDbType.NVarChar, 200, false, pdf.FileLocation),
                    CustomSqlHelper.CreateInputParameter("FileUrl", SqlDbType.NVarChar, 200, false, pdf.FileUrl),
                    CustomSqlHelper.CreateInputParameter("PdfStatusID", SqlDbType.Int, null, false, pdf.PdfStatusID),
                    CustomSqlHelper.CreateInputParameter("NumberImagesMissing", SqlDbType.Int, null, false, pdf.NumberImagesMissing),
                    CustomSqlHelper.CreateInputParameter("NumberOcrMissing", SqlDbType.Int, null, false, pdf.NumberOcrMissing),
                    CustomSqlHelper.CreateReturnValueParameter("ReturnCode", SqlDbType.Int, null, false)))
            {
                using (CustomSqlHelper<PDF> helper = new CustomSqlHelper<PDF>())
                {
                    List<PDF> list = helper.ExecuteReader(command);
                    if (list.Count > 0)
                    {
                        PDF o = list[0];
                        list = null;
                        return o;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        public void Save(SqlConnection sqlConnection, SqlTransaction sqlTransaction, PDF pdf)
        {
            SqlConnection connection = sqlConnection;
            SqlTransaction transaction = sqlTransaction;

            if (connection == null)
            {
                connection =
                    CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"));
            }

            bool isTransactionCoordinator = CustomSqlHelper.IsTransactionCoordinator(transaction);

            try
            {
                transaction = CustomSqlHelper.BeginTransaction(connection, transaction, isTransactionCoordinator);

                new PDFDAL().PDFManageAuto(connection, transaction, pdf);

                CustomSqlHelper.CommitTransaction(transaction, isTransactionCoordinator);
            }
            catch (Exception ex)
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
