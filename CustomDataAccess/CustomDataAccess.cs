using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace CustomDataAccess
{
    /// <summary>
    /// Specifies how elements in a list are sorted.
    /// </summary>
    [Serializable]
    public enum SortOrder
    {
        /// <summary>
        /// The elements are sorted in ascending order.
        /// </summary>
        Ascending = 0,
        /// <summary>
        /// The elements are sorted in descending order.
        /// </summary>
        Descending = 1
    }

    /// <summary>
    /// This Sql Helper based upon a named type provides helper functions assisting in the retrieval of the named type from a data base table.
    /// </summary>
    [Serializable]
    public class CustomSqlHelper<T> : IDisposable
    {
        internal CustomDataRow loadDataRow(CustomDataRow row)
        {
            return row;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="loadObjectCallBack"></param>
        /// <returns></returns>
        public List<T> ExecuteReader(SqlCommand command, CustomDataDelegate<T>.LoadObjectCallBack
            loadObjectCallBack)
        {
            return executeReader(null, command, loadObjectCallBack);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="transporterCallBack"></param>
        /// <param name="command"></param>
        public List<T> ExecuteReader(CustomDataDelegate<T>.TransporterCallBack transporterCallBack,
            SqlCommand command, CustomDataDelegate<T>.LoadObjectCallBack loadObjectCallBack)
        {
            return executeReader(transporterCallBack, command, loadObjectCallBack);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="transporterCallBack"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        internal List<T> executeReader(CustomDataDelegate<T>.TransporterCallBack transporterCallBack,
            SqlCommand command, CustomDataDelegate<T>.LoadObjectCallBack loadObjectCallBack)
        {
            List<T> list = new List<T>();

            // rectify connection state, i.e. if connection is closed, open connection
            command = CustomSqlHelper.rectifyConnectionState(command);

            SqlDataReader reader;

            if (command.Transaction == null)
            {
                reader = command.ExecuteReader(CommandBehavior.SingleResult | CommandBehavior.CloseConnection);
            }
            else
            {
                reader = command.ExecuteReader(CommandBehavior.SingleResult);
            }

            using (reader)
            {
                while (reader.Read())
                {
                    T o = default(T);

                    if (loadObjectCallBack != null)
                    {
                        // use the load object call back 
                        o = loadObjectCallBack(CustomSqlHelper.Convert(reader));
                    }
                    else
                    {
                        o = (T)Activator.CreateInstance(typeof(T));
                        if (o is ISetValues)
                        {
                            ((ISetValues)o).SetValues(CustomSqlHelper.Convert(reader));
                        }
                        else
                        {
                            o = (T)reader.GetValue(0);
                        }
                    }

                    if (transporterCallBack != null)
                    {
                        try
                        {
                            // use the transporter call back delegate to return the object
                            transporterCallBack(new CustomDataTransporter<T>(o));
                        }
                        catch (Exception e)
                        {
                            // if an error occurs with the call back on the delegate, then end this request
                            reader.Close();
                            throw new Exception("Error on call back.", e);
                        }
                    }
                    else
                    {
                        list.Add(o);
                    }
                }
            }

            if (transporterCallBack != null)
            {
                transporterCallBack(new CustomDataTransporter<T>(true));
                return null;
            }
            else
            {
                return list;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public List<T> ExecuteReader(SqlCommand command)
        {
            return executeReader(null, command, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="transporterCallBack"></param>
        /// <param name="command"></param>
        public List<T> ExecuteReader(CustomDataDelegate<T>.TransporterCallBack transporterCallBack,
            SqlCommand command)
        {
            return executeReader(transporterCallBack, command, null);
        }

        ///<summary>
        ///Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        ///</summary>
        ///<filterpriority>2</filterpriority>
        public void Dispose()
        {
            // implemented to conform with the IDisposable interface
        }

        internal void persist()
        {
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class CustomSqlHelper
    {
        /// <summary>
        /// This is the Sql parameter token.
        /// </summary>
        public static char ParameterToken
        {
            get
            {
                return '@';
            }
        }

        /// <summary>
        /// Retrieves parameter information from the stored procedure as specified in the procedure string.
        /// </summary>
        /// <param name="procedure">Stored procedure name as a string.</param>
        /// <param name="connection"><see cref="SqlConnection"/> where the stored procedure is located.</param>
        /// <returns>An array of <see cref="SqlParameter"/>.</returns>
        public static SqlParameter[] DeriveParameters(string procedure, SqlConnection connection)
        {
            SqlCommand command = CreateCommand(procedure, connection, null, null);
            SqlCommandBuilder.DeriveParameters(command);

            SqlParameter[] parameters = new SqlParameter[command.Parameters.Count];
            command.Parameters.CopyTo(parameters, 0);

            return parameters;
        }

        /// <summary>
        /// Starts a database transaction with an isolation level of RepeatableRead and the specified name if isTransactionCoordinator is true.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="connection"></param>
        /// <param name="transaction"></param>
        /// <param name="isTransactionCoordinator"></param>
        /// <returns></returns>
        public static SqlTransaction BeginTransaction(string name, SqlConnection connection, SqlTransaction transaction,
            bool isTransactionCoordinator)
        {
            if (isTransactionCoordinator)
            {
                return BeginTransaction(name, connection);
            }
            else
            {
                return transaction;
            }
        }

        /// <summary>
        /// Starts a database transaction with an isolation level of RepeatableRead and a unique name if isTransactionCoordinator is true.
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="transaction"></param>
        /// <param name="isTransactionCoordinator"></param>
        /// <returns></returns>
        public static SqlTransaction BeginTransaction(SqlConnection connection, SqlTransaction transaction,
            bool isTransactionCoordinator)
        {
            if (isTransactionCoordinator)
            {
                return BeginTransaction(connection);
            }
            else
            {
                return transaction;
            }
        }

        /// <summary>
        /// Starts a database transaction with an isolation level of RepeatableRead and the specified transaction name.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="connection"></param>
        /// <returns></returns>
        public static SqlTransaction BeginTransaction(string name, SqlConnection connection)
        {
            return connection.BeginTransaction(IsolationLevel.RepeatableRead, name);
        }

        /// <summary>
        /// Starts a database transaction with an isolation level of RepeatableRead and a unique name.
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        public static SqlTransaction BeginTransaction(SqlConnection connection)
        {
            return BeginTransaction(Guid.NewGuid().ToString("n"), connection);
        }

        /// <summary>
        /// Commits the database transaction and closes the transaction connection if isTransactionCoordinator is true.
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="isTransactionCoordinator"></param>
        /// <returns></returns>
        public static bool CommitTransaction(SqlTransaction transaction, bool isTransactionCoordinator)
        {
            if (isTransactionCoordinator)
            {
                return CommitTransaction(transaction);
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Commits the database transaction and closes the transaction connection.
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static bool CommitTransaction(SqlTransaction transaction)
        {
            if (transaction == null)
            {
                return false;
            }
            else
            {
                SqlConnection connection = transaction.Connection;
                transaction.Commit();
                connection.Close();

                return true;
            }
        }

        /// <summary>
        /// Rolls back a database transaction from a pending state and closes the transaction connection if isTransactionCoordinator is true.
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="isTransactionCoordinator"></param>
        /// <returns></returns>
        public static bool RollbackTransaction(SqlTransaction transaction, bool isTransactionCoordinator)
        {
            if (isTransactionCoordinator)
            {
                return RollbackTransaction(transaction);
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Rolls back a database transaction from a pending state and closes the transaction connection.
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static bool RollbackTransaction(SqlTransaction transaction)
        {
            if (transaction == null)
            {
                return false;
            }
            else
            {
                SqlConnection connection = transaction.Connection;
                transaction.Rollback();
                connection.Close();

                return true;
            }
        }

        #region Connection

        /// <summary>
        /// This is a sample configuration that this function addresses:
        /// <code>
        /// &lt;?xml version="1.0" encoding="utf-8"?&gt;
        /// &lt;configuration&gt;
        /// &lt;connectionStrings&gt;
        /// &lt;add name="Botanicus" connectionString="Data Source=127.0.0.1;Initial Catalog=DBName;user=DBUser;password=Password;Connection Timeout=180"
        /// providerName="System.Data.SqlClient" /&gt;
        /// &lt;/connectionStrings&gt;
        /// &lt;/configuration&gt;
        /// </code>
        /// </summary>
        /// <param name="name">Connection name</param>
        /// <returns>The associated connection string from the connectionStrings section of the configuration file.</returns>
        public static string GetConnectionStringFromConnectionStrings(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }

        /// <summary>
        /// This is a sample configuration that this function addresses:
        /// <code>
        /// &lt;?xml version="1.0" encoding="utf-8"?&gt;
        /// &lt;configuration&gt;
        /// &lt;appSettings&gt;
        /// &lt;add key="Botanicus" value="Data Source=127.0.0.1;Initial Catalog=DBName;user=DBUser;password=Password;Connection Timeout=180" /&gt;
        /// &lt;/appSettings&gt;
        /// &lt;/configuration&gt;
        /// </code>
        /// </summary>
        /// <param name="name">Connection name</param>
        /// <returns>The associated connection string from the appSettings section of the configuration file.</returns>
        public static string GetConnectionStringFromAppSetting(string name)
        {
            return ConfigurationManager.AppSettings.Get(name);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="connection"></param>
        /// <returns></returns>
        public static SqlConnection CreateConnection(string connectionString, SqlConnection connection)
        {
            if (connection == null)
            {
                return CreateConnection(connectionString);
            }
            else
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                return connection;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static SqlConnection CreateConnection(string connectionString)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            return connection;
        }

        public static bool CloseConnection(SqlConnection connection, bool isTransactionCoordinator)
        {
            if (isTransactionCoordinator)
            {
                return CloseConnection(connection);
            }

            return true;
        }

        public static bool CloseConnection(SqlConnection connection)
        {
            if (connection != null)
            {
                connection.Close();
            }

            return true;
        }

        #endregion Connection

        #region Is Transaction Coordinator

        /// <summary>
        /// Determines if this is the transaction coordinator based upon the state of the transaction. If the transaction is null then returns true, this is the transaction coordinator, otherwise returns false.
        /// </summary>
        /// <param name="transaction">Transaction as a SqlTransaction.</param>
        /// <returns>True if this is the transaction coordinator, otherwise returns false.</returns>
        public static bool IsTransactionCoordinator(SqlTransaction transaction)
        {
            if (transaction == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion Is Transaction Coordinator

        #region Command

        public static SqlCommand CreateCommand(string procedure, SqlConnection connection)
        {
            return createCommand(procedure, connection, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="procedure"></param>
        /// <param name="connection"></param>
        /// <param name="transaction"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static SqlCommand CreateCommand(string procedure, SqlConnection connection, SqlTransaction transaction, params SqlParameter[] parameters)
        {
            return createCommand(procedure, connection, transaction, parameters);
        }

        private static SqlCommand createCommand(string procedure, SqlConnection connection, SqlTransaction transaction, params SqlParameter[] parameters)
        {
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.Connection = connection;
            command.CommandText = procedure;
            command.Parameters.AddRange(parameters);
            command.Transaction = transaction;
            command.CommandTimeout = getCommandTimeout();

            return command;
        }

        private const int DEFAULTCOMMANDTIMEOUT = 180;

        private static int getCommandTimeout()
        {
            string[] cts = ConfigurationManager.AppSettings.GetValues("DBCommandTimeout");
            if (cts == null || cts.Length == 0)
            {
                return DEFAULTCOMMANDTIMEOUT;
            }
            else
            {
                int result = 0;
                bool flag = int.TryParse(cts[0], out result);
                if (flag)
                {
                    return result;
                }
                else
                {
                    return DEFAULTCOMMANDTIMEOUT;
                }
            }
        }

        #endregion Command

        #region Parameter

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="dbType"></param>
        /// <param name="size"></param>
        /// <param name="direction"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        internal static SqlParameter createParameter(string parameterName, SqlDbType dbType, int? size, bool isNullable, ParameterDirection direction, object value)
        {
            SqlParameter parameter;

            string p = tokenizeParameter(parameterName);

            if (size.HasValue)
            {
                parameter = new SqlParameter(p, dbType, size.Value);
            }
            else
            {
                parameter = new SqlParameter(p, dbType);
            }

            parameter.Direction = direction;

            if (value is string && size.HasValue)
            {
                string v = (string)value;
                if (v.Length > size.Value)
                {
                    v = v.Substring(0, size.Value);
                }
                value = v;
            }

            if (value != null)
            {
                parameter.Value = value;
            }
            else if (isNullable)
            {
                parameter.Value = DBNull.Value;
            }

            return parameter;
        }

        public static SqlParameter CreateInputParameter(string parameterName, SqlDbType dbType, int? size, bool isNullable, object value)
        {
            return createParameter(parameterName, dbType, size, isNullable, ParameterDirection.Input, value);
        }

        public static SqlParameter CreateOutputParameter(string parameterName, SqlDbType dbType, int? size, bool isNullable)
        {
            return createParameter(parameterName, dbType, size, isNullable, ParameterDirection.Output, null);
        }

        public static SqlParameter CreateInputOutputParameter(string parameterName, SqlDbType dbType, int? size, bool isNullable, object value)
        {
            return createParameter(parameterName, dbType, size, isNullable, ParameterDirection.InputOutput, value);
        }

        public static SqlParameter CreateReturnValueParameter(string parameterName, SqlDbType dbType, int? size, bool isNullable)
        {
            return createParameter(parameterName, dbType, size, isNullable, ParameterDirection.ReturnValue, null);
        }

        public static SqlParameter CreateParameter(string parameterName, SqlDbType dbType, int? size, bool isNullable, ParameterDirection direction, object value)
        {
            return createParameter(parameterName, dbType, size, isNullable, direction, value);
        }

        public static CustomDataRow Convert(SqlDataReader reader)
        {
            CustomDataRow dataRow = null;
            for (int i = 0; i < reader.FieldCount; i++)
            {
                CustomDataColumn column = new CustomDataColumn(reader.GetOrdinal(reader.GetName(i)), reader.GetName(i), reader.GetFieldType(i), reader.GetValue(i), reader.IsDBNull(i));

                if (dataRow == null)
                {
                    dataRow = new CustomDataRow();
                }

                dataRow.Add(column);
            }

            return dataRow;
        }

        #endregion Parameter

        #region ExecuteNonQuery

        public static int ExecuteNonQuery(SqlCommand command)
        {
            return executeNonQuery(command);
        }

        internal static int executeNonQuery(SqlCommand command)
        {
            command = rectifyConnectionState(command);

            int i = command.ExecuteNonQuery();

            if (command.Transaction == null)
            {
                CloseConnection(command.Connection);
            }

            return i;
        }

        public static int ExecuteNonQuery(SqlCommand command, string returnParameterName)
        {
            return executeNonQuery(command, returnParameterName);
        }

        internal static int executeNonQuery(SqlCommand command, string returnParameterName)
        {
            command.ExecuteNonQuery();

            string s = tokenizeParameter(returnParameterName);

            int i = (int)command.Parameters[command.Parameters.IndexOf(s)].Value;

            if (command.Transaction == null)
            {
                CloseConnection(command.Connection);
            }

            return i;
        }

        #endregion ExecuteNonQuery

        internal static String tokenizeParameter(string parameterName)
        {
            string s = parameterName;
            if (!s.StartsWith(ParameterToken.ToString()))
            {
                s = string.Format("{0}{1}", ParameterToken, parameterName);
            }

            return s;
        }

        public static List<CustomDataRow> ExecuteReaderAndReturnRows(SqlCommand command)
        {
            CustomSqlHelper<CustomDataRow> helper = new CustomSqlHelper<CustomDataRow>();
            List<CustomDataRow> rows = helper.executeReader(null, command, new CustomDataDelegate<CustomDataRow>.LoadObjectCallBack(helper.loadDataRow));

            return rows;
        }

        public static void ExecuteReaderAndReturnRows(CustomDataDelegate<CustomDataRow>.TransporterCallBack transporterCallBack,
            SqlCommand command)
        {
            CustomSqlHelper<CustomDataRow> helper = new CustomSqlHelper<CustomDataRow>();
            helper.executeReader(transporterCallBack, command, new CustomDataDelegate<CustomDataRow>.LoadObjectCallBack(helper.loadDataRow));
        }

        public static object ExecuteScalar(SqlCommand command)
        {
            return executeScalar(command);
        }

        internal static object executeScalar(SqlCommand command)
        {
            command = rectifyConnectionState(command);

            object o = command.ExecuteScalar();

            if (command.Transaction == null)
            {
                CloseConnection(command.Connection);
            }

            return o;
        }

        internal static SqlCommand rectifyConnectionState(SqlCommand command)
        {
            if (command.Connection.State == ConnectionState.Closed)
            {
                command.Connection.Open();
            }

            return command;
        }
    }
}