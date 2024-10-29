using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using MOBOT.BHL.Utility;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Web.Services.Description;

namespace MOBOT.BHL.AdminWeb.Models
{
    public class ServiceOverviewModel
    {
        public List<DataObjects.Service> ServiceList { get; set; }

        public ServiceOverviewModel()
        {
            ServiceList = GetServices();
        }

        public List<DataObjects.Service> GetServices()
        {
            List<DataObjects.Service> serviceList = new BHLProvider().ServiceLogSelectSummaryList();
            return serviceList;
        }
    }

    public class ServiceLogModel
    {
        public List<ServiceLog> ServiceLogList { get; set; } = new List<ServiceLog>();

        public string ServiceID { get; set; } = string.Empty;
        public string SeverityID { get; set; } = string.Empty;
        public DateTime? StartDate {get; set;}
        public DateTime? EndDate { get; set; }
        public byte[] DownloadLog { get; set; }


        public ServiceLogModel()
        {
        }

        public ServiceLogModel(int? id)
        {
        }

        public List<ServiceLog> GetServiceLog(int? serviceID, int? severityID = null, DateTime? startDate = null, DateTime? endDate = null )
        {
            List<ServiceLog> serviceLogList = new BHLProvider().ServiceLogSelectDetailedList(serviceID, severityID, startDate, endDate);
            return serviceLogList;
        }

        /// <summary>
        /// Get service log records
        /// </summary>
        /// <param name="serviceID">Identifier of the service</param>
        /// <param name="severityID">Identifier of the log severity</param>
        /// <param name="startDate">Starting date of log records to return</param>
        /// <param name="endDate">Ending date of log records to return</param>
        /// <param name="numRows">Number of rows to return</param>
        /// <param name="startRow">First row to return (enables paging)</param>
        /// <param name="sortColumn">Column by which to sort data</param>
        /// <param name="sortDirection">Direction of sort</param>
        public ServiceLogJson.Rootobject GetServiceLogRecords(int? serviceID, int? severityID, DateTime? startDate, DateTime? endDate, 
            int numRows, int startRow, string sortColumn, string sortDirection)
        {
            List<ServiceLog> records = new BHLProvider().ServiceLogSelectDetailedList(serviceID, severityID, startDate, endDate,
                numRows, startRow, sortColumn, sortDirection);

            ServiceLogJson.Rootobject json = new ServiceLogJson.Rootobject();
            json.iTotalRecords = (records.Count == 0) ? "0" : records[0].TotalRecords.ToString();
            json.iTotalDisplayRecords = json.iTotalRecords;

            ServiceLogJson.Datum[] aaData = new ServiceLogJson.Datum[records.Count];

            for (int x = 0; x < records.Count; x++)
            {
                aaData[x] = new ServiceLogJson.Datum()
                {
                    id = records[x].ServiceLogID.ToString(),
                    severityID = records[x].SeverityID.ToString(),
                    name = records[x].Name,
                    param = records[x].Param,
                    frequencyLabel = records[x].FrequencyLabel,
                    intervalInMinutes = records[x].IntervalInMinutes,
                    minutesElapsedSinceLog = records[x].MinutesElapsedSinceLog,
                    severityLabel = records[x].SeverityLabel,
                    fgColorHexCode = records[x].FGColorHexCode,
                    message = records[x].Message,
                    creationDate = records[x].CreationDate.ToString("yyyy-MM-dd HH:mm:ss")
                };
            }
            json.aaData = aaData;

            return json;
        }

        public void GetServiceLogCSV()
        {
            // Get the data to be formatted as CSV
            List<ServiceLog> logs = new BHLProvider().ServiceLogSelectDetailedList(Convert.ToInt32(this.ServiceID), Convert.ToInt32(this.SeverityID), 
                this.StartDate, this.EndDate, 1000000, 1, "l.CreationDate", "desc");

            var data = new List<dynamic>();
            foreach (var log in logs)
            {
                var record = new ExpandoObject() as IDictionary<string, Object>;
                record.Add("Service", log.FullName);
                record.Add("Frequency", log.FrequencyLabel);
                record.Add("Log Date", log.CreationDate.ToString("yyyy-MM-dd HH:mm:ss"));
                record.Add("Message", log.Message);
                record.Add("Severity", log.SeverityLabel);
                data.Add(record);
            }

            DownloadLog = new CSV().FormatCSVData(data);
        }
    }

    /// <summary>
    /// Class used to produce the JSON representation of citations records that is needed by jQuery DataTables
    /// </summary>
    [Serializable]
    public class ServiceLogJson
    {
        public class Rootobject
        {
            public int sEcho { get; set; }
            public string iTotalRecords { get; set; }
            public string iTotalDisplayRecords { get; set; }
            public Datum[] aaData { get; set; }
        }

        public class Datum
        {
            public string id { get; set; }
            public string serviceID { get; set; }
            public string name { get; set; }
            public string param { get; set; }
            public string fullName
            {
                get
                {
                    return (this.name + " " + (string.IsNullOrWhiteSpace(this.param) ? "" : "/") + " " + this.param).Trim();
                }
            }
            public string frequencyLabel { get; set; }
            public int? intervalInMinutes { get; set; }
            public int? minutesElapsedSinceLog { get; set; }
            public string severityID { get; set; }
            public string severityLabel { get; set; }
            public string fgColorHexCode { get; set; }
            public string severity
            {
                get
                {
                    return string.Format("<span style='color:{0}'>{1}</span>", this.fgColorHexCode, this.severityLabel);
                }
            }
            public string message { get; set; }
            public string creationDate { get; set; }
            public string logDate
            {
                get
                {
                    return string.Format("<span style='color:{0}'>{1}</span>", 
                        ((this.intervalInMinutes ?? 100000000) < (this.minutesElapsedSinceLog ?? 0) ? "#FF0000" : "#000000"),
                        (this.creationDate == null ? "" : this.creationDate));
                }
            }
        }
    }
}