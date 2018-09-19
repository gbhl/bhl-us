
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
    }
}

