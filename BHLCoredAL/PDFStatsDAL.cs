using CustomDataAccess;
using MOBOT.BHL.DataObjects;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace MOBOT.BHL.DAL
{
    public partial class PDFStatsDAL
    {
        public List<PDFStats> PDFStatsSelectOverview(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("PDFStatsSelectOverview", connection, transaction))
            {
                List<CustomDataRow> list = CustomSqlHelper.ExecuteReaderAndReturnRows(command);
                List<PDFStats> listOfStats = new List<PDFStats>();
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

        public List<PDFStats> PDFStatsSelectExpanded(
                SqlConnection sqlConnection,
                SqlTransaction sqlTransaction)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("PDFStatsSelectExpanded", connection, transaction))
            {
                List<CustomDataRow> list = CustomSqlHelper.ExecuteReaderAndReturnRows(command);
                List<PDFStats> listOfStats = new List<PDFStats>();
                foreach (CustomDataRow row in list)
                {
                    PDFStats stats = new PDFStats();
                    stats.Year = (int)row["Year"].Value;
                    stats.Week = (int)row["Week"].Value;
                    stats.WeekStartDate = row["WeekStartDate"].Value.ToString();
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
