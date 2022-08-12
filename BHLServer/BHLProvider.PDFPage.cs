using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;
using System.Collections.Generic;

namespace MOBOT.BHL.Server
{
    public partial class BHLProvider
    {
        public List<PDFPage> PDFPageSelectForPdfID(int pdfId)
        {
            return new PDFPageDAL().PDFPageSelectForPdfID(null, null, pdfId);
        }
    }
}
