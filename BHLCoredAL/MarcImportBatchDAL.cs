using System;
using System.Data;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHL.DataObjects;

namespace MOBOT.BHL.DAL
{
	public partial class MarcImportBatchDAL
	{
        public CustomGenericList<MarcImportBatch> MarcImportBatchSelectStatsByInstitution(
                SqlConnection sqlConnection,
                SqlTransaction sqlTransaction,
                String institutionCode)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
            CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("MarcImportBatchSelectStatsByInstitution", connection, transaction,
                CustomSqlHelper.CreateInputParameter("InstitutionCode", SqlDbType.NVarChar, 10, false, institutionCode)))
            {
                using (CustomSqlHelper<MarcImportBatch> helper = new CustomSqlHelper<MarcImportBatch>())
                {
                    CustomGenericList<MarcImportBatch> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }
    }
}
