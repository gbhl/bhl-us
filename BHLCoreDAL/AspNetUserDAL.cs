﻿using CustomDataAccess;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace MOBOT.BHL.DAL
{
    public partial class AspNetUserDAL
    {
        public List<CustomDataRow> AspNetUserSelectAll(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("AspNetUserSelectAll", connection, transaction))
            {
                return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
            }
        }

        public List<CustomDataRow> AspNetUserSelectWithImportFiles(SqlConnection sqlConnection, SqlTransaction sqlTransaction)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("AspNetUserSelectWithImportFiles", connection, transaction))
            {
                return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
            }
        }

        public List<CustomDataRow> AspNetUserSelectWithDoi(SqlConnection sqlConnection, SqlTransaction sqlTransaction)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("AspNetUserSelectWithDoi", connection, transaction))
            {
                return CustomSqlHelper.ExecuteReaderAndReturnRows(command);
            }
        }
    }
}
