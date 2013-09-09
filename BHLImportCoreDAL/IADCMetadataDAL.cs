
#region Using

using System;
using System.Data;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHLImport.DataObjects;

#endregion Using

namespace MOBOT.BHLImport.DAL
{
	public partial class IADCMetadataDAL
	{
        /// <summary>
        /// Select the DCMetadata record for the specified item, element, and source.
        /// </summary>
        /// <param name="sqlConnection">Sql connection or null.</param>
        /// <param name="sqlTransaction">Sql transaction or null.</param>
        /// <param name="itemID">Identifier of the item</param>
        /// <param name="elementName">DCMetadata element to return</param>
        /// <param name="source">Source of the element</param>
        /// <returns>Object of type DCMetadata.</returns>
        public IADCMetadata IADCMetadataSelectByItemElementNameAndSource(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            int itemID,
            string elementName,
            string source)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHLImport"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("IADCMetadataSelectByItemAndElementName", connection, transaction,
                CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemID),
                CustomSqlHelper.CreateInputParameter("DCElementName", SqlDbType.NVarChar, 15, false, elementName),
                CustomSqlHelper.CreateInputParameter("Source", SqlDbType.NVarChar, 10, false, source)))
            {
                using (CustomSqlHelper<IADCMetadata> helper = new CustomSqlHelper<IADCMetadata>())
                {
                    CustomGenericList<IADCMetadata> list = helper.ExecuteReader(command);

                    if (list.Count > 0)
                    {
                        return list[0];
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Delete the DCMetadata for the specified item and source.
        /// </summary>
        /// <param name="sqlConnection">Sql connection or null.</param>
        /// <param name="sqlTransaction">Sql transaction or null.</param>
        /// <param name="itemID">Identifier of the item</param>
        /// <param name="source">Source of the elements to delete</param>
        /// <returns>Object of type Item.</returns>
        public void IADCMetadataDeleteForItemAndSource(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            int itemID,
            string source)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHLImport"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("IADCMetadataDeleteForItemAndSource", connection, transaction,
                CustomSqlHelper.CreateInputParameter("ItemID", SqlDbType.Int, null, false, itemID),
                CustomSqlHelper.CreateInputParameter("Source", SqlDbType.NVarChar, 10, false, source)))
            {
                CustomSqlHelper.ExecuteNonQuery(command);
                if (transaction == null) CustomSqlHelper.CloseConnection(connection);
            }
        }
    }
}
