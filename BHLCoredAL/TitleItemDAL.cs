
#region Using

using System;
using System.Data;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHL.DataObjects;

#endregion Using

namespace MOBOT.BHL.DAL
{
	public partial class TitleItemDAL
	{
        /// <summary>
        /// Update the ItemSequence for the specified TitleItem.
        /// </summary>
        /// <param name="sqlConnection">Sql connection or null</param>
        /// <param name="sqlTransaction">Sql transaction or null</param>
        /// <param name="titleID">Identifier of a specific title</param>
        /// <param name="itemID">Identifier of a specific item</param>
        /// <param name="itemSequence">ItemSequence value</param>
        /// <returns>The updated titleitem</returns>
        public TitleItem TitleItemUpdateItemSequence(SqlConnection sqlConnection, SqlTransaction sqlTransaction, 
            int titleID, int itemID, short? itemSequence)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleItemUpdateItemSequence", connection, transaction,
                CustomSqlHelper.CreateInputParameter("TitleID", SqlDbType.Int, null, false, titleID),
                CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemID),
                CustomSqlHelper.CreateInputParameter("ItemSequence", SqlDbType.SmallInt, null, true, itemSequence)))
            {
                using (CustomSqlHelper<TitleItem> helper = new CustomSqlHelper<TitleItem>())
                {
                    CustomGenericList<TitleItem> list = helper.ExecuteReader(command);
                    if (list.Count > 0)
                        return list[0];
                    else
                        return null;
                }
            }
        }

        public CustomGenericList<TitleItem> TitleItemSelectByItem(
                SqlConnection sqlConnection,
                SqlTransaction sqlTransaction,
                int itemID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;
            using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleItemSelectByItem", connection, transaction,
                CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemID)))
            {
                using (CustomSqlHelper<TitleItem> helper = new CustomSqlHelper<TitleItem>())
                {
                    CustomGenericList<TitleItem> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }

        public CustomGenericList<TitleItem> TitleItemSelectByTitle(
                SqlConnection sqlConnection,
                SqlTransaction sqlTransaction,
                int titleID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;
            using (SqlCommand command = CustomSqlHelper.CreateCommand("TitleItemSelectByTitle", connection, transaction,
                CustomSqlHelper.CreateInputParameter("TitleID", SqlDbType.Int, null, false, titleID)))
            {
                using (CustomSqlHelper<TitleItem> helper = new CustomSqlHelper<TitleItem>())
                {
                    CustomGenericList<TitleItem> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }
    }
}
