using CustomDataAccess;
using System;
using System.Data.SqlClient;
using System.Data;
using MOBOT.BHL.DataObjects;
using System.Collections.Generic;

namespace MOBOT.BHL.DAL
{
	public partial class ServiceLogDAL
	{
        public void ServiceLogInsert(
                SqlConnection sqlConnection,
                SqlTransaction sqlTransaction,
                DateTime logdate, 
                string servicename, 
                string serviceparam, 
                string severityname,
                int? errornumber, 
                string procedure, 
                int? line, 
                string message, 
                string stacktrace)
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("servlog.ServiceLogInsert", connection, transaction,
                    CustomSqlHelper.CreateInputParameter("LogDate", SqlDbType.DateTime, null, false, logdate),
                    CustomSqlHelper.CreateInputParameter("ServiceName", SqlDbType.NVarChar, 200, false, servicename),
                    CustomSqlHelper.CreateInputParameter("ServiceParam", SqlDbType.NVarChar, 30, false, serviceparam),
                    CustomSqlHelper.CreateInputParameter("SeverityName", SqlDbType.NVarChar, 30, false, severityname),
                    CustomSqlHelper.CreateInputParameter("ErrorNumber", SqlDbType.Int, null, true, errornumber),
                    CustomSqlHelper.CreateInputParameter("Procedure", SqlDbType.NVarChar, 500, false, procedure),
                    CustomSqlHelper.CreateInputParameter("Line", SqlDbType.Int, null, true, line),
                    CustomSqlHelper.CreateInputParameter("Message", SqlDbType.NVarChar, 1073741823, false, message),
                    CustomSqlHelper.CreateInputParameter("StackTrace", SqlDbType.NVarChar, 1073741823, false, stacktrace)
                    ))
            {
                CustomSqlHelper.ExecuteNonQuery(command);
            }
        }

        public List<ServiceLog> ServiceLogSelectDetailedList(
            SqlConnection sqlConnection, 
            SqlTransaction sqlTransaction,
            int? serviceID = null,
            int? severityID = null,
            DateTime? startDate = null,
            DateTime? endDate = null,
            int numRows = 100,
            int startRow = 1,
            string sortColumn = "CreationDate",
            string sortDirection = "DESC"
            )
        {
            SqlConnection connection = CustomSqlHelper.CreateConnection(
                CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"), sqlConnection);
            SqlTransaction transaction = sqlTransaction;

            using (SqlCommand command = CustomSqlHelper.CreateCommand("servlog.ServiceLogSelectDetailedList", connection, transaction,
                CustomSqlHelper.CreateInputParameter("ServiceID", SqlDbType.Int, null, true, serviceID),
                CustomSqlHelper.CreateInputParameter("SeverityID", SqlDbType.Int, null, true, severityID),
                CustomSqlHelper.CreateInputParameter("StartDate", SqlDbType.DateTime, null, true, startDate),
                CustomSqlHelper.CreateInputParameter("EndDate", SqlDbType.DateTime, null, true, endDate),
                CustomSqlHelper.CreateInputParameter("NumRows", SqlDbType.Int, null, false, numRows),
                CustomSqlHelper.CreateInputParameter("StartRow", SqlDbType.Int, null, false, startRow),
                CustomSqlHelper.CreateInputParameter("SortColumn", SqlDbType.NVarChar, 150, false, sortColumn),
                CustomSqlHelper.CreateInputParameter("SortDirection", SqlDbType.NVarChar, 4, false, sortDirection)))
            {
                using (CustomSqlHelper<ServiceLog> helper = new CustomSqlHelper<ServiceLog>())
                {
                    List<ServiceLog> list = helper.ExecuteReader(command);
                    return (list);
                }
            }
        }
    }
}

