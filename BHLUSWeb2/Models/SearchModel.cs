using BHL.Search;
using System;
using System.Collections.Generic;

namespace MOBOT.BHL.Web2.Models
{
    public class SearchModel
    {
        public SearchModel()
        {
            Params = new SearchParams();
            ItemPage = 1;
            AuthorPage = 1;
            KeywordPage = 1;
            NamePage = 1;
        }

        public SearchParams Params { get; set; }

        public string ParamLabel
        {
            get
            {
                return new MVCServices.SearchService().GetSearchCriteriaLabel(
                    Params.SearchCategory, Params.SearchTerm, (Params.Language != null ? Params.Language.Item1 : string.Empty), 
                    Params.LastName, Params.Volume, Params.Year, Params.Subject, 
                    (Params.Collection != null ? Params.Collection.Item1 : string.Empty));

            }
        }

        public string QueryString
        {
            get
            {
                List<string> qsParams = new List<string>();
                if (!string.IsNullOrWhiteSpace(Params.SearchCategory)) qsParams.Add(string.Format("{0}={1}", "searchCat", Params.SearchCategory));
                if (!string.IsNullOrWhiteSpace(Params.SearchTerm)) qsParams.Add(string.Format("{0}={1}", "searchTerm", Params.SearchTerm));
                if (!string.IsNullOrWhiteSpace(Params.LastName)) qsParams.Add(string.Format("{0}={1}", "lname", Params.LastName));
                if (!string.IsNullOrWhiteSpace(Params.Volume)) qsParams.Add(string.Format("{0}={1}", "vol", Params.Volume));
                if (!string.IsNullOrWhiteSpace(Params.Year)) qsParams.Add(string.Format("{0}={1}", "yr", Params.Year));
                if (!string.IsNullOrWhiteSpace(Params.Subject)) qsParams.Add(string.Format("{0}={1}", "subj", Params.Subject));
                if (Params.Language != null) qsParams.Add(string.Format("{0}={1}", "lang", Params.Language.Item1));
                if (Params.Collection != null) qsParams.Add(string.Format("{0}={1}", "col", Params.Collection.Item1));

                return string.Join("&", qsParams.ToArray());
            }
        }

        public int ItemPage { get; set; }
        public int AuthorPage { get; set; }
        public int KeywordPage { get; set; }
        public int NamePage { get; set; }

        public ISearchResult ItemResult { get; set; }
        public ISearchResult AuthorResult { get; set; }
        public ISearchResult KeywordResult { get; set; }
        public ISearchResult NameResult { get; set; }

        public List<string> Suggestions
        {
            get
            {
                List<string> suggestions = new List<string>();

                if (IsItemResultValid) GetUniqueSuggestions(ItemResult.Suggestions, suggestions);
                if (IsAuthorResultValid) GetUniqueSuggestions(AuthorResult.Suggestions, suggestions);
                if (IsKeywordResultValid) GetUniqueSuggestions(KeywordResult.Suggestions, suggestions);
                if (IsNameResultValid) GetUniqueSuggestions(NameResult.Suggestions, suggestions);               

                return suggestions;
            }
        }

        public int TotalFacets
        {
            get
            {
                if (Params == null)
                    return 0;
                else
                    return Params.GenreFacets.Count + Params.MaterialTypeFacets.Count + Params.AuthorFacets.Count +
                        Params.DateRangeFacets.Count + Params.ContributorFacets.Count + Params.KeywordFacets.Count +
                        Params.LanguageFacets.Count;
            }
        }

        /// <summary>
        /// Add unique suggestions in the newSuggestions dictionary to the uniqueSuggestions list
        /// </summary>
        /// <param name="newSuggestions"></param>
        /// <param name="uniqueSuggestions"></param>
        private void GetUniqueSuggestions(Dictionary<SearchField, List<string>> newSuggestions, List<string> uniqueSuggestions)
        {
            foreach (KeyValuePair<SearchField, List<string>> kvp in newSuggestions)
            {
                foreach (string suggestion in kvp.Value)
                {
                    if (!uniqueSuggestions.Contains(suggestion)) uniqueSuggestions.Add(suggestion);
                }
            }
        }

        public bool IsItemResultValid
        {
            get
            {
                bool valid = false;
                if (ItemResult != null) valid = ItemResult.IsValid;
                return valid;
            }
        }

        public bool IsAuthorResultValid
        {
            get
            {
                bool valid = false;
                if (AuthorResult != null) valid = AuthorResult.IsValid;
                return valid;
            }
        }

        public bool IsKeywordResultValid
        {
            get
            {
                bool valid = false;
                if (KeywordResult != null) valid = KeywordResult.IsValid;
                return valid;
            }
        }

        public bool IsNameResultValid
        {
            get
            {
                bool valid = false;
                if (NameResult != null) valid = NameResult.IsValid;
                return valid;
            }
        }
    }

    public class SearchParams
    {
        public SearchParams()
        {
            SearchCategory = string.Empty;
            SearchTerm = string.Empty;
            TermInclude = "A";
            LastName = string.Empty;
            LastNameInclude = "A";
            Volume = string.Empty;
            Year = string.Empty;
            Subject = string.Empty;
            SubjectInclude = "A";
            Language = null;
            Collection = null;
        }

        public string SearchCategory { get; set; }
        public string SearchTerm { get; set; }

        // Indicates if responses to searches for SearchTerm,
        // LastNmae, or Subject should include all words (A) 
        // or the exact phrase (P).
        // * Only applies to Advanced Searches.
        public string TermInclude { get; set; }
        public string LastNameInclude { get; set; }
        public string SubjectInclude { get; set; }

        public string LastName { get; set; }
        public string Volume { get; set; }
        public string Year { get; set; }
        public string Subject { get; set; }
        public Tuple<string, string> Language { get; set; }
        public Tuple<string, string> Collection { get; set; }

        private List<FacetParam> _genreFacets = new List<FacetParam>();
        public List<FacetParam> GenreFacets {
            get { return _genreFacets; }
            set { _genreFacets = value; }
        }

        private List<FacetParam> _materialTypeFacets = new List<FacetParam>();
        public List<FacetParam> MaterialTypeFacets
        {
            get { return _materialTypeFacets; }
            set { _materialTypeFacets = value; }
        }

        private List<FacetParam> _authorFacets = new List<FacetParam>();
        public List<FacetParam> AuthorFacets
        {
            get { return _authorFacets; }
            set { _authorFacets = value; }
        }

        private List<FacetParam> _dateRangeFacets = new List<FacetParam>();
        public List<FacetParam> DateRangeFacets
        {
            get { return _dateRangeFacets; }
            set { _dateRangeFacets = value; }
        }

        private List<FacetParam> _contributorFacets = new List<FacetParam>();
        public List<FacetParam> ContributorFacets
        {
            get { return _contributorFacets; }
            set { _contributorFacets = value; }
        }

        private List<FacetParam> _keywordFacets = new List<FacetParam>();
        public List<FacetParam> KeywordFacets
        {
            get { return _keywordFacets; }
            set { _keywordFacets = value; }
        }

        private List<FacetParam> _languageFacets = new List<FacetParam>();
        public List<FacetParam> LanguageFacets
        {
            get { return _languageFacets; }
            set { _languageFacets = value; }
        }
    }

    public class FacetParam
    {
        public FacetParam()
        {
            Checked = false;
        }

        public FacetParam(string type, string value, long? numHits, bool selected = false)
        {
            Type = type;
            Value = value;
            NumHits = numHits;
            Checked = selected;
        }

        public string Type { get; set; }
        public string Value { get; set; }
        public long? NumHits { get; set; }
        public bool Checked { get; set; }
    }

}