using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MOBOT.BHL.Web.Utilities;

namespace MOBOT.BHL.AdminWeb.Controls
{
    public partial class WebStatsNavControl : System.Web.UI.UserControl
    {
        private int _applicationID = 0;
        public int ApplicationID
        {
            get { return _applicationID; }
            set { _applicationID = value; }
        }

        private string _menu = "v1";

        /// <summary>
        /// Valid values are "v1" for API v1, "v2" for API v2, and "ou" for OpenUrl.  "v1" is the default value.
        /// </summary>
        public string Menu
        {
            get { return _menu; }
            set { _menu = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Set the menu title
                litTitle.Text = new RequestLog().SelectApplicationName(ApplicationID);

                // Set all navigation links
                dailyLink.NavigateUrl = string.Format(dailyLink.NavigateUrl, ApplicationID.ToString(), Menu);
                hourlyLink.NavigateUrl = string.Format(hourlyLink.NavigateUrl, ApplicationID.ToString(), Menu);
                ipLink.NavigateUrl = string.Format(ipLink.NavigateUrl, ApplicationID.ToString(), Menu);
                userLink.NavigateUrl = string.Format(userLink.NavigateUrl, ApplicationID.ToString(), Menu);
                historyLink.NavigateUrl = string.Format(historyLink.NavigateUrl, ApplicationID.ToString(), Menu);
                resultsLink.NavigateUrl = string.Format(resultsLink.NavigateUrl, ApplicationID.ToString(), Menu);

                // Enable/disable menus as appropriate
                switch (Menu)
                {
                    case "v2":
                        {
                            userMenu.Visible = true;
                            break;
                        }
                    case "ou":
                        {
                            resultsMenu.Visible = true;
                            break;
                        }
                    case "v1":
                    default:
                        {
                            break;
                        }
                }
            }
        }
    }
}