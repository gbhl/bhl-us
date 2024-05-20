using BHL.Search;
using MOBOT.BHL.API.BHLApiDataObjects2;
using MOBOT.BHL.API.BHLApiDataObjects3;
using MOBOT.BHL.DataObjects;
using MOBOT.BHL.DataObjects.Enum;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.UI.HtmlControls;

namespace MOBOT.BHL.Web2
{
    public partial class SectionPage : BrowsePage
    {

        protected Segment BhlSegment { get; set; }
        protected DataObjects.Institution RightsHolder { get; set; }
        protected int IsVirtual { get; set; } = 0;
        protected int HasLocalContent { get; set; } = 1;
        protected int SegmentID { get; set; }
        protected string SchemaType { get; set; }

        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            int segmentID;

            if (!int.TryParse((string)RouteData.Values["segmentid"], out segmentID))
            {
                Response.Redirect("~/pagenotfound");
            }
            else
            {
                SegmentID = segmentID;
                BhlSegment = bhlProvider.SegmentSelectExtended(SegmentID);
                if (BhlSegment == null)
                {
                    Response.Redirect("~/pagenotfound");
                }
                else
                {
                    // Check to make sure this title hasn't been replaced.  If it has, redirect
                    // to the appropriate titleid.
                    if (BhlSegment.RedirectSegmentID != null)
                    {
                        Response.Redirect("~/part/" + BhlSegment.RedirectSegmentID);
                    }

                    // Make sure the title is published.
                    if (BhlSegment.SegmentStatusID != (int)ItemStatus.ItemStatusValue.New && 
                        BhlSegment.SegmentStatusID != (int)ItemStatus.ItemStatusValue.Published)
                    {
                        Response.Redirect("~/itemunavailable");
                    }
                }

                List<PageSummaryView> psv = bhlProvider.PageSummarySegmentSelectBySegmentID(SegmentID);
                if (psv.Count > 0) {
                    IsVirtual = psv[0].IsVirtual; BhlSegment.BarCode = psv[0].BarCode; 
                }
                else { 
                    HasLocalContent = 0; 
                }

                BhlSegment.IdentifierList = bhlProvider.ItemIdentifierSelectForDisplayBySegmentID(SegmentID);
                InstitutionNameComparer comp = new InstitutionNameComparer();
                BhlSegment.ContributorList.Sort(comp);

                // Get the rights holder of the container item
                if (BhlSegment.BookID != null)
                {
                    DataObjects.Book book = bhlProvider.BookSelectAuto((int)BhlSegment.BookID);
                    List<DataObjects.Institution> institutions = bhlProvider.InstitutionSelectByItemID(book.ItemID);
                    foreach(DataObjects.Institution institution in institutions)
                    {
                        if (institution.InstitutionRoleName == "Rights Holder") RightsHolder = institution;
                    }
                }

                // Add Google Scholar metadata to the page headers
                List<KeyValuePair<string, string>> tags = bhlProvider.GetGoogleScholarMetadataForSegment(SegmentID, ConfigurationManager.AppSettings["PartPageUrl"]);
                foreach (KeyValuePair<string, string> tag in tags)
                {
                    HtmlMeta htmlMetaTag = new HtmlMeta();
                    htmlMetaTag.Name = tag.Key;
                    htmlMetaTag.Content = Server.HtmlEncode(tag.Value);
                    this.Page.Header.Controls.Add(htmlMetaTag);
                }

                // Set the data for the COinS output
                COinS.SegmentID = SegmentID;
                COinS.ItemIdentifiers = BhlSegment.IdentifierList;
                COinS.ItemKeywords = BhlSegment.KeywordList;
                COinS.ItemAuthors = BhlSegment.AuthorList;
                COinS.Genre = BhlSegment.GenreName;
                COinS.ArticleTitle = BhlSegment.Title;
                COinS.Title = BhlSegment.ContainerTitle;
                COinS.Volume = BhlSegment.Volume;
                COinS.Issue = BhlSegment.Issue;
                COinS.StartPageNumber = BhlSegment.StartPageNumber;
                COinS.EndPageNumber = BhlSegment.EndPageNumber;
                COinS.PageRange = BhlSegment.PageRange;
                COinS.Language = BhlSegment.LanguageCode;
                COinS.Date = BhlSegment.Date;

                // Set the Schema.org itemtype
                switch (BhlSegment.GenreName)
                {
                    case "Book":
                    case "Journal":
                        SchemaType = "https://schema.org/Book";
                        break;
                    case "Article":
                    case "Preprint":
                        SchemaType = "https://schema.org/ScholarlyArticle";
                        break;
                    default: // BookItem, Chapter, Issue, Proceeding, Conference, Unknown, Treatment
                        SchemaType = "https://schema.org/CreativeWork";
                        break;
                }

                main.Page.Title = string.Format("Details - {0} - Biodiversity Heritage Library", BhlSegment.Title);
            }
        }
    }
}