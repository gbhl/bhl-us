
#region Using

using System;
using System.Data;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHLImport.DataObjects;

#endregion Using

namespace MOBOT.BHLImport.DAL
{
	public partial class IAPageDAL
	{
        /// <summary>
        /// Delete the page entries for the specified item.
        /// </summary>
        /// <param name="sqlConnection">Sql connection or null.</param>
        /// <param name="sqlTransaction">Sql transaction or null.</param>
        /// <param name="marcID">Identifier of the item</param>
        public void IAPageDeleteByItem(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            int itemID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHLImport"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("IAPageDeleteByItem", connection, transaction,
                CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemID)))
            {
                CustomSqlHelper.ExecuteNonQuery(command);
                if (transaction == null) CustomSqlHelper.CloseConnection(connection);
            }
        }
    }
}
