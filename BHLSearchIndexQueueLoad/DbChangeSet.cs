using System;
using System.Collections.Generic;

namespace BHL.SearchIndexQueueLoad
{
    public class DbChangeSet
    {
        public List<DbChange> Changes { get; set; }
        public int LastAuditBasicID { get; set; }
        public DateTime LastAuditDate { get; set; }

        public DbChangeSet()
        {
            Changes = new List<DbChange>();
        }
    }
}
