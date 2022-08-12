using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;
using System.Collections.Generic;

namespace MOBOT.BHL.Server
{
    public partial class BHLProvider
    {
        public List<PDFStats> PDFStatsSelectOverview()
        {
            return (new PDFStatsDAL().PDFStatsSelectOverview(null, null));
        }

        public List<PDFStats> PDFStatsSelectExpanded()
        {
            return (new PDFStatsDAL().PDFStatsSelectExpanded(null, null));
        }
    }
}
