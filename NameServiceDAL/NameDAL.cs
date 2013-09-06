using System;
using System.Data;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHL.API.BHLApiDataObjects;

namespace MOBOT.BHL.API.BHLApiDAL
{
    public class NameApiDAL
    {
        public int NameCountUniqueConfirmed(SqlConnection sqlConnection, SqlTransaction sqlTransaction)
        {
            SqlConnection connection = sqlConnection;
            SqlTransaction transaction = sqlTransaction;

            if (connection == null)
            {
                connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"));
            }

            using (SqlCommand command = CustomSqlHelper.CreateCommand("NameCountUniqueConfirmed", connection, transaction))
            {
                using (CustomSqlHelper<int> helper = new CustomSqlHelper<int>())
                {
                    CustomGenericList<int> list = helper.ExecuteReader(command);

                    if (list.Count == 0)
                    {
                        return default(int);
                    }
                    else
                    {
                        return list[0];
                    }
                }
            }
        }

        public int NameCountUniqueConfirmedBetweenDates(SqlConnection sqlConnection, SqlTransaction sqlTransaction, 
            DateTime startDate, DateTime endDate)
        {
            SqlConnection connection = sqlConnection;
            SqlTransaction transaction = sqlTransaction;

            if (connection == null)
            {
                connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"));
            }

            using (SqlCommand command = CustomSqlHelper.CreateCommand("NameCountUniqueConfirmedBetweenDates", connection, transaction,
                CustomSqlHelper.CreateInputParameter("StartDate", SqlDbType.DateTime, null, false, startDate),
                CustomSqlHelper.CreateInputParameter("EndDate", SqlDbType.DateTime, null, false, endDate)))
            {
                using (CustomSqlHelper<int> helper = new CustomSqlHelper<int>())
                {
                    CustomGenericList<int> list = helper.ExecuteReader(command);

                    if (list.Count == 0)
                    {
                        return default(int);
                    }
                    else
                    {
                        return list[0];
                    }
                }
            }
        }

        public CustomGenericList<Name> NameListActive(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            int startRow, int batchSize)
        {
            SqlConnection connection = sqlConnection;
            SqlTransaction transaction = sqlTransaction;

            if (connection == null)
            {
                connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"));
            }

            using (SqlCommand command = CustomSqlHelper.CreateCommand("NameResolvedListActive", connection, transaction,
                CustomSqlHelper.CreateInputParameter("StartRow", SqlDbType.Int, null, false, startRow),
                CustomSqlHelper.CreateInputParameter("BatchSize", SqlDbType.Int, null, false, batchSize)))

            {
                using (CustomSqlHelper<Name> helper = new CustomSqlHelper<Name>())
                {
                    return helper.ExecuteReader(command);
                }
            }
        }

        public CustomGenericList<Name> NameListActiveBetweenDates(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            int startRow, int batchSize, DateTime startDate, DateTime endDate)
        {
            SqlConnection connection = sqlConnection;
            SqlTransaction transaction = sqlTransaction;

            if (connection == null)
            {
                connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"));
            }

            using (SqlCommand command = CustomSqlHelper.CreateCommand("NameResolvedListActiveBetweenDates", connection, transaction,
                CustomSqlHelper.CreateInputParameter("StartRow", SqlDbType.Int, null, false, startRow),
                CustomSqlHelper.CreateInputParameter("BatchSize", SqlDbType.Int, null, false, batchSize),
                CustomSqlHelper.CreateInputParameter("StartDate", SqlDbType.DateTime, null, false, startDate),
                CustomSqlHelper.CreateInputParameter("EndDate", SqlDbType.DateTime, null, false, endDate)))
            {
                using (CustomSqlHelper<Name> helper = new CustomSqlHelper<Name>())
                {
                    return helper.ExecuteReader(command);
                }
            }
        }

        public CustomGenericList<PageDetail> PageSelectByNameBankID(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            string nameBankID)
        {
            SqlConnection connection = sqlConnection;
            SqlTransaction transaction = sqlTransaction;

            if (connection == null)
            {
                connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"));
            }

            using (SqlCommand command = CustomSqlHelper.CreateCommand("PageSelectByNameBankID", connection, transaction,
                CustomSqlHelper.CreateInputParameter("NameBankID", SqlDbType.NVarChar, 100, false, nameBankID)))
            {
                using (CustomSqlHelper<PageDetail> helper = new CustomSqlHelper<PageDetail>())
                {
                    return helper.ExecuteReader(command);
                }
            }
        }

    }
}
