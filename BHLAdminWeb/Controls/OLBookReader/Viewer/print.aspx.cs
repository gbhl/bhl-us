using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using System;
using System.Collections.Generic;

namespace MOBOT.BHL.AdminWeb.Controls.OLBookReader.Viewer
{
    public partial class print : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BHLProvider bp = new BHLProvider();
            string itemIdStr = Request.QueryString["id"] as string;
            string index1Str = Request.QueryString["file"] as string;
            string index2Str = Request.QueryString["file2"] as string;
            string height1Str = Request.QueryString["height"] as string;
            string height2Str = Request.QueryString["height2"] as string;
            string width1Str = Request.QueryString["width"] as string;
            string width2Str = Request.QueryString["width2"] as string;
            int itemId;
            int index1;
            int index2;
            int height1;
            int height2;
            int width1;
            int width2;

            if (Int32.TryParse(itemIdStr, out itemId))
            {
                List<PageSummaryView> pageSummary = bp.PageSummarySelectForViewerByItemID(itemId);

                if (Int32.TryParse(index1Str, out index1) && 
                    Int32.TryParse(height1Str, out height1) && 
                    Int32.TryParse(width1Str, out width1))
                {
                    litImage1.Text = "<img src='" + this.GetImagePath(pageSummary, index1) + "' " + 
                        this.GetHtmlHeightWidth(height1, width1) + " alt='Page Image' />";
                }

                if (Int32.TryParse(index2Str, out index2) && 
                    Int32.TryParse(height2Str, out height2) && 
                    Int32.TryParse(width2Str, out width2))
                {
                    litImage2.Text = "<img src='" + this.GetImagePath(pageSummary, index2) + "' " +
                        this.GetHtmlHeightWidth(height2, width2) + " alt='Page Image' />";
                }
            }
        }

        private string GetImagePath(List<PageSummaryView> pageSummary, int index)
        {
            string iaImagePath = "{0}/download/{1}/page/n{2}.jpg";

            string imageUrl = string.Empty;
            if (pageSummary != null) {
                imageUrl = String.Format(iaImagePath, pageSummary[index].ExternalBaseURL, pageSummary[index].BarCode, 
                    (pageSummary[index].SequenceOrder - 1).ToString());
            }
            return imageUrl;
        }

        private string GetHtmlHeightWidth(int height, int width)
        {
            // Assume that the print aspect ratio is close to US Letter in portrait orientation
            double paperAspect = 8.5 / 11.0;  
            double imageAspect = (double)width / (double)height;
            string htmlAttribs = (imageAspect > paperAspect) ? "width='95%'" : "height='95%'";
            return htmlAttribs;
        }
    }
}
