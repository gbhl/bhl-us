using System;
using Newtonsoft.Json;

namespace BHLApi3Web.Models
{
    [Serializable]
    public class PageNumber : DataObjectBase
    {
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public PageNumber()
		{
		}

        public PageNumber(string prefix, string number)
        {
            _prefix = prefix;
            _number = number;
        }

		#endregion Constructors
		
        #region Properties

        [JsonProperty("prefix")]
        private string _prefix = null;

        [JsonIgnore]
        public string Prefix
        {
            get { return _prefix; }
            set { _prefix = value; }
        }

        [JsonProperty("number")]
        private string _number = null;

        [JsonIgnore]
        public string Number
        {
            get { return _number; }
            set { _number = value; }
        }

        #endregion Properties

    }
}
