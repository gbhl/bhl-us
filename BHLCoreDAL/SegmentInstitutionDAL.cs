using CustomDataAccess;
using MOBOT.BHL.DataObjects;
using System;
using System.Data;
using System.Data.SqlClient;

namespace MOBOT.BHL.DAL
{
	public partial class SegmentInstitutionDAL
	{
        public SegmentInstitution SegmentInstitutionInsert(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
        int segmentID, string institutionCode, string institutionRoleName, int userID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("SegmentInstitutionInsert", connection, transaction,
                    CustomSqlHelper.CreateInputParameter("SegmentID", SqlDbType.Int, null, false, segmentID),
                    CustomSqlHelper.CreateInputParameter("InstitutionCode", SqlDbType.NVarChar, 100, false, institutionCode),
                    CustomSqlHelper.CreateInputParameter("InstitutionRoleName", SqlDbType.NVarChar, 100, false, institutionRoleName),
                    CustomSqlHelper.CreateInputParameter("UserID", SqlDbType.Int, null, false, userID)))
            {
                using (CustomSqlHelper<SegmentInstitution> helper = new CustomSqlHelper<SegmentInstitution>())
                {
                    CustomGenericList<SegmentInstitution> list = helper.ExecuteReader(command);
                    if (list.Count > 0)
                        return list[0];
                    else
                        return null;
                }
            }
        }
    }
}

