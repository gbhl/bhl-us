using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.Caching;
using System.Web;
using MOBOT.BHL.Server;
using MOBOT.BHL.DataObjects;
using CustomDataAccess;
using MOBOT.BHL.DocumentResolver;

namespace BHLApi3Web.Services
{
    public class ResolverService : BHLApi3Web.Services.IResolverService
    {
        /// <summary>
        /// Resolve the specified title, author, and year using the specified resolver object.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="authors"></param>
        /// <param name="year"></param>
        /// <param name="type"></param>
        /// <param name="resolver"></param>
        /// <param name="results"></param>
        /// <returns></returns>
        public List<Models.ResolutionResult> Resolve(string title, string authors, string year, string type,
            List<Models.ResolutionResult> results)
        {
            title = title ?? string.Empty;
            authors = authors ?? string.Empty;
            year = year ?? string.Empty;

            String cacheKey = GetCacheKey(type, (authors != string.Empty), (year != string.Empty));

            // Get a resolver populated with an appropriate dictionary
            DocumentResolver resolver = GetResolver(authors, year, type, cacheKey);   

            // Resolve the specified data
            List<MOBOT.BHL.DocumentResolver.ResolutionResult> resolutionResults = resolver.Resolve(string.Format("{0} {1} {2}", title, year, authors).Trim(), true);

            // Limit results to scores of at least 0.01 (which is pretty low)
            IEnumerable<MOBOT.BHL.DocumentResolver.ResolutionResult> filteredResults = 
                (from resolutionResult in resolutionResults where resolutionResult.Score >= 0.01 select resolutionResult);

            if (filteredResults.Count() > 0)
            {
                // Add the resolver results to the list being returned
                foreach (MOBOT.BHL.DocumentResolver.ResolutionResult resolutionResult in filteredResults)
                {
                    Models.ResolutionResult result = new Models.ResolutionResult();
                    result.Type = type;
                    result.Key = resolutionResult.Key;
                    result.Document = resolutionResult.Document;
                    result.Score = resolutionResult.Score;
                    results.Add(result);
                }
            }

            // Sort the results being returned
            var sortedResults = from result in results orderby result.Score descending select result;

            return sortedResults.ToList();
        }

        /// <summary>
        /// Generate a key to be used when adding/accessing cache objects
        /// </summary>
        /// <param name="type"></param>
        /// <param name="includeAuthors"></param>
        /// <param name="includeYear"></param>
        /// <returns></returns>
        private static string GetCacheKey(string type, bool includeAuthors, bool includeYear)
        {
            return string.Format("{0}{1}{2}", type, (includeAuthors ? "true" : "false"), (includeYear ? "true" : "false"));
        }

        /// <summary>
        /// Get a resolver object from the cache or create a new resolver object.
        /// </summary>
        /// <param name="authors"></param>
        /// <param name="year"></param>
        /// <param name="type"></param>
        /// <param name="cacheKey"></param>
        /// <returns></returns>
        private DocumentResolver GetResolver(string authors, string year, string type, String cacheKey)
        {
            DocumentResolver resolver = null;

            if (MemoryCache.Default[cacheKey] != null)
            {
                // Use cached version
                resolver = (DocumentResolver)MemoryCache.Default[cacheKey];
            }
            else
            {
                // Set up resolver
                resolver = new DocumentResolver();
                resolver.SetEngine(DocumentResolver.EngineType.BayesTFIDF);
                switch (type)
                {
                    case "title":
                        resolver.SetDictionary(GetTitleDictionary(authors != string.Empty, year != string.Empty));
                        break;
                    case "part":
                    default:
                        resolver.SetDictionary(GetSegmentDictionary(authors != string.Empty, year != string.Empty));
                        break;
                }

                MemoryCache.Default.Add(cacheKey, resolver, DateTime.Now.AddMinutes(
                    Convert.ToDouble(ConfigurationManager.AppSettings["ResolverCacheTime"])));
            }

            return resolver;
        }

        /// <summary>
        /// Get the dictionary of segments to resolve against.
        /// </summary>
        /// <param name="includeAuthors"></param>
        /// <param name="includeYear"></param>
        /// <returns></returns>
        private Dictionary<string, string> GetSegmentDictionary(bool includeAuthors, bool includeYear)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            CustomGenericList<TitleEndNote> segments = new BHLProvider().SegmentSelectAllEndNoteCitations();

            foreach (TitleEndNote segment in segments)
            {
                if (!dictionary.ContainsKey(segment.SegmentID.ToString()))
                {
                    dictionary.Add(segment.SegmentID.ToString(),
                        string.Format("{0} {1} {2}",
                            segment.Title,
                            includeYear ? segment.Year : string.Empty,
                            includeAuthors ? segment.Authors : string.Empty).Trim());
                }
            }

            return dictionary;
        }

        /// <summary>
        /// Get the dictionary of titles to resolve against.
        /// </summary>
        /// <param name="includeAuthors"></param>
        /// <param name="includeYear"></param>
        /// <returns></returns>
        private Dictionary<string, string> GetTitleDictionary(bool includeAuthors, bool includeYear)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            CustomGenericList<TitleEndNote> titles = new BHLProvider().TitleEndNoteSelectAllTitleCitations();

            foreach (TitleEndNote title in titles)
            {
                if (!dictionary.ContainsKey(title.TitleID.ToString()))
                {
                    dictionary.Add(title.TitleID.ToString(),
                        string.Format("{0} {1} {2}",
                            title.Title,
                            includeYear ? title.Year : string.Empty,
                            includeAuthors ? title.Authors.Replace('|', ' ') : string.Empty).Trim());
                }
            }

            return dictionary;
        }
    }
}