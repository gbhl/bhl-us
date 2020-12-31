#region Using

using CustomDataAccess;
using MOBOT.BHLImport.DataObjects;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

#endregion Using

namespace MOBOT.BHLImport.DAL
{
    public partial class BSItemDAL
	{
        /// <summary>
        /// Delete all supporting data for the specified item.
        /// </summary>
        /// <param name="sqlConnection">Sql connection or null.</param>
        /// <param name="sqlTransaction">Sql transaction or null.</param>
        /// <param name="itemID">Identifier of the item.</param>
        public void BSItemDeleteAllSegments(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            int itemID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHLImport"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("BSItemDeleteAllSegments", connection, transaction,
                CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, true, itemID)))
            {
                command.ExecuteNonQuery();
            }
        }

        public List<BSItem> BSItemSelectByStatus(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int itemStatusId, int numberOfRows, int pageNumber,
            string sortColumn, string sortDirection)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHLImport"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("BSItemSelectByStatus", connection, transaction,
                CustomSqlHelper.CreateInputParameter("ItemStatusID", SqlDbType.Int, null, false, itemStatusId),
                CustomSqlHelper.CreateInputParameter("NumRows", SqlDbType.Int, null, false, numberOfRows),
                CustomSqlHelper.CreateInputParameter("PageNum", SqlDbType.Int, null, false, pageNumber),
                CustomSqlHelper.CreateInputParameter("SortColumn", SqlDbType.NVarChar, 150, false, sortColumn),
                CustomSqlHelper.CreateInputParameter("SortDirection", SqlDbType.NVarChar, 4, false, sortDirection)))
            {
                using (CustomSqlHelper<BSItem> helper = new CustomSqlHelper<BSItem>())
                {
                    List<BSItem> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }
    }
}
