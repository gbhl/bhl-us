using CustomDataAccess;
using MOBOT.BHL.DataObjects;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace MOBOT.BHL.DAL
{
    public partial class SegmentGenreDAL
	{
        public List<SegmentGenre> SegmentGenreSelectAll(SqlConnection sqlConnection, SqlTransaction sqlTransaction)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("SegmentGenreSelectAll", connection, transaction))
            {
                using (CustomSqlHelper<SegmentGenre> helper = new CustomSqlHelper<SegmentGenre>())
                {
                    List<SegmentGenre> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }

        public void Save(SqlConnection sqlConnection, SqlTransaction sqlTransaction, SegmentGenre segmentGenre, int userId)
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

                new SegmentGenreDAL().SegmentGenreManageAuto(connection, transaction, segmentGenre, userId);

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
