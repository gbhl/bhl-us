using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace MOBOT.BHL.AdminWeb
{
    public class ContentPanel : Control
    {
        private string tableID = "";
        private int width = 0;

        public string TableID
        {
            get
            {
                return tableID;
            }
            set
            {
                tableID = value;
            }
        }

        public int Width
        {
            get
            {
                return width;
            }
            set
            {
                width = value;
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            string openingTag = "<table style=\"height:99%;\" border=\"0\" ";
            if (tableID != null && tableID.Trim().Length > 0)
                openingTag += "id=\"" + tableID.Trim() + "\" ";

            if (width > 0)
                openingTag += "width=\"" + width.ToString() + "\" ";

            openingTag += "cellpadding=\"0\" cellspacing=\"0\">";

            writer.WriteLine(openingTag);
            writer.WriteLine("<tr>");
            writer.WriteLine("<td width=\"12\"><img src=\"images/panel/top_left.gif\" width=\"12\" height=\"12\" alt=\"Top Left\" /></td>");
            writer.WriteLine("<td background=\"images/panel/top.gif\"><img src=\"images/blank.gif\" width=\"1\" height=\"1\" alt=\"Top\" /></td>");
            writer.WriteLine("<td width=\"12\"><img src=\"images/panel/top_right.gif\" width=\"12\" height=\"12\" alt=\"Top Right\" /></td>");
            writer.WriteLine("</tr>");
            writer.WriteLine("<tr>");
            writer.WriteLine("<td background=\"images/panel/left.gif\"><img src=\"images/blank.gif\" width=\"12\" height=\"12\" alt=\"Blank\" /></td>");
            writer.WriteLine("<td background=\"images/panel/bg.gif\" valign=\"top\" height=\"99%\">");

            //add inner controls here
            foreach (Control control in base.Controls)
            {
                control.RenderControl(writer);
            }

            writer.WriteLine("</td>");
            writer.WriteLine("<td background=\"images/panel/right.gif\"><img src=\"images/blank.gif\" width=\"12\" height=\"12\" alt=\"Blank\" /></td>");
            writer.WriteLine("</tr>");
            writer.WriteLine("<tr>");
            writer.WriteLine("<td><img src=\"images/panel/bottom_left.gif\" width=\"12\" height=\"12\" alt=\"Bottom Left\" /></td>");
            writer.WriteLine("<td background=\"images/panel/bottom.gif\"><img src=\"images/blank.gif\" width=\"" + (width > 0 ? width : 196).ToString() + "\" height=\"12\" alt=\"Blank\" /></td>");
            writer.WriteLine("<td><img src=\"images/panel/bottom_right.gif\" width=\"12\" height=\"12\" alt=\"Bottom Right\" /></td>");
            writer.WriteLine("</tr>");
            writer.WriteLine("</table>");
        }

        internal void SetTableID(string tableID)
        {
            this.tableID = tableID;
        }
    }
}