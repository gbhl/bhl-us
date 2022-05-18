namespace BHL.WebServiceREST.v1.Models
{
    public class DoiModel
    {
        public int? entitytypeid { get; set; } = null;
        public int? entityid { get; set; } = null;
        public string? doiname { get; set; } = null;
        public int? userid { get; set; } = null;
        public int? doistatusid { get; set; } = null;
        public short? isvalid { get; set; } = null;
        public string? doibatchid { get; set; } = null;
        public string? message { get; set; } = null;
        public int? excludebhldoi { get; set; } = null;
    }
}
