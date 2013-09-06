using System;
using Newtonsoft.Json;

namespace BHLApi3Web.Models
{
    [Serializable]
    public class Identifier : DataObjectBase
    {
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public Identifier()
		{
		}

		#endregion Constructors
		
		#region Properties

        [JsonProperty("type")]
        private string _identifierName = null;

        [JsonIgnore]
        public string IdentifierName
        {
            get { return _identifierName; }
            set { _identifierName = value; }
        }

        [JsonProperty("value")]
        private string _identifierValue = null;

        [JsonIgnore]
        public string IdentifierValue
        {
            get { return _identifierValue; }
            set { _identifierValue = value; }
        }

        [JsonProperty("relationshiptype")]
        private string _relationshipType = null;

        [JsonIgnore]
        public string RelationshipType
        {
            get { return _relationshipType; }
            set { _relationshipType = value; }
        }

        #endregion

    }
}
