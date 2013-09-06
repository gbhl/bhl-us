using System;
using Newtonsoft.Json;

namespace BHLApi3Web.Models
{
    [Serializable]
    public class ResolutionResult : DataObjectBase
    {
        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ResolutionResult()
        {
        }

        #endregion Constructors

        #region Properties

        [JsonProperty("type")]
        private string _type;

        [JsonIgnore]
        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }

        [JsonProperty("id")]
        private string _key;

        [JsonIgnore]
        public string Key
        {
            get { return _key; }
            set { _key = value; }
        }

        [JsonProperty("details")]
        private string _document = string.Empty;

        [JsonIgnore]
        public string Document
        {
            get { return _document; }
            set { _document = value; }
        }

        [JsonProperty("score")]
        private double _score;

        [JsonIgnore]
        public double Score
        {
            get { return _score; }
            set { _score = value; }
        }

        #endregion Properties
    }
}