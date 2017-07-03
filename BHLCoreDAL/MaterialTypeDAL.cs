using CustomDataAccess;
using MOBOT.BHL.DataObjects;
using System;
using System.Data.SqlClient;

namespace MOBOT.BHL.DAL
{
	public partial class MaterialTypeDAL
	{
        public CustomGenericList<MaterialType> MaterialTypeSelectAll(SqlConnection sqlConnection,
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
                    CustomGenericList<MaterialType> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }
    }
}

