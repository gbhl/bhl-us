using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using System.Collections.Generic;
using System.Linq;

namespace MOBOT.BHL.AdminWeb.MVCServices
{
    public class ServiceLogService
    {
        public List<Service> ServiceList()
        {
            List<Service> allServices = new BHLProvider().ServiceSelectAll();
            List<Service> activeServices = (from s in allServices
                                            where s.Display == 1
                                            select s).ToList();
/*                                 select new Service { 
                                     ServiceID = s.ServiceID,
                                     Name = s.Name,
                                     Param = s.Param
                                 }).ToList();*/
            return activeServices;
        }

        public List<Severity> SeverityList()
        {
            List<Severity> severities = new BHLProvider().SeveritySelectAll();
            return severities;
        }
    }
}