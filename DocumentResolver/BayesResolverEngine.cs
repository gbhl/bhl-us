using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace MOBOT.BHL.DocumentResolver
{
    public class BayesResolverEngine : ResolverEngineBase
    {
        BayesClassifier _bc = null;

        public override List<ResolutionResult> Resolve(string document, bool useWordStemmer = false)
        {
            Dictionary<int, string> documents = new Dictionary<int, string>();
            List<ResolutionResult> resolutionResults = new List<ResolutionResult>();

            try
            {
                if (string.IsNullOrWhiteSpace(document)) throw new Exception("Please supply a document to resolve.");
                if (this.Dictionary.Count == 0) throw new Exception("Please supply a dictionary to resolve the document against.");

                // Initialize the resolver and load the dictionary data into it
                if (_dictionaryDirty) _bc = InitializeResolver(useWordStemmer);

                // Evaluate the string against all of the data entries
                List<ResolutionResult> results = new List<ResolutionResult>();
                Dictionary<string, double> score = _bc.Classify(new System.IO.StreamReader(new System.IO.MemoryStream(Encoding.Default.GetBytes(document))), useWordStemmer);
                foreach (string c in score.Keys)
                {
                    if (!double.IsNaN(score[c]))
                    {
                        // Get all of the results
                        ResolutionResult result = new ResolutionResult();
                        result.Score = score[c];
                        result.Key = c;
                        result.Document = this.Dictionary[c];
                        results.Add(result);
                    }
                }

                // Calculate the standard deviation for the results
                if (results.Count() > 0)
                {
                    double average = (from r in results where double.IsNaN(r.Score) != true select r.Score).Average();
                    double sumOfSquaresOfDifferences = (from r in results where double.IsNaN(r.Score) != true select ((r.Score - average) * (r.Score - average))).Sum();
                    double stddev = Math.Sqrt(sumOfSquaresOfDifferences / results.Count);

                    // Determine the minimum score that we will accept as a potential match
                    double minScore = average + (stddev * 3);   // triple the std dev to find the min score (otherwise not enough excluded);

                    // Exclude all results except for those that have a higher score than the standard deviation
                    List<ResolutionResult> limitedResults = new List<ResolutionResult>();
                    foreach (ResolutionResult result in results)
                    {
                        if (result.Score > minScore) limitedResults.Add(result);
                    }

                    // Sort the remaining results
                    var sortedResults = (from result in limitedResults orderby result.Score descending select result);

                    resolutionResults = sortedResults.ToList<ResolutionResult>();
                }
            }
            catch
            {
                throw;
            }

            return resolutionResults;
        }

        private BayesClassifier InitializeResolver(bool useWordStemmer)
        {
            BayesClassifier classifier = new BayesClassifier();
            Dictionary<string, string[]> phrases = new Dictionary<string, string[]>();

            foreach (KeyValuePair<string, string> kvp in this.Dictionary)
            {
                string[] documentInfo = new Tokeniser().Partition(
                    kvp.Value, new StopWordsHandler(), false).ToArray();
                classifier.TeachPhrases(kvp.Key.ToString(), documentInfo, useWordStemmer);
            }

            _dictionaryDirty = false;

            return classifier;
        }
    }
}
