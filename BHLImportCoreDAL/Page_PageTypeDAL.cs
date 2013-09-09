
#region Using

using System;
using System.Data;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHLImport.DataObjects;

#endregion Using

namespace MOBOT.BHLImport.DAL
{
	public partial class Page_PageTypeDAL
	{
        /// <summary>
        /// Select the new page_pagetype with the barcode, filenameprefix, sequence order,
        /// page type, and importSourceID.
        /// </summary>
        /// <param name="sqlConnection">Sql connection or null.</param>
        /// <param name="sqlTransaction">Sql transaction or null.</param>
        /// <param name="barCode"></param>
        /// <param name="fileNamePrefix"></param>
        /// <param name="sequenceOrder"></param>
        /// <param name="pageTypeID"></param>
        /// <param name="importSourceID"></param>
        /// <returns>Object of type Page_PageType.</returns>
        public Page_PageType Page_PageTypeSelectNewByKeyValuesAndSource(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            string barCode,
            string fileNamePrefix,
            int pageTypeID,
            int importSourceID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHLImport"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("Page_PageTypeSelectNewByKeyValuesAndSource", connection, transaction,
                CustomSqlHelper.CreateInputParameter("BarCode", SqlDbType.NVarChar, 40, false, barCode),
                CustomSqlHelper.CreateInputParameter("FileNamePrefix", SqlDbType.NVarChar, 50, false, fileNamePrefix),
                CustomSqlHelper.CreateInputParameter("PageTypeID", SqlDbType.Int, null, false, pageTypeID),
                CustomSqlHelper.CreateInputParameter("ImportSourceID", SqlDbType.Int, null, false, importSourceID)))
            {
                using (CustomSqlHelper<Page_PageType> helper = new CustomSqlHelper<Page_PageType>())
                {
                    CustomGenericList<Page_PageType> list = helper.ExecuteReader(command);

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
