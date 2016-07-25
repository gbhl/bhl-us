using CustomDataAccess;
using MOBOT.BHL.DataObjects;
using System;
using System.Data;
using System.Data.SqlClient;

namespace MOBOT.BHL.DAL
{
	public partial class ItemInstitutionDAL
	{
        public ItemInstitution ItemInstitutionInsert(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            int itemID, string institutionCode, string institutionRoleName, int userID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("ItemInstitutionInsert", connection, transaction,
                    CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemID),
                    CustomSqlHelper.CreateInputParameter("InstitutionCode", SqlDbType.NVarChar, 100, false, institutionCode),
                    CustomSqlHelper.CreateInputParameter("InstitutionRoleName", SqlDbType.NVarChar, 100, false, institutionRoleName),
                    CustomSqlHelper.CreateInputParameter("UserID", SqlDbType.Int, null, false, userID)))
            {
                using (CustomSqlHelper<ItemInstitution> helper = new CustomSqlHelper<ItemInstitution>())
                {
                    CustomGenericList<ItemInstitution> list = helper.ExecuteReader(command);
                    if (list.Count > 0)
                        return list[0];
                    else
                        return null;
                }
            }
        }

    }
}

