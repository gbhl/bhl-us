
#region Using

using System;
using System.Data;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHLImport.DataObjects;

#endregion Using

namespace MOBOT.BHLImport.DAL
{
	public partial class PageNameDAL
	{
        /// <summary>
        /// Select the new page name with the barcode, filenameprefix, sequence order,
        /// name found, and importSourceID.
        /// </summary>
        /// <param name="sqlConnection">Sql connection or null.</param>
        /// <param name="sqlTransaction">Sql transaction or null.</param>
        /// <param name="barCode"></param>
        /// <param name="fileNamePrefix"></param>
        /// <param name="sequenceOrder"></param>
        /// <param name="pageTypeID"></param>
        /// <param name="importSourceID"></param>
        /// <returns>Object of type PageName.</returns>
        public PageName PageNameSelectNewByKeyValuesAndSource(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            string barCode,
            string fileNamePrefix,
            string nameFound,
            int importSourceID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHLImport"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("PageNameSelectNewByKeyValuesAndSource", connection, transaction,
                CustomSqlHelper.CreateInputParameter("BarCode", SqlDbType.NVarChar, 40, false, barCode),
                CustomSqlHelper.CreateInputParameter("FileNamePrefix", SqlDbType.NVarChar, 50, false, fileNamePrefix),
                CustomSqlHelper.CreateInputParameter("NameFound", SqlDbType.NVarChar, 100, false, nameFound),
                CustomSqlHelper.CreateInputParameter("ImportSourceID", SqlDbType.Int, null, false, importSourceID)))
            {
                using (CustomSqlHelper<PageName> helper = new CustomSqlHelper<PageName>())
                {
                    CustomGenericList<PageName> list = helper.ExecuteReader(command);

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
