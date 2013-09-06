﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MOBOT.BHL.DataObjects;
using CustomDataAccess;

namespace MOBOT.BHL.Web2
{
    public partial class CreatorPage : BasePage
    {
        protected Author Author { get; set; }
        protected string sortBy { get; set; }       
        protected int count { get; set; }
        protected int segmentcount { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            sortBy = (string)RouteData.Values["sort"];
            if (string.IsNullOrWhiteSpace(sortBy)) sortBy = "";
            
            // Parse AuthorId
            int creatorId;
            if (!int.TryParse((string)RouteData.Values["creatorid"], out creatorId)) Response.Redirect("~/authornotfound");

            // Find Author
            // Author = bhlProvider.AuthorSelectAuto(creatorId);
            Author = bhlProvider.AuthorSelectWithNameByAuthorId(creatorId);
            if (Author == null) Response.Redirect("~/authornotfound");

            main.Page.Title = string.Format("{0} - Biodiversity Heritage Library", Author.FullName);

            // Build Lifespan literal
            if (!string.IsNullOrEmpty(Author.StartDate) && !string.IsNullOrEmpty(Author.EndDate))
            {
                litLifespan.Text = string.Format(" ({0} - {1})", Author.StartDate, Author.EndDate);
            }
            else if (!string.IsNullOrEmpty(Author.StartDate))
            {
                litLifespan.Text = string.Format(" (Born - {0})", Author.StartDate);
            }
            else if (!string.IsNullOrEmpty(Author.EndDate))
            {
                litLifespan.Text = string.Format(" (Died - {0})", Author.EndDate);
            }

            // Find & Bind Author Titles
            CustomGenericList<SearchBookResult> SearchBookResultList = bhlProvider.TitleSelectByAuthor(creatorId);
            BookBrowse.SortBy = string.IsNullOrEmpty(sortBy) ? null : sortBy;
            BookBrowse.Data = SearchBookResultList;
            BookBrowse.ShowVolume = false;
            count = SearchBookResultList.Count;

            //Find & Bind Author Segments
            CustomGenericList<Segment> SegmentsResultList = bhlProvider.SegmentSelectForAuthorID(creatorId);
            SectionBrowse.SortBy = string.IsNullOrEmpty(sortBy) ? null : sortBy;
            SectionBrowse.Data = SegmentsResultList;
            segmentcount = SegmentsResultList.Count;
        }

        public string SetSortClass(string sortType)
        {
            if (sortBy != string.Empty)
            {
                return sortBy.Equals(sortType, StringComparison.OrdinalIgnoreCase) ? "activesort" : string.Empty;
            }
            else
            {   //set Title to active
                return sortType.Equals("title", StringComparison.OrdinalIgnoreCase) ? "activesort" : string.Empty;
            }
        }
    }
}