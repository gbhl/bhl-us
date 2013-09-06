using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace BHLApi3Web.Models
{
    [Serializable]
    public class Page : DataObjectBase
    {
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public Page()
		{
		}

		#endregion Constructors
		
		#region Properties

        [JsonProperty("id")]
        private int _pageID = default(int);

        [JsonIgnore]
        public int PageID
        {
            get { return _pageID; }
            set { _pageID = value; }
        }

        [JsonProperty("itemid")]
        private int _itemID = default(int);

        [JsonIgnore]
        public int ItemID
        {
            get { return _itemID; }
            set { _itemID = value; }
        }

        [JsonProperty("volume")]
        private string _volume = null;

        [JsonIgnore]
        public string Volume
        {
            get { return _volume; }
            set { _volume = value; }
        }

        [JsonProperty("issue")]
        private string _issue = null;

        [JsonIgnore]
        public string Issue
        {
            get { return _issue; }
            set { _issue = value; }
        }

        [JsonProperty("year")]
        private string _year = null;

        [JsonIgnore]
        public string Year
        {
            get { return _year; }
            set { _year = value; }
        }

        [JsonProperty("url")]
        private string _pageUrl = null;

        [JsonIgnore]
        public string PageUrl
        {
            get { return _pageUrl; }
            set { _pageUrl = value; }
        }

        [JsonProperty("thumburl")]
        private string _thumbnailUrl = null;

        [JsonIgnore]
        public string ThumbnailUrl
        {
            get { return _thumbnailUrl; }
            set { _thumbnailUrl = value; }
        }

        [JsonProperty("imageurl")]
        private string _fullSizeImageUrl = null;

        [JsonIgnore]
        public string FullSizeImageUrl
        {
            get { return _fullSizeImageUrl; }
            set { _fullSizeImageUrl = value; }
        }

        [JsonProperty("ocrurl")]
        private string _ocrUrl = null;

        [JsonIgnore]
        public string OcrUrl
        {
            get { return _ocrUrl; }
            set { _ocrUrl = value; }
        }

        [JsonProperty("ocrtext")]
        private string _ocrText = null;

        [JsonIgnore]
        public string OcrText
        {
            get { return _ocrText; }
            set { _ocrText = value; }
        }

        [JsonProperty("types")]
        IEnumerable<PageType> _pageTypes = null;

        [JsonIgnore]
        public IEnumerable<PageType> PageTypes
        {
            get { return _pageTypes; }
            set { _pageTypes = value; }
        }

        [JsonProperty("numbers")]
        IEnumerable<PageNumber> _pageNumbers = null;

        [JsonIgnore]
        public IEnumerable<PageNumber> PageNumbers
        {
            get { return _pageNumbers; }
            set { _pageNumbers = value; }
        }

        [JsonProperty("names")]
        IEnumerable<Name> _names = null;

        [JsonIgnore]
        public IEnumerable<Name> Names
        {
            get { return _names; }
            set { _names = value; }
        }

		#endregion Properties

    }
}
