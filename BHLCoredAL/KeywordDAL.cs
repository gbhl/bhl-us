
#region Using

using System;
using System.Data;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHL.DataObjects;

#endregion Using

namespace MOBOT.BHL.DAL
{
	public partial class KeywordDAL
	{
        public CustomGenericList<CustomDataRow> KeywordSelectNewLocations(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("KeywordSelectNewLocations", connection, transaction))
            {
                return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
            }
        }

        /// <summary>
        /// Returns a list of title tags that have suspected character encoding problems.
        /// </summary>
        /// <param name="sqlConnection"></param>
        /// <param name="sqlTransaction"></param>
        /// <param name="institutionCode">Institution for which to return title tags</param>
        /// <param name="maxAge">Age in days of title tags to consider (i.e. title tags new in the last 30 days)</param>
        /// <returns></returns>
        public CustomGenericList<KeywordSuspectCharacter> KeywordSelectWithSuspectCharacters(
                SqlConnection sqlConnection,
                SqlTransaction sqlTransaction,
                String institutionCode,
                int maxAge)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;
            using (SqlCommand command = CustomSqlHelper.CreateCommand("KeywordSelectWithSuspectCharacters", connection, transaction,
                CustomSqlHelper.CreateInputParameter("InstitutionCode", SqlDbType.NVarChar, 10, false, institutionCode),
                CustomSqlHelper.CreateInputParameter("MaxAge", SqlDbType.Int, null, false, maxAge)))
            {
                using (CustomSqlHelper<KeywordSuspectCharacter> helper = new CustomSqlHelper<KeywordSuspectCharacter>())
                {
                    CustomGenericList<KeywordSuspectCharacter> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }

        public Keyword KeywordSelectByKeyword(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            string keyword)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;
            using (SqlCommand command = CustomSqlHelper.CreateCommand("KeywordSelectByKeyword", connection, transaction,
                CustomSqlHelper.CreateInputParameter("Keyword", SqlDbType.NVarChar, 50, false, keyword)))
            {
                using (CustomSqlHelper<Keyword> helper = new CustomSqlHelper<Keyword>())
                {
                    CustomGenericList<Keyword> list = helper.ExecuteReader(command);
                    if (list.Count > 0)
                        return list[0];
                    else
                        return null;
                }
            }
        }
    }
}
