﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Objects;
    
    public partial class BHLImportEntities : DbContext
    {
        public BHLImportEntities()
            : base("name=BHLImportEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<BSItem> BSItems { get; set; }
        public DbSet<BSSegment> BSSegments { get; set; }
        public DbSet<BSSegmentPage> BSSegmentPages { get; set; }
        public DbSet<ImportSource> ImportSources { get; set; }
        public DbSet<BSSegmentAuthor> BSSegmentAuthors { get; set; }
    
        public virtual int BSItemDeleteAllSegments(Nullable<int> itemID)
        {
            var itemIDParameter = itemID.HasValue ?
                new ObjectParameter("ItemID", itemID) :
                new ObjectParameter("ItemID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("BSItemDeleteAllSegments", itemIDParameter);
        }
    
        public virtual int BSItemSetStatus(Nullable<int> itemID, Nullable<int> itemStatusID)
        {
            var itemIDParameter = itemID.HasValue ?
                new ObjectParameter("ItemID", itemID) :
                new ObjectParameter("ItemID", typeof(int));
    
            var itemStatusIDParameter = itemStatusID.HasValue ?
                new ObjectParameter("ItemStatusID", itemStatusID) :
                new ObjectParameter("ItemStatusID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("BSItemSetStatus", itemIDParameter, itemStatusIDParameter);
        }
    
        public virtual int BSSegmentResolveAuthors(Nullable<int> segmentID)
        {
            var segmentIDParameter = segmentID.HasValue ?
                new ObjectParameter("SegmentID", segmentID) :
                new ObjectParameter("SegmentID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("BSSegmentResolveAuthors", segmentIDParameter);
        }
    
        public virtual int BSSegmentPublishToProduction(Nullable<int> itemID, Nullable<int> segmentID, ObjectParameter segmentStatusID)
        {
            var itemIDParameter = itemID.HasValue ?
                new ObjectParameter("ItemID", itemID) :
                new ObjectParameter("ItemID", typeof(int));
    
            var segmentIDParameter = segmentID.HasValue ?
                new ObjectParameter("SegmentID", segmentID) :
                new ObjectParameter("SegmentID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("BSSegmentPublishToProduction", itemIDParameter, segmentIDParameter, segmentStatusID);
        }
    
        public virtual ObjectResult<BSItem> BSItemAvailabilityCheck(Nullable<int> bHLItemID)
        {
            ((IObjectContextAdapter)this).ObjectContext.MetadataWorkspace.LoadFromAssembly(typeof(BSItem).Assembly);
    
            var bHLItemIDParameter = bHLItemID.HasValue ?
                new ObjectParameter("BHLItemID", bHLItemID) :
                new ObjectParameter("BHLItemID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<BSItem>("BSItemAvailabilityCheck", bHLItemIDParameter);
        }
    
        public virtual ObjectResult<BSItem> BSItemAvailabilityCheck(Nullable<int> bHLItemID, MergeOption mergeOption)
        {
            ((IObjectContextAdapter)this).ObjectContext.MetadataWorkspace.LoadFromAssembly(typeof(BSItem).Assembly);
    
            var bHLItemIDParameter = bHLItemID.HasValue ?
                new ObjectParameter("BHLItemID", bHLItemID) :
                new ObjectParameter("BHLItemID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<BSItem>("BSItemAvailabilityCheck", mergeOption, bHLItemIDParameter);
        }
    }
}
