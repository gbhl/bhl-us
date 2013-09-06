using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace MOBOT.BHL.DocumentResolver
{
    internal interface IResolverEngine
    {
        /// <summary>
        /// Dictionary of strings to resolve against.
        /// </summary>
        Dictionary<string, string> Dictionary { get; set; }

        /// <summary>
        /// Resolve the specified document against the dictionary, populating the Matches, PossibleMatches, 
        /// and AllResults dictionaries with the results.
        /// </summary>
        /// <param name="text"></param>
        List<ResolutionResult> Resolve(string document, bool useWordStemmer = false);
    }
}
