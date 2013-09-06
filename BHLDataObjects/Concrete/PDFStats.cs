using System;

namespace MOBOT.BHL.DataObjects
{
    [Serializable]
    public class PDFStats
    {
        private int _year;

        public int Year
        {
            get { return _year; }
            set { _year = value; }
        }

        private int _week;

        public int Week
        {
            get { return _week; }
            set { _week = value; }
        }

        private int _pdfStatusID;

        public int PdfStatusID
        {
            get { return _pdfStatusID; }
            set { _pdfStatusID = value; }
        }

        private String _pdfStatusName;

        public String PdfStatusName
        {
            get { return _pdfStatusName; }
            set { _pdfStatusName = value; }
        }

        private int _numberofPdfs;

        public int NumberofPdfs
        {
            get { return _numberofPdfs; }
            set { _numberofPdfs = value; }
        }

        private int _pdfsWithOcr;

        public int PdfsWithOcr
        {
            get { return _pdfsWithOcr; }
            set { _pdfsWithOcr = value; }
        }

        private int _pdfsWithArticleMetadata;

        public int PdfsWithArticleMetadata
        {
            get { return _pdfsWithArticleMetadata; }
            set { _pdfsWithArticleMetadata = value; }
        }

        private int _pdfsWithMissingImages;

        public int PdfsWithMissingImages
        {
            get { return _pdfsWithMissingImages; }
            set { _pdfsWithMissingImages = value; }
        }

        private int _pdfsWithMissingOcr;

        public int PdfsWithMissingOcr
        {
            get { return _pdfsWithMissingOcr; }
            set { _pdfsWithMissingOcr = value; }
        }

        private int _totalMissingImages;

        public int TotalMissingImages
        {
            get { return _totalMissingImages; }
            set { _totalMissingImages = value; }
        }

        private int _totalMissingOcr;

        public int TotalMissingOcr
        {
            get { return _totalMissingOcr; }
            set { _totalMissingOcr = value; }
        }

        private double? _aveMinutesToGenerate;

        public double? AveMinutesToGenerate
        {
            get { return _aveMinutesToGenerate; }
            set { _aveMinutesToGenerate = value; }
        }

        private int? _totalMinutesToGenerate;

        public int? TotalMinutesToGenerate
        {
            get { return _totalMinutesToGenerate; }
            set { _totalMinutesToGenerate = value; }
        }
    }
}
