using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using System;
using System.Collections.Generic;
using System.Text;

namespace MOBOT.BHL.AdminWeb
{
    public partial class ReportRecentlyClusteredSegments : System.Web.UI.Page
    {
        private string _colorBG = " style='background-color:#F1F1FB' ";
        private string _whiteBG = " style='background-color:#FFFFFF' ";

        protected void Page_Load(object sender, EventArgs e)
        {
            BHLProvider provider = new BHLProvider();
            List<Segment> segments = provider.SegmentSelectRecentlyClustered(100);

            StringBuilder sb = new StringBuilder();
            int prevClusterID = 0;
            string currentBGColor = string.Empty;
            foreach (Segment segment in segments)
            {
                currentBGColor = GetBGColor(prevClusterID, (int)segment.SegmentClusterId, currentBGColor);
                sb.Append("<tr " + currentBGColor + ">");
                sb.Append("<td valign='top'>");
                if (prevClusterID != (int)segment.SegmentClusterId) sb.Append(segment.CreationDate.ToString());
                sb.Append("</td>");

                sb.Append("<td align='right' valign='top'>");
                sb.Append("<a href='/segmentedit.aspx?id=" + segment.SegmentID.ToString() + "'>");
                sb.Append(segment.SegmentID.ToString());
                sb.Append("</a>");
                sb.Append("</td>");

                sb.Append("<td align='left' valign='top'>");
                if (prevClusterID != (int)segment.SegmentClusterId) sb.Append(segment.SegmentClusterTypeLabel);
                sb.Append("</td>");

                sb.Append("<td align='left' valign='top'>");
                if (prevClusterID != (int)segment.SegmentClusterId)
                {
                    if (segment.CreationUserID == 1)
                        sb.Append("System");
                    else
                        sb.Append("User");
                }
                sb.Append("</td>");

                sb.Append("<td align='right' valign='top'>");
                sb.Append(segment.ItemID.ToString());
                sb.Append("</td>");

                sb.Append("<td align='right' valign='top'>");
                sb.Append(segment.StartPageID.ToString());
                sb.Append("</td>");

                sb.Append("<td style='white-space:nowrap;' valign='top'>");
                sb.Append(segment.GenreName + ": " + segment.Title);
                sb.Append("<br />");
                if (!string.IsNullOrWhiteSpace(segment.ContainerTitle) ||
                    !string.IsNullOrWhiteSpace(segment.Date) || 
                    !string.IsNullOrWhiteSpace(segment.Volume))
                {
                    sb.Append(segment.ContainerTitle);
                    if (!string.IsNullOrWhiteSpace(segment.Volume)) sb.Append(" (" + segment.Volume + ") ");
                    if (!string.IsNullOrWhiteSpace(segment.Date)) sb.Append(" " + segment.Date);
                    sb.Append("<br />");
                }
                if (!string.IsNullOrWhiteSpace(segment.Authors))
                {
                    sb.Append(segment.Authors.Replace("|", " - "));
                    sb.Append("<br />");
                }
                if (!string.IsNullOrWhiteSpace(segment.StartPageNumber) || !string.IsNullOrWhiteSpace(segment.PageRange))
                {
                    sb.Append("p.");
                    if (!string.IsNullOrWhiteSpace(segment.PageRange))
                        sb.Append(segment.PageRange);
                    else
                        sb.Append(segment.StartPageNumber + "-" + segment.EndPageNumber);
                    sb.Append("<br />");
                }
                if (!string.IsNullOrWhiteSpace(segment.DOIName))
                {
                    sb.Append(segment.DOIName);
                    sb.Append("<br />");
                }

                sb.Append("</td>");
                sb.Append("</tr>");

                prevClusterID = (int)segment.SegmentClusterId;
            }

            litSegmentListTableRows.Text = sb.ToString();
        }

        private string GetBGColor(int prevSegmentID, int currentSegmentID, string currentBGColor)
        {
            if (currentBGColor == string.Empty) return _colorBG;
            if (prevSegmentID == currentSegmentID)
            {
                return currentBGColor;
            }
            else
            {
                if (currentBGColor == _colorBG)
                    return _whiteBG;
                else
                    return _colorBG;
            }
        }
    }
}