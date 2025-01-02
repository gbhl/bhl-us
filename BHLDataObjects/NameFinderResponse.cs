using System;
using System.Collections.Generic;

namespace MOBOT.BHL.DataObjects
{
    [Serializable]
	public class NameFinderResponse
	{
        public NameFinderResponse()
		{	}

		#region Private variables

		private string _name = string.Empty;
        private string _nameResolved = string.Empty;
		private string _canonicalName = string.Empty;
		private string _matchType = string.Empty;
		private string _curation = string.Empty;
        private List<string> _identifiers = new List<string>();

		#endregion

		#region Public Properties

		public string Name
		{
			get { return this._name; }
			set { this._name = value; }
		}

        public string NameResolved
        {
            get { return this._nameResolved; }
            set { this._nameResolved = value; }
        }

		public string CanonicalName
		{
			get { return this._canonicalName; }
			set { this._canonicalName = value; }
		}

		public string MatchType
        {
			get { return this._matchType; }
			set { this._matchType = value; }
        }

		public string Curation
        {
			get { return this._curation; }
			set { this._curation = value; }
        }

        public List<string> Identifiers
        {
            get { return this._identifiers; }
            set { this._identifiers = value; }
        }

		#endregion
	}
}
