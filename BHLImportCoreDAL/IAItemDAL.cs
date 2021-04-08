
#region Using

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHLImport.DataObjects;

#endregion Using

namespace MOBOT.BHLImport.DAL
{
	public partial class IAItemDAL
	{
        /// <summary>
        /// Select the item with the specified IAIdentifier.
        /// </summary>
        /// <param name="sqlConnection">Sql connection or null.</param>
        /// <param name="sqlTransaction">Sql transaction or null.</param>
        /// <returns>Object of type Item.</returns>
        public IAItem IAItemSelectByIAIdentifier(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            string iaIdentifier)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHLImport"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("IAItemSelectByIAIdentifier", connection, transaction,
                CustomSqlHelper.CreateInputParameter("IAIdentifier", SqlDbType.NVarChar, 200, false, iaIdentifier)))
            {
                using (CustomSqlHelper<IAItem> helper = new CustomSqlHelper<IAItem>())
                {
                    List<IAItem> list = helper.ExecuteReader(command);

                    if (list.Count > 0)
                    {
                        return list[0];
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Select items for which the XML files should be downloaded.
        /// </summary>
        /// <param name="sqlConnection">Sql connection or null.</param>
        /// <param name="sqlTransaction">Sql transaction or null.</param>
        /// <returns>List of objects of type Item.</returns>
        public List<IAItem> IAItemSelectForXMLDownload(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            String iaIdentifier)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHLImport"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("IAItemSelectForXMLDownload", connection, transaction,
                CustomSqlHelper.CreateInputParameter("IAIdentifier", SqlDbType.NVarChar, 200, false, iaIdentifier)))
            {
                using (CustomSqlHelper<IAItem> helper = new CustomSqlHelper<IAItem>())
                {
                    List<IAItem> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }

        /// <summary>
        /// Select items which can be published.
        /// </summary>
        /// <param name="sqlConnection">Sql connection or null.</param>
        /// <param name="sqlTransaction">Sql transaction or null.</param>
        /// <param name="itemID">Identifier of the item</param>
        /// <param name="minDaysBeforeAllowUnapproved">Age in days that an item must be before publishing is allowed</param>
        /// <returns>List of objects of type Item.</returns>
        public List<IAItem> IAItemSelectOKToPublish(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            int itemID,
            int minDaysBeforeAllowUnapproved)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHLImport"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("IAItemSelectOKToPublish", connection, transaction,
                CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemID),
                CustomSqlHelper.CreateInputParameter("Days", SqlDbType.Int, null, false, minDaysBeforeAllowUnapproved)))
            {
                using (CustomSqlHelper<IAItem> helper = new CustomSqlHelper<IAItem>())
                {
                    List<IAItem> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }

        /// <summary>
        /// Select items which should be published to the import tables.
        /// </summary>
        /// <param name="sqlConnection">Sql connection or null.</param>
        /// <param name="sqlTransaction">Sql transaction or null.</param>
        /// <returns>List of objects of type Item.</returns>
        public List<IAItem> IAItemSelectForPublishToImportTables(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            String iaIdentifier)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHLImport"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("IAItemSelectForPublishToImportTables", connection, transaction,
                CustomSqlHelper.CreateInputParameter("IAIdentifier", SqlDbType.NVarChar, 200, false, iaIdentifier)))
            {
                using (CustomSqlHelper<IAItem> helper = new CustomSqlHelper<IAItem>())
                {
                    List<IAItem> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }

        /// <summary>
        /// Publish the specified item to the production database.
        /// </summary>
        /// <param name="sqlConnection">Sql connection or null.</param>
        /// <param name="sqlTransaction">Sql transaction or null.</param>
        /// <param name="itemid">Identifier of the item to be published.</param>
        /// <returns>True if item published, false otherwise.</returns>
        public bool IAItemPublishToProduction(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            int itemID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHLImport"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("IAItemPublishToProduction", connection, transaction,
                CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemID)))
            {
                bool result = Convert.ToBoolean(CustomSqlHelper.ExecuteScalar(command));
                return result;
            }
        }

        /// <summary>
        /// Publish the specified item to the import tables.
        /// </summary>
        /// <param name="sqlConnection">Sql connection or null.</param>
        /// <param name="sqlTransaction">Sql transaction or null.</param>
        /// <param name="itemid">Identifier of the item to be published.</param>
        /// <returns>True if item published, false otherwise.</returns>
        public bool IAItemPublishToImportTables(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            int itemID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHLImport"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("IAItemPublishToImportTables", connection, transaction,
                CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemID)))
            {
                bool result = Convert.ToBoolean(CustomSqlHelper.ExecuteScalar(command));
                return result;
            }
        }

        /// <summary>
        /// Add new Internet Archive items from the IAAnalysis database to the Import database
        /// </summary>
        /// <param name="sqlConnection">Sql connection or null.</param>
        /// <param name="sqlTransaction">Sql transaction or null.</param>
        public void IAItemInsertFromIAAnalysis(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            String localFileFolder)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHLImport"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("IAItemInsertFromIAAnalysis", connection, transaction,
                CustomSqlHelper.CreateInputParameter("LocalFileFolder", SqlDbType.NVarChar, 200, false, localFileFolder)))
            {
                CustomSqlHelper.ExecuteScalar(command);
            }
        }

        public List<IAItem> IAItemSelectPendingApproval(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            int ageInDays)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHLImport"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("IAItemSelectPendingApproval", connection, transaction,
                CustomSqlHelper.CreateInputParameter("AgeInDays", SqlDbType.Int, null, false, ageInDays)))
            {
                using (CustomSqlHelper<IAItem> helper = new CustomSqlHelper<IAItem>())
                {
                    return helper.ExecuteReader(command);
                }
            }
        }

        public void IAItemResetForDownload(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string iaIdentifier)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHLImport"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("IAItemResetForDownload", connection, transaction,
                CustomSqlHelper.CreateInputParameter("IAIdentifier", SqlDbType.NVarChar, 200, false, iaIdentifier)))
            {
                CustomSqlHelper.ExecuteScalar(command);
            }
        }

        public List<IAItem> IAItemSelectByStatus(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int itemStatusId, int numberOfRows, int pageNumber,
            string sortColumn, string sortDirection)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHLImport"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("IAItemSelectByStatus", connection, transaction,
                CustomSqlHelper.CreateInputParameter("ItemStatusID", SqlDbType.Int, null, false, itemStatusId),
                CustomSqlHelper.CreateInputParameter("NumRows", SqlDbType.Int, null, false, numberOfRows),
                CustomSqlHelper.CreateInputParameter("PageNum", SqlDbType.Int, null, false, pageNumber),
                CustomSqlHelper.CreateInputParameter("SortColumn", SqlDbType.NVarChar, 150, false, sortColumn),
                CustomSqlHelper.CreateInputParameter("SortDirection", SqlDbType.NVarChar, 4, false, sortDirection)))
            {
                using (CustomSqlHelper<IAItem> helper = new CustomSqlHelper<IAItem>())
                {
                    List<IAItem> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }


    }
}
