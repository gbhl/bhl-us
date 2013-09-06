/*
StopWordsHandler
Original Author: Thanh Ngoc Dao - Thanh.dao@gmx.net
*/
using System;
using System.Collections;
using System.Collections.Generic;

namespace MOBOT.BHL.DocumentResolver
{
	/// <summary>
	/// Stop words are frequently occurring, insignificant words words 
	/// that appear in a database record, article or web page. 
	/// Common stop words include
	/// </summary>
	internal class StopWordsHandler
	{		
		public static string[] stopWordsList=new string[] {
			                                                 "a", "an", "and", "are", "as", "at", 
                                                             "b", "be", "been", 
                                                             "c",
                                                             "d",
                                                             "e",
                                                             "f", "for", 
                                                             "g",
                                                             "h", "has", "have", 
                                                             "i", "in", "is", "it", 
                                                             "j",
                                                             "k",
                                                             "l",
                                                             "m",
                                                             "n",
                                                             "o", "of", "on", "one", 
                                                             "p",
                                                             "q",
                                                             "r",
                                                             "s",
                                                             "t", "that", "the", "this", "to", 
                                                             "u",
                                                             "v",
                                                             "w", "was", "what", "which", "with", 
                                                             "x",
                                                             "y", "you",
                                                             "z",
		} ;

        private static Dictionary<string, int> _stopwords = null;

		public StopWordsHandler()
		{
			if (_stopwords == null)
			{
                _stopwords = new Dictionary<string, int>();
                for (int i=0;i<stopWordsList.Length;i++)
				{
                    _stopwords[stopWordsList[i]] = i;
				}
			}
		}

        public bool IsStopword(string str)
        {
            return _stopwords.ContainsKey(str.ToLower());
        }
	}
}

