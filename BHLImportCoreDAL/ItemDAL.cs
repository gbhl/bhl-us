
#region Using

using System;
using System.Data;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHLImport.DataObjects;

#endregion Using

namespace MOBOT.BHLImport.DAL
{
	public partial class ItemDAL
	{
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

            using (SqlCommand command = CustomSqlHelper.CreateCommand("ItemSelectNewByBarCodeAndSource", connection, transaction,
                CustomSqlHelper.CreateInputParameter("BarCode", SqlDbType.NVarChar, 40, false, barCode),
                CustomSqlHelper.CreateInputParameter("ImportSourceID", SqlDbType.Int, null, false, importSourceID)))
            {
                using (CustomSqlHelper<Item> helper = new CustomSqlHelper<Item>())
                {
                    CustomGenericList<Item> list = helper.ExecuteReader(command);

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
