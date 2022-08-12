using RssToolkit;
using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MOBOT.BHL.Web2
{
    public class RssFeedControlVersion2 : UserControl
    {
        private string feedLocation = "";
        private int maxRecords = 0;
        private string noItemsFoundText = "";
        private string target = "";
        private bool showDescription = true;
        private bool separateItems = false;
        private bool showDate = false;
        private int descriptionLimit = 0;

        public string FeedLocation
        {
            get { return feedLocation; }
            set { feedLocation = value; }
        }

        public int MaxRecords
        {
            get { return maxRecords; }
            set { maxRecords = value; }
        }

        public string NoItemsFoundText
        {
            get { return noItemsFoundText; }
            set { noItemsFoundText = value; }
        }

        public string Target
        {
            get { return target; }
            set { target = value; }
        }

        public bool ShowDescription
        {
            get { return showDescription; }
            set { showDescription = value; }
        }

        public int DescriptionLimit
        {
            get { return descriptionLimit; }
            set { descriptionLimit = value; }
        }

        public bool SeparateItems
        {
            get { return separateItems; }
            set { separateItems = value; }
        }

        public bool ShowDate
        {
            get { return showDate; }
            set { showDate = value; }
        }

        protected string TruncateAtWord(string input, int length)
        {
            if (input == null || input.Length < length)
                return input;
            int iNextSpace = input.LastIndexOf(" ", length);

            return string.Format("{0}...", input.Substring(0, (iNextSpace > 0) ? iNextSpace : length).Trim());
        }

        string RemoveHtmlTags(string html)
        {
            return Regex.Replace(html, "<.+?>", string.Empty);            
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

                Literal results = new Literal();
                //Probably a more programatic way to build HTML, but risk of added styling
                StringBuilder HTMLResults = new StringBuilder();
                HTMLResults.Append("<div class=\"rss-items\">");
                for (int i = 0; i < recordCount; i++)
                {
                    HTMLResults.Append("<div class=\"rss-item\">");
                    HTMLResults.Append(String.Format("<a class=\"rss-title\" href=\"{0}\" target=\"{1}\">{2}</a>", c.Items[i]["link"],Target,c.Items[i]["title"]));

                    if (showDate)
                    {
                        try
                        {
                            HTMLResults.Append(String.Format("<span class=\"rss-posted\">{0}</span>", DateTime.Parse(c.Items[i]["pubDate"]).ToShortDateString()));
                        }
                        catch
                        {
                        }
                    }
                    if (ShowDescription)
                    {
                        if (this.DescriptionLimit > 0)
                        {
                            HTMLResults.Append(String.Format("<div class=\"rss-description\">{0}</div>", TruncateAtWord(RemoveHtmlTags(c.Items[i]["description"]), this.DescriptionLimit)));
                        }
                        else //if description limit is 0, show full description
                        {
                            HTMLResults.Append(String.Format("<div class=\"rss-description\">{0}</div>", RemoveHtmlTags(c.Items[i]["description"])));
                        }
                    }
                    HTMLResults.Append("</div>");
                }
                HTMLResults.Append("</div>");
                results.Text = HTMLResults.ToString();
                this.Controls.Add(results);
            }
            catch
            {
                Literal messageLiteral = new Literal();
                messageLiteral.Text = "An error occurred while checking the rss feed";
                this.Controls.Add(messageLiteral);
            }
        }
    }
}
