using System;
using System.Collections.Generic;
using CustomDataAccess;
using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;

namespace MOBOT.BHL.Server
{
    public partial class BHLProvider
    {
        public CustomGenericList<PDFPage> PDFPageSelectForPdfID(int pdfId)
        {
            return new PDFPageDAL().PDFPageSelectForPdfID(null, null, pdfId);
        }
    }
}
