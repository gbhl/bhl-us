
#region Using

using CustomDataAccess;
using MOBOT.BHL.DataObjects;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

#endregion Using

namespace MOBOT.BHL.DAL
{
    public partial class TextImportBatchDAL
	{
        public List<TextImportBatch> TextImportBatchSelectDetails(SqlConnection sqlConnection, SqlTransaction sqlTransaction, 
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
                    List<TextImportBatch> list = helper.ExecuteReader(command);
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
                    List<TextImportBatch> list = helper.ExecuteReader(command);
                    if (list.Count > 0)
                        return list[0];
                    else
                        return null;
                }
            }
        }

        public List<TextImportBatch> TextImportBatchSelectForFileCreation(SqlConnection sqlConnection, SqlTransaction sqlTransaction)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("txtimport.TextImportBatchSelectForFileCreation", connection, transaction))
            {
                using (CustomSqlHelper<TextImportBatch> helper = new CustomSqlHelper<TextImportBatch>())
                {
                    List<TextImportBatch> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }
    }
}

