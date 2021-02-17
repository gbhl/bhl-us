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
        public string SegmentOption { get; set; } = "Segment";

        [Display(Name = "Type")]
        public int EntityTypeID { get; set; }

        [Display(Name = "Title IDs")]
        public string TitleIDs { get; set; }

        [Display(Name = "Title ID")]
        public string TitleID { get; set; }

        [Display(Name = "Item ID")]
        public string ItemID { get; set; }

        [Display(Name = "Segment IDs")]
        public string SegmentIDs { get; set; }
    }

    public class QueueAddConfirmViewModel
    {
        public bool CopyrightWarning { get; set; } = false;
        public List<string> Titles { get; set; } = new List<string>();
        public List<string> Segments { get; set; } = new List<string>();

        public QueueAddConfirmViewModel()
        {
        }

        public QueueAddConfirmViewModel(bool copyrightWarning, List<string> titles, List<string> segments)
        {
            CopyrightWarning = copyrightWarning;
            Titles = titles;
            Segments = segments;
        }
    }
}