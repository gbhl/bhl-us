
#region Using

using CustomDataAccess;
using MOBOT.BHL.DataObjects;
using System.Collections.Generic;
using System.Data.SqlClient;

#endregion Using

namespace MOBOT.BHL.DAL
{
    public partial class ImportFileStatusDAL
	{
        public List<ImportFileStatus> SelectAll(SqlConnection sqlConnection, SqlTransaction sqlTransaction)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;
            using (SqlCommand command = CustomSqlHelper.CreateCommand("import.ImportFileStatusSelectAll", connection, transaction))
            {
                using (CustomSqlHelper<ImportFileStatus> helper = new CustomSqlHelper<ImportFileStatus>())
                {
                    List<ImportFileStatus> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }
	}
}
