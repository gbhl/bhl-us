using CustomDataAccess;
using MOBOT.BHL.DataObjects;
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

        public List<DOI> DOISelectValidForTitle(SqlConnection sqlConnection,
        SqlTransaction sqlTransaction, int titleID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;
            using (SqlCommand command = CustomSqlHelper.CreateCommand("DOISelectValidForTitle",
                connection, transaction,
                CustomSqlHelper.CreateInputParameter("TitleID", SqlDbType.Int, null, false, titleID)))
            {
                using (CustomSqlHelper<DOI> helper = new CustomSqlHelper<DOI>())
                {
                    List<DOI> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }

        public List<DOI> DOISelectValidForSegment(SqlConnection sqlConnection,
        SqlTransaction sqlTransaction, int segmentID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;
            using (SqlCommand command = CustomSqlHelper.CreateCommand("DOISelectValidForSegment",
                connection, transaction,
                CustomSqlHelper.CreateInputParameter("SegmentID", SqlDbType.Int, null, false, segmentID)))
            {
                using (CustomSqlHelper<DOI> helper = new CustomSqlHelper<DOI>())
                {
                    List<DOI> list = helper.ExecuteReader(command);
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

        public DOI DOISelectByTypeAndID(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string doiEntityTypeName, int entityID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;
            using (SqlCommand command = CustomSqlHelper.CreateCommand("DOISelectByTypeAndID", connection, transaction,
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

        public void DOIDeleteByTypeAndID(SqlConnection sqlConnection, SqlTransaction sqlTransaction, int doiEntityTypeID, int entityID)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;
            using (SqlCommand command = CustomSqlHelper.CreateCommand("DOIDeleteByTypeAndID", connection, transaction,
                CustomSqlHelper.CreateInputParameter("DOIEntityTypeID", SqlDbType.Int, null, false, doiEntityTypeID),
                CustomSqlHelper.CreateInputParameter("EntityID", SqlDbType.Int, null, false, entityID)))
            {
                command.ExecuteNonQuery();
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

        public void DOIInsert(
            SqlConnection sqlConnection, SqlTransaction sqlTransaction, int dOIEntityTypeID, int entityID, int dOIStatusID, 
            string dOIBatchID, string dOIName, string statusMessage, short isValid, int creationUserID, int lastModifiedUserID,
            short allowDuplicate)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("dbo.DOIInsert", connection, transaction,
                    CustomSqlHelper.CreateInputParameter("DOIEntityTypeID", SqlDbType.Int, null, false, dOIEntityTypeID),
                    CustomSqlHelper.CreateInputParameter("EntityID", SqlDbType.Int, null, false, entityID),
                    CustomSqlHelper.CreateInputParameter("DOIStatusID", SqlDbType.Int, null, false, dOIStatusID),
                    CustomSqlHelper.CreateInputParameter("DOIBatchID", SqlDbType.NVarChar, 50, false, dOIBatchID),
                    CustomSqlHelper.CreateInputParameter("DOIName", SqlDbType.NVarChar, 50, false, dOIName),
                    CustomSqlHelper.CreateInputParameter("StatusMessage", SqlDbType.NVarChar, 1000, false, statusMessage),
                    CustomSqlHelper.CreateInputParameter("IsValid", SqlDbType.SmallInt, null, false, isValid),
                    CustomSqlHelper.CreateInputParameter("CreationUserID", SqlDbType.Int, null, false, creationUserID),
                    CustomSqlHelper.CreateInputParameter("LastModifiedUserID", SqlDbType.Int, null, false, lastModifiedUserID),
                    CustomSqlHelper.CreateInputParameter("AllowDuplicate", SqlDbType.SmallInt, null, false, allowDuplicate)))
            {
                command.ExecuteNonQuery();
            }
        }
    }
}
