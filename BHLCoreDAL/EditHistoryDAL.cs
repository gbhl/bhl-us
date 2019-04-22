using CustomDataAccess;
using MOBOT.BHL.DataObjects;
using System.Data;
using System.Data.SqlClient;

namespace MOBOT.BHL.DAL
{
    public class EditHistoryDAL
    {
        public CustomGenericList<EditHistory> EditHistorySelectByTitleID(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            int titleID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
            CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("EditHistorySelectByTitleID", connection, transaction,
                CustomSqlHelper.CreateInputParameter("TitleID", SqlDbType.NVarChar, 100, false, titleID.ToString())))
            {
                using (CustomSqlHelper<EditHistory> helper = new CustomSqlHelper<EditHistory>())
                {
                    CustomGenericList<EditHistory> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }
        public CustomGenericList<EditHistory> EditHistorySelectByItemID(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            int itemID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
            CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("EditHistorySelectByItemID", connection, transaction,
                CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.NVarChar, 100, false, itemID.ToString())))
            {
                using (CustomSqlHelper<EditHistory> helper = new CustomSqlHelper<EditHistory>())
                {
                    CustomGenericList<EditHistory> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }

        public CustomGenericList<EditHistory> EditHistorySelectBySegmentID(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            int segmentID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
            CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("EditHistorySelectBySegmentID", connection, transaction,
                CustomSqlHelper.CreateInputParameter("SegmentID", SqlDbType.NVarChar, 100, false, segmentID.ToString())))
            {
                using (CustomSqlHelper<EditHistory> helper = new CustomSqlHelper<EditHistory>())
                {
                    CustomGenericList<EditHistory> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }

        public CustomGenericList<EditHistory> EditHistorySelectByAuthorID(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            int authorID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
            CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("EditHistorySelectByAuthorID", connection, transaction,
                CustomSqlHelper.CreateInputParameter("AuthorID", SqlDbType.NVarChar, 100, false, authorID.ToString())))
            {
                using (CustomSqlHelper<EditHistory> helper = new CustomSqlHelper<EditHistory>())
                {
                    CustomGenericList<EditHistory> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }

        public CustomGenericList<EditHistory> EditHistorySelectByEntityAndID(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            string entitySchema, string entityName, string entityID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
            CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("EditHistorySelectByEntityAndID", connection, transaction,
                CustomSqlHelper.CreateInputParameter("EntitySchema", SqlDbType.NVarChar, 50, false, entitySchema),
                CustomSqlHelper.CreateInputParameter("EntityName", SqlDbType.NVarChar, 50, false, entityName),
                CustomSqlHelper.CreateInputParameter("EntityID", SqlDbType.NVarChar, 100, false, entityID)))
            {
                using (CustomSqlHelper<EditHistory> helper = new CustomSqlHelper<EditHistory>())
                {
                    CustomGenericList<EditHistory> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }
    }
}
