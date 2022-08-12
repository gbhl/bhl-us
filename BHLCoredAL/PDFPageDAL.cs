
#region Using

using CustomDataAccess;
using MOBOT.BHL.DataObjects;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

#endregion Using

namespace MOBOT.BHL.DAL
{
    public partial class PDFPageDAL
	{
        public List<PageSummaryView> PDFPageSummaryViewSelectByPdfID(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            int PdfId)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("PDFPageSummaryViewSelectByPdfId", connection, transaction,
                CustomSqlHelper.CreateInputParameter("PdfID", SqlDbType.Int, null, false, PdfId)))
            {
                using (CustomSqlHelper<PageSummaryView> helper = new CustomSqlHelper<PageSummaryView>())
                {
                    List<PageSummaryView> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }

        public List<PDFPage> PDFPageSelectForPdfID(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            int PdfId)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("PDFPageSelectForPdfID", connection, transaction,
                CustomSqlHelper.CreateInputParameter("PdfID", SqlDbType.Int, null, false, PdfId)))
            {
                using (CustomSqlHelper<PDFPage> helper = new CustomSqlHelper<PDFPage>())
                {
                    List<PDFPage> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }
    }
}
