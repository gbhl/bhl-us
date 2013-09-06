
#region Using

using System;
using System.Data;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHL.DataObjects;

#endregion Using

namespace MOBOT.BHL.DAL
{
	public partial class AnnotatedItemDAL
	{
        public AnnotatedItem AnnotatedItemSelectByExternalIdentifer(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            String externalIdentifier,
            int annotatedTitleID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("annotation.AnnotatedItemSelectByExternalIdentifer",
                connection, transaction,
                CustomSqlHelper.CreateInputParameter("ExternalIdentifier", SqlDbType.NVarChar, 50, false, externalIdentifier),
                CustomSqlHelper.CreateInputParameter("AnnotatedTitleID", SqlDbType.Int, null, false, annotatedTitleID)))
            {
                using (CustomSqlHelper<AnnotatedItem> helper = new CustomSqlHelper<AnnotatedItem>())
                {
                    CustomGenericList<AnnotatedItem> list = helper.ExecuteReader(command);
                    if (list.Count > 0)
                        return list[0];
                    else
                        return null;
                }
            }
        }

        public bool AnnotatedItemCheckForSurrogate(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            int annotatedItemID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;


            using (SqlCommand command = CustomSqlHelper.CreateCommand("annotation.AnnotatedItemCheckForSurrogate",
                connection, transaction,
                CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, annotatedItemID)))
            {
                using (CustomSqlHelper<bool> helper = new CustomSqlHelper<bool>())
                {
                    CustomGenericList<bool> list = helper.ExecuteReader(command);
                    if (list.Count > 0)
                        return list[0];
                    else
                        return false;
                }
            }
        }
    }
}
