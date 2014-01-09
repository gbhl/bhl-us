
#region Using

using CustomDataAccess;
using MOBOT.BHL.DataObjects;
using System;
using System.Data.SqlClient;

#endregion Using

namespace MOBOT.BHL.DAL
{
	public partial class ImportRecordStatusDAL
	{
        public CustomGenericList<ImportRecordStatus> SelectAll(SqlConnection sqlConnection, SqlTransaction sqlTransaction)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;
            using (SqlCommand command = CustomSqlHelper.CreateCommand("import.ImportRecordStatusSelectAll", connection, transaction))
            {
                using (CustomSqlHelper<ImportRecordStatus> helper = new CustomSqlHelper<ImportRecordStatus>())
                {
                    CustomGenericList<ImportRecordStatus> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }
	}
}
