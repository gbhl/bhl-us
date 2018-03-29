using System;
using System.Data.SqlClient;

namespace BHL.SearchIndexQueueLoad
{
    /// <summary>
    /// This class and its methods should be moved to the BHL DAL assemblies if/when they
    /// are verified/modified to work with .NET Core.
    /// </summary>
    public class DataAccess
    {
        private string _connectionString = null;

        public DataAccess(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DbChangeSet SelectChangeList(string startDate = "", string endDate = "")
        {
            DbChangeSet changeSet = new DbChangeSet();

            SqlConnection sqlConnection = new SqlConnection(_connectionString);
            sqlConnection.Open();

            try
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.CommandText = "audit.AuditBasicSelectForSearchIndexQueue";

                    // Add StartDate and EndDate parameters, if specified
                    if (!string.IsNullOrWhiteSpace(startDate))
                    {
                        sqlCommand.Parameters.AddWithValue("@StartDate", startDate);
                    }
                    if (!string.IsNullOrWhiteSpace(endDate))
                    {
                        sqlCommand.Parameters.AddWithValue("@EndDate", endDate);
                    }

                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DbChange change = new DbChange();
                            change.AuditId = reader.GetInt32(reader.GetOrdinal("AuditID"));
                            change.AuditDate = reader.GetDateTime(reader.GetOrdinal("AuditDate"));
                            change.Operation = reader.GetString(reader.GetOrdinal("Operation"));
                            change.IndexEntity = reader.GetString(reader.GetOrdinal("IndexEntity"));
                            change.Id = reader.GetString(reader.GetOrdinal("EntityID"));
                            changeSet.Changes.Add(change);

                            if (changeSet.Changes.Count == 1)
                            {
                                changeSet.LastAuditBasicID = reader.GetInt32(reader.GetOrdinal("LastAuditID"));
                                changeSet.LastAuditDate = reader.GetDateTime(reader.GetOrdinal("LastAuditDate"));
                            }
                        }
                    }
                }
            }
            finally
            {
                if (sqlConnection.State != System.Data.ConnectionState.Closed) sqlConnection.Close();
                sqlConnection.Dispose();
            }

            return changeSet;
        }

        public void InsertSearchIndexQueueLog(int Id, DateTime auditDate, int numberQueued)
        {
            SqlConnection sqlConnection = new SqlConnection(_connectionString);
            sqlConnection.Open();

            try
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.CommandText = "dbo.SearchIndexQueueLogInsert";
                    sqlCommand.Parameters.AddWithValue("@LastAuditBasicID", Id);
                    sqlCommand.Parameters.AddWithValue("@LastAuditDate", auditDate);
                    sqlCommand.Parameters.AddWithValue("@NumberQueued", numberQueued);
                    sqlCommand.ExecuteNonQuery();
                }
            }
            finally
            {
                if (sqlConnection.State != System.Data.ConnectionState.Closed) sqlConnection.Close();
                sqlConnection.Dispose();
            }
        }
    }
}
