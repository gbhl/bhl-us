using System;
using System.Data;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHL.DataObjects;

namespace MOBOT.BHL.DAL
{
	public partial class IdentifierDAL
	{
        public CustomGenericList<Identifier> IdentifierSelectAll(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("IdentifierSelectAll", connection, transaction))
            {
                using (CustomSqlHelper<Identifier> helper = new CustomSqlHelper<Identifier>())
                {
                    CustomGenericList<Identifier> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }

        public CustomGenericList<Identifier> IdentifierSelectByIDType(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
          string idTypeName)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
              CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("IdentifierSelectByIDType", connection, transaction,
                CustomSqlHelper.CreateInputParameter("IDTypeName", SqlDbType.NVarChar, 50, false, idTypeName)))
            {
                using (CustomSqlHelper<Identifier> helper = new CustomSqlHelper<Identifier>())
                {
                    CustomGenericList<Identifier> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }

        public Identifier IdentifierSelectByGNFinderDataSource(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
          int gnDataSourceID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
              CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("IdentifierSelectByGNFinderDataSource", connection, transaction,
                CustomSqlHelper.CreateInputParameter("DataSourceID", SqlDbType.Int, null, false, gnDataSourceID)))
            {
                using (CustomSqlHelper<Identifier> helper = new CustomSqlHelper<Identifier>())
                {
                    CustomGenericList<Identifier> list = helper.ExecuteReader(command);
                    if (list.Count > 0)
                        return list[0];
                    else
                        return null;
                }
            }
        }
    }
}
