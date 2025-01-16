using CustomDataAccess;
using MOBOT.BHL.DataObjects;
using System.Collections.Generic;
using System.Data;
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

        public List<PermissionsTitle> ReportSelectPermissionsTitles (SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            int? titleID, int notKnown, int inCopyright, int notProvided, int numRows, int startRow, string sortColumn, string sortDirection)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("dbo.ReportSelectPermissionsTitles", connection, transaction,
                CustomSqlHelper.CreateInputParameter("TitleID", SqlDbType.Int, null, true, titleID),
                CustomSqlHelper.CreateInputParameter("NotKnown", SqlDbType.Int, null, true, notKnown),
                CustomSqlHelper.CreateInputParameter("InCopyright", SqlDbType.Int, null, true, inCopyright),
                CustomSqlHelper.CreateInputParameter("NotProvided", SqlDbType.Int, null, true, notProvided),
                CustomSqlHelper.CreateInputParameter("NumRows", SqlDbType.Int, null, false, numRows),
                CustomSqlHelper.CreateInputParameter("StartRow", SqlDbType.Int, null, false, startRow),
                CustomSqlHelper.CreateInputParameter("SortColumn", SqlDbType.NVarChar, 150, false, sortColumn),
                CustomSqlHelper.CreateInputParameter("SortDirection", SqlDbType.NVarChar, 4, false, sortDirection)))
            {
                using (CustomSqlHelper<PermissionsTitle> helper = new CustomSqlHelper<PermissionsTitle>())
                {
                    List<PermissionsTitle> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }
    }
}
