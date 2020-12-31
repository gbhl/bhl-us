
#region Using

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHL.DataObjects;

#endregion Using

namespace MOBOT.BHL.DAL
{
	public partial class AnnotatedPageDAL
	{
        public AnnotatedPage AnnotatedPageSelectByExternalIdentifer(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            String externalIdentifier,
            int annotatedItemID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("annotation.AnnotatedPageSelectByExternalIdentifer",
                connection, transaction,
                CustomSqlHelper.CreateInputParameter("ExternalIdentifier", SqlDbType.NVarChar, 50, false, externalIdentifier),
                CustomSqlHelper.CreateInputParameter("AnnotatedItemID", SqlDbType.Int, null, false, annotatedItemID)))
            {
                using (CustomSqlHelper<AnnotatedPage> helper = new CustomSqlHelper<AnnotatedPage>())
                {
                    List<AnnotatedPage> list = helper.ExecuteReader(command);
                    if (list.Count > 0)
                        return list[0];
                    else
                        return null;
                }
            }
        }

        public AnnotatedPage AnnotatedPageSelectByPageID(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            int pageID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("annotation.AnnotatedPageSelectByPageID",
                connection, transaction,
                CustomSqlHelper.CreateInputParameter("PageID", SqlDbType.Int, null, false, pageID)))
            {
                using (CustomSqlHelper<AnnotatedPage> helper = new CustomSqlHelper<AnnotatedPage>())
                {
                    List<AnnotatedPage> list = helper.ExecuteReader(command);
                    if (list.Count > 0)
                        return list[0];
                    else
                        return null;
                }
            }
        }

        public CustomDataRow GetRelatedPageByExternalIdentifier( SqlConnection sqlConnection, SqlTransaction sqlTransaction, string externalIdentifier)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("annotation.GetRelatedPageByExternalIdentifier",
                connection, transaction,
                CustomSqlHelper.CreateInputParameter("ExternalIdentifier", SqlDbType.NVarChar, 50, false, externalIdentifier)))
            {
                using (CustomSqlHelper<AnnotatedPage> helper = new CustomSqlHelper<AnnotatedPage>())
                {
                    List<CustomDataRow> list = CustomSqlHelper.ExecuteReaderAndReturnRows(command);
                    if (list.Count > 0)
                        return list[0];
                    else
                        return null;
                }
            }
        }

    }
}
