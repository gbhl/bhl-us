using CustomDataAccess;
using MOBOT.BHLImport.DataObjects;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

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

        public List<BSItem> BSItemSelectQueuedByBHLItem(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int? bhlItemID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHLImport"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;
            using (SqlCommand command = CustomSqlHelper.CreateCommand("dbo.BSItemSelectQueuedByBHLItem", connection, transaction,
                CustomSqlHelper.CreateInputParameter("@BHLItemID", SqlDbType.Int, null, false, bhlItemID)))
            {
                using (CustomSqlHelper<BSItem> helper = new CustomSqlHelper<BSItem>())
                {
                    List<BSItem> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }

        public List<BSItem> BSItemSelectByBHLItemAndStatus(SqlConnection sqlConnection, SqlTransaction sqlTransaction, int? bhlItemID, int itemStatusID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHLImport"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;
            using (SqlCommand command = CustomSqlHelper.CreateCommand("dbo.BSItemSelectByBHLItemAndStatus", connection, transaction,
                CustomSqlHelper.CreateInputParameter("@BHLItemID", SqlDbType.Int, null, true, bhlItemID),
                CustomSqlHelper.CreateInputParameter("@ItemStatusID", SqlDbType.Int, null, false, itemStatusID)))
            {
                using (CustomSqlHelper<BSItem> helper = new CustomSqlHelper<BSItem>())
                {
                    List<BSItem> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }

        public List<BSItem> BSItemSelectByItemAndStatus(SqlConnection sqlConnection, SqlTransaction sqlTransaction, int? itemID, int itemStatusID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHLImport"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;
            using (SqlCommand command = CustomSqlHelper.CreateCommand("dbo.BSItemSelectByItemAndStatus", connection, transaction,
                CustomSqlHelper.CreateInputParameter("@ItemID", SqlDbType.Int, null, true, itemID),
                CustomSqlHelper.CreateInputParameter("@ItemStatusID", SqlDbType.Int, null, false, itemStatusID)))
            {
                using (CustomSqlHelper<BSItem> helper = new CustomSqlHelper<BSItem>())
                {
                    List<BSItem> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }

        public List<BSItem> BSItemAvailabilityCheck(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int? bhlItemID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHLImport"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;
            using (SqlCommand command = CustomSqlHelper.CreateCommand("dbo.BSItemAvailabilityCheck", connection, transaction,
                CustomSqlHelper.CreateInputParameter("@BHLItemID", SqlDbType.Int, null, false, bhlItemID)))
            {
                using (CustomSqlHelper<BSItem> helper = new CustomSqlHelper<BSItem>())
                {
                    List<BSItem> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }

        public void BSItemUpdateItemStatus(SqlConnection sqlConnection,SqlTransaction sqlTransaction, int itemID, int itemStatusID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHLImport"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;
            using (SqlCommand command = CustomSqlHelper.CreateCommand("dbo.BSItemUpdateItemStatus", connection, transaction,
                CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemID),
                CustomSqlHelper.CreateInputParameter("ItemStatusID", SqlDbType.Int, null, false, itemStatusID)))
            {
                command.ExecuteNonQuery();
            }
        }
    }
}
