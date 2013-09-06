using System;
using Newtonsoft.Json;

namespace BHLApi3Web.Models
{
    [Serializable]
    public class Subject : DataObjectBase
    {
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public Subject()
		{
		}

		#endregion Constructors
		
		#region Properties

        [JsonProperty("keyword")]
        private string _subjectText = null;

        [JsonIgnore]
        public string SubjectText
        {
          get { return _subjectText; }
          set { _subjectText = value; }
        }

        #endregion

    }
}
