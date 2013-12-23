using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MOBOT.BHL.AdminWeb.Models
{
    public class SegmentResolutionLogModel
    {
        private List<string> _segmentResolutionLogs = new List<string>();

        public List<string> SegmentResolutionLogs
        {
            get { return _segmentResolutionLogs; }
            set { _segmentResolutionLogs = value; }
        }        
    }
}