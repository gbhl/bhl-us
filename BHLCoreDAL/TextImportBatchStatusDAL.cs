
#region Using

using CustomDataAccess;
using MOBOT.BHL.DataObjects;
using System.Collections.Generic;
using System.Data.SqlClient;

#endregion Using

namespace MOBOT.BHL.DAL
{
    public partial class TextImportBatchStatusDAL
	{
        public List<TextImportBatchStatus> SelectAll(SqlConnection sqlConnection, SqlTransaction sqlTransaction)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;
            using (SqlCommand command = CustomSqlHelper.CreateCommand("txtimport.TextImportBatchStatusSelectAll", connection, transaction))
            {
                using (CustomSqlHelper<TextImportBatchStatus> helper = new CustomSqlHelper<TextImportBatchStatus>())
                {
                    List<TextImportBatchStatus> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }
    }
}

