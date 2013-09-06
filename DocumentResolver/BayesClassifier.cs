/* ====================================================================
 * Copyright (c) 2006 Erich Guenther (erich_guenther@hotmail.com)
 * All rights reserved.
 *                       
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions
 * are met:
 *
 * 1. Redistributions of source code must retain the above copyright
 *    notice, this list of conditions and the following disclaimer. 
 *
 * 2. Redistributions in binary form must reproduce the above copyright
 *    notice, this list of conditions and the following disclaimer in
 *    the documentation and/or other materials provided with the
 *    distribution.
 * 
 * 3. The name of the author(s) must not be used to endorse or promote 
 *    products derived from this software without prior written permission.
 *
 * THIS SOFTWARE IS PROVIDED BY THE AUTHOR ``AS IS'' AND ANY
 * EXPRESSED OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
 * IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
 * PURPOSE ARE DISCLAIMED.  IN NO EVENT SHALL THE AUTHOR OR
 * ITS CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL,
 * SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT
 * NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
 * LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION)
 * HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT,
 * STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE)
 * ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED
 * OF THE POSSIBILITY OF SUCH DAMAGE. 
 */
using System;
using System.Collections.Generic;
using System.Text;

namespace MOBOT.BHL.DocumentResolver
{
	/// <summary>
	/// Naive Bayesian classifier</summary>
	/// <remarks>
	/// It suppports exclusion of words but not Phrases 
	/// </remarks>
	internal class BayesClassifier : IBayesClassifier
	{
		Dictionary<string, IBayesCategory> m_Categories;
		StopWordsHandler m_ExcludedWords;
		
		public BayesClassifier()
		{
            m_Categories = new Dictionary<string, IBayesCategory>();
            m_ExcludedWords = new StopWordsHandler();
		}

		/// <summary>
		/// Gets total number of word occurences over all categories</summary>
		int CountTotalWordsInCategories()
		{
			int total = 0;
			foreach (BayesCategory cat in m_Categories.Values)
			{
				total += cat.TotalWords;
			}
			return total;
		}

		/// <summary>
		/// Gets or creates a category</summary>
        IBayesCategory GetOrCreateCategory(string cat)
		{
            IBayesCategory c;
			if (!m_Categories.TryGetValue(cat, out c))
			{
				c = new BayesCategory(cat, m_ExcludedWords);
				m_Categories.Add(cat, c);
			}
			return c;
		}

		/// <summary>
		/// Trains this Category from a word or phrase<\summary>
		public void TeachPhrases(string cat, string[] phrases, bool useWordStemmer = false)
		{
			GetOrCreateCategory(cat).TeachPhrases(phrases, useWordStemmer);
		}

		/// <summary>
		/// Trains this Category from a word or phrase<\summary>
		public void TeachCategory(string cat, System.IO.TextReader tr, bool useWordStemmer = false)
		{
			GetOrCreateCategory(cat).TeachCategory(tr, useWordStemmer);
		}

		/// <summary>
		/// Classifies a text<\summary>
	    /// <returns>
		/// returns classification values for the text, the higher, the better is the match.</returns>
		public Dictionary<string, double> Classify(System.IO.StreamReader tr, bool useWordStemmer = false)
		{
			Dictionary<string, double> score = new Dictionary<string, double>();
            foreach (KeyValuePair<string, IBayesCategory> cat in m_Categories)
			{
				score.Add(cat.Value.Name, 0.0);
			}

			BayesEnumerableCategory words_in_file = new BayesEnumerableCategory("", m_ExcludedWords);
			words_in_file.TeachCategory(tr, useWordStemmer);

			foreach (KeyValuePair<string, BayesPhraseCount> kvp1 in words_in_file)
			{
				BayesPhraseCount pc_in_file = kvp1.Value;
                foreach (KeyValuePair<string, IBayesCategory> kvp in m_Categories)
				{
                    IBayesCategory cat = kvp.Value;
					int count = cat.GetPhraseCount(pc_in_file.RawPhrase);
					if (0 < count)
					{
						score[cat.Name] += System.Math.Log((double)count / (double)cat.TotalWords);
					}
					else
					{
						score[cat.Name] += System.Math.Log(0.01 / (double)cat.TotalWords);
					}
					//System.Diagnostics.Trace.WriteLine(string.Format("{0}({1}){2}", pc_in_file.RawPhrase.ToString(), cat.Name, score[cat.Name]));
				}


			}
            int totalWordsinCats = this.CountTotalWordsInCategories();
            foreach (KeyValuePair<string, IBayesCategory> kvp in m_Categories)
			{
                IBayesCategory cat = kvp.Value;
                score[cat.Name] += System.Math.Log((double)cat.TotalWords / (double)totalWordsinCats);
			}
			return score;
		}
	}
}
