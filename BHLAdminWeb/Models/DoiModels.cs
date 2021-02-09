using MOBOT.BHL.DataObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MOBOT.BHL.AdminWeb.Models
{
    public class EditDoiQueueViewModel
    {
        public EditDoiQueueViewModel() { }

        // Allow Initialization with an instance of ApplicationUser:
        public EditDoiQueueViewModel(DOI doi)
        {
            this.EntityTypeID = doi.DOIEntityTypeID;
            this.EntityType = doi.DOIEntityTypeName;
            this.EntityID = doi.EntityID;
            this.DateQueued = doi.StatusDate;
            this.AddedBy = doi.CreationUserName;
        }


        public int EntityTypeID { get; set; }

        [Required]
        [Display(Name = "Entity Type")]
        public string EntityType { get; set; }

        [Display(Name = "Entity ID")]
        public int EntityID { get; set; }

        [Display(Name = "Added By")]
        public string AddedBy { get; set; }

        [Display(Name = "Date Queued")]
        public DateTime DateQueued { get; set; }
    }

    public class QueueAddViewModel
    {
        [Display(Name = "Type")]
        public int EntityTypeID { get; set; }

        [Display(Name = "Title IDs")]
        public string TitleIDs { get; set; }

        [Display(Name = "Title ID")]
        public string TitleID { get; set; }

        [Display(Name = "ItemID")]
        public string ItemID { get; set; }

        [Display(Name = "Segment IDs")]
        public string SegmentIDs { get; set; }
    }

    public class QueueAddConfirmViewModel
    {
        public List<int> TitleIDs { get; set; } = new List<int>();
        public List<int> SegmentIDs { get; set; } = new List<int>();

        public QueueAddConfirmViewModel()
        {
        }

        public QueueAddConfirmViewModel(List<int> titleIDs, List<int> segmentIDs)
        {
            TitleIDs = titleIDs;
            SegmentIDs = segmentIDs;
        }
    }
}