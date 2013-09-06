using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using RssToolkit;

namespace MOBOT.BHL.Web.Utilities
{
    public class RssFeedControl : UserControl
    {
        private string feedLocation = "";
        private int maxRecords = 0;
        private string noItemsFoundText = "";
        private string target = "";
        private bool showDescription = true;
        private bool separateItems = false;
        private bool showDate = false;

        public string FeedLocation
        {
            get
            {
                return feedLocation;
            }
            set
            {
                feedLocation = value;
            }
        }

        public int MaxRecords
        {
            get
            {
                return maxRecords;
            }
            set
            {
                maxRecords = value;
            }
        }

        public string NoItemsFoundText
        {
            get
            {
                return noItemsFoundText;
            }
            set
            {
                noItemsFoundText = value;
            }
        }

        public string Target
        {
            get
            {
                return target;
            }
            set
            {
                target = value;
            }
        }

        /// <summary>
        /// Defaults to true.
        /// </summary>
        public bool ShowDescription
        {
            get
            {
                return showDescription;
            }
            set
            {
                showDescription = value;
            }
        }

        public bool SeparateItems
        {
            get
            {
                return separateItems;
            }
            set
            {
                separateItems = value;
            }
        }

        public bool ShowDate
        {
            get
            {
                return showDate;
            }
            set
            {
                showDate = value;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            try
            {
                GenericRssChannel c = GenericRssChannel.LoadChannel(feedLocation);
                int recordCount = c.Items.Count;
                if (recordCount == 0)
                {
                    //if we didn't find anything, add a message literal and drop out.
                    Literal messageLiteral = new Literal();
                    messageLiteral.Text = NoItemsFoundText;
                    this.Controls.Add(messageLiteral);
                    return;
                }
                //limit the number of records to show if needed.
                if (MaxRecords > 0 && MaxRecords < recordCount)
                    recordCount = MaxRecords;

                HtmlTable table = new HtmlTable();
                table.CellPadding = 2;
                table.CellSpacing = 2;
                table.Border = 0;

                for (int i = 0; i < recordCount; i++)
                {
                    HtmlTableRow row = new HtmlTableRow();
                    HtmlTableCell cell = new HtmlTableCell();
                    HyperLink link = new HyperLink();
                    link.NavigateUrl = c.Items[i]["link"];
                    link.Text = c.Items[i]["title"];
                    if (separateItems)
                        link.Font.Bold = true;

                    if (Target.Trim().Length > 0)
                        link.Target = Target;

                    cell.Controls.Add(link);
                    if (showDate)
                    {
                        try
                        {
                            Literal dateLiteral = new Literal();
                            dateLiteral.Text = " - Posted: " + DateTime.Parse(c.Items[i]["pubDate"]).ToShortDateString();
                            cell.Controls.Add(dateLiteral);
                        }
                        catch
                        {
                        }
                    }
                    if (ShowDescription)
                    {
                        HtmlGenericControl div = new HtmlGenericControl("div");
                        //div.InnerHtml = Server.HtmlEncode(c.Items[i]["description"]);
                        div.InnerHtml = c.Items[i]["description"];
                        cell.Controls.Add(div);
                    }
                    row.Cells.Add(cell);
                    table.Rows.Add(row);
                    if (separateItems && recordCount > 1 && i < recordCount - 1)
                    {
                        HtmlTableRow ruleRow = new HtmlTableRow();
                        HtmlTableCell ruleCell = new HtmlTableCell();
                        HtmlGenericControl rule = new HtmlGenericControl("hr");
                        rule.Style.Add("width", "99%");
                        ruleCell.Controls.Add(rule);
                        ruleRow.Cells.Add(ruleCell);
                        table.Rows.Add(ruleRow);
                    }
                }
                this.Controls.Add(table);
            }
            catch (Exception ex)
            {
                Literal messageLiteral = new Literal();
                if (DebugUtility.IsDebugMode(Response, Request))
                {
                    messageLiteral.Text = "The following error occurred while checking the rss feed:<br /><br />";
                    messageLiteral.Text += "<b>Message:</b> " + ex.Message + "<br /><br />" +
                        "<b>Stack Trace:</b> " + ex.StackTrace.Replace("\n", "<br />");
                }
                else
                {
                    messageLiteral.Text = NoItemsFoundText;
                }
                this.Controls.Add(messageLiteral);
            }
        }
    }
}
