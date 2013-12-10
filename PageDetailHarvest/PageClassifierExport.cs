using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageDetailHarvest
{
    internal class PageClassifierExport
    {
        private int _pageDetailID;

        public int PageDetailID
        {
            get { return _pageDetailID; }
            set { _pageDetailID = value; }
        }

        private int _itemID;

        public int ItemID
        {
            get { return _itemID; }
            set { _itemID = value; }
        }

        private string _barCode;

        public string BarCode
        {
            get { return _barCode; }
            set { _barCode = value; }
        }

        private int _pageID;

        public int PageID
        {
            get { return _pageID; }
            set { _pageID = value; }
        }

        private int _sequenceOrder;

        public int SequenceOrder
        {
            get { return _sequenceOrder; }
            set { _sequenceOrder = value; }
        }

        private int _abbyyHasImage;

        public int AbbyyHasImage
        {
            get { return _abbyyHasImage; }
            set { _abbyyHasImage = value; }
        }

        private int _contrastHasImage;

        public int ContrastHasImage
        {
            get { return _contrastHasImage; }
            set { _contrastHasImage = value; }
        }

        private double _percentCoverage;

        public double PercentCoverage
        {
            get { return _percentCoverage; }
            set { _percentCoverage = value; }
        }

        private int _height;

        public int Height
        {
            get { return _height; }
            set { _height = value; }
        }

        private int _width;

        public int Width
        {
            get { return _width; }
            set { _width = value; }
        }

        private int _pixelDepth;

        public int PixelDepth
        {
            get { return _pixelDepth; }
            set { _pixelDepth = value; }
        }

        private int? _top;

        public int? Top
        {
            get { return _top; }
            set { _top = value; }
        }

        private int? _bottom;

        public int? Bottom
        {
            get { return _bottom; }
            set { _bottom = value; }
        }

        private int? _left;

        public int? Left
        {
            get { return _left; }
            set { _left = value; }
        }

        private int? _right;

        public int? Right
        {
            get { return _right; }
            set { _right = value; }
        }

        private string _authors;

        public string Authors
        {
            get { return _authors; }
            set { _authors = value; }
        }

        private string _shortTitle;

        public string ShortTitle
        {
            get { return _shortTitle; }
            set { _shortTitle = value; }
        }

        private string _publicationDetails;

        public string PublicationDetails
        {
            get { return _publicationDetails; }
            set { _publicationDetails = value; }
        }

        private string _volume;

        public string Volume
        {
            get { return _volume; }
            set { _volume = value; }
        }

        private string _copyrightStatus;

        public string CopyrightStatus
        {
            get { return _copyrightStatus; }
            set { _copyrightStatus = value; }
        }

        private string _pageYear;

        public string PageYear
        {
            get { return _pageYear; }
            set { _pageYear = value; }
        }

        private string _itemYear;

        public string ItemYear
        {
            get { return _itemYear; }
            set { _itemYear = value; }
        }

        private int _startYear;

        public int StartYear
        {
            get { return _startYear; }
            set { _startYear = value; }
        }

        private string _keywords;

        public string Keywords
        {
            get { return _keywords; }
            set { _keywords = value; }
        }

        private string _institutionName;

        public string InstitutionName
        {
            get { return _institutionName; }
            set { _institutionName = value; }
        }

        private int _bhlMemberLibrary;

        public int BhlMemberLibrary
        {
            get { return _bhlMemberLibrary; }
            set { _bhlMemberLibrary = value; }
        }
    }
}
