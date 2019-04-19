using CustomDataAccess;
using MOBOT.BHL.DataObjects;
using System.Data;
using System.Data.SqlClient;

namespace MOBOT.BHL.DAL
{
    public class EditHistoryDAL
    {
        public CustomGenericList<EditHistory> EditHistorySelectByItemID(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            int itemID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
            CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("EditHistorySelectByItemID", connection, transaction,
                CustomSqlHelper.CreateInputParameter("InstitutionCode", SqlDbType.NVarChar, 100, false, itemID.ToString())))
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
