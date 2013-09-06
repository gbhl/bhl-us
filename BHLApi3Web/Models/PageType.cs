using System;
using Newtonsoft.Json;

namespace BHLApi3Web.Models
{
    [Serializable]
    public class PageType : DataObjectBase
    {
        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public PageType()
        {
        }

        public PageType(string pageTypeName)
        {
            _pageTypeName = pageTypeName;
        }

        #endregion Constructors

        #region Properties

        [JsonProperty("type")]
        private string _pageTypeName = null;

        [JsonIgnore]
        public string PageTypeName
        {
            get { return _pageTypeName; }
            set { _pageTypeName = value; }
        }

        #endregion Properties

    }
}
