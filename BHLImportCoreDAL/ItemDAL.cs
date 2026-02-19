
#region Using

using CustomDataAccess;
using MOBOT.BHLImport.DataObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

#endregion Using

namespace MOBOT.BHLImport.DAL
{
    public partial class ItemDAL
	{
        public List<Item> ItemSelectForPublishToProduction(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string barCode)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHLImport"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;
            using (SqlCommand command = CustomSqlHelper.CreateCommand("dbo.ItemSelectForPublishToProduction", connection, transaction,
                CustomSqlHelper.CreateInputParameter("Barcode", SqlDbType.NVarChar, 200, false, barCode)))
            {
                using (CustomSqlHelper<Item> helper = new CustomSqlHelper<Item>())
                {
                    List<Item> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }

        public CustomDataRow ItemPublishToProductionIA(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string barCode)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHLImport"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;
            using (SqlCommand command = CustomSqlHelper.CreateCommand("dbo.ItemPublishToProductionIA", connection, transaction,
                CustomSqlHelper.CreateInputParameter("BarCode", SqlDbType.NVarChar, 200, false, barCode)))
            {
                List<CustomDataRow> list = CustomSqlHelper.ExecuteReaderAndReturnRows(command);
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

        /// <summary>
        /// Select the new item with the barcode and importSourceID.
        /// </summary>
        /// <param name="sqlConnection">Sql connection or null.</param>
        /// <param name="sqlTransaction">Sql transaction or null.</param>
        /// <param name="barCode"></param>
        /// <param name="importSourceID"></param>
        /// <returns>Object of type Item.</returns>
        public Item ItemSelectNewByBarCodeAndSource(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            string barCode,
            int importSourceID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHLImport"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("dbo.ItemSelectNewByBarCodeAndSource", connection, transaction,
                CustomSqlHelper.CreateInputParameter("BarCode", SqlDbType.NVarChar, 200, false, barCode),
                CustomSqlHelper.CreateInputParameter("ImportSourceID", SqlDbType.Int, null, false, importSourceID)))
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
    }
}
