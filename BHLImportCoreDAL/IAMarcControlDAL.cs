
#region Using

using System;
using System.Data;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHLImport.DataObjects;

#endregion Using

namespace MOBOT.BHLImport.DAL
{
	public partial class IAMarcControlDAL
	{
        /// <summary>
        /// Delete the MarcControl entries for the specified Marc ID.
        /// </summary>
        /// <param name="sqlConnection">Sql connection or null.</param>
        /// <param name="sqlTransaction">Sql transaction or null.</param>
        /// <param name="marcID">Identifier of the Marc header</param>
        public void IAMarcControlDeleteByMarcID(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            int marcID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHLImport"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("IAMarcControlDeleteByMarcID", connection, transaction,
                CustomSqlHelper.CreateInputParameter("MarcID", SqlDbType.Int, null, false, marcID)))
            {
                CustomSqlHelper.ExecuteNonQuery(command);
                if (transaction == null) CustomSqlHelper.CloseConnection(connection);
            }
        }
	}
}
