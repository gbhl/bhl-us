using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace MOBOT.BHL.DocumentResolver
{
    abstract public class ResolverEngineBase : IResolverEngine
    {
        #region Properties

        private Dictionary<string, string> _dictionary = new Dictionary<string, string>();
        protected bool _dictionaryDirty = false;

        public Dictionary<string, string> Dictionary
        {
            get { return _dictionary; }
            set { _dictionary = value; _dictionaryDirty = true; }
        }

        #endregion Properties

        #region Constructors

        public ResolverEngineBase()
        {
        }

        public ResolverEngineBase(Dictionary<string, string> dictionary)
        {
            this.Dictionary = dictionary;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Resolves the specified document against the entries in the dictionary.  Execution of
        /// this method should reset the _documentDirty flag to true.
        /// </summary>
        /// <param name="document"></param>
        /// <param name="useWordStemmer"></param>
        /// <returns></returns>
        abstract public List<ResolutionResult> Resolve(string document, bool useWordStemmer = false);

        #endregion Methods
    }
}
