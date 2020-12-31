using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHL.DataObjects;

namespace MOBOT.BHL.DAL
{
	public partial class MarcImportBatchDAL
	{
        public List<MarcImportBatch> MarcImportBatchSelectStatsByInstitution(
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
                    List<MarcImportBatch> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }
    }
}
