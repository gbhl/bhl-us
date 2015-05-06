using System;
using System.Data;
using System.Data.SqlClient;
using MOBOT.BHL.RequestLog.DataObjects;
using CustomDataAccess;

namespace MOBOT.BHL.RequestLog.DAL
{
	public partial class RequestLogDAL
	{
        public static int SelectDateRangeTotal(SqlConnection sqlConnection, SqlTransaction sqlTransaction, DateTime startDate,
            DateTime endDate, int applicationId)
        {
            return SelectDateRangeTotal(sqlConnection, sqlTransaction, startDate, endDate, applicationId, true);
        }

		public static int SelectDateRangeTotal( SqlConnection sqlConnection, SqlTransaction sqlTransaction, DateTime startDate,
			DateTime endDate, int applicationId, bool includeWebServices )
		{
			SqlConnection connection = sqlConnection;
			SqlTransaction transaction = sqlTransaction;

			if ( connection == null )
				connection = CustomSqlHelper.CreateConnection( CustomSqlHelper.GetConnectionStringFromConnectionStrings( "BHL" ) );

			using ( SqlCommand command = CustomSqlHelper.CreateCommand( "reqlog.RequestLogSelectDateTotal", connection, transaction,
				CustomSqlHelper.CreateInputParameter( "StartDate", SqlDbType.DateTime, null, false, startDate ),
				CustomSqlHelper.CreateInputParameter( "EndDate", SqlDbType.DateTime, null, false, endDate ),
                CustomSqlHelper.CreateInputParameter("ApplicationID", SqlDbType.Int, null, false, applicationId),
                CustomSqlHelper.CreateInputParameter("IncludeWebServices", SqlDbType.Bit, null, false, includeWebServices)
                ))
			{
				using ( CustomSqlHelper<int> helper = new CustomSqlHelper<int>() )
				{
					CustomGenericList<int> k = helper.ExecuteReader( command );

					return k[ 0 ];
				}
			}
		}

        public static CustomGenericList<GenericStat> SelectTypeByDateRange(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, DateTime startDate, DateTime endDate, int applicationId)
        {
            return SelectTypeByDateRange(sqlConnection, sqlTransaction, startDate, endDate, applicationId, true);
        }

        public static CustomGenericList<GenericStat> SelectTypeByDateRange(SqlConnection sqlConnection,
			SqlTransaction sqlTransaction, DateTime startDate, DateTime endDate, int applicationId, bool includeWebServices )
		{
			SqlConnection connection = sqlConnection;
			SqlTransaction transaction = sqlTransaction;

			if ( connection == null )
				connection = CustomSqlHelper.CreateConnection( CustomSqlHelper.GetConnectionStringFromConnectionStrings( "BHL" ) );

			using ( SqlCommand command = CustomSqlHelper.CreateCommand( "reqlog.RequestLogSelectTypesByDate", connection, transaction,
				CustomSqlHelper.CreateInputParameter( "StartDate", SqlDbType.DateTime, null, false, startDate ),
				CustomSqlHelper.CreateInputParameter( "EndDate", SqlDbType.DateTime, null, false, endDate ),
                CustomSqlHelper.CreateInputParameter("ApplicationID", SqlDbType.Int, null, false, applicationId),
                CustomSqlHelper.CreateInputParameter("IncludeWebServices", SqlDbType.Bit, null, false, includeWebServices)
                ))
			{
				using ( CustomSqlHelper<GenericStat> helper = new CustomSqlHelper<GenericStat>() )
				{
					CustomGenericList<GenericStat> list = helper.ExecuteReader( command );

					return list;
				}
			}
		}

        public static CustomGenericList<GenericStat> SelectHourRangeTotal(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, DateTime date, int applicationId)
        {
            return SelectHourRangeTotal(sqlConnection, sqlTransaction, date, applicationId, true);
        }

		public static CustomGenericList<GenericStat> SelectHourRangeTotal( SqlConnection sqlConnection,
			SqlTransaction sqlTransaction, DateTime date, int applicationId, bool includeWebServices )
		{
			SqlConnection connection = sqlConnection;
			SqlTransaction transaction = sqlTransaction;

			if ( connection == null )
				connection = CustomSqlHelper.CreateConnection( CustomSqlHelper.GetConnectionStringFromConnectionStrings( "BHL" ) );

			using ( SqlCommand command = CustomSqlHelper.CreateCommand( "reqlog.RequestLogSelectHourTotal", connection, transaction,
				CustomSqlHelper.CreateInputParameter( "StartDate", SqlDbType.DateTime, null, false, date.AddDays(-1) ),
				CustomSqlHelper.CreateInputParameter( "EndDate", SqlDbType.DateTime, null, false, date ),
                CustomSqlHelper.CreateInputParameter("ApplicationID", SqlDbType.Int, null, false, applicationId),
                CustomSqlHelper.CreateInputParameter("IncludeWebServices", SqlDbType.Bit, null, false, includeWebServices)
                ))
			{
				using ( CustomSqlHelper<GenericStat> helper = new CustomSqlHelper<GenericStat>() )
				{
					CustomGenericList<GenericStat> list = helper.ExecuteReader( command );

					return list;
				}
			}
		}

        public static CustomGenericList<GenericStat> SelectIPTotal(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            DateTime startDate, DateTime endDate, int applicationId)
        {
            return SelectIPTotal(sqlConnection, sqlTransaction, startDate, endDate, applicationId, true);
        }

		public static CustomGenericList<GenericStat> SelectIPTotal( SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			DateTime startDate, DateTime endDate, int applicationId, bool includeWebServices )
		{
			SqlConnection connection = sqlConnection;
			SqlTransaction transaction = sqlTransaction;

			if ( connection == null )
				connection = CustomSqlHelper.CreateConnection( CustomSqlHelper.GetConnectionStringFromConnectionStrings( "BHL" ) );

			using ( SqlCommand command = CustomSqlHelper.CreateCommand( "reqlog.RequestLogSelectTopIP", connection, transaction,
				CustomSqlHelper.CreateInputParameter( "StartDate", SqlDbType.DateTime, null, false, startDate),
				CustomSqlHelper.CreateInputParameter( "EndDate", SqlDbType.DateTime, null, false, endDate ),
                CustomSqlHelper.CreateInputParameter("ApplicationID", SqlDbType.Int, null, false, applicationId),
                CustomSqlHelper.CreateInputParameter("IncludeWebServices", SqlDbType.Bit, null, false, includeWebServices)
                ))
			{
				using ( CustomSqlHelper<GenericStat> helper = new CustomSqlHelper<GenericStat>() )
				{
					CustomGenericList<GenericStat> list = helper.ExecuteReader( command );

					return list;
				}
			}
		}

        public static CustomGenericList<GenericStat> SelectUserTotal(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            DateTime startDate, DateTime endDate, int applicationId)
        {
            return SelectUserTotal(sqlConnection, sqlTransaction, startDate, endDate, applicationId, true);
        }

		public static CustomGenericList<GenericStat> SelectUserTotal(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
			DateTime startDate, DateTime endDate, int applicationId, bool includeWebServices)
		{
			SqlConnection connection = sqlConnection;
			SqlTransaction transaction = sqlTransaction;

			if (connection == null)
				connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"));

			using (SqlCommand command = CustomSqlHelper.CreateCommand("reqlog.RequestLogSelectTopUser", connection, transaction,
				CustomSqlHelper.CreateInputParameter("StartDate", SqlDbType.DateTime, null, false, startDate),
				CustomSqlHelper.CreateInputParameter("EndDate", SqlDbType.DateTime, null, false, endDate),
                CustomSqlHelper.CreateInputParameter("ApplicationID", SqlDbType.Int, null, false, applicationId),
                CustomSqlHelper.CreateInputParameter("IncludeWebServices", SqlDbType.Bit, null, false, includeWebServices)
                ))
			{
				using (CustomSqlHelper<GenericStat> helper = new CustomSqlHelper<GenericStat>())
				{
					CustomGenericList<GenericStat> list = helper.ExecuteReader(command);

					return list;
				}
			}
		}

        public static CustomGenericList<MOBOT.BHL.RequestLog.DataObjects.RequestLog> SelectStatDetails(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
            DateTime startDate, DateTime endDate, int? requestTypeId, string ipAddress, 
            int? userId, int orderBy, int applicationId)
        {
            SqlConnection connection = sqlConnection;
            SqlTransaction transaction = sqlTransaction;

            if (connection == null)
                connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"));

            using (SqlCommand command = CustomSqlHelper.CreateCommand("reqlog.RequestLogSelectDetails", connection, transaction,
                CustomSqlHelper.CreateInputParameter("StartDate", SqlDbType.DateTime, null, false, startDate),
                CustomSqlHelper.CreateInputParameter("EndDate", SqlDbType.DateTime, null, false, endDate),
                CustomSqlHelper.CreateInputParameter("RequestTypeID", SqlDbType.Int, null, true, requestTypeId),
                CustomSqlHelper.CreateInputParameter("IPAddress", SqlDbType.VarChar, null, true, ipAddress),
                CustomSqlHelper.CreateInputParameter("UserID", SqlDbType.Int, null, true, userId),
                CustomSqlHelper.CreateInputParameter("OrderBy", SqlDbType.Int, null, false, orderBy),
                CustomSqlHelper.CreateInputParameter("ApplicationID", SqlDbType.Int, null, false, applicationId)
                ))
            {
                using (CustomSqlHelper<MOBOT.BHL.RequestLog.DataObjects.RequestLog> helper = new CustomSqlHelper<MOBOT.BHL.RequestLog.DataObjects.RequestLog>())
                {
                    CustomGenericList<MOBOT.BHL.RequestLog.DataObjects.RequestLog> list = helper.ExecuteReader(command);

                    return list;
                }
            }
        }

        public static void SaveRequestLog(SqlConnection sqlConnection, SqlTransaction sqlTransaction, MOBOT.BHL.RequestLog.DataObjects.RequestLog requestLog)
		{
			SqlConnection connection = sqlConnection;
			SqlTransaction transaction = sqlTransaction;

			if ( connection == null )
				connection = CustomSqlHelper.CreateConnection( CustomSqlHelper.GetConnectionStringFromConnectionStrings( "BHL" ) );

			bool isTransactionCoordinator = CustomSqlHelper.IsTransactionCoordinator( transaction );

			try
			{
				transaction = CustomSqlHelper.BeginTransaction( connection, transaction, isTransactionCoordinator );

                CustomDataAccessStatus<MOBOT.BHL.RequestLog.DataObjects.RequestLog> customRequestLog =
					new RequestLogDAL().RequestLogManageAuto( sqlConnection, sqlTransaction, requestLog );

				CustomSqlHelper.CommitTransaction( transaction, isTransactionCoordinator );
			}
			catch ( Exception ex )
			{
				CustomSqlHelper.RollbackTransaction( transaction, isTransactionCoordinator );
				throw ( ex );
			}
			finally
			{
				CustomSqlHelper.CloseConnection( connection, isTransactionCoordinator );
			}
		}

        public static CustomGenericList<HistoryStat> RequestHistorySelectByDateRangeAndRequestType(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction, DateTime startDate, DateTime endDate, int applicationID, int requestTypeID)
        {
            SqlConnection connection = sqlConnection;
            SqlTransaction transaction = sqlTransaction;

            if (connection == null)
                connection = CustomSqlHelper.CreateConnection(CustomSqlHelper.GetConnectionStringFromConnectionStrings("BHL"));

            using (SqlCommand command = CustomSqlHelper.CreateCommand("reqlog.RequestHistorySelectByDateRangeAndRequestType", connection, transaction,
                CustomSqlHelper.CreateInputParameter("StartDate", SqlDbType.DateTime, null, false, startDate),
                CustomSqlHelper.CreateInputParameter("EndDate", SqlDbType.DateTime, null, false, endDate),
                CustomSqlHelper.CreateInputParameter("ApplicationID", SqlDbType.Int, null, false, applicationID),
                CustomSqlHelper.CreateInputParameter("RequestTypeID", SqlDbType.Int, null, false, requestTypeID)
                ))
            {
                using (CustomSqlHelper<HistoryStat> helper = new CustomSqlHelper<HistoryStat>())
                {
                    CustomGenericList<HistoryStat> list = helper.ExecuteReader(command);

                    return list;
                }
            }
        }
    }
}
