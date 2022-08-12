#region Using

using CustomDataAccess;
using MOBOT.BHLImport.DataObjects;
using System.Collections.Generic;
using System.Data.SqlClient;

#endregion Using

namespace MOBOT.BHLImport.DAL
{
    public partial class BSItemStatusDAL
	{
        /// <summary>
        /// Select all BSItemStatus records.
        /// </summary>
        /// <param name="sqlConnection">Sql connection or null.</param>
        /// <param name="sqlTransaction">Sql transaction or null.</param>
        /// <returns>List of BSItemStatus.</returns>
        public List<BSItemStatus> BSItemStatusSelectAll(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHLImport"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("BSItemStatusSelectAll", connection, transaction))
            {
                using (CustomSqlHelper<BSItemStatus> helper = new CustomSqlHelper<BSItemStatus>())
                {
                    return helper.ExecuteReader(command);
                }
            }
        }
    }
}
