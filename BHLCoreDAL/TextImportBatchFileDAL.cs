
#region Using

using CustomDataAccess;
using MOBOT.BHL.DataObjects;
using System;
using System.Data;
using System.Data.SqlClient;

#endregion Using

namespace MOBOT.BHL.DAL
{
	public partial class TextImportBatchFileDAL
	{
        public CustomGenericList<TextImportBatchFile> TextImportBatchFileSelectForBatch(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            int batchID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("txtimport.TextImportBatchFileSelectForBatch", connection, transaction,
                CustomSqlHelper.CreateInputParameter("TextImportBatchID", SqlDbType.Int, null, false, batchID)))
            {
                using (CustomSqlHelper<TextImportBatchFile> helper = new CustomSqlHelper<TextImportBatchFile>())
                {
                    CustomGenericList<TextImportBatchFile> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }

        public CustomGenericList<TextImportBatchFile> TextImportBatchFileDetailSelectForBatch(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            int batchID, int numRows, int startRow, string sortColumn, string sortDirection)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("txtimport.TextImportBatchFileDetailSelectForBatch", connection, transaction,
                CustomSqlHelper.CreateInputParameter("TextImportBatchID", SqlDbType.Int, null, false, batchID),
                CustomSqlHelper.CreateInputParameter("NumRows", SqlDbType.Int, null, false, numRows),
                CustomSqlHelper.CreateInputParameter("StartRow", SqlDbType.Int, null, false, startRow),
                CustomSqlHelper.CreateInputParameter("SortColumn", SqlDbType.NVarChar, 150, false, sortColumn),
                CustomSqlHelper.CreateInputParameter("SortDirection", SqlDbType.NVarChar, 4, false, sortDirection)))
            {
                using (CustomSqlHelper<TextImportBatchFile> helper = new CustomSqlHelper<TextImportBatchFile>())
                {
                    CustomGenericList<TextImportBatchFile> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }
    }
}

