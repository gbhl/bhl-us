using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageDetailHarvest
{
    internal class PageDetail
    {
        private string _id;

        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private bool _processingComplete;

        public bool ProcessingComplete
        {
            get { return _processingComplete; }
            set { _processingComplete = value; }
        }

        private bool _processingError;

        public bool ProcessingError
        {
            get { return _processingError; }
            set { _processingError = value; }
        }

        private string _barcode;

        public string Barcode
        {
            get { return _barcode; }
            set { _barcode = value; }
        }

        private int _pageID;

        public int PageID
        {
            get { return _pageID; }
            set { _pageID = value; }
        }

        private int _height = 0;

        public int Height
        {
            get { return _height; }
            set { _height = value; }
        }

        private int _width = 0;

        public int Width
        {
            get { return _width; }
            set { _width = value; }
        }

        private int _pixelDepth = 0;

        public int PixelDepth
        {
            get { return _pixelDepth; }
            set { _pixelDepth = value; }
        }

        private bool _contrastHasImage = false;

        public bool ContrastHasImage
        {
            get { return _contrastHasImage; }
            set { _contrastHasImage = value; }
        }

        private bool _abbyHasImage = false;

        public bool AbbyHasImage
        {
            get { return _abbyHasImage; }
            set { _abbyHasImage = value; }
        }

        private double _percentCoverage = 0.0;

        public double PercentCoverage
        {
            get { return _percentCoverage; }
            set { _percentCoverage = value; }
        }

        private List<PageIllustration> _illustrations = new List<PageIllustration>();

        internal List<PageIllustration> Illustrations
        {
            get { return _illustrations; }
            set { _illustrations = value; }
        }

        private PageDetailStatus _pageDetailStatus = PageDetailStatus.Extracted;

        internal PageDetailStatus PageDetailStatus
        {
            get { return _pageDetailStatus; }
            set { _pageDetailStatus = value; }
        }
    }

    internal enum PageDetailStatus
    {
        Extracted = 10,
        Classifying = 20,
        Classified = 30,
        Describing = 40,
        Described = 50,
        NoImageFound = 60
    }
}
