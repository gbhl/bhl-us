
#region Using

using System;
using System.Data;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHL.DataObjects;

#endregion Using

namespace MOBOT.BHL.DAL
{
	public partial class PDFStatusDAL
	{
        public CustomGenericList<PDFStatus> PDFStatusSelectAll(SqlConnection sqlConnection, SqlTransaction sqlTransaction)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;
            using (SqlCommand command = CustomSqlHelper.CreateCommand("PDFStatusSelectAll", connection, transaction))
            {
                using (CustomSqlHelper<PDFStatus> helper = new CustomSqlHelper<PDFStatus>())
                {
                    CustomGenericList<PDFStatus> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }

    }
}
