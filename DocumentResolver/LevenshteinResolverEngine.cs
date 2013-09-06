using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MOBOT.BHL.DocumentResolver
{
    public class LevenshteinResolverEngine : ResolverEngineBase
    {
        private int _matchThreshold = 0;   // Minimum number of allowed differences to be considered a definite match

        public override List<ResolutionResult> Resolve(string document, bool useWordStemmer = false)
        {
            Dictionary<int, string> documents = new Dictionary<int, string>();
            List<ResolutionResult> resolutionResults = new List<ResolutionResult>();

            try
            {
                if (string.IsNullOrWhiteSpace(document)) throw new Exception("Please supply a document to resolve.");
                if (this.Dictionary.Count == 0) throw new Exception("Please supply a dictionary to resolve the document against.");
                _dictionaryDirty = false;

                // Evaluate the string against all of the data entries
                List<ResolutionResult> results = new List<ResolutionResult>();
                Levenshtein lv = new Levenshtein();

                foreach (KeyValuePair<string, string> kvp in Dictionary)
                {
                    string dictionaryItem = kvp.Value;

                    if (useWordStemmer)
                    {
                        // Remove noise words and reduce the remaining words to their root forms
                        Tokeniser tokeniser = new Tokeniser();
                        document = string.Join(" ", tokeniser.Partition(document, new StopWordsHandler(), true).ToArray());
                        dictionaryItem = string.Join(" ", tokeniser.Partition(dictionaryItem, new StopWordsHandler(), true).ToArray());
                    }

                    int score = lv.GetDistance(document, dictionaryItem);

                    // At least something matched
                    ResolutionResult result = new ResolutionResult();
                    result.Key = kvp.Key;
                    result.Document = kvp.Value;
                    result.Score = score;
                    results.Add(result);
                }

                // Sort the results
                var sortedResults = from result in results orderby result.Score ascending select result;

                // Indicate the matches in the list of results
                foreach (ResolutionResult resolutionResult in sortedResults)
                {
                    if (resolutionResult.Score <= _matchThreshold) resolutionResult.Match = true; 
                    resolutionResults.Add(resolutionResult);
                }
            }
            catch
            {
                throw;
            }

            return resolutionResults;
        }
    }
}
