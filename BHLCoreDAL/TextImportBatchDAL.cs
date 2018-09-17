
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
    }
}

