using MOBOT.BHL.DataObjects;
using System;
using System.Configuration;
using System.Web.UI;

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
            Response.Redirect("/search?SearchTerm=" + Server.UrlEncode(txtBookTitle.Text) +
                "&lname=" + Server.UrlEncode(txtBookAuthorLastName.Text) +
                "&vol=" + Server.UrlEncode(txtBookVolume.Text) +
                "&ed=" + Server.UrlEncode(txtBookEdition.Text) +
                "&yr=" + Server.UrlEncode(txtBookYear.Text) +
                "&col=4&SearchCat=T&return=dl");
        }

        protected void btnSearchAnnotation_Click(object sender, EventArgs e)
        {
            // This should trigger a search of both annotations and annotation index terms.
            // "return" argument should be used to redirect the "New Search" link on search.aspx back to this page
            Response.Redirect("/search?anno=" + Server.UrlEncode(txtAnnotationText.Text) +
                "&SearchTerm=" + Server.UrlEncode(txtAnnoTitle.Text) +
                "&lname=" + Server.UrlEncode(txtAnnoAuthorLastName.Text) +
                "&vol=" + Server.UrlEncode(txtAnnoVolume.Text) +
                "&ed=" + Server.UrlEncode(txtAnnoEdition.Text) +
                "&yr=" + Server.UrlEncode(txtAnnoYear.Text) +
                "&col=4&SearchCat=O&return=dl");
        }

    }
}