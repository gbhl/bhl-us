namespace BHL.Search
{
    public class SearchStringParam
    {
        public string searchValue { get; set; }
        public SearchStringParamOperator ParamOperator { get; set; }

        public SearchStringParam()
        {
            searchValue = string.Empty;
            ParamOperator = SearchStringParamOperator.And;
        }

        public SearchStringParam(string value, SearchStringParamOperator op)
        {
            searchValue = value;
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

