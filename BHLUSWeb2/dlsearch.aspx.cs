using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MOBOT.BHL.Web.Utilities;
using MOBOT.BHL.Server;
using CustomDataAccess;
using MOBOT.BHL.DataObjects;

namespace MOBOT.BHL.Web2
{
    public partial class dlsearch : BasePage
    {
        private TabName _startTab = TabName.Book;
        public string startTabDiv = "divBookSearch";

        public TabName StartTab
        {
            get { return _startTab; }
            set { _startTab = value; }
        }

        public enum TabName
        {
            Book,
            Annotation
        }

        protected void Page_Load(object sender, EventArgs e)
        {
          
            if (!IsPostBack)
            {
                Page.Title = String.Format(ConfigurationManager.AppSettings["PageTitle"], "Darwin's Library Search");
               // ((Main)Page.Master).SetTweetMessage(String.Format(ConfigurationManager.AppSettings["TweetMessage"], "Darwin's Library Search"));

                // Set the starting tab
                switch (StartTab)
                {
                    case TabName.Book:
                        startTabDiv = "divBookSearch";
                        break;
                    case TabName.Annotation:
                        startTabDiv = "divAnnotationSearch";
                        break;
                }
            }

        }

        protected void btnSearchTitle_Click(object sender, EventArgs e)
        {
            // "return" argument should be used to redirect the "New Search" link on search.aspx back to this page
            Response.Redirect("/search?SearchTerm=" + Server.HtmlEncode(txtBookTitle.Text) +
                "&lname=" + Server.HtmlEncode(txtBookAuthorLastName.Text) +
                "&vol=" + Server.HtmlEncode(txtBookVolume.Text) +
                "&ed=" + Server.HtmlEncode(txtBookEdition.Text) +
                "&yr=" + Server.HtmlEncode(txtBookYear.Text) +
                "&col=4&SearchCat=T&return=dl");
        }

        protected void btnSearchAnnotation_Click(object sender, EventArgs e)
        {
            // This should trigger a search of both annotations and annotation index terms.
            // "return" argument should be used to redirect the "New Search" link on search.aspx back to this page
            Response.Redirect("/search?anno=" + Server.HtmlEncode(txtAnnotationText.Text) +
                "&SearchTerm=" + Server.HtmlEncode(txtAnnoTitle.Text) +
                "&lname=" + Server.HtmlEncode(txtAnnoAuthorLastName.Text) +
                "&vol=" + Server.HtmlEncode(txtAnnoVolume.Text) +
                "&ed=" + Server.HtmlEncode(txtAnnoEdition.Text) +
                "&yr=" + Server.HtmlEncode(txtAnnoYear.Text) +
                "&col=4&SearchCat=O&return=dl");
        }

    }
}