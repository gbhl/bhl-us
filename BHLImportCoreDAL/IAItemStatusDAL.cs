#region Using

using CustomDataAccess;
using MOBOT.BHLImport.DataObjects;
using System.Collections.Generic;
using System.Data.SqlClient;

#endregion Using

namespace MOBOT.BHLImport.DAL
{
    public partial class IAItemStatusDAL
    {
        /// <summary>
        /// Select all IAItemStatus records.
        /// </summary>
        /// <param name="sqlConnection">Sql connection or null.</param>
        /// <param name="sqlTransaction">Sql transaction or null.</param>
        /// <returns>List of IAItemStatus.</returns>
        public List<IAItemStatus> IAItemStatusSelectAll(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHLImport"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("IAItemStatusSelectAll", connection, transaction))
            {
                using (CustomSqlHelper<IAItemStatus> helper = new CustomSqlHelper<IAItemStatus>())
                {
                    return helper.ExecuteReader(command);
                }
            }
        }
    }
}
