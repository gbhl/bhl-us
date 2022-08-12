using CustomDataAccess;
using MOBOT.BHL.DataObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace MOBOT.BHL.DAL
{
    public partial class DOIDAL
	{
        public List<DOI> DOISelectSubmitted(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int minutesSinceSubmit)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;
            using (SqlCommand command = CustomSqlHelper.CreateCommand("DOISelectSubmitted",
                connection, transaction,
                CustomSqlHelper.CreateInputParameter("MinutesSinceSubmit", SqlDbType.Int, null, false, minutesSinceSubmit)))
            {
                using (CustomSqlHelper<DOI> helper = new CustomSqlHelper<DOI>())
                {
                    List<DOI> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }

        public List<DOI> DOISelectQueued(SqlConnection sqlConnection, SqlTransaction sqlTransaction)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;
            using (SqlCommand command = CustomSqlHelper.CreateCommand("DOISelectQueued", connection, transaction))
            {
                using (CustomSqlHelper<DOI> helper = new CustomSqlHelper<DOI>())
                {
                    List<DOI> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }

        public List<Title_Identifier> DOISelectValidForTitle(SqlConnection sqlConnection,
        SqlTransaction sqlTransaction, int titleID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;
            using (SqlCommand command = CustomSqlHelper.CreateCommand("DOISelectValidForTitle",
                connection, transaction,
                CustomSqlHelper.CreateInputParameter("TitleID", SqlDbType.Int, null, false, titleID)))
            {
                using (CustomSqlHelper<Title_Identifier> helper = new CustomSqlHelper<Title_Identifier>())
                {
                    List<Title_Identifier> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }

        public List<ItemIdentifier> DOISelectValidForSegment(SqlConnection sqlConnection,
        SqlTransaction sqlTransaction, int segmentID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;
            using (SqlCommand command = CustomSqlHelper.CreateCommand("DOISelectValidForSegment",
                connection, transaction,
                CustomSqlHelper.CreateInputParameter("SegmentID", SqlDbType.Int, null, false, segmentID)))
            {
                using (CustomSqlHelper<ItemIdentifier> helper = new CustomSqlHelper<ItemIdentifier>())
                {
                    List<ItemIdentifier> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }

        public List<DOI> DOISelectByStatus(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, int doiStatusID, int numRows, int pageNum,
            string sortColumn, string sortOrder)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;
            using (SqlCommand command = CustomSqlHelper.CreateCommand("DOISelectByStatus",
                connection, transaction,
                CustomSqlHelper.CreateInputParameter("DOIStatusID", SqlDbType.Int, null, false, doiStatusID),
                CustomSqlHelper.CreateInputParameter("NumRows", SqlDbType.Int, null, false, numRows),
                CustomSqlHelper.CreateInputParameter("PageNum", SqlDbType.Int, null, false, pageNum),
                CustomSqlHelper.CreateInputParameter("SortColumn", SqlDbType.NVarChar, 150, false, sortColumn),
                CustomSqlHelper.CreateInputParameter("SortDirection", SqlDbType.NVarChar, 4, false, sortOrder)))
            {
                using (CustomSqlHelper<DOI> helper = new CustomSqlHelper<DOI>())
                {
                    List<DOI> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }

        public List<DOI> DOISelectStatusReport(SqlConnection sqlConnection, SqlTransaction sqlTransaction, 
            int userID, int doiStatusID, int doiEntityTypeID, int? entityID, DateTime startDate, DateTime endDate,
            int numRows, int pageNum, string sortColumn, string sortOrder)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;
            using (SqlCommand command = CustomSqlHelper.CreateCommand("DOISelectStatusReport",
                connection, transaction,
                CustomSqlHelper.CreateInputParameter("CreationUserID", SqlDbType.Int, null, false, userID),
                CustomSqlHelper.CreateInputParameter("DOIStatusID", SqlDbType.Int, null, false, doiStatusID),
                CustomSqlHelper.CreateInputParameter("DOIEntityTypeID", SqlDbType.Int, null, false, doiEntityTypeID),
                CustomSqlHelper.CreateInputParameter("EntityID", SqlDbType.Int, null, true, entityID),
                CustomSqlHelper.CreateInputParameter("StartDate", SqlDbType.DateTime, null, false, startDate),
                CustomSqlHelper.CreateInputParameter("EndDate", SqlDbType.DateTime, null, false, endDate),
                CustomSqlHelper.CreateInputParameter("NumRows", SqlDbType.Int, null, false, numRows),
                CustomSqlHelper.CreateInputParameter("PageNum", SqlDbType.Int, null, false, pageNum),
                CustomSqlHelper.CreateInputParameter("SortColumn", SqlDbType.NVarChar, 150, false, sortColumn),
                CustomSqlHelper.CreateInputParameter("SortDirection", SqlDbType.NVarChar, 4, false, sortOrder)))
            {
                using (CustomSqlHelper<DOI> helper = new CustomSqlHelper<DOI>())
                {
                    List<DOI> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }

        public DOI DOISelectQueuedByTypeAndID(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string doiEntityTypeName, int entityID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;
            using (SqlCommand command = CustomSqlHelper.CreateCommand("DOISelectQueuedByTypeAndID", connection, transaction,
                CustomSqlHelper.CreateInputParameter("DOIEntityTypeName", SqlDbType.NVarChar, 50, false, doiEntityTypeName),
                CustomSqlHelper.CreateInputParameter("EntityID", SqlDbType.Int, null, false, entityID)))
            {
                using (CustomSqlHelper<DOI> helper = new CustomSqlHelper<DOI>())
                {
                    List<DOI> list = helper.ExecuteReader(command);
                    if (list.Count > 0)
                        return list[0];
                    else
                        return null;
                }
            }
        }

        public void DOIDeleteQueuedByTypeAndID(SqlConnection sqlConnection, SqlTransaction sqlTransaction, int doiEntityTypeID, int entityID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;
            using (SqlCommand command = CustomSqlHelper.CreateCommand("DOIDeleteQueuedByTypeAndID", connection, transaction,
                CustomSqlHelper.CreateInputParameter("DOIEntityTypeID", SqlDbType.Int, null, false, doiEntityTypeID),
                CustomSqlHelper.CreateInputParameter("EntityID", SqlDbType.Int, null, false, entityID)))
            {
                CustomSqlHelper.ExecuteNonQuery(command);
            }
        }

        public List<DOIStatus> DOIStatusSelectAll(SqlConnection sqlConnection, SqlTransaction sqlTransaction)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;
            using (SqlCommand command = CustomSqlHelper.CreateCommand("DOIStatusSelectAll",
                connection, transaction))
            {
                using (CustomSqlHelper<DOIStatus> helper = new CustomSqlHelper<DOIStatus>())
                {
                    List<DOIStatus> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }

        public List<DOIEntityType> DOIEntityTypeSelectAll(SqlConnection sqlConnection, SqlTransaction sqlTransaction)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;
            using (SqlCommand command = CustomSqlHelper.CreateCommand("DOIEntityTypeSelectAll",
                connection, transaction))
            {
                using (CustomSqlHelper<DOIEntityType> helper = new CustomSqlHelper<DOIEntityType>())
                {
                    List<DOIEntityType> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }

        public List<DOIEntityType> DOIEntityTypeSelectWithDoi(SqlConnection sqlConnection, SqlTransaction sqlTransaction)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;
            using (SqlCommand command = CustomSqlHelper.CreateCommand("DOIEntityTypeSelectWithDoi",
                connection, transaction))
            {
                using (CustomSqlHelper<DOIEntityType> helper = new CustomSqlHelper<DOIEntityType>())
                {
                    List<DOIEntityType> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }

        public void DOIInsertQueue(
            SqlConnection sqlConnection, SqlTransaction sqlTransaction, int dOIEntityTypeID, int entityID, int creationUserID, int lastModifiedUserID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("dbo.DOIInsertQueue", connection, transaction,
                    CustomSqlHelper.CreateInputParameter("DOIEntityTypeID", SqlDbType.Int, null, false, dOIEntityTypeID),
                    CustomSqlHelper.CreateInputParameter("EntityID", SqlDbType.Int, null, false, entityID),
                    CustomSqlHelper.CreateInputParameter("CreationUserID", SqlDbType.Int, null, false, creationUserID),
                    CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, false, lastModifiedUserID)))
            {
                CustomSqlHelper.ExecuteNonQuery(command);
            }
        }

        public void DOIDelete(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            int doiEntityTypeID, int entityID, int userID = 1, int excludeBHLDOI = 1)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("dbo.DOIDelete", connection, transaction,
                    CustomSqlHelper.CreateInputParameter("DOIEntityTypeID", SqlDbType.Int, null, false, doiEntityTypeID),
                    CustomSqlHelper.CreateInputParameter("EntityID", SqlDbType.Int, null, false, entityID),
                    CustomSqlHelper.CreateInputParameter("UserID", SqlDbType.Int, null, false, userID),
                    CustomSqlHelper.CreateInputParameter("ExcludeBHLDOI", SqlDbType.Int, null, false, excludeBHLDOI)))
            {
                CustomSqlHelper.ExecuteNonQuery(command);
            }
        }

        public void DOIInsert(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            int doiEntityTypeID, int entityID, int doiStatusID, string doiName, int isValid,
            string doiBatchID, string statusMessage, int userID = 1, int excludeBHLDOI = 1)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("dbo.DOIInsert", connection, transaction,
                    CustomSqlHelper.CreateInputParameter("DOIEntityTypeID", SqlDbType.Int, null, false, doiEntityTypeID),
                    CustomSqlHelper.CreateInputParameter("EntityID", SqlDbType.Int, null, false, entityID),
                    CustomSqlHelper.CreateInputParameter("DOIStatusID", SqlDbType.Int, null, false, doiStatusID),
                    CustomSqlHelper.CreateInputParameter("DOIName", SqlDbType.NVarChar, 50, false, doiName),
                    CustomSqlHelper.CreateInputParameter("IsValid", SqlDbType.SmallInt, null, false, isValid),
                    CustomSqlHelper.CreateInputParameter("DOIBatchID", SqlDbType.NVarChar, 50, false, doiBatchID),
                    CustomSqlHelper.CreateInputParameter("StatusMessage", SqlDbType.NVarChar, 1000, false, statusMessage),
                    CustomSqlHelper.CreateInputParameter("UserID", SqlDbType.Int, null, false, userID),
                    CustomSqlHelper.CreateInputParameter("ExcludeBHLDOI", SqlDbType.Int, null, false, excludeBHLDOI)))
            {
                CustomSqlHelper.ExecuteNonQuery(command);
            }
        }

        public void DOIUpdate(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            int doiEntityTypeID, int entityID, int doiStatusID, string doiName, int isValid, string processName,
            string doiBatchID, string statusMessage, int userID = 1, int excludeBHLDOI = 1)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("dbo.DOIUpdate", connection, transaction,
                    CustomSqlHelper.CreateInputParameter("DOIEntityTypeID", SqlDbType.Int, null, false, doiEntityTypeID),
                    CustomSqlHelper.CreateInputParameter("EntityID", SqlDbType.Int, null, false, entityID),
                    CustomSqlHelper.CreateInputParameter("DOIStatusID", SqlDbType.Int, null, false, doiStatusID),
                    CustomSqlHelper.CreateInputParameter("DOIName", SqlDbType.NVarChar, 50, false, doiName),
                    CustomSqlHelper.CreateInputParameter("IsValid", SqlDbType.SmallInt, null, false, isValid),
                    CustomSqlHelper.CreateInputParameter("ProcessName", SqlDbType.NVarChar, 200, false, processName),
                    CustomSqlHelper.CreateInputParameter("DOIBatchID", SqlDbType.NVarChar, 50, false, doiBatchID),
                    CustomSqlHelper.CreateInputParameter("StatusMessage", SqlDbType.NVarChar, 1000, false, statusMessage),
                    CustomSqlHelper.CreateInputParameter("UserID", SqlDbType.Int, null, false, userID),
                    CustomSqlHelper.CreateInputParameter("ExcludeBHLDOI", SqlDbType.Int, null, false, excludeBHLDOI)))
            {
                CustomSqlHelper.ExecuteNonQuery(command);
            }
        }

        public void DOIInsertIdentifier(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            int doiEntityTypeID, int entityID, string doiName, int? userID = 1)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("dbo.DOIInsertIdentifier", connection, transaction,
                    CustomSqlHelper.CreateInputParameter("DOIEntityTypeID", SqlDbType.Int, null, false, doiEntityTypeID),
                    CustomSqlHelper.CreateInputParameter("EntityID", SqlDbType.Int, null, false, entityID),
                    CustomSqlHelper.CreateInputParameter("DOIName", SqlDbType.NVarChar, 50, false, doiName),
                    CustomSqlHelper.CreateInputParameter("UserID", SqlDbType.Int, null, false, userID)))
            {
                CustomSqlHelper.ExecuteNonQuery(command);
            }
        }
    }
}
