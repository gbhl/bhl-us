using CustomDataAccess;
using MOBOT.BHL.DataObjects;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace MOBOT.BHL.DAL
{
	public partial class TitleExternalResourceTypeDAL
	{
        public List<TitleExternalResourceType> TitleExternalResourceTypeSelectAll(SqlConnection sqlConnection, SqlTransaction sqlTransaction)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleExternalResourceTypeSelectAll", connection, transaction))
            {
                using (CustomSqlHelper<TitleExternalResourceType> helper = new CustomSqlHelper<TitleExternalResourceType>())
                {
                    List<TitleExternalResourceType> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }
    }
}

