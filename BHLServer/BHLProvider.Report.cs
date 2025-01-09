using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;
using System.Collections.Generic;

namespace MOBOT.BHL.Server
{
    public partial class BHLProvider
    {
        public List<ReportOrphan> ReportSelectOrphanedEntities()
        {
            return new ReportDAL().ReportSelectOrphanedEntities(null, null);
        }

        public List<PermissionsTitle> ReportSelectPermissionsTitles(int? titleID, int numRows, int startRow, string sortColumn, string sortDirection)
        {
            return new ReportDAL().ReportSelectPermissionsTitles(null, null, titleID, numRows, startRow, sortColumn, sortDirection);
        }
    }
}
