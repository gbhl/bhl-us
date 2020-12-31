using System;
using System.Collections.Generic;
using CustomDataAccess;
using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;

namespace MOBOT.BHL.Server
{
    public partial class BHLProvider
    {
        public PDF AddNewPdf(int itemID, String emailAddress, String shareWith, bool imagesOnly,
            String articleTitle,String articleCreators, String articleTags, List<int> pageIDs)
        {
            return new PDFDAL().AddNewPdf(null, null, itemID, emailAddress, shareWith, imagesOnly, 
                articleTitle, articleCreators, articleTags, pageIDs);
        }

        public List<PDF> PDFSelectForFileCreation()
        {
            return new PDFDAL().PDFSelectForFileCreation(null, null);
        }

        public List<PDF> PDFSelectForDeletion()
        {
            return new PDFDAL().PDFSelectForDeletion(null, null);
        }

        public List<PDF> PDFSelectDuplicateForPdfID(int pdfId)
        {
            return new PDFDAL().PDFSelectDuplicateForPdfID(null, null, pdfId);
        }

        public List<PageSummaryView> PDFPageSummaryViewSelectByPdfID(int pdfId)
        {
            return new PDFPageDAL().PDFPageSummaryViewSelectByPdfID(null, null, pdfId);
        }

        public List<PDF> PDFSelectForWeekAndStatus(int year, int week, int pdfStatusId)
        {
            return new PDFDAL().PDFSelectForWeekAndStatus(null, null, year, week, pdfStatusId);
        }

        public PDF PDFSelectAuto(int pdfId)
        {
            return new PDFDAL().PDFSelectAuto(null, null, pdfId);
        }

        public void PDFSave(PDF pdf)
        {
            new PDFDAL().Save(null, null, pdf);
        }

        public PDF PDFUpdateGenerationInfo(int pdfId, string fileLocation, string fileUrl,
            int numberImagesMissing, int numberOcrMissing)
        {
            PDFDAL dal = new PDFDAL();
            PDF pdf = dal.PDFSelectAuto(null, null, pdfId);
            if (pdf != null)
            {
                pdf.FileLocation = fileLocation;
                pdf.FileUrl = fileUrl;
                pdf.NumberImagesMissing = numberImagesMissing;
                pdf.NumberOcrMissing = numberOcrMissing;
                pdf.PdfStatusID = this.PdfStatusGenerated;
                pdf = dal.PDFUpdateGenerationInfo(null, null, pdf);
            }
            else
            {
                throw new Exception("Could not find existing pdf record");
            }
            return pdf;
        }

        public PDF PDFUpdateFileDeletion(int pdfId)
        {
            PDFDAL dal = new PDFDAL();
            PDF pdf = dal.PDFSelectAuto(null, null, pdfId);
            if (pdf != null)
            {
                pdf.FileDeletionDate = DateTime.Now;
                pdf = dal.PDFUpdateAuto(null, null, pdf);
            }
            else
            {
                throw new Exception("Could not find existing pdf record");
            }
            return pdf;
        }


        private int _pdfStatusPending = 10;
        public int PdfStatusPending { get { return _pdfStatusPending; } }
        private int _pdfStatusProcessing = 20;
        public int PdfStatusProcessing { get { return _pdfStatusProcessing; } }
        private int _pdfStatusGenerated = 30;
        public int PdfStatusGenerated { get { return _pdfStatusGenerated; } }
        private int _pdfStatusError = 40;
        public int PdfStatusError { get { return _pdfStatusError; } }
        private int _pdfStatusRejected = 50;
        public int PdfStatusRejected { get { return _pdfStatusRejected; } }

        public bool PDFUpdatePdfStatus(int pdfId, int pdfStatusId)
        {
            PDFDAL dal = new PDFDAL();
            return dal.PDFUpdatePdfStatus(null, null, pdfId, pdfStatusId);
        }
    }
}
