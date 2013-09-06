using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace MOBOT.BHL.DocumentResolver
{
    public class TFIDFResolverEngine : ResolverEngineBase
    {
        private double _matchThreshold = 0.9999;

        public override List<ResolutionResult> Resolve(string document, bool useWordStemmer = false)
        {
            Dictionary<string, string> documents = new Dictionary<string, string>();
            List<ResolutionResult> resolutionResults = new List<ResolutionResult>();

            try
            {
                if (string.IsNullOrWhiteSpace(document)) throw new Exception("Please supply a document to resolve.");
                if (this.Dictionary.Count == 0) throw new Exception("Please supply a dictionary to resolve the document against.");

                // Load the data to evaluate the document against
                documents.Add("-1", document);
                foreach (KeyValuePair<string, string> kvp in this.Dictionary)
                {
                    documents.Add(kvp.Key, kvp.Value);
                }
                _dictionaryDirty = false;

                // Evaluate the string against all of the data entries
                List<ResolutionResult> results = new List<ResolutionResult>();
                TFIDFMeasure tf = new TFIDFMeasure(documents, useWordStemmer);
                int x = 0;
                foreach(KeyValuePair<string, string> kvp in documents)
                {
                    if (x > 0)  // Skip the first element of the collection
                    {
                        double similarity = tf.GetSimilarity(0, x);
                        if (similarity > 0)
                        {
                            // At least something matched
                            ResolutionResult result = new ResolutionResult();
                            result.Key = kvp.Key;
                            result.Document = kvp.Value;
                            result.Score = similarity;
                            results.Add(result);
                        }
                    }
                    x++;
                }

                // Sort the results
                var sortedResults = from result in results orderby result.Score descending select result;
                
                // Indicate the matches in the list of results
                foreach (ResolutionResult resolutionResult in sortedResults)
                {
                    if (resolutionResult.Score >= _matchThreshold) resolutionResult.Match = true;
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
