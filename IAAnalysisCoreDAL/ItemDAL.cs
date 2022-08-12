
#region Using

using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.IAAnalysis.DataObjects;

#endregion Using

namespace MOBOT.IAAnalysis.DAL
{
	public partial class ItemDAL
	{
        /// <summary>
        /// Select the item with the specified Identifier.
        /// </summary>
        /// <param name="sqlConnection">Sql connection or null.</param>
        /// <param name="sqlTransaction">Sql transaction or null.</param>
        /// <returns>Object of type Item.</returns>
        public Item ItemSelectByIdentifier(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            string identifier)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("IAAnalysis"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("ItemSelectByIdentifier", connection, transaction,
                CustomSqlHelper.CreateInputParameter("Identifier", SqlDbType.NVarChar, 200, false, identifier)))
            {
                using (CustomSqlHelper<Item> helper = new CustomSqlHelper<Item>())
                {
                    List<Item> list = helper.ExecuteReader(command);

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
        public List<Item> ItemSelectForXMLDownload(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("IAAnalysis"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("ItemSelectForXMLDownload", connection, transaction))
            {
                using (CustomSqlHelper<Item> helper = new CustomSqlHelper<Item>())
                {
                    List<Item> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }

        public List<CustomDataRow> ItemSelectNextStartDate(SqlConnection sqlConnection, SqlTransaction sqlTransaction)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("IAAnalysis"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("dbo.ItemSelectNextStartDate", connection, transaction))
            {
                return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
            }
        }
    }
}
