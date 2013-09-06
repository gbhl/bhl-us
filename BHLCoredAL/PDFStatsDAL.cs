using System;
using System.Data;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHL.DataObjects;

namespace MOBOT.BHL.DAL
{
    public partial class PDFStatsDAL
    {
        public CustomGenericList<PDFStats> PDFStatsSelectOverview(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("PDFStatsSelectOverview", connection, transaction))
            {
                CustomGenericList<CustomDataRow> list = CustomSqlHelper.ExecuteReaderAndReturnRows(command);
                CustomGenericList<PDFStats> listOfStats = new CustomGenericList<PDFStats>();
                foreach (CustomDataRow row in list)
                {
                    PDFStats stats = new PDFStats();
                    stats.PdfStatusID = (int)row["PdfStatusID"].Value;
                    stats.PdfStatusName = row["PdfStatusName"].Value.ToString();
                    stats.NumberofPdfs = (int)row["NumberOfPDFs"].Value;
                    stats.PdfsWithOcr = (int)row["PDFsWithOCR"].Value;
                    stats.PdfsWithArticleMetadata = (int)row["PDFsWithArticleMetadata"].Value;
                    stats.PdfsWithMissingImages = (int)row["PDFsWithMissingImages"].Value;
                    stats.PdfsWithMissingOcr = (int)row["PDFsWithMissingOCR"].Value;
                    listOfStats.Add(stats);
                }

                return listOfStats;
            }
        }

        public CustomGenericList<PDFStats> PDFStatsSelectExpanded(
                SqlConnection sqlConnection,
                SqlTransaction sqlTransaction)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("PDFStatsSelectExpanded", connection, transaction))
            {
                CustomGenericList<CustomDataRow> list = CustomSqlHelper.ExecuteReaderAndReturnRows(command);
                CustomGenericList<PDFStats> listOfStats = new CustomGenericList<PDFStats>();
                foreach (CustomDataRow row in list)
                {
                    PDFStats stats = new PDFStats();
                    stats.Year = (int)row["Year"].Value;
                    stats.Week = (int)row["Week"].Value;
                    stats.PdfStatusID = (int)row["PdfStatusID"].Value;
                    stats.PdfStatusName = row["PdfStatusName"].Value.ToString();
                    stats.NumberofPdfs = (int)row["NumberOfPDFs"].Value;
                    stats.PdfsWithOcr = (int)row["PDFsWithOCR"].Value;
                    stats.PdfsWithArticleMetadata = (int)row["PDFsWithArticleMetadata"].Value;
                    stats.PdfsWithMissingImages = (int)row["PDFsWithMissingImages"].Value;
                    stats.PdfsWithMissingOcr = (int)row["PDFsWithMissingOCR"].Value;
                    stats.TotalMissingImages = (int)row["TotalMissingImages"].Value;
                    stats.TotalMissingOcr = (int)row["TotalMissingOCR"].Value;
                    stats.TotalMinutesToGenerate = (int?)row["TotalMinutesToGenerate"].Value;
                    stats.AveMinutesToGenerate = Convert.ToDouble(row["AvgMinutesToGenerate"].Value);
                    listOfStats.Add(stats);
                }

                return listOfStats;
            }
        }

    }
}
