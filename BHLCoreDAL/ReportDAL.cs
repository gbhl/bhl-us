using CustomDataAccess;
using MOBOT.BHL.DataObjects;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace MOBOT.BHL.DAL
{
    public class ReportDAL
    {
        public List<ReportOrphan> ReportSelectOrphanedEntities(
            SqlConnection sqlConnection, 
            SqlTransaction sqlTransaction)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("dbo.ReportSelectOrphanedEntities",
                connection, transaction))
            {
                using (CustomSqlHelper<ReportOrphan> helper = new CustomSqlHelper<ReportOrphan>())
                {
                    List<ReportOrphan> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }
    }
}
