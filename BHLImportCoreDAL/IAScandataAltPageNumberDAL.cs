
#region Using

using CustomDataAccess;
using MOBOT.BHLImport.DataObjects;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

#endregion Using

namespace MOBOT.BHLImport.DAL
{
    public partial class IAScandataAltPageNumberDAL
	{
        /// <summary>
        /// Delete the ScandataAltPageNumber entries for the specified Scandata ID.
        /// </summary>
        /// <param name="sqlConnection">Sql connection or null.</param>
        /// <param name="sqlTransaction">Sql transaction or null.</param>
        /// <param name="scandataID">Identifier of the scandata record</param>
        public void IAScandataAltPageNumberDeleteByScandataID(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            int scandataID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHLImport"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("IAScandataAltPageNumberDeleteByScandataID", connection, transaction,
                CustomSqlHelper.CreateInputParameter("ScandataID", SqlDbType.Int, null, false, scandataID)))
            {
                CustomSqlHelper.ExecuteNonQuery(command);
                if (transaction == null) CustomSqlHelper.CloseConnection(connection);
            }
        }

        public IAScandataAltPageNumber IAScandataAltPageNumberSelectByScandataAndSequence(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            int scandataID,
            int sequence)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHLImport"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("IAScandataAltPageNumberSelectByScandataAndSequence", connection, transaction,
                CustomSqlHelper.CreateInputParameter("ScandataID", SqlDbType.Int, null, false, scandataID),
                CustomSqlHelper.CreateInputParameter("Sequence", SqlDbType.Int, null, false, sequence)))
            {
                using (CustomSqlHelper<IAScandataAltPageNumber> helper = new CustomSqlHelper<IAScandataAltPageNumber>())
                {
                    List<IAScandataAltPageNumber> list = helper.ExecuteReader(command);

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
