using CustomDataAccess;
using MOBOT.BHL.DataObjects;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace MOBOT.BHL.DAL
{
    public partial class Title_IdentifierDAL
	{
        public List<Title_Identifier> Title_IdentifierSelectByTitleID(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            int titleID,
            int? display)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("Title_IdentifierSelectByTitleID", connection, transaction,
                CustomSqlHelper.CreateInputParameter("TitleID", SqlDbType.Int, null, false, titleID),
                CustomSqlHelper.CreateInputParameter("Display", SqlDbType.Int, null, true, display)))
            {
                using (CustomSqlHelper<Title_Identifier> helper = new CustomSqlHelper<Title_Identifier>())
                {
                    List<Title_Identifier> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }

        public List<Title_Identifier> Title_IdentifierSelectByNameAndID(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            string identifierName,
            int titleID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("Title_IdentifierSelectByNameAndID", connection, transaction,
                CustomSqlHelper.CreateInputParameter("IdentifierName", SqlDbType.NVarChar, 40, false, identifierName),
                CustomSqlHelper.CreateInputParameter("TitleID", SqlDbType.Int, null, true, titleID)))
            {
                using (CustomSqlHelper<Title_Identifier> helper = new CustomSqlHelper<Title_Identifier>())
                {
                    List<Title_Identifier> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }
    }
}
