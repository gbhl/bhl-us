using MOBOT.BHL.DataObjects;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.UI.HtmlControls;

namespace MOBOT.BHL.Web2
{
    public partial class SectionPage : BrowsePage
    {

        protected Segment BhlSegment { get; set; }
        protected int IsVirtual { get; set; } = 0;
        protected int HasLocalContent { get; set; } = 1;
        protected int SegmentID { get; set; }
        protected string SchemaType { get; set; }
        protected string DOI { get; set; }

        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            int segmentID;
            DOI = string.Empty;

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

                // Add Google Scholar metadata to the page headers
                List<KeyValuePair<string, string>> tags = bhlProvider.GetGoogleScholarMetadataForSegment(SegmentID, ConfigurationManager.AppSettings["PartPageUrl"]);
                foreach (KeyValuePair<string, string> tag in tags)
                {
                    HtmlMeta htmlMetaTag = new HtmlMeta();
                    htmlMetaTag.Name = tag.Key;
                    htmlMetaTag.Content = Server.HtmlEncode(tag.Value);
                    this.Page.Header.Controls.Add(htmlMetaTag);
                }

                COinS.SegmentID = SegmentID;
                
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

                List<DOI> dois = bhlProvider.DOISelectValidForSegment(SegmentID);
                if (dois.Count > 0) DOI = ConfigurationManager.AppSettings["DOIResolverURL"] + dois[0].DOIName;

                main.Page.Title = string.Format("Details - {0} - Biodiversity Heritage Library", BhlSegment.Title);
            }
        }
    }
}