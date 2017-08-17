using System;
using System.Collections.Generic;

namespace BHL.Search
{
    public interface ISearchResult
    {
        int PageSize { get; set; }
        int StartPage { get; set; }
        long TotalPages { get; set; }
        long TotalHits { get; set; }
        bool IsValid { get; set; }
        string ErrorMessage { get; set; }
        string DebugInfo { get; set; }
        List<IHit> Items { get; set; }
        List<IHit> Pages { get; set; }
        List<IHit> Names { get; set; }
        List<IHit> Keywords { get; set; }
        List<IHit> Authors { get; set; }
        Dictionary<SearchField, List<string>> Suggestions { get; set; }
        Dictionary<SearchField, Dictionary<string, long?>> Facets { get; set; }
        List<Tuple<SearchField, string>> Query { get; set; }
        List<Tuple<SearchField, string>> QueryLimits { get; set; }
    }
}
