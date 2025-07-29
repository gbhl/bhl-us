using MOBOT.BHL.DataObjects;
using System.Collections.Generic;

namespace MOBOT.BHL.Web2.Models
{
    public class PartModel
    {
        public Segment Segment { get; set; }
        public Institution RightsHolder { get; set; }
        public int IsVirtual { get; set; } = 0;
        public int HasLocalContent { get; set; } = 1;
        public int SegmentID { get; set; }
        public string SchemaType { get; set; }
        public List<KeyValuePair<string, string>> GoogleScholarTags = new List<KeyValuePair<string, string>>();
        public COinSModel COinS { get; set; } = new COinSModel();
    }
}