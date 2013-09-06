using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOBOT.BHL.DocumentResolver
{
    /// <summary>
    /// DocumentResolver is a class that can be used to resolve a single document (string) 
    /// against a dictionary of similar documents (strings).  The end result is a list of 
    /// documents that may match the document being resolved, including scores that indicate 
    /// the likelyhood of a match.
    /// </summary>
    /// <remarks>
    /// During testing it was determined that the TFIDF (Term Frequence - Inverse Document Frequency)
    /// and Levenshtein resolver engines produced clearer, more easily intepreted results than the 
    /// Bayes resolver engine.
    /// 
    /// However, when processing large numbers of documents that contain large numbers of unique terms 
    /// (tens of thousands of each), the TFIDF algorithm's performance is too poor to be practical.
    /// Resolution of one document against a dictionary of 90000+ documents containing 90000+ unique
    /// terms took over an hour.
    /// 
    /// Similarly, the Levenshtein algorithm, while much faster than the TFIDF algorithm, is still
    /// too slow to be practical when resolving a document against a large dictionary (such as one 
    /// containing tens of thousands of entries).  Resolution of one document against a 90000+ 
    /// document dictionary took between three and four minutes.  This is too long when considering
    /// that the resolution operation may be invoked from services that require real-time responses.
    /// 
    /// The Bayes resolver, on the other hand, was able to resolve a single document against the same
    /// document dictionary in less than five seconds.
    /// 
    /// Resolution against much smaller dictionaries with the TFIDF and Levenshtein resolvers produced 
    /// results in a second or two.
    /// 
    /// So, the decision was made to process a document to be resolved against the Bayes resolver to
    /// identify potential matches, and then to process only those candidates with either the TFIDF or
    /// Levenshtein resolver (whichever was selected by the user) to better identify the likely matches.
    /// By using the Bayes resolver to feed the secondary resolver, we are able to quickly resolve 
    /// against a large number of documents, and to produce easily interpreted results.
    /// 
    /// The TFIDF resolver is the default secondary resolver.
    /// </remarks>
    public class DocumentResolver
    {
        private IResolverEngine _bayesEngine = new BayesResolverEngine();
        private IResolverEngine _secondaryEngine = new TFIDFResolverEngine();
        private Dictionary<string, string> _dictionary = new Dictionary<string, string>();
        protected bool _dictionaryDirty = false;

        /// <summary>
        /// Defines the resolver engines that can be used.
        /// </summary>
        public enum EngineType
        {
            BayesTFIDF,
            BayesLevenshtein
        }

        #region Constructors

        public DocumentResolver()
        {
        }

        public DocumentResolver(Dictionary<string, string> dictionary)
        {
            this.SetDictionary(dictionary);
        }

        public DocumentResolver(EngineType engineType)
        {
            this.SetEngine(engineType);
        }

        public DocumentResolver(Dictionary<string, string> dictionary, EngineType engineType)
        {
            this.SetDictionary(dictionary);
            this.SetEngine(engineType);
        }

        #endregion Constructors

        /// <summary>
        /// Set the documents against which to resolve.  Use the document IDs as the 
        /// dictionary Keys, and the document content for the dictionary Values.  If the
        /// item to be resolved includes title, author, and date metadata (for example,
        /// "Origin of Species 1859 Darwin, Charles") then the "document" should 
        /// also include all of those elements.
        /// </summary>
        /// <param name="dictionary"></param>
        public void SetDictionary(Dictionary<string, string> dictionary)
        {
            this._dictionary = dictionary;
            this._dictionaryDirty = true;
        }

        public void AddDictionaryItem(string key, string dictionaryItem)
        {
            this._dictionary.Add(key, dictionaryItem);
            this._dictionaryDirty = true;
        }

        public void ClearDictionary()
        {
            this._dictionary.Clear();
            this._dictionaryDirty = true;
        }

        /// <summary>
        /// Sets the resolver engines that will be used to resolve the document against
        /// the dictionary.
        /// </summary>
        /// <param name="type"></param>
        public void SetEngine(EngineType type)
        {
            switch (type)
            {
                case EngineType.BayesTFIDF:
                    _secondaryEngine = new TFIDFResolverEngine();
                    break;
                case EngineType.BayesLevenshtein:
                    _secondaryEngine = new LevenshteinResolverEngine();
                    break;
            }
        }

        /// <summary>
        /// Resolve the specified document against the supplied dictionary.
        /// </summary>
        /// <param name="document">The document to be resolved</param>
        /// <param name="useWordStemmer">True to reduce the document and dictionary entries to word stems before resolution</param>
        /// <returns>A list of documents that may match the document being resolved.  Evaluate the attributes of each result to determine likelyhood of a match.  List is NULL if an error occurred.</returns>
        public List<ResolutionResult> Resolve(string document, bool useWordStemmer = false)
        {
            List<ResolutionResult> results = null;

            if (this._dictionary.Count == 0)
            {
                throw new Exception("The Dictionary used by the DocumentResolver is empty.");
            }
            else
            {
                // Use the BayesResolverEngine to find the initial list of potential matches
                if (_dictionaryDirty)
                {
                    _bayesEngine.Dictionary = _dictionary;
                    _dictionaryDirty = false;
                }

                results = _bayesEngine.Resolve(document, useWordStemmer);

                if (results.Count > 0)
                {
                    // Get the documents identified by the Bayes resolver.
                    Dictionary<string, string> bayesResults = new Dictionary<string, string>();
                    foreach (ResolutionResult result in results)
                    {
                        bayesResults.Add(result.Key, result.Document);
                    }

                    // Use the TFIDF or Levenshtein resolver to find the best matches within the results
                    // returned by the Bayes resolver.
                    _secondaryEngine.Dictionary = bayesResults;
                    results = _secondaryEngine.Resolve(document, useWordStemmer);
                }
            }

            return results;
        }
    }
}
