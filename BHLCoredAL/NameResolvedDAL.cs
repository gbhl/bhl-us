
#region Using

using System;
using System.Data;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHL.DataObjects;

#endregion Using

namespace MOBOT.BHL.DAL
{
	public partial class NameResolvedDAL
	{
        public CustomGenericList<NameResolved> NameResolvedSelectByPageID(SqlConnection sqlConnection, SqlTransaction sqlTransaction, int pageID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("NameResolvedSelectByPageID", connection, transaction,
                    CustomSqlHelper.CreateInputParameter("PageID", SqlDbType.Int, null, false, pageID)))
            {
                using (CustomSqlHelper<NameResolved> helper = new CustomSqlHelper<NameResolved>())
                {
                    return helper.ExecuteReader(command);
                }
            }
        }

        public CustomGenericList<NameResolved> NameResolvedSelectByNameLike(SqlConnection sqlConnection, 
            SqlTransaction sqlTransaction, string name, int returnCount )
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("NameResolvedSelectByNameLike", connection, transaction,
				    CustomSqlHelper.CreateInputParameter( "NameConfirmed", SqlDbType.NVarChar, 100, false, name ),
                    CustomSqlHelper.CreateInputParameter( "ReturnCount", SqlDbType.Int, null, false, returnCount)))
            {
                using (CustomSqlHelper<NameResolved> helper = new CustomSqlHelper<NameResolved>())
                {
                    return helper.ExecuteReader(command);
                }
            }
        }

        public NameResolved NameResolvedSelectByResolvedName(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, string name)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("NameResolvedSelectByResolvedName", connection, transaction,
                    CustomSqlHelper.CreateInputParameter("ResolvedNameString", SqlDbType.NVarChar, 100, false, name)))
            {
                using (CustomSqlHelper<NameResolved> helper = new CustomSqlHelper<NameResolved>())
                {
                    CustomGenericList<NameResolved> list = helper.ExecuteReader(command);
                    if (list.Count > 0)
                        return list[0];
                    else
                        return null;
                }
            }
        }

        public CustomGenericList<CustomDataRow> NameResolvedSearchForPages(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, string name, int numberOfRows, int pageNumber,
            string sortColumn, string sortDirection)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("NameResolvedSearchForPages", connection, transaction,
                CustomSqlHelper.CreateInputParameter("ResolvedNameString", SqlDbType.NVarChar, 100, false, name),
                CustomSqlHelper.CreateInputParameter("NumRows", SqlDbType.Int, null, false, numberOfRows),
                CustomSqlHelper.CreateInputParameter("PageNum", SqlDbType.Int, null, false, pageNumber),
                CustomSqlHelper.CreateInputParameter("SortColumn", SqlDbType.NVarChar, 150, false, sortColumn),
                CustomSqlHelper.CreateInputParameter("SortDirection", SqlDbType.NVarChar, 4, false, sortDirection)))
            {
                CustomGenericList<CustomDataRow> list = CustomSqlHelper.ExecuteReaderAndReturnRows(command);
                return list;
            }
        }

        public CustomGenericList<CustomDataRow> NameResolvedSearchForPagesDownload(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, string name)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("NameResolvedSearchForPagesDownload", connection, transaction,
                CustomSqlHelper.CreateInputParameter("ResolvedNameString", SqlDbType.NVarChar, 100, false, name)))
            {
                CustomGenericList<CustomDataRow> list = CustomSqlHelper.ExecuteReaderAndReturnRows(command);
                return list;
            }
        }
    }
}
