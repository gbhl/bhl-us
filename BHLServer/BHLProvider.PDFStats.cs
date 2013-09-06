using System;
using CustomDataAccess;
using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;

namespace MOBOT.BHL.Server
{
    public partial class BHLProvider
    {
        public CustomGenericList<PDFStats> PDFStatsSelectOverview()
        {
            return (new PDFStatsDAL().PDFStatsSelectOverview(null, null));
        }

        public CustomGenericList<PDFStats> PDFStatsSelectExpanded()
        {
            return (new PDFStatsDAL().PDFStatsSelectExpanded(null, null));
        }
    }
}
