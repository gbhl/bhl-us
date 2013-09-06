using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Reflection;

namespace MOBOT.BHL.Web.Utilities
{
    /// <summary>
    /// This class wraps the functionality of the MOBOT.Admin.* assemblies that enable logging of web requests.
    /// The MOBOT.Admin.* assemblies are loaded dynamically to reduce coupling to their functionality; if they
    /// are not available, then this class does nothing.
    /// 
    /// Non-MOBOT users of this code can replace the body of this class to enable logging to their own systems.
    /// </summary>
    public class RequestLog
    {
        private const string _adminDALName = "MOBOT.Admin.DAL";
        private const string _adminDataObjectsName = "MOBOT.Admin.DataObjects";
        private const string _dataAccessName = "MOBOT.DataAccess";
        private Assembly _adminDAL = null;
        private Assembly _adminDataObjects = null;
        private Assembly _dataAccess = null;

        #region Public properties

        private bool _loaded = false;

        public bool Loaded
        {
            get { return _loaded; }
            set { _loaded = value; }
        }

        private bool? _saveRequestLogSuccess = null;

        public bool? SaveRequestLogSuccess
        {
            get { return _saveRequestLogSuccess; }
            set { _saveRequestLogSuccess = value; }
        }

        private bool? _selectDateRangeTotalSuccess = null;

        public bool? SelectDateRangeTotalSuccess
        {
            get { return _selectDateRangeTotalSuccess; }
            set { _selectDateRangeTotalSuccess = value; }
        }

        private bool? _selectTypeByDateRangeSuccess = null;

        public bool? SelectTypeByDateRangeSuccess
        {
            get { return _selectTypeByDateRangeSuccess; }
            set { _selectTypeByDateRangeSuccess = value; }
        }

        private bool? _selectHourRangeTotalSuccess= null;

        public bool? SelectHourRangeTotalSuccess
        {
            get { return _selectHourRangeTotalSuccess; }
            set { _selectHourRangeTotalSuccess = value; }
        }

        private bool? _selectIPTotalSuccess = null;

        public bool? SelectIPTotalSuccess
        {
            get { return _selectIPTotalSuccess; }
            set { _selectIPTotalSuccess = value; }
        }

        private bool? _SelectApiUserTotalSuccess = null;

        public bool? SelectApiUserTotalSuccess
        {
            get { return _SelectApiUserTotalSuccess; }
            set { _SelectApiUserTotalSuccess = value; }
        }

        private bool? _selectUserTotalSuccess = null;

        public bool? SelectUserTotalSuccess
        {
            get { return _selectUserTotalSuccess; }
            set { _selectUserTotalSuccess = value; }
        }

        private bool? _requestHistorySelectByDateRangeAndRequestTypeSuccess = null;

        public bool? RequestHistorySelectByDateRangeAndRequestTypeSuccess
        {
            get { return _requestHistorySelectByDateRangeAndRequestTypeSuccess; }
            set { _requestHistorySelectByDateRangeAndRequestTypeSuccess = value; }
        }

        private bool? _selectStatDetailsSuccess = null;

        public bool? SelectStatDetailsSuccess
        {
            get { return _selectStatDetailsSuccess; }
            set { _selectStatDetailsSuccess = value; }
        }

        private bool? _requestTypeSelectByApplicationSuccess;

        public bool? RequestTypeSelectByApplicationSuccess
        {
            get { return _requestTypeSelectByApplicationSuccess; }
            set { _requestTypeSelectByApplicationSuccess = value; }
        }

        private bool? _selectApplicationNameSuccess;

        public bool? SelectApplicationNameSuccess
        {
            get { return _selectApplicationNameSuccess; }
            set { _selectApplicationNameSuccess = value; }
        }

        #endregion Public properties

        #region Constructor

        public RequestLog()
        {
            try
            {
                _adminDAL = System.Reflection.Assembly.Load(_adminDALName);
                _adminDataObjects = System.Reflection.Assembly.Load(_adminDataObjectsName);
                _dataAccess = System.Reflection.Assembly.Load(_dataAccessName);
                Loaded = true;
            }
            catch
            {
                // Swallow exceptions loading assemblies.  If not loaded, no log access is available, 
                // but that shouldn't throw errors to the host application.  If the host application
                // needs to know if the object instantiation succeeded, it should check the "Loaded"
                // property.
            }
        }

        #endregion Constructor

        #region Methods

        /// <summary>
        /// Saves the specified information to the request log.  Check the SaveRequestLogSuccess property
        /// to determine success or failure.
        /// </summary>
        /// <param name="applicationID"></param>
        /// <param name="ipAddress"></param>
        /// <param name="userID"></param>
        /// <param name="requestTypeID"></param>
        /// <param name="detail"></param>
        public void SaveRequestLog(int applicationID, string ipAddress, int? userID, int requestTypeID, string detail)
        {
            if (_adminDAL != null && _adminDataObjects != null)
            {
                try
                {
                    // Create an instance of a RequestLog object and set the properties
                    Type requestLogType = _adminDataObjects.GetType(_adminDataObjectsName + ".RequestLog");
                    object requestLogInstance = Activator.CreateInstance(requestLogType);

                    PropertyInfo propAppID = requestLogType.GetProperty("ApplicationID");
                    PropertyInfo propIpAddress = requestLogType.GetProperty("IPAddress");
                    PropertyInfo propUserID = requestLogType.GetProperty("UserID");
                    PropertyInfo propRequestTypeID = requestLogType.GetProperty("RequestTypeID");
                    PropertyInfo propDetail = requestLogType.GetProperty("Detail");

                    propAppID.SetValue(requestLogInstance, applicationID, null);
                    propIpAddress.SetValue(requestLogInstance, ipAddress, null);
                    propUserID.SetValue(requestLogInstance, userID, null);
                    propRequestTypeID.SetValue(requestLogInstance, requestTypeID, null);
                    propDetail.SetValue(requestLogInstance, detail, null);

                    // Create an instance of a RequestLogDAL object and invoke the SaveRequestLog method
                    Type requestLogDALType = _adminDAL.GetType(_adminDALName + ".RequestLogDAL");
                    object requestLogDALInstance = Activator.CreateInstance(requestLogDALType);

                    requestLogDALType.InvokeMember("SaveRequestLog",
                        System.Reflection.BindingFlags.InvokeMethod | System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public,
                        null, requestLogDALInstance, new object[] { null, null, requestLogInstance });

                    SaveRequestLogSuccess = true;
                }
                catch
                {
                    SaveRequestLogSuccess = false;
                }
            }
        }

        /// <summary>
        /// Get the total number of log entries between the specified dates.  Check the SelectDateRangeTotalSuccess 
        /// property to determine success or failure of this method.
        /// </summary>
        /// <param name="applicationID"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public int SelectDateRangeTotal(int applicationID, DateTime startDate, DateTime endDate)
        {
            int total = -1;

            if (_adminDAL != null && _adminDataObjects != null)
            {
                try
                {
                    // Create an instance of a RequestLogDAL object and invoke the SelectDateRangeTotal method
                    Type requestLogDALType = _adminDAL.GetType(_adminDALName + ".RequestLogDAL");
                    object requestLogDALInstance = Activator.CreateInstance(requestLogDALType);

                    total = (int) requestLogDALType.InvokeMember("SelectDateRangeTotal",
                        System.Reflection.BindingFlags.InvokeMethod | System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public,
                        null, requestLogDALInstance, new object[] { null, null, startDate, endDate, applicationID });
                    
                    SelectDateRangeTotalSuccess = true;
                }
                catch
                {
                    SelectDateRangeTotalSuccess = false;
                }
            }

            return total;
        }

        /// <summary>
        /// Get the log entries between the specified dates, grouped by request type.  Check the 
        /// SelectTypeByDateRangeSuccess property to determine success or failure of this method.
        /// </summary>
        /// <param name="applicationID"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public List<RequestLogStat> SelectTypeByDateRange(int applicationID, DateTime startDate, DateTime endDate)
        {
            List<RequestLogStat> stats = new List<RequestLogStat>();

            if (_adminDAL != null && _adminDataObjects != null && _dataAccess != null)
            {
                try
                {
                    // Create an instance of a RequestLogDAL object and invoke the SelectTypeByDateRange method
                    Type requestLogDALType = _adminDAL.GetType(_adminDALName + ".RequestLogDAL");
                    object requestLogDALInstance = Activator.CreateInstance(requestLogDALType);

                    // statList is of type MOBOT.DataAccess.DataCollection<MOBOT.Admin.DataObjects.GenericStat>
                    object statList = requestLogDALType.InvokeMember("SelectTypeByDateRange",
                        System.Reflection.BindingFlags.InvokeMethod | System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public,
                        null, requestLogDALInstance, new object[] { null, null, startDate, endDate, applicationID });
                    
                    // Build the list of stats to return.
                    Type genericStatType = _adminDataObjects.GetType(_adminDataObjectsName + ".GenericStat");
                    PropertyInfo propInt01 = genericStatType.GetProperty("IntColumn01");
                    PropertyInfo propInt02 = genericStatType.GetProperty("IntColumn02");
                    PropertyInfo propString01 = genericStatType.GetProperty("StringColumn01");

                    foreach (object stat in (IEnumerable<object>)statList)
                    {
                        RequestLogStat requestLogStat = new RequestLogStat();
                        requestLogStat.IntColumn01 = (int)propInt01.GetValue(stat, null);
                        requestLogStat.IntColumn02 = (int)propInt02.GetValue(stat, null);
                        requestLogStat.StringColumn01 = propString01.GetValue(stat, null).ToString();
                        stats.Add(requestLogStat);
                    }                    

                    SelectTypeByDateRangeSuccess = true;
                }
                catch
                {
                    SelectTypeByDateRangeSuccess = false;
                }
            }

            return stats;
        }

        /// <summary>
        /// Get the log entries for the specified date, grouped by hour.  Check the SelectHourRangeTotalSuccess 
        /// property to determine success or failure of this method.
        /// </summary>
        /// <param name="applicationID"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public List<RequestLogStat> SelectHourRangeTotal(int applicationID, DateTime date)
        {
            List<RequestLogStat> stats = new List<RequestLogStat>();

            if (_adminDAL != null && _adminDataObjects != null && _dataAccess != null)
            {
                try
                {
                    // Create an instance of a RequestLogDAL object and invoke the SelectTypeByDateRange method
                    Type requestLogDALType = _adminDAL.GetType(_adminDALName + ".RequestLogDAL");
                    object requestLogDALInstance = Activator.CreateInstance(requestLogDALType);

                    // statList is of type MOBOT.DataAccess.DataCollection<MOBOT.Admin.DataObjects.GenericStat>
                    object statList = requestLogDALType.InvokeMember("SelectHourRangeTotal",
                        System.Reflection.BindingFlags.InvokeMethod | System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public,
                        null, requestLogDALInstance, new object[] { null, null, date, applicationID });

                    // Build the list of stats to return.
                    Type genericStatType = _adminDataObjects.GetType(_adminDataObjectsName + ".GenericStat");
                    PropertyInfo propInt01 = genericStatType.GetProperty("IntColumn01");
                    PropertyInfo propInt02 = genericStatType.GetProperty("IntColumn02");
                    PropertyInfo propString01 = genericStatType.GetProperty("StringColumn01");
                    PropertyInfo propString02 = genericStatType.GetProperty("StringColumn02");

                    foreach (object stat in (IEnumerable<object>)statList)
                    {
                        RequestLogStat requestLogStat = new RequestLogStat();
                        requestLogStat.IntColumn01 = (int)propInt01.GetValue(stat, null);
                        requestLogStat.IntColumn02 = (int)propInt02.GetValue(stat, null);
                        stats.Add(requestLogStat);
                    }

                    SelectHourRangeTotalSuccess = true;
                }
                catch
                {
                    SelectHourRangeTotalSuccess = false;
                }
            }

            return stats;
        }

        /// <summary>
        /// Get the log entries for the specified dates, grouped by IP address.  Check the SelectIPTotalSuccess 
        /// property to determine success or failure of this method.
        /// </summary>
        /// <param name="applicationID"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public List<RequestLogStat> SelectIPTotal(int applicationID, DateTime startDate, DateTime endDate)
        {
            List<RequestLogStat> stats = new List<RequestLogStat>();

            if (_adminDAL != null && _adminDataObjects != null && _dataAccess != null)
            {
                try
                {
                    // Create an instance of a RequestLogDAL object and invoke the SelectTypeByDateRange method
                    Type requestLogDALType = _adminDAL.GetType(_adminDALName + ".RequestLogDAL");
                    object requestLogDALInstance = Activator.CreateInstance(requestLogDALType);

                    // statList is of type MOBOT.DataAccess.DataCollection<MOBOT.Admin.DataObjects.GenericStat>
                    object statList = requestLogDALType.InvokeMember("SelectIPTotal",
                        System.Reflection.BindingFlags.InvokeMethod | System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public,
                        null, requestLogDALInstance, new object[] { null, null, startDate, endDate, applicationID });

                    // Build the list of stats to return.
                    Type genericStatType = _adminDataObjects.GetType(_adminDataObjectsName + ".GenericStat");
                    PropertyInfo propInt01 = genericStatType.GetProperty("IntColumn01");
                    PropertyInfo propString01 = genericStatType.GetProperty("StringColumn01");

                    foreach (object stat in (IEnumerable<object>)statList)
                    {
                        RequestLogStat requestLogStat = new RequestLogStat();
                        requestLogStat.IntColumn01 = (int)propInt01.GetValue(stat, null);
                        requestLogStat.StringColumn01 = propString01.GetValue(stat, null).ToString();
                        stats.Add(requestLogStat);
                    }

                    SelectIPTotalSuccess = true;
                }
                catch
                {
                    SelectIPTotalSuccess = false;
                }
            }

            return stats;
        }

        /// <summary>
        /// Get the log entries for the specified dates, grouped by user.  Check the SelectUserTotalSuccess 
        /// property to determine success or failure of this method.
        /// </summary>
        /// <param name="applicationID"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public List<RequestLogStat> SelectUserTotal(int applicationID, DateTime startDate, DateTime endDate)
        {
            List<RequestLogStat> stats = new List<RequestLogStat>();

            if (_adminDAL != null && _adminDataObjects != null && _dataAccess != null)
            {
                try
                {
                    // Create an instance of a RequestLogDAL object and invoke the SelectTypeByDateRange method
                    Type requestLogDALType = _adminDAL.GetType(_adminDALName + ".RequestLogDAL");
                    object requestLogDALInstance = Activator.CreateInstance(requestLogDALType);

                    // statList is of type MOBOT.DataAccess.DataCollection<MOBOT.Admin.DataObjects.GenericStat>
                    object statList = requestLogDALType.InvokeMember("SelectUserTotal",
                        System.Reflection.BindingFlags.InvokeMethod | System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public,
                        null, requestLogDALInstance, new object[] { null, null, startDate, endDate, applicationID });

                    // Build the list of stats to return.
                    Type genericStatType = _adminDataObjects.GetType(_adminDataObjectsName + ".GenericStat");
                    PropertyInfo propInt01 = genericStatType.GetProperty("IntColumn01");
                    PropertyInfo propInt02 = genericStatType.GetProperty("IntColumn02");
                    PropertyInfo propString01 = genericStatType.GetProperty("StringColumn01");
                    PropertyInfo propString02 = genericStatType.GetProperty("StringColumn02");

                    foreach (object stat in (IEnumerable<object>)statList)
                    {
                        RequestLogStat requestLogStat = new RequestLogStat();
                        requestLogStat.IntColumn01 = (int)propInt01.GetValue(stat, null);
                        requestLogStat.IntColumn02 = (int)propInt02.GetValue(stat, null);
                        requestLogStat.StringColumn01 = propString01.GetValue(stat, null).ToString();
                        stats.Add(requestLogStat);
                    }

                    SelectUserTotalSuccess = true;
                }
                catch
                {
                    SelectUserTotalSuccess = false;
                }
            }

            return stats;
        }

        /// <summary>
        /// Get the log entries for the specified dates and request type, grouped by day.  Check the 
        /// RequestHistorySelectByDateRangeAndRequestTypeSuccess property to determine success or failure of this method.
        /// </summary>
        /// <param name="applicationID"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="requestTypeID"></param>
        /// <returns></returns>
        public List<RequestLogHistoryStat> RequestHistorySelectByDateRangeAndRequestType(int applicationID, 
            DateTime startDate, DateTime endDate, int requestTypeID)
        {
            List<RequestLogHistoryStat> stats = new List<RequestLogHistoryStat>();

            if (_adminDAL != null && _adminDataObjects != null && _dataAccess != null)
            {
                try
                {
                    // Create an instance of a RequestLogDAL object and invoke the SelectTypeByDateRange method
                    Type requestLogDALType = _adminDAL.GetType(_adminDALName + ".RequestLogDAL");
                    object requestLogDALInstance = Activator.CreateInstance(requestLogDALType);

                    // statList is of type MOBOT.DataAccess.DataCollection<MOBOT.Admin.DataObjects.HistoryStat>
                    object statList = requestLogDALType.InvokeMember("RequestHistorySelectByDateRangeAndRequestType",
                        System.Reflection.BindingFlags.InvokeMethod | System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public,
                        null, requestLogDALInstance, new object[] { null, null, startDate, endDate, applicationID, requestTypeID });

                    // Build the list of stats to return.
                    Type historyStatType = _adminDataObjects.GetType(_adminDataObjectsName + ".HistoryStat");
                    PropertyInfo propDay = historyStatType.GetProperty("Day");
                    PropertyInfo propMonth = historyStatType.GetProperty("Month");
                    PropertyInfo propNumRequests = historyStatType.GetProperty("NumRequests");
                    PropertyInfo propYear = historyStatType.GetProperty("Year");

                    foreach (object stat in (IEnumerable<object>)statList)
                    {
                        RequestLogHistoryStat requestLogStat = new RequestLogHistoryStat();
                        requestLogStat.Day = (int)propDay.GetValue(stat, null);
                        requestLogStat.Month = (int)propMonth.GetValue(stat, null);
                        requestLogStat.NumRequests = (int)propNumRequests.GetValue(stat, null);
                        requestLogStat.Year = (int)propYear.GetValue(stat, null);
                        stats.Add(requestLogStat);
                    }

                    RequestHistorySelectByDateRangeAndRequestTypeSuccess = true;
                }
                catch
                {
                    RequestHistorySelectByDateRangeAndRequestTypeSuccess = false;
                }
            }

            return stats;
        }

        /// <summary>
        /// Get the specific log details.  Check the SelectStatDetailsSuccess property to determine 
        /// success or failure of this method.
        /// </summary>
        /// <param name="applicationID"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="requestTypeID"></param>
        /// <param name="ipAddress"></param>
        /// <param name="userID"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public List<RequestLogRecord> SelectStatDetails(int applicationID,
            DateTime startDate, DateTime endDate, int? requestTypeID, string ipAddress,
            int? userID, int orderBy)
        {
            List<RequestLogRecord> stats = new List<RequestLogRecord>();

            if (_adminDAL != null && _adminDataObjects != null && _dataAccess != null)
            {
                try
                {
                    // Create an instance of a RequestLogDAL object and invoke the SelectTypeByDateRange method
                    Type requestLogDALType = _adminDAL.GetType(_adminDALName + ".RequestLogDAL");
                    object requestLogDALInstance = Activator.CreateInstance(requestLogDALType);

                    // statList is of type MOBOT.DataAccess.DataCollection<MOBOT.Admin.DataObjects.RequestLog>
                    object statList = requestLogDALType.InvokeMember("SelectStatDetails",
                        System.Reflection.BindingFlags.InvokeMethod | System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public,
                        null, requestLogDALInstance, 
                        new object[] { null, null, startDate, endDate, requestTypeID, ipAddress, userID, orderBy, applicationID });

                    // Build the list of stats to return.
                    Type requestLogType = _adminDataObjects.GetType(_adminDataObjectsName + ".RequestLog");
                    PropertyInfo propCreationDate = requestLogType.GetProperty("CreationDate");
                    PropertyInfo propDetail = requestLogType.GetProperty("Detail");
                    PropertyInfo propIPAddress = requestLogType.GetProperty("IPAddress");
                    PropertyInfo propRequestLogID = requestLogType.GetProperty("RequestLogID");
                    PropertyInfo propRequestTypeID = requestLogType.GetProperty("RequestTypeID");
                    PropertyInfo propUserID = requestLogType.GetProperty("UserID");
                    PropertyInfo propRequestTypeName = requestLogType.GetProperty("RequestTypeName");
                    PropertyInfo propUserName= requestLogType.GetProperty("UserName");

                    foreach (object stat in (IEnumerable<object>)statList)
                    {
                        RequestLogRecord requestLogRecord = new RequestLogRecord();
                        requestLogRecord.CreationDate = (DateTime)propCreationDate.GetValue(stat, null);
                        requestLogRecord.Detail = (propDetail.GetValue(stat, null) == null) ? string.Empty : propDetail.GetValue(stat, null).ToString();
                        requestLogRecord.IPAddress = (propIPAddress.GetValue(stat, null) == null) ? string.Empty : propIPAddress.GetValue(stat, null).ToString();
                        requestLogRecord.RequestLogID = (int)propRequestLogID.GetValue(stat, null);
                        requestLogRecord.RequestTypeID = (int)propRequestTypeID.GetValue(stat, null);
                        requestLogRecord.RequestTypeName = propRequestTypeName.GetValue(stat, null).ToString();
                        requestLogRecord.UserID = (propUserID.GetValue(stat, null) == null) ? 0 : (int)propUserID.GetValue(stat, null);
                        requestLogRecord.UserName = (propUserName.GetValue(stat, null) == null) ? string.Empty : propUserName.GetValue(stat, null).ToString();
                        stats.Add(requestLogRecord);
                    }

                    SelectStatDetailsSuccess = true;
                }
                catch
                {
                    SelectStatDetailsSuccess = false;
                }
            }

            return stats;
        }

        /// <summary>
        /// Get the request types for the specified application.  Check the RequestTypeSelectByApplicationSuccess 
        /// property to determine success or failure of this method.
        /// </summary>
        /// <param name="applicationID"></param>
        /// <returns></returns>
        public List<RequestType> RequestTypeSelectByApplication(int applicationID)
        {
            List<RequestType> types = new List<RequestType>();

            if (_adminDAL != null && _adminDataObjects != null && _dataAccess != null)
            {
                try
                {
                    // Create an instance of a RequestLogDAL object and invoke the SelectTypeByDateRange method
                    Type requestTypeDALType = _adminDAL.GetType(_adminDALName + ".RequestTypeDAL");
                    object requestTypeDALInstance = Activator.CreateInstance(requestTypeDALType);

                    // statList is of type MOBOT.DataAccess.DataCollection<MOBOT.Admin.DataObjects.GenericStat>
                    object statList = requestTypeDALType.InvokeMember("RequestTypeSelectByApplication",
                        System.Reflection.BindingFlags.InvokeMethod | System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public,
                        null, requestTypeDALInstance, new object[] { null, null, applicationID });

                    // Build the list of stats to return.
                    Type requestTypeType = _adminDataObjects.GetType(_adminDataObjectsName + ".RequestType");
                    PropertyInfo propID = requestTypeType.GetProperty("RequestTypeID");
                    PropertyInfo propName = requestTypeType.GetProperty("RequestTypeName");

                    foreach (object stat in (IEnumerable<object>)statList)
                    {
                        RequestType requestType = new RequestType();
                        requestType.RequestTypeID = (int)propID.GetValue(stat, null);
                        requestType.RequestTypeName = propName.GetValue(stat, null).ToString();
                        types.Add(requestType);
                    }

                    RequestTypeSelectByApplicationSuccess = true;
                }
                catch
                {
                    RequestTypeSelectByApplicationSuccess = false;
                }
            }

            return types;
        }

        public string SelectApplicationName(int applicationID)
        {
            string appName = string.Empty;

            if (_adminDAL != null && _adminDataObjects != null)
            {
                try
                {
                    // Create an instance of a RequestLogDAL object and invoke the SelectTypeByDateRange method
                    Type applicationDALType = _adminDAL.GetType(_adminDALName + ".ApplicationDAL");
                    object applicationDALInstance = Activator.CreateInstance(applicationDALType);

                    // statList is of type MOBOT.DataAccess.DataCollection<MOBOT.Admin.DataObjects.GenericStat>
                    object application = applicationDALType.InvokeMember("ApplicationSelectAuto",
                        System.Reflection.BindingFlags.InvokeMethod | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public,
                        null, applicationDALInstance, new object[] { null, null, applicationID });

                    // Build the list of stats to return.
                    Type applicationType = _adminDataObjects.GetType(_adminDataObjectsName + ".Application");
                    PropertyInfo propName = applicationType.GetProperty("ApplicationName");

                    if (application != null)
                    {
                        appName = propName.GetValue(application, null).ToString();
                        SelectApplicationNameSuccess = true;
                    }
                    else
                    {
                        SelectApplicationNameSuccess = false;
                    }
                }
                catch
                {
                    SelectApplicationNameSuccess = false;
                }
            }

            return appName;
        }

        #endregion Methods
    }

    #region Supporting classes

    public class RequestLogStat
    {
        public RequestLogStat() { }
        public RequestLogStat(int int01, int? int02, string str01, string str02)
        {
            IntColumn01 = int01;
            IntColumn02 = int02;
            StringColumn01 = str01;
            StringColumn02 = str02;
        }

        public int IntColumn01 { get; set; }
        public int? IntColumn02 { get; set; }
        public string StringColumn01 { get; set; }
        public string StringColumn02 { get; set; }
    }

    public class RequestLogHistoryStat
    {
        public int Day { get; set; }
        public int Month { get; set; }
        public int NumRequests { get; set; }
        public int Year { get; set; }
    }

    public class RequestLogRecord
    {
        public int ApplicationID { get; set; }
        public DateTime CreationDate { get; set; }
        public string Detail { get; set; }
        public string IPAddress { get; set; }
        public int RequestLogID { get; set; }
        public int RequestTypeID { get; set; }
        public int? UserID { get; set; }
        public string RequestTypeName { get; set; }
        public string UserName { get; set; }
    }

    public class RequestType
    {
        public int RequestTypeID { get; set; }
        public string RequestTypeName { get; set; }
    }

    #endregion Supporting classes

    #region Enums

    public enum RequestLogSearchOrderBy
    {
        RequestLogID = 0,
        ApplicationID = 1,
        IPAddress = 2,
        UserID = 3,
        CreationDate = 4,
        RequestTypeID = 5,
        Detail = 6,
        UserName = 7,
        RequestTypeName = 8,
    }

    public enum RequestLogSortOrder
    {
        None = 0,
        Ascending = 1,
        Descending = 2,
    }

    #endregion Enums
}
