using System;

namespace BHL.SearchIndexQueueLoad
{
    public class DbChange
    {
        public int AuditId { get; set; }
        public DateTime AuditDate { get; set; }
        public string Operation { get; set; }
        public string IndexEntity { get; set; }
        public int Id { get; set; }
    }
}
