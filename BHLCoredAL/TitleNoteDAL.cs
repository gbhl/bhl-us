using System;
using System.Data;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHL.DataObjects;

namespace MOBOT.BHL.DAL
{
	public partial class TitleNoteDAL
	{
        public CustomGenericList<TitleNote> TitleNoteSelectByTitleID(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int titleID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleNoteSelectByTitleID", connection, transaction,
                CustomSqlHelper.CreateInputParameter("TitleID", SqlDbType.Int, null, false, titleID)))
            {
                using (CustomSqlHelper<TitleNote> helper = new CustomSqlHelper<TitleNote>())
                {
                    CustomGenericList<TitleNote> list = helper.ExecuteReader(command);

                    return list;
                }
            }
        }
	}
}
