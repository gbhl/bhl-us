using System;
using System.Data;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHL.DataObjects;

namespace MOBOT.BHL.DAL
{
	public partial class NoteTypeDAL
	{
        public CustomGenericList<NoteType> NoteTypeSelectAll(SqlConnection sqlConnection, SqlTransaction sqlTransaction)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("NoteTypeSelectAll", connection, transaction))
            {
                using (CustomSqlHelper<NoteType> helper = new CustomSqlHelper<NoteType>())
                {
                    CustomGenericList<NoteType> list = helper.ExecuteReader(command);
                    return list;
                }
            }
        }

        public void Save(SqlConnection sqlConnection, SqlTransaction sqlTransaction, NoteType noteType, int userID)
        {
            SqlConnection connection = sqlConnection;
            SqlTransaction transaction = sqlTransaction;

            if (connection == null)
            {
                connection =
                    CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"));
            }

            bool isTransactionCoordinator = CustomSqlHelper.IsTransactionCoordinator(transaction);

            try
            {
                transaction = CustomSqlHelper.BeginTransaction(connection, transaction, isTransactionCoordinator);

                new NoteTypeDAL().NoteTypeManageAuto(connection, transaction, noteType, userID);

                CustomSqlHelper.CommitTransaction(transaction, isTransactionCoordinator);
            }
            catch (Exception ex)
            {
                CustomSqlHelper.RollbackTransaction(transaction, isTransactionCoordinator);

                throw;
            }
            finally
            {
                CustomSqlHelper.CloseConnection(connection, isTransactionCoordinator);
            }
        }
    }
}
