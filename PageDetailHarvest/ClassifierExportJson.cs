using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageDetailHarvest
{
    [Serializable]
    internal class ClassifierExportJson
    {
        [JsonProperty("items")]
        private List<ClassifierExportItem> _items = new List<ClassifierExportItem>();

        [JsonIgnore]
        internal List<ClassifierExportItem> Items
        {
            get { return _items; }
            set { _items = value; }
        }
    }

    [Serializable]
    internal class ClassifierExportItem
    {
        [JsonProperty("itemid")]
        private string _itemUrl;

        [JsonIgnore]
        public string ItemUrl
        {
            get { return _itemUrl; }
            set { _itemUrl = value; }
        }

        [JsonProperty("barcode")]
        private string _barcode;

        [JsonIgnore]
        public string Barcode
        {
            get { return _barcode; }
            set { _barcode = value; }
        }

        [JsonProperty("author")]
        private string _author = string.Empty;

        [JsonIgnore]
        public string Author
        {
            get { return _author; }
            set { _author = value; }
        }

        [JsonProperty("title")]
        private string _title = string.Empty;

        [JsonIgnore]
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        [JsonProperty("volume")]
        private string _volume = string.Empty;

        [JsonIgnore]
        public string Volume
        {
            get { return _volume; }
            set { _volume = value; }
        }

        [JsonProperty("publication_details")]
        private string _publicationDetails = string.Empty;

        [JsonIgnore]
        public string PublicationDetails
        {
            get { return _publicationDetails; }
            set { _publicationDetails = value; }
        }

        [JsonProperty("copyright")]
        private string _copyright = string.Empty;

        [JsonIgnore]
        public string Copyright
        {
            get { return _copyright; }
            set { _copyright = value; }
        }

        [JsonProperty("date")]
        private string _date = string.Empty;

        [JsonIgnore]
        public string Date
        {
            get { return _date; }
            set { _date = value; }
        }

        [JsonProperty("subjects")]
        private List<string> _subjects = new List<string>();

        [JsonIgnore]
        public List<string> Subjects
        {
            get { return _subjects; }
            set { _subjects = value; }
        }

        [JsonProperty("contributor")]
        private ClassifierExportContributor _contributor = new ClassifierExportContributor();

        [JsonIgnore]
        internal ClassifierExportContributor Contributor
        {
            get { return _contributor; }
            set { _contributor = value; }
        }

        [JsonProperty("pages")]
        private List<ClassifierExportPage> _pages = new List<ClassifierExportPage>();

        [JsonIgnore]
        internal List<ClassifierExportPage> Pages
        {
            get { return _pages; }
            set { _pages = value; }
        }
    }

    [Serializable]
    internal class ClassifierExportPage
    {
        [JsonProperty("pageid")]
        private string _pageUrl;

        [JsonIgnore]
        public string PageUrl
        {
            get { return _pageUrl; }
            set { _pageUrl = value; }
        }

        [JsonProperty("sequence_order")]
        private int _sequenceOrder;

        [JsonIgnore]
        public int SequenceOrder
        {
            get { return _sequenceOrder; }
            set { _sequenceOrder = value; }
        }

        [JsonProperty("abbyy_hasillustration")]
        private bool _abbyyHasIllustration = false;

        [JsonIgnore]
        public bool AbbyyHasIllustration
        {
            get { return _abbyyHasIllustration; }
            set { _abbyyHasIllustration = value; }
        }

        [JsonProperty("contrast_hasillustration")]
        private bool _contrastHasIllustration = false;

        [JsonIgnore]
        public bool ContrastHasIllustration
        {
            get { return _contrastHasIllustration; }
            set { _contrastHasIllustration = value; }
        }

        [JsonProperty("height")]
        private int _height = 0;

        [JsonIgnore]
        public int Height
        {
            get { return _height; }
            set { _height = value; }
        }

        [JsonProperty("width")]
        private int _width = 0;

        [JsonIgnore]
        public int Width
        {
            get { return _width; }
            set { _width = value; }
        }

        /*
        [JsonProperty("pixel_depth")]
        private int _pixelDepth = 0;

        [JsonIgnore]
        public int PixelDepth
        {
            get { return _pixelDepth; }
            set { _pixelDepth = value; }
        }
         */

        [JsonProperty("percent_coverage")]
        private double _percentCoverage = 0;

        [JsonIgnore]
        public double PercentCoverage
        {
            get { return _percentCoverage; }
            set { _percentCoverage = value; }
        }

        [JsonProperty("illustrations")]
        private List<ClassifierExportIllustration> _illustrations = new List<ClassifierExportIllustration>();

        [JsonIgnore]
        internal List<ClassifierExportIllustration> Illustrations
        {
            get { return _illustrations; }
            set { _illustrations = value; }
        }        
    }

    [Serializable]
    internal class ClassifierExportContributor
    {
        [JsonProperty("contributing_library")]
        private string _contributingLibrary = string.Empty;

        [JsonIgnore]
        public string ContributingLibrary
        {
            get { return _contributingLibrary; }
            set { _contributingLibrary = value; }
        }

        [JsonProperty("is_member_library")]
        private bool _memberLibrary = false;

        [JsonIgnore]
        public bool MemberLibrary
        {
            get { return _memberLibrary; }
            set { _memberLibrary = value; }
        }
    }

    [Serializable]
    internal class ClassifierExportIllustration
    {
        [JsonProperty("top")]
        private int _top;

        [JsonIgnore]
        public int Top
        {
            get { return _top; }
            set { _top = value; }
        }

        [JsonProperty("bottom")]
        private int _bottom;

        [JsonIgnore]
        public int Bottom
        {
            get { return _bottom; }
            set { _bottom = value; }
        }

        [JsonProperty("left")]
        private int _left;

        [JsonIgnore]
        public int Left
        {
            get { return _left; }
            set { _left = value; }
        }

        [JsonProperty("right")]
        private int _right;

        [JsonIgnore]
        public int Right
        {
            get { return _right; }
            set { _right = value; }
        }
    }
}
