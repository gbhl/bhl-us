using CustomDataAccess;
using MOBOT.BHL.DataObjects;
using System;
using System.Data;
using System.Data.SqlClient;

namespace MOBOT.BHL.DAL
{
	public partial class TitleInstitutionDAL
	{
        public TitleInstitution TitleInstitutionInsert(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            int titleID, string institutionCode, string institutionRoleName, string url, int userID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleInstitutionInsert", connection, transaction,
                    CustomSqlHelper.CreateInputParameter("TitleID", SqlDbType.Int, null, false, titleID),
                    CustomSqlHelper.CreateInputParameter("InstitutionCode", SqlDbType.NVarChar, 100, false, institutionCode),
                    CustomSqlHelper.CreateInputParameter("InstitutionRoleName", SqlDbType.NVarChar, 100, false, institutionRoleName),
                    CustomSqlHelper.CreateInputParameter("Url", SqlDbType.NVarChar, 500, false, url),
                    CustomSqlHelper.CreateInputParameter("UserID", SqlDbType.Int, null, false, userID)))
            {
                using (CustomSqlHelper<TitleInstitution> helper = new CustomSqlHelper<TitleInstitution>())
                {
                    CustomGenericList<TitleInstitution> list = helper.ExecuteReader(command);
                    if (list.Count > 0)
                        return list[0];
                    else
                        return null;
                }
            }
        }

        public CustomGenericList<TitleInstitution> TitleSelectWithExternalContentProvider(SqlConnection sqlConnection, SqlTransaction sqlTransaction)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleSelectWithExternalContentProvider", connection, transaction))
            {
                using (CustomSqlHelper<TitleInstitution> helper = new CustomSqlHelper<TitleInstitution>())
                {
                    CustomGenericList<TitleInstitution> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }
    }
}

