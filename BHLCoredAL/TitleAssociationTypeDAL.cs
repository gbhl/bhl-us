using CustomDataAccess;
using MOBOT.BHL.DataObjects;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace MOBOT.BHL.DAL
{
    public partial class TitleAssociationTypeDAL
	{
        public List<TitleAssociationType> SelectAll(SqlConnection sqlConnection, SqlTransaction sqlTransaction)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleAssociationTypeSelectAll", connection, transaction))
            {
                using (CustomSqlHelper<TitleAssociationType> helper = new CustomSqlHelper<TitleAssociationType>())
                {
                    List<TitleAssociationType> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }
    }
}
