using System.Data;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHL.DataObjects;

namespace MOBOT.BHL.DAL
{
	public partial class TitleLanguageDAL
	{
        public CustomGenericList<TitleLanguage> TitleLanguageSelectByTitleID(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int titleId)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleLanguageSelectByTitleID", connection, transaction,
                            CustomSqlHelper.CreateInputParameter("TitleID", SqlDbType.Int, null, false, titleId)))
            {
                using (CustomSqlHelper<TitleLanguage> helper = new CustomSqlHelper<TitleLanguage>())
                {
                    CustomGenericList<TitleLanguage> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }
	}
}
