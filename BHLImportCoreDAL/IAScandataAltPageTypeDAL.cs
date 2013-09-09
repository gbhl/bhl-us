
#region Using

using System;
using System.Data;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHLImport.DataObjects;

#endregion Using

namespace MOBOT.BHLImport.DAL
{
	public partial class IAScandataAltPageTypeDAL
	{
        /// <summary>
        /// Delete the ScandataAltPageType entries for the specified Scandata ID.
        /// </summary>
        /// <param name="sqlConnection">Sql connection or null.</param>
        /// <param name="sqlTransaction">Sql transaction or null.</param>
        /// <param name="scandataID">Identifier of the scandata record</param>
        public void IAScandataAltPageTypeDeleteByScandataID(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            int scandataID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHLImport"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("IAScandataAltPageTypeDeleteByScandataID", connection, transaction,
                CustomSqlHelper.CreateInputParameter("ScandataID", SqlDbType.Int, null, false, scandataID)))
            {
                CustomSqlHelper.ExecuteNonQuery(command);
                if (transaction == null) CustomSqlHelper.CloseConnection(connection);
            }
        }

        public IAScandataAltPageType IAScandataAltPageTypeSelectByScandataAndPageType(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            int scandataID,
            string pageType)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHLImport"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("IAScandataAltPageTypeSelectByScandataAndPageType", connection, transaction,
                CustomSqlHelper.CreateInputParameter("ScandataID", SqlDbType.Int, null, false, scandataID),
                CustomSqlHelper.CreateInputParameter("PageType", SqlDbType.NVarChar, 50, false, pageType)))
            {
                using (CustomSqlHelper<IAScandataAltPageType> helper = new CustomSqlHelper<IAScandataAltPageType>())
                {
                    CustomGenericList<IAScandataAltPageType> list = helper.ExecuteReader(command);

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
    }
}
