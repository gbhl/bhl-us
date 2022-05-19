using MOBOT.BHL.DataObjects;

namespace BHL.WebServiceREST.v1.Models
{
    public class PageNameModel
    {
        public List<NameFinderResponse> nameinfo { get; set; } = new List<NameFinderResponse>();
        public string sourcename { get; set; } = string.Empty;
    }
}
