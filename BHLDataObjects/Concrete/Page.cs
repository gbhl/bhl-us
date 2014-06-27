using System;
using CustomDataAccess;


namespace MOBOT.BHL.DataObjects
{
	[Serializable]
	public class Page : __Page
	{
        private int _pageID;
	    public new int PageID
        {
            get
            {
                if (_pageID == 0)
                {
                    _pageID = base.PageID;
                }
                return _pageID;
            }
            set
            {
                _pageID = value;
            }
        }

        private string _indicatedPages = string.Empty;
        public string IndicatedPages
        {
            get
            {
                return _indicatedPages;
            }
            set
            {
                if (value != null) value = value.Trim().PadRight(1000).Substring(0, 1000).Trim();
                if (_indicatedPages != value)
                {
                    _indicatedPages = value;
                }
            }
        }

        private string _pageTypes = string.Empty;
        public string PageTypes
        {
            get
            {
                return _pageTypes;
            }
            set
            {
                if (value != null) value = value.Trim().PadRight(1000).Substring(0, 1000).Trim();
                if (_pageTypes != value)
                {
                    _pageTypes = value;
                }
            }
        }

        private string _folderShare = string.Empty;
        public string FolderShare
        {
            get
            {
                return _folderShare;
            }
            set
            {
                if (value != null) value = value.Trim().PadRight(30).Substring(0, 30).Trim();
                if (_folderShare != value)
                {
                    _folderShare = value;
                }
            }
        }

        private string _webVirtualDirectory = string.Empty;
        public string WebVirtualDirectory
        {
            get
            {
                return _webVirtualDirectory;
            }
            set
            {
                if (value != null) value = value.Trim().PadRight(30).Substring(0, 30).Trim();
                if (_webVirtualDirectory != value)
                {
                    _webVirtualDirectory = value;
                }
            }
        }

        private string _barCode = string.Empty;
        public string BarCode
        {
            get
            {
                return _barCode;
            }
            set
            {
                if (value != null) value = value.Trim().PadRight(20).Substring(0, 20).Trim();
                if (_barCode != value)
                {
                    _barCode = value;
                }
            }
        }

        private string _ocrFolderShare = string.Empty;

        public string OcrFolderShare
        {
            get { return _ocrFolderShare; }
            set { _ocrFolderShare = value; }
        }

        private string _fileRootFolder = string.Empty;

        public string FileRootFolder
        {
            get { return _fileRootFolder; }
            set { _fileRootFolder = value; }
        }

        private string _marcBibID = string.Empty;
        public string MARCBibID
        {
            get
            {
                return _marcBibID;
            }
            set
            {
                if (value != null) value = value.Trim().PadRight(20).Substring(0, 20).Trim();
                if (_marcBibID != value)
                {
                    _marcBibID = value;
                }
            }
        }

        private string _shortTitle = string.Empty;

        public string ShortTitle
        {
            get { return _shortTitle; }
            set { _shortTitle = value; }
        }

        private bool _rareBooks = false;
        public bool RareBooks
        {
            get
            {
                return _rareBooks;
            }
            set
            {
                _rareBooks = value;
            }
        }

        private int? _segmentID;
        public int? SegmentID
        {
            get { return _segmentID; }
            set { _segmentID = value; }
        }

        //private bool _illustration = false;
        //public bool Illustration
        //{
        //    get
        //    {
        //        return _illustration;
        //    }
        //    set
        //    {
        //        _illustration = value;
        //    }
        //}

        //private string _externalURL = string.Empty;
        //public string ExternalURL
        //{
        //    get
        //    {
        //        return _externalURL;
        //    }
        //    set
        //    {
        //        _externalURL = value;
        //    }
        //}

        public string WebDisplay
        {
            get
            {
                string returnValue = string.Empty;
                string volumeInfo = string.Empty;

                string pageNums = this.IndicatedPages;

                string pageTypes = this.PageTypes;
                pageTypes = pageTypes.Replace("Text, ", "");
                pageTypes = pageTypes.Replace(", Text", "");

                if (pageTypes.Contains("Issue Start"))
                {
                    // Get the volume and issue strings
                    string year = (this.Year ?? string.Empty);
                    string volume = ((this.Volume ?? string.Empty) == string.Empty) ? string.Empty : "v." + this.Volume;
                    string issue = ((this.IssuePrefix ?? string.Empty) + " " + (this.Issue ?? string.Empty)).Trim();

                    // Build the year/volume/issue display string
                    System.Collections.Generic.List<string> displayElements = new System.Collections.Generic.List<string>();
                    if (year != string.Empty) displayElements.Add(year);
                    if (volume != string.Empty) displayElements.Add(volume);
                    if (issue != string.Empty) displayElements.Add(issue);
                    volumeInfo = string.Join(", ", displayElements.ToArray());
                }

                if (!string.IsNullOrWhiteSpace(pageTypes) && !string.IsNullOrWhiteSpace(pageNums)) pageTypes = "(" + pageTypes + ")";
                if (!string.IsNullOrWhiteSpace(volumeInfo)) volumeInfo = "<" + volumeInfo + ">";
                returnValue = string.Format("{0} {1} {2}", pageNums, pageTypes, volumeInfo).Trim();

                if (returnValue.Length == 0)
                {
                    returnValue = "Seq " + this.SequenceOrder.ToString();
                }
                return returnValue;
            }
            set
            {
            }
        }

        public string FlickrURL { get; set; }

        public override void SetValues(CustomDataRow row)
        {

            foreach (CustomDataColumn column in row)
            {
                switch (column.Name)
                {
                    case "IndicatedPages":
                        {
                            _indicatedPages = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "PageTypes":
                        {
                            _pageTypes = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "FolderShare":
                        {
                            _folderShare = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "WebVirtualDirectory":
                        {
                            _webVirtualDirectory = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "BarCode":
                        {
                            _barCode = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "OcrFolderShare":
                        {
                            _ocrFolderShare = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "FileRootFolder":
                        {
                            _fileRootFolder = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "MARCBibID":
                        {
                            _marcBibID = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "ShortTitle":
                        {
                            _shortTitle = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "RareBooks":
                        {
                            _rareBooks = Utility.FalseIfNull(column.Value);
                            break;
                        }
                    //case "Illustration":
                    //    {
                    //        _illustration = Utility.FalseIfNull(column.Value);
                    //        break;
                    //    }
                    //case "ExternalURL":
                    //    {
                    //        _externalURL = Utility.EmptyIfNull(column.Value);
                    //        break;
                    //    }
                    case "FlickrURL":
                        FlickrURL = Utility.EmptyIfNull(column.Value);
                        break;
                    case "SegmentID":
                        SegmentID = (int?)column.Value;
                        break;
                }
            }

            base.SetValues(row);

        }



    }
}
