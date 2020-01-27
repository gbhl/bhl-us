
#region Using

using CustomDataAccess;
using MOBOT.BHLImport.DataObjects;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

#endregion Using

namespace MOBOT.BHLImport.DAL
{
    public partial class IASegmentAuthorDAL
	{
        /// <summary>
        /// Select the author for the specified segment and sequence number.
        /// </summary>
        /// <param name="sqlConnection">Sql connection or null.</param>
        /// <param name="sqlTransaction">Sql transaction or null.</param>
        /// <param name="segmentID">Segment identifier for which to retreive data</param>
        /// <param name="sequence">Sequence number for which to retreive data</param>
        /// <returns>Object of type IASegmentAuthor.</returns>
        public IASegmentAuthor IASegmentAuthorSelectBySegmentAndSequence(
            SqlConnection sqlConnection,
            SqlTransaction sqlTransaction,
            int segmentID,
            int sequence)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHLImport"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("IASegmentAuthorSelectBySegmentAndSequence", connection, transaction,
                CustomSqlHelper.CreateInputParameter("SegmentID", SqlDbType.Int, null, false, segmentID),
                CustomSqlHelper.CreateInputParameter("Sequence", SqlDbType.Int, null, false, sequence)))
            {
                using (CustomSqlHelper<IASegmentAuthor> helper = new CustomSqlHelper<IASegmentAuthor>())
                {
                    List<IASegmentAuthor> list = helper.ExecuteReader(command);

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

