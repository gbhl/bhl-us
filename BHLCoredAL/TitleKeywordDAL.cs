
#region Using

using System;
using System.Data;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHL.DataObjects;

#endregion Using

namespace MOBOT.BHL.DAL
{
	public partial class TitleKeywordDAL
	{
        public CustomGenericList<TitleKeyword> TitleKeywordSelectByTitleID(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            int titleId)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleKeywordSelectByTitleID", connection, transaction,
                CustomSqlHelper.CreateInputParameter("TitleID", SqlDbType.Int, null, false, titleId)))
            {
                using (CustomSqlHelper<TitleKeyword> helper = new CustomSqlHelper<TitleKeyword>())
                {
                    CustomGenericList<TitleKeyword> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }

        public CustomGenericList<TitleKeyword> TitleKeywordSelectLikeTag(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            string tag,
            string languageCode,
            int returnCount)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleKeywordSelectLikeTag", connection, transaction,
                    CustomSqlHelper.CreateInputParameter("Tag", SqlDbType.NVarChar, 50, false, tag),
                    CustomSqlHelper.CreateInputParameter("LanguageCode", SqlDbType.NVarChar, 10, false, languageCode),
                    CustomSqlHelper.CreateInputParameter("ReturnCount", SqlDbType.Int, null, false, returnCount)))
            {
                using (CustomSqlHelper<TitleKeyword> helper = new CustomSqlHelper<TitleKeyword>())
                {
                    CustomGenericList<TitleKeyword> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }

        public CustomGenericList<TitleKeyword> TitleKeywordSelectKeywordByTitle(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            int titleId)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("KeywordSelectKeywordByTitleID", connection, transaction,
                CustomSqlHelper.CreateInputParameter("TitleID", SqlDbType.Int, null, false, titleId)))
            {
                using (CustomSqlHelper<TitleKeyword> helper = new CustomSqlHelper<TitleKeyword>())
                {
                    CustomGenericList<TitleKeyword> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }
    }
}
