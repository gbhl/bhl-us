//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MOBOT.BHLImport.BHLImportEFDataModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class BSSegmentAuthor
    {
        public int SegmentAuthorID { get; set; }
        public int ImportSourceID { get; set; }
        public int SegmentID { get; set; }
        public string BioStorID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public int SequenceOrder { get; set; }
        public string VIAFIdentifier { get; set; }
        public Nullable<int> BHLAuthorID { get; set; }
        public System.DateTime CreationDate { get; set; }
        public System.DateTime LastModifiedDate { get; set; }
    
        public virtual ImportSource ImportSource { get; set; }
    }
}