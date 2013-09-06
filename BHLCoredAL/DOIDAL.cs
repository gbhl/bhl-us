using System;
using System.Data;
using System.Data.SqlClient;
using CustomDataAccess;
using MOBOT.BHL.DataObjects;

namespace MOBOT.BHL.DAL
{
	public partial class DOIDAL
	{
        public CustomGenericList<DOI> DOISelectSubmitted(SqlConnection sqlConnection,
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
                    CustomGenericList<DOI> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }

        public CustomGenericList<DOI> DOISelectValidForTitle(SqlConnection sqlConnection,
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
                    CustomGenericList<DOI> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }

        public CustomGenericList<DOI> DOISelectValidForSegment(SqlConnection sqlConnection,
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
                    CustomGenericList<DOI> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }

        public CustomGenericList<DOI> DOISelectByStatus(SqlConnection sqlConnection,
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
                    CustomGenericList<DOI> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }

        public CustomGenericList<DOIStatus> DOIStatusSelectAll(SqlConnection sqlConnection, SqlTransaction sqlTransaction)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;
            using (SqlCommand command = CustomSqlHelper.CreateCommand("DOIStatusSelectAll",
                connection, transaction))
            {
                using (CustomSqlHelper<DOIStatus> helper = new CustomSqlHelper<DOIStatus>())
                {
                    CustomGenericList<DOIStatus> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }
    }
}
