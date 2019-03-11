
#region Using

using System;
using System.Data;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHLImport.DataObjects;

#endregion Using

namespace MOBOT.BHLImport.DAL
{
	public partial class IndicatedPageDAL
	{
        /// <summary>
        /// Select the new indicated page with the barcode, filenameprefix, sequence order,
        /// sequence, and importSourceID.
        /// </summary>
        /// <param name="sqlConnection">Sql connection or null.</param>
        /// <param name="sqlTransaction">Sql transaction or null.</param>
        /// <param name="barCode"></param>
        /// <param name="fileNamePrefix"></param>
        /// <param name="sequenceOrder"></param>
        /// <param name="sequence"></param>
        /// <param name="importSourceID"></param>
        /// <returns>Object of type IndicatedPage.</returns>
        public IndicatedPage IndicatedPageSelectNewByKeyValuesAndSource(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            string barCode,
            string fileNamePrefix,
            short? sequence,
            int importSourceID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHLImport"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("IndicatedPageSelectNewByKeyValuesAndSource", connection, transaction,
                CustomSqlHelper.CreateInputParameter("BarCode", SqlDbType.NVarChar, 40, false, barCode),
                CustomSqlHelper.CreateInputParameter("FileNamePrefix", SqlDbType.NVarChar, 200, false, fileNamePrefix),
                CustomSqlHelper.CreateInputParameter("Sequence", SqlDbType.TinyInt, null, true, sequence),
                CustomSqlHelper.CreateInputParameter("ImportSourceID", SqlDbType.Int, null, false, importSourceID)))
            {
                using (CustomSqlHelper<IndicatedPage> helper = new CustomSqlHelper<IndicatedPage>())
                {
                    CustomGenericList<IndicatedPage> list = helper.ExecuteReader(command);

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
