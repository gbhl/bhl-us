using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace BHLApi3Web.Models
{
    [Serializable]
    public class Name : DataObjectBase
    {
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public Name()
		{
		}

		#endregion Constructors

        #region Properties

        [JsonProperty("namefound")]
        private string _nameFound = null;

        [JsonIgnore]
        public string NameFound
        {
            get { return _nameFound; }
            set { _nameFound = value; }
        }

        [JsonProperty("nameconfirmed")]
        private string _nameConfirmed = null;

        [JsonIgnore]
        public string NameConfirmed
        {
            get { return _nameConfirmed; }
            set { _nameConfirmed = value; }
        }

        [JsonProperty("canonicalname")]
        private string _canonicalName = null;

        [JsonIgnore]
        public string CanonicalName
        {
            get { return _canonicalName; }
            set { _canonicalName = value; }
        }

        [JsonProperty("identifiers")]
        private IEnumerable<Identifier> _identifiers;

        [JsonIgnore]
        public IEnumerable<Identifier> Identifiers
        {
            get { return _identifiers; }
            set { _identifiers = value; }
        }

        #endregion Properties
		
    }
}
