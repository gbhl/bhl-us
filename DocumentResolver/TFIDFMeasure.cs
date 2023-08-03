/*
 * tf/idf implementation 
 * Original Author: Thanh Dao, thanh.dao@gmx.net
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MOBOT.BHL.DocumentResolver
{
	/// <summary>
	/// Summary description for TF_IDFLib.
	/// </summary>
	internal class TFIDFMeasure
	{
		private Dictionary<string, string> _docs;
		private int _numDocs=0;
		private int _numTerms=0;
		private List<string> _terms;
        private List<Dictionary<int, int>> _termFreq = new List<Dictionary<int, int>>();
        private List<List<double>> _termWeight = new List<List<double>>();
		private List<int> _maxTermFreq = new List<int>();
        private Dictionary<int, int> _docFreq = new Dictionary<int,int>();
        private bool _useWordStemmer = false;
        private StopWordsHandler _stopWords = new StopWordsHandler();

		public class TermVector
		{		
			public static double ComputeCosineSimilarity(double[] vector1, double[] vector2)
			{
				if (vector1.Length != vector2.Length) throw new Exception("DIFER LENGTH");

				double denom=(VectorLength(vector1) * VectorLength(vector2));
				if (denom == 0)				
					return 0;				
				else				
					return (InnerProduct(vector1, vector2) / denom);
			}

			public static double InnerProduct(double[] vector1, double[] vector2)
			{
				if (vector1.Length != vector2.Length) throw new Exception("DIFFER LENGTH ARE NOT ALLOWED");
			
				double result=0;
				for (int i=0; i < vector1.Length; i++)				
					result += vector1[i] * vector2[i];
				
				return result;
			}
		
			public static double VectorLength(double[] vector)
			{			
				double sum=0.0;
				for (int i=0; i < vector.Length; i++)
					sum += (vector[i] * vector[i]);
						
				return Math.Sqrt(sum);
			}
		}

		private IDictionary _wordsIndex=new Hashtable() ;

		public TFIDFMeasure(Dictionary<string, string> documents, bool useWordStemmer = false)
		{
			_docs=documents;
			_numDocs=documents.Count;
            _useWordStemmer = useWordStemmer;
			MyInit();
		}

		private List<string> GenerateTerms(Dictionary<string, string> docs)
		{
            List<string> uniques = new List<string>();
            foreach(KeyValuePair<string, string> kvp in docs)
			{
				Tokeniser tokenizer=new Tokeniser() ;
				List<string> words=tokenizer.Partition(kvp.Value, _stopWords, _useWordStemmer);

                foreach (string word in words)
                    if (!uniques.Contains(word))
                        uniques.Add(word);
			}
			return uniques;
		}

		private static object AddElement(IDictionary collection, object key, object newValue)
		{
			object element=collection[key];
			collection[key]=newValue;
			return element;
		}

		private int GetTermIndex(string term)
		{
			object index=_wordsIndex[term];
			if (index == null) return -1;
			return (int) index;
		}

		private void MyInit()
		{
			_terms=GenerateTerms (_docs );
			_numTerms=_terms.Count ;

			for(int i=0; i < _terms.Count ; i++)			
			{
				_termWeight.Add(new List<double>());
                _termFreq.Add(new Dictionary<int, int>());

				AddElement(_wordsIndex, _terms[i], i);			
			}
			
			GenerateTermFrequency ();

            // Removed term generation here because storing all weight values in RAM is 
            // impractical for large numbers (tens of thousands) of documents and terms.
            // Did not work at all when compiled for 32-bit processors.  Worked with a
            // 64-bit compile, but used all available RAM on machine.
            // 2013-07-10 MWL
			//GenerateTermWeight();
		}
		
		private double Log(double num)
		{
			return Math.Log(num) ;//log2
		}

		private void GenerateTermFrequency()
		{
            int i = 0;
            foreach(KeyValuePair<string, string> kvp in _docs)
			{								
				IDictionary freq=GetWordFrequency(kvp.Value);
				IDictionaryEnumerator enums=freq.GetEnumerator() ;
				_maxTermFreq.Add(int.MinValue);
				while (enums.MoveNext())
				{
					string word=(string)enums.Key;
					int wordFreq=(int)enums.Value ;
					int termIndex=GetTermIndex(word);

                    _termFreq[termIndex].Add(i, wordFreq);

                    if (_docFreq.ContainsKey(termIndex))
                        _docFreq[termIndex]++;
                    else
                        _docFreq[termIndex] = 1;

					if (wordFreq > _maxTermFreq[i]) _maxTermFreq[i]=wordFreq;					
				}
                i++;
			}
		}

		private void GenerateTermWeight()
		{			
			for(int i=0; i < _numTerms   ; i++)
			{
				for(int j=0; j < _numDocs ; j++)				
					_termWeight[i].Add(ComputeTermWeight (i, j));
			}
		}

		private double GetTermFrequency(int term, int doc)
		{			
            int freq = 0;
            if (_termFreq[term].ContainsKey(doc)) freq = _termFreq[term][doc];
			int maxfreq=_maxTermFreq[doc];			
			
			return ( (double) freq/(double)maxfreq );
		}

		private double GetInverseDocumentFrequency(int term)
		{
			int df=_docFreq[term];
			return Log((double) (_numDocs) / (double) df );
		}

		private double ComputeTermWeight(int term, int doc)
		{
			double tf=GetTermFrequency (term, doc);
			double idf=GetInverseDocumentFrequency(term);
			return tf * idf;
		}
		
		private  double[] GetTermVector(int doc)
		{
			double[] w = new double[_numTerms] ;

            // Compute the weights on the fly here, rather than reading the values from 
            // memory.  Computing and storing all weights in the MyInit() method is 
            // impractical due to the amount of RAM needed to handle large numbers of 
            // terms and documents.
            // 2013-07-10 MWL
            for (int i = 0; i < _numTerms; i++)
                w[i] = ComputeTermWeight(i, doc);						
				//w[i]=_termWeight[i][doc];			
				
			return w;
		}

		public double GetSimilarity(int doc_i, int doc_j)
		{
			double[] vector1=GetTermVector (doc_i);
			double[] vector2=GetTermVector (doc_j);

			return TermVector.ComputeCosineSimilarity(vector1, vector2) ;
		}
		
		private IDictionary GetWordFrequency(string input)
		{
			string convertedInput=input.ToLower() ;
					
			Tokeniser tokenizer=new Tokeniser() ;
			List<string> words=tokenizer.Partition(convertedInput, _stopWords, _useWordStemmer);
            words.Sort();
			
			List<string> distinctWords=GetDistinctWords(words);
						
			IDictionary result=new Hashtable();
            foreach(string word in distinctWords)
			{
				object tmp;
				tmp=CountWords(word, words);
				result[word]=tmp;
			}
			
			return result;
		}				
				
		private List<string> GetDistinctWords(List<string> input)
		{				
			if (input == null)			
				return new List<string>();
			else
			{
                List<string> list = new List<string>();

                foreach (string word in input)
                    if (!list.Contains(word))   // N-GRAM SIMILARITY?
                        list.Add(word);

                return list;
			}
		}
		
		private int CountWords(string word, List<string> words)
		{
            int count = words.Where(s => s == word).Select(s => s).Count();
			return count;
		}				
	}
}
