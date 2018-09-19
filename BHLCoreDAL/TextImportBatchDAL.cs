
#region Using

using CustomDataAccess;
using MOBOT.BHL.DataObjects;
using System;
using System.Data;
using System.Data.SqlClient;

#endregion Using

namespace MOBOT.BHL.DAL
{
	public partial class TextImportBatchDAL
	{
        public CustomGenericList<TextImportBatch> TextImportBatchSelectDetails(SqlConnection sqlConnection, SqlTransaction sqlTransaction, 
            //string institutionCode,
            int fileStatusID, int numberOfDays)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("txtimport.TextImportBatchSelectDetails", connection, transaction,
                //CustomSqlHelper.CreateInputParameter("ContributorCode", SqlDbType.NVarChar, 10, false, institutionCode),
                CustomSqlHelper.CreateInputParameter("FileStatusID", SqlDbType.Int, null, false, fileStatusID),
                CustomSqlHelper.CreateInputParameter("NumDays", SqlDbType.Int, null, false, numberOfDays)))
            {
                using (CustomSqlHelper<TextImportBatch> helper = new CustomSqlHelper<TextImportBatch> ())
                {
                    CustomGenericList<TextImportBatch> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }

        public TextImportBatch TextImportBatchSelectExpanded(SqlConnection sqlConnection, SqlTransaction sqlTransaction, 
            int batchID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("txtimport.TextImportBatchSelectExpanded", connection, transaction,
                CustomSqlHelper.CreateInputParameter("TextImportBatchID", SqlDbType.Int, null, false, batchID)))
            {
                using (CustomSqlHelper<TextImportBatch> helper = new CustomSqlHelper<TextImportBatch>())
                {
                    CustomGenericList<TextImportBatch> list = helper.ExecuteReader(command);
                    if (list.Count > 0)
                        return list[0];
                    else
                        return null;
                }
            }
        }

        public void TextImportBatchUpdateStatus(SqlConnection sqlConnection, SqlTransaction sqlTransaction, 
            int batchID, int statusID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("txtimport.TextImportBatchUpdateStatus", connection, transaction,
                CustomSqlHelper.CreateInputParameter("TextImportBatchID", SqlDbType.Int, null, false, batchID),
                CustomSqlHelper.CreateInputParameter("TextImportBatchStatusID", SqlDbType.Int, null, false, statusID)))
            {
                CustomSqlHelper.ExecuteNonQuery(command);
            }
        }
    }
}

