using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
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
}