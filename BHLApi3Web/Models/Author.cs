using System;
using Newtonsoft.Json;

namespace BHLApi3Web.Models
{
    [Serializable]
    public class Author : DataObjectBase
    {
        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Author()
        {
        }

        #endregion Constructors

        #region Properties

        [JsonProperty("id")]
        private int _creatorID = default(int);

        [JsonIgnore]
        public int CreatorID
        {
            get { return _creatorID; }
            set { _creatorID = value; }
        }

        [JsonProperty("fullname")]
        private string _name = null;

        [JsonIgnore]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        [JsonProperty("type")]
        private string _type = null;

        [JsonIgnore]
        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }

        // Applies to personal names
        [JsonProperty("numeration")]
        private string _numeration = null;

        [JsonIgnore]
        public string Numeration
        {
            get { return _numeration; }
            set { _numeration = value; }
        }

        // Applies to corporate authors
        [JsonProperty("unit")]
        private string _unit = null;

        [JsonIgnore]
        public string Unit
        {
            get { return _unit; }
            set { _unit = value; }
        }

        // Applies to personal names
        [JsonProperty("title")]
        private string _title = null;

        [JsonIgnore]
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        // Applies to corporate authors
        [JsonProperty("location")]
        private string _location = null;

        [JsonIgnore]
        public string Location
        {
            get { return _location; }
            set { _location = value; }
        }

        [JsonProperty("fullerform")]
        private string _fullerForm = null;

        [JsonIgnore]
        public string FullerForm
        {
            get { return _fullerForm; }
            set { _fullerForm = value; }
        }

        [JsonProperty("dates")]
        private string _dates = null;

        [JsonIgnore]
        public string Dates
        {
            get { return _dates; }
            set { _dates = value; }
        }

        #endregion Properties

    }
}
