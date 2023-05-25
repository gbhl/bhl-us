namespace BHL.Search
{
    public class SearchStringParam
    {
        public string SearchValue { get; set; }
        public SearchStringParamOperator ParamOperator { get; set; }

        public SearchStringParam()
        {
            SearchValue = string.Empty;
            ParamOperator = SearchStringParamOperator.And;
        }

        public SearchStringParam(string value, SearchStringParamOperator op)
        {
            SearchValue = value;
            ParamOperator = op;
        }
    }

    public enum SearchStringParamOperator
    {
        And,
        Or,
        Phrase
    }

}

