
#region Using

using CustomDataAccess;
using MOBOT.BHL.DataObjects;
using System.Collections.Generic;
using System.Data.SqlClient;

#endregion Using

namespace MOBOT.BHL.DAL
{
    public partial class PDFStatusDAL
	{
        public List<PDFStatus> PDFStatusSelectAll(SqlConnection sqlConnection, SqlTransaction sqlTransaction)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;
            using (SqlCommand command = CustomSqlHelper.CreateCommand("PDFStatusSelectAll", connection, transaction))
            {
                using (CustomSqlHelper<PDFStatus> helper = new CustomSqlHelper<PDFStatus>())
                {
                    List<PDFStatus> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }

    }
}
