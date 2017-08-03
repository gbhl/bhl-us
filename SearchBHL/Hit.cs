using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHL.Search
{
    public class Hit : IHit
    {
        /// <summary>
        /// Score of the Hit returned by the search engine
        /// </summary>
        private double? _score = 0;

        /// <summary>
        /// Identifier of the record delivered by the hit
        /// </summary>
        private string _id = string.Empty;

        /// <summary>
        /// List of field/value pairs.  Each pair contains the field in which the highlight 
        /// was found, as well as the value of the field (including the highlight)
        /// </summary>
        private List<Tuple<string, string>> _highlights = new List<Tuple<string, string>>();

        public double? Score
        {
            get { return _score; }
            set { _score = value; }
        }

        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public List<Tuple<string, string>> Highlights
        {
            get { return _highlights; }
            set { _highlights = value; }
        }
    }
}
