using System;
using System.Data;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHL.DataObjects;

namespace MOBOT.BHL.DAL
{
	public partial class Title_IdentifierDAL
	{
        public CustomGenericList<Title_Identifier> Title_IdentifierSelectByTitleID(
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
                    CustomGenericList<Title_Identifier> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }
    }
}
