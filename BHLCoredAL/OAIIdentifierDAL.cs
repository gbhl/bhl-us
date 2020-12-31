using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHL.DataObjects;

namespace MOBOT.BHL.DAL
{
    public class OAIIdentifierDAL
    {
        /// <summary>
        /// Select a list of item identifiers based on the specified OAI arguments.  The From and Until dates
        /// are evaluated against the LastModifiedDates of the items.
        /// </summary>
        /// <param name="sqlConnection"></param>
        /// <param name="sqlTransaction"></param>
        /// <param name="maxIdentifiers"></param>
        /// <param name="startId"></param>
        /// <param name="fromDate"></param>
        /// <param name="untilDate"></param>
        /// <returns></returns>
        public List<OAIIdentifier> OAIIdentifierSelectItems(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            int maxIdentifiers, int startId, DateTime? fromDate, DateTime? untilDate, Int16 includeLocalContent, Int16 includeExternalContent)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("OAIIdentifierSelectItems", connection, transaction,
                CustomSqlHelper.CreateInputParameter("MaxIdentifiers", SqlDbType.Int, null, false, maxIdentifiers),
                CustomSqlHelper.CreateInputParameter("StartID", SqlDbType.Int, null, false, startId),
                CustomSqlHelper.CreateInputParameter("FromDate", SqlDbType.DateTime, null, true, fromDate),
                CustomSqlHelper.CreateInputParameter("UntilDate", SqlDbType.DateTime, null, true, untilDate),
                CustomSqlHelper.CreateInputParameter("IncludeLocalContent", SqlDbType.SmallInt, null, false, includeLocalContent),
                CustomSqlHelper.CreateInputParameter("IncludeExternalContent", SqlDbType.SmallInt, null, false, includeExternalContent)))
            {
                using (CustomSqlHelper<OAIIdentifier> helper = new CustomSqlHelper<OAIIdentifier>())
                {
                    List<OAIIdentifier> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }

        /// <summary>
        /// Select a list of pdf identifiers based on the specified OAI arguments.  The From and Until dates
        /// are evaluated against the LastModifiedDates of the pdfs.
        /// </summary>
        /// <param name="sqlConnection"></param>
        /// <param name="sqlTransaction"></param>
        /// <param name="maxIdentifiers"></param>
        /// <param name="startId"></param>
        /// <param name="fromDate"></param>
        /// <param name="untilDate"></param>
        /// <returns></returns>
        public List<OAIIdentifier> OAIIdentifierSelectPDFs(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            int maxIdentifiers, int startId, DateTime? fromDate, DateTime? untilDate)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("OAIIdentifierSelectPDFs", connection, transaction,
                CustomSqlHelper.CreateInputParameter("MaxIdentifiers", SqlDbType.Int, null, false, maxIdentifiers),
                CustomSqlHelper.CreateInputParameter("StartID", SqlDbType.Int, null, false, startId),
                CustomSqlHelper.CreateInputParameter("FromDate", SqlDbType.DateTime, null, true, fromDate),
                CustomSqlHelper.CreateInputParameter("UntilDate", SqlDbType.DateTime, null, true, untilDate)))
            {
                using (CustomSqlHelper<OAIIdentifier> helper = new CustomSqlHelper<OAIIdentifier>())
                {
                    List<OAIIdentifier> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }

        /// <summary>
        /// Select a list of title identifiers based on the specified OAI arguments.  The From and Until dates
        /// are evaluated against the LastModifiedDates of the titles.
        /// </summary>
        /// <param name="sqlConnection"></param>
        /// <param name="sqlTransaction"></param>
        /// <param name="maxIdentifiers"></param>
        /// <param name="startId"></param>
        /// <param name="fromDate"></param>
        /// <param name="untilDate"></param>
        /// <returns></returns>
        public List<OAIIdentifier> OAIIdentifierSelectTitles(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            int maxIdentifiers, int startId, DateTime? fromDate, DateTime? untilDate)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("OAIIdentifierSelectTitles", connection, transaction,
                CustomSqlHelper.CreateInputParameter("MaxIdentifiers", SqlDbType.Int, null, false, maxIdentifiers),
                CustomSqlHelper.CreateInputParameter("StartID", SqlDbType.Int, null, false, startId),
                CustomSqlHelper.CreateInputParameter("FromDate", SqlDbType.DateTime, null, true, fromDate),
                CustomSqlHelper.CreateInputParameter("UntilDate", SqlDbType.DateTime, null, true, untilDate)))
            {
                using (CustomSqlHelper<OAIIdentifier> helper = new CustomSqlHelper<OAIIdentifier>())
                {
                    List<OAIIdentifier> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }

        /// <summary>
        /// Select a list of segment identifiers based on the specified OAI arguments.  The From and Until dates
        /// are evaluated against the LastModifiedDates of the segments.
        /// </summary>
        /// <param name="sqlConnection"></param>
        /// <param name="sqlTransaction"></param>
        /// <param name="maxIdentifiers"></param>
        /// <param name="startId"></param>
        /// <param name="fromDate"></param>
        /// <param name="untilDate"></param>
        /// <returns></returns>
        public List<OAIIdentifier> OAIIdentifierSelectSegments(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            int maxIdentifiers, int startId, DateTime? fromDate, DateTime? untilDate, Int16 includeLocalContent, Int16 includeExternalContent)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("OAIIdentifierSelectSegments", connection, transaction,
                CustomSqlHelper.CreateInputParameter("MaxIdentifiers", SqlDbType.Int, null, false, maxIdentifiers),
                CustomSqlHelper.CreateInputParameter("StartID", SqlDbType.Int, null, false, startId),
                CustomSqlHelper.CreateInputParameter("FromDate", SqlDbType.DateTime, null, true, fromDate),
                CustomSqlHelper.CreateInputParameter("UntilDate", SqlDbType.DateTime, null, true, untilDate),
                CustomSqlHelper.CreateInputParameter("IncludeLocalContent", SqlDbType.SmallInt, null, false, includeLocalContent),
                CustomSqlHelper.CreateInputParameter("IncludeExternalContent", SqlDbType.SmallInt, null, false, includeExternalContent)))
           {
                using (CustomSqlHelper<OAIIdentifier> helper = new CustomSqlHelper<OAIIdentifier>())
                {
                    List<OAIIdentifier> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }

        /// <summary>
        /// Select a list of identifiers based on the specified OAI arguments.  The From and Until dates
        /// are evaluated against the LastModifiedDates.
        /// </summary>
        /// <param name="sqlConnection"></param>
        /// <param name="sqlTransaction"></param>
        /// <param name="maxIdentifiers"></param>
        /// <param name="startId"></param>
        /// <param name="set"></param>
        /// <param name="fromDate"></param>
        /// <param name="untilDate"></param>
        /// <returns></returns>
        public List<OAIIdentifier> OAIIdentifierSelectAll(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            int maxIdentifiers, int startId, String set, DateTime? fromDate, DateTime? untilDate)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("OAIIdentifierSelectAll", connection, transaction,
                CustomSqlHelper.CreateInputParameter("MaxIdentifiers", SqlDbType.Int, null, false, maxIdentifiers),
                CustomSqlHelper.CreateInputParameter("StartID", SqlDbType.Int, null, false, startId),
                CustomSqlHelper.CreateInputParameter("SetSpec", SqlDbType.VarChar, 30, false, set),
                CustomSqlHelper.CreateInputParameter("FromDate", SqlDbType.DateTime, null, true, fromDate),
                CustomSqlHelper.CreateInputParameter("UntilDate", SqlDbType.DateTime, null, true, untilDate)))
            {
                using (CustomSqlHelper<OAIIdentifier> helper = new CustomSqlHelper<OAIIdentifier>())
                {
                    List<OAIIdentifier> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }
    }
}
