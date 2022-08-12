using CustomDataAccess;
using MOBOT.BHL.DataObjects;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace MOBOT.BHL.DAL
{
    public partial class MaterialTypeDAL
	{
        public List<MaterialType> MaterialTypeSelectAll(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("MaterialTypeSelectAll",
                connection, transaction))
            {
                using (CustomSqlHelper<MaterialType> helper = new CustomSqlHelper<MaterialType>())
                {
                    List<MaterialType> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }
    }
}

