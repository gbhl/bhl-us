using System;
using System.Collections.Generic;

namespace BHL.Search
{
    public interface IHit
    {
        /// <summary>
        /// Score of the Hit, as returned by the search engine
        /// </summary>
        double? Score { get; set; }

        /// <summary>
        /// Search engine identifier of the record delivered by the hit
        /// </summary>
        string Id { get; set; }

        /// <summary>
        /// List of field/value pairs.  Each pair contains the field in which the highlight 
        /// was found, as well as the value of the field (including the highlight)
        /// </summary>
        List<Tuple<string, string>> Highlights { get; set; }
    }
}
