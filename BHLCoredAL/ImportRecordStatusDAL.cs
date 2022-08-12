
#region Using

using CustomDataAccess;
using MOBOT.BHL.DataObjects;
using System.Collections.Generic;
using System.Data.SqlClient;

#endregion Using

namespace MOBOT.BHL.DAL
{
    public partial class ImportRecordStatusDAL
	{
        public List<ImportRecordStatus> SelectAll(SqlConnection sqlConnection, SqlTransaction sqlTransaction)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;
            using (SqlCommand command = CustomSqlHelper.CreateCommand("import.ImportRecordStatusSelectAll", connection, transaction))
            {
                using (CustomSqlHelper<ImportRecordStatus> helper = new CustomSqlHelper<ImportRecordStatus>())
                {
                    List<ImportRecordStatus> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }
	}
}
