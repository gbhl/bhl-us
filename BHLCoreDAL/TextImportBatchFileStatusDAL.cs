
#region Using

using CustomDataAccess;
using MOBOT.BHL.DataObjects;
using System.Collections.Generic;
using System.Data.SqlClient;

#endregion Using

namespace MOBOT.BHL.DAL
{
    public partial class TextImportBatchFileStatusDAL
	{
        public List<TextImportBatchFileStatus> SelectAll(SqlConnection sqlConnection, SqlTransaction sqlTransaction)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;
            using (SqlCommand command = CustomSqlHelper.CreateCommand("txtimport.TextImportBatchFileStatusSelectAll", connection, transaction))
            {
                using (CustomSqlHelper<TextImportBatchFileStatus> helper = new CustomSqlHelper<TextImportBatchFileStatus>())
                {
                    List<TextImportBatchFileStatus> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }
    }
}

