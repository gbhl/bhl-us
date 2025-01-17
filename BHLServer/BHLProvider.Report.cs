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

        public List<PermissionsTitle> ReportSelectPermissionsTitles(int? titleID, bool notKnown, bool inCopyright, bool notProvided, int numRows, int startRow, string sortColumn, string sortDirection)
        {
            return new ReportDAL().ReportSelectPermissionsTitles(null, null, titleID, (notKnown ? 1: 0), (inCopyright ? 1 : 0), (notProvided ? 1 : 0),
                numRows, startRow, sortColumn, sortDirection);
        }
    }
}
