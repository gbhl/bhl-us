
#region Using

using CustomDataAccess;
using MOBOT.BHL.DataObjects;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

#endregion Using

namespace MOBOT.BHL.DAL
{
    public partial class TitleVariantDAL
	{
        public List<TitleVariant> TitleVariantSelectByTitleID(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            int titleID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleVariantSelectByTitleID", connection, transaction,
                    CustomSqlHelper.CreateInputParameter("TitleID", SqlDbType.Int, null, false, titleID)))
            {
                using (CustomSqlHelper<TitleVariant> helper = new CustomSqlHelper<TitleVariant>())
                {
                    List<TitleVariant> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }

        public List<TitleVariantType> TitleVariantTypeSelectAll(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleVariantTypeSelectAll", connection, transaction))
            {
                using (CustomSqlHelper<TitleVariantType> helper = new CustomSqlHelper<TitleVariantType>())
                {
                    List<TitleVariantType> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }
    }
}
