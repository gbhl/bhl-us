using System;
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
            if (!int.TryParse(RouteData.Values["creatorid"] as string, out creatorId)) Response.Redirect("~/authornotfound");

            // Find Author
            // Author = bhlProvider.AuthorSelectAuto(creatorId);
            Author = bhlProvider.AuthorSelectWithNameByAuthorId(creatorId);
            if (Author == null) Response.Redirect("~/authornotfound");
            if (Author.RedirectAuthorID != null) Response.Redirect("~/creator/" + Author.RedirectAuthorID);

            main.Page.Title = string.Format("{0} - Biodiversity Heritage Library", Author.FullName);

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