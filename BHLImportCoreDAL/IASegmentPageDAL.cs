
#region Using

using CustomDataAccess;
using MOBOT.BHLImport.DataObjects;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

#endregion Using

namespace MOBOT.BHLImport.DAL
{
    public partial class IASegmentPageDAL
	{
        /// <summary>
        /// Select the page for the specified segment and sequence number.
        /// </summary>
        /// <param name="sqlConnection">Sql connection or null.</param>
        /// <param name="sqlTransaction">Sql transaction or null.</param>
        /// <param name="segmentID">Segment identifier for which to retreive data</param>
        /// <param name="sequence">Sequence number for which to retreive data</param>
        /// <returns>Object of type IASegmentPage.</returns>
        public IASegmentPage IASegmentPageSelectBySegmentAndSequence(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            int segmentID,
            int pageSequence)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHLImport"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("IASegmentPageSelectBySegmentAndSequence", connection, transaction,
                CustomSqlHelper.CreateInputParameter("SegmentID", SqlDbType.Int, null, false, segmentID),
                CustomSqlHelper.CreateInputParameter("PageSequence", SqlDbType.Int, null, false, pageSequence)))
            {
                using (CustomSqlHelper<IASegmentPage> helper = new CustomSqlHelper<IASegmentPage>())
                {
                    List<IASegmentPage> list = helper.ExecuteReader(command);

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

