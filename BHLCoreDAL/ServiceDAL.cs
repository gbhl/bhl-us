
using CustomDataAccess;
using MOBOT.BHL.DataObjects;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace MOBOT.BHL.DAL
{
	public partial class ServiceDAL
	{
        public List<Service> ServiceSelectAll(SqlConnection sqlConnection, SqlTransaction sqlTransaction)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("servlog.ServiceSelectAll", connection, transaction))
            {
                using (CustomSqlHelper<Service> helper = new CustomSqlHelper<Service>())
                {
                    List<Service> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }
    }
}

