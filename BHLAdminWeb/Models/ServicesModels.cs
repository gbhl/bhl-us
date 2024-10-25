using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using System;
using System.Collections.Generic;

namespace MOBOT.BHL.AdminWeb.Models
{
    public class ServiceOverviewModel
    {
        public List<Service> ServiceList { get; set; }

        public ServiceOverviewModel()
        {
            ServiceList = GetServices();
        }

        public List<Service> GetServices()
        {
            List<Service> serviceList = new BHLProvider().ServiceLogSelectSummaryList();
            return serviceList;
        }
    }

    public class ServiceLogModel
    {
        public List<ServiceLog> ServiceLogList { get; set; }

        public string ServiceID { get; set; } = string.Empty;
        public string SeverityID { get; set; } = string.Empty;
        public DateTime? StartDate {get; set;}
        public DateTime? EndDate { get; set; }

        public ServiceLogModel()
        {
            ServiceLogList = GetServiceLog(null);
        }

        public ServiceLogModel(int? id)
        {
            ServiceLogList = GetServiceLog(id);
        }

        public List<ServiceLog> GetServiceLog(int? serviceID, int? severityID = null, DateTime? startDate = null, DateTime? endDate = null )
        {
            List<ServiceLog> serviceLogList = new BHLProvider().ServiceLogSelectDetailedList(serviceID, severityID, startDate, endDate);
            return serviceLogList;
        }
    }
}