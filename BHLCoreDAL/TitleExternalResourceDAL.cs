using CustomDataAccess;
using MOBOT.BHL.DataObjects;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace MOBOT.BHL.DAL
{
    public partial class TitleExternalResourceDAL
    {
        public List<TitleExternalResource> TitleExternalResourceSelectByTitleID(SqlConnection sqlConnection,
         SqlTransaction sqlTransaction, int titleID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleExternalResourceSelectByTitleID", connection, transaction,
                CustomSqlHelper.CreateInputParameter("TitleID", SqlDbType.Int, null, false, titleID)))
            {
                using (CustomSqlHelper<TitleExternalResource> helper = new CustomSqlHelper<TitleExternalResource>())
                {
                    List<TitleExternalResource> list = helper.ExecuteReader(command);

                    return list;
                }
            }
        }
    }
}

