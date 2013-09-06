using System;
using CustomDataAccess;
using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;

namespace MOBOT.BHL.Server
{
    public partial class BHLProvider
    {
        public CustomGenericList<PDFStatus> PDFStatusSelectAll()
        {
            return (new PDFStatusDAL().PDFStatusSelectAll(null, null));
        }
    }
}
