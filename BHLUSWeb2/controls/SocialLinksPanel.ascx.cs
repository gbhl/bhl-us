using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MOBOT.BHL.Web2
{
    public partial class SocialLinksPanel : System.Web.UI.UserControl
    {
        private String width = "0";

        public String Width
        {
            get { return width; }
            set { width = value; }
        }

        private String tweetMessage = string.Empty;

        public String TweetMessage
        {
            get { return tweetMessage; }
            set { tweetMessage = value; }
        }

        private int titleID = 0;

        public int TitleID
        {
            get { return titleID; }
            set { titleID = value; }
        }

        private int itemID = 0;

        public int ItemID
        {
            get { return itemID; }
            set { itemID = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Set the width of the control
                int widthInt = 0;
                Int32.TryParse(Width, out widthInt);
                //socialLinksUserControl.Width = widthInt;

                // Set the facebook "Like" URL
                String currentUrl = String.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Request.RawUrl);
                ifrmFB.Attributes["src"] = String.Format(ifrmFB.Attributes["src"], Server.UrlEncode(currentUrl));

                // Set the Twitter "Tweet" text
                if (!String.IsNullOrEmpty(TweetMessage)) aTweet.Attributes.Add("data-text", TweetMessage);

                // If not on the production site, hide the "Like" and "Tweet" buttons
                if (ConfigurationManager.AppSettings["IsProduction"] != "true")
                {
                    divLike.Visible = false;
                    divDemo.Visible = true;
                }
            }
        }
    }
}