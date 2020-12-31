using System;
using System.Collections.Generic;
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

        public List<ItemNameFileLog> ItemNameFileLogSelectForCreate(SqlConnection sqlConnection, SqlTransaction sqlTransaction)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("ItemNameFileLogSelectForCreate", connection, transaction))
            {
                using (CustomSqlHelper<ItemNameFileLog> helper = new CustomSqlHelper<ItemNameFileLog>())
                {
                    List<ItemNameFileLog> list = helper.ExecuteReader(command);
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

        public List<ItemNameFileLog> ItemNameFileLogSelectForUpload(SqlConnection sqlConnection, SqlTransaction sqlTransaction)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("ItemNameFileLogSelectForUpload", connection, transaction))
            {
                using (CustomSqlHelper<ItemNameFileLog> helper = new CustomSqlHelper<ItemNameFileLog>())
                {
                    List<ItemNameFileLog> list = helper.ExecuteReader(command);
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
