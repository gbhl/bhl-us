
#region Using

using CustomDataAccess;
using MOBOT.BHL.DataObjects;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

#endregion Using

namespace MOBOT.BHL.DAL
{
    public partial class NamePageDAL
	{
        public List<NamePage> NamePageSelectByPageID(SqlConnection sqlConnection, SqlTransaction sqlTransaction, int pageID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("NamePageSelectByPageID", connection, transaction,
                    CustomSqlHelper.CreateInputParameter("PageID", SqlDbType.Int, null, false, pageID)))
            {
                using (CustomSqlHelper<NamePage> helper = new CustomSqlHelper<NamePage>())
                {
                    return helper.ExecuteReader(command);
                }
            }
        }

        public List<NamePage> NamePageSelectByPageIDAndSource(SqlConnection sqlConnection, 
            SqlTransaction sqlTransaction, int pageID, string sourceName)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("NamePageSelectByPageIDAndSource", connection, transaction,
                    CustomSqlHelper.CreateInputParameter("PageID", SqlDbType.Int, null, false, pageID),
                    CustomSqlHelper.CreateInputParameter("SourceName", SqlDbType.NVarChar, 50, false, sourceName)))
            {
                using (CustomSqlHelper<NamePage> helper = new CustomSqlHelper<NamePage>())
                {
                    return helper.ExecuteReader(command);
                }
            }
        }

        public NamePage NamePageSelectByPageIDAndNameString(SqlConnection sqlConnection, SqlTransaction sqlTransaction, int pageID, string nameString)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("NamePageSelectByPageIDAndNameString", connection, transaction,
                    CustomSqlHelper.CreateInputParameter("PageID", SqlDbType.Int, null, false, pageID),
                    CustomSqlHelper.CreateInputParameter("NameString", SqlDbType.NVarChar, 100, false, nameString)))
            {
                using (CustomSqlHelper<NamePage> helper = new CustomSqlHelper<NamePage>())
                {
                    List<NamePage> list = helper.ExecuteReader(command);
                    if (list.Count > 0)
                        return list[0];
                    else
                        return null;
                }
            }
        }

        public NamePage NamePageInsert(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            int pageID, string nameString, string resolvedNameString, string canonicalNameString, 
            string identifierList, string sourceName, short isFirstOccurrence, int userID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("NamePageInsert", connection, transaction,
                    CustomSqlHelper.CreateInputParameter("PageID", SqlDbType.Int, null, false, pageID),
                    CustomSqlHelper.CreateInputParameter("NameString", SqlDbType.NVarChar, 100, false, nameString),
                    CustomSqlHelper.CreateInputParameter("ResolvedNameString", SqlDbType.NVarChar, 100, false, resolvedNameString),
                    CustomSqlHelper.CreateInputParameter("CanonicalNameString", SqlDbType.NVarChar, 100, false, canonicalNameString),
                    CustomSqlHelper.CreateInputParameter("IdentifierString", SqlDbType.NVarChar, 2000, false, identifierList),
                    CustomSqlHelper.CreateInputParameter("SourceName", SqlDbType.NVarChar, 50, false, sourceName),
                    CustomSqlHelper.CreateInputParameter("IsFirstOccurrence", SqlDbType.TinyInt, null, false, isFirstOccurrence),
                    CustomSqlHelper.CreateInputParameter("CreationUserID", SqlDbType.Int, null, false, userID),
                    CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, false, userID)))
            {
                using (CustomSqlHelper<NamePage> helper = new CustomSqlHelper<NamePage>())
                {
                    List<NamePage> list = helper.ExecuteReader(command);
                    if (list.Count > 0)
                        return list[0];
                    else
                        return null;
                }
            }
        }

        public void NamePageUpdate(SqlConnection sqlConnection, SqlTransaction sqlTransaction, 
            int namePageID, string nameString, string resolvedNameString, string nameBankID, string eolID, 
            short isFirstOccurrence, int userID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("NamePageUpdate", connection, transaction,
                    CustomSqlHelper.CreateInputParameter("NamePageID", SqlDbType.Int, null, false, namePageID),
                    CustomSqlHelper.CreateInputParameter("NameString", SqlDbType.NVarChar, 100, false, nameString),
                    CustomSqlHelper.CreateInputParameter("ResolvedNameString", SqlDbType.NVarChar, 100, false, resolvedNameString),
                    CustomSqlHelper.CreateInputParameter("NameBankID", SqlDbType.NVarChar, 100, false, nameBankID),
                    CustomSqlHelper.CreateInputParameter("EOLID", SqlDbType.NVarChar, 100, true, eolID),
                    CustomSqlHelper.CreateInputParameter("IsFirstOccurrence", SqlDbType.TinyInt, null, false, isFirstOccurrence),
                    CustomSqlHelper.CreateInputParameter("CreationUserID", SqlDbType.Int, null, false, userID),
                    CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, false, userID)))
            {
                CustomSqlHelper.ExecuteNonQuery(command);
            }
        }

        public void NamePageDelete(SqlConnection sqlConnection, SqlTransaction sqlTransaction, int namePageID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("NamePageDelete", connection, transaction,
                    CustomSqlHelper.CreateInputParameter("NamePageID", SqlDbType.Int, null, false, namePageID)))
            {
                CustomSqlHelper.ExecuteNonQuery(command);
            }
        }

        public void NamePageDeleteByItemID(SqlConnection sqlConnection, SqlTransaction sqlTransaction, int itemID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("NamePageDeleteByItemID", connection, transaction,
                    CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemID)))
            {
                CustomSqlHelper.ExecuteNonQuery(command);
            }
        }
    }
}
