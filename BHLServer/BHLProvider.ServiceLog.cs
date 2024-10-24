using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;
using System;
using System.Collections.Generic;

namespace MOBOT.BHL.Server
{
    public partial class BHLProvider
    {
        public ServiceLog ServiceLogSelectAuto(int serviceLogID)
        {
            return new ServiceLogDAL().ServiceLogSelectAuto(null, null, serviceLogID);
        }

        public void ServiceLogInsert(DateTime logdate, string servicename, string serviceparam, string severityname,
                int? errornumber, string procedure, int? line, string message, string stacktrace)
        {
            new ServiceLogDAL().ServiceLogInsert(null, null, logdate, servicename, serviceparam, severityname, 
                errornumber, procedure, line, message, stacktrace);
        }

        public List<ServiceLog> ServiceLogSelectDetailedList(int? serviceID = null, int? severityID = null, DateTime? startDate = null, DateTime? endDate = null, int numRows = 100, int startRow = 1, string sortColumn = "CreationDate", string sortDirection = "DESC")
        {
            return new ServiceLogDAL().ServiceLogSelectDetailedList(null, null, serviceID, severityID, startDate, endDate, numRows, startRow, sortColumn, sortDirection);
        }
    }
}
