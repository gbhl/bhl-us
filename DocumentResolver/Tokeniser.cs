/*
Tokenization
Original Author: Thanh Ngoc Dao - Thanh.dao@gmx.net
Copyright (c) 2005 by Thanh Ngoc Dao.
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;


namespace MOBOT.BHL.DocumentResolver
{
	/// <summary>
	/// Summary description for Tokeniser.
	/// Partition string into SUBwords
	/// </summary>
	internal class Tokeniser
	{
		public List<string> Partition(string input, StopWordsHandler stopWords, bool useWordStemmer = false)
		{
			Regex r=new Regex("([ \\t{}():;., \n])");			
			input=input.ToLower() ;

			String [] tokens=r.Split(input);

            List<string> words = new List<string>();

			for (int i=0; i < tokens.Length ; i++)
			{
				MatchCollection mc=r.Matches(tokens[i]);
				if (mc.Count <= 0 && tokens[i].Trim().Length > 0 					 
					&& !stopWords.IsStopword (tokens[i]) )								
					words.Add(tokens[i]) ;
			}
			
            if (useWordStemmer)
            {
                // Process the word list with an implementation of Martin Porter's word stemmer algorithm.
                // This will reduce the words contained in the array to their "root" forms.
                PorterStemmer stemmer = new PorterStemmer();
                for (int i = 0; i < words.Count; i++)
                {
                    words[i] = stemmer.stemTerm(words[i]);
                }
            }

            return words;
        }

		public Tokeniser()
		{
		}
	}
}
