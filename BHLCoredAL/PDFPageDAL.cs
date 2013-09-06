
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
	public partial class PDFPageDAL
	{
        public CustomGenericList<PageSummaryView> PDFPageSummaryViewSelectByPdfID(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            int PdfId)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("PDFPageSummaryViewSelectByPdfId", connection, transaction,
                CustomSqlHelper.CreateInputParameter("PdfID", SqlDbType.Int, null, false, PdfId)))
            {
                using (CustomSqlHelper<PageSummaryView> helper = new CustomSqlHelper<PageSummaryView>())
                {
                    CustomGenericList<PageSummaryView> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }

        public CustomGenericList<PDFPage> PDFPageSelectForPdfID(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            int PdfId)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("PDFPageSelectForPdfID", connection, transaction,
                CustomSqlHelper.CreateInputParameter("PdfID", SqlDbType.Int, null, false, PdfId)))
            {
                using (CustomSqlHelper<PDFPage> helper = new CustomSqlHelper<PDFPage>())
                {
                    CustomGenericList<PDFPage> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }
    }
}
