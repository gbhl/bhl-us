
#region Using

using System;
using System.Data;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHLImport.DataObjects;

#endregion Using

namespace MOBOT.BHLImport.DAL
{
	public partial class IAMarcDataFieldDAL
	{
        /// <summary>
        /// Delete the MarcDataField and MarcSubField entries for the specified Marc ID.
        /// </summary>
        /// <param name="sqlConnection">Sql connection or null.</param>
        /// <param name="sqlTransaction">Sql transaction or null.</param>
        /// <param name="marcID">Identifier of the Marc header</param>
        public void IAMarcDataFieldDeleteByMarcID(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            int marcID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHLImport"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("IAMarcDataFieldDeleteByMarcID", connection, transaction,
                CustomSqlHelper.CreateInputParameter("MarcID", SqlDbType.Int, null, false, marcID)))
            {
                CustomSqlHelper.ExecuteNonQuery(command);
                if (transaction == null) CustomSqlHelper.CloseConnection(connection);
            }
        }
    }
}
