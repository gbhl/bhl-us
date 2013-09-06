using System;
using System.Data;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHL.DataObjects;

namespace MOBOT.BHL.DAL
{
	public partial class TitleAssociationTypeDAL
	{
        public CustomGenericList<TitleAssociationType> SelectAll(SqlConnection sqlConnection, SqlTransaction sqlTransaction)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleAssociationTypeSelectAll", connection, transaction))
            {
                using (CustomSqlHelper<TitleAssociationType> helper = new CustomSqlHelper<TitleAssociationType>())
                {
                    CustomGenericList<TitleAssociationType> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }
    }
}
