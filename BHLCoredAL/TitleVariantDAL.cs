
#region Using

using System;
using System.Data;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHL.DataObjects;

#endregion Using

namespace MOBOT.BHL.DAL
{
	public partial class TitleVariantDAL
	{
        public CustomGenericList<TitleVariant> TitleVariantSelectByTitleID(SqlConnection sqlConnection,
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
                    CustomGenericList<TitleVariant> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }

        public CustomGenericList<TitleVariantType> TitleVariantTypeSelectAll(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleVariantTypeSelectAll", connection, transaction))
            {
                using (CustomSqlHelper<TitleVariantType> helper = new CustomSqlHelper<TitleVariantType>())
                {
                    CustomGenericList<TitleVariantType> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }
    }
}
