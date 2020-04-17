using CustomDataAccess;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace MOBOT.BHL.DAL
{
    public partial class InstitutionGroupInstitutionDAL
	{
        public void InstitutionGroupInstitutionsInsert(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            int institutionGroupID,
            List<string> institutionCodes)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = connection.CreateCommand())
            {
                // Set up table-valued stored procedure parameter
                DataTable codeTable = new DataTable();
                codeTable.Columns.Add("Code", typeof(string));
                foreach (string code in institutionCodes) codeTable.Rows.Add(code);

                command.CommandText = "InstitutionGroupInstitutionsInsertList";
                command.CommandType = CommandType.StoredProcedure;
                SqlParameter parameter1 = command.Parameters.AddWithValue("@InstitutionGroupID", institutionGroupID);
                parameter1.SqlDbType = SqlDbType.Int;
                SqlParameter parameter2 = command.Parameters.AddWithValue("@Codes", codeTable);
                parameter2.SqlDbType = SqlDbType.Structured;
                parameter2.TypeName = "dbo.InstitutionCodeTable";

                command.ExecuteNonQuery();
            }
        }
    }
}

