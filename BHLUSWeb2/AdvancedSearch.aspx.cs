using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace MOBOT.BHL.Web2
{
    public partial class AdvancedSearch : BrowsePage
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
            Part,
            Author,
            Subject,
            Name,
            Annotation
        }

        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            main.Page.Title = String.Format(ConfigurationManager.AppSettings["PageTitle"], "Advanced Search");

            if (!this.IsPostBack)
            {
                // Initial populate of controls 
                BHLProvider provider = new BHLProvider();
                List<Language> languages = null;

                // Cache the results of the languages query
                String cacheKey = "LanguagesWithPubItems";
                if (Cache[cacheKey] != null)
                {
                    // Use cached version
                    languages = (List<Language>)Cache[cacheKey];
                }
                else
                {
                    // Refresh cache
                    languages = provider.LanguageSelectWithPublishedItems();
                    Cache.Add(cacheKey, languages, null, DateTime.Now.AddMinutes(
                        Convert.ToDouble(ConfigurationManager.AppSettings["LanguageListQueryCacheTime"])),
                        System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.Normal, null);
                }

                List<Collection> collections = null;

                // Cache the results of the collections query
                cacheKey = "CollectionListActive";
                if (Cache[cacheKey] != null)
                {
                    // Use cached version
                    collections = (List<Collection>)Cache[cacheKey];
                }
                else
                {
                    // Refresh cache
                    collections = provider.CollectionSelectActive();
                    Cache.Add(cacheKey, collections, null, DateTime.Now.AddMinutes(
                        Convert.ToDouble(ConfigurationManager.AppSettings["CollectionListQueryCacheTime"])),
                        System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.Normal, null);
                }

                // Fill the dropdowns
                ddlBookLanguage.DataSource = languages;
                ddlBookCollection.DataSource = collections;

                ddlBookLanguage.DataBind();
                ddlBookCollection.DataBind();

                ddlBookLanguage.Items.Insert(0, new System.Web.UI.WebControls.ListItem("(Any Language)", ""));
                ddlBookCollection.Items.Insert(0, new System.Web.UI.WebControls.ListItem("(Any Collection)", ""));

                // Set the starting tab
                if (!string.IsNullOrEmpty((string)RouteData.Values["searchtype"]))
                {
                    switch (((string)RouteData.Values["searchtype"]).ToLower())
                    {
                        case "book":
                            startTabDiv = "divBookSearch";
                            break;
                        case "author":
                            startTabDiv = "divAuthorSearch";
                            break;
                        case "subject":
                            startTabDiv = "divSubjectSearch";
                            break;
                        case "name":
                            startTabDiv = "divNameSearch";
                            break;
                        case "part":
                            startTabDiv = "divArticleSearch";
                            break;
                        default:
                            startTabDiv = "divBookSearch";
                            break;
                    }
                }
            }
        }

        protected void btnSearchTitle_Click(object sender, EventArgs e)
        {
            Response.Redirect("/search?SearchTerm=" + Server.UrlEncode(txtBookTitle.Text) +
                "&lname=" + Server.UrlEncode(txtBookAuthorLastName.Text) +
                "&vol=" + Server.UrlEncode(txtBookVolume.Text) +
                "&ed=" + Server.UrlEncode(txtBookEdition.Text) +
                "&yr=" + Server.UrlEncode(txtBookYear.Text) +
                "&subj=" + Server.UrlEncode(txtBookSubject.Text) +
                "&lang=" + Server.UrlEncode(ddlBookLanguage.SelectedValue) +
                "&col=" + Server.UrlEncode(ddlBookCollection.SelectedValue) +
                "&SearchCat=T&return=ADV");
        }

        protected void btnSearchArticle_Click(object sender, EventArgs e)
        {
            Response.Redirect("/search?SearchTerm=" + Server.UrlEncode(txtArticleTitle.Text) +
                "&cont=" + Server.UrlEncode(txtJournalTitle.Text) +
                "&lname=" + Server.UrlEncode(txtArticleAuthor.Text) +
                "&yr=" + Server.UrlEncode(txtArticleYear.Text) +
                "&SearchCat=SG&return=ADV");
        }

        protected void btnSearchAuthor_Click(object sender, EventArgs e)
        {
            string authorName = string.Empty;

            // Build the search string
            if (!string.IsNullOrEmpty(txtAuthorLastName.Text))
            {
                authorName = txtAuthorLastName.Text;
                if (!string.IsNullOrEmpty(txtAuthorFirstName.Text)) authorName += ", " + txtAuthorFirstName.Text;
            }
            else
            {
                authorName = txtAuthorCorporateName.Text;
            }

            // Execute the search
            Response.Redirect("/search?SearchTerm=" + Server.UrlEncode(authorName) + "&SearchCat=A&return=ADV");
        }

        protected void btnSearchSubject_Click(object sender, EventArgs e)
        {
            Response.Redirect("/search?SearchTerm=" + Server.UrlEncode(txtSubject.Text) + "&SearchCat=S&return=ADV");
        }

        protected void btnSearchName_Click(object sender, EventArgs e)
        {
            Response.Redirect("/search?SearchTerm=" + Server.UrlEncode(txtName.Text) + "&SearchCat=M&return=ADV");
        }
    }
}