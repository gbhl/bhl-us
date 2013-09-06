using System;
using System.Configuration;

namespace MOBOT.BHL.Web2.controls
{
    public partial class MendeleyShareControl : System.Web.UI.UserControl
    {
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

        private int segmentID = 0;

        public int SegmentID
        {
            get { return segmentID; }
            set { segmentID = value; }
        }

        public string ItemPageUrl = ConfigurationManager.AppSettings["ItemPageUrl"];
        public string PartPageUrl = ConfigurationManager.AppSettings["PartPageUrl"];

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // If on the production site, and a Title or Item identifier has been supplied, show the button
                if ((ConfigurationManager.AppSettings["IsProduction"] == "true") && (TitleID != 0 || ItemID != 0 || SegmentID != 0))
                {
                    aMendeley.Visible = true;
                    String currentUrl = String.Empty;
                    if (TitleID != 0) currentUrl = String.Format(ConfigurationManager.AppSettings["BibPageUrl"], TitleID.ToString());
                    if (ItemID != 0) currentUrl = String.Format(ConfigurationManager.AppSettings["ItemPageUrl"], ItemID.ToString());
                    if (SegmentID != 0) currentUrl = String.Format(ConfigurationManager.AppSettings["PartPageUrl"], SegmentID.ToString());
                    aMendeley.Attributes["href"] = String.Format(aMendeley.Attributes["href"], Server.UrlEncode(currentUrl));
                }
            }
        }
    }
}