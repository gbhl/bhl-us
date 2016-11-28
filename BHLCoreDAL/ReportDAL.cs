using CustomDataAccess;
using MOBOT.BHL.DataObjects;
using System.Data.SqlClient;

namespace MOBOT.BHL.DAL
{
    public class ReportDAL
    {
        public CustomGenericList<ReportOrphan> ReportSelectOrphanedEntities(
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
                    CustomGenericList<ReportOrphan> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }
    }
}
