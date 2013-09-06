using System;
using System.Web.Services;
using System.ComponentModel;
using CustomDataAccess;
using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;

namespace MOBOT.BHL.WebService
{
    public partial class BHLWS : System.Web.Services.WebService
    {
        [WebMethod]
        public CustomGenericList<PDF> PDFSelectForFileCreation()
        {
            BHLProvider bhlServer = new BHLProvider();
            return bhlServer.PDFSelectForFileCreation();
        }

        [WebMethod]
        public CustomGenericList<PDF> PDFSelectForDeletion()
        {
            BHLProvider bhlServer = new BHLProvider();
            return bhlServer.PDFSelectForDeletion();
        }

        [WebMethod]
        public CustomGenericList<PDF> PDFSelectDuplicateForPdfID(int pdfId)
        {
            BHLProvider bhlServer = new BHLProvider();
            return bhlServer.PDFSelectDuplicateForPdfID(pdfId);
        }

        [WebMethod]
        public CustomGenericList<PageSummaryView> PDFPageSummaryViewSelectByPdfID(int pdfId)
        {
            BHLProvider bhlServer = new BHLProvider();
            return bhlServer.PDFPageSummaryViewSelectByPdfID(pdfId);
        }

        [WebMethod]
        public PDF PDFUpdateGenerationInfo(int pdfId, String fileLocation, String fileUrl,
            int numberImagesMissing, int numberOcrMissing)
        {
            BHLProvider bhlServer = new BHLProvider();
            return bhlServer.PDFUpdateGenerationInfo(pdfId, fileLocation, fileUrl, 
                numberImagesMissing, numberOcrMissing);
        }

        [WebMethod]
        public PDF PDFUpdateFileDeletion(int pdfId)
        {
            BHLProvider bhlServer = new BHLProvider();
            return bhlServer.PDFUpdateFileDeletion(pdfId);
        }

        [WebMethod]
        public bool PDFUpdatePdfStatusProcessing(int pdfid)
        {
            return this.PDFUpdatePdfStatus(pdfid, new BHLProvider().PdfStatusProcessing);
        }

        [WebMethod]
        public bool PDFUpdatePdfStatusGenerated(int pdfid)
        {
            return this.PDFUpdatePdfStatus(pdfid, new BHLProvider().PdfStatusGenerated);
        }

        [WebMethod]
        public bool PDFUpdatePdfStatusError(int pdfid)
        {
            return this.PDFUpdatePdfStatus(pdfid, new BHLProvider().PdfStatusError);
        }

        [WebMethod]
        public bool PDFUpdatePdfStatusRejected(int pdfid)
        {
            return this.PDFUpdatePdfStatus(pdfid, new BHLProvider().PdfStatusRejected);
        }

        private bool PDFUpdatePdfStatus(int pdfId, int pdfStatusId)
        {
            BHLProvider bhlServer = new BHLProvider();
            return bhlServer.PDFUpdatePdfStatus(pdfId, pdfStatusId);
        }
    }
}
