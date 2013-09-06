using System;
using System.Data;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHL.DataObjects;

namespace MOBOT.BHL.DAL
{
	public partial class ItemNameFileLogDAL
	{
        public void ItemNameFileLogRefreshSinceDate(SqlConnection sqlConnection, 
            SqlTransaction sqlTransaction, DateTime startDate)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("ItemNameFileLogRefreshSinceDate", 
                connection, transaction,
                CustomSqlHelper.CreateInputParameter("StartDate", SqlDbType.DateTime, null, true, startDate)))
            {
                command.ExecuteNonQuery();
            }
        }

        public CustomGenericList<ItemNameFileLog> ItemNameFileLogSelectForCreate(SqlConnection sqlConnection, SqlTransaction sqlTransaction)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("ItemNameFileLogSelectForCreate", connection, transaction))
            {
                using (CustomSqlHelper<ItemNameFileLog> helper = new CustomSqlHelper<ItemNameFileLog>())
                {
                    CustomGenericList<ItemNameFileLog> list = helper.ExecuteReader(command);
                    if (list.Count > 0)
                    {
                        return list;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        public CustomGenericList<ItemNameFileLog> ItemNameFileLogSelectForUpload(SqlConnection sqlConnection, SqlTransaction sqlTransaction)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("ItemNameFileLogSelectForUpload", connection, transaction))
            {
                using (CustomSqlHelper<ItemNameFileLog> helper = new CustomSqlHelper<ItemNameFileLog>())
                {
                    CustomGenericList<ItemNameFileLog> list = helper.ExecuteReader(command);
                    if (list.Count > 0)
                    {
                        return list;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

    }
}
