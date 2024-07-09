using CustomDataAccess;
using MOBOT.BHL.DataObjects;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace MOBOT.BHL.DAL
{
    public partial class ExternalResourceTypeDAL
    {
        public List<ExternalResourceType> ExternalResourceTypeSelectByIDType(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
          string idTypeName)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
              CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("ExternalResourceTypeSelectByIDType", connection, transaction,
                CustomSqlHelper.CreateInputParameter("IDTypeName", SqlDbType.NVarChar, 50, false, idTypeName)))
            {
                using (CustomSqlHelper<ExternalResourceType> helper = new CustomSqlHelper<ExternalResourceType>())
                {
                    List<ExternalResourceType> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }
    }
}
